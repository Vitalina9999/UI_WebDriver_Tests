using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITests.Core.WebDriver;

namespace ParkingCalculator.Tests
{
    public abstract class TestBase
    {
        private TestContext _testContext;

        public TestContext TestContext
        {
            get { return _testContext; }
        }

        [TestCleanup]
        public virtual void TestCleanUp()
        {
            var testResult = TestContext.CurrentTestOutcome;

            bool isTestFailed = !(testResult == UnitTestOutcome.Passed || testResult == UnitTestOutcome.Inconclusive);


            if (isTestFailed)
            {
                try
                {
                    var myUniqueFileName = string.Format(@"screenshot_{0}.png", Guid.NewGuid());
                    var fullPath = Path.Combine(Path.GetTempPath(), myUniqueFileName);

                    var screenshot = WebDriverHelper.TakeScreenshot();
                    screenshot.SaveAsFile(fullPath, ImageFormat.Png);

                    // Attach image to the log file
                    TestContext.AddResultFile(fullPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to take screenshot:" + e.Message);
                }
            }

            bool shouldRestartBrowser = isTestFailed;
            if (shouldRestartBrowser)
            {
                WebDriverHelper.CloseDriver();
            }
        }
    }
}
