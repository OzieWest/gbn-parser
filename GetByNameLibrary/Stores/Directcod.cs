using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using HtmlAgilityPack;
using ReturnValues;
using SimpleLogger;
using System;
using System.Collections.Generic;

namespace GetByNameLibrary.Stores
{
	public class Directcod : BaseStore
	{
		public override RetValue<Boolean> StartParse()
		{
			var result = new RetValue<Boolean>();
			try
			{
				this.Parse(GetPage(PageUrl));
				this.SaveEntries();

				result.Value = true;
				result.Description = String.Format("{0}", _entries.Count);
			}
			catch (Exception ex)
			{
				result.Value = false;
				result.Description = ex.Message;
				_logger.Error(ex.ToString());
				_logger.WriteLogs();
			}
			return result;
		}

		protected override void Parse(String page)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(page);

			var nodes = doc.DocumentNode.SelectNodes("//div[@class='goodListSml double']");

			if (nodes != null)
			{
				var names = new List<String>();
				var links = new List<String>();
				var costs = new List<String>();
				var salesGameIndex = new List<int>();

				foreach (var node in nodes.Elements())
				{
					if (node.Name == "a")
					{
						names.Add(node.InnerText);
						links.Add(StoreUrl + node.GetAttributeValue("href", String.Empty));
					}
					else if (node.Name == "div")
					{
						String cost;

						var tempCost = node.SelectSingleNode(".//i");
						if (tempCost != null)
						{
							cost = tempCost.InnerText;
							costs.Add(cost);
							salesGameIndex.Add(costs.Count - 1);
						}
						else
						{
							cost = node.InnerText
									   .Replace("\t", "")
									   .Replace("\n", "")
									   .Replace("\r", "");

							int ind;
							if ((ind = cost.IndexOf("&nbsp;")) > -1)
							{
								cost = cost.Substring(0, ind);
								costs.Add(cost);
							}
						}
					}
				}

				if (names.Count == costs.Count && names.Count == links.Count)
				{
					for (int i = 0; i < names.Count; i++)
					{
						Boolean sale = false;

						String title = names[i];
						String searchString = this.DelBadChar(ref title);
						String gameUrl = links[i];
						String cost = costs[i];

						salesGameIndex.ForEach((item) =>
						{
							if (item == i)
								sale = true;
						});

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
