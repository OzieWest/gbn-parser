using GetByNameLibrary.Domains;
using HtmlAgilityPack;
using SimpleLogger;
using System;

namespace GetByNameLibrary.Stores
{
	public class Shop1c : Store
	{
		public override Boolean StartParse()
		{
			var result = true;
			try
			{
				this.Parse(GetPage(PageUrl));
				this.SaveEntries();
			}
			catch (Exception ex) 
            {
                result = false;
                _logger.AddEntry(ex.ToString(), MessageType.Error);
                _logger.WriteLogs();
            }

            return result;
		}

		protected override void Parse(string page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//ul[@class='list']//li");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var sale = false; //из списка нельзя получить инфу о скидке, поэтому всегда false

					var title = node.SelectSingleNode(".//div[@class='title']").FirstChild.InnerText.Replace("\r\n", " ");
					var searchString = DelBadChar(ref title);

					var gameUrl = StoreUrl + node.SelectSingleNode(".//a").GetAttributeValue("href", String.Empty);
					var cost = node.SelectSingleNode(".//div[@class='price']//span[@class='new']").InnerText;

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
