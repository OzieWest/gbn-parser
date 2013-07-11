using System;
using System.Runtime.Serialization;
namespace GetByNameLibrary.Domains
{
	[DataContract]
	public class GameEntry
	{
		[DataMember]
		public String SearchString { get; set; }

		[DataMember]
		public String Title { get; set; }

		[DataMember]
		public String GameUrl { get; set; }

		[DataMember]
		public String StoreUrl { get; set; }

		[DataMember]
		public String Cost { get; set; }

		[DataMember]
		public Boolean Sale { get; set; }
	}
}
