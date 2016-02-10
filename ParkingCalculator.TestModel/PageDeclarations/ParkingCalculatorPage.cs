using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using UITests.Core.WebDriver;

namespace ParkingCalculator.TestModel.PageDeclarations
{
    public class ParkingCalculatorPage : PageBase
    {
        #region Properties

        public string CurrentWindownName { get; set; }

        [FindsBy(How = How.Id, Using = "Lot")]
        private IWebElement _ddlChooseALot { get; set; }

        public SelectElement DdlChooseALot
        {
            get
            {
                SelectElement selectElement = new SelectElement(_ddlChooseALot);
                return selectElement;
            }
        }

        [FindsBy(How = How.XPath, Using = "/html/body/form/table/tbody/tr[2]/td[2]/font/a")]
        public IWebElement BtnEntryDateTimeCalendar { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/form/table/tbody/tr[3]/td[2]/font/a")]
        public IWebElement BtnLeavingDateTimeCalendar { get; set; }

        [FindsBy(How = How.Id, Using = "EntryDate")]
        public IWebElement EntryDateInput { get; set; }

        [FindsBy(How = How.Id, Using = "ExitDate")]
        public IWebElement ExitDateInput { get; set; }

        [FindsBy(How = How.Name, Using = "Submit")]
        public IWebElement BtnCalculate { get; set; }

        public CalendarWindow EntryCalendarWindow
        {
            get
            {
                return LoadCalendarWindow();
            }
        }

        public CalendarWindow LeavingCalendarWindow
        {
            get
            {
                return LoadCalendarWindow();
            }
        }

        #endregion

        #region Ctor
        public ParkingCalculatorPage()
        {
            CurrentWindownName = WebDriverHelper.Driver.CurrentWindowHandle;
        }
        #endregion

        #region Public Methods
        public override void Invoke()
        {
            Driver.Url = @"http://adam.goucher.ca/parkcalc";
        }

        public override bool IsDisplayed()
        {
            //return txtCaptcha.Displayed;
            return true;
        }

        public override void VerifyExpectedElementsAreDisplayed()
        {
            VerifyElementVisible("DdlChooseALot", DdlChooseALot.WrappedElement);
        }
        #endregion

        #region Private Methods
        private CalendarWindow LoadCalendarWindow()
        {
            CalendarWindow calendarWindow = null;

            ReadOnlyCollection<string> calendarNames = WebDriverHelper.Driver.WindowHandles;

            if (calendarNames.Count > 0)
            {
                string calendarWindowName = calendarNames.FirstOrDefault(x => x != CurrentWindownName);

                if (!string.IsNullOrEmpty(calendarWindowName))
                {
                    var calendarWindowsDriver = WebDriverHelper.Driver.SwitchTo().Window(calendarWindowName);

                    calendarWindow = new CalendarWindow(calendarWindowsDriver);
                    calendarWindow.ParentWindowName = CurrentWindownName;
                    return calendarWindow;
                }
            }

            return calendarWindow;
        }
        #endregion
    }
}
