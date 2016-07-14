using System;
using System.Runtime.InteropServices;

namespace FPT.Component.ExcelPlus
{
    public class Common
    {
        #region - API method -
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(Int32 hWnd, ref IntPtr lpdwProcessId);
        #endregion
    }

    public enum ResultState
    {
        Success,
        FailContinue,
        Halt
    }

    public class DummyLogger : IErrorLogger
    {
        #region IErrorLogger Members

        public void LogError(object error)
        {
        }

        public void LogException(Exception ex)
        {
        }

        #endregion
    }

    /// <summary>
    /// Binary Identity - Ignored Case
    /// </summary>
    public class BinaryId : IEquatable<BinaryId>
    {
        public string ID1 { get; set; }
        public string ID2 { get; set; }

        public BinaryId()
        {
            ID1 = string.Empty;
            ID2 = string.Empty;
        }

        public BinaryId(string id1, string id2)
        {
            ID1 = id1;
            ID2 = id2;
        }

        #region IEquatable<BinaryId> Members

        public bool Equals(BinaryId other)
        {
            return (ID1.ToUpper().Equals(other.ID1.ToUpper())
                && ID2.ToUpper().Equals(other.ID2.ToUpper()));
        }

        #endregion

        public override int GetHashCode()
        {
            string tmp = ID1 + ID2;
            return tmp.GetHashCode();
        }
    }
}
