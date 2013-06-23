using GetByNameLibrary.Domains;
using HtmlAgilityPack;
using System;

namespace GetByNameLibrary.Stores
{
	public class Gamagama : Store
	{
		public Gamagama()
		{
			FileName = "gamagama";
			StoreUrl = "http://gama-gama.ru";
			PageCount = 73;
			PageUrl = "http://gama-gama.ru/Prod?genre1=all&genre2=all&page=";
		}

		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='catalog-row']");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					Boolean sale = false;

					var title = node.SelectSingleNode(".//span[@class='catalog_name']").InnerText;
					var searchString = DelBadChar(ref title);

					var gameUrl = StoreUrl + node.GetAttributeValue("gg-card-link", String.Empty);

					var cost = String.Empty;
					var tempCost = node.SelectSingleNode(".//div[@class='price_2']//b");

					if (tempCost != null)
						cost = tempCost.InnerText;
					else
					{
						var tempGc = node.SelectSingleNode(".//div[@class='price_1_long']//b");

						if (tempGc != null)
						{
							cost = tempGc.InnerText;
							sale = true;
						}
						else
							cost = node.SelectSingleNode(".//div[@class='price_1']//b").InnerText;
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
