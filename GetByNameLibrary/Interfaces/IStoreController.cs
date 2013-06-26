using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface IStoreController
	{
		AnswerStack<String> StartParse();

		RetValue<Boolean> CompileSales();
		RetValue<Boolean> CompileGames();
	}
}
