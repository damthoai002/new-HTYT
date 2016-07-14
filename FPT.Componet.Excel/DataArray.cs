using System.Collections.Generic;

namespace FPT.Component.ExcelPlus
{
    public class DataArray : IRange
    {
        private List<List<string>> table;
        int maxCol;
        private int rBase;
        private int cBase;

        /// <summary>
        /// Gets or sets data with zero-based index
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string this[int row, int column]
        {
            get
            {
                return table[row - rBase][column - cBase];

            }
            set
            {
                table[row - rBase][column - cBase] = value;
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

        public string this[int row, string column]
        {
            get
            {
                int col = Utility.GetExcelColumnAddress(column);
                return table[row - rBase][col - cBase];

            }
            set
            {
                int col = Utility.GetExcelColumnAddress(column);
                table[row - rBase][col - cBase] = value;
            }
        }

        public int EndRow { get { return table.Count; } }

        public int EndColumn { get { return maxCol; } }

        public DataArray(object[,] data, int startRowIndex, int startColIndex, int rowBase, int colBase)
        {
            if (data != null)
            {
                table = new List<List<string>>();
                int maxRow = data.GetLength(0);
                maxCol = data.GetLength(1);
                table = new List<List<string>>();
                for (int r = 0; r < maxRow; r++)
                {
                    List<string> row = new List<string>();
                    for (int c = 0; c < maxCol; c++)
                    {
                        if (data[r + startRowIndex, c + startColIndex] == null)
                        {
                            row.Add(string.Empty);
                        }
                        else
                        {
                            row.Add(data[r + startRowIndex, c + startColIndex].ToString());
                        }
                    }
                    table.Add(row);
                }
            }
            else
            {
                maxCol = 0;
                table = new List<List<string>>();
            }
            rBase = rowBase;
            cBase = colBase;
        }

        public DataArray(List<List<string>> data, int rowBase, int colBase)
        {
            if (data != null)
            {
                maxCol = 0;
                foreach (List<string> row in data)
                {
                    if (maxCol < row.Count) maxCol = row.Count;
                }
                table = new List<List<string>>();
                foreach (List<string> row in data)
                {
                    List<string> tmp = new List<string>();
                    tmp.AddRange(row.ToArray());
                    int add = maxCol - tmp.Count;
                    for (int i = 0; i < add; i++)
                    {
                        tmp.Add(string.Empty);
                    }
                    table.Add(tmp);
                }
            }
            else
            {
                maxCol = 0;
                table = new List<List<string>>();
            }
            rBase = rowBase;
            cBase = colBase;
        }

        public DataArray()
        {
            rBase = 0;
            cBase = 0;
            maxCol = 0;
            table = new List<List<string>>();
        }

        public DataArray(int rowBase, int colBase)
        {
            maxCol = 0;
            table = new List<List<string>>();
            rBase = rowBase;
            cBase = colBase;
        }

        public void AddRow()
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < maxCol; i++)
            {
                tmp.Add(string.Empty);
            }
            table.Add(new List<string>(tmp.ToArray()));
        }

        public void TrimRight()
        {
            int col = maxCol - 1;
            for (; col >= 0; col--)
            {
                bool hasData = false;
                for (int i = 0; i < table.Count; i++)
                {
                    if (!string.IsNullOrEmpty(table[i][col]))
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
            col++;
            int count = maxCol - col;
            RemoveColumns(col, count);
        }

        public void TrimBottom()
        {
            int row = table.Count - 1;
            for (; row >= 0; row--)
            {
                bool hasData = false;
                for (int i = 0; i < maxCol; i++)
                {
                    if (!string.IsNullOrEmpty(table[row][i]))
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
            row++;
            int count = table.Count - row;
            RemoveRows(row, count);
        }

        public void RemoveColumns(int startIndex, int count)
        {
            if (startIndex + count > maxCol)
            {
                count = maxCol - startIndex;
            }
            for (int i = 0; i < table.Count; i++)
            {
                table[i].RemoveRange(startIndex, count);
            }
            maxCol -= count;
        }

        public void RemoveRows(int startIndex, int count)
        {
            if (startIndex + count > table.Count)
            {
                count = table.Count - startIndex;
            }
            table.RemoveRange(startIndex, count);
        }

        public void InsertRow(int index)
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < maxCol; i++)
            {
                tmp.Add(string.Empty);
            }
            table.Insert(index, new List<string>(tmp.ToArray()));
        }

        public void InsertRows(int index, int count)
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < maxCol; i++)
            {
                tmp.Add(string.Empty);
            }
            for (int i = 0; i < count; i++)
            {
                table.Insert(index, new List<string>(tmp.ToArray()));
            }
        }

        public void AddColumn()
        {
            for (int i = 0; i < table.Count; i++)
            {
                table[i].Add(string.Empty);
            }
            maxCol++;
        }

        public void InsertColumn(int index)
        {
            for (int i = 0; i < table.Count; i++)
            {
                table[i].Insert(index, string.Empty);
            }
            maxCol++;
        }

        public void InsertColumns(int index, int count)
        {
            List<string> tmp = new List<string>();
            for (int i = 0; i < count; i++)
            {
                tmp.Add(string.Empty);
            }

            for (int i = 0; i < table.Count; i++)
            {
                table[i].InsertRange(index, tmp.ToArray());
            }
            maxCol += count;
        }

        #region IRange Members


        public int StartRow
        {
            get { return rBase; }
        }

        public int StartColumn
        {
            get { return cBase; }
        }

        #endregion
    }
}
