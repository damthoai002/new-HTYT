using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;
using UKPI.BlendedReport.DAL;

namespace UKPI.BlendedReport
{
    public class ImportBlend : Importer
    {
        const int DECIMAL_PART = 3;

        #region Private and Protected Properties
        protected IImportBlendDao dao;
        protected BlendedHeaderIndex hIndexes;
        protected List<int> skipRows;
        protected System.Data.DataTable table;
        protected ImportBlendConfig blendConfig;
        protected IErrorLogger logger = null;
        protected int endRow;

        #endregion Private and Protected Properties
        #region Public Properties
        public ExcelVersion Version { get; set; }
        public ImportBlendConfig Config { get { return blendConfig; } }
        public System.Data.DataTable ImportResult { get { return table; } }
        #endregion Public Properties

        #region Constructor
        public ImportBlend(IImportBlendDao importBlendDao, ImportBlendConfig config)
            : base()
        {
            blendConfig = config;
            Version = config.ExcelVersion;
            base.PreProcessWorkbook += new WorkBookHandler(ImportBlend_PreProcessWorkbook);
            base.PreProcessSheet += new WorkSheetHandler(ImportBlend_PreProcessSheet);
            base.PostProcessWorkbook += new WorkBookHandler(ImportBlend_PostProcessWorkbook);
            base.PostProcessSheet += new WorkSheetHandler(ImportBlend_PostProcessSheet);
            dao = importBlendDao;
        }
        #endregion Constructor

        #region Process Events
        ResultState ImportBlend_PostProcessSheet(object sender, ISheet sheet)
        {
            return ResultState.Success;
        }

        ResultState ImportBlend_PostProcessWorkbook(object sender, IWorkbook workbook)
        {
            return ResultState.Success;
        }

        ResultState ImportBlend_PreProcessSheet(object sender, ISheet sheet)
        {
            hIndexes = new BlendedHeaderIndex();
            skipRows = new List<int>();
            table = new System.Data.DataTable();
            endRow = -1;
            return ResultState.Success;
        }

        ResultState ImportBlend_PreProcessWorkbook(object sender, IWorkbook workbook)
        {
            return ResultState.Success;
        }
        #endregion Process Events

        #region Implement Abstract method
        protected override List<int> ImportSheets
        {
            get
            {
                List<int> result = new List<int>();
                result.Add(Config.ImportSheet);
                return result;
            }
        }

        protected override IExcelReader ExcelReader
        {
            get
            {
                return ExcelFactory.CreateExcelReader(Version, Config.UseCOM);
            }
        }

        protected override Dictionary<string, System.Data.DataTable> TableSchemas
        {
            get
            {
                Dictionary<string, System.Data.DataTable> result = new Dictionary<string, System.Data.DataTable>();
                System.Data.DataTable table = dao.TableSchema;
                if (table != null && !string.IsNullOrEmpty(table.TableName))
                {
                    result.Add(table.TableName, table);
                }
                return result;
            }
        }

        protected override ResultState ProcessHeader(ISheet sheet)
        {
            ResultState result = ResultState.Success;
            bool halt = false;
            int hRow = Config.StartRow;
            hIndexes.DistributorID = Config.DistributorID;
            hIndexes.OutletID = Config.OutletID;
            hIndexes.Period = Config.Period;
            for (int i = Config.StartNameColumn; i <= sheet.Cells.EndColumn; i++)
            {
                string text = sheet.Cells[hRow, i].ToUpper().Trim();
                CellIndex pos = new CellIndex(sheet.SheetNumber, hRow, i);
                if (string.IsNullOrEmpty(text))
                {
                    logger.LogError(new ImportError(ErrorType.Blank, pos));
                    halt = true;
                }
                else
                {
                    if (text.Equals(Config.Vpp))
                    {
                        hIndexes.Vpp = i;
                    }
                    else if (text.Equals(Config.DistributorID))
                    {
                        halt = true;
                        logger.LogError(new ImportError(ErrorType.HeaderIndex, pos));
                    }
                    else if (text.Equals(Config.DistributorName))
                    {
                        halt = true;
                        logger.LogError(new ImportError(ErrorType.HeaderIndex, pos));
                    }
                    else if (text.Equals(Config.Lppc))
                    {
                        hIndexes.Lppc = i;
                    }
                    else if (text.Equals(Config.Npd))
                    {
                        hIndexes.Npd = i;
                    }
                    else if (text.Equals(Config.Osa))
                    {
                        hIndexes.Osa = i;
                    }
                    else if (text.Equals(Config.OutletID))
                    {
                        halt = true;
                        logger.LogError(new ImportError(ErrorType.HeaderIndex, pos));
                    }
                    else if (text.Equals(Config.Period))
                    {
                        halt = true;
                        logger.LogError(new ImportError(ErrorType.HeaderIndex, pos));
                    }
                    else if (text.Equals(Config.Pc))
                    {
                        hIndexes.Pc = i;
                    }
                    else if (text.Equals(Config.Promotion))
                    {
                        hIndexes.Promotion = i;
                    }
                    else if (text.Equals(Config.Ps))
                    {
                        hIndexes.Ps = i;
                    }
                    else if (text.Equals(Config.ShelfStandard))
                    {
                        hIndexes.ShelfStandard = i;
                    }
                    else if (text.Equals(Config.ToValue))
                    {
                        hIndexes.ToValue = i;
                    }
                    else
                    {
                        halt = true;
                        logger.LogError(new ImportError(ErrorType.HeaderName, pos));
                    }
                }
            }
            // Stop validating if encounter error
            if (halt) result = ResultState.Halt;
            return result;
        }

