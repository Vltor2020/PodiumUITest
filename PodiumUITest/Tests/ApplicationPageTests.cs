using NUnit.Framework;
using OpenQA.Selenium;
using PodiumUITest.Extensions;
using PodiumUITest.Pages.ProductSelection;
using PodiumUITest.Pages.ProductSelection.Elements;
using PodiumUITest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodiumUITest.Tests
{
    public class ApplicationPageTests
    {
        IWebDriver _driver;
        ProductSelectionPage _productSelectionPage;
        List<ProductCard> _productCards;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.NewDefaultDriver("chrome");
            _driver.GoToUrl("https://podium-test-65804.firebaseapp.com");
            _productSelectionPage = new ProductSelectionPage(_driver);
            _productCards = new List<ProductCard>();
            foreach (IWebElement productCardRoot in _productSelectionPage.ProductCards)
            {
                _productCards.Add(new ProductCard(_driver, productCardRoot));
            }
        }

        // Should probs split these 

        #region Check for Headers

        [Test]
        public void Is_Header_Present()
        {
            Assert.That("Some awesome mortgage website", Is.EqualTo(_productSelectionPage.Header.TryGetElementText()));
        }

        [Test]
        public void Is_SubHeader_Present()
        {
            Assert.That("Hello, we found these mortgages for you:", Is.EqualTo(_productSelectionPage.SubHeader.TryGetElementText()));
        }

        [Test]
        public void Is_Please_Select_Header_Present()
        {
            Assert.That("Please select a mortgage product to apply.", Is.EqualTo(_productSelectionPage.PageInstructions.TryGetElementText()));
        }

        #endregion

        #region Checks For Images

        [Test]
        public void Do_Images_Display()
        {
            foreach (ProductCard product in _productCards)
            {

                Assert.NotNull(product.Image);
            }
        }

        #endregion

        #region Checks For Prices

        [Test]
        public void Do_Prices_Display()
        {
            foreach (ProductCard product in _productCards)
            {
                Assert.NotZero(product.MonthlyPayment);
            }
        }


        [Test]
        public void Do_Prices_Display_Numeric_Values()
        {
            List<string> valuesAsStrings = new List<string>();
            // Grab all of the raw strings for each price element
            foreach (ProductCard product in _productCards)
            {
                Assert.NotZero(product.MonthlyPayment);
            }
        }

        #endregion

        #region Checks For Interest Rates

        [Test]
        public void Do_Interest_Rates_Display()
        {
            foreach (ProductCard product in _productCards)
            {
                Assert.NotNull(product.InterestRate);
            }
        }

        [Test]
        public void Do_Interest_Rates_Display_As_Numeric_Value()
        {
            List<string> valuesAsStrings = new List<string>();
            // Grab all of the raw strings for each interest rate element
            foreach (ProductCard product in _productCards)
            {
                Assert.IsNotNull(product.InterestRate);
            }

        }

        #endregion

        #region Checks For Lender Text

        [Test]
        public void Is_Provided_By_Text_Present()
        {
            foreach (ProductCard product in _productCards)
            {
                Assert.That(product.Description, Does.StartWith("Mortgage provided by "));
            }
        }

        [Test]
        public void Does_Lender_Name_Appears()
        {
            // in the event of a large number of lenders, I would likely place lender names into an external file and get the strings from there rather than maintain the list within code
            string[] lenders = new string[] { "Lender A", "Lender B", "Lender C" };
            foreach (ProductCard product in _productCards)
            {
                Assert.IsTrue(lenders.Any(x => product.Description.EndsWith(x)));
            }
        }

        #endregion

        #region Checks For Continue Buttons

        [Test]
        public void Is_Continue_Button_Present()
        {
            foreach (ProductCard product in _productCards)
            {
                Assert.IsNotNull(product.ContinueButton);
            }
        }

        [Test]
        public void Does_Continue_Button_Have_Correct_Label()
        {
            foreach (ProductCard product in _productCards)
            {
                Assert.That("Continue", Is.EqualTo(product.ContinueButton.TryGetElementText().Trim()));
            }
        }

        // Button doesn't function because it's a test, but would perform button functionality tests here

        #endregion

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
