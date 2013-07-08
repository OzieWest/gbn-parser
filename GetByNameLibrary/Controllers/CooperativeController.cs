﻿using GetByNameLibrary.Cooperatives;
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
		CooperativeParser _cooperativeParser;
		ISerializer _serializer;
		ILogger _logger;

		public CooperativeController()
		{
			_serializer = new JsonSerializer();
			_logger = new TxtLogger() { FileName = @"logs\" + DateTime.Today.ToShortDateString() + ".logs" };

			_cooperativeParser = this.LoadConfig();
			_cooperativeParser.WebDownloader = new WebDownloader();
			_cooperativeParser.Serializer = _serializer;
			_cooperativeParser.Logger = _logger;
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
