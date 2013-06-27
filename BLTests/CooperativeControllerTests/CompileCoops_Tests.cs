using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.CooperativeControllerTests
{
	[TestClass]
	public class CompileCoops_Tests : CooperativeControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void When_CompileCoops_Expect_Return_True()
		{
			//arrange

			//act
			var result = target.Compile();

			//assert
			Assert.IsTrue(!String.IsNullOrEmpty(result.Description));
			Assert.IsTrue(result.Value);
		}
	}
}
