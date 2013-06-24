using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GetByNameLibrary.Stores;

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
			result.ForEach((o) =>
			{
				Assert.IsNotNull(o);
			});
		}

		List<Store> RunTestMethod() { return target.LoadStores(); }
	}
}
