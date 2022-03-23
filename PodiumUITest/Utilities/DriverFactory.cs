using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace PodiumUITest.Utilities
{
    public class DriverFactory
    {
        public static IWebDriver NewDefaultDriver(string driverType)
        {
            IWebDriver returnedValue = null;

            switch (driverType.ToLower())
            {
                case "chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    // Default needed options would go here
                    chromeOptions.AddArguments("--start-maximized");
                    chromeOptions.AddArguments("disable-extensions");
                    //chromeProfile.AddArguments(@"user-data-dir=C:\{some pathway to saved profiles}"); -- would be used as pre-harvested cookies to bypass login if needed etc (just an example) of an argument
                    returnedValue = new ChromeDriver(chromeOptions);
                    break;
                case "firefox":
                // Implementation of default firefox arguments
                // Same situation with any other desired browsers
                default:
                    break;
            }

            if (returnedValue == null)
            {
                throw new ArgumentException("DriverFactory: NewDefaultDriver: driver type " +
                                            driverType +
                                            "is unknown");
            }

            returnedValue.Manage().Timeouts().ImplicitWait =
                TimeSpan.FromSeconds(5); // Default the implicit wait to 5 seconds
            return returnedValue;
        }

        public static IWebDriver NewCustomDriver(ChromeOptions chromeOptions)
        {
            if (chromeOptions != null)
            {
                return new ChromeDriver(chromeOptions);
            }
            else
            {
                throw new NullReferenceException("DriverFactory: NewCustomDriver: chromeOptions was null");
            }
        }
    }
}
