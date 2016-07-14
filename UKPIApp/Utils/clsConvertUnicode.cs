using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FSoft.UnicodeConvertLibs;

namespace UKPI.Utils
{
    public class clsConvertUnicode
    {
        //convert data to datatable "Không dấu"

        public  void ConvertDataTable(System.Data.DataTable dt)
        {
            string str = string.Empty;
            string strnew = string.Empty;
            try
            {
                bool result = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Unsignedword"]);
                if (result == true)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            //if data type int no convert 
                            if (col.DataType == typeof(Int64) || col.DataType == typeof(Int32) || col.DataType == typeof(Int16))
                                continue;
                            //if data type Datetime no convert 
                            else if (col.DataType != typeof(DateTime))
                            {
                                str = row[col].ToString();
                                     
                                if (string.IsNullOrEmpty(str))
                                {
                                    row[col] = str;
                                }
                                else
                                    if (col.ColumnName == "STORE_CODE")
                                    {
                                        row[col] = str;
                                    }
                                    else
                                    {
                                        strnew = Vietnamese(str);
                                        row[col] = strnew;
                                    }
                            }
                            //if (col.DataType == typeof(Int32) || col.DataType != typeof(DateTime))
                            //    continue;
                            ////if data type Datetime, int, float no convert 
                            //else 
                            //{
                            //    str = row[col].ToString();

                            //    if (string.IsNullOrEmpty(str))
                            //    {
                            //        row[col] = str;
                            //    }
                            //    else
                            //        if (col.ColumnName == "STORE_CODE")
                            //        {
                            //            row[col] = str;
                            //        }
                            //        else
                            //        {
                            //            strnew = Vietnamese(str);
                            //            row[col] = strnew;
                            //        }
                            //}
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {     
                throw ex;
            }
        }

        //Convert "Có dấu" to "Không dấu"
        public  string Vietnamese(string str)
        {
            Dictionary<string, string> VietName = new Dictionary<string, string>();
            string strnew = string.Empty;
            string strnewsum = string.Empty;
            //A 

            VietName.Add("Á", "A");
            VietName.Add("À", "A");
            VietName.Add("Ạ", "A");
            VietName.Add("Ả", "A");
            VietName.Add("Ã", "A");

            VietName.Add("Â", "A");
            VietName.Add("Ấ", "A");
            VietName.Add("Ầ", "A");
            VietName.Add("Ậ", "A");
            VietName.Add("Ẩ", "A");
            VietName.Add("Ẫ", "A");

            VietName.Add("Ă", "A");
            VietName.Add("Ắ", "A");
            VietName.Add("Ằ", "A");
            VietName.Add("Ặ", "A");
            VietName.Add("Ẳ", "A");
            VietName.Add("Ẵ", "A");

            //a

            VietName.Add("á", "a");
            VietName.Add("à", "a");
            VietName.Add("ạ", "a");
            VietName.Add("ả", "a");
            VietName.Add("ã", "a");

            VietName.Add("â", "a");
            VietName.Add("ấ", "a");
            VietName.Add("ầ", "a");
            VietName.Add("ậ", "a");
            VietName.Add("ẩ", "a");
            VietName.Add("ẫ", "a");

            VietName.Add("ă", "a");
            VietName.Add("ắ", "a");
            VietName.Add("ằ", "a");
            VietName.Add("ặ", "a");
            VietName.Add("ẳ", "a");
            VietName.Add("ẵ", "a");


            //E

            VietName.Add("É", "E");
            VietName.Add("È", "E");
            VietName.Add("Ẹ", "E");
            VietName.Add("Ẻ", "E");
            VietName.Add("Ẽ", "E");

            VietName.Add("Ê", "E");
            VietName.Add("Ế", "E");
            VietName.Add("Ề", "E");
            VietName.Add("Ễ", "E");
            VietName.Add("Ể", "E");
            VietName.Add("Ệ", "E");


            //e

            VietName.Add("é", "e");
            VietName.Add("è", "e");
            VietName.Add("ẹ", "e");
            VietName.Add("ẻ", "e");
            VietName.Add("ẽ", "e");

            VietName.Add("ê", "e");
            VietName.Add("ế", "e");
            VietName.Add("ề", "e");
            VietName.Add("ệ", "e");
            VietName.Add("ể", "e");
            VietName.Add("ễ", "e");

            //O
            VietName.Add("Ó", "O");
            VietName.Add("Ò", "O");
            VietName.Add("Ỏ", "O");
            VietName.Add("Õ", "O");
            VietName.Add("Ọ", "O");

            VietName.Add("Ô", "O");
            VietName.Add("Ố", "O");
            VietName.Add("Ồ", "O");
            VietName.Add("Ộ", "O");
            VietName.Add("Ổ", "O");
            VietName.Add("Ỗ", "O");

            VietName.Add("Ơ", "O");
            VietName.Add("Ớ", "O");
            VietName.Add("Ờ", "O");
            VietName.Add("Ợ", "O");
            VietName.Add("Ở", "O");
            VietName.Add("Ỡ", "O");

            //o
            VietName.Add("ó", "o");
            VietName.Add("ò", "o");
            VietName.Add("ỏ", "o");
            VietName.Add("õ", "o");
            VietName.Add("ọ", "o");

            VietName.Add("ô", "o");
            VietName.Add("ố", "o");
            VietName.Add("ồ", "o");
            VietName.Add("ộ", "o");
            VietName.Add("ổ", "o");
            VietName.Add("ỗ", "o");

            VietName.Add("ơ", "o");
            VietName.Add("ớ", "o");
            VietName.Add("ờ", "o");
            VietName.Add("ợ", "o");
            VietName.Add("ở", "o");
            VietName.Add("ỡ", "o");

            //U
            VietName.Add("Ú", "U");
            VietName.Add("Ù", "U");
            VietName.Add("Ủ", "U");
            VietName.Add("Ũ", "U");
            VietName.Add("Ụ", "U");

            VietName.Add("Ư", "U");
            VietName.Add("Ứ", "U");
            VietName.Add("Ừ", "U");
            VietName.Add("Ử", "U");
            VietName.Add("Ữ", "U");
            VietName.Add("Ự", "U");

            //U
            VietName.Add("ú", "u");
            VietName.Add("ù", "u");
            VietName.Add("ủ", "u");
            VietName.Add("ũ", "u");
            VietName.Add("ụ", "u");

            VietName.Add("ư", "u");
            VietName.Add("ứ", "u");
            VietName.Add("ừ", "u");
            VietName.Add("ử", "u");
            VietName.Add("ữ", "u");
            VietName.Add("ự", "u");

            //I
            VietName.Add("Í", "I");
            VietName.Add("Ì", "I");
            VietName.Add("Ỉ", "I");
            VietName.Add("Ĩ", "I");
            VietName.Add("Ị", "I");

            //i

            VietName.Add("í", "i");
            VietName.Add("ì", "i");
            VietName.Add("ỉ", "i");
            VietName.Add("ĩ", "i");
            VietName.Add("ị", "i");

            //D
            VietName.Add("Đ", "D");
            VietName.Add("đ", "d");

            //y
            VietName.Add("Ý", "Y");
            VietName.Add("Ỳ", "Y");
            VietName.Add("Ỷ", "Y");
            VietName.Add("Ỹ", "Y");
            VietName.Add("Ỵ", "Y");

            VietName.Add("ý", "y");
            VietName.Add("ỳ", "y");
            VietName.Add("ỷ", "y");
            VietName.Add("ỹ", "y");
            VietName.Add("ỵ", "y");

            if (str is string)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    string strk = str.Substring(i, 1);
                    if (VietName.ContainsKey(strk))
                    {
                        strnew = VietName[strk];
                        strnewsum += strnew;
                    }
                    else
                    {
                        strnewsum += strk;
                    }
                }
            }

            else
            {
                strnewsum = str;
            }
            
            return strnewsum;
        }

        //DongTC

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public  void ConVertTCVN3ToUNICODE(DataTable dt)
        {
            bool result =bool.Parse(System.Configuration.ConfigurationManager.AppSettings["TCVN3TOUNICODE"]);
            
            UnicodeConverter uniConverter = new UnicodeConverter(CharsetType.TCVN3, CharsetType.UNICODE);
            if (result == true)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        //if data type date time no convert
                        if (col.DataType != typeof(DateTime))
                        {
                            string convertTo = row[col].ToString();
                            if (!string.IsNullOrEmpty(convertTo))
                            {
                                string GoUnicode = uniConverter.ConvertString(convertTo);
                                row[col] = GoUnicode;
                            }
                        }
                    }
                }
            } 
            else
            {
                return;
            }
              
        }
    }
}
