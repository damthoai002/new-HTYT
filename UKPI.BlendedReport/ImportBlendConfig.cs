using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;

namespace UKPI.BlendedReport
{
    public class ImportBlendConfig
    {
        #region Constant
        const string CFG_CONFIG_PART = "ImportBlend";

        const string CFG_IMPORT_SHEET = "ImportSheet";
        const string CFG_EXCEL_VERSION = "ExcelVersion";

        const string CFG_DISTRIBUTORID = "DistributorID";
        const string CFG_DISTRIBUTORNAME = "DistributorName";
        const string CFG_OUTLETID = "OutletID";
        const string CFG_PERIOD = "Period";
        const string CFG_TO_VALUE = "TOValue";
        const string CFG_PC = "PC";
        const string CFG_LPPC = "LPPC";
        const string CFG_PS = "PS";
        const string CFG_OSA = "OSA";
        const string CFG_NPD = "NPD";
        const string CFG_SHELF_STANDARD = "ShelfStandard";
        const string CFG_PROMOTION = "Promotion";
        const string CFG_VPP = "VPP";

        const string CFG_MONTHFORMAT = "MonthFormat";

        const string CFG_START_ROW = "StartRow";
        const string CFG_START_COLUMN = "StartColumn";
        const string CFG_USE_COM = "UseMSExcel";
        #endregion Constant

        public int ImportSheet { get; set; }
        public ExcelVersion ExcelVersion { get; set; }

        #region Header text or relative index for import data. All Text will be changed to upper case
        public int DistributorID { get; set; }
        public int DistributorName { get; set; }
        public int OutletID { get; set; }
        public int Period { get; set; }
        public string ToValue { get; set; }
        public string Pc { get; set; }
        public string Lppc { get; set; }
        public string Ps { get; set; }
        public string Osa { get; set; }
        public string Npd { get; set; }
        public string ShelfStandard { get; set; }
        public string Promotion { get; set; }
        public string Vpp { get; set; }
        public bool UseCOM { get; set; }

        public int StartNameColumn
        {
            get
            {
                return Period + 1;
            }
        }
        #endregion Header text or relative index for import data

        public string MonthFormat { get; set; }

        #region Setting for sheet
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        #endregion Setting for sheet

        public ImportBlendConfig()
        {
        }

        public ImportBlendConfig(Config configuration)
        {
            ImportSheet = ParseInt(configuration[CFG_IMPORT_SHEET]);
            try
            {
                ExcelVersion = (ExcelVersion)Enum.Parse(typeof(ExcelVersion), configuration[CFG_EXCEL_VERSION]);
            }
            catch
            {
                ExcelVersion = ExcelVersion.Excel2007;
            }
            DistributorID = ParseInt(configuration[CFG_DISTRIBUTORID]);
            DistributorName = ParseInt(configuration[CFG_DISTRIBUTORNAME]);
            OutletID = ParseInt(configuration[CFG_OUTLETID]);
            Period = ParseInt(configuration[CFG_PERIOD]);
            ToValue = configuration[CFG_TO_VALUE].ToUpper().Trim();
            Pc = configuration[CFG_PC].ToUpper().Trim();
            Lppc = configuration[CFG_LPPC].ToUpper().Trim();
            Ps = configuration[CFG_PS].ToUpper().Trim();
            Osa = configuration[CFG_OSA].ToUpper().Trim();
            Npd = configuration[CFG_NPD].ToUpper().Trim();
            ShelfStandard = configuration[CFG_SHELF_STANDARD].ToUpper().Trim();
            Promotion = configuration[CFG_PROMOTION].ToUpper().Trim();
            Vpp = configuration[CFG_VPP].ToUpper().Trim();

            MonthFormat = configuration[CFG_MONTHFORMAT];
            StartRow = ParseInt(configuration[CFG_START_ROW]);
            StartColumn = ParseInt(configuration[CFG_START_COLUMN]);
            UseCOM = ParseBool(configuration[CFG_USE_COM].ToLower().Trim());
        }

        private int ParseInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return 0;
            }
        }

        private bool ParseBool(string value)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return false;
            }
        }
    }


}
