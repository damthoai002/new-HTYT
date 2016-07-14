
namespace FPT.Component.ExcelPlus
{
    public class WorkBook : IWorkbook
    {
        private WorkSheets sheets;

        public WorkBook()
        {
            sheets = new WorkSheets();
            Name = string.Empty;
        }

        #region IWorkbook Members

        public IWorkSheets WorkSheets
        {
            get { return sheets; }
        }

        public string Name { get; set; }
        #endregion
    }
}
