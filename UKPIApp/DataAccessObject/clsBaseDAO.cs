using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using UKPI.Utils;
using UKPI.ValueObject;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace UKPI.DataAccessObject
{
    /// <summary>
    /// Summary description for clsBaseDAO.
    /// </summary>
    /// <remarks>
    /// Author:			Nguyen Minh Duc. G3.
    /// Created date:	14/05/2006
    /// </remarks>
    public class clsBaseDAO
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(clsBaseDAO));

        // CanLV: add 11-Mar-2009: Get connection
        private static string strConn = clsCommon.GetConnectionString();
        /// <summary>
        /// Message when cannot get the connection or the connection is null
        /// </summary>
        public static string CONNECTION_ERROR = "Cannot get connection.";

        /// <summary>
        /// Errors ID When disconect to database server.
        /// </summary>
        public static long SQL_ERROR_DISCONNECT = -2147467259;


        /// <summary>
        /// 
        /// </summary>
        public clsBaseDAO()
        {
            //Init();
        }
        //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="con"></param>
        public clsBaseDAO(SqlConnection con)
        {
            m_Connection = con;
        }

        protected static SqlConnection m_Connection;

        /// <summary>
        /// Get Connection by ConnectionString
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlConnection GetConnection(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        /// <summary>
        /// Get Connection by server, database, user, password
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlConnection GetConnection(string server, string database, string user, string password)
        {
            object[] objs = new object[] { server, database, user, password };
            string connectionString = string.Format("Server={0};DataBase={1};user={2};password={3}", objs);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        /// <summary>
        /// Connection's used for the whole system.
        /// </summary>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public static SqlConnection Connection
        {
            get { return m_Connection; }
            set { m_Connection = value; }
        }

        /// <summary>
        /// Create a con by ConnectionString in Configuration and Test Connection
        /// </summary>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public static void Init()
        {
            if (m_Connection != null)
                return;

            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                con.Close();
                m_Connection = con;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw new Exception(CONNECTION_ERROR, ex);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw new Exception(CONNECTION_ERROR, ex);
            }
        }

        /// <summary>
        /// Get schema of table by table name
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetSchemaTable(string TableName)
        {
            string cmdText = string.Format("SET FMTONLY ON;SELECT * FROM [{0}];SET FMTONLY OFF;", TableName);
            DataTable dt = GetDataTable(cmdText);
            dt.TableName = TableName;
            return dt;
        }
        /// <summary>
        /// Get DataTable by cmdText
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>return DataTable</returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetDataTable(string cmdText)
        {
            SqlConnection con = Connection;

            DataTable dt = null;

            try
            {
                dt = new DataTable();
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(cmdText, con);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get DataTable by SqlCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetDataTable(SqlCommand cmd)
        {
            if (cmd.Connection == null)
                cmd.Connection = Connection;

            DataTable dt = null;
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get DataTable by cmdText
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetDataTable(DataTable dt, string cmdText)
        {
            SqlConnection con = Connection;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdText, con);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get DataTable by SqlCommand
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetDataTable(DataTable dt, SqlCommand cmd)
        {
            SqlConnection con = Connection;
            if (cmd.Connection == null)
                cmd.Connection = con;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get SqlDataReader by cmdText
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlDataReader GetSqlDataReader(string cmdText)
        {
            SqlConnection con = Connection;

            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                cmd = new SqlCommand(cmdText, con);
                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                reader = null;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                reader = null;
            }
            return reader;
        }

        /// <summary>
        /// Get SqlDataReader by SqlCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SqlDataReader GetSqlDataReader(SqlCommand cmd)
        {
            SqlDataReader reader = null;

            if (cmd.Connection == null)
                cmd.Connection = Connection;

            SqlConnection con = cmd.Connection;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                reader = null;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                reader = null;
            }
            return reader;
        }

        /// <summary>
        ///	Create a SqlCommand without ReturnValue
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlCommand CreateCommand(string storeProcedure, IDataParameter[] iparams)
        {
            //			SqlConnection con = m_Connection;
            //
            //			SqlCommand cmd = con.CreateCommand();
            //			cmd.CommandText = storeProcedure;
            //			cmd.CommandType = CommandType.StoredProcedure;

            SqlConnection con = Connection;

            SqlCommand cmd = new SqlCommand(storeProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            foreach (SqlParameter par in iparams)
            {
                cmd.Parameters.Add(par);
            }

            return cmd;
        }

        /// <summary>
        /// Create a SqlCommand without ReturnValue
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlCommand CreateCommand(string storeProcedure, SqlConnection con, IDataParameter[] iparams)
        {
            //			SqlConnection con = m_Connection;
            //
            //			SqlCommand cmd = con.CreateCommand();
            //			cmd.CommandText = storeProcedure;
            //			cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd = new SqlCommand(storeProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            foreach (SqlParameter par in iparams)
            {
                cmd.Parameters.Add(par);
            }

            return cmd;
        }
        /// <summary>
        /// Create a SqlCommand with ReturnValue
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlCommand CreateCommandReturn(string storeProcedure, IDataParameter[] iparams)
        {
            SqlCommand cmd = CreateCommand(storeProcedure, iparams);

            SqlParameter par = new SqlParameter(
                "ReturnValue",
                SqlDbType.Int,
                4,
                ParameterDirection.ReturnValue,
                false,
                0,
                0,
                string.Empty,
                DataRowVersion.Original,
                null);
            cmd.Parameters.Add(par);
            return cmd;
        }

        /// <summary>
        /// Create a SqlCommand with ReturnValue
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="con"></param>
        /// <param name="iparams"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlCommand CreateCommandReturn(string storeProcedure, SqlConnection con, IDataParameter[] iparams)
        {
            SqlCommand cmd = CreateCommand(storeProcedure, con, iparams);

            SqlParameter par = new SqlParameter(
                "ReturnValue",
                SqlDbType.Int,
                4,
                ParameterDirection.ReturnValue,
                false,
                0,
                0,
                string.Empty,
                DataRowVersion.Original,
                null);
            cmd.Parameters.Add(par);
            return cmd;
        }

        /// <summary>
        /// Execute a store procedure for Insert, Update or Delete data...
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int ExecuteProcedure(string storeProcedure, IDataParameter[] iparams, out int rowEffected)
        {
            SqlConnection con = Connection;
            SqlTransaction trans = null;

            int count, result;

            SqlCommand cmd = CreateCommandReturn(storeProcedure, iparams); ;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();
                cmd.Transaction = trans;

                count = cmd.ExecuteNonQuery();
                trans.Commit();

                rowEffected = count;
                result = (int)cmd.Parameters["ReturnValue"].Value;

                return result;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute a store procedure for Insert, Update or Delete data...
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int ExecuteProcedureInTransaction(string storeProcedure, IDataParameter[] iparams, out int rowEffected)
        {
            SqlConnection con = Connection;
            SqlTransaction trans = null;

            int count, result;

            SqlCommand cmd = CreateCommandReturn(storeProcedure, iparams);

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();
                cmd.Transaction = trans;

                count = cmd.ExecuteNonQuery();
                trans.Commit();

                rowEffected = count;
                result = (int)cmd.Parameters["ReturnValue"].Value;

                return result;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute sql. Do not use transaction
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Execute(string cmdText)
        {
            SqlConnection con = Connection;
            int count = 0;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand(cmdText, con);

                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return count;
        }


        /// <summary>
        /// Execute a sql statements. Do not use transaction
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Execute(SqlCommand cmd)
        {
            if (cmd.Connection == null)
                cmd.Connection = Connection;

            SqlConnection con = cmd.Connection;

            int count = -1;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute a sql statements
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int ExecuteInTransaction(SqlCommand cmd)
        {
            if (cmd.Connection == null)
                cmd.Connection = Connection;

            SqlConnection con = cmd.Connection;
            SqlTransaction trans = null;

            int count = -1;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();
                cmd.Transaction = trans;

                count = cmd.ExecuteNonQuery();
                trans.Commit();

                return count;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute a sql statements
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int ExecuteInTransaction(string cmdText)
        {
            SqlConnection con = Connection;
            SqlTransaction trans = null;

            int count = -1;

            SqlCommand cmd = new SqlCommand(cmdText, con);

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();
                cmd.Transaction = trans;

                count = cmd.ExecuteNonQuery();
                trans.Commit();

                return count;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute a sql statements
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int ExecuteInTransaction(string cmdText, IDataParameter[] iparams, out int rowEffected)
        {
            SqlConnection con = Connection;
            SqlTransaction trans = null;

            int count, result;

            SqlCommand cmd = new SqlCommand(cmdText, con);

            foreach (SqlParameter par in iparams)
            {
                cmd.Parameters.Add(par);
            }

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();
                cmd.Transaction = trans;

                count = cmd.ExecuteNonQuery();
                trans.Commit();

                rowEffected = count;
                result = (int)cmd.Parameters["ReturnValue"].Value;

                return result;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute a sql statements
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="iRowEffected"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Execute(string cmdText, IDataParameter[] iparams, out int rowEffected)
        {
            SqlConnection con = Connection;

            int count, result;

            SqlCommand cmd = new SqlCommand(cmdText, con);

            cmd.Parameters.Add(
                new SqlParameter(
                "ReturnValue",
                SqlDbType.Int,
                4,
                ParameterDirection.ReturnValue,
                false,
                0,
                0,
                string.Empty,
                DataRowVersion.Original,
                null)
                );

            foreach (SqlParameter par in iparams)
            {
                cmd.Parameters.Add(par);
            }

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                count = cmd.ExecuteNonQuery();

                rowEffected = count;
                result = (int)cmd.Parameters["ReturnValue"].Value;

                return result;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute and return the value of the first column and the first row of the result set.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public object ExecuteScalar(SqlCommand cmd)
        {
            if (cmd.Connection == null)
                cmd.Connection = Connection;

            SqlConnection con = cmd.Connection;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                object obj = cmd.ExecuteScalar();

                return obj;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute and return the value of the first column and the first row of the result set.
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public object ExecuteScalar(string cmdText)
        {
            SqlConnection con = Connection;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand(cmdText, con);

                object obj = cmd.ExecuteScalar();

                return obj;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute and return the value of the first column and the first row of the result set.
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public object ExecuteScalarInTransaction(string cmdText)
        {
            SqlConnection con = Connection;
            SqlTransaction trans = null;

            object obj = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand(cmdText, con, trans);

                obj = cmd.ExecuteScalar();

                trans.Commit();

            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return obj;

        }

        /// <summary>
        /// Execute and return the value of the first column and the first row of the result set.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public object ExecuteScalarInTransaction(SqlCommand cmd)
        {
            if (cmd.Connection == null)
                cmd.Connection = Connection;

            SqlConnection con = cmd.Connection;
            SqlTransaction trans = null;

            object obj = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                trans = con.BeginTransaction();

                cmd.Transaction = trans;

                obj = cmd.ExecuteScalar();

                trans.Commit();

            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }

            return obj;

        }

        /// <summary>
        /// Execute a store procedure and return a SqlDataReader
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public SqlDataReader ExecuteProcedure(string storeProcedure, IDataParameter[] iparams)
        {
            SqlConnection con = Connection;

            SqlCommand cmd = CreateCommand(storeProcedure, iparams);

            if (con.State != ConnectionState.Open)
                con.Open();

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// Execute a store procedure and return a DataSet
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataSet ExecuteProcedure(string storeProcedure, IDataParameter[] iparams, string tablename)
        {
            SqlCommand cmd = CreateCommand(storeProcedure, iparams);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tablename);
            return ds;
        }

        /// <summary>
        /// Execute a store procedure and return a DataSet
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="iparams"></param>
        /// <param name="ds"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataSet ExecuteProcedure(string storeProcedure, IDataParameter[] iparams, DataSet ds, string tablename)
        {
            SqlCommand cmd = CreateCommandReturn(storeProcedure, iparams);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds, tablename);
            return ds;
        }

        /// <summary>
        /// Replace ' by ''
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public string EncodeString(string value)
        {
            if (value == null || value.Length == 0)
                return value;
            return value.Replace("'", "''");
        }

        /// <summary>
        /// Get parameter value by parameter name
        /// </summary>
        /// <param name="param_Name"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public string GetParameterValue(string param_Name)
        {
            SqlConnection con = Connection;

            string param_Value;

            string cmdText = "SELECT Param_Value FROM FPT_ENV_Parameters WHERE Param_Name=@Param_Name";

            SqlCommand cmd = new SqlCommand(cmdText, con);
            cmd.Parameters.Add("@Param_Name", SqlDbType.VarChar).Value = param_Name;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                param_Value = (string)cmd.ExecuteScalar();
                return param_Value;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /// <summary>
        /// Execute a store procedure and return a dataset that contain a table
        /// </summary>
        /// <remarks>
        /// Author			: Vu Thanh Tuan - FSOFT G3
        /// Modifications	: Created 20-Apr-2006
        /// </remarks>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable ExecuteQuerySp(string spName, SqlParameter[] arrParam)
        {
            DataTable dt;
            SqlDataAdapter da;
            SqlConnection con = Connection;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = spName;
                cmd.CommandTimeout = 1000;

                // tuannh2 modified 20080813: neu arrParam khac null thi moi chay for
                // vi co truong hop tham so arrParam truyen vao = null
                // foreach(SqlParameter param in arrParam)
                // {
                //		cmd.Parameters.Add(param);
                // }
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                // end tuannh2

                dt = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        /// <summary>
        /// Get RegionCode by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuthorizedRegion()
        {
            try
            {
                string sql = "SELECT '' AS 'REGION_CODE', '[ALL]' AS 'REGION_NAME' ";
                sql += "UNION ";
                sql += "SELECT DISTINCT A.REGION_CODE, A.REGION_NAME ";
                sql += "FROM FPT_ENV_REGION_HIERARCHY A ";
                sql += "    INNER JOIN FPT_ENV_AUT_USER_REGION B ";
                sql += "       ON A.REGION_CODE = B.REGION_CODE ";
                sql += "WHERE B.USERNAME = @USERNAME ";
                sql += "ORDER BY A.REGION_CODE ";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@USERNAME", clsSystemConfig.UserName);
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql, parameters).Tables[0];
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get RegionCode by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetAuthorizedRegion(string userName)
        {
            try
            {
                string sql = "SELECT '' AS 'REGION_CODE', '[ALL]' AS 'REGION_NAME' ";
                sql += "UNION ";
                sql += "SELECT DISTINCT A.REGION_CODE, A.REGION_NAME ";
                sql += "FROM FPT_ENV_REGION_HIERARCHY A ";
                sql += "    INNER JOIN FPT_ENV_AUT_USER_REGION B ";
                sql += "       ON A.REGION_CODE = B.REGION_CODE ";
                sql += "WHERE B.USERNAME = @USERNAME ";
                sql += "ORDER BY A.REGION_CODE ";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@USERNAME", userName);
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql, parameters).Tables[0];
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get Strategic Region by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuthorizedStrategicRegion()
        {
            try
            {
                string sql = "SELECT '' AS 'STRATEGIC_REGION_CODE', '[ALL]' AS 'STRATEGIC_REGION_NAME' ";
                sql += "UNION ";
                sql += "SELECT DISTINCT A.STRATEGIC_REGION_CODE, A.STRATEGIC_REGION_CODE AS 'STRATEGIC_REGION_NAME' ";
                sql += "FROM FPT_ENV_STRATEGIC_REGION A ";
                sql += "    INNER JOIN FPT_ENV_AUT_USER_STRATEGIC_REGION B ";
                sql += "       ON A.STRATEGIC_REGION_CODE = B.STRATEGIC_REGION_CODE ";
                sql += "WHERE B.USERNAME = @USERNAME ";
                sql += "ORDER BY A.STRATEGIC_REGION_CODE ";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@USERNAME", clsSystemConfig.UserName);
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql, parameters).Tables[0];
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get Strategic Region by UserName
        /// CanLV: 11-Mar-2009
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuthorizedStrategicRegion(string userName)
        {
            try
            {
                string sql = "SELECT '' AS 'STRATEGIC_REGION_CODE', '[ALL]' AS 'STRATEGIC_REGION_NAME' ";
                sql += "UNION ";
                sql += "SELECT DISTINCT A.STRATEGIC_REGION_CODE, A.STRATEGIC_REGION_CODE AS 'STRATEGIC_REGION_NAME' ";
                sql += "FROM FPT_ENV_STRATEGIC_REGION A ";
                sql += "    INNER JOIN FPT_ENV_AUT_USER_STRATEGIC_REGION B ";
                sql += "       ON A.STRATEGIC_REGION_CODE = B.STRATEGIC_REGION_CODE ";
                sql += "WHERE B.USERNAME = @USERNAME ";
                sql += "ORDER BY A.STRATEGIC_REGION_CODE ";

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@USERNAME", userName);
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql, parameters).Tables[0];
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
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
                using (SqlConnection cn = Connection)
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

        /// <summary>
        /// Bulk insent one table to database
        /// Author: QuangVD
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        /// <param name="columnNames">List of column name in DataTable, must be the same as table in database</param>
        public bool BulkInsert(DataTable table, string tableName)
        {
            try
            {
                string conStr =  System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
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

        public bool BulkInsert(DataTable table, string tableName, SqlConnection con)
        {
            try
            {
                string conStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                //using (SqlConnection con = new SqlConnection(conStr))
                //{
                    //con.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(con))
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            copy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        }
                        copy.DestinationTableName = tableName;
                        copy.WriteToServer(table);
                    }
                    //con.Close();
                //}
                return true;
            }
            catch (Exception ex)
            {
                //string strEx = ex.StackTrace + ex.Message;
                log.Error(ex);
                return false;
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            try
            {
                List<T> data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        private  T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] == DBNull.Value)
                        {
                            try
                            {
                                dr[column.ColumnName] = string.Empty;
                            }
                            catch
                            {
                                dr[column.ColumnName] = 0;
                            }
                        }
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
