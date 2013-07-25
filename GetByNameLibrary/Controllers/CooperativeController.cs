using GetByNameLibrary.Cooperatives;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using ReturnValues;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleParser.Cooperative
{
	public class CooperativeController : ICompile
	{
		public CooperativeParser _cooperativeParser;
		public ISerializer _serializer;
		public ILogger _logger;

		public CooperativeController()
		{
			_cooperativeParser = this.LoadConfig();
		}

		public AsyncRetValue<Boolean> AsyncCompile(Action method)
		{
			return _cooperativeParser.AsyncStartParse(method);
		}

		CooperativeParser LoadConfig()
		{
			return _serializer.Load<CooperativeParser>(@"configs\CooperativeParser.config");
		}
	}
}
