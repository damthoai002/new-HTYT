using System.Collections.Generic;

namespace FPT.Component.ExcelPlus
{
    public interface IExcelReader
    {
        IWorkbook ReadWorkbook(string filePath, IEnumerable<int> sheets);
        IWorkbook ReadWorkbook(string filePath);
    }
}
