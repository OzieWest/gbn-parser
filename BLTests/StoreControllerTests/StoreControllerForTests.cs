using GetByNameLibrary.Controllers;
using GetByNameLibrary.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.StoreControllerTests
{
	public class StoreControllerForTests : StoreController
	{
		new public List<Store> LoadStores()
		{
			return base.LoadStores();
		}
	}
}
