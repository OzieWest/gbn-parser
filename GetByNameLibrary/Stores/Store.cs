using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
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
	public class Store
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

		public Store()
		{
			_replacer = new Replacer();
			_webDownloader = new WebDownloader();
			_serializer = new SerializerToJson();
			_logger = new TxtLogger(FileName + ".logs", true);

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

		protected virtual void SaveEntries()
		{
			_serializer.SaveList<GameEntry>(_entries, FileName);
		}

		protected virtual String DelBadChar(ref String source)
		{
			return _replacer.DelBadChar(ref source);
		}
	}
}
