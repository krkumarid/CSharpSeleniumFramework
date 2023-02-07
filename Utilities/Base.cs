using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using Amazon.S3.Model.Internal.MarshallTransformations;
using System.Data;

namespace CSharpSeleniumFramework.Utilities
{
    public class Base
        
    {
        // public static IWebDriver driver;
        String browserName;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public ExtentReports extentReports;
        public ExtentTest test;
      
        [ OneTimeSetUp]
        public void Setup() 
        
        {
            // Getting the Current and parent directory
            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlReport = new ExtentHtmlReporter(reportPath);
            extentReports = new ExtentReports();

            extentReports.AttachReporter(htmlReport);
            extentReports.AddSystemInfo("HostName", "localhost");
            extentReports.AddSystemInfo("Environment", "QA");
            extentReports.AddSystemInfo("UserName", "Rajeevkumar");



        }
        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("Inside the Base class SetUp method");
            // Entry of Report generation
           test = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
           // Configuration
            browserName = TestContext.Parameters["browserName"];
            if( browserName == null ) 
            {
             browserName = ConfigurationManager.AppSettings["browser"];
            }
           
            //var Url = ConfigurationManager.AppSettings["Url"];
             InitBrowser(browserName);

            // Implicit wait ,It applied globally

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // waiting 5 seconds

            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            driver.Value.Manage().Window.Maximize();
        }
        public void InitBrowser( String strBrowserName)
        {
            switch (strBrowserName) 
            {

                case "Chrome":
                     new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                default: TestContext.Progress.WriteLine("Please pass a valid Browsewr name");
                    break;


            }
        }
        public IWebDriver GetWebDriver() 
        {
            return driver.Value;
        }
        public  static JsonReader getDataParser()
        {
            return new JsonReader();
        }
        [TearDown]
        public void AfterTest()
        {
            TestContext.Progress.WriteLine("Closing the browser");

            // Catching the test result
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if( status == TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "Test failed with logtrace " + stackTrace);

            }
            else if( status== TestStatus.Passed) 
            {

            }
            extentReports.Flush();

            driver.Value.Quit();

         }
        public MediaEntityModelProvider captureScreenShot( IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenShot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, screenShotName).Build();


        }

    }
}

