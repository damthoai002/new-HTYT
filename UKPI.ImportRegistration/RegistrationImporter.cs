using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using UKPI.Core;

namespace UKPI.ImportRegistration
{
    public class RegistrationImporter
    {
        public const string COL_STORECODE = "StoreCode";
        public const string COL_ERRORCODE = "ErrorCode";
        public const string COL_PROGRAMCODE = "ProgramCode";
        public const string COL_SHOP_FORMAT = "ShopFormat";
        public const string MAPPING_FILE = "RegImportMapping.xml";
        public const string PARAM_DEADLINE = "Deadline_Receive_RegisterList";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(RegistrationImporter));

        protected RegistrationImportDao dao;
        protected string currentImportFile;
        protected RegistrationImportLog importLog;

        public string ConfigurationFolder { get; set; }
        public string PendingFolder { get; set; }
        public string BackupFolder { get; set; }
        public string RejectFolder { get; set; }
        public string PasswordToken { get; set; }
        public int MaxTokenLen { get; set; }
        public bool LogSuccess { get; set; }

        // TuanDH add on 14 Feb 2012
        public SendMail EmailSender { get; set; }

        protected bool exceedDeadline;

        public RegistrationImporter(string connectionString)
        {
            dao = new RegistrationImportDao(connectionString);
            importLog = new RegistrationImportLog(connectionString);
            importLog.ErrorMessages.Add(ErrorCode.NotExistStore, "Store code does not exist");
            importLog.ErrorMessages.Add(ErrorCode.InvalidProgramCode, "Invalid program code");
            importLog.ErrorMessages.Add(ErrorCode.ExceedDeadline, "The registration time is over");
            PendingFolder = string.Empty;
            BackupFolder = string.Empty;
            RejectFolder = string.Empty;
            currentImportFile = string.Empty;
            LogSuccess = false;
            exceedDeadline = false;
        }

        public void ImportZip(string filePath)
        {
            currentImportFile = filePath;
            DataTable table = ReadZipFile(filePath);
            importLog.CreateLog(Path.GetFileName(filePath));
            exceedDeadline = false;
            if (table != null)
            {
                ImportData(table);
            }
        }

        public void MarkResolvePending(int headerID)
        {
            importLog.MarkPendingResolve(headerID);
        }

        //protected void ImportPending(string filePath, int headerLogID)
        //{
        //    processPending = true;
        //    currentImportFile = filePath;
        //    DataTable table = ReadZipFile(filePath);
        //    importLog.CreateLog(Path.GetFileName(filePath));
        //    if (table != null)
        //    {
        //        ImportData(table);
        //    }
        //}

        protected DataTable ReadZipFile(string filePath)
        {
            Cryptography crypt = new Cryptography();
            string password = crypt.GenPWDByFilename(Path.GetFileName(filePath), MaxTokenLen, PasswordToken);

            List<KeyValuePair<string, byte[]>> dataFiles = Zipper.UnZipFile(filePath, password);
            if (dataFiles.Count > 0)
            {
                DataTable table = null;
                using (MemoryStream ms = new MemoryStream(dataFiles[0].Value))
                {
                    DataSet data = new DataSet();
                    data.ReadXml(ms);
                    if (data.Tables.Count > 0)
                    {
                        table = data.Tables[0];
                    }
                }
                return table;
            }
            else
            {
                log.Warn(string.Format("Cannot read data from {0}", filePath));
                return null;
            }
        }

        public DateTime GetDeadline(int year, int month)
        {
            int m = 0;
            string dl = dao.GetParameterValue(PARAM_DEADLINE);
            int d = 1;
            try
            {
                string[] tmp = dl.Split('@');
                if (tmp.Length > 1)
                {
                    d = int.Parse(tmp[1]);
                    m = int.Parse(tmp[0]);
                }

            }
            catch { }
            return new DateTime(year, month, d).AddMonths(m);
        }

        public DataSet SearchLog(RegistrationLogSE searchEntity)
        {
            return dao.SearchLog(searchEntity);
        }

        protected void ImportData(DataTable table)
        {
            if (!table.Columns.Contains(COL_ERRORCODE))
                table.Columns.Add(COL_ERRORCODE);
            CheckImportTemplate(table);
            CheckStoreCode(table);
            CheckRegistrationRules(table);
            if (exceedDeadline)
            {
                RejectDeadline(table);
            }
            else
            {
                PostProcessTable(table);
            }

            table.Columns.Remove(COL_ERRORCODE);
        }

