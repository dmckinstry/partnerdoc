using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace dotnetWeb.FunctionalTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private static TestContext testContext;
        private static bool hideBrowser = true;
        private static string baseUrl;
        private static RemoteWebDriver driver;
        private static string driverPath;
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            testContext = context;
            baseUrl = testContext.Properties["BaseUrl"].ToString();
            string hideBrowserFlag= testContext.Properties["HideBrowser"].ToString();
            hideBrowser = (string.IsNullOrEmpty(hideBrowserFlag) && hideBrowserFlag.ToLower().StartsWith("f"));

            string chromeDriverPath = Environment.GetEnvironmentVariable("ChromeWebDriver");
            chromeDriverPath = (string.IsNullOrEmpty(chromeDriverPath)) ? testContext.Properties["ChromeDriverPath"].ToString() : chromeDriverPath;
            driverPath = (string.IsNullOrEmpty(chromeDriverPath)) ? Environment.CurrentDirectory : chromeDriverPath;

            testContext.WriteLine("*** BaseURL: {0}", baseUrl);
            testContext.WriteLine("*** HideBrowser: {0}", hideBrowserFlag);
            testContext.WriteLine("*** driverPath: {0}", driverPath);
        }

        [TestMethod]
        public void HomePageResponseWithCorrectTitle()
        {
            RemoteWebDriver driver = null;
            try
            {
                var webAppUrl = baseUrl + "/";
                testContext.WriteLine("*** Testing URL: {0}", webAppUrl);

                driver = GetChromeDriver();
                Assert.IsNotNull(driver, "Unable to initialize web driver");
                driver.Navigate().GoToUrl(webAppUrl);

                Assert.AreEqual("Home Page - ASP.NET CORE", driver.Title);
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void AboutPageRespondsCorrectly()
        {
            try
            {
                var webAppUrl = baseUrl + "/Home/About";
                testContext.WriteLine("*** Testing URL: {0}", webAppUrl);

                driver = GetChromeDriver();
                driver.Navigate().GoToUrl(webAppUrl);

                Assert.AreEqual("About - ASP.NET CORE", driver.Title);

                string environmentText = driver.FindElementById("envMessage").Text;
                Assert.IsFalse(environmentText.EndsWith(":"), "Environment is not configured");
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void ContactPageRespondsCorrectly()
        {
            try
            {
                var webAppUrl = baseUrl + "/Home/Contact";
                testContext.WriteLine("*** Testing URL: {0}", webAppUrl);

                driver = GetChromeDriver();
                driver.Navigate().GoToUrl(webAppUrl);

                Assert.AreEqual("Contact - ASP.NET CORE", driver.Title);
            }
            finally
            {
                driver.Quit();
            }
        }

        private static RemoteWebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");
            if (hideBrowser)
            {
                testContext.WriteLine("*** Configured for headless testing");
                options.AddArguments("--headless");
            }

            testContext.WriteLine("*** Using driver path: {0}", driverPath);
            return new ChromeDriver(driverPath, options, TimeSpan.FromSeconds(300));
        }
    }
}
