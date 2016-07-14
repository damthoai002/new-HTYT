using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FPT.Component.ExcelPlus;
using UKPI.Core;

namespace UKPI.AuditResult
{
    public class AuditResultDao : DataAccess
    {
        public const string COL_PERIOD = "PERIOD";
        public const string COL_STORE_ID = "STORE_ID";
        public const string COL_STORE_NAME = "STORE_NAME";
        public const string COL_AUDIT_DATE = "AUDIT_DATE";
        public const string COL_PS_COMP = "PS_COMPL";
        public const string COL_OSA_COMP = "OSA_COMPL";
        public const string COL_NPD_COMP = "NPD_COMPL";
        public const string COL_SOS_COMP = "SOS_COMPL";
        public const string COL_SHELF_STD_COMP = "SHELF_STD_COMPL";
        public const string COL_SOF_COMP = "SOF_COMPL";
        public const string COL_PROMO_COMP = "PRO_COMPL";
        public const string COL_CTA_COMP = "CTA_COMPL";
        public const string COL_SHOP_NO = "SHOP_NO";
        public const string COL_MARKET = "MARKET";
        public const string COL_STREET_NO = "STREET_NO";
        public const string COL_STREET_NAME = "STREET_NAME";
        public const string COL_WARD = "WARD";
        public const string COL_TOWN = "TOWN";
        public const string COL_CITY = "CITY";
        public const string COL_PROVINCE = "PROVINCE";
        public const string COL_LATITUDE = "LATITUDE";
        public const string COL_LONGITUDE = "LONGITUDE";
        public const string COL_AUDIT_STATUS = "AUDIT_STATUS";
        public const string COL_AUDIT_RESULT = "AUDIT_RESULT";
        public const string COL_DISPLAY_SET = "DISPLAY_SET";
        public const string COL_CATEGORY_ID = "CATEGORY_ID";
        public const string COL_CATEGORY_NAME = "CATEGORY_NAME";
        public const string COL_RESULT = "RESULT";
        public const string TAB_AUDIT_RESULT_INFO = "FPT_ENV_AUDIT_RESULT_INFORMATION";
        public const string TAB_AUDIT_RESULT_DETAIL = "FPT_ENV_AUDIT_RESULT_DETAIL";
        public const string SP_GET_INVALID_AUDIT_STORE = "p_FPT_ENV_GET_INVALID_AUDIT_STORE_NO_RC";
        public const string SP_GET_INVALID_AUDIT_STORE_P1 = "STORE_CODES";
        public const string SP_GET_INVALID_AUDIT_STORE_P2 = "PERIOD";
        public const string SP_GET_INVALID_AUDIT_STORE_R1 = "STORE_CODE";

        public const string SP_DELETE_EXISTED_AUDIT_STORE = "p_FPT_ENV_DELETE_EXIST_AUDIT_RESULT_INFO";
        public const string SP_DELETE_EXISTED_AUDIT_STORE_P1 = "STORE_CODES";
        public const string SP_DELETE_EXISTED_AUDIT_STORE_P2 = "PERIOD";

        public const string SP_GET_INVALID_STORE_DISPLAYSET = "p_FPT_ENV_GET_INVALID_STORE_DISPLAYSET";
        public const string SP_GET_INVALID_STORE_DISPLAYSET_P1 = "STORE_DISPLAYSET";
        public const string SP_GET_INVALID_STORE_DISPLAYSET_P2 = "PERIOD";
        public const string SP_GET_INVALID_STORE_DISPLAYSET_R1 = "STORE_CODE";
        public const string SP_GET_INVALID_STORE_DISPLAYSET_R2 = "DISPLAYSET";

        public const string SP_GET_INVALID_AUDIT_CATEGORY = "p_FPT_ENV_GET_INVALID_AUDIT_CATEGORY";
        public const string SP_GET_INVALID_AUDIT_CATEGORY_P1 = "STORE_CATEGORY";
        public const string SP_GET_INVALID_AUDIT_CATEGORY_P2 = "PERIOD";
        public const string SP_GET_INVALID_AUDIT_CATEGORY_R1 = "STORE_CODE";
        public const string SP_GET_INVALID_AUDIT_CATEGORY_R2 = "CATEGORY";

