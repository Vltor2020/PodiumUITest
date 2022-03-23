using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using PodiumUITest.Extensions;
using PodiumUITest.Pages.ProductSelection.Enums;

namespace PodiumUITest.Pages.ProductSelection.Elements
{
    public class ProductCard
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement _root;

        #region Elements

        private IWebElement Content => _root.TryGetChild(_driver, By.ClassName("content"));
        private IWebElement Metadata => Content.TryGetChild(_driver, By.ClassName("meta"));
        private IWebElement Header => Content.TryGetChild(_driver, By.ClassName("header"));

        #endregion

        public IWebElement Image => _root.TryGetChild(_driver, By.CssSelector("[class='ui small image']"))
            .TryGetChild(_driver, By.TagName("img"));

        public IWebElement ContinueButton => Content.TryGetChild(_driver, By.ClassName("extra"))
            .TryGetChild(_driver, By.CssSelector("[class='ui left floated button']"));

        public int TermInYears
        {
            get
            {
                var (years, _) = ParseProductDetails(Header.Text);
                return years;
            }
        }

        public ProductType ProductType
        {
            get
            {
                var (_, productType) = ParseProductDetails(Header.Text);
                return productType;
            }
        }
        public int MonthlyPayment
        {
            get 
            {
                string rawPrice = Metadata.TryGetChild(_driver, By.ClassName("price")).Text;
                rawPrice = rawPrice.Replace("£", "");
                int returnedValue = 0;
                if (int.TryParse(rawPrice, out returnedValue)) { return returnedValue; }
                else { return 0; }
            }
        }
        public decimal InterestRate 
        { 
            get
            {
                string rawValue = Metadata.TryGetChild(_driver, By.ClassName("stay")).Text;
                rawValue = rawValue.Replace("£", "");
                decimal returnedValue = 0;
                if(decimal.TryParse(rawValue, out returnedValue)) { return returnedValue; }
                else { return 0; }
            } 
        }

        public string Description => Content
            .TryGetChild(_driver, By.ClassName("description"))
            .TryGetChild(_driver, By.TagName("p"))
            .Text;

        public ProductCard(IWebDriver driver, IWebElement element)
        {
            _driver = driver;
            _root = element;
        }

        // In the future these private functions could be in a seperate place
        private (int Years, ProductType ProductType) ParseProductDetails(string productDetails)
        {
            var detailArray = productDetails.Split(" yr ");

            var years = Convert.ToInt32(detailArray[0]);
            var productType = ParseProductType(detailArray[1]);

            return (years, productType);
        }

        private ProductType ParseProductType(string productType) => productType switch
        {
            "Fixed" => ProductType.Fixed,
            "Tracker" => ProductType.Tracker,
            // In the future there could be custom exceptions
            _ => throw new Exception("Could not parse product type")
        };
    }
}

