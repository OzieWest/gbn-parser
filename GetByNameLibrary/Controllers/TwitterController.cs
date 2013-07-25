using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Twitter;
using GetByNameLibrary.Utilities;
using ReturnValues;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace GetByNameLibrary.Controllers
{
	public class TwitterController : ICompile
	{
		public ITwitter _twitterParser;

		public TwitterController()
		{
			//_twitterParser = new TwitterParser();
			//_twitterParser.Serializer = new JsonSerializer();
			//_twitterParser.Logger = new TxtLogger() { FileName = @"logs\" + DateTime.Today.ToShortDateString() + ".logs" };
			//_twitterParser.EntriesCount = 10;
		}

		public AsyncRetValue<Boolean> AsyncCompile(Action method)
		{
			return _twitterParser.AsyncStartParse(method);
		}
	}
}
