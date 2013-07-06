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
		AnswerStack<String> StartParse();

		RetValue<Boolean> ParseThis(String parserName);
	}
}
