using GetByNameLibrary.Utilities;
using ReturnValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface IParser<T> where T: class
	{
		List<T> GetEntries();
		RetValue<Boolean> StartParser();
	}
}
