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
		IMetacritic _metacriticParser;
		ISerializer _serializer;
		ILogger _logger;

		public MetacriticController()
		{
			_serializer = new JsonSerializer();
			_logger = new TxtLogger();
			_logger.FileName = @"logs\" + DateTime.Today.ToShortDateString() + ".logs";

			_metacriticParser = this.LoadConfig();
			_metacriticParser.Logger = _logger;
			_metacriticParser.Serializer = _serializer;
			_metacriticParser.WebDownloader = new WebDownloader();
		}

		public RetValue<Boolean> Compile()
		{
			return _metacriticParser.StartParser();
		}

		MetacriticParser LoadConfig()
		{
			return _serializer.Load<MetacriticParser>(@"configs\MetacriticParser.config");
		}
	}
}
