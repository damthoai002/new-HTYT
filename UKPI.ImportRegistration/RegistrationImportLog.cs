using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UKPI.Core;

namespace UKPI.ImportRegistration
{
    public class RegistrationImportLog
    {
        public Dictionary<ErrorCode, string> ErrorMessages { get; set; }

        protected FileProcessLogger logger;
        protected bool hasError;
        protected bool hasPending;
        protected bool processPending;

        protected LogInformation<ImportRegLogItem> logInformation;

        public RegistrationImportLog(string connectionString)
        {
            logger = new FileProcessLogger(connectionString);
            ErrorMessages = new Dictionary<ErrorCode, string>();
            hasError = false;
            hasPending = false;
            logInformation = new LogInformation<ImportRegLogItem>();
        }
		
		public void UpdateCounting(int total, int success, string pendingFile)
		{
			logInformation.Total = total;
			logInformation.Success = success;
            logInformation.Remark = pendingFile;
		}

        public void CreateLog(string fileName)
        {
            logInformation = new LogInformation<ImportRegLogItem>();
            logInformation.FileName = fileName;
			logInformation.Total = 0;
			logInformation.Success = 0;
            hasError = false;
            hasPending = false;
            processPending = false;
        }

        public void CreateLog(string fileName, bool isPending)
        {
            logInformation = new LogInformation<ImportRegLogItem>();
            logInformation.FileName = fileName;
            logInformation.Total = 0;
            logInformation.Success = 0;
            hasError = false;
            hasPending = false;
            processPending = isPending;
        }

        public void MarkPendingResolve(int headerID)
        {
            logger.UpdateHeaderStatus(headerID, FileProcessStatus.Resolve.ToString());
        }

        public void RejectItem(DataRow row, List<ErrorCode> errors)
        {
            hasError = true;
            ImportRegLogItem item = new ImportRegLogItem();
            item.ProgramCode = row[RegistrationImportDao.COL_PROGRAMCODE].ToString();
            item.StoreCode = row[RegistrationImportDao.COL_STORECODE].ToString();
            item.DisplaySetCode = row[RegistrationImportDao.COL_DISPLAYSETCODE].ToString();
            
            List<string> errMsg = new List<string>();
            foreach (var e in errors)
            {
                if (ErrorMessages.ContainsKey(e))
                    errMsg.Add(ErrorMessages[e]);
            }
            item.Result = string.Join(DataAccess.DB_FIELD_SEPERATOR, errMsg.ToArray());
            item.Status = FileProcessStatus.Reject.ToString();
            item.ProcessedOn = DateTime.Now;
            logInformation.DetailCollection.Add(item);
			logInformation.Total++;
        }

        public void PendingItem(DataRow row)
        {
            hasPending = true;
            ImportRegLogItem item = new ImportRegLogItem();
            item.ProgramCode = row[RegistrationImportDao.COL_PROGRAMCODE].ToString();
            item.StoreCode = row[RegistrationImportDao.COL_STORECODE].ToString();
            item.DisplaySetCode = row[RegistrationImportDao.COL_DISPLAYSETCODE].ToString();

            item.Result = string.Empty;
            if (ErrorMessages.ContainsKey(ErrorCode.NotExistStore))
                item.Result = ErrorMessages[ErrorCode.NotExistStore];
            item.Status = FileProcessStatus.Pending.ToString();
            item.ProcessedOn = DateTime.Now;
            logInformation.DetailCollection.Add(item);
			logInformation.Total++;
        }

        public void SuccessItem(DataRow row)
        {
            ImportRegLogItem item = new ImportRegLogItem();
            item.ProgramCode = row[RegistrationImportDao.COL_PROGRAMCODE].ToString();
            item.StoreCode = row[RegistrationImportDao.COL_STORECODE].ToString();
            item.DisplaySetCode = row[RegistrationImportDao.COL_DISPLAYSETCODE].ToString();

            item.Result = string.Empty;
            item.Status = FileProcessStatus.Success.ToString();
            item.ProcessedOn = DateTime.Now;
            logInformation.DetailCollection.Add(item);
			logInformation.Total++;
			logInformation.Success++;
        }

        public void SaveLog()
        {
            if (hasError)
            {
                logInformation.Status = FileProcessStatus.Resolve.ToString();
            }
            else if (hasPending)
            {
                logInformation.Status = FileProcessStatus.Pending.ToString();
            }
            else
            {
                logInformation.Status = FileProcessStatus.Success.ToString();
            }

            logger.CreateLog(logInformation);
        }

        /// <summary>
        /// TuanDH add on 15 Feb 2012
        /// </summary>
        /// <param name="sender"></param>
        public void SendEmail(SendMail sender)
        {
            List<ImportRegLogItem> rejectList = new List<ImportRegLogItem>(
                logInformation.DetailCollection.Where(x => x.Status == FileProcessStatus.Reject.ToString()));

            List<ImportRegLogItem> pendingList = new List<ImportRegLogItem>(
                logInformation.DetailCollection.Where(x => x.Status == FileProcessStatus.Pending.ToString()));

            // There is no rejected or pending item
            if (rejectList.Count == 0 && pendingList.Count == 0)
            {
                return;
            }

            // Get email information: To, CC, ShipToCode, Subject
            string[] emailInfo = logger.GetEmailInfo(logInformation.DetailCollection[0].StoreCode);
            string shipToCode = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;
            string subject = string.Empty;

            if (emailInfo != null)
            {
                shipToCode = emailInfo[0];
                to = emailInfo[1];
                cc = emailInfo[2];
                subject = string.Format(@"{0} - {1}", sender.Subject, shipToCode);
            }

            // There is no 
            if (to.Trim().Length == 0)
            {
                return;
            }

            // Build body of email from reject and pending lists
            string body = sender.ContentHeaderLine1 + "<br/><br/>";
            body += sender.ContentHeaderLine2 + "<br/><br/>";
            body += sender.ContentHeaderLine3 + "<br/><br/>";
            if (rejectList.Count > 0)
            {
                // Reject list
                body += sender.ContentDetailRejectList + "<br/>";
                foreach (ImportRegLogItem item in rejectList)
                {
                    body += string.Format(@"{0}, {1}, {2}", item.StoreCode, item.DisplaySetCode, item.Result);
                    body += "<br/>";
                }

                body += "<br/>";
            }
            if (pendingList.Count > 0)
            {
                // Pending list
                body += sender.ContentDetailPendingList + "<br/>";
                foreach (ImportRegLogItem item in pendingList)
                {
                    body += string.Format(@"{0}, {1}, {2}", item.StoreCode, item.DisplaySetCode, item.Result);
                    body += "<br/>";
                }

                body += "<br/>";
            }

            body += sender.ContentDetailFooterLine1 + "<br/><br/>";
            body += sender.ContentDetailFooterLine2 + "<br/><br/>";

            // Send email
            sender.Send(to, cc, subject, body);
        }
    }

    public enum FileProcessStatus
    {
        Resolve,  // 
        Pending,  // Pending
        Success,   // Success
        Reject
    }
}
