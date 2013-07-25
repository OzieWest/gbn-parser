using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Metacritic;
using GetByNameLibrary.Utilities;
using ReturnValues;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Controllers
{
	public class MetacriticController: ICompile
	{
		public IMetacritic _metacriticParser;
		public ISerializer _serializer;
		public ILogger _logger;

		public MetacriticController()
		{
			_metacriticParser = this.LoadConfig();
		}

		public AsyncRetValue<Boolean> AsyncCompile(Action method)
		{
			return _metacriticParser.AsyncStartParse(method);
		}

		MetacriticParser LoadConfig()
		{
			return _serializer.Load<MetacriticParser>(@"configs\MetacriticParser.config");
		}
	}
}
