using GetByNameLibrary.Domains;
using SerializeLibra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace GetByNameLibrary.Twitter
{
	public class TwitterParser : ITwitterParser
	{
		List<TwitterEntry> _entries;
		ISerializer _serializer;

		public TwitterParser()
		{
			_entries = new List<TwitterEntry>();
			_serializer = new JsonSerializer();
		}

		public void SaveEntries()
		{
			_serializer.Save<List<TwitterEntry>>(_entries, @"completed\tweets.json");
		}

		public List<TwitterEntry> GetTweets()
		{
			return _entries;
		}

		public Boolean SendMessage(String msg)
		{
			throw new NotImplementedException();
		}

		public Boolean GrabTimeLine(int count)
		{
			var config = _serializer.Load<AuthConfig>(@"configs\twitter.config");

			var service = new TwitterService(config.ConsumerKey, config.ConsumerSecret);
			service.AuthenticateWith(config.AccessToken, config.AccessTokenSecret);

			var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { Count = count }).ToList();

			tweets.ForEach((tweet) =>
			{
				var item = tweet.RetweetedStatus ?? tweet;

				var date = item.CreatedDate.ToShortDateString() + " " + tweet.CreatedDate.ToShortTimeString();
				var text = item.Text;
				var idTweet = item.Id.ToString();
				var nameUser = item.User.Name;
				var imageUser = item.User.ProfileImageUrl;
				var screenName = item.User.ScreenName;

				_entries.Add(new TwitterEntry()
				{
					IdTweet = idTweet,
					Text = text,
					Date = date,
					NameUser = nameUser,
					ImageUser = imageUser,
					ScreenName = screenName
				});
			});

			return count > 0 && tweets.Count() > 0 ? true : false;
		}
	}
}