        protected void PostProcessTable(DataTable table)
        {
            DataTable success = table.Clone();
            DataTable reject = table.Clone();
            DataTable pending = table.Clone();
            // Add logging later

            foreach (DataRow row in table.Rows)
            {
                object obj = row[COL_ERRORCODE];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                {
                    string[] tmp = obj.ToString().Split(',');
                    List<ErrorCode> errorCodes = new List<ErrorCode>();
                    foreach (string item in tmp)
                    {
                        ErrorCode code = (ErrorCode)Enum.Parse(typeof(ErrorCode), item.Trim());
                        errorCodes.Add(code);
                    }

                    if (errorCodes.Contains(ErrorCode.NotExistStore))
                    {
                        errorCodes.RemoveAll(x => x == ErrorCode.NotExistStore);
                        if (errorCodes.Count > 0)
                        {
                            reject.Rows.Add(row.ItemArray);
                            importLog.RejectItem(row, errorCodes);
                            log.Error(string.Format("Reject: {0}", GetString(row.ItemArray)));
                        }
                        else
                        {
                            pending.Rows.Add(row.ItemArray);
                            importLog.PendingItem(row);
                            log.Warn(string.Format("Pending: {0}", GetString(row.ItemArray)));
                        }
                    }
                    else
                    {
                        reject.Rows.Add(row.ItemArray);
                        importLog.RejectItem(row, errorCodes);
                        log.Error(string.Format("Reject: {0}", GetString(row.ItemArray)));
                    }

                }
                else
                {
                    success.Rows.Add(row.ItemArray);
                    // Not log success item into database
                    if (LogSuccess)
                        importLog.SuccessItem(row);
                    log.Info(string.Format("Saved: {0}", GetString(row.ItemArray)));
                }
            }

            // Import valid data
            dao.UpdateRegistration(success, Path.Combine(ConfigurationFolder, MAPPING_FILE));

            RejectData(reject);

            string pendingFile = SkipPending(pending);

            // Move to backup folder
            string backupPath = Path.Combine(BackupFolder, Path.GetFileName(currentImportFile));
            Utilities.MoveFile(currentImportFile, backupPath);

            importLog.UpdateCounting(table.Rows.Count, success.Rows.Count, pendingFile);
            importLog.SaveLog();

            // TuanDH add on 14 Feb 2012: send email rejected and pending registered stores
            importLog.SendEmail(this.EmailSender);
        }

        protected void RejectData(DataTable table)
        {
        }

        protected void RejectDeadline(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                List<ErrorCode> errorCodes = new List<ErrorCode>();
                object obj = row[COL_ERRORCODE];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                {
                    string[] tmp = obj.ToString().Split(',');
                    
                    foreach (string item in tmp)
                    {
                        ErrorCode code = (ErrorCode)Enum.Parse(typeof(ErrorCode), item.Trim());
                        errorCodes.Add(code);
                    }
                }
                errorCodes.Add(ErrorCode.ExceedDeadline);
                importLog.RejectItem(row, errorCodes);
                log.Error(string.Format("Reject: {0}", GetString(row.ItemArray)));
            }

