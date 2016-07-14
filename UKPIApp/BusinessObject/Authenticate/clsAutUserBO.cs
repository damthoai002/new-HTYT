using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using log4net;

using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    /// <summary>
    /// Summary description for clsAutUserBO.
    /// </summary>
    /// <remarks>
    /// Author:			Nguyen Minh Duc. G3.
    /// Created date:	14/05/2006
    /// </remarks>
    public class clsAutUserBO
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(clsAutUserBO));
        private clsAutUserDAO dao = new clsAutUserDAO();

        private static DataTable m_dtAuthority;

        public clsAutUserBO() { }

        /// <summary>
        /// Get schema of FPT_ENV_AUT_USER table
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetSchemaTable()
        {
            return dao.GetSchemaTable();
        }

        /// <summary>
        /// Search data from 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="roleID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable Search(DataTable dt, string userName, string firstName, string lastName, string email, string roleID, string status)
        {
            clsCommon common = new clsCommon();
            string strSql = "SELECT USERNAME, PASSWORD, FIRSTNAME, LASTNAME, EMAIL, ADDRESS, PHONE, START_DATE, END_DATE, PWD_CHG_DATE, STATUS, UROLE_ID, DESCRIPTION FROM FPT_ENV_AUT_USER";
            StringBuilder sb = new StringBuilder();

            if (userName != null && userName.Length > 0)
            {
                sb.Append(string.Format(" AND USERNAME LIKE '%{0}%' ", common.EncodeKeyword(userName)));
            }

            if (roleID != null && roleID.Length > 0)
            {
                sb.Append(string.Format(" AND UROLE_ID = '{0}' ", common.EncodeString(roleID)));
            }

            if (status != null && status.Length > 0)
            {
                sb.Append(string.Format(" AND STATUS = '{0}' ", common.EncodeString(status)));
            }

            if (firstName != null && firstName.Length > 0)
            {
                sb.Append(string.Format(" AND FIRSTNAME LIKE '%{0}%' ", common.EncodeKeyword(firstName)));
            }

            if (lastName != null && lastName.Length > 0)
            {
                sb.Append(string.Format(" AND LASTNAME LIKE '%{0}%' ", common.EncodeKeyword(lastName)));
            }

            if (email != null && email.Length > 0)
            {
                sb.Append(string.Format(" AND EMAIL LIKE '%{0}%' ", common.EncodeKeyword(email)));
            }

            if (sb.Length > 0)
                strSql = strSql + " WHERE " + sb.ToString(4, sb.Length - 4);

            return dao.GetDataTable(dt, strSql);
        }
        /// <summary>
        /// Load all user from FPT_ENV_AUT_USER table
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable LoadAll()
        {
            return dao.LoadAll();
        }

        /// <summary>
        /// Check whether this user name exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool Exist(string userName)
        {
            return dao.Exist(userName);
        }
        /// <summary>
        /// Get one user from FPT_ENV_AUT_USER table by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetOne(string username)
        {
            return dao.GetOne(username);
        }

        /// <summary>
        /// Load all Status
        /// </summary>
        /// <returns></returns>
        public DataTable LoadAllStatus()
        {
            return dao.LoadAllStatus();
        }

        /// <summary>
        /// Get Region from FPT_ENV_AUT_USER_REGION table by UserName
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ArrayList GetRegion(string UserName)
        {
            return dao.GetRegion(UserName);
        }

        /// <summary>
        /// Set Rights for one user on region and strategic region
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="regions"></param>
        /// <param name="strategicRegions"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int SetRights(string userName, ArrayList regions, ArrayList strategicRegions)
        {
            return dao.SetRights(userName, regions, strategicRegions);
        }
        /// <summary>
        /// Get StragicRegion from FPT_ENV_AUT_USER_STRATEGIC_REGION table by UserName
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ArrayList GetStrategicRegion(string UserName)
        {
            return dao.GetStrategicRegion(UserName);
        }

        /// <summary>
        /// Load All UserRole from FPT_ENV_AUT_USERROLE table
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable LoadAllUserRole()
        {
            return dao.LoadAllUserRole();
        }

        /// <summary>
        /// Insert one row into FPT_ENV_AUT_USER table
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Insert(DataRow row)
        {
            return dao.Insert(row);
        }

        /// <summary>
        /// Update one row of FPT_ENV_AUT_USER by USERNAME
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Update(DataRow row)
        {
            return dao.Update(row);
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
        public DataTable GetCommonMenu()
        {
            //string strSql = "SELECT FORM_ID, FORM_NAME, MENU_NAME, MENU_LEVEL, MENU_PID, MENU_ZORDER, TOOLBAR_BUTTON_INDEX, TOOLBAR_BUTTON_NAME, TOOLBAR_NAME, DESCRIPTION, ICON_NAME FROM FPT_ENV_AUT_FORM WHERE MENU_NAME IN ('mnuFile', 'mnuWindow', 'mnuHelp', 'mnuLogin', 'mnuSeparate', 'mnuExit', 'mnuWindowCascade', 'mnuWindowTileHoz', 'mnuHelpTopic', 'mnuSeparate', 'mnuHelpAbout', 'mnuEN', 'mnuVN', 'mnuMaximized', 'mnuSystemStyle')";
            string strSql = "SELECT FORM_ID, FORM_NAME, MENU_NAME, MENU_LEVEL, MENU_PID, MENU_ZORDER, TOOLBAR_BUTTON_INDEX, TOOLBAR_BUTTON_NAME, TOOLBAR_NAME, DESCRIPTION, ICON_NAME FROM FPT_ENV_AUT_FORM WHERE COMMON_MENU = '0'";
            return dao.GetDataTable(strSql);
        }
        /// <summary>
        /// Delete one row of FPT_ENV_AUT_USER by USERNAME
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Delete(string username)
        {
            return dao.Delete(username);
        }

        /// <summary>
        /// Changed password by username, oldPassword, newPassword
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int ChangePassword(string username, string oldPassword, string newPassword)
        {
            return dao.ChangePassword(username, oldPassword, newPassword);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int Login(string username, string password)
        {
            int intResult;
            clsAutUserDAO login;
            int intExpiredDays;

            try
            {
                login = new clsAutUserDAO();
                intExpiredDays = 1;//int.Parse(dao.GetParameterValue(clsConstants.EXPIRED_DAYS)); ;//Convert.ToInt32(ConfigurationManager.AppSettings["ExpiredDays"]);

                DataTable dt = login.Login(username, password, intExpiredDays, out intResult);
                if (intResult == clsConstants.LOGIN_SUCCESS)
                {
                    DataTable tbUser = SelectOneUser(username);
                 //   clsSystemConfig.MaNhanVien = Int32.Parse(tbUser.Rows[0]["MaNhanVien"].ToString());
                 //   clsSystemConfig.MaNVUnilever = tbUser.Rows[0]["MaNVUnilever"].ToString();
                    clsSystemConfig.UserName = tbUser.Rows[0]["USERNAME"].ToString();
                    clsSystemConfig.EMAIL = tbUser.Rows[0]["EMAIL"].ToString();
                    clsSystemConfig.FullName = tbUser.Rows[0]["LASTNAME"].ToString() + "" + tbUser.Rows[0]["FIRSTNAME"].ToString();
                //    clsSystemConfig.LevelQuanLy = Int32.Parse(tbUser.Rows[0]["LevelQuanLy"].ToString());
                    clsSystemConfig.UROLE_ID = tbUser.Rows[0]["UROLE_ID"].ToString();
               //     clsSystemConfig.CardNo = tbUser.Rows[0]["CardNo"].ToString();


                    m_dtAuthority = dt;
                }

                return intResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw new Exception("clsAutUserBO.LogIn error");
            }
        }

        /// <summary>
        /// Get authority. Return all feature of this user.
        /// </summary>
        /// <returns>Return all feature of this user</returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public DataTable GetAuthority()
        {
            return m_dtAuthority;
        }

        /// <summary>
        /// Clear authority
        /// </summary>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public void ClearAuthority()
        {
            if (m_dtAuthority != null)
            {
                m_dtAuthority.Clear();
            }
        }

        /// <summary>
        /// Clear authority
        /// </summary>
        /// <param name="oldcustcode">Old Customer code</param>
        /// <param name="newcustcode">New Customer code</param>
        /// <returns>Return vale from function</returns>
        /// <remarks>
        /// Author:       Nguyen Quy Vinh Loc. G3.
        /// Created date: 9/04/2008
        /// </remarks>
        public int ChangeCustCode(string oldcustcode, string newcustcode)
        {
            int intResult;
            clsAutUserDAO changeCC;
            try
            {
                changeCC = new clsAutUserDAO();
                DataTable dt = changeCC.ChangeCustCode(oldcustcode, newcustcode, out intResult);

                return intResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw new Exception("clsAutUserBO.ChangeCustCode error");
            }
        }

        public ClsNhanVien GetOneByNhanVien(string nhanVienId)
        {

            DataTable tb = dao.GetOneByNhanVien(nhanVienId);
            var clsNv = new ClsNhanVien();
            if (tb.Rows.Count > 0)
            {
                clsNv.SysId = long.Parse(tb.Rows[0][clsCommon.NhanVien.SysId].ToString());
                clsNv.MaNVUnilever = (tb.Rows[0][clsCommon.NhanVien.MaNVUnilever].ToString());
                clsNv.CardNo = (tb.Rows[0][clsCommon.NhanVien.CardNo].ToString());
                clsNv.LNAME = (tb.Rows[0][clsCommon.NhanVien.FNAME].ToString());
                clsNv.FNAME = (tb.Rows[0][clsCommon.NhanVien.LNAME].ToString());
                clsNv.GioiTinh = tb.Rows[0][clsCommon.NhanVien.GioiTinh].ToString() == "1";
                clsNv.Email = (tb.Rows[0][clsCommon.NhanVien.Email].ToString());

            }

            return clsNv;
        }

        public int Insert(ClsNhanVien nv)
        {
            return dao.Insert(nv);
        }

        public DataTable SelectOneUser(string userName)
        {
            return dao.SelectOneUser(userName);
        }
    }
}