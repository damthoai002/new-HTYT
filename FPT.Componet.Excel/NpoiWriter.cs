using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using System.IO;

namespace FPT.Component.ExcelPlus
{
    public class NpoiWriter : IExcelWriter
    {
        const short BOLD_WEIGHT = 700;
        const short NORMAL_WEIGHT = 400;
        //const int TOTAL_PALETE_COLOR = 64;

        //protected FileStream processingFile;
        protected HSSFWorkbook workbook;
        protected string currentFilePath;
        private IErrorLogger logger = null;
        private HSSFPalette customPalette;

        public NpoiWriter()
        {
            currentFilePath = string.Empty;
            logger = new DummyLogger();
        }

        public NpoiWriter(string filePath)
        {
            this.currentFilePath = filePath;
            logger = new DummyLogger();
        }

        protected void InitializePalette()
        {
            customPalette = workbook.GetCustomPalette();
        }

        protected short GetColorIndex(System.Drawing.Color color)
        {
            NPOI.HSSF.Util.HSSFColor result = customPalette.FindColor(color.R, color.G, color.B);
            if (result == null)
            {
                result = customPalette.AddColor(color.R, color.G, color.B);
            }
            return result.GetIndex();
        }


        #region IExcelWriter Members

        public bool WriteToWorkBook(IList<System.Data.DataTable> tables)
        {
            bool result = true;
            try
            {
                for (int i = 0; i < tables.Count; i++)
                {
                    string name = tables[i].TableName.Trim();
                    if (string.IsNullOrEmpty(name))
                    {
                        name = (i + 1).ToString();
                    }
                    workbook.CreateSheet(name);
                    result = WriteToWorkSheet(tables[i], i + 1);
                }
            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.LogException(ex);
                }
                result = false;
            }
            return result;
        }

        public bool WriteToWorkSheet(System.Data.DataTable table, int sheetNo)
        {
            bool result = true;
            try
            {
                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(sheetNo - 1);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.Row row = sheet.CreateRow(i);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        NPOI.SS.UserModel.Cell cell = row.CreateCell(j);
                        object value = table.Rows[i][j];
                        if (value is bool)
                            cell.SetCellValue((bool)value);
                        else if (value is DateTime)
                            cell.SetCellValue((DateTime)value);
                        else if (value is DateTime? && value != null)
                            cell.SetCellValue(((DateTime?)value).Value);
                        else if (Utility.IsNumericType(value.GetType()))
                            cell.SetCellValue((double)value);
                        else
                            cell.SetCellValue(value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                logger.LogException(ex);
            }
            return result;
        }

        public bool CreateWorkBook(string filePath)
        {
            bool result = true;
            try
            {
                //processingFile = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                //workbook = new HSSFWorkbook(processingFile);
                workbook = new HSSFWorkbook();
                currentFilePath = filePath;
                InitializePalette();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
                currentFilePath = string.Empty;
            }
            return result;
        }

        public bool CloseWorkBook()
        {
            bool result = true;
            //if (processingFile != null)
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                try
                {
                    //currentFilePath = string.Empty;
                    using (FileStream fs = new FileStream(currentFilePath, FileMode.Create))
                    {
                        workbook.Write(fs);
                    }
                    //processingFile.Dispose();
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                    result = false;
                }
                finally
                {
                    currentFilePath = string.Empty;
                    if (workbook != null)
                    {
                        workbook.Dispose();
                    }
                }
            }
            return result;
        }

