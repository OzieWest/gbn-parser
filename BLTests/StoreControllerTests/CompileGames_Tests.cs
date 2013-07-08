using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

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
			Debug.WriteLine("Compile: Start");

			//act
			var result = target.AsyncCompile(() => { Debug.WriteLine("Compile: End"); });

			while(!result.IsComplete())
			{
				var prog = result.GetProgress();
				Debug.WriteLine("Compile: " + prog);
			}

			//assert
			Assert.IsTrue(result.Value);
		}
	}
}
