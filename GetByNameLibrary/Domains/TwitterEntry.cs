using System;
using System.Runtime.Serialization;
namespace GetByNameLibrary.Domains
{
	[DataContract]
	public class TwitterEntry
	{
		[DataMember]
		public String IdTweet { get; set; }

		[DataMember]
		public String Text { get; set; }

		[DataMember]
		public String Date { get; set; }

		[DataMember]
		public String NameUser { get; set; }

		[DataMember]
		public String ScreenName { get; set; }

		[DataMember]
		public String ImageUser { get; set; }
	}
}