using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using log4net;
using UKPI.Utils;
using UKPI.ValueObject;


namespace UKPI.DataAccessObject
{
    public class NhanVienUsersDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(NhanVienUsersDao));

        private const string PGetNhanVienQuanLy = "p_GetNhanVienQuanLy";
        private const string PGetNhanVienToAddNVQL = "p_GetNhanVienToAddNVQL";
        private const string PInsertNhanVienQuanLy = "p_InsertNhanVienQuanLy";
        private const string PRemoveNhanVienQuanLy = "p_RemoveNhanVienQuanLy";
        private const string PGetUserAvailible = "p_GetUserAvailible";

        private const string PGetNvUser  = "p_GetNV_User";
        public DataTable GetNhanVienQuanLy()
        {

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNhanVienQuanLy);
                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetNhanVienChamCong(string lName, string fName, string maNvUnilever, string loaiNv, string cardNo)
        {

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@LName", lName);
                sqlParams[1] = new SqlParameter("@FName", fName);
                sqlParams[2] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                sqlParams[3] = new SqlParameter("@LoaiNV", loaiNv);
                sqlParams[4] = new SqlParameter("@CardNo", cardNo);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNhanVienToAddNVQL, sqlParams);
                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void InsertNvQuanLy(string strSysId, string userId)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@UserId", userId);
                sqlParams[1] = new SqlParameter("@strSysId", strSysId);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PInsertNhanVienQuanLy, sqlParams);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

        }

        public void RemoveNvQl (List<ClsNhanVienUser> lstNvQl)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(DataServices.GetConnectionString());
                conn.Open();
                trans = conn.BeginTransaction();

                foreach (var item in lstNvQl)
                {
                    RemoveNvQuanLy(item, trans);
                }

               
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        public void RemoveNvQuanLy(ClsNhanVienUser nvQl, SqlTransaction trans)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@SysId", nvQl.SysId);
                sqlParams[1] = new SqlParameter("@MaNhanVien", nvQl.MaNhanVien);
                sqlParams[2] = new SqlParameter("@UserName", nvQl.UserName);
                sqlParams[3] = new SqlParameter("@LastUpDate", nvQl.LastUpDate);
                sqlParams[4] = new SqlParameter("@LastUpdateId", nvQl.lastUpdateId);

                DataServices.ExecuteNonQuery(trans,CommandType.StoredProcedure, PRemoveNhanVienQuanLy, sqlParams);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

        }

        public DataTable GetUserAvailible()
        {
            try
            {
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetUserAvailible);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }

        }

        public DataTable GetNvUser()
        {
            try
            {
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvUser);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

    }
}
