using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ParkingCalculator.TestModel.Enums;
using ParkingCalculator.TestModel.PageDeclarations;

namespace ParkingCalculator.Tests.Sanity
{
    [TestClass]
    public class CalculatorTestsGui
    {

        [TestMethod]
        public void ChooseNextYearTest()
        {
            //ARRANGE
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            //ACT
            parkingCalculatorPage.DdlChooseALot.SelectByText("Short-Term Parking");

            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();

            IWebElement nextYearBtn = parkingCalculatorPage.EntryCalendarWindow.WrappedDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr[1]/td/table/tbody/tr/td[2]/a[2]"));
            nextYearBtn.Click();
            
            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            string valueTextOfNextYearBtn = parkingCalculatorPage.EntryDateInput.GetAttribute("value");

            int nextYear = DateTime.Now.Year + 1;

            int mouthNow = DateTime.Now.Month;

            int dayNow = DateTime.Now.Day;

            string dateTimeOfNextYear = mouthNow + "/" + dayNow + "/" + nextYear;


            //ASSERT
            Assert.AreEqual(valueTextOfNextYearBtn, dateTimeOfNextYear);

        }

        [TestMethod]
        public void ChooseBackYearTest()
        {
            //ARRANGE
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            //ACT
            parkingCalculatorPage.DdlChooseALot.SelectByText("Short-Term Parking");

            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();

            IWebElement backYearBtn = parkingCalculatorPage.EntryCalendarWindow.WrappedDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr[1]/td/table/tbody/tr/td[2]/a[1]"));
            backYearBtn.Click();

            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            string valueTextOfBackYearBtn = parkingCalculatorPage.EntryDateInput.GetAttribute("value");

            int backYear = DateTime.Now.Year - 1;

            int mouthNow = DateTime.Now.Month;

            int dayNow = DateTime.Now.Day;

            string dateTimeOfNextYear = mouthNow + "/" + dayNow + "/" + backYear;


            //ASSERT
            Assert.AreEqual(valueTextOfBackYearBtn, dateTimeOfNextYear);
        }
        
    }
}
