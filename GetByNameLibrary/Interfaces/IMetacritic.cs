﻿using GetByNameLibrary.Domains;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface IMetacritic : IParser<MetaEntry>
	{
		String SiteUrl { get; set; }
		String ParseUrl { get; set; }
		String FileName { get; set; }

		ISerializer Serializer { get; set; }
		IWebDownloader WebDownloader { get; set; }
		ILogger Logger { get; set; }
	}
}
