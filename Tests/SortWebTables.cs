using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumCS
{
    [Parallelizable(ParallelScope.Self)]
    internal class SortWebTables
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

            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
            driver.Manage().Window.Maximize();


        }
        [Test]
        public void SortWebtableTest()
        {
            ArrayList vegList = new ArrayList();

            SelectElement selDrop = new SelectElement(driver.FindElement(By.Id("page-menu")));
            selDrop.SelectByValue("20");
            // Step 1- Collect all the veg names into a lsit
            IList<IWebElement> veggies = driver.FindElements(By.XPath("//table[@class='table table-bordered']//tbody/tr/td[1]"));
            foreach (IWebElement element in veggies) 
            {
                vegList.Add(element.Text);
            }
            // Sorting the Array list
            vegList.Sort();
            foreach( String vegName in vegList)
            {
                TestContext.Progress.WriteLine(vegName);
            }
            ////th[contains(@aria-label,'Veg/fruit name')]
            driver.FindElement(By.XPath("//th[contains(@aria-label,'Veg/fruit name')]")).Click();
            // step 4
            ArrayList vegListb = new ArrayList();
            IList<IWebElement> sortedVeggies = driver.FindElements(By.XPath("//table[@class='table table-bordered']//tbody/tr/td[1]"));
            foreach (IWebElement element in sortedVeggies)
            {
                vegListb.Add(element.Text);
            }
            // Compare the Arrays
            Assert.That(vegListb, Is.EqualTo(vegList));

        }
        [TearDown]
        public void CloseTheBrowser()
        {
            driver.Close();
        }

    }
}
