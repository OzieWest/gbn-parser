using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using ReturnValues;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace GetByNameLibrary.Stores
{
	[Serializable]
	public class BaseStoreParser
	{
		public String StoreUrl { get; set; }
		public String FileName { get; set; }
		public int PageCount { get; set; }
		public String PageUrl { get; set; }

		protected List<GameEntry> _entries;

		public IReplacer _replacer;
		public IWebDownloader _webDownloader;
		public ISerializer _serializer;
		public ILogger _logger;

		//TODO: Подключить Springframework
		public BaseStoreParser()
		{
			_replacer = new Replacer();
			_webDownloader = new WebDownloader();
			_serializer = new JsonSerializer();

			_logger = new TxtLogger();
			_logger.FileName = @"logs\" + DateTime.Today.ToShortDateString() + ".logs";

			_entries = new List<GameEntry>();
		}

		protected virtual void Parse(String page) { }

		public virtual AsyncRetValue<Boolean> AsyncStartParse(Action<AsyncRetValue<Boolean>> method)
		{
			var result = new AsyncRetValue<Boolean>();
			result.SetProgressRange(0, PageCount);

			result.SetWorker(() =>
			{
				var _pages = new List<String>();
				try
				{
					for (int i = 0; i < PageCount; i++)
					{
						_pages.Add(GetPage(PageUrl + (i + 1).ToString()));
						result.MoveProgress(i);
					}

					_pages.ForEach((page) => { this.Parse(page); });
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
			}, FileName);

			result.OnComplete(method);
			result.StartWork();

			return result;
		}

		public virtual List<GameEntry> GetEntries()
		{
			return _entries;
		}

		protected virtual String GetPage(String path)
		{
			return _webDownloader.GetPage(path);
		}

		//TODO: сохранять файлы в разных папках
		protected virtual void SaveEntries()
		{
			_serializer.Save<List<GameEntry>>(_entries, @"incompleted\" + FileName + ".json");
		}

		protected virtual String DelBadChar(ref String source)
		{
			return _replacer.DelBadChar(ref source);
		}
	}
}
