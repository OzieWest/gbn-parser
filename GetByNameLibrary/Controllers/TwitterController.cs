using GetByNameLibrary.Twitter;
using GetByNameLibrary.Utilities;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Controllers
{
	public class TwitterController
	{
		TxtLogger _logger;

		public TwitterController() 
		{
			_logger = new TxtLogger(@"logs\twitterController.logs", true);
		}

		public RetValue<Boolean> CompileTweets()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var twitterParser = new TwitterParser();
				var grabStatus = twitterParser.GrabTimeLine(10);

				if (grabStatus)
				{
					twitterParser.SaveEntries();
					result.Description = "Count: " + twitterParser.GetTweets().Count.ToString();
				}
				else
				{
					result.Description = "method |GrabTimeLine| return |false|";
				}

				result.Value = grabStatus;
				
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
	}
}
