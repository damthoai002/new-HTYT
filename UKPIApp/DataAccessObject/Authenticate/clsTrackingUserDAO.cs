using System;
using System.Data;
using System.Data.SqlClient;

namespace UKPI.DataAccessObject.Authenticate
{
	/// <summary>
	/// Summary description for clsTrackingUserDAO.
	/// </summary>
	public class clsTrackingUserDAO:clsBaseDAO
	{
		public clsTrackingUserDAO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public DataTable ShowDetail(string createUser, string createTime,string updateUser,string updateTime,string tableName)
		{
			DataTable dt = new DataTable();
			SqlConnection con = Connection;
			if(createTime.Equals(""))
				createTime = "01/01/1900 00:00:00";
			if(updateTime.Equals(""))
				updateTime = "01/01/1900 00:00:00";
			string sqlQuery =  "SELECT DISTINCT * FROM " + tableName ;
			if(!tableName.Equals("FPT_ENV_PROMOTION_REGION_SWAP") && !tableName.Equals("FPT_ENV_PROMOTION_CUST_SWAP"))
			{
				sqlQuery += " WHERE CREATE_USER = '" + createUser +"' AND CONVERT(VARCHAR(20),CREATE_TIME,103) + ' ' + CONVERT(VARCHAR(20),CREATE_TIME,108) = '" + createTime +
					"' AND UPDATE_USER = '"+ updateUser + 
					"' AND CONVERT(VARCHAR(20),UPDATE_TIME,103) + ' ' + CONVERT(VARCHAR(20),UPDATE_TIME,108) = '" + updateTime +"'";
			}
			else
			{
				sqlQuery += " WHERE CREATED_BY = '" + createUser +"' AND CONVERT(VARCHAR(20),CREATED_DATE,103) + ' ' + CONVERT(VARCHAR(20),CREATED_DATE,108) = '" + createTime +
					"' AND LAST_UPDATED_BY = '"+ updateUser + 
					"' AND CONVERT(VARCHAR(20),LAST_UPDATED_DATE,103) + ' ' + CONVERT(VARCHAR(20),LAST_UPDATED_DATE,108) = '" + updateTime +"'";
			}
			SqlCommand cmd = new SqlCommand(sqlQuery,con);
			if(con.State == ConnectionState.Closed)
				con.Open();
			SqlDataAdapter m_da = new SqlDataAdapter(cmd);
			try
			{
				m_da.Fill(dt);
			}
			catch(Exception ex){
				string bug = ex.Message;
				con.Close();}
			return dt;
		}
		public DataTable SearchTrackingUser(string userName, string tableName,string createDate, string updateDate, string operation)
		{
			DataTable dt = new DataTable();
			SqlConnection con = Connection;
			if(con.State == ConnectionState.Closed)
				con.Open();
			SqlCommand cmdDelete = new SqlCommand("p_FPT_ENV_CollectTrackingInfo",con);
			cmdDelete.CommandType = CommandType.StoredProcedure;
			try
			{
				cmdDelete.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				string tes = ex.Message;
			}
			finally
			{
				con.Close();
			}
			string strSqlQuery = " SELECT DISTINCT CREATE_USER,CASE CREATE_TIME WHEN '1900-01-01 00:00:00.000' THEN '' ELSE CREATE_TIME END AS CREATE_TIME, UPDATE_USER,"+
					" CASE UPDATE_TIME WHEN '1900-01-01 00:00:00.000' THEN '' ELSE UPDATE_TIME END AS UPDATE_TIME, TABLE_NAME  FROM FPT_ENV_TRACKING_COLLECTION WHERE 1=1";
			if((operation == "[ALL]")&& (userName != ""))
				strSqlQuery += " AND ((CREATE_USER = '"+ userName +"') OR (UPDATE_USER = '"+userName+"')) ";
			else
			{
				if((operation.Equals("Added")) && (userName !=""))
				{
					strSqlQuery += " AND CREATE_USER = '" + userName+"'";
				}
				else
					if(operation.Equals("Modified") && userName != "")
				{
					strSqlQuery += "AND UPDATE_USER = '" + userName +"'";
				}
				if((operation.Equals("Added")) && (userName ==""))
				{
					strSqlQuery += " AND CREATE_USER <> ''";
				}
				else
					if(operation.Equals("Modified") && userName == "")
				{
					strSqlQuery += "AND UPDATE_USER <> ''";
				}
			}
			if(tableName != "[ALL]")
			{
				strSqlQuery += " AND TABLE_NAME = '" + tableName + "'";
			}
			if(createDate != "")
			{
				strSqlQuery += " AND CREATE_TIME LIKE '" + createDate + "%'";
			}
			if(updateDate != "")
			{
				strSqlQuery += " AND UPDATE_TIME LIKE '" + updateDate + "%'";
			}
			if(con.State == ConnectionState.Closed)
				con.Open();
			SqlCommand cmd = new SqlCommand(strSqlQuery,con);
			SqlDataAdapter dta = new SqlDataAdapter(cmd);
			dta.Fill(dt);
			return dt;
		}
	}
}
