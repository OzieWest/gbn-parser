using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.StoreControllerTests
{
	[TestClass]
	public class Compile : StoreControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void When_CompileGames_Expect_Return_CorrectValue()
		{
			//arrange

			//act
			var result = target.CompileGames();

			//assert
			Assert.IsTrue(result > 8000);
		}

		[TestMethod]
		public void When_CompileSales_Expect_Return_CorrectValue()
		{
			//arrange

			//act
			var result = target.CompileSales();

			//assert
			Assert.IsTrue(result > 300);
		}
	}
}
