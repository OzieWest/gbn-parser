using GetByNameLibrary.Domains;
using HtmlAgilityPack;
using System;

namespace GetByNameLibrary.Stores
{
    public class Yuplay : Store
    {
        protected override void Parse(String page)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(page);

            var nodes = doc.DocumentNode.SelectNodes("//div[@class='catalogue-content']");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var tempWar = node.SelectSingleNode(".//div[@class='fright warning r']");

                    if (!(tempWar != null && tempWar.InnerText.Contains("продано")))
                    {
                        var title = node.SelectSingleNode(".//h1").InnerText;
                        var searchString = DelBadChar(ref title);

                        var gameUrl = StoreUrl + node.SelectSingleNode(".//h1//a").GetAttributeValue("href", String.Empty);

                        var temp = node.SelectSingleNode(".//div[@class='buy-text']//b").InnerText;
                        var cost = temp.Contains(" руб") ? temp.Substring(0, temp.IndexOf(" руб")) : String.Empty;
                        var sale = node.InnerText.Contains("Скидка") ? true : false;

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
