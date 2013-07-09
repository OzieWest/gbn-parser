using GetByNameLibrary.Controllers;
using GetByNameLibrary.Stores;
using GetByNameLibrary.Utilities;
using ReturnValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.StoreControllerTests
{
	public class StoreControllerForTests : StoreController
	{
		new public RetValue<List<BaseStoreParser>> LoadStores()
		{
			return base.LoadStores();
		}
	}
}
