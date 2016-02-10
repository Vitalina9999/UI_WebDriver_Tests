using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests.Core.Interfaces
{
    public interface IInvokable
    {
        void Invoke();
        bool IsDisplayed();
    }
}
