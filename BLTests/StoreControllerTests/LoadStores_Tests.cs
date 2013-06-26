using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GetByNameLibrary.Stores;
using GetByNameLibrary.Utilities;

namespace BLTests.StoreControllerTests
{
	[TestClass]
	public class LoadStores_Tests : StoreControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void Expect_Return_NotNull()
		{
			//arrange

			//act
			var result = RunTestMethod();

			//assert
			result.Value.ForEach((o) => { Assert.IsNotNull(o); });
			Assert.IsTrue(String.IsNullOrEmpty(result.Description));
		}

		RetValue<List<BaseStore>> RunTestMethod() { return target.LoadStores(); }
	}
}
