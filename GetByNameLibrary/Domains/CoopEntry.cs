using System;
using System.Runtime.Serialization;
namespace GetByNameLibrary.Domains
{
	[DataContract]
	public class CoopEntry
	{
		[DataMember]
		public String Name { get; set; }

		[DataMember]
		public String Offline { get; set; }

		[DataMember]
		public String Online { get; set; }

		[DataMember]
		public String Lan { get; set; }

		[DataMember]
		public String CoopMode { get; set; }

		[DataMember]
		public String CoopComp { get; set; }
	}
}
