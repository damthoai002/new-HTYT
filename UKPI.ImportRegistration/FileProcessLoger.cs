using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKPI.Core;
using System.Data.SqlClient;
using System.Data;

namespace UKPI.ImportRegistration
{
    public class FileProcessLogger : DataAccess
    {
        public const string SP_CREATE_LOG_HEADER = "p_FPT_ENV_CREATE_FILE_LOG";
        public const string SP_CREATE_LOG_HEADER_P1 = "FileName";
        public const string SP_CREATE_LOG_HEADER_P2 = "TotalRecord";
        public const string SP_CREATE_LOG_HEADER_P3 = "SuccessRecord";
        public const string SP_CREATE_LOG_HEADER_P4 = "Status";
        public const string SP_CREATE_LOG_HEADER_P5 = "Remark";
        public const string SP_CREATE_LOG_HEADER_P6 = "ImportType";
        public const string SP_CREATE_LOG_HEADER_P7 = "ProcessedBy";
        public const string SP_CREATE_LOG_HEADER_P8 = "ProcessedOn";

        public const string SP_CREATE_LOG_DETAIL = "p_FPT_ENV_FILE_LOG_ADD_ITEM";
        public const string SP_CREATE_LOG_DETAIL_P1 = "HeaderID";
        public const string SP_CREATE_LOG_DETAIL_P2 = "ItemName";
        public const string SP_CREATE_LOG_DETAIL_P3 = "Result";
        public const string SP_CREATE_LOG_DETAIL_P4 = "Status";
        public const string SP_CREATE_LOG_DETAIL_P5 = "ProcessedBy";
        public const string SP_CREATE_LOG_DETAIL_P6 = "ProcessedOn";

        public const string SP_CREATE_REG_LOG_DETAIL = "p_FPT_ENV_REGISTERED_STORE_LOG_ADD_ITEM";
        public const string SP_CREATE_REG_LOG_DETAIL_P1 = "HeaderID";
        public const string SP_CREATE_REG_LOG_DETAIL_P2 = "ProgramCode";
        public const string SP_CREATE_REG_LOG_DETAIL_P3 = "StoreCode";
        public const string SP_CREATE_REG_LOG_DETAIL_P4 = "DisplaySetCode";
        public const string SP_CREATE_REG_LOG_DETAIL_P5 = "Result";
        public const string SP_CREATE_REG_LOG_DETAIL_P6 = "Status";
        public const string SP_CREATE_REG_LOG_DETAIL_P7 = "ProcessedBy";
        public const string SP_CREATE_REG_LOG_DETAIL_P8 = "ProcessedOn";

        public const string SP_UPDATE_STATUS_FILE_LOG = "p_FPT_ENV_UPDATE_STATUS_FILE_LOG";
        public const string SP_UPDATE_STATUS_FILE_LOG_P1 = "HeaderID";
        public const string SP_UPDATE_STATUS_FILE_LOG_P2 = "Status";

        public const string SP_GET_EMAIL_INFORMATION = "p_FPT_ENV_GetEmailInformation";
        public const string SP_GET_EMAIL_INFORMATION_P1 = "@StoreCode";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(FileProcessLogger));

        public FileProcessLogger(string connectionString)
            : base(connectionString)
        {
        }

        public void CreateLog<T>(LogInformation<T> logInfo)
        {
            int headerId = CreateLogHeader<T>(logInfo);
            if (headerId > 0)
            {
                foreach (T item in logInfo.DetailCollection)
                {
                    CreateLogDetail<T>(item, headerId);
                }
            }
        }

        public string[] GetEmailInfo(string storeCode)
        {
            string[] result = new string[3] { string.Empty, string.Empty, string.Empty };

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter(SP_GET_EMAIL_INFORMATION_P1, storeCode);

                DataTable dt = this.ExecuteDataTable(CommandType.StoredProcedure, SP_GET_EMAIL_INFORMATION, sqlParams);

                if (dt.Rows.Count > 0)
                {
                    result[0] = dt.Rows[0]["ShipToCode"].ToString();
                    result[1] = dt.Rows[0]["EmailTo"].ToString();
                    result[2] = dt.Rows[0]["EmailCC"].ToString();
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }

            return result;
        }

