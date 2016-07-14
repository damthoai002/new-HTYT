using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;

namespace UKPI.BlendedReport
{
    public class ExportError : IErrorObject
    {
        public ExportErrorType Error{get;set;}
        public object Value{get;set;}

        #region IErrorObject Members

        public object[] GetErrorArguments()
        {
            object[] args = new object[0];
            switch (Error)
            {
                case ExportErrorType.FileExisted:
                    args = new object[] { Value.ToString() };
                    break;
                default:
                    args = new object[] { Value };
                    break;
            }
            return args;
        }

        public string GetErrorKey()
        {
            return Error.ToString();
        }

        public IErrorObject CopyFrom(Exception ex)
        {
            Error = ExportErrorType.Unknown;
            Value = ex;
            return this;
        }

        #endregion

        public ExportError()
        {
        }

        public ExportError(ExportErrorType type, object value)
        {
            Error = type;
            Value = value;
        }
    }

    public enum ExportErrorType
    {
        FileExisted,
        Unknown
    }
}
