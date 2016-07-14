using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;

namespace UKPI.BlendedReport
{
    public class ExportBlendConfig
    {
        #region Constants
        public const string CFG_TOCOUNT = "ToCount";
        public const string CFG_PCCOUNT = "PcCount";
        public const string CFG_LPPCCOUNT = "LppcCount";
        public const string CFG_VPPCOUNT = "VppCount";
        public const string CFG_PSCOUNT = "PsCount";
        public const string CFG_OSACOUNT = "OsaCount";
        public const string CFG_NPDCOUNT = "NpdCount";
        public const string CFG_SHELFSTDCOUNT = "ShelfStdCount";
        public const string CFG_PROMOTIONCOUNT = "PromotionCount";
        public const string CFG_TITLETEXT = "TitleText";
        public const string CFG_UPDATETEXT = "UpdateText";
        public const string CFG_REPORTPERIODTEXT = "ReportPeriodText";
        public const string CFG_TITLEROW = "TitleRow";
        public const string CFG_TITLECOLUMN = "TitleColumn";
        public const string CFG_UPDATEROW = "UpdateRow";
        public const string CFG_UPDTECOLUMN = "UpdateColumn";
        public const string CFG_RPERIODLABELROW = "RPeriodLabelRow";
        public const string CFG_RPERIODLABELCOLUMN = "RPeriodLabelColumn";
        public const string CFG_RPERIODROW = "RPeriodRow";
        public const string CFG_RPERIODCOLUMN = "RPeriodColumn";
        public const string CFG_SUBTITLE = "SubTitle";
        public const string CFG_SUBTITLEROW = "SubTitleRow";
        public const string CFG_SUBTITLECOLUMN = "SubTitleColumn";
        public const string CFG_STARTROW = "StartRow";
        public const string CFG_STARTCOLUMN = "StartColumn";
        public const string CFG_REGIONTEXT = "RegionText";
        public const string CFG_REGIONCOLUMN = "RegionColumn";
        public const string CFG_DISTRIBUTORTEXT = "DistributorText";
        public const string CFG_DISTRIBUTORCOLUMN = "DistributorColumn";
        public const string CFG_SUPTEXT = "SupText";
        public const string CFG_SUPCOLUMN = "SupColumn";
        public const string CFG_OUTLETTEXT = "OutletText";
        public const string CFG_OUTLETCOLUMN = "OutletColumn";
        public const string CFG_OUTLETIDTEXT = "OutletIDText";
        public const string CFG_OUTLETIDCOLUMN = "OutletIDColumn";
        public const string CFG_TOTEXT = "ToText";
        public const string CFG_PCTEXT = "PcText";
        public const string CFG_LPPCTEXT = "LppcText";
        public const string CFG_VPPTEXT = "VppText";
        public const string CFG_PSTEXT = "PsText";
        public const string CFG_OSATEXT = "OsaText";
        public const string CFG_NPDTEXT = "NpdText";
        public const string CFG_SHELFSTDTEXT = "ShelfStdText";
        public const string CFG_PROMOTIONTEXT = "PromotionText";
        public const string CFG_MONTHFORMAT = "MonthFormat";
        public const string CFG_RPMONTHFORMAT = "RPMonthFormat";
        public const string CFG_SHEETNAME = "SheetName";
        public const string CFG_NUMBERFORMAT = "NumberFormat";

        public const string CFG_LATESTDATAROW = "LatestDataRow";
        public const string CFG_LATESTDATACOLUMN= "LatestDataColumn";
        #endregion Constants

        #region Properties
        public FCell Title { get; set; }
        public FCell Update { get; set; }
        public FCell RPLabel { get; set; }
        public FCell RPeriod { get; set; }
        public FCell SubTitle { get; set; }

        public int ToCount { get; set; }
        public int PcCount { get; set; }
        public int LppcCount { get; set; }
        public int VppCount { get; set; }
        public int PsCount { get; set; }
        public int OsaCount { get; set; }
        public int NpdCount { get; set; }
        public int ShelfStdCount { get; set; }
        public int PromotionCount { get; set; }

        public string RegionText { get; set; }
        public int RegionColumn { get; set; }
        public string DistributorText { get; set; }
        public int DistributorColumn { get; set; }
        public string SupText { get; set; }
        public int SupColumn { get; set; }
        public string OutletText { get; set; }
        public int OutletColumn { get; set; }
        public string OutletIDText { get; set; }
        public int OutletIDColumn { get; set; }

        public string ToText { get; set; }
        public string PcText { get; set; }
        public string LppcText { get; set; }
        public string VppText { get; set; }
        public string PsText { get; set; }
        public string OsaText { get; set; }
        public string NpdText { get; set; }
        public string ShelfStdText { get; set; }
        public string PromotionText { get; set; }

