
namespace FPT.Component.ExcelPlus
{
    public class SheetView : ISheet
    {
        private IRange cells;
        private int sheetNo;
        private string sheetName;

        public SheetView()
        {
            cells = new DataArray();
        }

        #region ISheetView Members

        public IRange Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        public int SheetNumber
        {
            get { return sheetNo; }
            set { sheetNo = value; }
        }

        public string SheetName
        {
            get { return sheetName; }
            set { sheetName = value; }
        }

        #endregion
    }
}
