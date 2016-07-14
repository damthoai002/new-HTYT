
using System.Collections.Generic;
using System.Drawing;
namespace FPT.Component.ExcelPlus
{
    public interface IExcelWriter
    {
        bool WriteToWorkBook(IList<System.Data.DataTable> tables);
        bool WriteToWorkSheet(System.Data.DataTable table, int sheetNo);

        bool CreateWorkBook(string filePath);
        bool CloseWorkBook();

        bool MergeCells(IList<FRangeAddress> mergeCells, int sheetNo);
        bool SetCellStyle(IList<FRangeAddress> cells, CellStyle style, int sheetNo);

        bool SetColumnWidth(int sheetNo, IList<int> columns, int width);
        bool SetRowHeight(int sheetNo, IList<int> rows, int height);
        bool SetAutoWidthColumn(int sheetNo, IList<int> columns);

        bool SetColumnWidth(int sheetNo, IList<string> columns, int width);
        bool SetRowHeight(int sheetNo, IList<string> rows, int height);
        bool SetAutoWidthColumn(int sheetNo, IList<string> columns);

        void SetLogger(IErrorLogger errorLogger);
    }
}
