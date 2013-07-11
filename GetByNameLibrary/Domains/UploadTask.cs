using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Domains
{
	[DataContract]
	public class UploadTask
	{
		[DataMember]
		public String Name { get; set; }

		[DataMember]
		public String Local { get; set; }

		[DataMember]
		public String Remote { get; set; }
	}
}
