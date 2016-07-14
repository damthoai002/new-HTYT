using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UKPI.Utils;

namespace UKPI.Utils
{
    public class DataServices
    {
        /// <summary>
        /// Get connection string from App config
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static DataTable GetSchemaTable(string tableName)
        {
            return SqlHelper.GetSchemaTable(GetConnectionString(), tableName);
        }
        
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteReader(GetConnectionString(), commandType, commandText, commandParameters);
        }
        
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(GetConnectionString(), commandType, commandText, commandParameters).Tables[0];
        }

        public static DataTable ExecuteDataTable(SqlConnection cn,CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(cn,GetConnectionString(), commandType, commandText, commandParameters).Tables[0];
        }

        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(GetConnectionString(), commandType, commandText, commandParameters);
        }

        public static void  ExecuteNonQuery( CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {    
            SqlHelper.ExecuteNonQuery(GetConnectionString(), commandType, commandText, commandParameters);
        }
        public static void ExecuteNonQuery(SqlTransaction transaction,CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlHelper.ExecuteNonQuery(transaction, commandType, commandText, commandParameters);
        }
        public static int ExecuteStoredProcedure(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(GetConnectionString(), commandType, commandText, commandParameters);            
        }
        public static object ExecuteScalar(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar(GetConnectionString(), commandType, commandText);
        }
        ///
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar(GetConnectionString(), commandType, commandText, commandParameters);
        }
        /// <summary>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar(transaction, commandType, commandText, commandParameters);
        }
        //
              /// <param name="commandText"></param>
        public static void ExecuteNonQuery(CommandType commandType, string commandText)
        {
            SqlHelper.ExecuteNonQuery(GetConnectionString(), commandType, commandText);
        }
        public static int ExecuteStoredProcedure(SqlConnection conn ,CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(conn, commandType, commandText, commandParameters);
        }
    }
}
