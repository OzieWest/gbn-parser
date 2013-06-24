using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Controllers;
using System.Collections.Generic;

namespace BLTests.StoreControllerTests
{
	[TestClass]
	public class StartParse_Tests : StoreControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[Ignore]
		[TestMethod]
		public void Expect_Return_CorrectValue()
		{
			//arrange

			//act
			var result = RunTestMethod();
			while (result.Count != 9)
			{
				//
			}

			//assert
		}

		List<String> RunTestMethod() { return target.StartParse(); }
	}
}
