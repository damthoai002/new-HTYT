using System.Collections.Generic;
using OfficeOpenXml;
using System.IO;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// Read Excel 2007 file to IWorkBook object.
    /// Note: All cells with boolean format will treate boolean value 0/1 instead of FALSE/TRUE.
    /// </summary>
    public class EpplusReader : IExcelReader
    {
        public EpplusReader()
        {
        }

        #region IExcelReader Members

        IWorkbook IExcelReader.ReadWorkbook(string filePath, IEnumerable<int> sheetIndexes)
        {
            FileInfo file = new FileInfo(filePath);
            IWorkbook wb = new WorkBook();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorkbook workbook = package.Workbook;
                IList<ISheet> sheetViews = GetSheetViews(workbook, sheetIndexes);
                wb.WorkSheets.AddRange(sheetViews);
            }
            return wb;
        }

        IWorkbook IExcelReader.ReadWorkbook(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            IWorkbook wb = new WorkBook();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorkbook workbook = package.Workbook;
                IList<ISheet> sheetViews = GetSheetViews(workbook);
                wb.WorkSheets.AddRange(sheetViews);
            }
            return wb;
        }

        #endregion

        protected IList<ISheet> GetSheetViews(ExcelWorkbook workbook, IEnumerable<int> sheetIndexes)
        {
            IList<ISheet> sheetViews = new List<ISheet>();
            foreach (int sheetNo in sheetIndexes)
            {
                if (workbook.Worksheets.Count >= sheetNo)
                {
                    ExcelWorksheet sheet = workbook.Worksheets[sheetNo];
                    ISheet sv = new SheetView();
                    sv.Cells = new EppCells(sheet);
                    sv.SheetNumber = sheetNo;
                    sv.SheetName = sheet.Name;
                    sheetViews.Add(sv);
                }
            }

            return sheetViews;
        }

        protected IList<ISheet> GetSheetViews(ExcelWorkbook workbook)
        {
            IList<ISheet> sheetViews = new List<ISheet>();
            foreach (ExcelWorksheet sheet in workbook.Worksheets)
            {
                ISheet sv = new SheetView();
                sv.Cells = new EppCells(sheet);
                sv.SheetNumber = sheet.Index;
                sv.SheetName = sheet.Name;
                sheetViews.Add(sv);
            }

            return sheetViews;
        }
    }
}
