using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.MetacriticControllerTests
{
	[TestClass]
	public class CompileMeta_Tests : MetacriticControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void When_CompileMeta_Expect_Return_True()
		{
			//arrange

			//act
			var result = target.Compile();

			//assert
			Assert.IsTrue(result.Value);
		}
	}
}
