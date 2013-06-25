using GetByNameLibrary.Domains;
using HtmlAgilityPack;
using System;

namespace GetByNameLibrary.Stores
{
	public class Gamazavr : Store
	{
		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='goodList']//a[@class='item']");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var title = node.GetAttributeValue("title", String.Empty);
					var searchString = DelBadChar(ref title);
					var gameUrl = StoreUrl + node.GetAttributeValue("href", String.Empty);
					var cost = node.SelectSingleNode(".//span[@class='price']//b").InnerText;
					var sale = node.SelectSingleNode(".//span[@class='price']//s") != null ? true : false;

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
