using System.Collections.Generic;
using dotnetWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotnetWeb.UnitTests
{
    public class ConfigStub : IConfiguration
    {
        private Dictionary<string,string> config = new Dictionary<string, string>();
        public string this[string key]
        {
            get { return ( config.ContainsKey(key) ? config[key] : null ); }
            set { config[key] = value; }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new System.NotImplementedException();
        }
    }

    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexPageTest()
        {
            var cfg = new ConfigStub();
            var controller = new HomeController(cfg);
            IActionResult result = controller.Index();
            Assert.AreEqual(null, controller.ViewData["Message"]);
        }

        [TestMethod]
        public void AboutPageTest()
        {
            var cfg = new ConfigStub();
            cfg["ENVIRONMENT"] = "Unit Test";
            var controller = new HomeController(cfg);
            IActionResult result = controller.About();
            Assert.AreEqual("Your application is running in: Unit Test", controller.ViewData["Message"]);
        }

        [TestMethod]
        public void ContactPageTest()
        {
            var cfg = new ConfigStub();
            var controller = new HomeController(cfg);
#pragma warning disable IDE0059 // Value assigned to symbol is never used
            IActionResult result = controller.Contact();
#pragma warning restore IDE0059 // Value assigned to symbol is never used
            Assert.AreEqual("Your contact page.", controller.ViewData["Message"]);
        }
    }
}