        public const string SP_DELETE_EXISTED_AUDIT_DETAIL = "p_FPT_ENV_DELETE_EXIST_AUDIT_RESULT_DETAIL";
        public const string SP_DELETE_EXISTED_AUDIT_DETAIL_P1 = "AUDIT_DETAILS";
        public const string SP_DELETE_EXISTED_AUDIT_DETAIL_P2 = "PERIOD";

        public const string SP_UPDATE_STORE_ADDRESS = "p_FPT_ENV_UPDATE_STORE_ADDRESS";

        public const string COL_RESULT_CODE = "RESULT_CODE";
        public const string COL_RESULT_NAME = "RESULT_NAME";
        public const string COL_AUDIT_EVALUATION = "AUDIT_EVALUATION";
        public const string TAB_RESULT_MAPPING = "FPT_ENV_AUDIT_RESULT_MAPPING";
        public const string DB_LIST_SEPERATOR = ";";
        public const string DB_FIELD_SEPERATOR = ",";
        public const int DB_SP_MAX_PARAM_LENGTH = 4000;

        public int MaxParamLength { get; set; }

        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuditResultDao));

        public AuditResultDao(string connectionString)
            : base(connectionString)
        {
            MaxParamLength = DB_SP_MAX_PARAM_LENGTH;
        }

        public IList<AuditResultMapping> GetAuditResultMapping()
        {
            List<AuditResultMapping> result = new List<AuditResultMapping>();

            string cmdText = string.Format("SELECT {0}, {1}, {2} FROM [{3}]", new object[] { COL_RESULT_CODE, COL_RESULT_NAME, COL_AUDIT_EVALUATION, TAB_RESULT_MAPPING });
            System.Data.DataTable table = ExecuteDataTable(CommandType.Text, cmdText, new SqlParameter[0]);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    AuditResultMapping m = new AuditResultMapping();
                    m.ResultCode = row[COL_RESULT_CODE].ToString();
                    m.ResultName = row[COL_RESULT_NAME].ToString();
                    m.Evaludation = TryParse(row[COL_AUDIT_EVALUATION]);
                    result.Add(m);
                }
            }

            return result;
        }

        private int TryParse(object value)
        {
            try
            {
                return int.Parse(value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public List<string> GetInvalidAuditStoreNoRegionChannel(List<string> storeIdCollection, string period)
        {
            try
            {
                List<string> result = new List<string>();
                List<string> pValues = BuildSqlParamStrings(storeIdCollection);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_GET_INVALID_AUDIT_STORE_P1, pValue);
                    SqlParameter p2 = new SqlParameter(SP_GET_INVALID_AUDIT_STORE_P2, period);
                    parameters.Add(p1);
                    parameters.Add(p2);
                    DataTable invalid = ExecuteDataTable(CommandType.StoredProcedure, SP_GET_INVALID_AUDIT_STORE, parameters.ToArray());
                    if (invalid != null)
                    {
                        foreach (DataRow row in invalid.Rows)
                        {
                            object val = row[SP_GET_INVALID_AUDIT_STORE_R1];
                            if (val != null)
                            {
                                result.Add(val.ToString());
                            }
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

        protected void DeleteExistedAuditDetail(DataTable table, string period)
        {
            try
            {
                List<AuditDetailKey> auditKeyCollection = new List<AuditDetailKey>();
                foreach (DataRow row in table.Rows)
                {
                    AuditDetailKey key = new AuditDetailKey();
                    key.StoreID = row[COL_STORE_ID].ToString();
                    key.Category = row[COL_CATEGORY_ID].ToString();
                    key.DisplaySet = row[COL_DISPLAY_SET].ToString();
                    auditKeyCollection.Add(key);
                }
                List<string> pValues = BuildSqlParamStrings(auditKeyCollection);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_DELETE_EXISTED_AUDIT_DETAIL_P1, pValue);
                    SqlParameter p2 = new SqlParameter(SP_DELETE_EXISTED_AUDIT_DETAIL_P2, period);
                    parameters.Add(p1);
                    parameters.Add(p2);
                    ExecuteNonQuery(CommandType.StoredProcedure, SP_DELETE_EXISTED_AUDIT_DETAIL, parameters.ToArray());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void DeleteExistedAuditStore(List<string> storeIdCollection, string period)
        {
            try
            {
                List<string> pValues = BuildSqlParamStrings(storeIdCollection);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_DELETE_EXISTED_AUDIT_STORE_P1, pValue);
                    SqlParameter p2 = new SqlParameter(SP_DELETE_EXISTED_AUDIT_STORE_P2, period);
                    parameters.Add(p1);
                    parameters.Add(p2);
                    ExecuteNonQuery(CommandType.StoredProcedure, SP_DELETE_EXISTED_AUDIT_STORE, parameters.ToArray());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public DataTable InfoTableSchema
        {
            get
            {
                string cmdText = string.Format("SET FMTONLY ON;SELECT * FROM [{0}];SET FMTONLY OFF;", TAB_AUDIT_RESULT_INFO);
                System.Data.DataTable table = ExecuteDataTable(CommandType.Text, cmdText, new SqlParameter[0]);
                table.TableName = TAB_AUDIT_RESULT_INFO;
                return table;
            }
        }

        public DataTable DetailTableSchema
        {
            get
            {
                string cmdText = string.Format("SET FMTONLY ON;SELECT * FROM [{0}];SET FMTONLY OFF;", TAB_AUDIT_RESULT_DETAIL);
                System.Data.DataTable table = ExecuteDataTable(CommandType.Text, cmdText, new SqlParameter[0]);
                table.TableName = TAB_AUDIT_RESULT_DETAIL;
                return table;
            }
        }

        public List<BinaryId> GetInvalidStoreDisplaySet(List<BinaryId> storeDisplaySets, string period)
        {
            try
            {
                List<BinaryId> result = new List<BinaryId>();
                List<string> pValues = BuildSqlParamStrings(storeDisplaySets);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_GET_INVALID_STORE_DISPLAYSET_P1, pValue);
                    SqlParameter p2 = new SqlParameter(SP_GET_INVALID_STORE_DISPLAYSET_P2, period);
                    
                    parameters.Add(p1);
                    parameters.Add(p2);
                    DataTable invalid = ExecuteDataTable(CommandType.StoredProcedure, SP_GET_INVALID_STORE_DISPLAYSET, parameters.ToArray());
                    if (invalid != null)
                    {
                        foreach (DataRow row in invalid.Rows)
                        {
                            object val1 = row[SP_GET_INVALID_STORE_DISPLAYSET_R1];
                            object val2 = row[SP_GET_INVALID_STORE_DISPLAYSET_R2];
                            string id1 = string.Empty;
                            string id2 = string.Empty;
                            if (val1 != null)
                                id1 = val1.ToString();
                            if (val2 != null)
                                id2 = val2.ToString();
                            result.Add(new BinaryId(id1, id2));
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

        public List<BinaryId> GetInvalidAuditCategory(List<BinaryId> storeCategory, string period)
        {
            try
            {
                List<BinaryId> result = new List<BinaryId>();
                List<string> pValues = BuildSqlParamStrings(storeCategory);
                foreach (string pValue in pValues)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    SqlParameter p1 = new SqlParameter(SP_GET_INVALID_AUDIT_CATEGORY_P1, pValue);
                    SqlParameter p2 = new SqlParameter(SP_GET_INVALID_AUDIT_CATEGORY_P2, period);
                    parameters.Add(p1);
                    parameters.Add(p2);
                    DataTable invalid = ExecuteDataTable(CommandType.StoredProcedure, SP_GET_INVALID_AUDIT_CATEGORY, parameters.ToArray());
                    if (invalid != null)
                    {
                        foreach (DataRow row in invalid.Rows)
                        {
                            object val1 = row[SP_GET_INVALID_AUDIT_CATEGORY_R1];
                            object val2 = row[SP_GET_INVALID_AUDIT_CATEGORY_R2];
                            string id1 = string.Empty;
                            string id2 = string.Empty;
                            if (val1 != null)
                                id1 = val1.ToString();
                            if (val2 != null)
                                id2 = val2.ToString();
                            result.Add(new BinaryId(id1, id2));
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

        public void SaveInfo(DataTable table, string period)
        {
            List<string> storeIdCollection = GetStoreId(table);
            DeleteExistedAuditStore(storeIdCollection, period);
            BulkInsert(table, TAB_AUDIT_RESULT_INFO);
        }

        public void UpdateStoreAddress(string storeID, string address1, string address2, string address3)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];

                sqlParams[0] = new SqlParameter("@STOREID", storeID);
                sqlParams[1] = new SqlParameter("@ADDRESS1", address1);
                sqlParams[2] = new SqlParameter("@ADDRESS2", address2);
                sqlParams[3] = new SqlParameter("@ADDRESS3", address3);

                ExecuteNonQuery(CommandType.StoredProcedure, SP_UPDATE_STORE_ADDRESS, sqlParams);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        private List<string> GetStoreId(DataTable table)
        {
            List<string> result = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                result.Add(row[COL_STORE_ID].ToString());
            }
            return result;
        }

        public void SaveDetail(DataTable table, string period)
        {
            DeleteExistedAuditDetail(table, period);
            BulkInsert(table, TAB_AUDIT_RESULT_DETAIL);
        }

        protected List<string> BuildSqlParamStrings(List<string> items)
        {
            List<string> result = new List<string>();
            string tmp = string.Empty;

            foreach (string item in items)
            {
                if (tmp.Length + item.Length + DB_FIELD_SEPERATOR.Length > MaxParamLength)
                {
                    tmp = tmp.Trim(DB_FIELD_SEPERATOR.ToCharArray());
                    result.Add(tmp);
                    tmp = string.Empty;
                }
                tmp += item + DB_FIELD_SEPERATOR;
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                tmp = tmp.Trim(DB_FIELD_SEPERATOR.ToCharArray());
                result.Add(tmp);
            }

            return result;
        }

        protected List<string> BuildSqlParamStrings(List<BinaryId> binaryIdCollection)
        {
            List<string> result = new List<string>();

            string tmp = string.Empty;
            foreach (BinaryId item in binaryIdCollection)
            {
                if (tmp.Length + BuildSqlString(item).Length + DB_LIST_SEPERATOR.Length > MaxParamLength)
                {
                    tmp = tmp.Trim(DB_LIST_SEPERATOR.ToCharArray());
                    result.Add(tmp);
                    tmp = string.Empty;
                }
                tmp += BuildSqlString(item) + DB_LIST_SEPERATOR;
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                tmp = tmp.Trim(DB_LIST_SEPERATOR.ToCharArray());
                result.Add(tmp);
            }

            return result;
        }

        protected List<string> BuildSqlParamStrings(List<AuditDetailKey> auditKeyCollection)
        {
            List<string> result = new List<string>();

            string tmp = string.Empty;
            foreach (AuditDetailKey item in auditKeyCollection)
            {
                if (tmp.Length + BuildSqlString(item).Length + DB_LIST_SEPERATOR.Length > MaxParamLength)
                {
                    tmp = tmp.Trim(DB_LIST_SEPERATOR.ToCharArray());
                    result.Add(tmp);
                    tmp = string.Empty;
                }
                tmp += BuildSqlString(item) + DB_LIST_SEPERATOR;
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                tmp = tmp.Trim(DB_LIST_SEPERATOR.ToCharArray());
                result.Add(tmp);
            }

            return result;
        }

        protected string BuildSqlString(BinaryId binId)
        {
            string result = binId.ID1;
            result += DB_FIELD_SEPERATOR + binId.ID2;
            return result;
        }

        protected string BuildSqlString(AuditDetailKey key)
        {
            string result = key.StoreID;
            result += DB_FIELD_SEPERATOR + key.Category;
            result += DB_FIELD_SEPERATOR + key.DisplaySet;
            return result;
        }
    }

    public class AuditDetailKey
    {
        public string StoreID { get; set; }
        public string Category { get; set; }
        public string DisplaySet { get; set; }
    }

    public class AuditResultMapping
    {
        public string ResultName { get; set; }
        public int Evaludation { get; set; }
        public string ResultCode { get; set; }
    }

}
