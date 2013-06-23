using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetByNameLibrary.Controllers;

namespace BLTests
{
    [TestClass]
    public class StoreControllerTest
    {
        StoreController target;

        [TestInitialize]
        public void Given()
        {
            target = new StoreController();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //arrange

            //act
            var result = target.StartParse();
            while (result.Count != 2)
            {
                //
            }

            //assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
