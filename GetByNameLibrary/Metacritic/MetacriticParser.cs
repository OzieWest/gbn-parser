using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using HtmlAgilityPack;
using ReturnValues;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GetByNameLibrary.Metacritic
{
	public class MetacriticParser : IMetacritic
	{
		[ScriptIgnore]
		public IWebDownloader WebDownloader { get; set; }
		[ScriptIgnore]
		public ISerializer Serializer { get; set; }
		[ScriptIgnore]
		public ILogger Logger { get; set; }

		public String SiteUrl { get; set; }
		public String ParseUrl { get; set; }
		public String FileName { get; set; }

		List<MetaEntry> _entries;

		public MetacriticParser()
		{
			_entries = new List<MetaEntry>();
		}

		public List<MetaEntry> GetEntries()
		{
			return _entries;
		}

		public AsyncRetValue<Boolean> AsyncStartParse(Action method)
		{
			int Count = 2;

			var result = new AsyncRetValue<Boolean>();
			result.SetProgressRange(0, Count);

			result.SetWorker(() =>
			{
				try
				{
					for (int i = 0; i < Count; i++)
					{
						this.Parse(this.GetPage(ParseUrl + i.ToString()));
						result.MoveProgress(i);
					}

					this.SaveEntries();

					result.Value = true;
					result.Description = String.Format("count: {0}", _entries.Count);
				}
				catch (Exception ex)
				{
					result.AbortProgress(false, ex.Message);

					Logger.Error(ex.ToString());
					Logger.WriteLogs();
				}

				result.Complete();
			});

			result.OnComplete(method);

			result.StartWork();

			return result;
		}

		String GetPage(String path)
		{
			return WebDownloader.GetPage(path);
		}

		void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//ol[@class='list_products list_product_condensed']");
			if (nodes != null)
			{
				foreach (var node in nodes.Elements("li"))
				{
					var name = node.SelectSingleNode(".//div[@class='basic_stat product_title']//a").InnerHtml;

					var tempMetaScore = node.SelectNodes(".//div[@class='score_wrap']//span")[1].InnerText;
					var tempUserScore = node.SelectNodes(".//li[@class='stat product_avguserscore']//span")[1].InnerHtml.Replace(".", "");

					var metaScore = tempMetaScore.Equals("tbd") ? 0 : Int32.Parse(tempMetaScore);
					var userScore = tempUserScore.Equals("tbd") ? 0 : Int32.Parse(tempUserScore);

					var date = node.SelectSingleNode(".//li[@class='stat release_date']//span[@class='data']").InnerHtml;

					_entries.Add(new MetaEntry()
					{
						Name = name,
						MetaScore = metaScore,
						UserScore = userScore,
						Date = date
					});
				}
			}
		}

		void SaveEntries()
		{
			Serializer.Save<List<MetaEntry>>(_entries, @"completed\" + FileName + ".json");
		}
	}
}
