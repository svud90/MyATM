using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyATM.Controllers;
using System.Web.Mvc;
namespace MyATM.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FooActionReturnAboutView()
        {
            var homeController = new HomeController();
            var result = homeController.Foo() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }
        [TestMethod]
        public void ContactFormSayThanks()
        {
            var homeController = new HomeController();
            var result = homeController.Contact() as ViewResult;
            Assert.IsNotNull(result.ViewBag.Message);
        }
    }
}
