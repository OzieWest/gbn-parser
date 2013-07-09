using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Controllers;
using System.Collections.Generic;
using GetByNameLibrary.Utilities;
using System.Diagnostics;
using ReturnValues;

namespace BLTests.StoreControllerTests
{
	[TestClass]
	public class StartParse_Tests : StoreControllerSpecification
	{
		int count;

		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void Expect_Return_CorrectValue()
		{
			//arrange
			var result = new List<AsyncRetValue<Boolean>>();

			//act
			result = target.AsyncStartParse(PrintResult);

			//assert
			while (count != result.Count)
			{
				//
			}

			result.ForEach((answer) => { Assert.IsTrue(answer.Value); });
		}

		public void PrintResult(AsyncRetValue<Boolean> result)
		{
			Debug.WriteLine("{0}|{1}", result.Value, result.Description);
			count++;
		}
	}
}
