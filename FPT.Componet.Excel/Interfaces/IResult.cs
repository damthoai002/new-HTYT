using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public interface IResult<T>
        where T:IErrorObject
    {
        void Update(IResult<T> other);
        List<T> Errors { get; set; }

        bool ForceStop { get; set; }

        bool IsSuccess { get; }

        void AddException(Exception ex);
    }

    public interface IErrorObject
    {
        object[] GetErrorArguments();
        string GetErrorKey();
        IErrorObject CopyFrom(Exception ex);
    }

    public class ErrorObject:IErrorObject
    {

        #region IErrorObject Members

        public object[] GetErrorArguments()
        {
            return new object[0];
        }

        public string GetErrorKey()
        {
            return string.Empty;
        }

        public IErrorObject CopyFrom(Exception ex)
        {
            return new ErrorObject();
        }

        #endregion
    }
}
