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
using System.Threading;
using System.Web.Script.Serialization;

namespace GetByNameLibrary.Cooperatives
{
	public class CooperativeParser : ICooperative
	{
		public String SiteUrl { get; set; }
		public String ParseUrl { get; set; }
		public String FileName { get; set; }

		[ScriptIgnore]
		public ISerializer Serializer { get; set; }
		[ScriptIgnore]
		public IWebDownloader WebDownloader { get; set; }
		[ScriptIgnore]
		public ILogger Logger { get; set; }

		List<CoopEntry> _entries;

		public CooperativeParser()
		{
			_entries = new List<CoopEntry>();
		}

		public List<CoopEntry> GetEntries()
		{
			return _entries;
		}

		public AsyncRetValue<Boolean> AsyncStartParse(Action method)
		{
			var result = new AsyncRetValue<Boolean>();
			result.SetWorker(() =>
			{
				try
				{
					var pages = new List<String>();

					var doc = new HtmlDocument();
					doc.LoadHtml(this.GetPage(ParseUrl + "1"));

					var nodes = doc.DocumentNode.SelectNodes("//ul[@class='pagination']//li//a");

					if (nodes != null)
					{
						foreach (var node in nodes)
							pages.Add(node.InnerText);
					}

					int maxPage = 0;

					if (pages.Count > 3)
						maxPage = Convert.ToInt32(pages[pages.Count - 3]);

					if (maxPage != 0)
					{
						pages = new List<String>();

						result.SetProgressRange(0, nodes.Count);

						for (var i = 0; i < maxPage; i++)
						{
							pages.Add(this.GetPage(ParseUrl + i));
							result.MoveProgress(i);
						}
					}

					pages.ForEach((item) => { this.Parse(item); });
					this.SaveEntries();

					result.Value = true;
					result.Description = String.Format("{0}", _entries.Count);
					result.Complete();
				}
				catch (Exception ex)
				{
					result.AbortProgress(false, ex.Message);

					Logger.Error(ex.ToString());
					Logger.WriteLogs();
				}
			});

			result.OnComplete(method);
			result.StartWork();

			return result;
		}

		protected String GetPage(String path)
		{
			return WebDownloader.GetPage(path);
		}

		protected void Parse(String page)
		{
			var gameInfoPages = new List<String>();

			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//table[@id='results_table']//tbody//tr");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var id = node.GetAttributeValue("id", true);
					gameInfoPages.Add(@"http://www.co-optimus.com/ajax/ajax_gameInfo.php/"+id);
				}
			}

			nodes = doc.DocumentNode.SelectNodes("//table[@id='results_table']//tbody//tr");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var id = node.GetAttributeValue("id", true);
					gameInfoPages.Add(@"http://www.co-optimus.com/ajax/ajax_gameInfo.php/" + id);
				}
			}

					//var tempName = node.SelectSingleNode(".//td[@class='gamename']//a");

					//if (tempName != null)
					//{
					//	var name = tempName.InnerText;

					//	var collumns = node.SelectNodes(".//td[@align='center']").Elements().ToList();

					//	var offline = collumns[0].InnerText;
					//	var online = collumns[1].InnerText;
					//	var lan = collumns[2].InnerText;
					//	var coopmode = collumns[3].OuterHtml.Contains("x.png") ? "No" : "Yes";
					//	var coopcomp = collumns[4].OuterHtml.Contains("x.png") ? "No" : "Yes";

					//	_entries.Add(new CoopEntry()
					//	{
					//		Name = name,
					//		Offline = offline,
					//		Online = online,
					//		Lan = lan,
					//		CoopMode = coopmode,
					//		CoopComp = coopcomp
					//	});
					//}
			
		}

		protected void SaveEntries()
		{
			Serializer.Save<List<CoopEntry>>(_entries, @"completed\" + FileName + ".json");
		}
	}
}
