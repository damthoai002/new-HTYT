using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// Read Excel 2003/2007 file to IWorkbook object.
    /// The correlative excel application is required.
    /// </summary>
    public class ExcelCOMReader : IExcelReader
    {
        public ExcelCOMReader()
        {
        }

        #region IExcelReader Members

        public IWorkbook ReadWorkbook(string filePath, IEnumerable<int> sheets)
        {
            IWorkbook workbook = new WorkBook();
            foreach (int index in sheets)
            {
                ISheet sheet = ReadSheetView(filePath, index);
                if (sheet != null)
                {
                    workbook.WorkSheets.Add(sheet);
                }
            }
            return workbook;
        }

        public IWorkbook ReadWorkbook(string filePath)
        {
            IWorkbook workbook = new WorkBook();
            workbook.WorkSheets.AddRange(ReadAllSheetView(filePath));
            return workbook;
        }
        #endregion


        protected ISheet ReadSheetView(string filePath, int sheetNumber)
        {
            object[,] data = null;
            object m_objMissing = System.Reflection.Missing.Value;
            Excel._Worksheet workSheet;
            Excel.Workbook workBook = null;
            Excel.Application excelApp = null;
            try
            {
                ISheet sheet = new SheetView();
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                workBook = excelApp.Workbooks.Open(filePath, m_objMissing, true, m_objMissing, m_objMissing, m_objMissing, m_objMissing, m_objMissing, Excel.XlSaveAsAccessMode.xlNoChange, m_objMissing, m_objMissing, m_objMissing, m_objMissing, m_objMissing, m_objMissing);
                workSheet = (Excel._Worksheet)workBook.Worksheets[sheetNumber];

                sheet.SheetName = workSheet.Name;
                sheet.SheetNumber = sheetNumber;

                Excel.Range range = workSheet.UsedRange;
                int padRow = range.Row;
                int padCol = range.Column;
                data = (object[,])range.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                Marshal.ReleaseComObject(range);
                workBook.Close(false, string.Empty, false);
                excelApp.DisplayAlerts = true;

                int rowBase = 1;
                int colBase = 1;

                DataArray table = new DataArray(data, 1, 1, rowBase, colBase);
                table.InsertRows(0, padRow - 1);
                table.InsertColumns(0, padCol - 1);

                table.TrimBottom();
                table.TrimRight();
                sheet.Cells = table;

                return sheet;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                #region - release excel instance -
                IntPtr xlsApplicationProcessID = new IntPtr(0);
                Common.GetWindowThreadProcessId(excelApp.Hwnd, ref xlsApplicationProcessID);
                System.Diagnostics.Process.GetProcessById(Convert.ToInt32(xlsApplicationProcessID.ToString())).Kill();
                #endregion
            }
        }

        protected List<ISheet> ReadAllSheetView(string filePath)
        {
            object[,] data = null;
            object m_objMissing = System.Reflection.Missing.Value;
            Excel._Worksheet workSheet;
            Excel.Workbook workBook = null;
            Excel.Application excelApp = null;
            try
            {
                List<ISheet> result = new List<ISheet>();
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                workBook = excelApp.Workbooks.Open(filePath, m_objMissing, true, m_objMissing, m_objMissing, m_objMissing, m_objMissing, m_objMissing, Excel.XlSaveAsAccessMode.xlNoChange, m_objMissing, m_objMissing, m_objMissing, m_objMissing, m_objMissing, m_objMissing);
                for (int i = 1; 1 <= workBook.Worksheets.Count; i++)
                {
                    ISheet sheet = new SheetView();

                    workSheet = (Excel._Worksheet)workBook.Worksheets[i];

                    sheet.SheetName = workSheet.Name;
                    sheet.SheetNumber = i;

                    Excel.Range range = workSheet.UsedRange;
                    int padRow = range.Row;
                    int padCol = range.Column;
                    data = (object[,])range.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                    Marshal.ReleaseComObject(range);
                    int rowBase = 1;
                    int colBase = 1;
                    DataArray table = new DataArray(data, 1, 1, rowBase, colBase);
                    table.InsertRows(0, padRow - 1);
                    table.InsertColumns(0, padCol - 1);
                    table.TrimBottom();
                    table.TrimRight();
                    sheet.Cells = table;
                    result.Add(sheet);
                }
                workBook.Close(false, string.Empty, false);
                excelApp.DisplayAlerts = true;
                return result;
            }
            catch (Exception)
            {
                return new List<ISheet>();
            }
            finally
            {
                #region - release excel instance -
                IntPtr xlsApplicationProcessID = new IntPtr(0);
                Common.GetWindowThreadProcessId(excelApp.Hwnd, ref xlsApplicationProcessID);
                System.Diagnostics.Process.GetProcessById(Convert.ToInt32(xlsApplicationProcessID.ToString())).Kill();
                #endregion
            }
        }
    }
}
