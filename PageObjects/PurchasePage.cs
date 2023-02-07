using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{
    public class PurchasePage
    {
        IWebDriver driver;
        By country = By.LinkText("India");
        public PurchasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement eleCounry;
        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement eleCountrySelected;
        [FindsBy(How = How.XPath, Using = "//div[@class='checkbox checkbox-primary']/label")]
        private IWebElement eleCheckAgreeTerms;
        [FindsBy(How = How.XPath, Using = "//input[@type='submit' and @value ='Purchase']")]
        private IWebElement btnPurchase;
        [FindsBy(How = How.XPath, Using = "//div[@class ='alert alert-success alert-dismissible']")]
        private IWebElement SussessMessage;
        [FindsBy(How = How.XPath, Using = "//div[@class ='alert alert-success alert-dismissible']/a")]
        private IWebElement closeMessage;
        public void GetCountry( string country) 
        {
            eleCounry.SendKeys(country);
        }
        public void SetCountry()
        {
            eleCountrySelected.Click();
        }
        public void WaitForCountryList()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(country));
        }
        public void AgreeTerms()
        {
            eleCheckAgreeTerms.Click();
        }
        public void ConfirmPurchase()
        {
            btnPurchase.Click();
        }
        public String SuccessMessage()
        {
            return SussessMessage.Text;
        }
        public void ClosingMessage()
        {
            closeMessage.Click();
        }
    }
}
