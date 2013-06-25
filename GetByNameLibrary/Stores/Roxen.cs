using GetByNameLibrary.Domains;
using HtmlAgilityPack;
using System;

namespace GetByNameLibrary.Stores
{
	public class Roxen : Store
	{
		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='area_game']");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var sale = node.SelectSingleNode(".//span[@class='discount']") != null ? true : false;

					var tempCost = node.SelectSingleNode(".//p[@class='price']");

					if (tempCost != null)
					{
						var cost = tempCost.InnerText.Replace(" i", "");

						var tempTitle = node.SelectSingleNode(".//a[@class='game_title_a']");
						var title = tempTitle.InnerText.Replace("\t", "");
						var searchString = DelBadChar(ref title);

						var gameUrl = StoreUrl + tempTitle.GetAttributeValue("href", String.Empty);

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
