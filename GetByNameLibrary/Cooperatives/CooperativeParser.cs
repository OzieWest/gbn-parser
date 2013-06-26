using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using HtmlAgilityPack;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GetByNameLibrary.Cooperatives
{
	public class CooperativeParser
	{
		public String SiteUrl { get; set; }
		public String ParseUrl { get; set; }
		public String FileName { get; set; }

		List<CoopEntry> _entries;

		ISerializer _serializer;
		IWebDownloader _webDownloader;
		TxtLogger _logger;

		public CooperativeParser()
		{
			SiteUrl = "http://www.co-optimus.com";
			ParseUrl = @"http://www.co-optimus.com/system.php?id=4&page=";
			FileName = "coops";

			_entries = new List<CoopEntry>();

			_webDownloader = new WebDownloader();
			_serializer = new JsonSerializer();
			_logger = new TxtLogger(@"logs\cooperativeController.logs");
		}

		public List<CoopEntry> GetEntries()
		{
			return _entries;
		}

		protected String GetPage(String path)
		{
			return _webDownloader.GetPage(path);
		}

		protected void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//div[@id='pagebody-left']//table//tr");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var tempName = node.SelectSingleNode(".//td[@class='gamename']//a");

					if (tempName != null)
					{
						var name = tempName.InnerText;

						var collumns = node.SelectNodes(".//td[@align='center']").Elements().ToList();

						var offline = collumns[0].InnerText;
						var online = collumns[1].InnerText;
						var lan = collumns[2].InnerText;
						var coopmode = collumns[3].OuterHtml.Contains("x.png") ? "No" : "Yes";
						var coopcomp = collumns[4].OuterHtml.Contains("x.png") ? "No" : "Yes";

						_entries.Add(new CoopEntry()
						{
							Name = name,
							Offline = offline,
							Online = online,
							Lan = lan,
							CoopMode = coopmode,
							CoopComp = coopcomp
						});
					}
				}
			}
		}

		//http://www.co-optimus.com/system.php?id=4&page=
		public RetValue<Boolean> StartParser()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var pages = new List<String>();

				var doc = new HtmlDocument();
				doc.LoadHtml(this.GetPage(ParseUrl + "0"));

				var nodes = doc.DocumentNode.SelectNodes("//div[@class='pagerNav']//li//a");
				if (nodes != null)
				{
					foreach (var node in nodes)
					{
						var link = SiteUrl + node.GetAttributeValue("href", String.Empty);
						pages.Add(this.GetPage(link));
					}
				}

				pages.ForEach((item) => { this.Parse(item); });
				this.SaveEntries();

				result.Value = true;
				result.Description = String.Format("{0}", _entries.Count);
			}
			catch (Exception ex) 
			{
				result.Value = false;
				result.Description = ex.Message;
				_logger.AddEntry(ex.ToString(), MessageType.Error);
				_logger.WriteLogs();
			}

			return result;
		}

		protected void SaveEntries()
		{
			_serializer.Save<List<CoopEntry>>(_entries, @"completed\" + FileName + ".json");
		}
	}
}
