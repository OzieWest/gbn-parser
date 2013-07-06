using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Controllers;
using System.Collections.Generic;
using GetByNameLibrary.Utilities;
using System.Diagnostics;

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

		[TestMethod]
		public void Expect_Return_CorrectValue()
		{
			//arrange
			var assertList = new List<String>();

			//act
			var result = RunTestMethod();
			while (!result.isEnded())
			{
				var answer = result.Pop();
				if (answer != null)
					assertList.Add(answer);
			}

			//assert
			assertList.ForEach((answer) => { Assert.AreNotEqual(String.Empty, answer); });
		}

		AnswerStack<String> RunTestMethod() { return target.StartParse(); }
	}
}
