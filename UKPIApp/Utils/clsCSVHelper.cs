using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;

namespace UKPI.Utils
{
    public class clsCSVHelper
    {
        public static void WriteCSV(string PathAndFileName, DataTable dt)
        {
            //Create the CSV file
            StreamWriter sw = new StreamWriter(PathAndFileName);
            
            //write header
            for (int i = 0; i < dt.Columns.Count;i++ )
            {
                sw.Write(dt.Columns[i]);
                if (i<dt.Columns.Count-1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);

            //write all rows
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count;i++ )
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        if (row[i].ToString().Contains(","))
                        {
                            sw.Write("\"" + row[i].ToString().Trim() + "\"");
                        }
                        else
                        {
                            sw.Write(row[i].ToString().Trim());
                        }
                    }
                    if (i<dt.Columns.Count-1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void WriteCSV(string PathAndFileName, DataTable dt, Dictionary<string, string> headerMapping)
        {
            //Create the CSV file
            StreamWriter sw = new StreamWriter(PathAndFileName);

            //write header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (headerMapping == null)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                }
                else
                {
                    sw.Write(headerMapping[dt.Columns[i].ColumnName]);
                }

                if (i < dt.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);

            //write all rows
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        sw.Write(row[i].ToString());
                    }
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void WriteCSV_ExcludeFields(string PathAndFileName, DataTable dt, Dictionary<string, string> headerMapping, 
            params string[] excludedFieldNames)
        {
            List<string> excludeFieldNames = AddToList(excludedFieldNames);

            //Create the CSV file
            StreamWriter sw = new StreamWriter(PathAndFileName);

            //write header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (!excludeFieldNames.Contains(dt.Columns[i].ColumnName))
                {
                    if (headerMapping == null)
                    {
                        sw.Write(dt.Columns[i].ColumnName);
                    }
                    else
                    {
                        sw.Write(headerMapping[dt.Columns[i].ColumnName]);
                    }

                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
            }
            sw.Write(sw.NewLine);

            //write all rows
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!excludeFieldNames.Contains(dt.Columns[i].ColumnName))
                    {
                        if (!Convert.IsDBNull(row[i]))
                        {
                            sw.Write(row[i].ToString());
                        }
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


        public static void WriteCSV_SelectFields(string PathAndFileName, DataTable dt, Dictionary<string, string> headerMapping,
          params string[] selectedFieldNames)
        {
            //Create the CSV file
            StreamWriter sw = new StreamWriter(PathAndFileName);
            
            //write header
            for (int i = 0; i < selectedFieldNames.Length; i++)
            {
                string fieldName = selectedFieldNames[i];

                if (dt.Columns.Contains(fieldName))
                {
                    if (headerMapping == null)
                    {
                        sw.Write(fieldName);
                    }
                    else
                    {
                        sw.Write(headerMapping[fieldName]);
                    }

                    if (i < selectedFieldNames.Length - 1)
                    {
                        sw.Write(",");
                    }
                }
            }
            sw.Write(sw.NewLine);

            //write all rows
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < selectedFieldNames.Length; i++)
                {
                    string fieldName = selectedFieldNames[i];

                    if (dt.Columns.Contains(fieldName))
                    {
                        if (!Convert.IsDBNull(row[fieldName]))
                        {
                            sw.Write(ConvertStringToExport(row[fieldName].ToString()));
                        }
                        if (i < selectedFieldNames.Length - 1)
                        {
                            sw.Write(",");
                        }
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


        public static void WriteCSV_SelectFieldsStores(string PathAndFileName, DataTable dt, Dictionary<string, string> headerMapping,
           Dictionary<string, string> headerRegion, params string[] selectedFieldNames)
        {
            //Create the CSV file
            StreamWriter sw = new StreamWriter(PathAndFileName);
            //Write header Region
            for (int i = 0; i < selectedFieldNames.Length; i++)
            {
                string fieldName = selectedFieldNames[i];

                if (dt.Columns.Contains(fieldName))
                {
                    if (headerRegion == null)
                    {
                        sw.Write(fieldName);
                    }
                    else
                    {
                        sw.Write(headerRegion[fieldName]);
                    }

                    if (i < selectedFieldNames.Length - 1)
                    {
                        sw.Write(",");
                    }
                }
            }
            sw.Write(sw.NewLine);


            //write header
            for (int i = 0; i < selectedFieldNames.Length; i++)
            {
                string fieldName = selectedFieldNames[i];

                if (dt.Columns.Contains(fieldName))
                {
                    if (headerMapping == null)
                    {
                        sw.Write(fieldName);
                    }
                    else
                    {
                        sw.Write(headerMapping[fieldName]);
                    }

                    if (i < selectedFieldNames.Length - 1)
                    {
                        sw.Write(",");
                    }
                }
            }
            sw.Write(sw.NewLine);

            //write all rows
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < selectedFieldNames.Length; i++)
                {
                    string fieldName = selectedFieldNames[i];

                    if (dt.Columns.Contains(fieldName))
                    {
                        if (!Convert.IsDBNull(row[fieldName]))
                        {
                            sw.Write(ConvertStringToExport(row[fieldName].ToString()));
                        }
                        if (i < selectedFieldNames.Length - 1)
                        {
                            sw.Write(",");
                        }
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        /// <summary>
        /// process string before export to csv
        /// Creator: Sonlv
        /// Create Date: 14/04/2010
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static String ConvertStringToExport(String value)        
        {
            value = value.Replace("\"", "\"\"");
            if(value.IndexOf(",") >= 0)
            {
                value = '"' + value  + '"';
            }
            return value;
        }

        private static List<string> AddToList(string[] values)
        {
            List<string> result = new List<string>();

            foreach (string value in values)
            {
                result.Add(value);
            }

            return result;
        }

        /*public static void WriteDataTableToCsvFile(string PathAndFileName, DataTable dt, params string[] Options)
         {
             //variables
               string separator;
               if (Options.Length > 0)
               {
                separator = Options[0];
               }
               else
               {
                separator = ","; //default
               }
               string quote = "'";

               //create CSV file
               StreamWriter sw = new StreamWriter(PathAndFileName);

               //write header line
               for (int i = 0; i < dt.Columns.Count; i++)
               {
                sw.Write(dt.Columns[i]);
                if (i < dt.Columns.Count-1)
                {
                 sw.Write(separator);
                }
               }
               sw.Write(sw.NewLine);

               //write rows
               foreach (DataRow dr in dt.Rows)
               {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                 if (!Convert.IsDBNull(dr[i]))
                 {
                  string data = dr[i].ToString();
                  data = data.Replace(",", ";");
                  sw.Write(quote + data + quote);
                 }
                 if (i < dt.Columns.Count-1)
                 {
                  sw.Write(separator);
                 }
                }
                sw.Write(sw.NewLine);
               }
               sw.Close();
         }*/

    }
   
}
