using GetByNameLibrary.Cooperatives;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParser.Cooperative
{
	public class CooperativeController : ICompile
	{
		ICooperative _cooperativeParser;
		ISerializer _serializer;
		TxtLogger _logger;

		public CooperativeController()
		{
			_serializer = new JsonSerializer();
			_logger = new TxtLogger(@"logs\" + DateTime.Today.ToShortDateString() + ".logs");

			_cooperativeParser = this.LoadConfig();
			_cooperativeParser.WebDownloader = new WebDownloader();
			_cooperativeParser.Serializer = _serializer;
			_cooperativeParser.Logger = _logger;
		}

		public RetValue<Boolean> Compile()
		{
			return _cooperativeParser.StartParser();
		}

		CooperativeParser LoadConfig()
		{
			return _serializer.Load<CooperativeParser>(@"configs\CooperativeParser.config");
		}
	}
}
