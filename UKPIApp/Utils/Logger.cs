using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKPI.BlendedReport;
using FPT.Component.ExcelPlus;

namespace UKPI.Utils
{
    public class Logger: IErrorLogger
    {
        public const string MSG_ERROR_PREFIX = "ImportError.";
        private static log4net.ILog log = null;

        public Logger(log4net.ILog logger)
        {
            log = logger;
        }

        public void LogError<T>(T error)
            where T : IErrorObject
        {
            ImportError er = error as ImportError;
            if (er != null)
            {
                string key = er.GetErrorKey();
                object[] args = er.GetErrorArguments();
                string msg = clsResources.GetMessage(GetMessageKey(key), args);
                log.Error(msg);
            }
        }

        public void LogError(string message)
        {
            log.Error(message);
        }

        public string GetMessageKey(string key)
        {
            return MSG_ERROR_PREFIX + key;
        }

        #region IErrorLogger Members

        public void LogError(object error)
        {
            if (error is ImportError)
            {
                LogError<ImportError>(error as ImportError);
            }
        }

        public void LogException(Exception ex)
        {
            log.Error(ex);
        }

        #endregion
    }
}
