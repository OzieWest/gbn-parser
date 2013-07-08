using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Utilities;
using ReturnValues;
using System.Diagnostics;

namespace BLTests.TwitterControllerTests
{
	[TestClass]
	public class CompileTweets_Test : TwitterControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void When_CompileTweets_Expect_Return_True()
		{
			Debug.WriteLine("Compile: Start");

			//act
			var result = target.AsyncCompile(() => { Debug.WriteLine("Compile: End"); });

			while (!result.IsComplete())
			{
				var item = result.IsComplete();
				var prog = result.GetProgress();
				Debug.WriteLine("Compile: " + prog);
			}

			//assert
			Assert.IsTrue(result.Value);
		}
	}
}
