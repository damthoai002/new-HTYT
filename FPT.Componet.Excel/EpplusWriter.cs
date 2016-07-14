using System;
using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;
using System.IO;

namespace FPT.Component.ExcelPlus
{
    public class EpplusWriter : IExcelWriter
    {
        protected ExcelPackage package = null;
        protected string currentFilePath;
        protected IErrorLogger logger = null;
        public string CurrentFile { get { return currentFilePath; } }

        public EpplusWriter()
        {
            currentFilePath = string.Empty;
            logger = new DummyLogger();
        }

        public EpplusWriter(string filePath)
        {
            this.currentFilePath = filePath;
            logger = new DummyLogger();
        }

        #region IExcelWriterMembers

        public bool WriteToWorkBook(IList<System.Data.DataTable> tables)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                for (int i = 0; i < tables.Count; i++)
                {
                    string name = tables[i].TableName.Trim();
                    if (string.IsNullOrEmpty(name))
                    {
                        name = (i + 1).ToString();
                    }
                    workbook.Worksheets.Add(name);
                    result = WriteToWorkSheet(tables[i], i + 1);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool WriteToWorkSheet(System.Data.DataTable table, int sheetNo)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet sheet = workbook.Worksheets[sheetNo];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        object value = table.Rows[i][j];
                        //decimal tmp = 0m;
                        //if (Utility.IsNumeric(value, out tmp))
                        //{
                        //    sheet.Cells[i + 1, j + 1].Value = tmp;
                        //}
                        //else
                        //{
                        sheet.Cells[i + 1, j + 1].Value = table.Rows[i][j];
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool CreateWorkBook(string filePath)
        {
            bool result = true;
            this.currentFilePath = filePath;
            if (package != null)
            {
                result = CloseWorkBook();
            }
            try
            {
                package = new ExcelPackage();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool CloseWorkBook()
        {
            bool result = true;
            if (package != null)
            {
                try
                {
                    using (Stream file = new FileStream(currentFilePath, FileMode.Create))
                    {
                        package.SaveAs(file);
                    }
                    package.Dispose();
                    currentFilePath = string.Empty;
                    package = null;
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                    result = false;
                    package = null;
                }
            }
            return result;
        }

        public bool MergeCells(IList<FRangeAddress> mergeCells, int sheetNo)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet sheet = workbook.Worksheets[sheetNo];
                foreach (FRangeAddress addr in mergeCells)
                {
                    ExcelRange range = sheet.Cells[addr.FromCell.Row, addr.FromCell.Column, addr.ToCell.Row, addr.ToCell.Column];
                    range.Merge = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public OfficeOpenXml.Style.ExcelBorderStyle GetLineStyle(LineStyle style)
        {
            int tmp = (int)style;
            return (OfficeOpenXml.Style.ExcelBorderStyle)tmp;
        }

        public bool SetCellStyle(IList<FRangeAddress> cells, CellStyle style, int sheetNo)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet sheet = workbook.Worksheets[sheetNo];

                foreach (FRangeAddress c in cells)
                {
                    ExcelRange range = sheet.Cells[c.FromCell.Row, c.FromCell.Column, c.ToCell.Row, c.ToCell.Column];

                    range.Style.Border.Top.Style = GetLineStyle(style.Border.Top.Style);
                    if (style.Border.Top.Style != LineStyle.None)
                        range.Style.Border.Top.Color.SetColor(style.Border.Top.BorderColor);

                    range.Style.Border.Bottom.Style = GetLineStyle(style.Border.Bottom.Style);
                    if (style.Border.Bottom.Style != LineStyle.None)
                        range.Style.Border.Bottom.Color.SetColor(style.Border.Bottom.BorderColor);

                    range.Style.Border.Left.Style = GetLineStyle(style.Border.Left.Style);
                    if (style.Border.Left.Style != LineStyle.None)
                        range.Style.Border.Left.Color.SetColor(style.Border.Left.BorderColor);

                    range.Style.Border.Right.Style = GetLineStyle(style.Border.Right.Style);
                    if (style.Border.Right.Style != LineStyle.None)
                        range.Style.Border.Right.Color.SetColor(style.Border.Right.BorderColor);

                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.PatternColor.SetColor(style.BackGroundColor);
                    range.Style.Fill.BackgroundColor.SetColor(style.BackGroundColor);

                    range.Style.Font.Color.SetColor(style.TextStyle.TextColor);
                    range.Style.Font.Bold = style.TextStyle.Bold;
                    range.Style.Font.Italic = style.TextStyle.Italic;
                    range.Style.Font.Strike = style.TextStyle.Strikeout;
                    range.Style.Font.UnderLine = style.TextStyle.Underline;
                    if (!string.IsNullOrEmpty(style.NumberFormat))
                        range.Style.Numberformat.Format = style.NumberFormat;

                    range.Style.WrapText = style.WrapText;

                    if (style.TextStyle.Size > 0)
                        range.Style.Font.Size = style.TextStyle.Size;
                    if (!string.IsNullOrEmpty(style.TextStyle.FontName))
                        range.Style.Font.Name = style.TextStyle.FontName;

                    range.Style.HorizontalAlignment = (OfficeOpenXml.Style.ExcelHorizontalAlignment)style.HAlign;
                    range.Style.VerticalAlignment = (OfficeOpenXml.Style.ExcelVerticalAlignment)style.VAlign;

                }

            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool SetColumnWidth(int sheetNo, IList<int> columns, int width)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet sheet = workbook.Worksheets[sheetNo];
                foreach (int col in columns)
                {
                    sheet.Column(col).Width = width;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool SetRowHeight(int sheetNo, IList<int> rows, int height)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet sheet = workbook.Worksheets[sheetNo];
                foreach (int r in rows)
                {
                    sheet.Row(r).Height = height;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool SetAutoWidthColumn(int sheetNo, IList<int> columns)
        {
            bool result = true;
            try
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet sheet = workbook.Worksheets[sheetNo];
                foreach (int col in columns)
                {
                    sheet.Column(col).BestFit = true;
                    sheet.Column(col).AutoFit();
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        public bool SetColumnWidth(int sheetNo, IList<string> columns, int width)
        {
            IList<int> columnCollection = new List<int>();
            foreach (string c in columns)
            {
                columnCollection.Add(Utility.GetExcelColumnAddress(c));
            }
            return SetColumnWidth(sheetNo, columnCollection, width);
        }

        public bool SetRowHeight(int sheetNo, IList<string> rows, int height)
        {
            IList<int> rowCollection = new List<int>();
            foreach (string c in rows)
            {
                rowCollection.Add(Utility.GetExcelColumnAddress(c));
            }
            return SetRowHeight(sheetNo, rowCollection, height);
        }

        public bool SetAutoWidthColumn(int sheetNo, IList<string> columns)
        {
            IList<int> columnCollection = new List<int>();
            foreach (string c in columns)
            {
                columnCollection.Add(Utility.GetExcelColumnAddress(c));
            }
            return SetAutoWidthColumn(sheetNo, columnCollection);
        }


        public void SetLogger(IErrorLogger errorLogger)
        {
            logger = errorLogger;
        }

        #endregion
    }
}
