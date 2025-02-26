using Microsoft.VisualStudio.TestTools.UnitTesting;
using IiaTdd.routes; // Assure-toi d'importer ton namespace
using System;

namespace IiaTddTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           
            var controller = new TestController();

        
            bool result = controller.Get();

       
            Assert.IsTrue(result);
        }
    }
}