using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingCalculator.TestModel;
using ParkingCalculator.TestModel.PageDeclarations;

namespace ParkingCalculator.Tests.Smoke
{
    [TestClass]
    public class SmokeTestsForEachPage : TestBase
    {
        public void PageTest(PageBase page)
        {
            // Implement Dispose inside page object in order to do cleanup
            using (page)
            {
                // SwdBrowser.HandleJavaScriptErrors();

                page.Invoke();

                // SwdBrowser.HandleJavaScriptErrors();

                page.VerifyExpectedElementsAreDisplayed();

                // SwdBrowser.HandleJavaScriptErrors();
            }
        }

        // Add testMethods for your new pages here:
        // *PageName*_VerifyExpectedElements()

        [TestMethod]
        public void HomePage_VerifyExpectedElements()
        {
            ParkingCalculatorPage parkingCalculatorPage = Pages.ParkingCalculatorPage;

            PageTest(parkingCalculatorPage);
        }

        //[TestMethod]
        //public void CreateNewAccountPage_VerifyExpectedElements()
        //{
        //    PageTest(Pages.CreateNewAccountPage_VerifyExpectedElements);
        //}
    }
}
