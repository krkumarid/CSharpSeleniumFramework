using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumCS
{
    [Parallelizable( ParallelScope.Self)]
    internal class ChildWindows
    {
        public IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("Starting Chrome browser");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            // Implicit wait ,It applied globally

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // waiting 5 seconds

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();


        }
        [Test]
        public void childWindowAccess_Test()
        {
            String parentWindow = driver.CurrentWindowHandle.ToString();
            String email = "mentor@rahulshettyacademy.com";
            driver.FindElement(By.XPath("//a[@class='blinkingText']")).Click();
            TestContext.Progress.WriteLine(driver.WindowHandles.Count);
            Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));
           IList<String> lstw = driver.WindowHandles;
            driver.SwitchTo().Window(lstw[1]);// child window;
            TestContext.Progress.WriteLine(driver.Title);

            String text = driver.FindElement(By.XPath("//p[@class='im-para red']")).Text;
            TestContext.Progress.WriteLine($"{text}");
            String[] splitTxt = text.Split("at");
            String[] stremail = splitTxt[1].Trim().Split(' ');
            TestContext.Progress.WriteLine(stremail[0]);
            Assert.That( stremail[0], Is.EqualTo(email));

            driver.SwitchTo().Window(parentWindow);
            TestContext.Progress.WriteLine(driver.Title);
            driver.FindElement(By.Id("username")).SendKeys(stremail[0]);
        }
        [TearDown]
        public void CloseTheBrowser()
        {
            driver.Quit();
        }
    }
}
