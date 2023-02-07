using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CSharpSeleniumFramework.Utilities;

namespace CSharpSeleniumFramework.PageObjects
{
    public class LoginPage
    {
        //driver.FindElement(By.Id("password")).SendKeys("learning");
        //driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
        //driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();
        private  IWebDriver driver;
        public LoginPage( IWebDriver driver )
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        // PageObject Factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userName;
        [FindsBy( How = How.Id,Using = "password")]
        private IWebElement password;
        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkBox;
        [FindsBy(How = How.XPath, Using = "//input[@id='signInBtn']")]
        private IWebElement submitButton;

        public IWebElement GetUserName()
        {
            TestContext.Progress.WriteLine("inside Login Page gerUsername element");
            return userName;
        }
        public IWebElement GetPassword()
        {
            TestContext.Progress.WriteLine("inside Login Page password element");
            return password;
        }
        public IWebElement GetSubmitButton()
        {
            TestContext.Progress.WriteLine("inside Login Page password element");
            return submitButton;
        }
        public IWebElement GetCheckBox()
        {
            TestContext.Progress.WriteLine("inside Login Page Checkbox element");
            return checkBox;
        }
        public ProductsPage ValidateLogin( String userID,String pass)
        {
            TestContext.Progress.WriteLine(" Validating the login funtionality");
            userName.SendKeys(userID);
            password.SendKeys(pass);
            checkBox.Click();
            submitButton.Click();
            return new ProductsPage(driver);
        }
    }

}
    
