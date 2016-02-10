using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using UITests.Core.WebDriver;

namespace UITests.Core.Pages
{
    public abstract class CorePage
    {
        public IWebDriver Driver { get { return WebDriverHelper.Driver; } }

        protected CorePage()
        {
            PageFactory.InitElements(Driver, this);
        }
    }
}
