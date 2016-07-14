using System;
using System.Collections.Generic;

namespace FPT.Component.ExcelPlus
{
    public class KeyComparer : IComparable<KeyComparer>, IEquatable<KeyComparer>
    {
        protected int index;
        protected int sheetNo;
        protected List<string> value;

        public KeyComparer()
        {
            index = -1;
            sheetNo = -1;
            value = new List<string>();
        }

        public void SetData(ISheet sheet, int row, List<int> indexes)
        {
            if (sheet != null)
            {
                index = row;
                sheetNo = sheet.SheetNumber;
                value = new List<string>();
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (sheet.Cells[row, indexes[i]] != null)
                    {
                        value.Add(sheet.Cells[row, indexes[i]].Trim().ToUpper());
                    }
                    else
                    {
                        value.Add(string.Empty);
                    }
                }
            }
        }

        public KeyComparer(int kIndex, List<string> keys)
            : this()
        {
            value.AddRange(keys.ToArray());
            this.index = kIndex;
        }

        public KeyComparer(int kIndex, string[] keys)
            : this()
        {
            value.AddRange(keys);
            this.index = kIndex;
        }

        public KeyComparer(ISheet sheet, int row, List<int> indexes)
            : this()
        {
            SetData(sheet, row, indexes);
        }

        public int Sheet { get { return sheetNo; } }

        public int Index { get { return index; } }

        public List<string> Key { get { return value; } }

        #region IComparable<KeyComparer> Members

        public int CompareTo(KeyComparer other)
        {
            int result = 0;
            for (int i = 0; i < value.Count; i++)
            {
                result = value[i].CompareTo(other.value[i]);
                if (result != 0)
                {
                    break;
                }
            }
            return result;
        }

        #endregion

        #region IEquatable<KeyComparer> Members

        public bool Equals(KeyComparer other)
        {
            bool result = true;
            for (int i = 0; i < value.Count; i++)
            {

                if (!value[i].Equals(other.value[i]))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        #endregion
    }

    public class KeyComparerEquality : IEqualityComparer<KeyComparer>
    {
        #region IEqualityComparer<KeyComparer> Members

        public bool Equals(KeyComparer x, KeyComparer y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(KeyComparer obj)
        {
            string tmp = string.Empty;
            for (int i = 0; i < obj.Key.Count; i++)
            {
                tmp += obj.Key[i];
            }
            return tmp.GetHashCode();
        }

        #endregion
    }
}