        protected override ResultState ProcessDetails(ref Dictionary<string, System.Data.DataTable> schemas, ISheet sheet)
        {
            ResultState result = ResultState.Success;
            bool success = true;
            endRow = GetEndRow(sheet);

            success = success && ValidateDuplicateRows(sheet);

            success = success && ValidatePeriod(sheet);

            success = success && CheckExisted(sheet); ;

            if (success)
            {
                ResultState numeric = ValidateAndAdd(ref schemas, sheet);
                success = success && (numeric == ResultState.Success);
            }
            if (!success) result = ResultState.Halt;
            return result;
        }

        protected override ResultState SaveDataTable(Dictionary<string, System.Data.DataTable> tables)
        {
            table = tables[dao.TableName].Copy();
            return ResultState.Success;
        }
        #endregion Implement Abstract method

        #region Validation Parts
        protected ResultState ValidateAndAdd(ref Dictionary<string, System.Data.DataTable> tables, ISheet sheet)
        {
            ResultState result = ResultState.Success;
            bool success = true;
            string name = dao.TableName;
            int startRow = Config.StartRow + 1;

            for (int i = startRow; i <= endRow; i++)
            {
                System.Data.DataRow row = tables[name].NewRow();
                bool error = false;
                row[Constant.DB_BLEND_DISTRIBUTOR_ID] = sheet.Cells[i, hIndexes.DistributorID];
                row[Constant.DB_BLEND_OUTLET_ID] = sheet.Cells[i, hIndexes.OutletID];
                string mText = sheet.Cells[i, hIndexes.Period];
                DateTime? month = ParseMonth(mText, Config.MonthFormat);
                if (month != null)
                {
                    row[Constant.DB_BLEND_PERIOD_MONTH] = month.Value.Month;
                    row[Constant.DB_BLEND_PERIOD_YEAR] = month.Value.Year;
                }
                else
                {
                    error = true;
                }
                // Check Percentage columns
                Dictionary<int, string> percentages = PercentageColumns;

                foreach (KeyValuePair<int, string> col in percentages)
                {
                    decimal value = 0;
                    ErrorType eT = IsPercentage(sheet.Cells[i, col.Key], out value);
                    if (eT != ErrorType.None)
                    {
                        error = true;
                        success = false;
                        logger.LogError(new ImportError(eT, new CellIndex(sheet.SheetNumber, i, col.Key)));
                    }
                    else
                    {
                        row[col.Value] = Math.Round(value, DECIMAL_PART, MidpointRounding.AwayFromZero).ToString();
                    }
                }
                Dictionary<int, string> volumeColumns = VolumeColumns;
                foreach (KeyValuePair<int, string> col in VolumeColumns)
                {
                    decimal value = 0;
                    ErrorType eT = ParseDecimal(sheet.Cells[i, col.Key], out value);
                    if (eT != ErrorType.None)
                    {
                        error = true;
                        success = false;
                        logger.LogError(new ImportError(eT, new CellIndex(sheet.SheetNumber, i, col.Key)));
                    }
                    else
                    {
                        row[col.Value] = Math.Round(value, DECIMAL_PART, MidpointRounding.AwayFromZero).ToString();
                    }
                }

                if (error)
                {
                    skipRows.Add(i);
                }
                else
                {
                    tables[name].Rows.Add(row);
                }
            }
            if (!success) result = ResultState.FailContinue;
            return result;
        }

