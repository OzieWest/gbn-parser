using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using HtmlAgilityPack;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GetByNameLibrary.Stores
{
	public class Origin : BaseStore
	{
		public Origin()
		{
			_logger = new TxtLogger(@"logs\" + FileName + ".logs", true);
		}

		public override RetValue<Boolean> StartParse()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var pages = new List<String>();

				var tempDocs = new List<String>();

				var doc = new HtmlDocument();
				doc.LoadHtml(GetPage(PageUrl + "0"));

				var nodes = doc.DocumentNode.SelectNodes("//div[@id='dr_totalSize']/select");
				if (nodes != null)
				{
					foreach (var node in nodes.Elements("option"))
						tempDocs.Add(PageUrl + node.GetAttributeValue("value", String.Empty));
				}
				tempDocs.Distinct().ToList().ForEach((item) => { pages.Add(GetPage(item)); });

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

		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//table[@id='dr_categoryProductListTbl']//tr");
			if (nodes != null)
			{
				foreach (var node in nodes)
				{
					var platform = node.SelectSingleNode(".//td[@class='dr_platform']");

					if (platform != null)
					{
						if (platform.InnerText != "Mac")
						{
							var tempTitle = node.SelectSingleNode(".//td[@class='dr_productName']//a");

							if (tempTitle != null)
							{
								var sale = false;
								var title = tempTitle.InnerText;

								var searchString = DelBadChar(ref title);
								var gameUrl = tempTitle.GetAttributeValue("href", String.Empty);

								var cost = String.Empty;
								var tempCost = node.SelectSingleNode(".//span[@class='white2b']/span[@class='pricebold']");

								if (tempCost != null)
								{
									cost = tempCost.InnerText;
									sale = true;
								}
								else
								{
									tempCost = node.SelectSingleNode(".//span[@class='pricebold']");
									cost = tempCost.InnerText;
								}

								int costCont;
								if ((costCont = cost.IndexOf("руб.")) > -1)
									cost = cost.Substring(0, costCont);

								cost = cost.Replace(",", ".")
										   .Replace(" ", ""); //аномальный пробел - не удалять

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
}
