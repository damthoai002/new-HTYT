using System;
using System.Data;
using System.Data.SqlClient;

using UKPI.DataAccessObject.Authenticate;

namespace UKPI.BusinessObject.Authenticate
{
	/// <summary>
	/// Summary description for cls.
	/// </summary>
	public class clsTrackingUserBO:clsBaseBO
	{
		private clsTrackingUserDAO m_DAO = new clsTrackingUserDAO();
		public clsTrackingUserBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public DataTable TrackingDetail(string createUser,string createTime,string updateUser,string updateTime,string tableName)
		{
			DataTable dt = null;
			try
			{
				dt = m_DAO.ShowDetail(createUser,createTime,updateUser,updateTime,tableName);
			}
			catch{
				dt = null;
			}
			return dt;
		}
		public DataTable SearchTrackingUser(string userName,string tableName, string operation, string createTime, string updateTime)
		{
			DataTable dt = null;
			try
			{
				dt= m_DAO.SearchTrackingUser(userName,tableName,createTime,updateTime,operation);
			}
			catch{
				dt = null;
			}
			finally
			{}
			return dt;
		}

	}
}
