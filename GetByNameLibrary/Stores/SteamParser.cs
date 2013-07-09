using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using GetByNameLibrary.Domains;
using SimpleLogger;
using GetByNameLibrary.Utilities;
using ReturnValues;

namespace GetByNameLibrary.Stores
{
	public class SteamParser : BaseStoreParser
	{
		public override AsyncRetValue<Boolean> AsyncStartParse(Action<AsyncRetValue<Boolean>> method)
		{
			var result = new AsyncRetValue<Boolean>();

			result.SetWorker(() =>
			{
				try
				{
					var _pages = new List<String>();

					var categoryUrls = new List<String>();
					categoryUrls.Add(@"http://store.steampowered.com/search/?sort_by=&sort_order=ASC&category1=998&page=");
					categoryUrls.Add(@"http://store.steampowered.com/search/?sort_by=&sort_order=ASC&category1=997&page=");
					categoryUrls.Add(@"http://store.steampowered.com/search/?sort_by=&sort_order=ASC&category1=996&page=");
					categoryUrls.Add(@"http://store.steampowered.com/search/?sort_by=&sort_order=ASC&category1=21&page=");

					categoryUrls.ForEach((url) =>
					{
						var tempString = this.GetPage(url + "1");

						var doc = new HtmlDocument();
						doc.LoadHtml(tempString);

						var list = doc.DocumentNode
									  .SelectSingleNode("//div[@class='search_pagination_right']")
									  .Elements("a")
									  .ToList();

						var Count = Convert.ToInt32(list[list.Count - 2].InnerText);

						result.SetProgressRange(0, Count);

						for (int i = 0; i < Count; i++)
						{
							var page = this.GetPage(url + (i + 1).ToString());
							_pages.Add(page);
							result.MoveProgress(i);
						}
					});

					_pages.ForEach((item) => { this.Parse(item); });
					this.SaveEntries();

					result.Value = true;
					result.Description = String.Format("{0}", _entries.Count);
				}
				catch (Exception ex)
				{
					result.AbortProgress(false, ex.Message);

					_logger.Error(ex.ToString());
					_logger.WriteLogs();
				}

				result.Complete();
			});

			result.OnComplete(method);
			result.StartWork();

			return result;
		}

		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var col = new List<HtmlNodeCollection>();
			col.Add(doc.DocumentNode.SelectNodes("//a[@class='search_result_row even']"));
			col.Add(doc.DocumentNode.SelectNodes("//a[@class='search_result_row odd']"));

			foreach (var colItem in col)
			{
				var tNODES = colItem;
				if (tNODES != null)
				{
					foreach (var node in tNODES)
					{
						var sale = false;

						var tempCost = String.Empty;
						var a = node.SelectSingleNode(".//div[@class='col search_price']");
						if (a.SelectSingleNode(".//strike") != null)
						{
							tempCost = a.ChildNodes[2].InnerText; // !!!!порнуха
							sale = true;
						}
						else
							tempCost = a.InnerText;

						if (tempCost.Length > 1)
						{
							var title = node.SelectSingleNode(".//div[@class='col search_name ellipsis']//h4").InnerText;
							var searchString = this.DelBadChar(ref title);
							var gameUrl = node.GetAttributeValue("href", String.Empty);
							var cost = tempCost.Contains(" p&#1091;&#1073;.") ? tempCost.Substring(0, tempCost.IndexOf(" p&#1091;&#1073;.")) : tempCost;

							_entries.Add(new GameEntry()
							{
								SearchString = searchString,
								StoreUrl = StoreUrl,
								Title = title,
								GameUrl = gameUrl,
								Cost = cost,
								Sale = sale
							});
						}
					}
				}
			}
		}
	}
}

