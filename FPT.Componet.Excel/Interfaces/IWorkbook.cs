using System.Collections.Generic;

namespace FPT.Component.ExcelPlus
{
    public interface IWorkbook
    {
        IWorkSheets WorkSheets { get; }
        string Name { get; set; }
    }
}
