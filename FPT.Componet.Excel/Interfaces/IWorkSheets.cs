using System.Collections.Generic;

namespace FPT.Component.ExcelPlus
{
    public interface IWorkSheets: IEnumerable<ISheet>
    {
        ISheet this[int sheetNo] { get; set; }
        IEnumerable<ISheet> this[string sheetName] { get; }

        void Add(ISheet sheet);
        void AddRange(IEnumerable<ISheet> sheetCollection);
        void Remove(int sheetNo);
        void Remove(ISheet sheet);
        int Count();
    }
}
