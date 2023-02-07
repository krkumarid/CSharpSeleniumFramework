using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{
    public class CheckOutPage
    {
        IWebDriver driver;
        public CheckOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//h4[@class='media-heading']/a")]
        private IList<IWebElement> checkOutList;
        [FindsBy(How = How.XPath, Using = "//button[@class='btn btn-success']")]
        private IWebElement cheOutButton;
        public IList<IWebElement> GetCartItems()
        {
            return checkOutList;
        }
        public PurchasePage CheckOut()
        {
            cheOutButton.Click();
            return new PurchasePage(driver);
        }
    }
}
