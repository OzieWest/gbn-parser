using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Domains
{
	[DataContract]
	public class MetaEntry
	{
		[DataMember]
		public String Name { get; set; }

		[DataMember]
		public int MetaScore { get; set; }

		[DataMember]
		public int UserScore { get; set; }

		[DataMember]
		public String Date { get; set; }
	}
}
