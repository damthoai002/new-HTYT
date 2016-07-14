using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public interface IErrorLogger
    {
        void LogError(object error);
        void LogException(Exception ex);
    }
}
