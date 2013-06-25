using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Utilities;

namespace BLTests.TwitterControllerTests
{
	[TestClass]
	public class CreateTweetsAndSave_Test : TwitterControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void Expect_Return_True()
		{
			//arrange

			//act
			var result = RunTestMethod();

			//assert
			Assert.IsTrue(result.Value);
		}

		RetValue<Boolean> RunTestMethod() { return target.CreateTweetsAndSave(); }
	}
}
