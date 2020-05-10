using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSResults.UITests
{
    
    public class HomeControllerUITest
    {
        [Fact]
        public void LoadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://localhost:44368/");
            }
        }
    }
}
