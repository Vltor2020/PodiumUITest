using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodiumUITest.Extensions
{
    public static class SelEx
    {
        #region Driver 
        public static bool GoToUrl(this IWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Elements

        /// <summary>
        /// Is there at least 1 element in the DOM that meets the criteria of a given By query
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns>returns a bool indicating if an element is present</returns>
        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            if (by == null || driver == null)
            {
                return false;
            }

            try
            {
                driver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsElementPresent(this IWebElement parent, IWebDriver driver, By by)
        {
            if (parent == null || driver == null || by == null)
            {
                return false;
            }
            try
            {
                parent.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to find the first element that meets the criteria of a given By query.  If no element meets the criteria, returns null
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static IWebElement TryGetElement(this IWebDriver driver, By by)
        {
            if (driver == null || by == null)
            {
                return null;
            }

            if (driver.IsElementPresent(by))
            {
                return driver.FindElement(by);
            }
            else return null;
        }
        public static IWebElement TryGetChild(this IWebElement parent, IWebDriver driver, By by)
        {
            if (parent == null || driver == null || by == null)
            {
                return null;
            }

            if (parent.IsElementPresent(driver, by))
            {
                try
                {
                    return parent.FindElement(by);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static IWebElement TryGetChild(this IWebDriver driver, By parentBy, By by)
        {
            IWebElement parentElement = driver.TryGetElement(by);
            if (parentElement != null)
            {
                return parentElement.TryGetChild(driver, by);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  Tries To Get All elements on the page that meet the criteria of a BY statement - Returns NULL if BY Criteria not met
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static IList<IWebElement> TryGetElements(this IWebDriver driver, By by)
        {
            List<IWebElement> returnedValue = new List<IWebElement>();

            // If there's at least 1 element that meets this criteria
            if (driver.IsElementPresent(by))
            {
                returnedValue.AddRange(driver.FindElements(by));
                return returnedValue;
            }

            else return null;
        }
        public static IList<IWebElement> TryGetChildren(this IWebElement parent, IWebDriver driver, By by)
        {
            if (parent == null || driver == null || by == null)
            {
                return null;
            }

            List<IWebElement> returnedValue = new List<IWebElement>();
            if (parent.IsElementPresent(driver, by))
            {
                returnedValue.AddRange(parent.FindElements(by));
                return returnedValue;
            }
            else return null;
        }
        #endregion

        #region Element Text
        public static string TryGetElementText(this IWebElement element)
        {
            string returnedValue = "";
            if (element == null)
            {
                return null;
            }

            try
            {
                if (element.Text != null)
                {
                    returnedValue = element.Text;
                    return returnedValue;
                }
                else return null;
            }
            catch (NotFoundException ex)
            {
                return null;
            }
        }
        #endregion
    }
}