        public bool MergeCells(IList<FRangeAddress> mergeCells, int sheetNo)
        {
            bool result = true;
            try
            {
                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(sheetNo - 1);

                foreach (FRangeAddress addr in mergeCells)
                {
                    NPOI.SS.Util.CellRangeAddress region = new NPOI.SS.Util.CellRangeAddress(addr.FromCell.Row - 1, addr.ToCell.Row - 1, addr.FromCell.Column - 1, addr.ToCell.Column - 1);
                    sheet.AddMergedRegion(region);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                result = false;
            }
            return result;
        }

        protected NPOI.SS.UserModel.CellBorderType GetBorderType(LineStyle style)
        {
            NPOI.SS.UserModel.CellBorderType result = NPOI.SS.UserModel.CellBorderType.NONE;
            switch (style)
            {
                case LineStyle.DashDot:
                    result = NPOI.SS.UserModel.CellBorderType.DASH_DOT;
                    break;
                case LineStyle.DashDotDot:
                    result = NPOI.SS.UserModel.CellBorderType.DASH_DOT_DOT;
                    break;
                case LineStyle.Dashed:
                    result = NPOI.SS.UserModel.CellBorderType.DASHED;
                    break;
                case LineStyle.Dotted:
                    result = NPOI.SS.UserModel.CellBorderType.DOTTED;
                    break;
                case LineStyle.Double:
                    result = NPOI.SS.UserModel.CellBorderType.DOUBLE;
                    break;
                case LineStyle.Hair:
                    result = NPOI.SS.UserModel.CellBorderType.HAIR;
                    break;
                case LineStyle.Medium:
                    result = NPOI.SS.UserModel.CellBorderType.MEDIUM;
                    break;
                case LineStyle.MediumDashDot:
                    result = NPOI.SS.UserModel.CellBorderType.MEDIUM_DASH_DOT;
                    break;
                case LineStyle.MediumDashDotDot:
                    result = NPOI.SS.UserModel.CellBorderType.MEDIUM_DASH_DOT_DOT;
                    break;
                case LineStyle.MediumDashed:
                    result = NPOI.SS.UserModel.CellBorderType.MEDIUM_DASHED;
                    break;
                case LineStyle.Thick:
                    result = NPOI.SS.UserModel.CellBorderType.THICK;
                    break;
                case LineStyle.Thin:
                    result = NPOI.SS.UserModel.CellBorderType.THIN;
                    break;
                default:
                    result = NPOI.SS.UserModel.CellBorderType.NONE;
                    break;
            }
            return result;
        }

        protected NPOI.SS.UserModel.HorizontalAlignment GetHAlign(FHorizontalAlignment align)
        {
            NPOI.SS.UserModel.HorizontalAlignment result = NPOI.SS.UserModel.HorizontalAlignment.GENERAL;
            switch (align)
            {
                case FHorizontalAlignment.Center:
                    result = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    break;
                case FHorizontalAlignment.CenterContinuous:
                    result = NPOI.SS.UserModel.HorizontalAlignment.CENTER_SELECTION;
                    break;
                case FHorizontalAlignment.Distributed:
                    result = NPOI.SS.UserModel.HorizontalAlignment.DISTRIBUTED;
                    break;
                case FHorizontalAlignment.Fill:
                    result = NPOI.SS.UserModel.HorizontalAlignment.FILL;
                    break;
                case FHorizontalAlignment.General:
                    result = NPOI.SS.UserModel.HorizontalAlignment.GENERAL;
                    break;
                case FHorizontalAlignment.Justify:
                    result = NPOI.SS.UserModel.HorizontalAlignment.JUSTIFY;
                    break;
                case FHorizontalAlignment.Left:
                    result = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
                    break;
                case FHorizontalAlignment.Right:
                    result = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
                    break;
                default:
                    result = NPOI.SS.UserModel.HorizontalAlignment.GENERAL;
                    break;
            }
            return result;
        }

        protected NPOI.SS.UserModel.VerticalAlignment GetVAlign(FVerticalAlignment align)
        {
            NPOI.SS.UserModel.VerticalAlignment result = NPOI.SS.UserModel.VerticalAlignment.TOP;
            switch (align)
            {
                case FVerticalAlignment.Bottom:
                    result = NPOI.SS.UserModel.VerticalAlignment.BOTTOM;
                    break;
                case FVerticalAlignment.Center:
                    result = NPOI.SS.UserModel.VerticalAlignment.CENTER;
                    break;
                case FVerticalAlignment.Top:
                    result = NPOI.SS.UserModel.VerticalAlignment.TOP;
                    break;
                case FVerticalAlignment.Distributed:
                    result = NPOI.SS.UserModel.VerticalAlignment.DISTRIBUTED;
                    break;
                case FVerticalAlignment.Justify:
                    result = NPOI.SS.UserModel.VerticalAlignment.JUSTIFY;
                    break;
                default:
                    result = NPOI.SS.UserModel.VerticalAlignment.TOP;
                    break;
            }
            return result;
        }

        public bool SetCellStyle(IList<FRangeAddress> cells, CellStyle style, int sheetNo)
        {
            bool result = true;
            try
            {
                NPOI.SS.UserModel.Font font = workbook.CreateFont();
                font.IsItalic = style.TextStyle.Italic;
                font.Boldweight = (style.TextStyle.Bold ? (BOLD_WEIGHT) : (NORMAL_WEIGHT));
                font.IsStrikeout = style.TextStyle.Strikeout;
                font.Underline = style.TextStyle.Underline ? HSSFFontFormatting.U_SINGLE : HSSFFontFormatting.U_NONE;
                font.Color = GetColorIndex(style.TextStyle.TextColor);
                if (style.TextStyle.Size > 0)
                    font.FontHeightInPoints = style.TextStyle.Size;

                if (!string.IsNullOrEmpty(style.TextStyle.FontName))
                    font.FontName = style.TextStyle.FontName;

                NPOI.SS.UserModel.CellStyle cellstyle = workbook.CreateCellStyle();
                cellstyle.SetFont(font);
                cellstyle.Alignment = GetHAlign(style.HAlign);
                cellstyle.VerticalAlignment = GetVAlign(style.VAlign);

                cellstyle.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;
                cellstyle.FillBackgroundColor = GetColorIndex(style.BackGroundColor);
                cellstyle.FillForegroundColor = GetColorIndex(style.BackGroundColor);

                cellstyle.BorderBottom = GetBorderType(style.Border.Bottom.Style);
                if (style.Border.Bottom.Style != LineStyle.None)
                    cellstyle.BottomBorderColor = GetColorIndex(style.Border.Bottom.BorderColor);

                cellstyle.BorderTop = GetBorderType(style.Border.Top.Style);
                if (style.Border.Top.Style != LineStyle.None)
                    cellstyle.TopBorderColor = GetColorIndex(style.Border.Top.BorderColor);

                cellstyle.BorderLeft = GetBorderType(style.Border.Left.Style);
                if (style.Border.Left.Style != LineStyle.None)
                    cellstyle.LeftBorderColor = GetColorIndex(style.Border.Left.BorderColor);

                cellstyle.BorderRight = GetBorderType(style.Border.Right.Style);
                if (style.Border.Right.Style != LineStyle.None)
                    cellstyle.RightBorderColor = GetColorIndex(style.Border.Right.BorderColor);

                cellstyle.WrapText = style.WrapText;
                if (!string.IsNullOrEmpty(style.NumberFormat))
                    cellstyle.DataFormat = workbook.CreateDataFormat().GetFormat(style.NumberFormat);

                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(sheetNo - 1);

                foreach (FRangeAddress addr in cells)
                {
                    for (int i = addr.FromCell.Row - 1; i < addr.ToCell.Row; i++)
                    {
                        NPOI.SS.UserModel.Row row = sheet.GetRow(i);
                        for (int j = addr.FromCell.Column - 1; j < addr.ToCell.Column; j++)
                        {
                            row.GetCell(j).CellStyle = cellstyle;
                        }
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

        public bool SetColumnWidth(int sheetNo, IList<int> columns, int width)
        {
            bool result = true;
            try
            {
                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(sheetNo - 1);

                foreach (int c in columns)
                {
                    sheet.SetColumnWidth(c - 1, width);
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
                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(sheetNo - 1);

                foreach (int r in rows)
                {
                    sheet.GetRow(r - 1).Height = (short)height;
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
                NPOI.SS.UserModel.Sheet sheet = workbook.GetSheetAt(sheetNo - 1);

                foreach (int c in columns)
                {
                    sheet.AutoSizeColumn(c - 1);
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
