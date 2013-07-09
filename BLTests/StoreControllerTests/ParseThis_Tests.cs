using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ReturnValues;

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
			var result = new AsyncRetValue<Boolean>();
			result = target.AsyncParseThis("directcod", PrintResult);

			while (!result.IsComplete())
			{
				//
			}

			//assert
			Assert.IsTrue(result.Value);
		}

		public void PrintResult(AsyncRetValue<Boolean> result)
		{
			Debug.WriteLine("{0}|{1}", result.Value, result.Description);
		}
	}
}
