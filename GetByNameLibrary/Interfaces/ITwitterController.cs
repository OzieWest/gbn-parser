using GetByNameLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface ITwitterController
	{
		RetValue<Boolean> CompileTweets();
	}
}
