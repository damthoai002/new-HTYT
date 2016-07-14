using System;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Windows.Forms;

using UKPI.Utils;
using UKPI.DataAccessObject;
using UKPI.ValueObject;

using System.Runtime.InteropServices; // For COMException
using System.Diagnostics; // to ensure EXCEL process is really killed
using System.ComponentModel;

namespace UKPI.BusinessObject
{
	/// <summary>
	/// Summary description for BaseBO.
	/// </summary>
	public class clsBaseBO
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(clsBaseBO));

		protected const char DEF_SPACE = ' ';
		protected const char DEF_ZERO = '0';

		//Use to import/export excel
		public static string []COL_NAME = {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
										   "AA","AB","AC","AD","AE","AF","AG","AH","AI","AJ","AK","AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV","AW","AX","AY","AZ",
										   "BA","BB","BC","BD","BE","BF","BG","BH","BI","BJ","BK","BL","BM","BN"};
		public static object missing = Missing.Value;
		public static int EXCEL_COL_SPACE = 2;

		public static bool IS_EXPORT_DAILY_PPO = false;

		private clsBaseDAO dao = new clsBaseDAO();


	
		public clsBaseBO()
		{
		}

		/// <summary>
		/// Get current week
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public int GetCurrentWeek()
		{
            //Thanhnq fix bug: 20090218: get curent week from database
			//I will get current year from database by shipping calendar.
            //int week = (DateTime.Now.DayOfYear / 7) + 1;

            //if(week > 52)
            //    week = 52;

			//return week;
            return clsCommon.GetCurrentULVWeek();

		}
		public string WEEK
		{
			get{return GetWeek();}
		}

		/// <summary>
		/// Get current year
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public int GetCurrentYear()
		{
			//Thanhnq fix bug: 20090218: get curent year from database
            //return DateTime.Now.Year;
            return clsCommon.GetCurrentULVYear();
		}
        /*=======12-Mar-2009: Dung ham chung trong clsBase==============
		/// <summary>
		/// Get all regions by logined user
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public DataTable GetRegions()
		{
			clsCommon common = new clsCommon();
			string strSql = string.Format("SELECT A.REGION_CODE, REGION_NAME FROM (SELECT REGION_CODE FROM FPT_ENV_AUT_USER_REGION WHERE USERNAME = '{0}') AS A LEFT JOIN FPT_ENV_REGION_HIERARCHY B ON A.REGION_CODE = B.REGION_CODE ORDER BY B.PRIORITY", common.EncodeString(clsSystemConfig.UserName));
			DataTable dt = dao.GetDataTable(strSql);
			DataRow row = dt.NewRow();
			row[0] = string.Empty;
			row[1] = string.Empty;
			dt.Rows.InsertAt(row, 0);
			return dt;
		}
        =======================*/

		/// <summary>
		/// Get one product by P_ID
		/// </summary>
		/// <param name="pid"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public DataRow GetProduct(string pid)
		{
			SqlCommand cmd = new SqlCommand("SELECT P_ID, P_DESC, PCS_PER_CASE, NET_WEIGHT, PRICE, DIVISION_ID, CAT_ID, SUB_CAT_ID, BRAND_ID, STATUS, BRANDY_ID, BRANDY_NAME, VARIANT_ID, PACKSIZE, STDSKU, STDSKU_NAME FROM FPT_ENV_PRODUCT WHERE P_ID = @P_ID");
			cmd.Parameters.Add("@P_ID", SqlDbType.VarChar, 14).Value = pid;
			DataTable dt = dao.GetDataTable(cmd);
			if(dt.Rows.Count == 0)
				return null;
			else
				return dt.Rows[0];
		}

		/// <summary>
		/// Get all customers
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public DataTable GetCustomers()
		{
			clsCommon common = new clsCommon();
			string strSql = string.Format("SELECT DISTINCT CUST_CODE, CUST_NAME FROM FPT_ENV_DISTRIBUTOR_HIERARCHY ORDER BY CUST_CODE");
			DataTable dt = dao.GetDataTable(strSql);
			DataRow row = dt.NewRow();
			row[0] = string.Empty;
			row[1] = string.Empty;
			dt.Rows.InsertAt(row, 0);
			return dt;
		}

		public DataTable GetShipTo(string strCustCode)
		{
			string strSql;

			try
			{
				strSql = "SELECT SHIP_TO_CODE, SHIP_TO_NAME, PROMOTION_PERCENT, ADDRESS = CASE WHEN ADDRESS <> '' THEN ADDRESS ELSE '(n/a)' END, PHONE = CASE WHEN PHONE <> '' THEN PHONE ELSE '(n/a)' END, CONTACT_PERSON = CASE WHEN CONTACT_PERSON <> '' THEN CONTACT_PERSON ELSE '(n/a)' END, MAIN_SITE = CASE WHEN MAIN_SITE = 'Y' THEN 'Yes' ELSE 'No' END, STATUS = CASE WHEN STATUS = 'AC' THEN 'Active' ELSE 'Inactive' END, RURAL FROM FPT_ENV_SHIP_TO WHERE STATUS = 'AC' AND CUST_CODE = '" + strCustCode + "' ORDER BY MAIN_SITE DESC, SHIP_TO_CODE";
				return dao.GetDataTable(strSql);
			}
			catch(Exception ex)
			{
				log.Error(ex.ToString());
				return null;
			}
		}

		public DataTable GetRuralSKUList(string shipToCode, int week, int year)
		{
			try
			{
				SqlParameter[] parameters = new SqlParameter[3];

				parameters[0] = new SqlParameter();
				parameters[0].ParameterName = "@SHIP_TO_CODE";
				parameters[0].SqlDbType = SqlDbType.VarChar;
				parameters[0].Value = shipToCode;

				parameters[1] = new SqlParameter();
				parameters[1].ParameterName = "@WEEK";
				parameters[1].SqlDbType = SqlDbType.Int;
				parameters[1].Value = week;

				parameters[2] = new SqlParameter();
				parameters[2].ParameterName = "@YEAR";
				parameters[2].SqlDbType = SqlDbType.Int;
				parameters[2].Value = year;

				return dao.ExecuteQuerySp("sp_GetRuralSTDSKUList", parameters);
			}
			catch(Exception ex)
			{
				log.Error(ex.ToString());
				throw new Exception("clsPPODAO.GetRuralSKUList error");
			}
		}

		/// <summary>
		/// Get region code of customer by CustCode
		/// </summary>
		/// <param name="custCode"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public string GetRegionCode(string custCode)
		{
			string strSql = string.Format("SELECT TOP 1 REGION_CODE FROM FPT_ENV_DISTRIBUTOR_HIERARCHY WHERE CUST_CODE = '{0}'", custCode);
			object obj = dao.ExecuteScalar(strSql);
			if(obj == null)
				return "";
			else
				return obj.ToString();
		}



		#region Export to Excel


		/// <summary>
		/// Export data table to Excel
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataTable dt)
		{
			Excel.Application excelApp = null;
			Excel.Workbook excelBook = null;
			Excel.Worksheet sheet = null;
			Excel.Range range = null;

			int i = 0;
			int j = 0;
			int rowCout = dt.Rows.Count;
			int colCount = dt.Columns.Count;
			DataColumnCollection cols = dt.Columns;
			DataRowCollection rows = dt.Rows;

			try
			{
				excelApp = new Excel.Application();
				excelBook = excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				sheet = (Excel.Worksheet) excelBook.Worksheets[1];
				range = null;

				//Export header
				for(i = 0; i < colCount; i ++)
				{
					range = sheet.get_Range(COL_NAME[i] + "1", missing);
					range.Font.Bold = true;
					range.Value2 = cols[i].ColumnName;

					range.EntireColumn.AutoFit();
				}
				
				// Export data row in dt into excel row
				for(i = 0; i < rowCout; i ++)
				{
					for(j = 0; j < colCount; j ++)
					{
						sheet.get_Range(COL_NAME[j] + (i + EXCEL_COL_SPACE), missing).Value2 = rows[i][j].ToString();
					}
				}

				
				excelApp.Visible = true;
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				if(excelApp != null)
					excelApp.Visible = true;
			}
		}

		/// <summary>
		/// Export data table to Excel like DataGridTableStyle
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="grdStyle"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataTable dt, DataGridTableStyle grdStyle)
		{
			ExportToExcel(dt, grdStyle, 0, 0);
		}

		public void ExportToExcel(DataTable dt, DataGridTableStyle grdStyle, int startRow, int startCol)
		{
			if(dt == null)
				return;
			ExportToExcel(dt.DefaultView, grdStyle, startRow, startCol);
		}

		/// <summary>
		/// Export data table to Excel like DataGridTableStyle
		/// </summary>
		/// <param name="view"></param>
		/// <param name="grdStyle"></param>
		/// <param name="startRow"></param>
		/// <param name="startCol"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataView view, DataGridTableStyle grdStyle, int startRow, int startCol)
		{
			if(view == null || grdStyle == null)
				return;

			clsCommon common = new clsCommon();
			string[] headers = null;
			int[] indexes = null;
			common.GetExportInfo(view, grdStyle.GridColumnStyles, ref headers, ref indexes);
			ExportToExcel(view, headers, indexes, startRow, startCol);
		}

		/// <summary>
		/// Export data table to Excel like DataGridTableStyle
		/// </summary>
		/// <param name="view"></param>
		/// <param name="headers"></param>
		/// <param name="indexes"></param>
		/// <param name="startRow"></param>
		/// <param name="startCol"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataView view, string[] headers, int[] indexes, int startRow, int startCol)
		{
			Excel.Application excelApp = null;
			Excel.Workbook excelBook = null;
			Excel.Worksheet sheet = null;

			try
			{
				excelApp = new Excel.Application();
				excelBook = excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				sheet = (Excel.Worksheet) excelBook.Worksheets[1];

				ExportToExcel(view, headers, indexes, startRow, startCol, sheet);

				excelApp.Visible = true;
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				if(excelApp != null)
					excelApp.Visible = true;
			}
		}

		/// <summary>
		/// Export data table to Excel
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataView view, DataGridTableStyle grdStyle, int startRow, int startCol, Excel.Worksheet sheet)
		{
			clsCommon common = new clsCommon();
			string[] headers = null;
			int[] indexes = null;
			common.GetExportInfo(view, grdStyle.GridColumnStyles, ref headers, ref indexes);

			ExportToExcel(view, headers, indexes, startRow, startCol, sheet);
		}

		/// <summary>
		/// Export data table to Excel
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataView view, string[] headers, int[] indexes, int startRow, int startCol, Excel.Worksheet sheet)
		{
			Excel.Range range = null;
			object obj = null;

			int i = 0;
			int j = 0;
			DataColumnCollection cols = view.Table.Columns;
			//DataRowCollection rows = dt.Rows;
			DataView rows = view;
			int rowCout = rows.Count;
			int colCount = indexes.Length;

			for(i = 0; i < rowCout; i ++)
			{
				for(j = 0; j < colCount; j ++)
				{
					obj = rows[i][indexes[j]];
                    sheet.get_Range(COL_NAME[j + startCol] + (i + startRow + EXCEL_COL_SPACE), missing).NumberFormat = "@";
					sheet.get_Range(COL_NAME[j + startCol] + (i + startRow + EXCEL_COL_SPACE), missing).Value2 = obj.ToString();
				}
			}

			//Export header
			colCount = headers.Length;
			for(i = 0; i < colCount; i ++)
			{
				range = sheet.get_Range(COL_NAME[i + startCol] + (startRow + 1), missing);
				range.Font.Bold = true;
				range.Value2 = headers[i];
				range.EntireColumn.AutoFit();
			}
		}

		/// <summary>
		/// Export data table to Excel
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void ExportToExcel(DataTable dt, string[] headers, int[] indexes)
		{
			ExportToExcel(dt.DefaultView, headers, indexes, 0, 0);
		}

		#endregion Export to Excel


		/// <summary>
		/// Replace ' by ''
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public string EncodeString(string value)
		{
			if(value == null || value.Length == 0)
				return value;
			return value.Replace("'", "''");
		}

		/// <summary>
		/// tra ve gia tri tuan bang chu (W12_2006)
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		/// <returns>strDate</returns>

		public string GetWeek()
		{
			string strDate="W";
			SqlConnection con = clsBaseDAO.Connection;
			if(con == null)
				throw new Exception(clsBaseDAO.CONNECTION_ERROR);
			if(con.State != ConnectionState.Open)
				con.Open();
			
			SqlCommand cmd = new SqlCommand("sp_GETWEEK", con);
			cmd.CommandType = CommandType.StoredProcedure;
			try
			{
				strDate	 += cmd.ExecuteScalar().ToString();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			strDate += "_";
			strDate += DateTime.Now.Year.ToString();
			return strDate;
		}

		/// <summary>
		/// tra ve gia tri tuan bang so
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		/// <returns>strDate</returns>

		public string GetWeekSingle()
		{
			string strDate = "";
			SqlConnection con = clsBaseDAO.Connection;

			try
			{
				if(con.State != ConnectionState.Open)
					con.Open();
			
				SqlCommand cmd = new SqlCommand("sp_GETWEEK", con);
				cmd.CommandType = CommandType.StoredProcedure;

				object obj = cmd.ExecuteScalar();
				if(obj != null)
					strDate = obj.ToString();
				else
					strDate = "";

				//strDate	 = cmd.ExecuteScalar();
				return strDate;
			}
			catch(Exception ex)
			{
				throw ex;
			}			
		}

		/// <summary>
		/// bien chuoi truyen vao theo kieu WildCard
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		/// <returns>dtTemp</returns>

		public string WildCard(string strValue)
		{
			for (int i=0;i<strValue.Length;i++)
			{
				if(strValue[i] == '*')
					strValue = strValue.Replace('*', '%');
				else if(strValue[i] == '?')
					strValue = strValue.Replace('?', '_');
			}
			strValue= strValue + "%";
			return strValue;
		}

		/// <summary>
		/// tra ve gia tri tuan: tu tuan hien tai cho den tuan 52
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		/// <returns>intWeeks</returns>
	
		public int[] InitWeek()
		{
			clsCommon common = new clsCommon();
			int intCurrentWeek = common.GetInt(GetWeekSingle());
			if(intCurrentWeek < 52)
				intCurrentWeek += 1;
			int intArraySize = 53-intCurrentWeek;
			int[] intWeeks  = new int[intArraySize];
			int i=0;
			while(intCurrentWeek<=52)
			{
				intWeeks[i] = intCurrentWeek;
				i++;
				intCurrentWeek++;
			}
			return intWeeks;
		}

		/// <summary>
		/// tra ve gia tri nam: nam hien tai, qua khu va nam tiep theo
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		/// <returns>intYears</returns>
		
		public int[] InitYear()
		{
			int intCurrentYear = DateTime.Now.Year;
			int[] intYears = {intCurrentYear-1, intCurrentYear, intCurrentYear+1};
			return intYears;
		}

		/// <summary>
		/// neu chuoi rong tra ve gia tri can thiet, neu ko tra ve chinh chuoi do
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		/// <param name="strValue"</param>
		/// <returns>intYears</returns>

		public object EmptyNull(object objValue, object objReturnValue)
		{
			if(objValue.ToString().Length == 0 || objValue == DBNull.Value)
				return objReturnValue;
			else
				return objValue;
		}


        /// <summary>
        /// Get Strategic Region by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuthorizedStrategicRegion()
        {
            try
            {
                return dao.GetAuthorizedStrategicRegion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Strategic Region by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetAuthorizedStrategicRegion(string userName)
        {
            try
            {
                return dao.GetAuthorizedStrategicRegion(userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get RegionCode by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuthorizedRegion()
        {
            try
            {
                return dao.GetAuthorizedRegion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get RegionCode by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetAuthorizedRegion(string userName)
        {
            try
            {
                return dao.GetAuthorizedRegion(userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add order-number column to datatable.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colName">Name of order0number column</param>
        /// <returns></returns>
        public DataTable AddOrderNumColumn(DataTable dt, string colName)
        {
            //Add column if not exist
            if (!dt.Columns.Contains(colName))
            {
                dt.Columns.Add(colName);
            }

            int num = 1;

            foreach (DataRow row in dt.Rows)
            {
                row[colName] = num;
                num++;
            }

            return dt;
        }

        public DataTable GetAllChannel()
        {
            try
            {
                string strSQL = "SELECT DISTINCT [CHANNEL_CODE],[CHANNEL_NAME] FROM FPT_ENV_CHANNEL ORDER BY CHANNEL_NAME ASC ";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null); 
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        //DongTC
        public DataTable GetAllChannelCopy()
        {
            try
            {
                string strSQL = "SELECT DISTINCT [CHANNEL_CODE],[CHANNEL_NAME] FROM FPT_ENV_CHANNEL ORDER BY CHANNEL_NAME ASC ";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }


        //Get Distributor
        public DataTable GetDistributor()
        {
            try
            {
                string sql = "SELECT DISTINCT D.[CUST_NAME] FROM FPT_ENV_STORE S LEFT JOIN FPT_ENV_DISTRIBUTOR_HIERARCHY D ON S.[DISTRIBUTOR_CODE]=D.[CUST_CODE] ORDER BY D.CUST_NAME";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);

                //foreach (DataRow row in dt.Rows)
                //{
                //    string strcustcode = row["CUST_NAME"].ToString();
                //    string strcustname = row["CUST_NAME"].ToString();

                //    //if (string.IsNullOrEmpty(strcustname))
                //    //{
                //    //    row["CUST_NAME"] = strcustcode;
                //    //}
                //}
                return dt;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public DataTable GetAllRegion()
        {
            try
            {
                string sqlregion = "p_FPT_ENV_GetAllRegionFromRegionHierarchy";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public DataTable GetAllRegionExMS()
        {
            try
            {
                string sqlregion = "p_FPT_ENV_GetAllRegionFromRegionHierarchyExMS";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }
        //DongTC
        public DataTable GetAllRegionCopy()
        {
            try
            {
                string strSQL = "SELECT DISTINCT [REGION_CODE],[REGION_NAME] FROM FPT_ENV_REGION_HIERARCHY ORDER BY REGION_NAME ASC";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }


        public DataTable GetAllTown()
        {
            try
            {
                string strSQL = "SELECT DISTINCT [TOWN_CODE],[TOWN_NAME] FROM FPT_ENV_TOWN ORDER BY TOWN_NAME ASC";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public DataTable GetAllProvince()
        {
            try
            {
                string strSQL = "SELECT DISTINCT [PROVINCE_CODE], [PROVINCE_NAME] FROM FPT_ENV_PROVINCE ORDER BY PROVINCE_NAME ASC";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        //DongTC
        public DataTable GetAllByRegionAndProvinceAndTown(string region,string province, string town)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter("@strRegionName", region);
                para[1] = new SqlParameter("@strProvinceName", province);
                para[2] = new SqlParameter("@strTownName", town);
                string strSQL = "p_FPT_ENV_GetAllByRegionAndProvinceAndTown";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.StoredProcedure, strSQL, para);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }


        public DataTable GetAllPSType()
        {
            try
            {
                string strSQL = "SELECT DISTINCT * FROM FPT_ENV_PS_TYPE";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public DataTable GetChecklistScopeRecords(long checklistId, long kpiTypeId)
        {
            try
            {
                string strSQL = "SELECT PS_TYPE_CODE, STAR_CLUB, UPDATED_DATE FROM FPT_ENV_CHECKLIST_SCOPE WHERE CHECKLIST_ID = " + checklistId.ToString()
                    + " AND KPI_TYPE_ID = " + kpiTypeId.ToString();
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public DataTable GetAllHotspot()
        {
            try
            {
                string strStoredProcName = "p_FPT_ENV_Hotspot_Select_All";
                return UKPI.Utils.DataServices.ExecuteDataTable(CommandType.StoredProcedure, strStoredProcName);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        #region Load checkedlist
        public static DataTable GetPSType_Name()
        {
            try
            {
                string strSQL = "SELECT DISTINCT [NAME] FROM FPT_ENV_PS_TYPE ORDER BY [NAME] ASC";

                DataTable dt = UKPI.Utils.DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);

                return dt;
         
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       
        public static DataTable GetAllProduct()
        {
            try
            {
                DataTable dt = new DataTable();
                string strSQL = "SELECT * FROM FPT_ENV_PRODUCT";
                dt = DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
                return dt;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public static DataTable GetAllPSTypeName()
        {
            try
            {
                DataTable dt = new DataTable();
                string strSQL = "SELECT DISTINCT [NAME] AS PS_TYPE_NAME FROM FPT_ENV_PS_TYPE ORDER BY PS_TYPE_NAME ASC ";
                dt = DataServices.ExecuteDataTable(CommandType.Text, strSQL, null);
                return dt;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }

        public System.Data.DataTable CreatDatatable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("NAME", typeof(string));
            DataRow row;

            row = dt.NewRow();
            row["NAME"] = "English";
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["NAME"] = "Vietnamese";
            dt.Rows.Add(row);
            return dt;
        }

        //DongTC
        /// <summary>
        /// Delete OSA where product and check list
        /// </summary>
        /// <param name="product"></param>
        /// <param name="checklist"></param>

        public void DeleteTableOSA(long checklist)
        {
            string SQL = "DELETE FROM FPT_ENV_CHECKLIST_OSA WHERE [CHECKLIST_ID]='" + checklist + "'";
            DataServices.ExecuteNonQuery(CommandType.Text, SQL);   
        }


        //DongTC
        /// <summary>
        /// Delete NPD where product and check list
        /// </summary>
        /// <param name="product"></param>
        /// <param name="checklist"></param>

        public void DeleteTableNPD(long checklist)
        {
            string SQL = "DELETE FROM FPT_ENV_CHECKLIST_NPD WHERE [CHECKLIST_ID]='" + checklist + "'";
            DataServices.ExecuteNonQuery(CommandType.Text, SQL);
        }

        //DongTC
        /// <summary>
        /// Delete STANDARD PRICE where product and check list
        /// </summary>
        /// <param name="product"></param>
        /// <param name="checklist"></param>

        public void DeleteTableSTANDARDPRICE(long checklist)
        {
            string SQL = "DELETE FROM FPT_ENV_CHECKLIST_STD_PRICE WHERE  [CHECKLIST_ID]='" + checklist + "'";
            DataServices.ExecuteNonQuery(CommandType.Text, SQL);
        }

        
        //DongTC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CheckGetChange(DataTable dt)
        {
            if (dt.GetChanges()!=null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        //DongTC
        public DataTable SearchDistributorByChannelAndRegion(string channel, string region,string
            province,string town)
        {
            try
            {
                string sql = "p_FPT_ENV_SearchDistributorByChannelAndRegion";
                SqlParameter[] para = new SqlParameter[4];
                para[0] = new SqlParameter("@strChannelCode", channel);
                para[1] = new SqlParameter("@strRegionName", region);
                para[2] = new SqlParameter("@strProvinceName", province);
                para[3] = new SqlParameter("@strTownName", town);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, sql, para);
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC
        public void GetDistributorChannelAndRegionAn(ComboBox cbochannel, ComboBox cboregion, ComboBox cboprovince, ComboBox cbotown, TextBox txtDistributors)
        {
            try
            {
                string channel = cbochannel.SelectedValue.ToString();
                string region = cboregion.Text.ToString();
                string province = cboprovince.Text.ToString();
                string town = cbotown.Text.ToString();
                if (string.IsNullOrEmpty(channel) && string.IsNullOrEmpty(region) && string.IsNullOrEmpty(province) && string.IsNullOrEmpty(town))
                {
                    //cboDistributor.DataSource = null;
                    txtDistributors.Text = null;
                    System.Data.DataTable dtDistributor = GetDistributor();
                    dtDistributor.Rows.InsertAt(dtDistributor.NewRow(), 0);
                    //cbodistributor.ValueMember = "CUST_NAME";
                    //cbodistributor.DisplayMember = "CUST_NAME";
                    //cbodistributor.DataSource = dtDistributor;
                }

                else
                {
                    //cbodistributor.DataSource = null;
                    System.Data.DataTable dt = SearchDistributorByChannelAndRegion(channel, region, province, town);
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    string strcustcode = row["CUST_NAME"].ToString();
                    //    string strcustname = row["CUST_NAME"].ToString();

                    //    if (string.IsNullOrEmpty(strcustname))
                    //    {
                    //        row["CUST_NAME"] = strcustcode;
                    //    }
                    //}

                    dt.Rows.InsertAt(dt.NewRow(), 0);
                    //cbodistributor.ValueMember = "CUST_NAME";
                    //cbodistributor.DisplayMember = "CUST_NAME";
                    //cbodistributor.DataSource = dt;

                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC OSA
        public bool CheckDataOSABeforeCopy(long checklist)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_CHECKLIST_OSA WHERE [CHECKLIST_ID]='"+checklist+"'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC NPD
        public bool CheckDataNPDBeforeCopy(long checklist)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_CHECKLIST_NPD WHERE [CHECKLIST_ID]='" + checklist + "'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC Promotion Compliance
        public bool CheckDataPromotionComplianceBeforeCopy(long checklist)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_CHECKLIST_PROMOTION_COMPLIANCE WHERE [CHECKLIST_ID]='" + checklist + "'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC
        public void ChecklistPromotionComplianceDeleteBeforeCopy(long checklist)
        {
            try
            {
                SqlParameter []para=new SqlParameter[2];
                string sql = "SELECT * FROM FPT_ENV_CHECKLIST_PROMOTION_COMPLIANCE WHERE [CHECKLIST_ID]='" + checklist + "'";
                string sqlDel = "p_FPT_ENV_Checklist_Promotion_compliance_Delete_BeforeCopy";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                //Delete promotion and product
                foreach (DataRow row in dt.Rows)
                {
                    string ID = row["ID"].ToString();
                    long checklistDel=long.Parse(row["CHECKLIST_ID"].ToString());
                    para[0] = new SqlParameter("@bintchecklist", checklistDel);
                    para[1] = new SqlParameter("@bintID", ID);
                    DataServices.ExecuteNonQuery(CommandType.StoredProcedure, sqlDel, para);
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC Standard price
        public bool CheckDataStandardPriceBeforeCopy(long checklist)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_CHECKLIST_STD_PRICE WHERE [CHECKLIST_ID]='" + checklist + "'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC SOF
        public bool CheckDataSOFBeforeCopy(long checklist)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_CHECKLIST_SOF WHERE [CHECKLIST_ID]='" + checklist + "'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC SOF Delete table SOF before copy data
        public void DeleteSOFBeforeCopy(long checklist)
        {
            try
            {
                string sql = "DELETE FROM FPT_ENV_CHECKLIST_SOF WHERE [CHECKLIST_ID]='" + checklist + "'";
                DataServices.ExecuteNonQuery(CommandType.Text, sql, null);
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC
        //Check store code Exists in FPT_ENV_STORE
        public bool CheckStoreExists(string storecode)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_STORE WHERE [STORE_CODE]= N'" + storecode + "'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);
                if (dt.Rows.Count>0)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC
        //Check store name, store address and PS Type
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StoreId"></param>
        /// <param name="storeName"></param>
        /// <param name="storeAdd"></param>
        /// <param name="PSType"></param>
        /// <returns></returns>
        public bool CheckStoreImportIsNullOrEmpty(string StoreId,string storeName,
            string storeAdd, string SaleSupID, bool isEdit)
        {
            try
            {
                if (isEdit)
                {
                    if (!string.IsNullOrEmpty(StoreId) && !string.IsNullOrEmpty(storeName) &&
                    !string.IsNullOrEmpty(SaleSupID))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(StoreId) && !string.IsNullOrEmpty(storeName) &&
                        !string.IsNullOrEmpty(storeAdd) && !string.IsNullOrEmpty(SaleSupID))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }



        public bool CheckStoreImportIsNullOrEmptyAddNew(string StoreId, string storeName,
            string storeAdd,string Distributorcode,string DistributorName,string Town,string Province,
            string region,string urban,string typeofoutlet,string location,string starclub,string channel,
            string SaleSupID)
        {
            try
            {
                if (!string.IsNullOrEmpty(StoreId) && !string.IsNullOrEmpty(storeName) &&
                    !string.IsNullOrEmpty(storeAdd) && !string.IsNullOrEmpty(SaleSupID)&&
                    !string.IsNullOrEmpty(Distributorcode)&&!string.IsNullOrEmpty(DistributorName)&&
                    !string.IsNullOrEmpty(Town)&&!string.IsNullOrEmpty(Province)&&
                    !string.IsNullOrEmpty(region)&&!string.IsNullOrEmpty(urban)&&
                    !string.IsNullOrEmpty(typeofoutlet)&&!string.IsNullOrEmpty(location)&&
                    !string.IsNullOrEmpty(starclub)&&!string.IsNullOrEmpty(channel))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //Check data null
        //DongTC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void SetValueIsNull(DataTable dt)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        string rowValue = row[col].ToString();
                        if (string.IsNullOrEmpty(rowValue))
                        {
                            row[col] = "Null";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        //DongTC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PsType"></param>
        /// <returns></returns>
        
        public bool CheckPSTypeExists(string PsType)
        {
            try
            {
                string sql = "SELECT * FROM FPT_ENV_PS_TYPE WHERE [NAME]='"+ PsType +"'";
                DataTable dt = DataServices.ExecuteDataTable(CommandType.Text, sql, null);

                if (dt.Rows.Count>0)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboRegion"></param>
        /// <param name="cboProvince"></param>
        /// <param name="cboTown"></param>
        public void GetRegionProvinceTownChannelFromStore(ComboBox cbochannel, ComboBox cboRegion, ComboBox cboProvince, ComboBox cboTown)
        {
            try
            {
                //string sqlregion = "p_FPT_ENV_GetAllRegionFromStore";
                //DataTable dtRegion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, null);
                DataTable dtRegion = GetAllRegionExMS();
                dtRegion.Rows.InsertAt(dtRegion.NewRow(), 0);
                cboRegion.ValueMember = "REGION_NAME";
                cboRegion.DisplayMember = "REGION_NAME";
                cboRegion.DataSource = dtRegion;

                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromStore";
                DataTable dtProvince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, null);

                dtProvince.Rows.InsertAt(dtProvince.NewRow(), 0);
                cboProvince.ValueMember = "PROVINCE_NAME";
                cboProvince.DisplayMember = "PROVINCE_NAME";
                cboProvince.DataSource = dtProvince;


                string sqltown = "p_FPT_ENV_GetAllTownFromStore";
                DataTable dtTown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, null);

                dtTown.Rows.InsertAt(dtTown.NewRow(), 0);
                cboTown.ValueMember = "TOWN_NAME";
                cboTown.DisplayMember = "TOWN_NAME";
                cboTown.DataSource = dtTown;

                //string sqlchannel = "p_FPT_ENV_GetAllChannelFromStore";
                //DataTable dtChannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, null);
                DataTable dtChannel = GetAllChannel();
                foreach (DataRow row in dtChannel.Rows)
                {
                    string strchannelname = row["CHANNEL_NAME"].ToString();
                    string strchannelcode = row["CHANNEL_CODE"].ToString();
                    if (string.IsNullOrEmpty(strchannelname))
                    {
                        row["CHANNEL_NAME"] = strchannelcode;
                    }
                }

                dtChannel.Rows.InsertAt(dtChannel.NewRow(), 0);
                cbochannel.ValueMember = "CHANNEL_CODE";
                cbochannel.DisplayMember = "CHANNEL_NAME";
                cbochannel.DataSource = dtChannel;

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void GetRegionProvinceTownChannelFromStoreNW(ComboBox cbochannel, ComboBox cboRegion, ComboBox cboProvince, ComboBox cboTown)
        {
            try
            {
                DataTable dtRegion = GetAllRegionExMS();
                DataRow rowNW;
                rowNW = dtRegion.NewRow();
                rowNW["REGION_CODE"] = "NW";
                rowNW["REGION_NAME"] = "NATION WIDE";
                dtRegion.Rows.Add(rowNW);
                dtRegion.Rows.InsertAt(dtRegion.NewRow(), 0);
                cboRegion.ValueMember = "REGION_NAME";
                cboRegion.DisplayMember = "REGION_NAME";
                cboRegion.DataSource = dtRegion;

                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromStore";
                DataTable dtProvince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, null);

                dtProvince.Rows.InsertAt(dtProvince.NewRow(), 0);
                cboProvince.ValueMember = "PROVINCE_NAME";
                cboProvince.DisplayMember = "PROVINCE_NAME";
                cboProvince.DataSource = dtProvince;


                string sqltown = "p_FPT_ENV_GetAllTownFromStore";
                DataTable dtTown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, null);

                dtTown.Rows.InsertAt(dtTown.NewRow(), 0);
                cboTown.ValueMember = "TOWN_NAME";
                cboTown.DisplayMember = "TOWN_NAME";
                cboTown.DataSource = dtTown;

                //string sqlchannel = "p_FPT_ENV_GetAllChannelFromStore";
                //DataTable dtChannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, null);
                DataTable dtChannel = GetAllChannel();
                foreach (DataRow row in dtChannel.Rows)
                {
                    string strchannelname = row["CHANNEL_NAME"].ToString();
                    string strchannelcode = row["CHANNEL_CODE"].ToString();
                    if (string.IsNullOrEmpty(strchannelname))
                    {
                        row["CHANNEL_NAME"] = strchannelcode;
                    }
                }

                dtChannel.Rows.InsertAt(dtChannel.NewRow(), 0);
                cbochannel.ValueMember = "CHANNEL_CODE";
                cbochannel.DisplayMember = "CHANNEL_NAME";
                cbochannel.DataSource = dtChannel;

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        #region Changed ComboBox

        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboRegion
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboRegion"></param>
        /// <param name="cboProvince"></param>
        /// <param name="cboTown"></param>
        public void GetRegionProvinceTownChannelFromStoreFilterByRegion(ComboBox cbochannel, ComboBox cboRegion, ComboBox cboProvince, ComboBox cboTown)
        {
            try
            {
                string strchannel = cbochannel.SelectedValue.ToString();
                string strregion = cboRegion.Text.ToString();
                string strprovince = "";
                string strtown = "";
                //strprovince = cboProvince.Text.ToString();
                //strtown = cboTown.Text.ToString();

                string sqlregion = "p_FPT_ENV_GetAllRegionByFilterChannelProvinceTown";
                string sqlprovince = "p_FPT_ENV_GetAllProvinceByFilterChannelRegionTown";
                string sqltown = "p_FPT_ENV_GetAllTownByFilterChannelRegionProvince";
                string sqlchannel = "p_FPT_ENV_GetAllChannelByFilterRegionProvinceTown";

                SqlParameter[]para=new SqlParameter[4];
                para[0] = new SqlParameter("@strChannelCode", strchannel);
                para[1] = new SqlParameter("@strRegionName", strregion);
                //para[2] = new SqlParameter("@strProvinceName", string.Empty);
                para[2] = new SqlParameter("@strProvinceName", strprovince);
                para[3] = new SqlParameter("@strTownName", strtown);

                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);
                DataTable dttown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, para);
                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);
                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                
                //Modify 17Jan2011
                #region Don't reload cboChannel, alway load these Channel
                /*
                //load data on cbochannel
                foreach (DataRow row in dtchannel.Rows)
                {
                    string strchannelname = row["CHANNEL_NAME"].ToString();
                    string strchannelcode = row["CHANNEL_CODE"].ToString();

                    if (string.IsNullOrEmpty(strchannelname))
                    {
                        row["CHANNEL_NAME"] = strchannelcode;
                    }
                }

                if (string.IsNullOrEmpty(strchannel))
                {
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
                else
                {
                    DataRow[] rows = dtchannel.Select();
                    int countcbochannel = cbochannel.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbochannel != countrows)
                    {
                        int count = dtchannel.Rows.Count + 1;
                        dtchannel.Rows.InsertAt(dtchannel.NewRow(), count);
                        cbochannel.ValueMember = "CHANNEL_CODE";
                        cbochannel.DisplayMember = "CHANNEL_NAME";
                        cbochannel.DataSource = dtchannel;

                    }
                }
                //load data on cboregion
                if (string.IsNullOrEmpty(strregion))
                {
                    
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboRegion.ValueMember = "REGION_NAME";
                    cboRegion.DisplayMember = "REGION_NAME";
                    cboRegion.DataSource = dtregion;
                } 
                 
                */
                #endregion
                
                if (string.IsNullOrEmpty(strprovince))
                {
                    //load data on cboprovince
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovince;
                }
                else
                {
                    DataRow[] rows = dtprovince.Select();
                    int countcboprovince=cboProvince.Items.Count-1;
                    int countrows = rows.Length;
                    if (countcboprovince != countrows)
                    {
                        //load data on cboprovince
                        int count = dtprovince.Rows.Count + 1;
                        dtprovince.Rows.InsertAt(dtprovince.NewRow(), count);
                        cboProvince.ValueMember = "PROVINCE_NAME";
                        cboProvince.DisplayMember = "PROVINCE_NAME";
                        cboProvince.DataSource = dtprovince;
                    }
                   
                }

                if (string.IsNullOrEmpty(strtown))
                {
                    //load data on cbotown
                    dttown.Rows.InsertAt(dttown.NewRow(), 0);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttown;
                }
                else
                {
                    DataRow[] rows = dttown.Select();
                    int countcbotown=cboTown.Items.Count-1;
                    int countrows = rows.Length;
                    if (countcbotown != countrows)
                    {
                        //load data on cbotown
                        int count = dttown.Rows.Count + 1;
                        dttown.Rows.InsertAt(dttown.NewRow(), count);
                        cboTown.ValueMember = "TOWN_NAME";
                        cboTown.DisplayMember = "TOWN_NAME";
                        cboTown.DataSource = dttown;
                    }
                }

                /*
             //check all region ,province, town is null
                if (string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && !string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dttownReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, paraReload);

                    int count = dttownReload.Rows.Count + 1;
                    dttownReload.Rows.InsertAt(dttownReload.NewRow(), count);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttownReload;
                }
                
                //Reload channel
                if (!string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtchannelReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, paraReload);

                    foreach (DataRow row in dtchannelReload.Rows)
                    {
                        string strchannelname = row["CHANNEL_NAME"].ToString();
                        string strchannelcode = row["CHANNEL_CODE"].ToString();

                        if (string.IsNullOrEmpty(strchannelname))
                        {
                            row["CHANNEL_NAME"] = strchannelcode;
                        }
                    }

                    int count = dtchannelReload.Rows.Count + 1;
                    dtchannelReload.Rows.InsertAt(dtchannelReload.NewRow(), count);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannelReload;
                }

                if (string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && !string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtprovinceReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, paraReload);
                    int count = dtprovinceReload.Rows.Count + 1;
                    dtprovinceReload.Rows.InsertAt(dtprovinceReload.NewRow(), count);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovinceReload;
                }*/

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboProvince
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboRegion"></param>
        /// <param name="cboProvince"></param>
        /// <param name="cboTown"></param>
        public void GetRegionProvinceTownChannelFromStoreFilterByProvince(ComboBox cbochannel, ComboBox cboRegion, ComboBox cboProvince, ComboBox cboTown)
        {
            try
            {
                string strchannel = cbochannel.SelectedValue.ToString();
                string strregion = cboRegion.Text.ToString();
                string strprovince = cboProvince.Text.ToString();
                string strtown = "";
                //strtown = cboTown.Text.ToString();

                //string strtown = string.Empty;

                string sqlregion = "p_FPT_ENV_GetAllRegionByFilterChannelProvinceTown";

                string sqlprovince = "p_FPT_ENV_GetAllProvinceByFilterChannelRegionTown";

                string sqltown = "p_FPT_ENV_GetAllTownByFilterChannelRegionProvince";

                string sqlchannel = "p_FPT_ENV_GetAllChannelByFilterRegionProvinceTown";

                SqlParameter[] para = new SqlParameter[4];
                para[0] = new SqlParameter("@strChannelCode", strchannel);
                para[1] = new SqlParameter("@strRegionName", strregion);
                para[2] = new SqlParameter("@strProvinceName", strprovince);
                para[3] = new SqlParameter("@strTownName", strtown);
                //para[3] = new SqlParameter("@strTownName", string.Empty);

                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                DataTable dttown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, para);

                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);

                #region Modify 17Jan2011-Don't reload cboChannel, cboRegion. Page always load these values
                /*
                //load data on cbochannel

                foreach (DataRow row in dtchannel.Rows)
                {
                    string strchannelname = row["CHANNEL_NAME"].ToString();
                    string strchannelcode = row["CHANNEL_CODE"].ToString();

                    if (string.IsNullOrEmpty(strchannelname))
                    {
                        row["CHANNEL_NAME"] = strchannelcode;
                    }
                }

                if (string.IsNullOrEmpty(strchannel))
                {
                    //int count = dtchannel.Rows.Count + 1;
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
                else
                {
                    DataRow[] rows = dtchannel.Select();
                    int countcbochannel = cbochannel.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbochannel!=countrows)
                    {
                        int count = dtchannel.Rows.Count + 1;
                        dtchannel.Rows.InsertAt(dtchannel.NewRow(), count);
                        cbochannel.ValueMember = "CHANNEL_CODE";
                        cbochannel.DisplayMember = "CHANNEL_NAME";
                        cbochannel.DataSource = dtchannel;

                    }
                  
                }
                
                //load data on cboregion
                if (string.IsNullOrEmpty(strregion))
                {
                    
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboRegion.ValueMember = "REGION_NAME";
                    cboRegion.DisplayMember = "REGION_NAME";
                    cboRegion.DataSource = dtregion;
                } 
                else
                {
                    DataRow[] rows = dtregion.Select();
                    int countcboregion = cboRegion.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboregion!=countrows)
                    {
                        int count = dtregion.Rows.Count + 1;
                        dtregion.Rows.InsertAt(dtregion.NewRow(), count);
                        cboRegion.ValueMember = "REGION_NAME";
                        cboRegion.DisplayMember = "REGION_NAME";
                        cboRegion.DataSource = dtregion;
                    }
                   
                }

                //load data on cboprovince
                if (string.IsNullOrEmpty(strprovince))
                {
                    DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovince;
                }
                */
                #endregion


                if (string.IsNullOrEmpty(strtown))
                {
                    //load data on cbotown
                    dttown.Rows.InsertAt(dttown.NewRow(), 0);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttown;
                }
                else
                {
                    DataRow[] rows = dttown.Select();
                    int countcbotown = cboTown.Items.Count - 1;
                    int countrows = rows.Length;
                    //load data on cbotown
                    if (countcbotown != countrows)
                    {
                        int count = dttown.Rows.Count + 1;
                        dttown.Rows.InsertAt(dttown.NewRow(), count);
                        cboTown.ValueMember = "TOWN_NAME";
                        cboTown.DisplayMember = "TOWN_NAME";
                        cboTown.DataSource = dttown;
                    }
                  
                }
                /*
                 //check all region ,province, town is null
                if (string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && !string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dttownReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, paraReload);

                    int count = dttownReload.Rows.Count + 1;
                    dttownReload.Rows.InsertAt(dttownReload.NewRow(), count);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttownReload;

                }

                //Reload channel
                if (!string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtchannelReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, paraReload);

                    foreach (DataRow row in dtchannelReload.Rows)
                    {
                        string strchannelname = row["CHANNEL_NAME"].ToString();
                        string strchannelcode = row["CHANNEL_CODE"].ToString();

                        if (string.IsNullOrEmpty(strchannelname))
                        {
                            row["CHANNEL_NAME"] = strchannelcode;
                        }
                    }

                    int count = dtchannelReload.Rows.Count + 1;
                    dtchannelReload.Rows.InsertAt(dtchannelReload.NewRow(), count);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannelReload;
                }

                if (string.IsNullOrEmpty(strchannel) && !string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtregionReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, paraReload);

                    int count = dtregionReload.Rows.Count + 1;
                    dtregionReload.Rows.InsertAt(dtregionReload.NewRow(), count);
                    cboRegion.ValueMember = "REGION_NAME";
                    cboRegion.DisplayMember = "REGION_NAME";
                    cboRegion.DataSource = dtregionReload;
                }*/
               
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboTown
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboRegion"></param>
        /// <param name="cboProvince"></param>
        /// <param name="cboTown"></param>
        public void GetRegionProvinceTownChannelFromStoreFilterByTown(ComboBox cbochannel, ComboBox cboRegion, ComboBox cboProvince, ComboBox cboTown)
        {
            try
            {
                string strchannel = cbochannel.SelectedValue.ToString();
                string strregion = cboRegion.Text.ToString();
                string strprovince = cboProvince.Text.ToString();
                string strtown = cboTown.Text.ToString();
                
                string sqlregion = "p_FPT_ENV_GetAllRegionByFilterChannelProvinceTown";

                string sqlprovince = "p_FPT_ENV_GetAllProvinceByFilterChannelRegionTown";

                string sqltown = "p_FPT_ENV_GetAllTownByFilterChannelRegionProvince";

                string sqlchannel = "p_FPT_ENV_GetAllChannelByFilterRegionProvinceTown";

                SqlParameter[] para = new SqlParameter[4];
                para[0] = new SqlParameter("@strChannelCode", strchannel);
                para[1] = new SqlParameter("@strRegionName", strregion);
                para[2] = new SqlParameter("@strProvinceName", strprovince);
                para[3] = new SqlParameter("@strTownName", strtown);

                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);

                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);

                DataTable dttown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, para);
                #region Modify 17Jan2011-Don't reload cboChannel, cboRegion. Page always load these values
                /*
                //load data on cbochannel

                foreach (DataRow row in dtchannel.Rows)
                {
                    string strchannelname = row["CHANNEL_NAME"].ToString();
                    string strchannelcode = row["CHANNEL_CODE"].ToString();

                    if (string.IsNullOrEmpty(strchannelname))
                    {
                        row["CHANNEL_NAME"] = strchannelcode;
                    }
                }

                if (string.IsNullOrEmpty(strchannel))
                {
                   
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
                else
                {
                    DataRow[] rows = dtchannel.Select();
                    int countcbochannel = cbochannel.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbochannel!=countrows)
                    {
                        int count = dtchannel.Rows.Count + 1;
                        dtchannel.Rows.InsertAt(dtchannel.NewRow(), count);
                        cbochannel.ValueMember = "CHANNEL_CODE";
                        cbochannel.DisplayMember = "CHANNEL_NAME";
                        cbochannel.DataSource = dtchannel;

                    }
                }
               
                //load data on cboregion

                if (string.IsNullOrEmpty(strregion))
                {
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboRegion.ValueMember = "REGION_NAME";
                    cboRegion.DisplayMember = "REGION_NAME";
                    cboRegion.DataSource = dtregion;

                } 
                else
                {
                    DataRow[] rows = dtregion.Select();
                    int countcboregion = cboRegion.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboregion!=countrows)
                    {
                        int count = dtregion.Rows.Count + 1;
                        dtregion.Rows.InsertAt(dtregion.NewRow(), count);
                        cboRegion.ValueMember = "REGION_NAME";
                        cboRegion.DisplayMember = "REGION_NAME";
                        cboRegion.DataSource = dtregion;
                    }
                }
                */
                #endregion

                //load data on cboprovince
                //Modify 17Jan2011
                //if (string.IsNullOrEmpty(strprovince))
                //{
                //    if (dtprovince.Rows.Count > 0 && strtown != "")
                //    {
                //        strprovince = dtprovince.Rows[0]["PROVINCE_NAME"].ToString();
                //    }
                //}
                //else
                //{
                //    if (strtown != "" && strprovince != dtprovince.Rows[0]["PROVINCE_NAME"].ToString())
                //    {
                //        strprovince = dtprovince.Rows[0]["PROVINCE_NAME"].ToString();
                //    }
                //}
                //cboProvince.SelectedValue = strprovince;

                /*if (string.IsNullOrEmpty(strprovince))
                {
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovince;
                }
                else
                {
                    DataRow[] rows = dtprovince.Select();
                    int countcboprovince = cboProvince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboprovince!=countrows)
                    {
                        int count = dtprovince.Rows.Count + 1;
                        dtprovince.Rows.InsertAt(dtprovince.NewRow(), count);
                        cboProvince.ValueMember = "PROVINCE_NAME";
                        cboProvince.DisplayMember = "PROVINCE_NAME";
                        cboProvince.DataSource = dtprovince;
                    }
                }
                */
                
                if (string.IsNullOrEmpty(strprovince))
                {
                    sqltown = "p_FPT_ENV_GetAllTownFromStore";
                    dttown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, null);

                    dttown.Rows.InsertAt(dttown.NewRow(), 0);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttown;
                    if (!string.IsNullOrEmpty(strtown))
                    {
                        cboTown.SelectedValue = strtown;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(strtown))
                    {
                        //int count = dttown.Rows.Count + 1;
                        dttown.Rows.InsertAt(dttown.NewRow(), 0);
                        cboTown.ValueMember = "TOWN_NAME";
                        cboTown.DisplayMember = "TOWN_NAME";
                        cboTown.DataSource = dttown;
                    }
                }

                //load data on cbotown
                //Closed 17Jan2011 By HangTTN
                /*
                if (string.IsNullOrEmpty(strtown))
                {

                    //int count = dttown.Rows.Count + 1;
                    dttown.Rows.InsertAt(dttown.NewRow(), 0);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttown;
                }
                */

                #region Closed before 17Jan2011 by DongTC
                /*
                //Reload channel
                if (!string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtchannelReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, paraReload);

                    foreach (DataRow row in dtchannelReload.Rows)
                    {
                        string strchannelname = row["CHANNEL_NAME"].ToString();
                        string strchannelcode = row["CHANNEL_CODE"].ToString();

                        if (string.IsNullOrEmpty(strchannelname))
                        {
                            row["CHANNEL_NAME"] = strchannelcode;
                        }
                    }

                    int count = dtchannelReload.Rows.Count + 1;
                    dtchannelReload.Rows.InsertAt(dtchannelReload.NewRow(), count);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannelReload;
                }

                if (string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && !string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtprovinceReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, paraReload);
                    int count = dtprovinceReload.Rows.Count + 1;
                    dtprovinceReload.Rows.InsertAt(dtprovinceReload.NewRow(), count);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovinceReload;
                }

                if (string.IsNullOrEmpty(strchannel) && !string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtregionReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, paraReload);

                    int count = dtregionReload.Rows.Count + 1;
                    dtregionReload.Rows.InsertAt(dtregionReload.NewRow(), count);
                    cboRegion.ValueMember = "REGION_NAME";
                    cboRegion.DisplayMember = "REGION_NAME";
                    cboRegion.DataSource = dtregionReload;
                }*/
                #endregion

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboChannel
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboRegion"></param>
        /// <param name="cboProvince"></param>
        /// <param name="cboTown"></param>
        public void GetRegionProvinceTownChannelFromStoreFilterByChannel(ComboBox cbochannel, ComboBox cboRegion, ComboBox cboProvince, ComboBox cboTown)
        {
            try
            {
                string strchannel = cbochannel.SelectedValue.ToString();
                string strregion = cboRegion.Text.ToString();
                string strprovince = cboProvince.Text.ToString();
                string strtown = cboTown.Text.ToString();
               
                string sqlregion = "p_FPT_ENV_GetAllRegionByFilterChannelProvinceTown";

                string sqlprovince = "p_FPT_ENV_GetAllProvinceByFilterChannelRegionTown";

                string sqltown = "p_FPT_ENV_GetAllTownByFilterChannelRegionProvince";

                string sqlchannel = "p_FPT_ENV_GetAllChannelByFilterRegionProvinceTown";

                SqlParameter[] para = new SqlParameter[4];
                para[0] = new SqlParameter("@strChannelCode", strchannel);
                para[1] = new SqlParameter("@strRegionName",strregion);
                //para[1] = new SqlParameter("@strRegionName", string.Empty);
                para[2] = new SqlParameter("@strProvinceName", strprovince);
                para[3] = new SqlParameter("@strTownName", strtown);

                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);

                DataTable dttown = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, para);

                
                #region Modify 17Jan2011-Don't reload cboChannel, cboRegion. Page always load these values
                //load data on cbochannel
                /*
                //load data on cbochannel
                if (string.IsNullOrEmpty(strchannel))
                {
                    DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);
                    foreach (DataRow row in dtchannel.Rows)
                    {
                        string strchannelname = row["CHANNEL_NAME"].ToString();
                        string strchannelcode = row["CHANNEL_CODE"].ToString();

                        if (string.IsNullOrEmpty(strchannelname))
                        {
                            row["CHANNEL_NAME"] = strchannelcode;
                        }
                    }
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
               */
                //Modify 17Jan2011
                //Don't reload cboRegion, alway load these region
                /*
                 if (string.IsNullOrEmpty(strregion))
                 {
                     //load data on cboregion
                     dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                     cboRegion.ValueMember = "REGION_NAME";
                     cboRegion.DisplayMember = "REGION_NAME";
                     cboRegion.DataSource = dtregion;
                 }
                 else
                 {
                     DataRow[] rows = dtregion.Select();
                     int countcboregion = cboRegion.Items.Count-1;
                     int countrows = rows.Length;
                     if (rows.Length>0)
                     {
                         if (countcboregion!= countrows)
                         {
                             //load data on cboregion
                             int count = dtregion.Rows.Count + 1;
                             dtregion.Rows.InsertAt(dtregion.NewRow(), count);
                             cboRegion.ValueMember = "REGION_NAME";
                             cboRegion.DisplayMember = "REGION_NAME";
                             cboRegion.DataSource = dtregion;
                         } 
                     } 
                 }
                */
                #endregion
               
                if (string.IsNullOrEmpty(strprovince))
                {
                    //load data on cboprovince
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovince;
                }
                else
                {
                    DataRow[] rows = dtprovince.Select();
                    int countcboprovince = cboProvince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (rows.Length > 0)
                    {
                        if (countcboprovince != countrows)
                        {
                            //load data on cboregion
                            int count = dtregion.Rows.Count + 1;
                            dtprovince.Rows.InsertAt(dtprovince.NewRow(),count);
                            cboProvince.ValueMember = "PROVINCE_NAME";
                            cboProvince.DisplayMember = "PROVINCE_NAME";
                            cboProvince.DataSource = dtprovince;

                        }
                    } 
                }

                
                if (string.IsNullOrEmpty(strtown))
                {
                    //load data on cbotown
                    dttown.Rows.InsertAt(dttown.NewRow(), 0);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttown;
                }
                else
                {

                    DataRow[] rows = dttown.Select();
                    int countcbotown = cboTown.Items.Count - 1;
                    int countrows = rows.Length;
                    if (rows.Length > 0)
                    {
                        if (countcbotown != countrows)
                        {
                            //load data on cbotown
                            int count = dtregion.Rows.Count + 1;
                            dttown.Rows.InsertAt(dttown.NewRow(), count);
                            cboTown.ValueMember = "TOWN_NAME";
                            cboTown.DisplayMember = "TOWN_NAME";
                            cboTown.DataSource = dttown;

                        }
                    } 
                }

                /*
                 //check all region ,province, town is null
                if (string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && !string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dttownReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqltown, paraReload);

                    int count = dttownReload.Rows.Count + 1;
                    dttownReload.Rows.InsertAt(dttownReload.NewRow(), count);
                    cboTown.ValueMember = "TOWN_NAME";
                    cboTown.DisplayMember = "TOWN_NAME";
                    cboTown.DataSource = dttownReload;
                }

                //check all region ,province, town is null
                if (string.IsNullOrEmpty(strchannel) && string.IsNullOrEmpty(strregion) && !string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtprovinceReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, paraReload);

                    int count = dtprovinceReload.Rows.Count + 1;
                    dtprovinceReload.Rows.InsertAt(dtprovinceReload.NewRow(), count);
                    cboProvince.ValueMember = "PROVINCE_NAME";
                    cboProvince.DisplayMember = "PROVINCE_NAME";
                    cboProvince.DataSource = dtprovinceReload;
                }

                if (string.IsNullOrEmpty(strchannel) && !string.IsNullOrEmpty(strregion) && string.IsNullOrEmpty(strprovince) && string.IsNullOrEmpty(strtown))
                {
                    SqlParameter[] paraReload = new SqlParameter[4];
                    paraReload[0] = new SqlParameter("@strChannelCode", string.Empty);
                    paraReload[1] = new SqlParameter("@strRegionName", string.Empty);
                    paraReload[2] = new SqlParameter("@strProvinceName", string.Empty);
                    paraReload[3] = new SqlParameter("@strTownName", string.Empty);

                    DataTable dtregionReload = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, paraReload);

                    int count = dtregionReload.Rows.Count + 1;
                    dtregionReload.Rows.InsertAt(dtregionReload.NewRow(), count);
                    cboRegion.ValueMember = "REGION_NAME";
                    cboRegion.DisplayMember = "REGION_NAME";
                    cboRegion.DataSource = dtregionReload;
                }*/
                
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        #endregion 


        /// <summary>
        /// Create date 20100729
        /// load combobox when select on Distributor screen
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboregion"></param>
        /// <param name="cboprovince"></param>
        /// <param name="cbosubprovince"></param>
        public void GetAllChannelRegionrovinceSubProvinceFromDistributor(ComboBox cbochannel,ComboBox cboregion,ComboBox cboprovince,ComboBox cbosubprovince)
        {
            try
            {
                string sqlchannel = "p_FPT_ENV_GetAllChannelFromDistributor";
                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, null);

                dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                cbochannel.ValueMember = "CHANNEL_CODE";
                cbochannel.DisplayMember = "CHANNEL_NAME";
                cbochannel.DataSource = dtchannel;

                string sqlregion = "p_FPT_ENV_GetAllRegionFromDistributor";
                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, null);

                dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                cboregion.ValueMember = "REGION_CODE";
                cboregion.DisplayMember = "REGION_NAME";
                cboregion.DataSource = dtregion;


                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromDistributor";
                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, null);

                dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                cboprovince.ValueMember = "PROVINCE_CODE";
                cboprovince.DisplayMember = "PROVINCE_NAME";
                cboprovince.DataSource = dtprovince;

                string sqlsubprovince = "p_FPT_ENV_GetAllSubProvinceFromDistributor";
                DataTable dtsubprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlsubprovince, null);

                dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), 0);
                cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                cbosubprovince.DataSource = dtsubprovince;

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboChannel
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboregion"></param>
        /// <param name="cboprovince"></param>
        /// <param name="cbosubprovince"></param>
        public void GetAllChannelFromDistributorFilter(ComboBox cbochannel, ComboBox cboregion, ComboBox cboprovince, ComboBox cbosubprovince)
        {
            try
            {
                string strchannel = cbochannel.Text.ToString();
                string strregion = cboregion.Text.ToString();
                string strprovince = cboprovince.Text.ToString();
                string strsubprovince = cbosubprovince.Text.ToString();

                SqlParameter[] para = new SqlParameter[4];

                para[0] = new SqlParameter("@strChannel", strchannel);
                para[1] = new SqlParameter("@strRegion", strregion);
                para[2] = new SqlParameter("@strProvince", strprovince);
                para[3] = new SqlParameter("@strSubProvince", strsubprovince);

                string sqlchannel = "p_FPT_ENV_GetAllChannelFromDistributorFilter";
               

                string sqlregion = "p_FPT_ENV_GetAllRegionFromDistributorFilter";
                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromDistributorFilter";
                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);

                string sqlsubprovince = "p_FPT_ENV_GetAllSubProvinceFromDistributorFilter";
                DataTable dtsubprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlsubprovince, para);

                //load data on cbochannel

                if (string.IsNullOrEmpty(strchannel))
                {
                    DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);

                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;

                }

                if (string.IsNullOrEmpty(strregion))
                {
                    //load data on cboregion
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboregion.ValueMember = "REGION_CODE";
                    cboregion.DisplayMember = "REGION_NAME";
                    cboregion.DataSource = dtregion;

                }
                else
                {
                    DataRow[] rows = dtregion.Select();
                    int countcboregion = cboregion.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboregion!=countrows)
                    {
                        int count = dtregion.Rows.Count + 1;
                        //load data on cboregion
                        dtregion.Rows.InsertAt(dtregion.NewRow(), count);
                        cboregion.ValueMember = "REGION_CODE";
                        cboregion.DisplayMember = "REGION_NAME";
                        cboregion.DataSource = dtregion;

                    }
                }

                if (string.IsNullOrEmpty(strprovince))
                {
                    //load data on cboprovince
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboprovince.ValueMember = "PROVINCE_CODE";
                    cboprovince.DisplayMember = "PROVINCE_NAME";
                    cboprovince.DataSource = dtprovince;

                }
                else
                {
                    DataRow[] rows = dtprovince.Select();
                    int countcboprovince = cboprovince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboprovince != countrows)
                    {
                        //load data on cboprovince
                        int count = dtprovince.Rows.Count + 1;
                        dtprovince.Rows.InsertAt(dtprovince.NewRow(), count);
                        cboprovince.ValueMember = "PROVINCE_CODE";
                        cboprovince.DisplayMember = "PROVINCE_NAME";
                        cboprovince.DataSource = dtprovince;

                    }
                }

                if (string.IsNullOrEmpty(strsubprovince))
                {
                    //load data on cbosubprovince
                    dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), 0);
                    cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                    cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                    cbosubprovince.DataSource = dtsubprovince;

                }
                else
                {
                    DataRow[] rows = dtsubprovince.Select();
                    int countcbosupprovince = cbosubprovince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbosupprovince != countrows)
                    {
                        int count = dtsubprovince.Rows.Count + 1;
                        //load data on cbosubprovince
                        dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), count);
                        cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                        cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                        cbosubprovince.DataSource = dtsubprovince;

                    }
                }

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboRegion
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboregion"></param>
        /// <param name="cboprovince"></param>
        /// <param name="cbosubprovince"></param>
        public void GetAllRegionFromDistributorFilter(ComboBox cbochannel, ComboBox cboregion, ComboBox cboprovince, ComboBox cbosubprovince)
        {
            try
            {
                string strchannel = cbochannel.Text.ToString();
                string strregion = cboregion.Text.ToString();
                string strprovince = cboprovince.Text.ToString();
                string strsubprovince = cbosubprovince.Text.ToString();

                SqlParameter[] para = new SqlParameter[4];

                para[0] = new SqlParameter("@strChannel", strchannel);
                para[1] = new SqlParameter("@strRegion", strregion);
                para[2] = new SqlParameter("@strProvince", strprovince);
                para[3] = new SqlParameter("@strSubProvince", strsubprovince);

                string sqlchannel = "p_FPT_ENV_GetAllChannelFromDistributorFilter";
                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);

                string sqlregion = "p_FPT_ENV_GetAllRegionFromDistributorFilter";
                //DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromDistributorFilter";
                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);

                string sqlsubprovince = "p_FPT_ENV_GetAllSubProvinceFromDistributorFilter";
                DataTable dtsubprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlsubprovince, para);

                //load data on cbochannel

                if (string.IsNullOrEmpty(strchannel))
                {
                   
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
                else
                {
                    DataRow[] rows = dtchannel.Select();
                    int countcbochannel = cbochannel.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbochannel!=countrows)
                    {
                        int count = dtchannel.Rows.Count + 1;
                        dtchannel.Rows.InsertAt(dtchannel.NewRow(), count);
                        cbochannel.ValueMember = "CHANNEL_CODE";
                        cbochannel.DisplayMember = "CHANNEL_NAME";
                        cbochannel.DataSource = dtchannel;
                    }
                    
                }

                //load data on cboregion
                if (string.IsNullOrEmpty(strregion))
                {
                    DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboregion.ValueMember = "REGION_NAME";
                    cboregion.DisplayMember = "REGION_NAME";
                    cboregion.DataSource = dtregion;
                }


                if (string.IsNullOrEmpty(strprovince))
                {
                    //load data on cboprovince

                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboprovince.ValueMember = "PROVINCE_NAME";
                    cboprovince.DisplayMember = "PROVINCE_NAME";
                    cboprovince.DataSource = dtprovince;

                }
                else
                {
                    DataRow[] rows = dtprovince.Select();
                    int countcboprovince = cboprovince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboprovince!=countrows)
                    {
                        int count = dtprovince.Rows.Count + 1;
                        dtprovince.Rows.InsertAt(dtprovince.NewRow(), count);
                        cboprovince.ValueMember = "PROVINCE_NAME";
                        cboprovince.DisplayMember = "PROVINCE_NAME";
                        cboprovince.DataSource = dtprovince;
                    }
                }


                if (string.IsNullOrEmpty(strsubprovince))
                {
                    //load data on cbo sub province

                    dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), 0);
                    cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                    cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                    cbosubprovince.DataSource = dtsubprovince;
                }
                else
                {
                    DataRow[] rows = dtsubprovince.Select();
                    int countcbosupprovince = cbosubprovince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbosupprovince!=countrows)
                    {
                        int count = dtsubprovince.Rows.Count + 1;
                        dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), count);
                        cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                        cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                        cbosubprovince.DataSource = dtsubprovince;
                    }
                }

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboProvince
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboregion"></param>
        /// <param name="cboprovince"></param>
        /// <param name="cbosubprovince"></param>
        public void GetAllProvinceFromDistributorFilter(ComboBox cbochannel, ComboBox cboregion, ComboBox cboprovince, ComboBox cbosubprovince)
        {
            try
            {
                string strchannel = cbochannel.Text.ToString();
                string strregion = cboregion.Text.ToString();
                string strprovince = cboprovince.Text.ToString();
                string strsubprovince = cbosubprovince.Text.ToString();

                SqlParameter[] para = new SqlParameter[4];

                para[0] = new SqlParameter("@strChannel", strchannel);
                para[1] = new SqlParameter("@strRegion", strregion);
                para[2] = new SqlParameter("@strProvince", strprovince);
                para[3] = new SqlParameter("@strSubProvince", strsubprovince);

                string sqlchannel = "p_FPT_ENV_GetAllChannelFromDistributorFilter";
                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);

                string sqlregion = "p_FPT_ENV_GetAllRegionFromDistributorFilter";
                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromDistributorFilter";
                //DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);

                string sqlsubprovince = "p_FPT_ENV_GetAllSubProvinceFromDistributorFilter";
                DataTable dtsubprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlsubprovince, para);

                //load data on cbochannel

                if (string.IsNullOrEmpty(strchannel))
                {
                    
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
                else
                {
                    DataRow[] rows = dtchannel.Select();
                    int countcbochannel = cbochannel.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbochannel!=countrows)
                    {
                        int count = dtchannel.Rows.Count + 1;
                        dtchannel.Rows.InsertAt(dtchannel.NewRow(), count);
                        cbochannel.ValueMember = "CHANNEL_CODE";
                        cbochannel.DisplayMember = "CHANNEL_NAME";
                        cbochannel.DataSource = dtchannel;
                    }
                   
                }

                //load data on cboregion

                if (string.IsNullOrEmpty(strregion))
                {
                   
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboregion.ValueMember = "REGION_NAME";
                    cboregion.DisplayMember = "REGION_NAME";
                    cboregion.DataSource = dtregion;
                }
                else
                {
                    DataRow[] rows = dtregion.Select();
                    int countcboregion = cboregion.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboregion != countrows)
                    {
                        int count = dtregion.Rows.Count + 1;
                        dtregion.Rows.InsertAt(dtregion.NewRow(), count);
                        cboregion.ValueMember = "REGION_NAME";
                        cboregion.DisplayMember = "REGION_NAME";
                        cboregion.DataSource = dtregion;
                    }
                   
                }

                //load data on cboprovince
                if (string.IsNullOrEmpty(strprovince))
                {
                    DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboprovince.ValueMember = "PROVINCE_NAME";
                    cboprovince.DisplayMember = "PROVINCE_NAME";
                    cboprovince.DataSource = dtprovince;

                }

                if (string.IsNullOrEmpty(strsubprovince))
                {
                    //load data on cbo subprovince

                    dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), 0);
                    cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                    cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                    cbosubprovince.DataSource = dtsubprovince;
                }
                else
                {
                    DataRow[] rows = dtsubprovince.Select();
                    int countcbosupprovince = cbosubprovince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbosupprovince!=countrows)
                    {
                        int count = dtsubprovince.Rows.Count + 1;
                        dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), count);
                        cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                        cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                        cbosubprovince.DataSource = dtsubprovince;
                    }
                }

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// Create date 20100729
        /// Reload combobox when select cboSubProvince
        /// </summary>
        /// <param name="cbochannel"></param>
        /// <param name="cboregion"></param>
        /// <param name="cboprovince"></param>
        /// <param name="cbosubprovince"></param>
        public void GetAllSubProvinceFromDistributorFilter(ComboBox cbochannel, ComboBox cboregion, ComboBox cboprovince, ComboBox cbosubprovince)
        {
            try
            {
                string strchannel = cbochannel.Text.ToString();
                string strregion = cboregion.Text.ToString();
                string strprovince = cboprovince.Text.ToString();
                string strsubprovince = cbosubprovince.Text.ToString();
                SqlParameter[] para = new SqlParameter[4];

                para[0] = new SqlParameter("@strChannel", strchannel);
                para[1] = new SqlParameter("@strRegion", strregion);
                para[2] = new SqlParameter("@strProvince", strprovince);
                para[3] = new SqlParameter("@strSubProvince", strsubprovince);

                string sqlchannel = "p_FPT_ENV_GetAllChannelFromDistributorFilter";
                DataTable dtchannel = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlchannel, para);

                string sqlregion = "p_FPT_ENV_GetAllRegionFromDistributorFilter";
                DataTable dtregion = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlregion, para);

                string sqlprovince = "p_FPT_ENV_GetAllProvinceFromDistributorFilter";
                DataTable dtprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlprovince, para);

                string sqlsubprovince = "p_FPT_ENV_GetAllSubProvinceFromDistributorFilter";
                //DataTable dtsubprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlsubprovince, para);

                //load data on cbochannel

                if (string.IsNullOrEmpty(strchannel))
                {
                    
                    dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                    cbochannel.ValueMember = "CHANNEL_CODE";
                    cbochannel.DisplayMember = "CHANNEL_NAME";
                    cbochannel.DataSource = dtchannel;
                }
                else
                {
                    DataRow[] rows = dtchannel.Select();
                    int countcbochannel = cbochannel.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcbochannel!=countrows)
                    {
                        int count = dtchannel.Rows.Count + 1;
                        dtchannel.Rows.InsertAt(dtchannel.NewRow(), 0);
                        cbochannel.ValueMember = "CHANNEL_CODE";
                        cbochannel.DisplayMember = "CHANNEL_NAME";
                        cbochannel.DataSource = dtchannel;
                    }
                   
                }

                //load data on cboregion

                if (string.IsNullOrEmpty(strregion))
                {
                    dtregion.Rows.InsertAt(dtregion.NewRow(), 0);
                    cboregion.ValueMember = "REGION_NAME";
                    cboregion.DisplayMember = "REGION_NAME";
                    cboregion.DataSource = dtregion;
                }
                else
                {
                    DataRow[] rows = dtregion.Select();
                    int countcboregion = cboregion.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboregion != countrows)
                    {
                        int count = dtregion.Rows.Count + 1;
                        dtregion.Rows.InsertAt(dtregion.NewRow(), count);
                        cboregion.ValueMember = "REGION_NAME";
                        cboregion.DisplayMember = "REGION_NAME";
                        cboregion.DataSource = dtregion;
                    }

                }

                //load data on cboprovince

                if (string.IsNullOrEmpty(strprovince))
                {
                    dtprovince.Rows.InsertAt(dtprovince.NewRow(), 0);
                    cboprovince.ValueMember = "PROVINCE_NAME";
                    cboprovince.DisplayMember = "PROVINCE_NAME";
                    cboprovince.DataSource = dtprovince;
                }
                else
                {
                    DataRow[] rows = dtprovince.Select();
                    int countcboprovince = cboprovince.Items.Count - 1;
                    int countrows = rows.Length;
                    if (countcboprovince != countrows)
                    {
                        int count = dtprovince.Rows.Count + 1;
                        dtprovince.Rows.InsertAt(dtprovince.NewRow(), count);
                        cboprovince.ValueMember = "PROVINCE_NAME";
                        cboprovince.DisplayMember = "PROVINCE_NAME";
                        cboprovince.DataSource = dtprovince;
                    }
                  
                }

                //load data on cbotown
                if (string.IsNullOrEmpty(strsubprovince))
                {
                    DataTable dtsubprovince = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sqlsubprovince, para);

                    dtsubprovince.Rows.InsertAt(dtsubprovince.NewRow(), 0);
                    cbosubprovince.ValueMember = "SUB_PROVINCE_CODE";
                    cbosubprovince.DisplayMember = "SUB_PROVINCE_NAME";
                    cbosubprovince.DataSource = dtsubprovince;

                }

            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        /// <summary>
        /// DongTC
        /// check Sale Sup exist 
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public bool CheckEmPloyeeFromSalesForce(string saleId)
        {
            try
            {
                string sql = "p_FPT_ENV_CheckEmployeeIDFromSALES_FORCE";
                SqlParameter[]para=new SqlParameter[1];
                para[0] = new SqlParameter("@strSalesSupId", saleId);
                DataTable dt = DataServices.ExecuteDataTable(CommandType.StoredProcedure, sql, para);

                if (dt.Rows.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

      /// <summary>
      /// check data type Numeric
      /// </summary>
      /// <param name="value"></param>
      /// <param name="mainLen"></param>
      /// <returns></returns>
        public bool CheckNumeric(string value, int mainLen)
        {
            bool result = true;
            double temp;
            if (!string.IsNullOrEmpty(value))
            {
                result = double.TryParse(value, out temp);
                if (result)
                {
                    if (temp < 0)
                    {
                        return false;
                    }

                    string strNum = temp.ToString("#0.000");

                    string[] parts = strNum.Split('.');
                    if (parts[0].Length > mainLen)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                result = false;
            }
            return result;

        }
    }

}
