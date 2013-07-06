using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.StoreControllerTests
{
	[TestClass]
	public class ParseThis_Tests : StoreControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void TestMethod1()
		{
			//arrange

			//act
			var result = target.ParseThis("directcod");

			//assert
			Assert.IsTrue(result.Value);
		}
	}
}
