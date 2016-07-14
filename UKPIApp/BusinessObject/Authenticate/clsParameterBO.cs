using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Web;

using UKPI.DataAccessObject;
using UKPI.BusinessObject;
using UKPI.Utils;

namespace UKPI.BusinessObject
{
	/// <summary>
	/// Summary description for CustomerBO.
	/// </summary>
	public class clsParameterBO:clsBaseBO
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(clsParameterBO));
		protected clsParameterDAO dao = new clsParameterDAO();
		public clsParameterBO()
		{
			
		}	

		/// <summary>
		/// Return DataTable to Combobox
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		
		public DataTable LoadAll()
		{
			return dao.GetDataTable("SELECT DISTINCT PARAM_GROUP FROM FPT_ENV_PARAMETERS ORDER BY PARAM_GROUP DESC");
		}
		
		
		/// <summary>
		/// Return DataTable to Set Source for DAtaGrid
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		
		public DataTable GetOne(string ParamterGroup, string param)
		{
            return dao.GetOne(ParamterGroup, param);
		}

		/// <summary>
		/// Update Parameter value
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>		
		
		public bool Update(string m_value, string m_name)
		{
			return dao.UpdateValue(m_value, m_name);
		}

		/// <summary>
		/// Check Parameter value
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
        /*   * s: kieu du lieu string (character)
		    * i: kieu du lieu Interger
		    * f: kieu du lieu float
		    * p: period theo format [interger]@[Date of moth]; Ex: -1@30
		    * d: Date of Week: MON, THU, ....
		    * b: boolean Y: True, N: False
		    * t: time(hh:mm)
		*/// </remarks>		
		
		public bool Validate(string strType, string strValue)
		{
            try
            {
			    if(strType=="t")
			    {
                    return IsTimer(strValue);
			    }
			    else if(strType=="b")
			    {
				    if(!(strValue=="Y"||strValue=="y" || strValue== "N"||strValue=="n"))
					    return false;
			    }
                else if (strType == "d")
                {
                    int intCheck = 0;
                    string[] strDate = { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
                    for (int i = 0; i < strDate.Length; i++)
                    {
                        if (strValue.ToUpper() != strDate[i].ToUpper())
                            intCheck += 1;
                    }
                    if (intCheck == strDate.Length) return false;
                }
                else if(strType == "p")
                {
                    return IsPeriod(strValue);
                }
                else if (strType == "i")
                {
                    return clsCommon.IsNumeric(strValue);
                }
                else if (strType == "f")
                {
                    return clsCommon.IsFloat(strValue);
                }

            }
            catch (Exception ex)
            {
                return false;

            }
			return true;
		}

		/// <summary>
		/// Check chuoi co phai la numeric hay ko
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>

		public bool isNumeric(string val)
		{
			try
			{
				Double.Parse(val);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Export Parameter
		/// </summary>
		/// <remarks>
		/// Author			:	Nguyen Bao Nguyen G3
		/// Created day		:	24-Apr-2006
		/// </remarks>
		public string ExportParameter(string strRRWeek, string strMaxPPO, string strMinPPO)
		{
			string strParamPath = "";
			try
			{
				strParamPath = dao.ExportParameter(strRRWeek, strMaxPPO, strMinPPO);
				if (strParamPath != "") // khac rong co nghia la export param thanh cong
					ZipFileParam(strParamPath);
				return strParamPath;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//-- tuannh2 added 20080822: export param cua nhung cust_code dat hang daily
		public string ExportDailyParameter()
		{
			try
			{
				string path = "";
				DataTable dt = dao.GetDataTable("select distinct cust_code from FPT_ENV_DISTRIBUTOR_HIERARCHY where PPO_TYPE = 'D' AND STATUS = 'AC'");
				string strExportPath = dao.GetExportParamPath();
				foreach(DataRow drow in dt.Rows)
				{
					string strCustCode = drow["CUST_CODE"].ToString();
					path = dao.ExportDailyParameter(strCustCode, strExportPath);
				}
				ZipFileParam(path);
				return path;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		//-- end tuannh2

		/// <summary>
		/// Get Status of Stock Paramters
		/// </summary>
		///  <remarks>
		///  //true: exported , false : unexported
		/// Author:			Nguyen Bao Nguyen	G3
		/// Modified:		24-Apr-2006
		/// </remarks>
		/// <returns></returns>
		public bool ExistUnexportedParam()
		{
			try 
			{
				return dao.ExistUnexportedParam();
			}
			catch (Exception ex)
			{
				log.Error(ex.Message, ex);
				throw ex;
			}
		}


		/// <summary>
		/// Zip file
		/// </summary>
		/// <remarks>
		/// Author			:	Nguyen Bao Nguyen G3
		/// Created day		:	24-Apr-2006
		/// </remarks>
		public void ZipFileParam(string strParamPath)
		{
			try 
			{
				//string strFileNameToEncode = "";
				string strZipFilename = "";
				string strPass = "";

				string strPath = strParamPath;
				string[] filenames = Directory.GetFiles(strPath, "*.xml");
				clsCryptography genPass = new clsCryptography();
		

				foreach (string file in filenames)
				{
					strZipFilename = file.Substring(0, file.Length -4)+".zip";
					//strFileNameToEncode = file.Substring(0, file.Length -4);
					strPass = genPass.GenPWDByFilename(strZipFilename);
					clsZip.ZipFiles(file, strZipFilename, strPass);
					File.Delete(file);
				}
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

        public bool SendParameters(ref string strPath)
        {
            try
            {
                DataTable dtParameters = dao.GetParameters();
                dtParameters.DataSet.DataSetName = "Parameters";
                dtParameters.TableName = "PaymentToolParameters";
                ExportParameters(dtParameters,ref strPath);    
            }catch(Exception ex){
                log.Error(ex.Message);
                return false;
            }
            return true;
        }

        private void ExportParameters(DataTable dtParam,ref string strPath)
        {
            string strCurrentDate = DateTime.Now.ToString("yyyyMMddhhmmss");
            strPath = dao.GetParameterValue("ParametersExportPath");
            string strXMLName = string.Format(ConfigurationManager.AppSettings["ParameterExportName"].ToString().Trim(), strCurrentDate);

            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            if (!strPath.EndsWith("\\"))
            {
                strPath += "\\";
            }

            dtParam.WriteXml(strPath + strXMLName);

            clsCommon.ZipFile(strPath);
        }

        /// <summary>
        /// Validate for timer have format: HH:mm
        /// Add by KienTNT
        /// </summary>
        /// <returns>true if it's valid</returns>
        /// <returns>false if it's invalid</returns>
        private bool IsTimer(string strValue)
        {
            try
            {
                int timer;
                string[] arrValue = strValue.Split(new char[] { ':' });
                if (arrValue[0].ToString().Trim().Length == 0 || arrValue[1].ToString().Trim().Length == 0)
                    return false;

                if (arrValue[0].ToString().Trim().Length < 2 && arrValue[0].ToString().Trim().Length == 1)
                    arrValue[0] = "0" + arrValue[0].ToString().Trim();

                if (arrValue[1].ToString().Trim().Length < 2 && arrValue[1].ToString().Trim().Length == 1)
                    arrValue[1] = "0" + arrValue[1].ToString().Trim();

                timer = Convert.ToInt32(arrValue[0].ToString() + arrValue[1].ToString());


                if (timer > 2400 || timer < 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Validate for *@**
        /// Add by KienTNT
        /// </summary>
        /// <returns>true if it's valid</returns>
        /// <returns>false if it's invalid</returns>
        private bool IsPeriod(string value)
        {
            string[] arrValue = value.Split(new char[] { '@' });

            if (arrValue.Length == 0 || arrValue.Length != 2)
            {
                return false;
            }

            try
            {
                int Month = Convert.ToInt32(arrValue[0]);
                if (Month > 5 || Month < -5)
                    return false;
            }catch(Exception ex){
                return false;
            }

            try
            {
                int Day = Convert.ToInt32(arrValue[1]);
                if (Day > 31 || Day < 0)
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
	}
	
}
