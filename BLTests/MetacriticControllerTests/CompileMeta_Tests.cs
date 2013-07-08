using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BLTests.MetacriticControllerTests
{
	[TestClass]
	public class CompileMeta_Tests : MetacriticControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void When_CompileMeta_Expect_Return_True()
		{
			Debug.WriteLine("Compile: Start");

			//act
			var result = target.AsyncCompile(() => { Debug.WriteLine("Compile: End"); });

			while (!result.IsComplete())
			{
				var prog = result.GetProgress();
				Debug.WriteLine("Compile: " + prog);
			}

			//assert
			Assert.IsTrue(result.Value);
		}
	}
}
