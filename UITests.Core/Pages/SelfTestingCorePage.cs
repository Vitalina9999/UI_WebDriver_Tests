using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITests.Core.Interfaces;

namespace UITests.Core.Pages
{
    public abstract class SelfTestingCorePage : CorePage, IInvokable, ICheckExpectedWebElements
    {
        public abstract void Invoke();

        public abstract bool IsDisplayed();

        public abstract void VerifyExpectedElementsAreDisplayed();
    }
}
