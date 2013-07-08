using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BLTests.CooperativeControllerTests
{
	[TestClass]
	public class CompileCoops_Tests : CooperativeControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void When_CompileCoops_Expect_Return_True()
		{
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
