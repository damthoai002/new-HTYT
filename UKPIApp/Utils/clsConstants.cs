using System;

namespace UKPI.Utils
{
	/// <summary>
	/// Summary description for clsConstants.
	/// </summary>
	/// <remarks>
	/// Author:			Nguyen Minh Duc. G3.
	/// Created date:	14/05/2006
	/// </remarks>
	public class clsConstants
	{
		public clsConstants()
		{}

		/// <summary>
		/// WEEK = "WEEK"
		/// </summary>
		//public static string WEEK = "WEEK";
		
        ///// <summary>
        ///// Excel template file path
        ///// </summary>
        //public static string EXCEL_PROMOTION_TEMPLATE= "\\Template\\Template For Promotion.xls";

		///<summary>
		///The path containing CO Master file
		///</summary>
		//public const string CO_MASTER_PATH = "DEF_CO_MASTER_PATH";

		///<summary>
		///The path containing CO file for each distributor
		///</summary>
		//public const string CO_RELEASE_PATH = "DEF_CO_RELEASE_PATH";

		///<summary>
		///Date to check CO
		///</summary>
		//public const string DATE_CHECK_CO = "DEF_DATE_CHECK_CO";

		///<summary>
		///Date to check PPO
		///</summary>
		//public const string DATE_CHECK_PPO = "DEF_DATE_CHECK_PPO";

		///<summary>
		///The number of records for each times I saved to text file.
		///</summary>
		//public const string EXPORT_DAILY_PPO_NUMBER = "DEF_EXPORT_DAILY_PPO_NUMBER";

		///<summary>
		///The path containing daily PPO files
		///</summary>
		//public const string PPO_DAILY_PATH = "DEF_PPO_DAILY_PATH";

		///<summary>
		///The folder contains PPO Backup files
		///</summary>
		//public const string PPO_ORIGINAL_BACKUP = "DEF_PPO_ORIGINAL_BACKUP";

		///<summary>
		///The path containing the original PPO file
		///</summary>
		//public const string PPO_ORIGINAL_PATH = "DEF_PPO_ORIGINAL_PATH";

		///<summary>
		///Time to check CO file
		///</summary>
		//public const string TIME_CHECK_CO = "DEF_TIME_CHECK_CO";

		///<summary>
		///Time to check PPO
		///</summary>
		//public const string TIME_CHECK_PPO = "DEF_TIME_CHECK_PPO";


		//public const string SWAP_TYPE_SUPPLY = "S";

		//public const string SWAP_TYPE_RECEIVE = "R";

		//public const int SWAP_CONCURRENT = -1;

		//public const string EXCLUDE_STOCK = "DEF_EXCLUDE_STOCK_PATH";

		//public const string EXCLUDE_RR = "DEF_EXCLUDE_RR_PATH";

		//public const string CASE_PALLET_STDSKU_CODE_TYPE = "H";


		#region Login Constants

		/// <summary>
		/// Expired day
		/// </summary>
		public static string EXPIRED_DAYS = "DEF_EXPIRED_DAYS";

		/// <summary>
		/// Default expired days. DEFAULT_EXPIRED_DAYS = 90
		/// </summary>
		public const int DEFAULT_EXPIRED_DAYS = 90;

		/// <summary>
		/// LOGIN_SUCCESS = 0
		/// </summary>
		public const int LOGIN_SUCCESS = 0;

		/// <summary>
		/// ACCOUNT_NOT_EXIST = -2
		/// </summary>
		public const int ACCOUNT_NOT_EXIST = -2;

		/// <summary>
		/// PASSWORD_WRONG = -3
		/// </summary>
		public const int PASSWORD_WRONG = -3;

		/// <summary>
		/// ACCOUNT_INACTIVE = -4
		/// </summary>
		public const int ACCOUNT_INACTIVE = -4;

		/// <summary>
		/// ACCOUNT_EXPIRED = -5
		/// </summary>
		public const int ACCOUNT_EXPIRED = -5;

		/// <summary>
		/// PASSWORD_EXPIRED = -6
		/// </summary>
		public const int PASSWORD_EXPIRED = -6;

        //public const int PPO_STATUS_UNZIP_FAIL = 2;
        //public const int PPO_STATUS_IMPORT_FAIL = 4;
        //public const int PPO_STATUS_READY_REVISE = 5;

        //public const string PPO_DESCR_UPDATE = "Application updated";
        public const string APP_NAME = "UKPI";
		#endregion Login Constants

		#region Action
		public static string MINUS = "-";
		public const string MENU_WINDOWS = "mnuWindow";
		public static string MENU_SEPARATE = "mnuSeparate";
		public static string MENU_HELP_TOPIC = "mnuHelpTopic";
		public static string MENU_HELP_ABOUT = "mnuHelpAbout";
		public static string EXPORT_STOCK_POLICY = "mnuSPExportSP";
		public static string WINDOW_CASCADE = "mnuWindowCascade";
		public static string WINDOW_TILE_HOZ = "mnuWindowTileHorizontal";
		public static string WINDOW_TILE_VERT = "mnuWindowTileVertical";
		public static string WINDOW_CLOSE_ALL = "mnuCloseAllWindow";
        public static string IMPORT_PPO_MANUALLY = "mnuImportPPO";
		
