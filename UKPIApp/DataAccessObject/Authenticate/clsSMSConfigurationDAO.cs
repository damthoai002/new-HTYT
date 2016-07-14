using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using UKPI.Utils;
using System.Collections;

namespace UKPI.DataAccessObject
{
	/// <summary>
	/// Summary description for clsSMSConfigurationDAO.
	/// Author: DucND 15-12-2008
	/// </summary>
	public class clsSMSConfigurationDAO:clsBaseDAO
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(clsSMSConfigurationDAO));
        protected clsParameterDAO paramDao = new clsParameterDAO();
		public clsSMSConfigurationDAO()
		{
			
		}

		public clsSMSConfigurationDAO(SqlConnection con):base(con)
		{

		}
		/*******************************************************************************
		'Purpose      : Nhan vao nut Apply/OK thi luu parameters vao trong database
		'Author       : DucND - Developer, G3.
		'Created      : 13-Dec-2008
		'******************************************************************************/
        /*
        public bool SetSMSParameters(Hashtable parameters)
		{ 
			bool blnsuccess = false;
			SqlConnection con = Connection;
			if(con == null)
				throw new Exception(CONNECTION_ERROR);
			if(con.State != ConnectionState.Open)
				con.Open();
			
			SqlCommand cmd = new SqlCommand("sp_SetParametersSMS", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("SMS_ACTIVE_CHECK_BPCS", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_ACTIVE_CHECK_BPCS_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_ACTIVE_CHECK_UPLIFT", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_ACTIVE_CHECK_UPLIFT_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_COMPLETE_PPO_DURATION", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_COMPLETE_PPO_DURATION_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_DAILY_ORDER", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_DAILY_ORDER_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_INFORM_METHOD", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_INFORM_METHOD_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_PROCESS_SUCCESSFULLY", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_PROCESS_SUCCESSFULLY_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_AFTER_BPCS", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_AFTER_BPCS_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_AFTER_UPLIFT", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_AFTER_UPLIFT_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_FREQUENCY_BPCS", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_FREQUENCY_BPCS_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_FREQUENCY_UPLIFT", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_RECHECK_FREQUENCY_UPLIFT_value", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_WEEKLY_ORDER", SqlDbType.VarChar);
			cmd.Parameters.Add("SMS_WEEKLY_ORDER_value", SqlDbType.VarChar);

			cmd.Parameters["SMS_ACTIVE_CHECK_BPCS_value"].Value = Convert.ToString(parameters["SMS_ACTIVE_CHECK_BPCS"]);
			cmd.Parameters["SMS_ACTIVE_CHECK_BPCS"].Value = "SMS_ACTIVE_CHECK_BPCS";
			cmd.Parameters["SMS_ACTIVE_CHECK_UPLIFT_value"].Value = Convert.ToString(parameters["SMS_ACTIVE_CHECK_UPLIFT"]);
			cmd.Parameters["SMS_ACTIVE_CHECK_UPLIFT"].Value = "SMS_ACTIVE_CHECK_UPLIFT";
			cmd.Parameters["SMS_COMPLETE_PPO_DURATION_value"].Value = Convert.ToString(parameters["SMS_COMPLETE_PPO_DURATION"]);
			cmd.Parameters["SMS_COMPLETE_PPO_DURATION"].Value = "SMS_COMPLETE_PO_DURATION";
			cmd.Parameters["SMS_DAILY_ORDER_value"].Value = Convert.ToString(parameters["SMS_DAILY_ORDER"]);
			cmd.Parameters["SMS_DAILY_ORDER"].Value = "SMS_DAILY_ORDER";
			cmd.Parameters["SMS_INFORM_METHOD_value"].Value = Convert.ToString(parameters["SMS_INFORM_METHOD"]);
			cmd.Parameters["SMS_INFORM_METHOD"].Value = "SMS_INFORM_METHOD";
			cmd.Parameters["SMS_PROCESS_SUCCESSFULLY_value"].Value = Convert.ToString(parameters["SMS_PROCESS_SUCCESSFULLY"]);
			cmd.Parameters["SMS_PROCESS_SUCCESSFULLY"].Value = "SMS_PROCESS_SUCCESSFULLY";
			cmd.Parameters["SMS_RECHECK_AFTER_BPCS_value"].Value = Convert.ToString(parameters["SMS_RECHECK_AFTER_BPCS"]);
			cmd.Parameters["SMS_RECHECK_AFTER_BPCS"].Value = "SMS_RECHECK_AFTER_BPCS";
			cmd.Parameters["SMS_RECHECK_AFTER_UPLIFT_value"].Value = Convert.ToString(parameters["SMS_RECHECK_AFTER_UPLIFT"]);
			cmd.Parameters["SMS_RECHECK_AFTER_UPLIFT"].Value = "SMS_RECHECK_AFTER_UPLIFT";
			cmd.Parameters["SMS_RECHECK_FREQUENCY_BPCS_value"].Value = Convert.ToString(parameters["SMS_RECHECK_FREQUENCY_BPCS"]);
			cmd.Parameters["SMS_RECHECK_FREQUENCY_BPCS"].Value = "SMS_RECHECK_FREQUENCY_BPCS";
			cmd.Parameters["SMS_RECHECK_FREQUENCY_UPLIFT_value"].Value = Convert.ToString(parameters["SMS_RECHECK_FREQUENCY_UPLIFT"]);
			cmd.Parameters["SMS_RECHECK_FREQUENCY_UPLIFT"].Value = "SMS_RECHECK_FREQUENCY_UPLIFT";
			cmd.Parameters["SMS_WEEKLY_ORDER_value"].Value = Convert.ToString(parameters["SMS_WEEKLY_ORDER"]);
			cmd.Parameters["SMS_WEEKLY_ORDER"].Value = "SMS_WEEKLY_ORDER";

			try
			{
				cmd.ExecuteNonQuery();
				blnsuccess=true;
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				if(con != null && con.State == ConnectionState.Open)
					con.Close();
			}
			return blnsuccess;
		}
        */


        /*******************************************************************************
        'Purpose      : SetSMSParameters viet chuoi qua, viet lai.
        'Author       : Thanhnq - Developer, G3.
        'Created      : 7-May-2009
        '******************************************************************************/
        public bool UpdateSMSParameters(Hashtable parameters)
        {
            foreach (string key in parameters.Keys)
            {
                string value = parameters[key].ToString();
                if (!paramDao.UpdateValue(value, key))
                {
                    return false;
                }
            }

            return true;
        }

		/*******************************************************************************
		'Purpose      : Lay du lieu tham so thuoc nhom 'SMS' tu database len
		'Author       : DucND - Developer, G3.
		'Created      : 15-Dec-2008
		'******************************************************************************/
		public DataTable GetSMSParameters()
		{
			SqlConnection con = Connection;
			if(con == null)
				throw new Exception(CONNECTION_ERROR);

			SqlCommand cmd = new SqlCommand("SELECT PARAM_NAME, PARAM_VALUE FROM FPT_ENV_PARAMETERS WHERE PARAM_GROUP = @Param_Group", con);
			cmd.Parameters.Add("@Param_Group", SqlDbType.VarChar);
			cmd.Parameters["@Param_Group"].Value = "SMS";

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
				MessageBox.Show(ex.ToString());
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
