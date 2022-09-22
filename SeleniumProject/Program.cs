using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Threading;

namespace SeleniumProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver("File Path");
            driver.Navigate().GoToUrl("https://www.liberis.com/");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@class='btn']")).Click();
            Thread.Sleep(2000);
            // verifying landing partner selection page
            var radioButtons = driver.FindElements(By.XPath("//*[@type='radio']"));
            radioButtons.Count.Should().Be(3);
            var lables = driver.FindElements(By.XPath("//label[@for]"));
            var i = 0;
            List<string> values = new List<string>
            {
                "I'm a Broker",
                "I'm an ISO",
                "I'm a Strategic Partner"
            };

            for (i = 0; i < radioButtons.Count; i++)
            {
                lables[i].Text.Should().Be(values[i]);
            }

            // verify if button is clicked without partner selection
            var button = driver.FindElement(By.XPath("(//*[@class='inner-content'])[2]"));
            button.FindElement(By.TagName("a")).Click();
            var error = driver.FindElement(By.XPath("//*[@class='ph-error-inner']"));
            error.Text.Should().Be("Please select a type of partner");
        }
    }
}
