using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using CSharpSeleniumFramework.Utilities;
using CSharpSeleniumFramework.PageObjects;

namespace CSharpSeleniumFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    internal class E2Etest : Base
    {


        [Test,Category("Regression")]
        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("krkumar", "abc123")]
        [TestCaseSource("AddTestDataConfig")]
        [Parallelizable( ParallelScope.All)]
        public void E2EFlow(String userName,String password, String[] expProducts)
        {
            //string[] expProducts = { "iphone X", "Blackberry" };
            string[] actProducts = new string[2];
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            LoginPage loginPage = new LoginPage(GetWebDriver());
            ProductsPage productPage = loginPage.ValidateLogin(userName, password);
            productPage.waitForPageDisplay();
            IList<IWebElement> lstProduct = productPage.GetCards();
            
            foreach (IWebElement products in lstProduct)
            {
                //String productName = product.FindElement(By.CssSelector(".card-title a")).Text;
                // TestContext.WriteLine(productName);
                if (expProducts.Contains(products.FindElement(productPage.GetProductLocator()).Text))
                {
                    products.FindElement( productPage.GetCardFooterLocator()).Click();
                }
            }
            //Check out button
            // driver.FindElement(By.PartialLinkText("Checkout")).Click();
             CheckOutPage checkoutPage =  productPage.CheckOut();


            // Xpath of Check out list
            // //table[@class='table table-hover']//tbody/tr/td
            // h4[@class='media-heading']/a (Media list)

            IList<IWebElement> checkoutProducts = checkoutPage.GetCartItems();

            for (int i = 0; i < checkoutProducts.Count; i++)
            {
                TestContext.Progress.WriteLine(checkoutProducts[i].Text);
                actProducts[i] = checkoutProducts[i].Text;
            }
            Assert.That(actProducts, Is.EqualTo(expProducts));
            // xpath of Check out button
            //  driver.FindElement(By.XPath("//button[@class='btn btn-success']")).Click();
            PurchasePage purchasePage = checkoutPage.CheckOut();

            //driver.FindElement(By.Id("country")).SendKeys("Ind");
            purchasePage.GetCountry("ind");
            purchasePage.WaitForCountryList();
            purchasePage.SetCountry();
           // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
           //driver.FindElement(By.LinkText("India")).Click();

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='checkbox checkbox-primary']/input")));
            //driver.FindElement(By.XPath("//div[@class='checkbox checkbox-primary']/label")).Click();
            purchasePage.AgreeTerms();

            Thread.Sleep(3000);
            purchasePage.ConfirmPurchase();
            //driver.FindElement(By.XPath("//input[@type='submit' and @value ='Purchase']")).Click();
            string strResult = purchasePage.SuccessMessage();

            TestContext.Progress.WriteLine(strResult);

            StringAssert.Contains("Success", strResult);

            // Closing the success message
            purchasePage.ClosingMessage();

            //driver.FindElement(By.XPath("//div[@class ='alert alert-success alert-dismissible']/a")).Click();
        }
        [Test,Category("Smoke")]
        public void InvalidLogin_Test()
        {
            TestContext.Progress.WriteLine("LocatorIdentification_Test ");
            String title = driver.Value.Title;
            driver.Value.FindElement(By.Id("username")).SendKeys("krkumarid@gmail.com");
            driver.Value.FindElement(By.Id("password")).SendKeys("KasiAppu1*");

            // XPath and CSS selector
            // Syntax of CSS tagname[attribute='value']
            // input[id='signInBtn']

            // driver.FindElement(By.CssSelector("input[id='signInBtn']")).Click();
            // Check box Xpath
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
            String strCond = driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span[2]")).Text;

            // CSS for Check box
            //  .text-info span:nth-child(1) input

            // Locating usign Xpath
            driver.Value.FindElement(By.XPath("//input[@id='signInBtn']")).Click();
            //Thread.Sleep(3000);

            // Explicit wait
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(By.XPath("//input[@id='signInBtn']"), "Sign In"));

            String strErrorMsg = driver.Value.FindElement(By.XPath("//div[@class='alert alert-danger col-md-12']")).Text;
            TestContext.Progress.WriteLine(strErrorMsg);
            
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().ExtractData("username"), getDataParser().ExtractData("password"), getDataParser().ExtractDataArray("products"));
            yield return new TestCaseData(getDataParser().ExtractData("username"), getDataParser().ExtractData("password"), getDataParser().ExtractDataArray("products"));
            yield return new TestCaseData(getDataParser().ExtractData("username_wrong"), getDataParser().ExtractData("password_wrong"), getDataParser().ExtractDataArray("products"));
        }
    }
}