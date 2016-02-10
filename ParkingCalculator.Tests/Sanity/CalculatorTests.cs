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
    public class CalculatorTests
    {
        [TestMethod]
        public void CheckAllLotsTest()
        {
            //ARRANGE
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            List<string> lotOption = new List<string>();
            lotOption.Add("Short-Term Parking");
            lotOption.Add("Economy Parking");
            lotOption.Add("Long-Term Surface Parking");
            lotOption.Add("Long-Term Garage Parking");
            lotOption.Add("Valet Parking");

            //ASSERT
            Assert.AreEqual(5, parkingCalculatorPage.DdlChooseALot.Options.Count);

            foreach (IWebElement selectOption in parkingCalculatorPage.DdlChooseALot.Options)
            {
                string textOption = selectOption.Text;

                Assert.IsTrue(lotOption.Contains(textOption), "Oops, text " + textOption + " does not exist");
            }
        }

        [TestMethod]
        public void StpLotTest()
        {
            //ARRANGE
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            //ACT
            parkingCalculatorPage.DdlChooseALot.SelectByText("Short-Term Parking");

            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();
            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 0; i < 10; i++)
            {
                if (daysInMonth != DateTime.Now.Day)
                {
                    parkingCalculatorPage.BtnLeavingDateTimeCalendar.Click();
                    parkingCalculatorPage.LeavingCalendarWindow.ClickByDay(DateTime.Now.AddDays(i + 1).Day);

                    parkingCalculatorPage.BtnCalculate.Click();

                    ReadOnlyCollection<IWebElement> tds = parkingCalculatorPage.Driver.FindElements(By.TagName("td"));

                    foreach (var td in tds)
                    {
                        if (td.GetAttribute("width") == "325")
                        {
                            IWebElement b = td.FindElement(By.TagName("b"));

                            string bText = b.Text.Remove(0, 2).Replace('.', ','); // remove "replace" for english localization

                            decimal priceActual = 0;
                            decimal.TryParse(bText, out priceActual);
                            if (priceActual == 0)
                            {
                                Assert.Fail("Price cannot be ZERO!");
                            }

                            decimal expectedPrice = CalculatePrice(i + 1, LotEnumType.Stp);

                            //ASSERT
                            Assert.AreEqual(expectedPrice, priceActual);

                        }
                    }
                }
            }
        }

        [TestMethod]
        public void EpLotTest()
        {
            //Arrange
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            //Act
            parkingCalculatorPage.DdlChooseALot.SelectByText("Economy Parking");
            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();

            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 0; i < 10; i++)
            {
                parkingCalculatorPage.BtnLeavingDateTimeCalendar.Click();

                parkingCalculatorPage.LeavingCalendarWindow.ClickByDay(DateTime.Now.AddDays(i + 1).Day);

                parkingCalculatorPage.BtnCalculate.Click();

                ReadOnlyCollection<IWebElement> tds = parkingCalculatorPage.Driver.FindElements(By.TagName("td"));

                foreach (var td in tds)
                {
                    if (td.GetAttribute("width") == "325")
                    {
                        IWebElement b = td.FindElement(By.TagName("b"));

                        string bText = b.Text.Remove(0, 2).Replace('.', ','); // remove "replace" for english localization
                        //
                        parkingCalculatorPage.DdlChooseALot.SelectByText("Economy Parking");
                        //

                        decimal priceActual = 0;
                        decimal.TryParse(bText, out priceActual);
                        if (priceActual == 0)
                        {
                            Assert.Fail("Price cannot be ZERO!");
                        }

                        decimal expectedPrice = CalculatePrice(i + 1, LotEnumType.Ep);

                        //ASSERT
                        Assert.AreEqual(expectedPrice, priceActual);

                    }
                }
            }
        }

        [TestMethod]
        public void LtsLotTest()
        {
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            parkingCalculatorPage.DdlChooseALot.SelectByText("Long-Term Surface Parking");
            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();

            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 0; i < 6; i++)
            {
                parkingCalculatorPage.BtnLeavingDateTimeCalendar.Click();

                parkingCalculatorPage.LeavingCalendarWindow.ClickByDay(DateTime.Now.AddDays(i + 1).Day);

                parkingCalculatorPage.BtnCalculate.Click();

                ReadOnlyCollection<IWebElement> tds = parkingCalculatorPage.Driver.FindElements(By.TagName("td"));

                foreach (var td in tds)
                {
                    if (td.GetAttribute("width") == "325")
                    {
                        IWebElement b = td.FindElement(By.TagName("b"));

                        string bText = b.Text.Remove(0, 2).Replace('.', ','); // remove "replace" for english localization

                        parkingCalculatorPage.DdlChooseALot.SelectByText("Long-Term Surface Parking");

                        decimal priceActual = 0;
                        decimal.TryParse(bText, out priceActual);
                        if (priceActual == 0)
                        {
                            Assert.Fail("Price cannot be ZERO!");
                        }

                        decimal expectedPrice = CalculatePrice(i + 1, LotEnumType.Lts);

                        //ASSERT
                        Assert.AreEqual(expectedPrice, priceActual);

                    }
                }
            }

        }

        [TestMethod]
        public void LtgLotTest()
        {
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            parkingCalculatorPage.DdlChooseALot.SelectByText("Long-Term Garage Parking");
            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();

            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 0; i < 6; i++)
            {

                parkingCalculatorPage.BtnLeavingDateTimeCalendar.Click();

                parkingCalculatorPage.LeavingCalendarWindow.ClickByDay(DateTime.Now.AddDays(i + 1).Day);

                parkingCalculatorPage.BtnCalculate.Click();

                ReadOnlyCollection<IWebElement> tds = parkingCalculatorPage.Driver.FindElements(By.TagName("td"));

                foreach (var td in tds)
                {
                    if (td.GetAttribute("width") == "325")
                    {
                        IWebElement b = td.FindElement(By.TagName("b"));

                        string bText = b.Text.Remove(0, 2).Replace('.', ','); // remove "replace" for english localization

                        parkingCalculatorPage.DdlChooseALot.SelectByText("Long-Term Garage Parking");

                        decimal priceActual = 0;
                        decimal.TryParse(bText, out priceActual);
                        if (priceActual == 0)
                        {
                            Assert.Fail("Price cannot be ZERO!");
                        }

                        decimal expectedPrice = CalculatePrice(i + 1, LotEnumType.Ltg);

                        //ASSERT
                        Assert.AreEqual(expectedPrice, priceActual);

                    }
                }
            }

        }

        [TestMethod]
        public void VpLotTest()
        {
            ParkingCalculatorPage parkingCalculatorPage = new ParkingCalculatorPage();
            parkingCalculatorPage.Invoke();

            parkingCalculatorPage.DdlChooseALot.SelectByText("Valet Parking");
            parkingCalculatorPage.BtnEntryDateTimeCalendar.Click();

            parkingCalculatorPage.EntryCalendarWindow.ClickByDay(DateTime.Now.Day);

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 0; i < 6; i++)
            {
                parkingCalculatorPage.BtnLeavingDateTimeCalendar.Click();

                parkingCalculatorPage.LeavingCalendarWindow.ClickByDay(DateTime.Now.AddDays(i + 1).Day);

                parkingCalculatorPage.BtnCalculate.Click();

                ReadOnlyCollection<IWebElement> tds = parkingCalculatorPage.Driver.FindElements(By.TagName("td"));

                foreach (var td in tds)
                {
                    if (td.GetAttribute("width") == "325")
                    {
                        IWebElement b = td.FindElement(By.TagName("b"));

                        string bText = b.Text.Remove(0, 2).Replace('.', ','); // remove "replace" for english localization

                        parkingCalculatorPage.DdlChooseALot.SelectByText("Valet Parking");

                        decimal priceActual;
                        decimal.TryParse(bText, out priceActual);
                        if (priceActual == 0)
                        {
                            Assert.Fail("Price cannot be ZERO!");
                        }

                        decimal expectedPrice = CalculatePrice(i + 1, LotEnumType.Vp);

                        //ASSERT
                        Assert.AreEqual(expectedPrice, priceActual);

                    }
                }
            }

        }



        #region CalculatePrice
        private decimal CalculatePrice(int daysCount, LotEnumType lotType)
        {
            decimal result = 0;
            int priceOfFirstDate = 0;
            int priceOfNonFirstDay = 0;

            switch (lotType)
            {
                case LotEnumType.Stp:
                    priceOfFirstDate = 28;
                    priceOfNonFirstDay = 26;

                    if (daysCount == 1)
                    {
                        result = priceOfFirstDate;
                    }
                    else
                    {
                        result = (priceOfNonFirstDay * (daysCount - 1)) + priceOfFirstDate;
                    }

                    break;

                case LotEnumType.Ep:
                    priceOfFirstDate = 9;
                    if (daysCount == 1)
                    {
                        result = priceOfFirstDate;
                    }
                    else
                    {  // on site is incorrect calculate: for 6 and 7 days the same price $54, but should be $63 for 7 day
                        result = (priceOfFirstDate * (daysCount - 1)) + priceOfFirstDate;
                    }

                    break;

                case LotEnumType.Lts:
                    priceOfFirstDate = 10;
                    if (daysCount == 1)
                    {
                        result = priceOfFirstDate;
                    }
                    else
                    {
                        // on site is incorrect calculate: for 6 and 7 day the same price $60
                        result = (priceOfFirstDate * (daysCount - 1)) + priceOfFirstDate;
                    }
                    break;


                case LotEnumType.Ltg:
                    priceOfFirstDate = 12;
                    if (daysCount == 1)
                    {
                        result = priceOfFirstDate;
                    }
                    else
                    {
                        // on site is incorrect calculate: for 6 and 7 day the same price $72
                        result = (priceOfFirstDate * (daysCount - 1)) + priceOfFirstDate;
                    }

                    break;

                case LotEnumType.Vp:
                    priceOfFirstDate = 42;
                    priceOfNonFirstDay = 30;

                    if (daysCount == 1)
                    {
                        result = priceOfFirstDate;
                    }
                    else
                    {
                        result = (priceOfNonFirstDay * (daysCount - 1)) + priceOfFirstDate;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException("lotType");
            }

            return result;
        }

        #endregion
    }
}
