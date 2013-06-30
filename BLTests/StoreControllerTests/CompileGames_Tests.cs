﻿using System;
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
		public void When_CompileGames_Expect_Return_True()
		{
			//arrange

			//act
			var result = target.Compile();

			//assert
			Assert.IsTrue(result.Value);
		}
	}
}
