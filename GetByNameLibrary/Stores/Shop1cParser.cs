using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using HtmlAgilityPack;
using ReturnValues;
using SimpleLogger;
using System;

namespace GetByNameLibrary.Stores
{
	public class Shop1cParser : BaseStoreParser
	{
		public override AsyncRetValue<Boolean> AsyncStartParse(Action<AsyncRetValue<Boolean>> method)
		{
			var result = new AsyncRetValue<Boolean>();
			result.SetProgressRange(0, 1);

			result.SetWorker(() =>
			{
				try
				{
					this.Parse(this.GetPage(PageUrl));
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
