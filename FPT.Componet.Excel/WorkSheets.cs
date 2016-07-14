using System.Collections.Generic;
using System.Linq;

namespace FPT.Component.ExcelPlus
{
    public class WorkSheets : IWorkSheets
    {
        IList<ISheet> sheets;
        Dictionary<int, int> sheetIndexes;

        public WorkSheets()
        {
            sheets = new List<ISheet>();
            sheetIndexes = new Dictionary<int, int>();
        }

        #region IWorkSheets Members

        public ISheet this[int sheetNo]
        {
            get
            {
                if (sheetIndexes.ContainsKey(sheetNo))
                {
                    return sheets[sheetIndexes[sheetNo]];
                }
                return null;
            }
            set
            {
                if (sheetIndexes.ContainsKey(sheetNo))
                {
                    sheets[sheetIndexes[sheetNo]] = value;
                }                
            }
        }

        public void Add(ISheet sheet)
        {
            int count = sheets.Count;
            if (!sheetIndexes.ContainsKey(sheet.SheetNumber))
            {
                sheets.Add(sheet);
                sheetIndexes.Add(sheet.SheetNumber, count);
            }
            else
            {
                // Throw exception here
            }
        }

        public IEnumerable<ISheet> this[string sheetName]
        {
            get { return sheets.Where(x=>x.SheetName.ToUpper().Equals(sheetName.ToUpper())); }
        }

        public void Remove(int sheetNo)
        {
            if (sheetIndexes.ContainsKey(sheetNo))
            {
                int key = sheetIndexes[sheetNo];
                sheets.RemoveAt(key);
                sheetIndexes.Remove(sheetNo);
                RebuildIndex(key);
            }
        }

        public void Remove(ISheet sheet)
        {
            Remove(sheet.SheetNumber);
        }

        public int Count()
        {
            return sheets.Count;
        }

        #endregion

        private void RebuildIndex(int key)
        {
            foreach (int p in sheetIndexes.Keys)
            {
                if (sheetIndexes[p] > key)
                {
                    sheetIndexes[p] = sheetIndexes[p] - 1;
                }
            }
        }

        #region IEnumerable<ISheetView> Members

        public IEnumerator<ISheet> GetEnumerator()
        {
            return sheets.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return sheets.GetEnumerator();
        }

        #endregion

        #region IWorkSheets Members


        public void AddRange(IEnumerable<ISheet> sheetCollection)
        {
            foreach (ISheet sheet in sheetCollection)
            {
                Add(sheet);
            }
        }

        #endregion
    }
}
