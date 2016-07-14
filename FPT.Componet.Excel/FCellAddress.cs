using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public class FCellAddress
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public FCellAddress()
        {
            Row = 0;
            Column = 0;
        }

        public FCellAddress(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public FCellAddress(int row, string column)
        {
            Row = row;
            Column = Utility.GetExcelColumnAddress(column);
        }

        public static FCellAddress FromString(string cellAddress)
        {
            return Utility.GetExcelAddress(cellAddress);
        }
    }

    public class FRangeAddress
    {
        public FCellAddress FromCell { get; set; }
        public FCellAddress ToCell { get; set; }

        public FRangeAddress()
        {
            FromCell = new FCellAddress();
            ToCell = new FCellAddress();
        }

        public FRangeAddress(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            FromCell = new FCellAddress(fromRow, fromColumn);
            ToCell = new FCellAddress(toRow, toColumn);
        }
    }
}
