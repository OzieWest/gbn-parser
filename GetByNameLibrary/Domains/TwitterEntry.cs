using System;
namespace GetByNameLibrary.Domains
{
	[Serializable]
	public class TwitterEntry
	{
		public String IdTweet { get; set; }
		public String Text { get; set; }
		public String Date { get; set; }

		public String NameUser { get; set; }
		public String ScreenName { get; set; }
		public String ImageUser { get; set; }
	}
}