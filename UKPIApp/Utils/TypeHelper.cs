using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace UKPI.Utils
{
	/// <summary>
	/// This class is used to manage/convert common type.
	/// </summary>
	public sealed class TypeHelper
	{
		/// <summary>
		/// Date Format Culture.
		/// </summary>
		public static IFormatProvider DATEFORMAT = new CultureInfo("en-US", true);

        /// <summary>
        /// Get String ToUpper. Null is considered as blank
        /// </summary>
        public static string GetStringToUpper(object a_obj)
        {
            try
            {
                if (a_obj != null)
                {
                    string temp = a_obj.ToString();
                    return temp.ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Get String ToUpper First Letter. Null is considered as blank
        /// </summary>
        public static string GetStringToUpperFisrtLetter(object a_obj)
        {
            string subFirst = string.Empty;
            string subEnd = string.Empty;
            string temp = string.Empty;
            try
            {
                if (a_obj != null)
                {
                    subFirst = a_obj.ToString().Substring(0, 1);
                    subEnd = a_obj.ToString().Substring(1, a_obj.ToString().Length - 1);
                   
                    if (subFirst != null || subFirst != "")
                    {
                        subFirst = subFirst.ToUpper();
                    }
                    if (subEnd.Length > 0)
                    {
                        temp = subFirst + subEnd;
                    }
                    return temp;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Get String ToUpper Letter. Null is considered as blank
        /// </summary>
        public static string GetStringToUpperLetter(object a_obj)
        {
            string subFirst = string.Empty;
            string subEnd = string.Empty;
            string temp = string.Empty;
            try
            {
                if (a_obj != null)
                {
                    string[] splStr = a_obj.ToString().Split(' ');
                    for (int i = 0; i < splStr.Length; i++)
                    {
                        if (splStr[i].ToString() != "")
                        {
                            subFirst = splStr[i].ToString().Substring(0, 1);
                            subEnd = splStr[i].ToString().Substring(1, splStr[i].ToString().Length - 1);

                            if (subFirst != null || subFirst != "")
                            {
                                subFirst = subFirst.ToUpper();
                            }
                            if (subEnd.Length > 0)
                            {
                                temp = temp + " " + subFirst + subEnd;
                            }
                            else
                            {
                                temp = temp + " " + subFirst;
                            }
                        }
                    }
                    return temp;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
		/// Get String. Null is considered as blank
		/// </summary>
		public static string GetString(object a_obj)
		{
			return GetHTMLRequest(a_obj).Trim();
		}
		/// <summary>
		/// Get String. Null is considered as blank
		/// <returns>A string of data submit from Client.</returns>
		/// </summary>
		public static string GetHTMLRequest(object a_obj)
		{
			if (a_obj == null)
			{
				return string.Empty;
			}
			else
			{
				return a_obj.ToString();
			}
		}
		/// <summary>
		/// Get a string and cut off % sign at the end 
		/// which is submited from HTML form.
		/// <returns>A decimal data type presenting data submit from Client.</returns>
		/// </summary>
		public static decimal GetPercentateHTMLRequest(object a_obj)
		{
			if (a_obj == null)
			{
				return 0;
			}
			else
			{
				return GetDecimal(a_obj.ToString().Replace("%", ""));
			}
		}

		/// <summary>
		/// Get String. Null is considered as blank
		/// If a string is not blank, encode it for HTML safe character.
		/// <returns>A string that converted all unsafe HTML character in to code</returns>
		/// </summary>
		public static string ToHTMLSafeCharacter(object a_obj)
		{
			//if object is null or blank, return blank string immediately.
			if (a_obj == null || a_obj.ToString().Equals(""))
			{
				return string.Empty;
			}
			string sValue = string.Empty;
			try
			{
				sValue = a_obj.ToString();
				sValue = sValue.Replace("<", "&lt;");
				sValue = sValue.Replace(">", "&gt;");
				sValue = sValue.Replace("'", "&#39");
				sValue = sValue.Replace("\"", "&quot;");
				//More known unsafe character can be added here.
			}
			catch
			{
				return a_obj.ToString();
			}
			return sValue;
		}

		/// <summary>
		/// Get String. Null is considered as blank
		/// If a string is not blank, encode it for HTML safe character.
		/// <returns>A string that converted all unsafe HTML character in to code</returns>
		/// </summary>
		public static string ToUnsafeCharacter(object a_obj)
		{
			//if object is null or blank, return blank string immediately.
			if (a_obj == null || a_obj.ToString().Equals(""))
			{
				return string.Empty;
			}
			string sValue = string.Empty;
			try
			{
				sValue = a_obj.ToString();
				sValue = sValue.Replace("&lt;", "<");
				sValue = sValue.Replace("&gt;", ">");
				sValue = sValue.Replace("&#39", "'");
				sValue = sValue.Replace("&quot;", "\"");
				//More known encoded character can be added here.
			}
			catch
			{
				return a_obj.ToString();
			}
			return sValue;
		}

		/// <summary>
		/// Get String. Null is considered as blank
		/// If a string is not blank, encode it for JavaScript safe character.
		/// <returns>A string that converted all unsafe JavaScript character in to code</returns>
		/// </summary>
		public static string ToSafeJavaScript(object a_obj)
		{
			return ToScriptString(a_obj);
		}

		/// <summary>
		/// Get String. Null is considered as blank
		/// If a string is not blank, encode it for JavaScript safe character.
		/// <returns>A string that converted all unsafe JavaScript character in to code</returns>
		/// </summary>
		public static string ToScriptString(object a_obj)
		{
			//if object is null or blank, return blank string immediately.
			if (a_obj == null || a_obj.ToString().Equals(""))
			{
				return string.Empty;
			}
			string sValue = string.Empty;
			try
			{
				sValue = ToUnsafeCharacter(a_obj);
				sValue = sValue.Replace("\\", "\\\\");
				sValue = sValue.ToString().Replace("'", "\\'");
				sValue = sValue.Replace("\"", "\\\"");
				//More known unsafe character can be added here.
			}
			catch
			{
				return a_obj.ToString();
			}
			return sValue;
		}
		/// <summary>
		/// To Safe SQL String
		/// </summary>
		/// <param name="a_obj"></param>
		/// <returns></returns>
		public static string ToSmoothQuote(object a_obj)
		{
			if (a_obj == null || a_obj.ToString().Equals(""))
			{
				return string.Empty;
			}
			string sValue = string.Empty;
			try
			{
				sValue = a_obj.ToString().Replace("'", "''");
			}
			catch
			{
				return a_obj.ToString();
			}
			return sValue;
		}

		/// <summary>
		/// For Parse to long data type
		/// </summary>
		public static long GetLong(object objInfo)
		{
			long lRet = 0;
			try
			{
				if (objInfo != null)
					lRet = long.Parse(objInfo.ToString());
			}
			catch
			{
				lRet = 0;
			}
			return lRet;
		}

		/// <summary>
		/// Convert to Int datatype
		/// </summary>
		/// <param name="objInfo">Object to be converted</param>
		/// <returns>int value.</returns>
		public static int GetInt(object objInfo)
		{
			int iRet = 0;
			try
			{
				if (objInfo != null)
					iRet = int.Parse(objInfo.ToString());
			}
			catch
			{
				return iRet;
			}

			return iRet;
		}

        public static int? GetIntNullable(object objInfo)
        {
            int? iRet = null;
            try
            {
                if (objInfo != null && objInfo != "")
                    iRet = int.Parse(objInfo.ToString());
            }
            catch
            {
                return iRet;
            }

            return iRet;
        }

		public static bool IsNumber(object objInfo)
		{
			try
			{
				int.Parse(TypeHelper.GetString(objInfo));
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

        /// <summary>
        /// Convert Double Data Type
        /// </summary>
        /// <param name="objInfo">Object to be converted</param>
        /// <returns>Double value</returns>
        public static double GetDouble(object objInfo)
        {
            string temp = objInfo.ToString();
            if (temp.StartsWith("$"))
            {
                temp = temp.Substring(1);
            }
            double dRet = 0;
            try
            {
                return dRet = double.Parse(temp);
            }
            catch
            {
                return dRet;
            }
        }

        /// <summary>
        /// Convert Float Data Type
        /// </summary>
        /// <param name="objInfo">Object to be converted</param>
        /// <returns>Float value</returns>
        public static float GetFloat(object objInfo)
        {
            string temp = objInfo.ToString();
            if (temp.StartsWith("$"))
            {
                temp = temp.Substring(1);
            }
            float dRet = 0;
            try
            {
                return dRet = float.Parse(temp);
            }
            catch
            {
                return dRet;
            }
        }
        
		/// <summary>
		/// For Parse to byte data type
		/// </summary>
		public static byte GetByte(object a_obj)
		{
			byte bRet = 0;
			try
			{
				bRet = byte.Parse(a_obj.ToString());
			}
			catch
			{
				bRet = 0;
			}
			return bRet;
		}

		/// <summary>
		/// Convert String to Hex Value.
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public static short StringToHex(object objInfo)
		{
			uint uiDecimal = 0;
			try
			{
				// Convert text string to unsigned integer
				string strvalue = objInfo == null ? "" : objInfo.ToString();
				if (strvalue.Length < 3) return 0;
				strvalue = strvalue.Substring(2);
				uiDecimal = checked((uint) Convert.ToUInt32(strvalue, 16));
			}

			catch
			{
				return 0;
			}
			return (short) uiDecimal;
		}

		/// <summary>
		/// Convert Short Data Type
		/// </summary>
		/// <param name="objInfo">Object to be converted</param>
		/// <returns>Short value</returns>
		public static short GetShort(object objInfo)
		{
			short sRet = (short) 0;
			try
			{
				sRet = short.Parse(objInfo.ToString());
			}
			catch
			{
				return sRet;
			}
			return sRet;
		}

		/// <summary>
		/// Convert Bool data type.
		/// </summary>
		/// <param name="objInfo">Object to be converted</param>
		/// <returns>Bool value</returns>
		public static bool GetBool(object objInfo)
		{
			bool bRet = false;
			try
			{
				if (objInfo != null)
					bRet = bool.Parse(objInfo.ToString());
			}
			catch
			{
				return bRet;
			}
			return bRet;
		}

		/// <summary>
		/// Convert Decimal Data Type
		/// </summary>
		/// <param name="objInfo">Object to be converted</param>
		/// <returns>Decimal value</returns>
		public static decimal GetDecimal(object objInfo)
		{
			string temp = objInfo.ToString();
			if (temp.StartsWith("$"))
			{
				temp = temp.Substring(1);
			}
			decimal dRet = 0;
			try
			{
				return dRet = decimal.Parse(temp);
			}
			catch
			{
				return dRet;
			}
		}

		/// <summary>
		/// Convert DateTime data type
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static DateTime GetDateTime(object info)
		{
			DateTime date = DateTime.MinValue;
			try
			{
				date = DateTime.Parse(info.ToString().Trim());
			}
			catch
			{
				return date;
			}
			return date;
		}
        /// <summary>
        /// Convert DateTime data type
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DateTime? GetDateTimeReturnEmtpyIfNull(string info)
        {
            if (info != "" && info != null)
            {
                return GetDateTime(info);
            }
            else return null;
        }
		/// <summary>
		/// Convert Date
		/// </summary>
		public static DateTime GetDate(object a_object)
		{
			if (a_object == null || a_object.ToString() == "")
			{
				return DateTime.MinValue;
			}
			else
			{
				try
				{
					DateTime dRet = DateTime.Parse(a_object.ToString(), DATEFORMAT, DateTimeStyles.NoCurrentDateDefault);
					if (dRet.Year < 1753 || dRet.Year > 9999)
					{
						dRet = DateTime.MinValue;
					}
					return dRet;
				}
				catch
				{
					return DateTime.MinValue;
				}

			}
		}

        /// <summary>
        /// Show friendly time format.
        /// </summary>
        /// <param name="_sourceDate"></param>
        /// <returns></returns>
        public static DateTime GetShortDate(DateTime _sourceDate)
        {
            return TypeHelper.GetDate(_sourceDate.ToShortDateString());
        }

		/// <summary>
		/// Show friendly time format.
		/// </summary>
		/// <param name="a_object"></param>
		/// <returns></returns>
		public static string GetTimeOnView(object a_object)
		{
			return GetDate(a_object).ToShortTimeString();
		}
		/// <summary>
		/// Show friendly Date format.
		/// </summary>
		/// <param name="a_object"></param>
		/// <returns></returns>
		public static string GetDateOnView(object a_object)
		{
			string sret = GetDate(a_object).ToShortDateString();
			if (sret == "1/1/0001")
			{
				return string.Empty;
			}
			else
			{
				return sret;	
			}
		}
        /// <summary>
        /// Show friendly Date and Time format.
        /// </summary>
        /// <param name="a_object"></param>
        /// <returns></returns>
        public static string GetDateTimeOnView(object a_object)
        {
            string sret = GetDate(a_object).ToShortDateString();
            if (sret == "1/1/0001")
            {
                return string.Empty;
            }
            else
            {
                string am = (GetDateTime(a_object).Hour > 12) ? "PM" : "AM";
                int hour = (GetDateTime(a_object).Hour > 12) ? (GetDateTime(a_object).Hour - 12) : GetDateTime(a_object).Hour;
                sret += string.Format(" {0:00}:{1:00} {2}", hour, GetDateTime(a_object).Minute, am);
                return sret;
            }
        }
		/// <summary>
		/// Show friendly Date format.
		/// </summary>
		/// <param name="a_object"></param>
		/// <returns></returns>
		public static string GetDateOnViewEx(object a_object)
		{
			string sret = GetDate(a_object).ToString("MM/dd/yyyy");
			if (sret == "01/01/0001")
			{
				return string.Empty;
			}
			else
			{
				return sret;
			}
		}
        /// <summary>
        /// Show standard Currency format
        /// </summary>
        /// <param name="a_Object"></param>
        /// <returns></returns>
        public static string GetCurrencyOnView(object a_Object)
        {
            if (a_Object == null || a_Object.ToString().Equals(""))
                return null;
            
            return String.Format("{0:c}", GetDecimal(a_Object));
        }

		/// <summary>
		/// <summary>
		/// Format Date in to view date type.
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string FormatDate(DateTime date)
		{
			DateTimeFormatInfo format = new DateTimeFormatInfo();
			if (date == DateTime.MinValue) return "";
			return date.ToShortDateString();
		}

		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string FormatDateMMddyyyyHHMM(DateTime date)
		{
			return date.ToShortDateString() + " " + date.ToShortTimeString();
		}

		/// <summary>
		/// Use for export data
		/// <summary>
		/// Use for export data
		/// Ex:20050223
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string FormatDateYYYYMMDD(DateTime date)
		{
			DateTimeFormatInfo format = new DateTimeFormatInfo();
			return date.ToString("yyyyMMdd");
			;
		}

		public static string FormatTime(DateTime date)
		{
			if (date == DateTime.MinValue) return string.Empty;
			else return date.ToShortTimeString();
		}

		/// <summary>
		/// use for export data
		/// Ex: 1650PM
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string FormatTimeHHMMA(DateTime date)
		{
			string result = "";
			if (date == DateTime.MinValue) return string.Empty;
			else result = date.ToString("hhmmtt");
			result = result.Replace(":", "");
			result = result.Replace(" ", "");
			return result;
		}
		/// <summary>
		/// For Parse to long data type
		/// </summary>
		public static object IsNullData(string data, object valueIsNull)
		{
			if (data == null)
				return valueIsNull;
			else if (data.Length == 0)
				return valueIsNull;
			return data;
		}

		/// <summary>
		/// Function to convert a DateValue in to SQL DateTime data type.
		/// </summary>
		/// <param name="date">Date to be converted</param>
		/// <returns>Object presenting SQL DateTime</returns>
		public static object ToDateOrNull(DateTime date)
		{
			if (date == DateTime.MinValue)
			{
				return null;
			}
			return date;
		}

        /// <summary>
		/// Function to convert a DateValue in to SQL DateTime data type.
		/// </summary>
		/// <param name="date">Date to be converted</param>
		/// <returns>Object presenting SQL DateTime</returns>
		public static object ToDateOrNull(object a_Object)
		{
            DateTime date = (a_Object != null && a_Object.ToString() != "") ? DateTime.Parse(a_Object.ToString().Trim()) : DateTime.MinValue;

            if (date == DateTime.MinValue)
			{
				return null;
			}
            return date;
		}

		/// <summary>
		/// Function to convert a String value in to a string or NULL varchar type
		/// before saving it to syste,
		/// </summary>
		/// <param name="aObj">aObj to be converted</param>
		/// <returns>Object presenting varchar</returns>
		public static object ToVarcharOrNull(object aObj)
		{
			if (aObj == null || aObj.ToString().Equals(String.Empty))
			{
				return null;
			}
			else
			{
				return aObj;
			}

		}

		/// <summary>
		/// Function to convert a Bool value in to a string or NULL varchar type
		/// before saving it to syste,
		/// </summary>
		/// <param name="obj">obj to be converted</param>
		/// <returns>Object presenting varchar</returns>
		public static object ToBoolOrNull(object obj)
		{
			if (obj == null || obj.ToString().Equals(String.Empty))
			{
				return null;
			}
			else
			{
				return obj;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj">Object to be parsed</param>
		/// <returns>Number object or Null if presented 0</returns>
		public static object ToNumberOrNull(object obj)
		{
			if (obj == null)
			{
				return null;
			}
            return obj;
		}
        /// <summary>
        /// Convert long value into SQL data type.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object ToDecimalOrNull(object obj)
        {
            if (obj == null || obj.ToString().Equals(""))
			{
				return null;
			}
            string str = obj.ToString().Replace("$", "");
			return GetDecimal(str.Replace("%", ""));
        }
		/// <summary>
		/// Convert long value into SQL data type.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static object ToLongOrNull(long key)
		{
			if (key <= 0)
			{
				return null;
			}
			return key;
		}
		/// <summary>
		/// Convert long value into SQL data type.
		/// </summary>
		/// <param name="obj">object hold int value</param>
		/// <returns>int object</returns>
		public static object ToIntOrNull(object obj)
		{
			if (GetInt(obj) <= 0)
			{
				return null;
			}
			return obj;
		}
		/// <summary>
		/// Convert an any object to string
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ToString(object obj)
		{
			if (obj != null)
			{
				return string.Format("{0}", obj);
			}
			return string.Empty;
		}

        /// <summary>
        /// convert an any object to percent
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPercent(object a_Object)
        {
            NumberFormatInfo info = NumberFormatInfo.InvariantInfo;
            if (a_Object == null || a_Object.ToString().Equals(String.Empty))
            {
                return null;
            }
            return String.Format("{0}", a_Object + info.PercentSymbol);
        }

        /// <summary>
        /// convert object to decimal with 10 digit
        /// </summary>
        /// <param name="a_Object"></param>
        /// <returns></returns>
        public static string GetDecimalDigit(object a_Object)
        {            
            if (a_Object == null || a_Object.ToString().Equals(String.Empty))
            {
                return null;
            }            
            return GetDecimal(a_Object).ToString("N10");
        }

		public static DateTime ToDbDate(object date)
		{
			if (GetDate(date) == DateTime.MinValue)
			{
				return GetDate("02/02/1753");
			}
			else
			{
				return GetDate(date);
			}
		}
        
        public static bool IsWeekend(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int Sum(int obj1, int obj2)
        {
            int sum = 0;
            sum = obj1 + obj2;
            return sum;
        }
    }

	public class ArrayListEx : ArrayList
	{
		private int total = 0;

		public ArrayListEx()
		{
		}

		public ArrayListEx(ICollection list, int count): base(list)
		{
			total = count;
		}

		public int Total
		{
			get { return total; }
			set { total = value; }
		}
	}

    public class ArrayListEp : ArrayListEx
    {
        private int fullTotal = 0;

        public ArrayListEp(ICollection list, int total, int full)
            : base(list, total)
        {
            fullTotal = full;
        }

        public int FullTotal
        {
            get { return fullTotal; }
            set { fullTotal = value; }
        }
    }
}