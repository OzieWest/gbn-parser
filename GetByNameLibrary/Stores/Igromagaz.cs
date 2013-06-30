using GetByNameLibrary.Domains;
using HtmlAgilityPack;
using SimpleLogger;
using System;

namespace GetByNameLibrary.Stores
{
	public class Igromagaz : BaseStore
	{
		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='product-card-prv clearfix']");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					Boolean sale = false;

					var tempTitle = node.SelectSingleNode(".//a[@class='product-card-prv__title']");
					var title = tempTitle.InnerText;
					var searchString = DelBadChar(ref title);

					var gameUrl = StoreUrl + tempTitle.GetAttributeValue("href", String.Empty);

					var cost = String.Empty;
					var tempCost = node.SelectSingleNode(".//span[@class='product-card-prv__prices__new']");
					if (tempCost != null)
					{
						cost = tempCost.InnerText.Substring(0, tempCost.InnerText.IndexOf("р."));
						sale = true;
					}
					else
					{
						var tempC = node.SelectSingleNode(".//span[@class='product-card-prv__prices__current']").InnerText;
						cost = tempC.Substring(0, tempC.IndexOf("р."));
					}

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
