using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.BlendedReport
{
    public class Constant
    {
        #region Blend Constant
        public const string DB_BLEND_DISTRIBUTOR_ID = "DISTRIBUTOR_ID";
        public const string DB_BLEND_OUTLET_ID = "OUTLET_ID";
        public const string DB_BLEND_PERIOD_MONTH = "MONTH";
        public const string DB_BLEND_PERIOD_YEAR = "YEAR";
        public const string DB_BLEND_PC = "PC";
        public const string DB_BLEND_LPPC = "LPPC";
        public const string DB_BLEND_PS = "PS";
        public const string DB_BLEND_OSA = "OSA";
        public const string DB_BLEND_NPD = "NPD";
        public const string DB_BLEND_SHELF_STANDARD = "SHELF_STANDARD";
        public const string DB_BLEND_PROMOTION = "PROMOTION";
        public const string DB_BLEND_VPP = "VPP";
        public const string DB_BLEND_TO_VALUE = "TO_VALUE";
        public const string DB_BLEND_TABLE_NAME = "FPT_ENV_BLENDED";
        #endregion Blend Constant

        #region Export Blend Constant
        public const string DB_SP_EXP_DISTRIBUTOR = "DISTRIBUTOR_NAME";
        public const string DB_SP_EXP_REGION = "REGION_NAME";
        public const string DB_SP_EXP_OUTLET_ID = "STORE_CODE";
        public const string DB_SP_EXP_OUTLET_NAME = "STORE_NAME";
        public const string DB_SP_EXP_SUPERVISOR = "SALE_SUP_NAME";
        public const string DB_SP_EXP_MONTH = "MONTH";
        public const string DB_SP_EXP_YEAR = "YEAR";
        public const string DB_SP_EXP_PC = "PC";
        public const string DB_SP_EXP_LPPC = "LPPC";
        public const string DB_SP_EXP_PS = "PS";
        public const string DB_SP_EXP_OSA = "OSA";
        public const string DB_SP_EXP_NPD = "NPD";
        public const string DB_SP_EXP_SHELF_STANDARD = "SHELF_STANDARD";
        public const string DB_SP_EXP_PROMOTION = "PROMOTION";
        public const string DB_SP_EXP_VPP = "VPP";
        public const string DB_SP_EXP_TO_VALUE = "TO_VALUE";
        #endregion Export Blend Constant

        public const int MONTH_PER_YEAR = 12;
        public const int MAX_DECIMAL_LENGTH = 25;

    }
}
