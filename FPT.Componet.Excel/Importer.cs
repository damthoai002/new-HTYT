using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public abstract class Importer
    {
        public const string EXCEL2003_EXTENSION = ".XLS";

        #region Abstract Methods
        protected abstract List<int> ImportSheets { get; }
        protected abstract IErrorLogger Logger { get; }
        protected abstract Dictionary<string, System.Data.DataTable> TableSchemas { get; }
        protected abstract ResultState ProcessHeader(ISheet sheet);
        protected abstract ResultState ProcessDetails(ref Dictionary<string, System.Data.DataTable> schemas, ISheet sheet);
        protected abstract ResultState SaveDataTable(Dictionary<string, System.Data.DataTable> tables);
        #endregion Abstract Methods

        #region Events
        public event WorkBookHandler PreProcessWorkbook;
        public event WorkBookHandler PostProcessWorkbook;
        public event WorkSheetHandler PreProcessSheet;
        public event WorkSheetHandler PostProcessSheet;

        protected virtual ResultState OnPreProcessWorkBook(IWorkbook workbook)
        {
            if (PreProcessWorkbook != null)
            {
                return this.PreProcessWorkbook.Invoke(this, workbook);
            }
            return ResultState.Success;
        }

        protected virtual ResultState OnPostProcessWorkbook(IWorkbook workbook)
        {
            if (PostProcessWorkbook != null)
            {
                return this.PostProcessWorkbook.Invoke(this, workbook);
            }
            return ResultState.Success;
        }

        protected virtual ResultState OnPreProcessSheet(ISheet sheet)
        {
            if (PreProcessSheet != null)
            {
                return this.PreProcessSheet.Invoke(this, sheet);
            }
            return ResultState.Success;
        }

        protected virtual ResultState OnPostProcessSheet(ISheet sheet)
        {
            if (PostProcessSheet != null)
            {
                return this.PostProcessSheet.Invoke(this, sheet);
            }
            return ResultState.Success;
        }
        #endregion Events

        #region Constructors
        public Importer()
        {
            writer2k3 = null;
            writer2k7 = null;
        }

        #endregion Constructors

        #region Public Methods
        public virtual bool Import(string filePath)
        {
            bool result = true;
            currentFile = filePath;
            List<int> importSheets = ImportSheets;
            IExcelReader reader = ExcelReader;

            // Please contact owner of function ImportStores of PaymentTool, 
            // if you wanna modify the line code below
            Dictionary<string, System.Data.DataTable> tables = null;

            IWorkbook workbook = reader.ReadWorkbook(filePath, importSheets);

            ResultState preWb = OnPreProcessWorkBook(workbook);
            if (preWb == ResultState.Halt)
            {
                return false;
            }
            else if (preWb != ResultState.Success)
            {
                result = false;
            }

            foreach (ISheet sheet in workbook.WorkSheets)
            {
                ResultState preWS = OnPreProcessSheet(sheet);
                if (preWS == ResultState.Halt)
                {
                    return false;
                }
                else if (preWS != ResultState.Success)
                {
                    result = false;
                }

                ResultState hdr = ProcessHeader(sheet);
                if (hdr == ResultState.Halt)
                {
                    return false;
                }
                else if (hdr != ResultState.Success)
                {
                    result = false;
                }

                // Please contact owner of function ImportStores of PaymentTool, 
                // if you wanna modify the line code below
                tables = TableSchemas;

                ResultState details = ProcessDetails(ref tables, sheet);
                if (details == ResultState.Halt)
                {
                    return false;
                }
                else if (details != ResultState.Success)
                {
                    result = false;
                }
                ResultState postWS = OnPostProcessSheet(sheet);
                if (postWS == ResultState.Halt)
                {
                    return false;
                }
                else if (postWS != ResultState.Success)
                {
                    result = false;
                }
            }
            ResultState postWB = OnPostProcessWorkbook(workbook);
            if (postWB == ResultState.Halt)
            {
                return false;
            }
            else if (postWB != ResultState.Success)
            {
                result = false;
            }
            ResultState save = SaveDataTable(tables);
            if (save != ResultState.Success)
            {
                result = false;
            }

            return result;
        }

        public virtual ExcelVersion GetVersion(string filePath)
        {
            string ext = System.IO.Path.GetExtension(filePath).ToUpper();
            if (ext.Equals(EXCEL2003_EXTENSION))
            {
                return ExcelVersion.Excel2003;
            }
            return ExcelVersion.Excel2007;
        }
        #endregion Public Methods

        #region Protected Properties and methods
        protected IExcelReader writer2k3;
        protected IExcelReader writer2k7;
        protected string currentFile;

        protected virtual IExcelReader ExcelReader
        {
            get
            {
                ExcelVersion version = GetVersion(currentFile);
                if (version == ExcelVersion.Excel2003)
                {
                    if (writer2k3 == null)
                    {
                        writer2k3 = ExcelFactory.CreateExcelReader(ExcelVersion.Excel2003, false);
                    }
                    return writer2k3;
                }
                else
                {
                    if (writer2k7 == null)
                    {
                        writer2k7 = ExcelFactory.CreateExcelReader(ExcelVersion.Excel2007, false);
                    }
                    return writer2k7;
                }
            }
        }
        #endregion Protected Properties and methods
    }
}
