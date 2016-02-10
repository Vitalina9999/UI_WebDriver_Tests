using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using UITests.Core.Configuration;
using UITests.Core.Exceptions;

namespace UITests.Core.WebDriver
{
    public static class WebDriverHelper
    {
        private static IWebDriver _driver = null;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = WebDriverRunner.Run(Config.swdBrowserType,
                                                  Config.wdIsRemote,
                                                  Config.wdRemoteUrl);
                }
                return _driver;
            }
        }

        /// <summary>
        /// Closes the current WebDriver instance (and a web-browser window)
        /// </summary>
        public static void CloseDriver()
        {
            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="jsCode">JavaScript code block</param>
        /// <returns>The value returned by the script</returns>
        public static object ExecuteScript(string jsCode)
        {
            return (Driver as IJavaScriptExecutor).ExecuteScript(jsCode);
        }

        /// <summary>
        /// *Executes JavaScript code block inside opened Web-Browser*   
        ///  
        /// Collects JavaScript errors on the page and throws JavaScriptErrorOnThePageException 
        /// in case unhandled JavaScript errors had occurred on the WebPage   
        /// During the first call on the specific web page, this method injects a script 
        /// for error collection into the web page. The next calls will check if there 
        /// are errors collected.   
        /// If any JavaScript errors are captured – the method will throw JavaScriptErrorOnThePageException
        /// </summary>
        public static void HandleJavaScriptErrors()
        {
            string jsCode =
            #region JavaScript Error Handler code
 @"
                    if (typeof window.jsErrors === 'undefined') 
                    {
                        window.jsErrors = '';
                        window.onerror = function (errorMessage, url, lineNumber) 
                                         {
                                              var message = 'Error: [' + errorMessage + '], url: [' + url + '], line: [' + lineNumber + ']';
                                              message = message + ""\n"";
                                              window.jsErrors += message;
                                              return false;
                                         };
                    }

                    var errors = window.jsErrors;
                    window.jsErrors = '';
                    return errors;";
            #endregion

            string errors = "";
            errors = (string)ExecuteScript(jsCode);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new JavaScriptErrorOnThePageException(errors);
            }
        }

        public static Screenshot TakeScreenshot()
        {
            return Driver.TakeScreenshot();
        }

        /// <summary>
        /// Driver Finalizer: automatically closes WebDriver when the test suite run in completed
        /// </summary>
        static readonly Finalizer finalizer = new Finalizer();

        sealed class Finalizer
        {
            ~Finalizer()
            {
                CloseDriver();
            }
        }
    }
}