        public int StartRow { get; set; }
        public int StartColumn { get; set; }

        public string MonthFormat { get; set; }
        public string RPMonthFormat { get; set; }
        public string SheetName { get; set; }

        public FCell LatestData { get; set; }
        public string NumberFormat { get; set; }
        #endregion Properties

        public ExportBlendConfig()
        {
        }

        public ExportBlendConfig(Config config)
        {
            this.Title = new FCell();
            this.Update = new FCell();
            RPLabel = new FCell();
            RPeriod = new FCell();
            SubTitle = new FCell();
            LatestData = new FCell();

            this.ToCount = ParseInt(config[CFG_TOCOUNT]);
            this.PcCount = ParseInt(config[CFG_PCCOUNT]);
            this.LppcCount = ParseInt(config[CFG_LPPCCOUNT]);
            this.VppCount = ParseInt(config[CFG_VPPCOUNT]);
            this.PsCount = ParseInt(config[CFG_PSCOUNT]);
            this.OsaCount = ParseInt(config[CFG_OSACOUNT]);
            this.NpdCount = ParseInt(config[CFG_NPDCOUNT]);
            this.ShelfStdCount = ParseInt(config[CFG_SHELFSTDCOUNT]);
            this.PromotionCount = ParseInt(config[CFG_PROMOTIONCOUNT]);
            this.Title.Value = config[CFG_TITLETEXT];
            this.Update.Value = config[CFG_UPDATETEXT];
            this.RPLabel.Value = config[CFG_REPORTPERIODTEXT];
            this.Title.Row = ParseInt(config[CFG_TITLEROW]);
            this.Title.Column = ParseInt(config[CFG_TITLECOLUMN]);
            this.Update.Row = ParseInt(config[CFG_UPDATEROW]);
            this.Update.Column = ParseInt(config[CFG_UPDTECOLUMN]);
            this.RPLabel.Row = ParseInt(config[CFG_RPERIODLABELROW]);
            this.RPLabel.Column = ParseInt(config[CFG_RPERIODLABELCOLUMN]);
            this.RPeriod.Row = ParseInt(config[CFG_RPERIODROW]);
            this.RPeriod.Column = ParseInt(config[CFG_RPERIODCOLUMN]);
            this.SubTitle.Value = config[CFG_SUBTITLE];
            this.SubTitle.Row = ParseInt(config[CFG_SUBTITLEROW]);
            this.SubTitle.Column = ParseInt(config[CFG_SUBTITLECOLUMN]);
            this.StartRow = ParseInt(config[CFG_STARTROW]);
            this.StartColumn = ParseInt(config[CFG_STARTCOLUMN]);
            this.RegionText = config[CFG_REGIONTEXT];
            this.RegionColumn = ParseInt(config[CFG_REGIONCOLUMN]);
            this.DistributorText = config[CFG_DISTRIBUTORTEXT];
            this.DistributorColumn = ParseInt(config[CFG_DISTRIBUTORCOLUMN]);
            this.SupText = config[CFG_SUPTEXT];
            this.SupColumn = ParseInt(config[CFG_SUPCOLUMN]);
            this.OutletText = config[CFG_OUTLETTEXT];
            this.OutletColumn = ParseInt(config[CFG_OUTLETCOLUMN]);
            this.OutletIDText = config[CFG_OUTLETIDTEXT];
            this.OutletIDColumn = ParseInt(config[CFG_OUTLETIDCOLUMN]);
            this.ToText = config[CFG_TOTEXT];
            this.PcText = config[CFG_PCTEXT];
            this.LppcText = config[CFG_LPPCTEXT];
            this.VppText = config[CFG_VPPTEXT];
            this.PsText = config[CFG_PSTEXT];
            this.OsaText = config[CFG_OSATEXT];
            this.NpdText = config[CFG_NPDTEXT];
            this.ShelfStdText = config[CFG_SHELFSTDTEXT];
            this.PromotionText = config[CFG_PROMOTIONTEXT];
            this.MonthFormat = FormatReplace(config[CFG_MONTHFORMAT]);
            this.RPMonthFormat = FormatReplace(config[CFG_RPMONTHFORMAT]);
            this.SheetName = config[CFG_SHEETNAME];
            this.LatestData.Row = ParseInt(config[CFG_LATESTDATAROW]);
            this.LatestData.Column = ParseInt(config[CFG_LATESTDATACOLUMN]);
            this.LatestData.Value = string.Empty;
            NumberFormat = config[CFG_NUMBERFORMAT];
        }

        private int ParseInt(string value)
        {
            int result = 0;
            try
            {
                result = int.Parse(value);
            }
            catch { }
            return result;
        }

        protected string FormatReplace(string value)
        {
            value = value.Replace("'","\\\'");
            value = value.Replace("\"", "\\\"");
            return value;
        }
    }
}