            importLog.UpdateCounting(table.Rows.Count, 0, string.Empty);
            importLog.SaveLog();
            // Move to reject folder
            string rejectPath = Path.Combine(RejectFolder, Path.GetFileName(currentImportFile));
            Utilities.MoveFile(currentImportFile, rejectPath);
        }

        protected string SkipPending(DataTable table)
        {
            if (!string.IsNullOrEmpty(PendingFolder))
            {
                if (!Directory.Exists(PendingFolder))
                    Directory.CreateDirectory(PendingFolder);
                if (table.Columns.Contains(COL_ERRORCODE))
                    table.Columns.Remove(COL_ERRORCODE);
                // Save To pending folder

                DataSet pending = new DataSet("RegisterInfo");
                table.TableName = "RegisteredStore";
                pending.Tables.Add(table);
                string fileName = Path.GetFileNameWithoutExtension(currentImportFile);
                bool processPending = fileName.Contains("Pending");
                if (processPending)
                    fileName = fileName.Substring(0, fileName.Length - 22);
                else
                    fileName = fileName.Substring(0, fileName.Length - 14);
                fileName = fileName + DateTime.Now.ToString("yyyyMMddhhmmss");
                fileName += "_Pending.zip";
                fileName = Path.Combine(PendingFolder, fileName);
                SaveDataToFile(pending, fileName);

                log.Info(string.Format("Move pending data of {0} to {1}.", currentImportFile, fileName));
                return Path.GetFileName(fileName);
            }
            return string.Empty;
        }

        protected void SaveDataToFile(DataSet exportData, string zipFilePath)
        {
            Cryptography crypt = new Cryptography();
            string fileName = Path.ChangeExtension(zipFilePath, ".xml");
            string password = crypt.GenPWDByFilename(Path.GetFileName(zipFilePath), MaxTokenLen, PasswordToken);

            if (exportData != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    exportData.WriteXml(ms);
                    ms.Flush();
                    Zipper.ZipFiles(ms, fileName, zipFilePath, password);
                }
            }
        }

        protected void CheckImportTemplate(DataTable table)
        {
        }

        protected void CheckStoreCode(DataTable table)
        {
            List<string> storeCollection = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                string storeCode = string.Empty;
                object obj = row[COL_STORECODE];
                if (obj != null)
                    storeCode = obj.ToString();
                if (!string.IsNullOrEmpty(storeCode) && !storeCollection.Contains(storeCode))
                {
                    storeCollection.Add(storeCode);
                }
            }
            List<string> notExist = dao.GetNotExistStores(storeCollection);
            if (table.Columns.Contains(COL_ERRORCODE))
            {
                foreach (DataRow row in table.Rows)
                {
                    string storeCode = string.Empty;
                    object obj = row[COL_STORECODE];
                    if (obj != null)
                        storeCode = obj.ToString().ToUpper();
                    if (notExist.Contains(storeCode))
                    {
                        obj = row[COL_ERRORCODE];
                        if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                            row[COL_ERRORCODE] = obj.ToString() + "," + ErrorCode.NotExistStore.ToString();
                        else
                            row[COL_ERRORCODE] = ErrorCode.NotExistStore.ToString();
                    }
                }
            }
        }

        protected void CheckRegistrationRules(DataTable table)
        {
            string programCode = CheckProgramCodeConsitence(table);
            List<string> storeCollection = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                string storeCode = string.Empty;
                object obj = row[COL_STORECODE];
                if (obj != null)
                    storeCode = obj.ToString();
                if (!string.IsNullOrEmpty(storeCode) && !storeCollection.Contains(storeCode))
                {
                    storeCollection.Add(storeCode);
                }
            }
            bool hasShopFormat = false;
            if (table.Columns.Contains(COL_SHOP_FORMAT))
                hasShopFormat = true;
            else
                table.Columns.Add(COL_SHOP_FORMAT);
            DataSet registrationRules = dao.LoadRegistrationRules(programCode);
            Dictionary<string, string> shopFormatMapping = dao.GetShopFormatMapping(storeCollection);

            bool invalidProgramCode = false;
            if (registrationRules.Tables.Contains(RegistrationImportDao.TAB_MARKETING_PROGRAM)
                && registrationRules.Tables[RegistrationImportDao.TAB_MARKETING_PROGRAM].Rows.Count > 0)
            {
                object code = registrationRules.Tables[RegistrationImportDao.TAB_MARKETING_PROGRAM].Rows[0][COL_PROGRAMCODE];
                if (code == null || code.ToString().Trim() != programCode)
                {
                    invalidProgramCode = true;
                }
            }
            else
            {
                invalidProgramCode = true;
            }
            if (invalidProgramCode)
            {
                foreach (DataRow row in table.Rows)
                {
                    object obj = row[COL_ERRORCODE];
                    if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                        row[COL_ERRORCODE] = obj.ToString() + "," + ErrorCode.InvalidProgramCode.ToString();
                    else
                        row[COL_ERRORCODE] = ErrorCode.InvalidProgramCode.ToString();
                }
            }
            if (hasShopFormat)
                table.Columns.Remove(COL_SHOP_FORMAT);
            if (!string.IsNullOrEmpty(programCode) && programCode.Length > 6)
            {
                int year = ParseInt(programCode.Substring(programCode.Length - 6, 4));
                int month = ParseInt(programCode.Substring(programCode.Length - 2, 2));
                if (year > 0 && month > 0)
                {
                    DateTime deadline = GetDeadline(year, month);
                    if (deadline < DateTime.Now.Date)
                        exceedDeadline = true;
                }
            }
        }

        protected int ParseInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch { return 0; }
        }

        protected string GetString(object[] itemArray)
        {
            List<string> items = new List<string>();
            foreach (var obj in itemArray)
            {
                string val = string.Empty;
                if (obj != null) val = obj.ToString();
                items.Add(val);
            }
            return string.Join(",", items.ToArray());
        }

        protected string CheckProgramCodeConsitence(DataTable table)
        {
            string programCode = string.Empty;
            bool isFirst = true;
            foreach (DataRow row in table.Rows)
            {
                string value = string.Empty;
                object obj = row[COL_PROGRAMCODE];
                if (obj != null) value = obj.ToString().Trim().ToUpper();
                if (isFirst)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        obj = row[COL_ERRORCODE];
                        if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                            row[COL_ERRORCODE] = obj.ToString() + "," + ErrorCode.InvalidProgramCode.ToString();
                        else
                            row[COL_ERRORCODE] = ErrorCode.InvalidProgramCode.ToString();
                    }
                    else
                    {
                        programCode = value;
                        isFirst = false;
                    }
                }
                else
                {
                    if (value != programCode)
                    {
                        obj = row[COL_ERRORCODE];
                        if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
                            row[COL_ERRORCODE] = obj.ToString() + "," + ErrorCode.InvalidProgramCode.ToString();
                        else
                            row[COL_ERRORCODE] = ErrorCode.InvalidProgramCode.ToString();
                    }
                }
            }
            return programCode;
        }
    }

    public enum ErrorCode
    {
        InvalidTemplate,
        NotExistStore,
        InvalidProgramCode,
        ExceedDeadline

    }
}
