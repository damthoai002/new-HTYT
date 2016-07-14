using OfficeOpenXml;
using System;

namespace FPT.Component.ExcelPlus
{
    public class EppCells : IRange
    {
        ExcelWorksheet range;
        int startRow;
        int endRow;
        int startColumn;
        int endColumn;
        #region IDataArray Members

        public string this[int row, int column]
        {
            get
            {
                //if (range.Cells[row, column] == null || range.Cells[row, column].Text == null)
                //{
                //    return string.Empty;
                //}
                if (range.Cells[row, column].Text.Trim().Equals(string.Empty))
                {
                    object obj = range.Cells[row, column].Value;
                    if (obj != null)
                    {
                        try
                        {

                            int i = Convert.ToInt16(obj);
                            return i.ToString();
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    }
                }
                return range.Cells[row, column].Text;
            }
            set
            {
                range.Cells[row, column].Value = value;
            }
        }

        public string this[int row, string column]
        {
            get
            {
                int col = Utility.GetExcelColumnAddress(column);
                //if (range.Cells[row, col] == null || range.Cells[row, col].Value == null)
                //{
                //    return string.Empty;
                //}
                if (range.Cells[row, col].Text.Trim().Equals(string.Empty))
                {
                    object obj = range.Cells[row, col].Value;
                    if (obj != null)
                    {
                        try
                        {
                            int i = Convert.ToInt16(obj);
                            return i.ToString();
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    }
                }
                return range.Cells[row, col].Text;
            }
            set
            {
                int col = Utility.GetExcelColumnAddress(column);
                range.Cells[row, col].Value = value;
            }
        }

        public string this[string cellAddress]
        {
            get
            {
                FCellAddress addr = Utility.GetExcelAddress(cellAddress);
                return this[addr.Row, addr.Column];
            }
            set
            {
                FCellAddress addr = Utility.GetExcelAddress(cellAddress);
                this[addr.Row, addr.Column] = value;
            }
        }

        public int EndRow
        {
            get { return endRow; }
        }

        public int EndColumn
        {
            get { return endColumn; }
        }

        public int StartRow
        {
            get { return startRow; }
        }

        public int StartColumn
        {
            get { return startColumn; }
        }

        #endregion

        public EppCells(ExcelWorksheet excelSheet)
        {
            range = excelSheet;
            if (range.Dimension != null)
            {
                startRow = range.Dimension.Start.Row;
                startColumn = range.Dimension.Start.Column;
                endColumn = GetRight(range) - startColumn + 1;
                endRow = GetBottom(range) - startRow + 1;
            }
            else
            {
                startRow = 0;
                endRow = 0;
                startColumn = 0;
                endColumn = 0;
            }
        }

        protected int GetRight(ExcelWorksheet excelRange)
        {

            int endCol = excelRange.Dimension.End.Column;
            int startCol = excelRange.Dimension.Start.Column;
            int startRow = excelRange.Dimension.Start.Row;
            int endRow = excelRange.Dimension.End.Row;
            int col = endCol;
            for (; col >= startCol; col--)
            {
                bool hasData = false;
                for (int i = startRow; i <= endRow; i++)
                {
                    if (excelRange.Cells[i, col].Value != null && !string.IsNullOrEmpty(excelRange.Cells[i, col].Value.ToString()))
                    {
                        hasData = true;
                        break;
                    }
                }
                if (hasData)
                {
                    break;
                }
            }
            return col;
        }

        protected int GetBottom(ExcelWorksheet excelRange)
        {
            int endCol = excelRange.Dimension.End.Column;
            int startCol = excelRange.Dimension.Start.Column;
            int startRow = excelRange.Dimension.Start.Row;
            int endRow = excelRange.Dimension.End.Row;
            int row = endRow;
            for (; row >= startRow; row--)
            {
                bool hasData = false;
                for (int i = startCol; i <= endCol; i++)
                {
                    if (excelRange.Cells[row, i].Value != null && !string.IsNullOrEmpty(excelRange.Cells[row, i].Value.ToString()))
                    {
                        hasData = true;
                        break;
                    }
                }
                if (hasData)
                {
                    break;
                }
            }
            return row;
        }
    }
}
