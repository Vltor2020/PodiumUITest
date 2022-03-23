using OpenQA.Selenium;
using PodiumUITest.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PodiumUITest.Pages.ProductSelection
{
    public class ProductSelectionPage
    {
        private readonly IWebDriver _driver;

        public ProductSelectionPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Header => _driver.TryGetElement(By.TagName("h1"));

        public IWebElement SubHeader => _driver.TryGetElement(By.TagName("h2"));

        public IWebElement PageInstructions => _driver.TryGetElement(By.TagName("p"));

        public List<IWebElement> ProductCards
        {
            get
            {
                IWebElement productList = _driver.TryGetElement(By.CssSelector("[class='ui items divided']"));
                return productList.TryGetChildren(_driver, By.ClassName("item")).ToList();
            }
        }
    }
}
