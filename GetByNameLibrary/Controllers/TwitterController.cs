using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Twitter;
using GetByNameLibrary.Utilities;
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
		ITwitter _twitterParser;

		public TwitterController()
		{
			_twitterParser = new TwitterParser();
			_twitterParser.Serializer = new JsonSerializer();
			_twitterParser.Logger = new TxtLogger(@"logs\" + DateTime.Today.ToShortDateString() + ".logs", true);
			_twitterParser.EntriesCount = 10;
		}

		public RetValue<Boolean> Compile()
		{
			var result = _twitterParser.StartParser();

			if (result.Value)
				result.Description = "Count: " + _twitterParser.GetEntries().Count.ToString();

			return result;
		}
	}
}
