using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace UKPI.Core
{
    public class DataAccess
    {
        #region constant
        const string COL_ISKEY = "IsKey";
        const string COL_IS_IDENTITY = "IsIdentity";
        const string COL_ISAUTOINCREMENT = "IsAutoIncrement";
        const string COL_COLUMN_NAME = "ColumnName";

        public const string DB_LIST_SEPERATOR = ";";
        public const string DB_FIELD_SEPERATOR = ",";
        public const int DB_SP_MAX_PARAM_LENGTH = 4000;
        #endregion


        protected string conStr;

        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(DataAccess));

        public DataAccess(string connectionString)
        {
            conStr = connectionString;
        }

        public DataTable CopyTableWithTemplate(DataTable source, string xmlMappingFile, string sourceColumn, string destinationColumn, string dataTypeColumn)
        {
            DataTable mapping = new DataTable();
            DataSet tmp = new DataSet();
            tmp.ReadXml(xmlMappingFile);
            if (tmp.Tables.Count > 0)
            {
                mapping = tmp.Tables[0];
            }
            DataTable result = new DataTable();
            Dictionary<string, string> columns = new Dictionary<string, string>();
            foreach (DataRow row in mapping.Rows)
            {
                string name = row[destinationColumn].ToString();
                string type = row[dataTypeColumn].ToString();
                try
                {
                    Type t = Type.GetType(type);
                    result.Columns.Add(name, t);
                }
                catch
                {
                    result.Columns.Add(name);
                }
                if (!columns.ContainsKey(name.ToUpper().Trim()))
                    columns.Add(name.Trim(), row[sourceColumn].ToString());
            }

            foreach (DataRow row in source.Rows)
            {
                DataRow des = result.NewRow();
                foreach (KeyValuePair<string, string> kvp in columns)
                {
                    des[kvp.Key] = Utilities.ChangeType(row[kvp.Value], result.Columns[kvp.Key].DataType, string.Empty);
                }
                result.Rows.Add(des);
            }

            return result;
        }

        public List<string> BuildSqlParamStrings(List<string> items)
        {
            List<string> result = new List<string>();
            string tmp = string.Empty;

            foreach (string item in items)
            {
                if (tmp.Length + item.Length + DB_FIELD_SEPERATOR.Length > DB_SP_MAX_PARAM_LENGTH)
                {
                    tmp = tmp.Trim(DB_FIELD_SEPERATOR.ToCharArray());
                    result.Add(tmp);
                    tmp = string.Empty;
                }
                tmp += item + DB_FIELD_SEPERATOR;
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                tmp = tmp.Trim(DB_FIELD_SEPERATOR.ToCharArray());
                result.Add(tmp);
            }

            return result;
        }

        public List<SqlParameter[]> BuildParameter(DataTable source, string sinkTableName, string xmlMappingFile, string sourceColumn, string sinkColumn, string dataTypeColumn)
        {
            List<SqlParameter[]> result = new List<SqlParameter[]>();
            DataTable tmp = CopyTableWithTemplate(source, xmlMappingFile, sourceColumn, sinkColumn, dataTypeColumn);
            List<string> ignoreColumns = GetAutoColumns(sinkTableName);
            foreach (DataRow row in tmp.Rows)
            {
                result.Add(BuildColumnParameter(row, ignoreColumns));
            }
            return result;
        }

        public SqlParameter[] BuildColumnParameter(DataRow row, List<string> ignoredColumns)
        {
            List<SqlParameter> result = new List<SqlParameter>();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                DataColumn col = row.Table.Columns[i];
                if (!ignoredColumns.Contains(col.ColumnName))
                {
                    SqlParameter p = new SqlParameter(col.ColumnName, row[col.ColumnName]);
                    p.SourceColumn = col.ColumnName;
                    result.Add(p);
                }
            }
            return result.ToArray();
        }

        public List<string> GetAutoColumns(string tableName)
        {
            List<string> result = new List<string>();
            DataTable schema = GetSchemaInfoTable(tableName);
            foreach (DataRow row in schema.Rows)
            {
                bool id = bool.Parse(row[COL_IS_IDENTITY].ToString());
                bool auto = bool.Parse(row[COL_ISAUTOINCREMENT].ToString());
                if (id || auto)
                {
                    result.Add(row[COL_COLUMN_NAME].ToString());
                }
            }
            return result;
        }

        public DataTable InsertToDataTable(DataRow source, DataTable sink)
        {
            DataRow row = sink.NewRow();
            foreach (DataColumn col in sink.Columns)
            {
                if (source.Table.Columns.Contains(col.ColumnName))
                    row[col] = source[col.ColumnName];
            }
            sink.Rows.Add(row);
            return sink;
        }

        public DataTable InsertToDataTable(DataTable source, DataTable sink)
        {
            foreach (DataRow row in source.Rows)
            {
                DataRow dR = sink.NewRow();
                bool insert = false;
                foreach (DataColumn col in sink.Columns)
                {
                    if (source.Columns.Contains(col.ColumnName))
                    {
                        dR[col.ColumnName] = Utilities.ChangeType(row[col.ColumnName], col.DataType, string.Empty);
                        insert = true;
                    }
                }
                if (insert) sink.Rows.Add(dR);
            }
            return sink;
        }

        public bool BulkInsert(DataTable table, string tableName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(con))
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            copy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        }
                        copy.DestinationTableName = tableName;
                        copy.WriteToServer(table);
                    }
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                //string strEx = ex.StackTrace + ex.Message;
                log.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Bulk insent one table to database
        /// Author: QuangVD
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        /// <param name="columnNames">List of column name in DataTable, must be the same as table in database</param>
        public bool BulkInsert(DataTable table, string tableName, List<string> columnNames)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conStr))
                {
                    cn.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                    {
                        foreach (string column in columnNames)
                        {
                            copy.ColumnMappings.Add(column, column);
                        }
                        copy.DestinationTableName = tableName;
                        copy.WriteToServer(table);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //string strEx = ex.StackTrace + ex.Message;
                log.Error(ex);
                return false;
            }
        }

        public DataTable GetSchemaInfoTable(string tableName)
        {
            return SqlHelper.GetSchemaTable(conStr, tableName);
        }

        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteReader(conStr, commandType, commandText, commandParameters);
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(conStr, commandType, commandText, commandParameters).Tables[0];
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(conStr, commandType, commandText, commandParameters);
        }

        public void ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlHelper.ExecuteNonQuery(conStr, commandType, commandText, commandParameters);
        }
        public void ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlHelper.ExecuteNonQuery(transaction, commandType, commandText, commandParameters);
        }
        public int ExecuteStoredProcedure(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(conStr, commandType, commandText, commandParameters);
        }
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar(conStr, commandType, commandText);
        }
        ///
        public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar(conStr, commandType, commandText, commandParameters);
        }
        /// <summary>
        public object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar(transaction, commandType, commandText, commandParameters);
        }
        //
        /// <param name="commandText"></param>
        public void ExecuteNonQuery(CommandType commandType, string commandText)
        {
            SqlHelper.ExecuteNonQuery(conStr, commandType, commandText);
        }
        public int ExecuteStoredProcedure(SqlConnection conn, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(conn, commandType, commandText, commandParameters);
        }
    }
}