        protected Dictionary<int, string> PercentageColumns
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(hIndexes.Npd, Constant.DB_BLEND_NPD);
                result.Add(hIndexes.Osa, Constant.DB_BLEND_OSA);
                result.Add(hIndexes.Pc, Constant.DB_BLEND_PC);
                result.Add(hIndexes.Promotion, Constant.DB_BLEND_PROMOTION);
                result.Add(hIndexes.Ps, Constant.DB_BLEND_PS);
                result.Add(hIndexes.ShelfStandard, Constant.DB_BLEND_SHELF_STANDARD);

                return result;
            }
        }

        protected Dictionary<int, string> VolumeColumns
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(hIndexes.Lppc, Constant.DB_BLEND_LPPC);
                result.Add(hIndexes.ToValue, Constant.DB_BLEND_TO_VALUE);
                result.Add(hIndexes.Vpp, Constant.DB_BLEND_VPP);

                return result;
            }
        }

        protected bool CheckExisted(ISheet sheet)
        {
            bool result = true;
            List<string> outlets = new List<string>();
            List<string> distributors = new List<string>();
            Dictionary<string, List<int>> olGroup = new Dictionary<string, List<int>>();
            Dictionary<string, List<int>> dtGroup = new Dictionary<string, List<int>>();

            for (int i = Config.StartRow + 1; i <= endRow; i++)
            {
                bool ok = true;
                string dt = sheet.Cells[i, hIndexes.DistributorID];
                string ol = sheet.Cells[i, hIndexes.OutletID];
                if (string.IsNullOrEmpty(dt))
                {
                    result = false;
                    logger.LogError(new ImportError(ErrorType.Blank, new CellIndex(sheet.SheetNumber, i, hIndexes.DistributorID)));
                    ok = false;
                }
                else
                {
                    if (!distributors.Contains(dt))
                    {
                        distributors.Add(dt);
                    }
                    if (!dtGroup.ContainsKey(dt))
                    {
                        dtGroup.Add(dt, new List<int>());
                    }
                    dtGroup[dt].Add(i);
                }
                if (string.IsNullOrEmpty(ol))
                {
                    result = false;
                    logger.LogError(new ImportError(ErrorType.Blank, new CellIndex(sheet.SheetNumber, i, hIndexes.DistributorID)));
                    ok = false;
                }
                else
                {
                    if (!outlets.Contains(ol))
                    {
                        outlets.Add(ol);
                    }
                    if (!olGroup.ContainsKey(ol))
                    {
                        olGroup.Add(ol, new List<int>());
                    }
                    olGroup[ol].Add(i);
                }
                if (!ok)
                {
                    skipRows.Add(i);
                }
            }

            List<string> notExistedOL = dao.GetNotExistedOutlet(outlets);
            List<string> notExistedDT = dao.GetNotExistedDistributor(distributors);
            if (notExistedOL == null)
            {
                result = false;
                logger.LogError(new ImportError(ErrorType.Unknown, new Exception("Could not get not existed outlet")));
            }
            else if (notExistedOL.Count > 0)
            {
                foreach (string ol in notExistedOL)
                {
                    if (!olGroup.ContainsKey(ol)) continue;
                    foreach (int r in olGroup[ol])
                    {
                        ExistenceError obj = new ExistenceError();
                        obj.Cell = new CellIndex(sheet.SheetNumber, r, hIndexes.OutletID);
                        obj.Value = ol;
                        result = false;
                        logger.LogError(new ImportError(ErrorType.OLNotExisted, obj));
                    }
                }
            }

            if (notExistedDT == null)
            {
                result = false;
                logger.LogError(new ImportError(ErrorType.Unknown, new Exception("Could not get not existed distributor")));
            }
            else if (notExistedDT.Count > 0)
            {
                foreach (string dt in notExistedDT)
                {
                    if (!dtGroup.ContainsKey(dt)) continue;
                    foreach (int r in dtGroup[dt])
                    {
                        ExistenceError obj = new ExistenceError();
                        obj.Cell = new CellIndex(sheet.SheetNumber, r, hIndexes.OutletID);
                        obj.Value = dt;
                        result = false;
                        logger.LogError(new ImportError(ErrorType.DTNotExisted, obj));
                    }
                }
            }

            return result;
        }

        protected int GetEndRow(ISheet sheet)
        {
            int result = sheet.Cells.EndRow;
            for (int i = Config.StartRow; i <= sheet.Cells.EndRow; i++)
            {
                bool empty = true;
                for (int j = Config.StartColumn; j <= sheet.Cells.EndColumn; j++)
                {
                    if (!string.IsNullOrEmpty(sheet.Cells[i, j].Trim()))
                    {
                        empty = false;
                        break;
                    }
                }
                if (empty)
                {
                    result = i - 1;
                    break;
                }
            }

            return result;
        }

        protected bool ValidateDuplicateRows(ISheet sheet)
        {
            bool result = true;
            List<int> keyIndexes = new List<int>();
            keyIndexes.Add(hIndexes.DistributorID);
            keyIndexes.Add(hIndexes.OutletID);
            keyIndexes.Add(hIndexes.Period);
            List<int[]> dupRows = Utility.GetDuplicateRows(sheet, keyIndexes, Config.StartRow + 1, endRow, new List<int>());
            foreach (int[] dupGroup in dupRows)
            {
                DupplicateError error = new DupplicateError(sheet.SheetNumber);
                error.Rows.AddRange(dupGroup);
                result = false;
                logger.LogError(new ImportError(ErrorType.Duplicate, error));
                skipRows.AddRange(dupGroup.ToArray());
            }

            return result;
        }

        protected bool ValidatePeriod(ISheet sheet)
        {
            bool result = true;
            int pIndex = hIndexes.Period;
            string mFormat = Config.MonthFormat;

            for (int i = Config.StartRow + 1; i <= endRow; i++)
            {
                string text = sheet.Cells[i, pIndex];
                CellIndex pos = new CellIndex(sheet.SheetNumber, i, pIndex);
                if (string.IsNullOrEmpty(text))
                {
                    result = false;
                    logger.LogError(new ImportError(ErrorType.Blank, pos));
                    skipRows.Add(i);
                }
                else
                {
                    DateTime? value = ParseMonth(text, mFormat);
                    if (value == null)
                    {
                        result = false;
                        logger.LogError(new ImportError(ErrorType.MonthFormat, pos));
                        skipRows.Add(i);
                    }
                }
            }

            // Check unique here if needed 

            return result;
        }

        protected DateTime? ParseMonth(string value, string format)
        {
            DateTime tmp;

            if (DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out tmp))
            {
                return tmp;
            }
            return null;
        }

        protected ErrorType IsPercentage(string value, out decimal result)
        {
            ErrorType error = ErrorType.None;

            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    result = 0;
                }
                else
                {
                    result = decimal.Parse(value);
                    if (result < 0 || result > 100)
                    {
                        error = ErrorType.OutOfRange;
                    }
                }
            }
            catch
            {
                result = 0;
                error = ErrorType.Numeric;
            }

            return error;
        }

        protected ErrorType ParseDecimal(string value, out decimal result)
        {
            ErrorType error = ErrorType.None;
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    result = 0;
                }
                else
                {
                    result = 0;
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[+-]?[\\d\\]+[\\.]?[\\d]*");
                    if (regex.IsMatch(value))
                    {
                        int len = value.Replace(".", "").Replace("-", "").Replace("+", "").Length;

                        if (len > Constant.MAX_DECIMAL_LENGTH)
                        {
                            error = ErrorType.OutOfRange;
                        }
                        else
                        {
                            result = decimal.Parse(value);
                            if (result < 0)
                            {
                                error = ErrorType.OutOfRange;
                            }
                        }
                    }
                    else
                    {
                        error = ErrorType.Numeric;
                    }
                }
            }
            catch
            {
                result = 0;
                error = ErrorType.Numeric;
            }
            return error;
        }
        #endregion Validation Parts

        #region Public Methods
        public ResultState SaveToDatabase(System.Data.DataTable table)
        {
            ResultState result = ResultState.Success;
            // Remove existed record
            try
            {
                dao.SaveToDatabase(table);
            }
            catch (Exception ex)
            {
                result = ResultState.Halt;
                logger.LogException(ex);
            }

            return result;
        }
        #endregion Public Methods


        protected override IErrorLogger Logger
        {
            get { return logger; }
        }

        public void SetLooger(IErrorLogger errorLogger)
        {
            logger = errorLogger;
        }
    }

    public struct BlendedHeaderIndex
    {
        public int DistributorID;
        public int OutletID;
        public int Period;
        public int ToValue;
        public int Pc;
        public int Lppc;
        public int Vpp;
        public int Ps;
        public int Osa;
        public int Npd;
        public int ShelfStandard;
        public int Promotion;

    }
}
