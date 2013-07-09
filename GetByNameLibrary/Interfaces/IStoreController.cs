using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using ReturnValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface IStoreController : ICompile
	{
		List<AsyncRetValue<Boolean>> AsyncStartParse(Action<AsyncRetValue<Boolean>> method);

		AsyncRetValue<Boolean> AsyncParseThis(String parserName, Action<AsyncRetValue<Boolean>> method);
	}
}
