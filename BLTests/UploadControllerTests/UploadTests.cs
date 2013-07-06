using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Domains;

namespace BLTests.UploadControllerTests
{
	[TestClass]
	public class UploadTests: UploadControllerSpecification
	{
		[TestInitialize]
		public void Given()
		{
			//
		}

		[TestMethod]
		public void TestMethod1()
		{
			//arange
			target.AddRequest(new UploadTask() { Local = "completed/games.json", Name = "games", Remote = @"query\games.json" });
			target.AddRequest(new UploadTask() { Local = "completed/sales.json", Name = "sales", Remote = @"query\sales.json" });
			target.AddRequest(new UploadTask() { Local = "completed/coops.json", Name = "coops", Remote = @"query\coops.json" });
			target.AddRequest(new UploadTask() { Local = "completed/tweets.json", Name = "tweets", Remote = @"query\tweets.json" });
			target.AddRequest(new UploadTask() { Local = "completed/metas.json", Name = "metas", Remote = @"query\metas.json" });

			//act
			var result = target.StartUpload();

			//assert
			Assert.AreEqual(4, result.Value.Count);
		}
	}
}
