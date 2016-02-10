using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UITests.Core.Pages;

namespace ParkingCalculator.TestModel
{
    public abstract class PageBase : SelfTestingCorePage, IDisposable
    {
        // Verifies the expected WebElement to be Visible
        public virtual void VerifyElementVisible(string elementName, IWebElement webElement)
        {
            if (!webElement.Displayed)
            {
                string message = "Error: WebElement with name <" + elementName + ">\n"
                                 + "was expected to be visible,"
                                 + "but the element was not found on the page.";

                throw new Exception();
            }
        }

        // Override and implement this method, 
        // in case you want the pages to clean up
        public virtual void Dispose()
        {
            // Does nothing at the moment
        }
    }
}
