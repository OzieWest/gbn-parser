using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Utilities
{
	public class RetValue<T>
	{
		public T Value { get; set; }
		public String Description { get; set; }
	}
}
