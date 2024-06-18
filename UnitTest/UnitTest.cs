using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ProductAppsTest
{
    public class UnitTest
    {
        IWebDriver driver;

        public UnitTest()
        {
            driver = new FirefoxDriver();
        }

        [Fact]
        public void TestCreateProductForm()
        {

            driver.Url = "http://localhost:55402/";

            driver.Navigate().GoToUrl("http://localhost:55402/Product/Create");

            driver.Manage().Window.Maximize();

            IWebElement FeedCreate = driver.FindElement(By.XPath("/html/body/div/h2"));
            string text = FeedCreate.Text;
            Assert.Equal("Create", text);
            Thread.Sleep(2000);
            driver.Navigate().Back();
            Thread.Sleep(5000);
        }

        [Theory]
        [InlineData("Pineapple", "3.99", "Sweet pinapple from Alabama")]
        [InlineData("Coconut", "0.75", "Mouth watery coconut")]
        public void TestCreate(string productName, string price, string description)
        {
            driver.Url = "http://localhost:55402/";

            driver.Navigate().GoToUrl("http://localhost:55402/Product/Create");

            driver.Manage().Window.Maximize();

            IWebElement ProductNameTextbox = driver.FindElement(By.XPath("/html/body/div/form/div/div[1]/div/input"));
            ProductNameTextbox.Click();
            ProductNameTextbox.SendKeys(productName);
            Thread.Sleep(2000);

            IWebElement PriceTextbox = driver.FindElement(By.XPath("/html/body/div/form/div/div[2]/div/input"));
            PriceTextbox.Click();
            PriceTextbox.SendKeys(price);
            Thread.Sleep(2000);

            IWebElement DescriptionTextbox = driver.FindElement(By.XPath("/html/body/div/form/div/div[3]/div/input"));
            DescriptionTextbox.Click();
            DescriptionTextbox.SendKeys(description);
            Thread.Sleep(2000);

            IWebElement SubmitButton = driver.FindElement(By.XPath("/html/body/div/form/div/div[4]/div/input"));
            SubmitButton.Click();

            IWebElement IndexLabel = driver.FindElement(By.XPath("/html/body/div/h2"));
            string labelText = IndexLabel.Text;
            Thread.Sleep(2000);
            Assert.Equal("Index", labelText);
            Thread.Sleep(2000);
            Thread.Sleep(5000);

            driver.Quit();
        }
    }
}
