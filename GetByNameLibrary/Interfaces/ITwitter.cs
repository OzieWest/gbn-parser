using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface ITwitter : IParser<TwitterEntry>
	{
		int EntriesCount { get; set; }
		ISerializer Serializer { get; set; }
		TxtLogger Logger { get; set; }
	}
}
