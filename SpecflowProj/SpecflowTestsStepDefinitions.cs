using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace SpecflowProj
{
    [Binding]
    public class SpecflowTestsStepDefinitions
    {
        IWebDriver driver;

        [Given(@"I am on the Create new product page")]
        public void GivenIAmOnTheCreateNewProductPage()
        {
            driver = new FirefoxDriver();
            driver.Url = "http://localhost:55402/";
            driver.Navigate().GoToUrl("http://localhost:55402/Product/Create");
            driver.Manage().Window.Maximize();

            IWebElement FeedCreate = driver.FindElement(By.XPath("/html/body/div/h2"));
            string text = FeedCreate.Text;
            Assert.Equal("Create", text);
            Thread.Sleep(2000);
        }

        [Given(@"I enter the product name Pear")]
        public void GivenIEnterTheProductNamePear()
        {
            IWebElement ProductNameTextbox = driver.FindElement(By.XPath("/html/body/div/form/div/div[1]/div/input"));
            ProductNameTextbox.Click();
            ProductNameTextbox.SendKeys("Strawberry");
            Thread.Sleep(2000);
        }

        [Given(@"I enter a price value")]
        public void GivenIEnterAPriceValue()
        {
            IWebElement PriceTextbox = driver.FindElement(By.XPath("/html/body/div/form/div/div[2]/div/input"));
            PriceTextbox.Click();
            PriceTextbox.SendKeys("1.39");
            Thread.Sleep(2000);
        }


        [Given(@"I enter a description of the product that it is a juicy fresh Pear")]
        public void GivenIEnterADescriptionOfTheProductThatItIsAJuicyFreshPear()
        {
            IWebElement DescriptionTextbox = driver.FindElement(By.XPath("/html/body/div/form/div/div[3]/div/input"));
            DescriptionTextbox.Click();
            DescriptionTextbox.SendKeys("Sweet juicy strawberries");
            Thread.Sleep(2000);
        }

        [When(@"I create the product")]
        public void WhenICreateTheProduct()
        {
            IWebElement SubmitButton = driver.FindElement(By.XPath("/html/body/div/form/div/div[4]/div/input"));
            SubmitButton.Click();
        }

        [Then(@"I should see the Index page")]
        public void ThenIShouldSeeTheIndexPage()
        {
            IWebElement IndexLabel = driver.FindElement(By.XPath("/html/body/div/h2"));
            string labelText = IndexLabel.Text;
            Thread.Sleep(2000);
            Assert.Equal("Index", labelText);
            Thread.Sleep(2000);
        }

        [AfterScenario]
        public void DisposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