        public List<ImportRegLogItem> LoadImportLogDetail(int headerLogID)
        {
            try
            {
                List<ImportRegLogItem> result = new List<ImportRegLogItem>();
                //ImportRegLogItem logItem = item as ImportRegLogItem;
                //SqlParameter[] prs = new SqlParameter[8];
                //prs[0] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P1, headerID);
                //prs[1] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P2, logItem.ProgramCode);
                //prs[2] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P3, logItem.StoreCode);
                //prs[3] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P4, logItem.DisplaySetCode);
                //prs[4] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P5, logItem.Result);
                //prs[5] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P6, logItem.Status);
                //prs[6] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P7, logItem.ProcessedBy);
                //prs[7] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P8, logItem.ProcessedOn);
                //object obj = ExecuteScalar(CommandType.StoredProcedure, SP_CREATE_REG_LOG_DETAIL, prs);
                //try
                //{
                //    detailID = int.Parse(obj.ToString());
                //}
                //catch { }
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public int CreateLogHeader<T>(LogInformation<T> logInfo)
        {
            try
            {
                int headerID = -1;
                SqlParameter[] prs = new SqlParameter[8];
                prs[0] = new SqlParameter(SP_CREATE_LOG_HEADER_P1, logInfo.FileName);
                prs[1] = new SqlParameter(SP_CREATE_LOG_HEADER_P2, logInfo.Total);
                prs[2] = new SqlParameter(SP_CREATE_LOG_HEADER_P3, logInfo.Success);
                prs[3] = new SqlParameter(SP_CREATE_LOG_HEADER_P4, logInfo.Status);
                prs[4] = new SqlParameter(SP_CREATE_LOG_HEADER_P5, logInfo.Remark);
                prs[5] = new SqlParameter(SP_CREATE_LOG_HEADER_P6, logInfo.ImportType);
                prs[6] = new SqlParameter(SP_CREATE_LOG_HEADER_P7, logInfo.ProcessedBy);
                prs[7] = new SqlParameter(SP_CREATE_LOG_HEADER_P8, logInfo.ProcessedOn);
                object obj = ExecuteScalar(CommandType.StoredProcedure, SP_CREATE_LOG_HEADER, prs);
                try
                {
                    headerID = int.Parse(obj.ToString());
                }
                catch { }
                return headerID;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return -1;
            }
        }

        public void UpdateHeaderStatus(int headerID, string status)
        {
            try
            {
                SqlParameter[] prs = new SqlParameter[2];
                prs[0] = new SqlParameter(SP_UPDATE_STATUS_FILE_LOG_P1, headerID);
                prs[1] = new SqlParameter(SP_UPDATE_STATUS_FILE_LOG_P2, status);
                ExecuteNonQuery(CommandType.StoredProcedure, SP_UPDATE_STATUS_FILE_LOG, prs);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public int CreateLogDetail<T>(T item, int headerID)
        {
            try
            {
                int detailID = -1;
                if (item is LogDetail)
                {
                    LogDetail logDetail = item as LogDetail;
                    SqlParameter[] prs = new SqlParameter[6];
                    prs[0] = new SqlParameter(SP_CREATE_LOG_DETAIL_P1, headerID);
                    prs[1] = new SqlParameter(SP_CREATE_LOG_DETAIL_P2, logDetail.ItemName);
                    prs[2] = new SqlParameter(SP_CREATE_LOG_DETAIL_P3, logDetail.Result);
                    prs[3] = new SqlParameter(SP_CREATE_LOG_DETAIL_P4, logDetail.Status);
                    prs[4] = new SqlParameter(SP_CREATE_LOG_DETAIL_P5, logDetail.ProcessedBy);
                    prs[5] = new SqlParameter(SP_CREATE_LOG_DETAIL_P6, logDetail.ProcessedOn);
                    object obj = ExecuteScalar(CommandType.StoredProcedure, SP_CREATE_LOG_DETAIL, prs);
                    try
                    {
                        detailID = int.Parse(obj.ToString());
                    }
                    catch { }
                }
                else if (item is ImportRegLogItem)
                {
                    ImportRegLogItem logItem = item as ImportRegLogItem;
                    SqlParameter[] prs = new SqlParameter[8];
                    prs[0] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P1, headerID);
                    prs[1] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P2, logItem.ProgramCode);
                    prs[2] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P3, logItem.StoreCode);
                    prs[3] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P4, logItem.DisplaySetCode);
                    prs[4] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P5, logItem.Result);
                    prs[5] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P6, logItem.Status);
                    prs[6] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P7, logItem.ProcessedBy);
                    prs[7] = new SqlParameter(SP_CREATE_REG_LOG_DETAIL_P8, logItem.ProcessedOn);
                    object obj = ExecuteScalar(CommandType.StoredProcedure, SP_CREATE_REG_LOG_DETAIL, prs);
                    try
                    {
                        detailID = int.Parse(obj.ToString());
                    }
                    catch { }
                }
                return detailID;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return -1;
            }
        }
    }

    public class LogDetail
    {
        public string ItemName { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string ProcessedBy { get; set; }
        public DateTime? ProcessedOn { get; set; }

        public LogDetail()
        {
            ItemName = string.Empty;
            Result = string.Empty;
            Status = string.Empty;
            ProcessedBy = string.Empty;
            ProcessedOn = DateTime.Now;
        }
    }

    public class ImportRegLogItem
    {
        public string ProgramCode { get; set; }
        public string StoreCode { get; set; }
        public string DisplaySetCode { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string ProcessedBy { get; set; }
        public DateTime? ProcessedOn { get; set; }

        public ImportRegLogItem()
        {
            ProgramCode = string.Empty;
            StoreCode = string.Empty;
            DisplaySetCode = string.Empty;
            Result = string.Empty;
            Status = string.Empty;
            ProcessedBy = string.Empty;
            ProcessedOn = DateTime.Now;
        }
    }

    public class LogInformation<T>
    {
        public string Remark { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string ImportType { get; set; }
        public int Total { get; set; }
        public int Success { get; set; }
        public string ProcessedBy { get; set; }
        public DateTime? ProcessedOn { get; set; }

        public List<T> DetailCollection { get; set; }

        public LogInformation()
        {
            DetailCollection = new List<T>();
            FileName = string.Empty;
            Status = string.Empty;
            Total = 0;
            Success = 0;
            ProcessedBy = string.Empty;
            ProcessedOn = DateTime.Now;
            ImportType = typeof(T).Name;
            Remark = string.Empty;
        }
    }
}
