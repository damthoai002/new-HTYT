using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// Read Excel 2003 file to IWorkbook object
    /// </summary>
    public class NpoiReader : IExcelReader
    {
        public NpoiReader()
        {
        }

        #region IExcelReader Members

        public IWorkbook ReadWorkbook(string filePath, IEnumerable<int> sheets)
        {
            IWorkbook wbView = new WorkBook();
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                using (HSSFWorkbook workbook = new HSSFWorkbook(reader))
                {
                    foreach (int index in sheets)
                    {
                        Sheet sheet = workbook.GetSheetAt(index - 1);
                        SheetView sv = new SheetView();
                        sv.SheetName = sheet.SheetName;
                        sv.SheetNumber = index;
                        sv.Cells = ReadSheet(sheet);
                        wbView.WorkSheets.Add(sv);
                    }
                }
            }

            return wbView;
        }

        public IWorkbook ReadWorkbook(string filePath)
        {
            IWorkbook wbView = new WorkBook();
            using (FileStream reader = new FileStream(filePath, FileMode.Open))
            {
                using (HSSFWorkbook workbook = new HSSFWorkbook(reader))
                {
                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        Sheet sheet = workbook.GetSheetAt(i);
                        SheetView sv = new SheetView();
                        sv.SheetName = sheet.SheetName;
                        sv.SheetNumber = i + 1;
                        sv.Cells = ReadSheet(sheet);
                        wbView.WorkSheets.Add(sv);
                    }
                }
            }
            return wbView;
        }

        #endregion

        private IRange ReadSheet(Sheet sheet)
        {
            List<List<string>> table = new List<List<string>>();
            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                Row row = sheet.GetRow(i);
                List<string> value = new List<string>();
                if (row != null)
                {
                    for (int j = 0; j < row.FirstCellNum; j++)
                    {
                        value.Add(string.Empty);
                    }
                    for (int j = row.FirstCellNum; j <= row.LastCellNum; j++)
                    {
                        Cell cell = row.GetCell(j);

                        if (cell != null)
                        {
                            //switch (cell.CellType)
                            //{
                            //    //case CellType.BOOLEAN:
                            //    //    value.Add(cell.BooleanCellValue.ToString());
                            //    //    break;
                            //    case CellType.ERROR:
                            //        value.Add(string.Empty);
                            //        break;
                            //    case CellType.FORMULA:
                            //        value.Add(cell.StringCellValue);
                            //        break;
                            //    case CellType.NUMERIC:
                            //        value.Add(cell.NumericCellValue.ToString());
                            //        break;
                            //    //case CellType.STRING:
                            //    //    value.Add(cell.StringCellValue);
                            //    //    break;
                            //    case CellType.Unknown:
                            //        value.Add(cell.StringCellValue);
                            //        break;
                            //    default:
                            //        value.Add(cell.ToString());
                            //        break;
                            //}
                            value.Add(cell.ToString());

                        }
                        else
                        {
                            value.Add(string.Empty);
                        }
                    }
                }
                table.Add(value);
            }

            DataArray data = new DataArray(table, 1, 1);
            data.TrimBottom();
            data.TrimRight();
            return data;
        }
    }
}
