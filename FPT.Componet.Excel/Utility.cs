using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

namespace FPT.Component.ExcelPlus
{
    public class Utility
    {
        const string EXCEL2003_EXTENSION = ".XLS";
        public static void SaveToExcelFile(System.Data.DataTable table, string filePath, bool addHeader)
        {
            IExcelWriter writer = null;
            string ext = System.IO.Path.GetExtension(filePath).ToUpper();
            if (ext.Equals(EXCEL2003_EXTENSION))
            {
                writer = ExcelFactory.CreateExcelWriter(ExcelVersion.Excel2003);
            }
            else
            {
                writer = ExcelFactory.CreateExcelWriter(ExcelVersion.Excel2007);
            }
            System.Data.DataTable exportTable = new System.Data.DataTable();
            if (addHeader)
            {
                exportTable.TableName = table.TableName;
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    exportTable.Columns.Add(table.Columns[i].ColumnName);
                }
                System.Data.DataRow row = exportTable.NewRow();
                foreach (System.Data.DataColumn col in exportTable.Columns)
                {
                    row[col] = col.ColumnName;
                }
                exportTable.Rows.Add(row);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = exportTable.NewRow();
                    foreach (System.Data.DataColumn col in exportTable.Columns)
                    {
                        row[col] = table.Rows[i][col.ColumnName].ToString();
                    }
                    exportTable.Rows.Add(row);
                }
            }
            else
            {
                exportTable = table.Copy();
            }
            writer.CreateWorkBook(filePath);
            writer.WriteToWorkBook(new List<System.Data.DataTable>(new System.Data.DataTable[] { exportTable }));
            writer.CloseWorkBook();
        }

        public static void SaveToExcelFile(Dictionary<string, System.Data.DataTable> tableCollection, string filePath, bool addHeader)
        {
            IExcelWriter writer = null;
            string ext = System.IO.Path.GetExtension(filePath).ToUpper();
            if (ext.Equals(EXCEL2003_EXTENSION))
            {
                writer = ExcelFactory.CreateExcelWriter(ExcelVersion.Excel2003);
            }
            else
            {
                writer = ExcelFactory.CreateExcelWriter(ExcelVersion.Excel2007);
            }
            IList<System.Data.DataTable> tables = new List<System.Data.DataTable>();
            if (addHeader)
            {
                foreach (string key in tableCollection.Keys)
                {
                    System.Data.DataTable tab = new System.Data.DataTable();
                    tab.TableName = tableCollection[key].TableName;
                    for (int i = 0; i < tableCollection[key].Columns.Count; i++)
                    {
                        tab.Columns.Add(tableCollection[key].Columns[i].ColumnName);
                    }
                    System.Data.DataRow row = tab.NewRow();
                    foreach (System.Data.DataColumn col in tab.Columns)
                    {
                        row[col] = col.ColumnName;
                    }
                    tab.Rows.Add(row);
                    for (int i = 0; i < tableCollection[key].Rows.Count; i++)
                    {
                        row = tab.NewRow();
                        foreach (System.Data.DataColumn col in tab.Columns)
                        {
                            row[col] = tableCollection[key].Rows[i][col.ColumnName].ToString();
                        }
                        tab.Rows.Add(row);
                    }
                    tables.Add(tab);
                }
            }
            else
            {
                foreach (string key in tableCollection.Keys)
                {
                    tables.Add(tableCollection[key]);
                }
            }
            writer.CreateWorkBook(filePath);
            writer.WriteToWorkBook(tables);
            writer.CloseWorkBook();
        }

        public static List<int[]> GetDuplicateRows(ISheet sheet, List<int> keyIndexes, int minRow, int maxRow, List<int> skipRowIndexes)
        {
            List<int[]> dupList = new List<int[]>();

            List<KeyComparer> keyTable = new List<KeyComparer>();

            for (int i = minRow; i <= maxRow; i++)
            {
                if (skipRowIndexes.Contains(i)) continue;
                KeyComparer key = new KeyComparer(sheet, i, keyIndexes);
                keyTable.Add(key);
            }

            return GetDuplicateRows(keyTable);
        }

        public static List<int[]> GetDuplicateRows(List<KeyComparer> keyTable)
        {
            List<int[]> dupList = new List<int[]>();

            keyTable.Sort();
            List<int> tmp = new List<int>();
            bool isFirst = true;
            for (int i = 0; i < keyTable.Count; i++)
            {
                for (int j = i + 1; j < keyTable.Count; j++)
                {
                    if (keyTable[i].CompareTo(keyTable[j]) == 0)
                    {
                        if (isFirst)
                        {
                            tmp.Add(keyTable[i].Index);
                            isFirst = false;
                        }
                        tmp.Add(keyTable[j].Index);
                        i++;
                    }
                    else
                    {
                        isFirst = true;
                        if (tmp.Count > 0)
                        {
                            tmp.Sort();
                            dupList.Add(tmp.ToArray());
                            tmp.Clear();
                        }
                        break;
                    }
                }
            }
            if (tmp.Count > 0)
            {
                dupList.Add(tmp.ToArray());
            }

            return dupList;
        }

        public static bool IsNonNegative(object value)
        {
            decimal dummy = new decimal();
            if (!decimal.TryParse(value.ToString(), out dummy))
            {
                return false;
            }
            return (dummy >= 0);
        }

        public static bool IsNegative(object value)
        {
            decimal dummy = new decimal();
            if (!decimal.TryParse(value.ToString(), out dummy))
            {
                return false;
            }
            return (dummy < 0);
        }

        public static bool IsPositive(object value)
        {
            decimal dummy = new decimal();
            if (!decimal.TryParse(value.ToString(), out dummy))
            {
                return false;
            }
            return (dummy > 0);
        }

        public static bool IsNumeric(object value)
        {
            decimal dummy = new decimal();
            return decimal.TryParse(value.ToString(), out dummy);
        }

        public static bool CheckIntValue(object obj, int value)
        {
            int tmp = 0;
            if (obj == null) return false;
            bool result = int.TryParse(obj.ToString().Trim(), out tmp);
            result = result && (tmp == value);
            return result;
        }

        /// <summary>
        /// Check if type is numeric type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }


        public static bool IsNumeric(object strValue, out decimal value)
        {
            return decimal.TryParse(strValue.ToString(), out value);
        }

        public static object GetNumericValue(object value)
        {
            return decimal.Parse(value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValueFromString(Object obj, String propertyName, String str, String format)
        {
            if (obj == null)
                return;
            Type type = obj.GetType();

            PropertyInfo pi = type.GetProperty(propertyName.Trim());

            if (pi == null)
                return;

            if (pi.PropertyType == typeof(DateTime) && !string.IsNullOrEmpty(format) && !string.IsNullOrEmpty(str))
            {
                DateTime date = DateTime.ParseExact(str, format, null);
                if (date != DateTime.MinValue)
                {
                    pi.SetValue(obj, date, null);
                }
            }
            else if (pi.PropertyType == typeof(DateTime?) && !string.IsNullOrEmpty(format) && !string.IsNullOrEmpty(str))
            {
                DateTime date = DateTime.ParseExact(str, format, null);
                DateTime? date2 = new DateTime?(date);
                pi.SetValue(obj, date2, null);
            }
            else if (pi.PropertyType != typeof(DateTime) && pi.PropertyType != typeof(DateTime?))
            {
                if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (str == null)
                    {
                        pi.SetValue(obj, null, null);
                    }
                    object value = Convert.ChangeType(str, pi.PropertyType.GetGenericArguments()[0]);
                    pi.SetValue(obj, value, null);
                }
                else
                {
                    object value = Convert.ChangeType(str, pi.PropertyType);
                    pi.SetValue(obj, value, null);
                }
            }
        }

        public static Object ChangeType(Object value, Type type, string format)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }

            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType, format);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }

            if (type == typeof(DateTime) && !string.IsNullOrEmpty(value.ToString()))
            {
                if (string.IsNullOrEmpty(format))
                    return DateTime.Parse(value.ToString());
                DateTime date = DateTime.ParseExact(value.ToString(), format, null);
                return Convert.ChangeType(date, type);
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);

            return Convert.ChangeType(value, type);
        }

        public static bool IsBlankOrNumeric(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            return IsNumeric(value);
        }

        public static int GetExcelColumnAddress(string address)
        {
            int result = 0;
            address = address.ToUpper();
            for (int i = 0; i < address.Length; i++)
            {
                if (address[i] < 'A' || address[i] > 'Z')
                    return 0;
                result = result * 26 + (address[i] - 'A') + 1;
            }
            return result;
        }

        public static string GetExcelColumnAddress(int column)
        {
            string result = string.Empty;
            while (column > 0)
            {
                result = (char)(column % 26 - 1 + 'A') + result;
                column = column / 26;
            }

            return result;
        }

        public static System.Data.DataTable InsertToDataTable(System.Data.DataTable source, System.Data.DataTable sink, string dateFormat)
        {
            foreach (System.Data.DataRow row in source.Rows)
            {
                System.Data.DataRow dR = sink.NewRow();
                bool insert = false;
                foreach (System.Data.DataColumn col in sink.Columns)
                {
                    if (source.Columns.Contains(col.ColumnName))
                    {
                        dR[col.ColumnName] = Utility.ChangeType(row[col.ColumnName], col.DataType, dateFormat);
                        insert = true;
                    }
                }
                if (insert) sink.Rows.Add(dR);
            }
            return sink;
        }

        public static System.Data.DataTable InsertToDataTable(System.Data.DataTable source, System.Data.DataTable sink)
        {
            foreach (System.Data.DataRow row in source.Rows)
            {
                System.Data.DataRow dR = sink.NewRow();
                bool insert = false;
                foreach (System.Data.DataColumn col in sink.Columns)
                {
                    if (source.Columns.Contains(col.ColumnName))
                    {
                        dR[col.ColumnName] = Utility.ChangeType(row[col.ColumnName], col.DataType, string.Empty);
                        insert = true;
                    }
                }
                if (insert) sink.Rows.Add(dR);
            }
            return sink;
        }

        public static System.Data.DataTable InsertToDataTable(System.Data.DataRow source, System.Data.DataTable sink)
        {
            System.Data.DataRow row = sink.NewRow();
            foreach (System.Data.DataColumn col in sink.Columns)
            {
                if (source.Table.Columns.Contains(col.ColumnName))
                    row[col] = source[col.ColumnName];
            }
            sink.Rows.Add(row);
            return sink;
        }

        public static void WriteToSheet(ISheet sheet, System.Data.DataTable table)
        {
        }

        public static FCellAddress GetExcelAddress(string address)
        {
            FCellAddress result = new FCellAddress();
            Regex regex = new Regex("^(?<col>[A-Z]+)(?<row>[0-9]+)$");
            address = address.ToUpper().Trim();
            Match m = regex.Match(address);
            if (m.Success)
            {
                result.Row = int.Parse(m.Groups["row"].Value);
                result.Column = Utility.GetExcelColumnAddress(m.Groups["col"].Value);
            }
            return result;
        }

        public static string GetExcelAddress(FCellAddress address)
        {
            string col = Utility.GetExcelColumnAddress(address.Column);
            return col + address.Row.ToString();
        }

        public static string GetExcelAddress(int row, int column)
        {
            string col = Utility.GetExcelColumnAddress(column);
            return col + row.ToString();
        }
    }
}
