using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
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
	public class BaseStore
	{
		public String StoreUrl { get; set; }
		public String FileName { get; set; }
		public int PageCount { get; set; }
		public String PageUrl { get; set; }

		protected List<GameEntry> _entries;

		protected IReplacer _replacer;
		protected IWebDownloader _webDownloader;
		protected ISerializer _serializer;
		protected TxtLogger _logger;

		//TODO: FileName используется раньше чем присваивается
		public BaseStore()
		{
			_replacer = new Replacer();
			_webDownloader = new WebDownloader();
			_serializer = new JsonSerializer();
			_logger = new TxtLogger(@"logs\" + FileName + ".logs", true);

			_entries = new List<GameEntry>();
		}

		protected virtual void Parse(String page) { }

		public virtual RetValue<Boolean> StartParse()
		{
			var _pages = new List<String>();

			var result = new RetValue<Boolean>();
			try
			{
				for (int i = 0; i < PageCount; i++)
					_pages.Add(GetPage(PageUrl + (i + 1).ToString()));

				_pages.ForEach((page) => { this.Parse(page); });

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
