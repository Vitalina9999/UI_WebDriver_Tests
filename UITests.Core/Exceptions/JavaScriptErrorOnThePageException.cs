using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests.Core.Exceptions
{
    public class JavaScriptErrorOnThePageException : Exception
    {
        public JavaScriptErrorOnThePageException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
