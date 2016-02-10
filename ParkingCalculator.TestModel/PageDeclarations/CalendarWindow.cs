using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using UITests.Core.WebDriver;

namespace ParkingCalculator.TestModel.PageDeclarations
{
    public class CalendarWindow
    {
        private IWebDriver _webDriver;
        private IList<IWebElement> _calendarDaysLinks;

        public CalendarWindow(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string ParentWindowName { get; set; }

        public IWebDriver WrappedDriver
        {
            get
            {
                return _webDriver;
            }
        }

        public IList<IWebElement> CalendarDaysLinks
        {
            get
            {
                LoadCalendarDays();
                return _calendarDaysLinks;
            }
        }

        public void ClickByDay(int day)
        {
            LoadCalendarDays();

            foreach (var calenderLink in _calendarDaysLinks)
            {
                if (calenderLink.Text == day.ToString())
                {
                    calenderLink.Click();
                    WebDriverHelper.Driver.SwitchTo().Window(ParentWindowName);
                    break;
                }
            }
        }

        private void LoadCalendarDays()
        {
            _calendarDaysLinks = new List<IWebElement>();

            ReadOnlyCollection<IWebElement> tds = _webDriver.FindElements(By.TagName("td"));

            foreach (var td in tds)
            {
                if (td.GetAttribute("width") == "20")
                {
                    IWebElement link = td.FindElement(By.TagName("a"));
                    if (!string.IsNullOrEmpty(link.Text.Trim()))
                    {
                        _calendarDaysLinks.Add(link);
                    }
                }
            }
        }
    }
}