		/// <summary>
		/// Exit application
		/// </summary>
		public static string LOG_OUT = "mnuLogout";
		/// <summary>
		/// Exit application
		/// </summary>
		public static string EXIT = "mnuExit";

		/// <summary>
		/// Change language to English
		/// </summary>
		public static string ENGLISH = "EN";

		/// <summary>
		/// Change languate to Vietnamese
		/// </summary>
		public static string VIETNAMESE = "VN";

		/// <summary>
		/// Change language to English
		/// </summary>
		public static string SET_ENGLISH_LANGUAGE = "mnuEN";

		/// <summary>
		/// Change languate to Vietnamese
		/// </summary>
		public static string SET_VIETNAMESE_LANGUAGE = "mnuVN";

		/// <summary>
		/// Maximized MDI Children when appear.
		/// </summary>
		public static string MAXIMIZED = "mnuMaximized";

		/// <summary>
		/// Flat System style
		/// </summary>
		public static string SYSTEM_STYLE = "mnuSystemStyle";
		public static string COLOR_FOCUS_CONTROL = "mnuColorFocusControl";

        /// <summary>
        /// Import Store list
        /// </summary>
        public static string IMPORT_STORE = "mnuImportStore";
        /// <summary>
        /// Import Store list by data from DMS
        /// </summary>
        public static string IMPORT_STORE_FROM_DMS = "mnuImportStoreFromDms";
        /// <summary>
        /// Export KPI
        /// </summary>
        public static string EXPORT_KPI = "mnuExportKPI";
        /// <summary>
        /// Import Branch Personnel
        /// </summary>
        public static string IMPORT_BP = "mnuImportBP";
        /// <summary>
        /// Import product
        /// </summary>
        public static string IMPORT_PRODUCT = "mnuImportProduct";
        /// <summary>
        /// Import Distributor
        /// </summary>
        public static string IMPORT_DISTRIBUTOR = "mnuImportDistributor";

        #endregion Action

        #region CHECKLIST_KPI

        //PhongNTT

        public static string EXPORT_CHECKLIST_SHEETNAME_STORES = "Stores";
        public static string EXPORT_CHECKLIST_SHEETNAME_OSA = "OSA";
        public static string EXPORT_CHECKLIST_SHEETNAME_NPD = "NPD";
        public static string EXPORT_CHECKLIST_SHEETNAME_SHELFSTANDARD = "Shelf Standard";
        public static string EXPORT_CHECKLIST_SHEETNAME_PROMOTIONCOMPLIANCE = "Promotion Compliance";
        public static string EXPORT_CHECKLIST_SHEETNAME_SOS = "SOS";
        public static string EXPORT_CHECKLIST_SHEETNAME_STANDARDPRICE = "Standard Price";
        public static string EXPORT_CHECKLIST_SHEETNAME_SOF = "SOF";
        public static string EXPORT_CHECKLIST_SHEETNAME_CTA = "Call To Action";

        public static int CHECKLIST_KPI_TYPE_OSA = 1;
        public static int CHECKLIST_KPI_TYPE_NPD = 2;
        public static int CHECKLIST_KPI_TYPE_SHELFSTANDARD = 3;
        public static int CHECKLIST_KPI_TYPE_PROMOTIONCOMPLIANCE = 4;
        public static int CHECKLIST_KPI_TYPE_SOS = 5;
        public static int CHECKLIST_KPI_TYPE_STANDARDPRICE = 6;
        public static int CHECKLIST_KPI_TYPE_SOF = 7;

        public static string CHECKLIST_KPI_NAME_OSA = "OSA";
        public static string CHECKLIST_KPI_NAME_NPD = "NPD";
        public static string CHECKLIST_KPI_NAME_SHELFSTANDARD = "Shelf Standard";
        public static string CHECKLIST_KPI_NAME_PROMOTIONCOMPLIANCE = "Promotion Compliance";
        public static string CHECKLIST_KPI_NAME_SOS = "SOS";
        public static string CHECKLIST_KPI_NAME_STANDARDPRICE = "Standard Price";
        public static string CHECKLIST_KPI_NAME_SOF = "SOF";

        #endregion CHECKLIST_KPI

        public static string SP_STORE_REPORT_GET_CHANNEL = "p_FPT_ENV_GetChannel";
        public static string SP_STORE_REPORT_GET_REGION_1 = "p_FPT_ENV_GetRegionByChannel";
        public static string SP_STORE_REPORT_GET_PROVINCE_2 = "p_FPT_ENV_GetProvinceByRegion";
        public static string SP_STORE_SEARCH_9 = "p_FPT_ENV_SEARCH_STORE";
        public static string SP_BLENDED_SEARCH_2 = "p_FPT_ENV_SEARCH_BLENDED";
        public static string SP_BLENDED_EXPORT_3 = "p_FPT_ENV_EXPORT_BLENDED";
        public static string Value_StoreID = "STORE ID";
        public static string Value_StoreName = "STORE NAME";
        public static string Value_RegionNorth = "NORTH + CENTRAL";
        public static string Value_RegionSouth = "HCM&E + MKD";
        public static string Value_DefaultDist = "(PRESS F3 TO SEARCH)";
        public static string Value_MT = "B14";
        
    }
    public enum DataCombo
    {
        CHANNEL,
        REGION,
        PROVINCE,
        TOWN
    }
}
