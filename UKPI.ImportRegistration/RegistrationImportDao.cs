using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKPI.Core;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace UKPI.ImportRegistration
{
    public class RegistrationImportDao : DataAccess
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(RegistrationImportDao));

        #region Tables
        public const string TAB_MARKETING_PROGRAM = "FPT_ENV_MARKETING_PROGRAM";
        public const string TAB_DISPLAY_SET = "FPT_ENV_DISPLAY_SET";
        public const string TAB_DISPLAY_SET_SHOP_FORMAT = "FPT_ENV_DISPLAY_SET_SHOP_FORMAT";
        public const string TAB_BASIC_EXTRA_SET = "FPT_ENV_BASIC_EXTRA_DISPLAY_SET";
        public const string TAB_REGISTRATION_STORES = "FPT_ENV_REGISTERED_DISPLAY_STORES";
        #endregion Tables

        #region Column names
        public const string COL_PROGRAMCODE = "ProgramCode";
        public const string COL_PROGRAMTYPE = "ProgramType";
        public const string COL_STORECODE = "StoreCode";
        public const string COL_YEAR = "Year";
        public const string COL_MONTH = "Month";
        public const string COL_MAXREGPERBASICSET = "MaxRegPerBasicSet";
        public const string COL_MAXREGPEREXTRASET = "MaxRegPerExtraSet";
        public const string COL_MAXBASICSET = "MaxBasicSet";
        public const string COL_MAXEXTRASET = "MaxExtraSet";
        public const string COL_MODIFIED = "Modified";
        public const string COL_ISSENT = "IsSent";
        public const string COL_CREATEDBY = "CreatedBy";
        public const string COL_CREATEDON = "CreatedOn";
        public const string COL_UPDATEDBY = "UpdatedBy";
        public const string COL_UPDATEDON = "UpdatedOn";

        public const string COL_DISPLAYSETCODE = "DisplaySetCode";
        public const string COL_DISPLAYSETNAME = "DisplaySetName";
        public const string COL_DESCRIPTION = "Description";
        public const string COL_ICONIC = "Iconic";
        public const string COL_EXTRA = "Extra";
        public const string COL_ACTIVE = "Active";
        public const string COL_TOLIMIT = "TOLimit";
        public const string COL_INCENTIVEVALUE = "IncentiveValue";

        public const string COL_SHOPFORMAT = "ShopFormat";

        public const string COL_BASICSETCODE = "BasicSetCode";
        public const string COL_EXTRASETCODE = "ExtraSetCode";
        public const string COL_SOURCE_COLUMN = "SourceColumn";
        public const string COL_SINK_COLUMN = "DestinationColumn";
        public const string COL_DATATYPE = "DataType";
        #endregion Column names

        #region Stored Procedures
        public const string SP_LOAD_REGISTRATION_RULES = "p_FPT_ENV_LOAD_REGISTRATION_RULES";
        public const string SP_LOAD_REGISTRATION_RULES_P1 = "ProgramCode";

        public const string SP_GET_STORE_SHOP_FORMAT = "p_FPT_ENV_GET_STORE_SHOP_FORMAT";
        public const string SP_GET_STORE_SHOP_FORMAT_P1 = "STORE_CODES";
        public const string SP_GET_STORE_SHOP_FORMAT_R1 = "STORE_CODE";
        public const string SP_GET_STORE_SHOP_FORMAT_R2 = "SHOP_FORMAT";

        public const string SP_GET_NOT_EXIST_STORES = "p_FPT_ENV_GET_NOT_EXISTED_STORES";
        public const string SP_GET_NOT_EXIST_STORES_P1 = "STORE_CODES";
        public const string SP_GET_NOT_EXIST_STORES_R1 = "STORE_CODE";

        public const string SP_UPDATE_REGISTRATION_STORE = "p_FPT_ENV_UPDATE_REGISTRATION_STORE";

        public const string SP_SEARCH_LOG = "p_FPT_ENV_SEARCH_REGISTRATION_IMPORT_LOG";
        public const string SP_SEARCH_LOG_P1 = "FromDate";
        public const string SP_SEARCH_LOG_P2 = "ToDate";
        public const string SP_SEARCH_LOG_P3 = "FileName";
        #endregion Stored Procedures

        public const string DB_LIST_SEPERATOR = ";";
        public const string DB_FIELD_SEPERATOR = ",";
        public const int DB_SP_MAX_PARAM_LENGTH = 4000;


        public RegistrationImportDao(string connectionString)
            : base(connectionString)
        {
        }

        public void UpdateRegistration(DataTable table, string mappingFile)
        {
            List<SqlParameter[]> prs = BuildParameter(table, TAB_REGISTRATION_STORES, mappingFile, COL_SOURCE_COLUMN, COL_SINK_COLUMN, COL_DATATYPE);
            foreach (SqlParameter[] item in prs)
            {
                try
                {
                    ExecuteNonQuery(CommandType.StoredProcedure, SP_UPDATE_REGISTRATION_STORE, item);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        public DataSet SearchLog(RegistrationLogSE seachEntity)
        {
            try
            {
                SqlParameter[] prs = new SqlParameter[3];
                prs[0] = new SqlParameter(SP_SEARCH_LOG_P1, seachEntity.FromDate);
                prs[1] = new SqlParameter(SP_SEARCH_LOG_P2, seachEntity.ToDate);
                prs[2] = new SqlParameter(SP_SEARCH_LOG_P3, seachEntity.FileName);
                DataSet tmp = ExecuteDataSet(CommandType.StoredProcedure, SP_SEARCH_LOG, prs);
                DataTable tableNames = tmp.Tables[0];
                for (int i = 0; i < tableNames.Rows.Count; i++)
                {
                    tmp.Tables[i].TableName = tableNames.Rows[i][0].ToString();
                }
                tmp.Tables.Remove(tableNames.Rows[0][0].ToString());
                return tmp;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public string GetParameterValue(string param_Name)
        {
            string cmdText = "SELECT Param_Value FROM FPT_ENV_Parameters WHERE Param_Name=' "+ param_Name+"'";

            try
            {
                return ExecuteScalar(CommandType.Text, cmdText).ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return string.Empty;
            }
        }

        public DataSet LoadRegistrationRules(string programCode)
        {
            try
            {
                SqlParameter p1 = new SqlParameter(SP_LOAD_REGISTRATION_RULES_P1, programCode);
                DataSet tmp = ExecuteDataSet(CommandType.StoredProcedure, SP_LOAD_REGISTRATION_RULES, new System.Data.SqlClient.SqlParameter[] { p1 });
                DataTable tableNames = tmp.Tables[0];
                for (int i = 0; i < tableNames.Rows.Count; i++)
                {
                    tmp.Tables[i].TableName = tableNames.Rows[i][0].ToString();
                }
                tmp.Tables.Remove(tableNames.Rows[0][0].ToString());
                return tmp;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public List<string> GetNotExistStores(List<string> storeCollection)
        {
            try
            {
                List<string> result = new List<string>();
                List<string> pValues = BuildSqlParamStrings(storeCollection);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_GET_NOT_EXIST_STORES_P1, pValue);
                    parameters.Add(p1);
                    DataTable mapping = ExecuteDataTable(CommandType.StoredProcedure, SP_GET_NOT_EXIST_STORES, parameters.ToArray());
                    if (mapping != null)
                    {
                        foreach (DataRow row in mapping.Rows)
                        {
                            string storeId = string.Empty;
                            object val = row[SP_GET_NOT_EXIST_STORES_R1];
                            if (val != null)
                                storeId = val.ToString().ToUpper();
                            if (!string.IsNullOrEmpty(storeId) && !result.Contains(storeId))
                                result.Add(storeId);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public Dictionary<string, string> GetShopFormatMapping(List<string> storeIdColelction)
        {
            try
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                List<string> pValues = BuildSqlParamStrings(storeIdColelction);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_GET_STORE_SHOP_FORMAT_P1, pValue);
                    parameters.Add(p1);
                    DataTable mapping = ExecuteDataTable(CommandType.StoredProcedure, SP_GET_STORE_SHOP_FORMAT, parameters.ToArray());
                    if (mapping != null)
                    {
                        foreach (DataRow row in mapping.Rows)
                        {
                            string storeId = string.Empty;
                            string shopFormat = string.Empty;
                            object val = row[SP_GET_STORE_SHOP_FORMAT_R1];
                            if (val != null)
                                storeId = val.ToString().ToUpper();
                            val = row[SP_GET_STORE_SHOP_FORMAT_R2];
                            if (val != null)
                                shopFormat = val.ToString().ToUpper();
                            if (!string.IsNullOrEmpty(storeId) && !result.ContainsKey(storeId))
                                result.Add(storeId, shopFormat);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }
    }

    public class RegistrationLogSE
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FileName { get; set; }

        public RegistrationLogSE()
        {
            FromDate = SqlDateTime.MinValue.Value;
            ToDate = SqlDateTime.MaxValue.Value;
            FileName = string.Empty;
        }
    }
}
