using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using Xunit;

namespace CSResultsCore.UITests
{
    public class UnitTest1
    {
        [Fact]
        public void LoadHomePage()
        {
            //Using statement is used to dispose of the driver when the test is complete
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:44356/");

                //Check that the title of the home page is correct
                Assert.Equal("Home Page", driver.Title);

                //Check that the url is correct
                Assert.Equal("https://localhost:44356/", driver.Url);

            }
        }

        [Fact]
        public void Navigate()
        {
            //Using statement is used to dispose of the driver when the test is complete
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:44356/");
                driver.Navigate().GoToUrl("https://localhost:44356/Data");

                //Go back to the previous page i.e the home page
                driver.Navigate().Back();

                //Go forward 1 page i.e the data page
                driver.Navigate().Forward();

                //Check that the url is still the homepage after rereshing
                Assert.Equal("https://localhost:44356/Data", driver.Url);
            }
        }

        [Fact]
        public void HTMLTest()
        {
            //Using statement is used to dispose of the driver when the test is complete
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:44356/");

                //get readonly list
                IReadOnlyCollection<IWebElement> li_element_list = driver.FindElements(By.TagName("li"));

                //create new list
                List<IWebElement> allHandles2 = new List<IWebElement>(li_element_list);

                var res = allHandles2[0].Text;

            }
        }
    }
}
