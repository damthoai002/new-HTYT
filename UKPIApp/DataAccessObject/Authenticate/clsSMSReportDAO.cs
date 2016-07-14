using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using UKPI.Utils;

namespace UKPI.DataAccessObject
{
	/// <summary>
	/// Summary description for clsSMSReportDAO.
    /// Author: DucND 16-12-2008
	/// </summary>
	public class clsSMSReportDAO:clsBaseDAO
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(clsSMSReportDAO));

		public clsSMSReportDAO()
		{

		}
		public clsSMSReportDAO(SqlConnection con):base(con)
		{

		}

		public DataTable GetDataTable(string distributor, string infStatus, string fromDate, string toDate)
		{
			SqlCommand cmd = new SqlCommand();

            string strSql = "SELECT DISTINCT INF.CREATED_DATE, INF.RECEIVE_PERSON, INF.ROLE, INF.INFORM_METHOD, INF.EMAIL, INF.SMS, DIS.CUST_NAME,INF.PPO_CODE, INF.PPO_TYPE, INF.TIME_CHECK, INF.INFORM_STATUS, INF.INFORM_TIME, INF.NOTE FROM FPT_ENV_INFORM_LOG INF"
				+ " LEFT JOIN FPT_ENV_DISTRIBUTOR_HIERARCHY DIS ON INF.DISTRIBUTOR = DIS.CUST_CODE"
				+ " WHERE 1=1";
			if(distributor != "")
			{
				strSql += " AND INF.DISTRIBUTOR = @DISTRIBUTOR";
				cmd.Parameters.Add("@DISTRIBUTOR", SqlDbType.VarChar);
				cmd.Parameters["@DISTRIBUTOR"].Value = distributor;
			}
			if(infStatus != "" && infStatus != "[ALL]")
			{
				strSql += " AND INF.INFORM_STATUS = @INFSTATUS";
				cmd.Parameters.Add("@INFSTATUS", SqlDbType.VarChar);
				cmd.Parameters["@INFSTATUS"].Value = infStatus;
			}
			strSql += " AND INF.CREATED_DATE >= @FROMDATE AND INF.CREATED_DATE <= DATEADD(dd,1,@TODATE)";
			cmd.Parameters.Add("@FROMDATE", SqlDbType.DateTime);
			cmd.Parameters["@FROMDATE"].Value = fromDate;
			cmd.Parameters.Add("@TODATE", SqlDbType.DateTime);
			cmd.Parameters["@TODATE"].Value = toDate;
			
			SqlConnection con = Connection;
			if(con == null)
				throw new Exception(CONNECTION_ERROR);
			cmd.Connection = con;
			cmd.CommandText = strSql;

			try
			{
				DataTable dt = new DataTable();
				SqlDataAdapter m_DataAdapter = new SqlDataAdapter(cmd);
				m_DataAdapter.Fill(dt);
				return dt;
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				//MessageBox.Show(ex.ToString());
			}
			finally
			{
				if(con != null && con.State == ConnectionState.Open)
					con.Close();
			}
			return null;
		}
	}
}
