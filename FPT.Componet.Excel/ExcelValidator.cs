using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// This class is use for validating Excel data with 1 data table per sheet only (can contain other cell properties).
    /// </summary>
    public abstract class ExcelValidator1T
    {
        public const string EXCEL2003_EXTENSION = ".XLS";

        #region Events
        public event EventHandler SheetOutOfRange;
        public event SheetNameHandler WrongSheetName;
        public event EventHandler NotEnoughSheet;


        protected void OnSheetOutOfRange()
        {
            if (SheetOutOfRange != null)
                SheetOutOfRange.Invoke(this, new EventArgs());
        }

        protected void OnNotEnoughSheet()
        {
            if (NotEnoughSheet != null)
                NotEnoughSheet.Invoke(this, new EventArgs());
        }

        protected void OnWrongSheetName(string sheetName)
        {
            if (WrongSheetName != null)
                WrongSheetName.Invoke(this, sheetName);
        }
        #endregion Events

        #region Constructors
        public ExcelValidator1T(string templatePath)
        {
            writer2k3 = null;
            writer2k7 = null;
            importSheetCollection = new List<ImportSheet>();
            template = ImportTemplate.Load(templatePath);
        }

        #endregion Constructors

        #region Public Methods
        /// <summary>
        /// Validate excel data based on import template (XML data)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public virtual bool Validate(string filePath)
        {
            bool result = true;
            currentFile = filePath;
            IExcelReader reader = ExcelReader;
            IWorkbook workbook = reader.ReadWorkbook(filePath);
            
            importSheetCollection = new List<ImportSheet>();

            if (template.UseSheetIndex)
            {
                int maxIndex = template.GetMaxSheetIndex();
                if (maxIndex > workbook.WorkSheets.Count() - 1)
                {
                    OnSheetOutOfRange();
                }
                foreach (SheetTemplate sh in template.SheetCollection)
                {
                    if (workbook.WorkSheets.Count() > sh.Index)
                    {
                        ISheet sheet = workbook.WorkSheets[sh.Index];
                        ImportSheet data = ImportSheet.Load(sheet, sh);
                        importSheetCollection.Add(data);
                    }
                }
            }
            else
            {
                foreach (SheetTemplate sh in template.SheetCollection)
                {
                    IEnumerable<ISheet> sheets = workbook.WorkSheets[sh.Name];
                    if (sheets != null && sheets.Count() > 0)
                    {
                        ISheet sheet = sheets.ElementAt(0);
                        ImportSheet data = ImportSheet.Load(sheet, sh);
                        importSheetCollection.Add(data);
                    }
                    else
                    {
                        OnWrongSheetName(sh.Name);
                    }
                }
            }
            if (importSheetCollection.Count < template.SheetCollection.Count)
                OnNotEnoughSheet();

            if (importSheetCollection.Count == 0)
            {
                throw new Exception("All sheet names are not correct");
            }

            if (!PreValidate())
                return false;
            foreach (ImportSheet importSheet in importSheetCollection)
            {
                if (!ValidateSheet(importSheet))
                    result = false;
            }
            if (!ValidateRelation())
                result = false;

            if (!PostValidate())
                return false;

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
        protected List<ImportSheet> importSheetCollection;
        protected ImportTemplate template;

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

        /// <summary>
        /// Pre-validate for all data. If pre-validation failed, importing process will be halt.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        protected virtual bool PreValidate()
        {
            return true;
        }

        /// <summary>
        /// This will apply after all validation completed
        /// </summary>
        /// <returns></returns>
        protected virtual bool PostValidate()
        {
            return true;
        }

        /// <summary>
        /// Validate relation among multiple sheets.
        /// This will be applied after validating all sheets separately
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidateRelation()
        {
            return true;
        }

        /// <summary>
        /// Validate each sheet separately
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        protected virtual bool ValidateSheet(ImportSheet sheet)
        {
            return true;
        }
        #endregion Protected Properties and methods
    }

    public class ValidationResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorColumn { get; set; }
    }
}
