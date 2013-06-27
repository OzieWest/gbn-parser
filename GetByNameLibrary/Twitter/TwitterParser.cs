using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace GetByNameLibrary.Twitter
{
	public class TwitterParser : ITwitter
	{
		public int EntriesCount { get; set; }
		public ISerializer Serializer { get; set; }
		public TxtLogger Logger { get; set; }

		List<TwitterEntry> _entries;

		public TwitterParser()
		{
			_entries = new List<TwitterEntry>();
		}

		public List<TwitterEntry> GetEntries()
		{
			return _entries;
		}

		public RetValue<Boolean> StartParser()
		{
			var result = new RetValue<Boolean>();
			try
			{
				if (this.GrabTimeLine())
				{
					this.SaveEntries();
					result.Value = true;
				}
				else
				{
					result.Value = false;
					result.Description = "method |GrabTimeLine| return false";
				}
			}
			catch (Exception ex)
			{
				result.Value = false;
				result.Description = ex.Message;
				Logger.AddEntry(ex.ToString(), MessageType.Error);
				Logger.WriteLogs();
			}
			return result;
		}

		protected void SaveEntries()
		{
			Serializer.Save<List<TwitterEntry>>(_entries, @"completed\tweets.json");
		}

		protected Boolean GrabTimeLine()
		{
			var config = Serializer.Load<AuthConfig>(@"configs\twitter.config");

			var service = new TwitterService(config.ConsumerKey, config.ConsumerSecret);
			service.AuthenticateWith(config.AccessToken, config.AccessTokenSecret);

			var tweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { Count = EntriesCount }).ToList();

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

			return EntriesCount > 0 && tweets.Count() > 0 ? true : false;
		}
	}
}
