using GetByNameLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.StoreControllerTests
{
	public class StoreControllerSpecification
	{
		protected IStoreController target;

		public StoreControllerSpecification()
		{
			target = new StoreControllerForTests();
		}
	}
}
