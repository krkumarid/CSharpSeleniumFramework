using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace CSharpSeleniumFramework.PageObjects
{
    public class ProductsPage
    {
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

        //  IList<IWebElement> lstProduct = driver.FindElements(By.TagName("app-card"));

        // driver.FindElements(By.XPath("//h4[@class='media-heading']/a"));
        // By.XPath("//button[@class='btn btn-success']")

        //Check out button
           // driver.FindElement(By.PartialLinkText("Checkout")).Click();
        public IWebDriver driver;
        By productLocator = By.CssSelector(".card-title a");
        By cardFooterLocator = By.CssSelector(".card-footer button");

        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy (How =How.XPath,Using = "//app-card[@class='col-lg-3 col-md-6 mb-3']")]
        private IList<IWebElement> lstCard;

       
        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement eleCheckout;
        
        public void waitForPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

        }
        
        public IList<IWebElement> GetCards() 
        {
            return lstCard;
        }
        public By GetProductLocator()
        {
            return productLocator;
        }
        public By GetCardFooterLocator() 
        {
            return cardFooterLocator;
        }
        
        public CheckOutPage CheckOut()
        {
            eleCheckout.Click();
            return new CheckOutPage(driver);
        }
    }
}
