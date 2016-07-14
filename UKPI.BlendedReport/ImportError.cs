using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPT.Component.ExcelPlus;

namespace UKPI.BlendedReport
{
    public class ImportError : IErrorObject
    {
        public ErrorType Error { get; set; }

        public object Value { get; set; }

        public ImportError()
        {
            Error = ErrorType.Unknown;
            Value = null;
        }

        public ImportError(ErrorType error, object value)
        {
            Error = error;
            Value = value;
        }

        /// <summary>
        /// Return Error message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #region IErrorObject Members

        public IErrorObject CopyFrom(Exception ex)
        {
            Error = ErrorType.Unknown;
            Value = ex;
            return this;
        }

        #endregion

        #region IErrorObject Members


        public string GetErrorKey()
        {
            return Error.ToString();
        }

        #endregion

        #region IErrorObject Members

        public object[] GetErrorArguments()
        {
            object[] args = new object[0];
            switch (Error)
            {
                case ErrorType.Unknown:
                    args = new object[] { Value };
                    break;
                case ErrorType.Duplicate:
                    DupplicateError dup = Value as DupplicateError;
                    if (dup != null)
                    {
                        args = new object[] { dup.Sheet, ConvertToString(dup.Rows) };
                    }
                    break;
                case ErrorType.None:
                    break;
                case ErrorType.OLNotExisted:
                    ExistenceError ee = Value as ExistenceError;
                    if (ee != null)
                    {
                        args = new object[] { ee.Cell.Sheet, ee.Cell.Row, ee.Cell.Column, ee.Value };
                    }
                    break;
                case ErrorType.DTNotExisted:
                    ExistenceError ee2 = Value as ExistenceError;
                    if (ee2 != null)
                    {
                        args = new object[] { ee2.Cell.Sheet, ee2.Cell.Row, ee2.Cell.Column, ee2.Value };
                    }
                    break;
                case ErrorType.ChannelNotExisted:
                    ExistenceError ee3 = Value as ExistenceError;
                    if (ee3 != null)
                    {
                        args = new object[] { ee3.Cell.Sheet, ee3.Cell.Row, ee3.Cell.Column, ee3.Value };
                    }
                    break;
                case ErrorType.WrongTemplate:
                    args = new string[]{};
                    break;
                default:
                    CellIndex cell = Value as CellIndex;
                    if (cell != null)
                    {
                        args = new object[] { cell.Sheet, cell.Row, cell.Column };
                    }
                    break;
            }
            return args;
        }

        #endregion

        protected string ConvertToString(List<int> numbers)
        {
            string result = string.Empty;
            foreach (var val in numbers)
            {
                result += string.Format("[{0}],", val);
            }
            result = result.Trim(", ".ToCharArray());
            return result;
        }
    }

    public enum ErrorType
    {
        Blank,
        HeaderName,
        HeaderIndex,
        Numeric,
        OutOfRange,
        Duplicate,
        MonthFormat,
        OLNotExisted,
        DTNotExisted,
        Unknown,
        None,
        ChannelNotExisted,
        WrongTemplate,
        InvalidTimePeriod
    }

    public class ExistenceError
    {
        public CellIndex Cell { get; set; }
        public object Value { get; set; }

        public ExistenceError()
        {
            Cell = new CellIndex();
            Value = string.Empty;
        }
    }

    public class CellIndex
    {
        public int Sheet { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public CellIndex()
        {
        }

        public CellIndex(int sheet, int row, int column)
        {
            Sheet = sheet;
            Row = row;
            Column = column;
        }
    }

    public class DupplicateError
    {
        public int Sheet { get; set; }
        public List<int> Rows { get { return rows; } }

        private List<int> rows = new List<int>();

        public DupplicateError(int sheet)
        {
            rows = new List<int>();
            Sheet = sheet;
        }
    }
}
