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
    public class NhanVienDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(NhanVienDao));

        private const string PGetNhanVienProWatch = "p_GetNhanVienProWatch";
        private const string PInsertNhanVien = "p_InsertNhanVien";

        private const string PSearchNhanVienChamCong = "p_SearchNhanVienChamCong";

        private const string PCheckMaNvUnilever = "p_CheckMaNvUnilever";
        private const string PCheckEmail = "p_CheckEmail";
        private const string PUpdateNhanVienCC = "p_UpdateNhanVienCC";

        private const string PGetNvQuanlyLevelHr = "p_GetNvQuanlyLevelHr";

        private const string PGetNvL3InL4 = "p_GetNvL3InL4";

        private const string PGetNvQuanlyLevel3Available = "p_GetNvQuanlyLevel3Available";

        private const string PAddL3ToL4 = "p_AddL3ToL4";


        private const string PGetNvL2InHr = "p_GetNvL2InHr";

        private const string PGetNvQuanlyLevel2Available = "p_GetNvQuanlyLevel2Available";

        private const string PGetNvL1InL2 = "p_GetNvL1InL2";

        private const string PGetNvQuanlyLevel1Available = "p_GetNvQuanlyLevel1Available";

        private const string PGetNvL0InL1 = "p_GetNvL0InL1";

        private const string PGetNvQuanlyLevel0Available = "p_GetNvQuanlyLevel0Available";

        private const string HUFS_LOADNHANVIEN = "HUFS_LOADNHANVIEN";

        private const string UpdateThongTinNhanVien = "UpdateThongTinNhanVien";
        private const string InsertThongTinNhanVien = "InsertNhanVien";


        public DataTable GetNhanVienProWatch()
        {

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNhanVienProWatch);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void InsertNhanVien(int sysId)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@SysId", sysId);
                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PInsertNhanVien, sqlParams);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }

        }


        public DataTable SearchNhanVienChamCong(string lName, string fName, string email, int isDataCc)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@LName", lName);
                sqlParams[1] = new SqlParameter("@FName", fName);
                sqlParams[2] = new SqlParameter("@Email", email);
                sqlParams[3] = new SqlParameter("@IsDataCC", isDataCc);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PSearchNhanVienChamCong, sqlParams);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable CheckMaNvUnilerver(string maNvUnilever)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PCheckMaNvUnilever, sqlParams);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

        public DataTable CheckEmail(string email)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Email", email);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PCheckEmail, sqlParams);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new DataTable();
            }

        }

        public void UpdateNhanVienCC(ClsNhanVien objNhanVien)
        {
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@SysId", objNhanVien.SysId);
                sqlParams[1] = new SqlParameter("@Email", objNhanVien.Email);
                sqlParams[2] = new SqlParameter("@MaNVUnilever", objNhanVien.MaNVUnilever);
                sqlParams[3] = new SqlParameter("@GioiTinh", objNhanVien.GioiTinh);
                sqlParams[4] = new SqlParameter("@IsDataCC", true);
                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PUpdateNhanVienCC, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);

            }

        }

        /// <summary>
        /// Process L3 To L4
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="maNvUnilever"></param>
        /// <param name="userName"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public DataTable GetNhanVienHr(string fName, string lName, string maNvUnilever, string userName, string cardNo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@FIRSTNAME", fName);
                sqlParams[2] = new SqlParameter("@LASTNAME", lName);
                sqlParams[3] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                sqlParams[4] = new SqlParameter("@CardNo", cardNo);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvQuanlyLevelHr, sqlParams);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetNvl3InL4(string userName, string maNvUnilever)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@MaNVUnilever", maNvUnilever);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvL3InL4, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable GetNvl3Available(string fName, string lName, string maNvUnilever, string userName, string cardNo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@FIRSTNAME", fName);
                sqlParams[2] = new SqlParameter("@LASTNAME", lName);
                sqlParams[3] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                sqlParams[4] = new SqlParameter("@CardNo", cardNo);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvQuanlyLevel3Available, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void AddNvL3ToL4(string userId, List<ClsNhanVien> lstnv, string userNameL4, int levelQuanLyL4)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = GetConnection(clsCommon.GetConnectionString());
                //Open connection
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();

                foreach (var item in lstnv)
                {
                    AddEachNvL3ToL4(userId, item, userNameL4, levelQuanLyL4, trans);
                }

                trans.Commit();//Commit
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                if (trans != null)
                    trans.Rollback();   //Rollback data
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();   //Close connection
            }
        }


        public void AddEachNvL3ToL4(string userId, ClsNhanVien nv, string userNameL4, int levelQuanLyL4, SqlTransaction trans)
        {
            try
            {

                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@ParentId", userNameL4);
                param[1] = new SqlParameter("@Parent_Level", levelQuanLyL4);
                param[2] = new SqlParameter("@ChildId", nv.Username);
                param[3] = new SqlParameter("@Child_Level", nv.LevelQuanLy);
                param[4] = new SqlParameter("@CreaterId", Int16.Parse(userId));


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PAddL3ToL4, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }


        /// <summary>
        /// Process L2 to L3
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="maNvUnilever"></param>
        /// <returns></returns>

        public DataTable GetNvl2InHr(string userName, string maNvUnilever)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@MaNVUnilever", maNvUnilever);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvL2InHr, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable GetNvl2Available(string fName, string lName, string maNvUnilever, string userName, string cardNo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@FIRSTNAME", fName);
                sqlParams[2] = new SqlParameter("@LASTNAME", lName);
                sqlParams[3] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                sqlParams[4] = new SqlParameter("@CardNo", cardNo);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvQuanlyLevel2Available, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void AddNvL2ToL3(string userId, List<ClsNhanVien> lstnv, string userNameL3, int levelQuanLyL3)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = GetConnection(clsCommon.GetConnectionString());
                //Open connection
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();

                foreach (var item in lstnv)
                {
                    AddEachNvL3ToL4(userId, item, userNameL3, levelQuanLyL3, trans);
                }

                trans.Commit();//Commit
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                if (trans != null)
                    trans.Rollback();   //Rollback data
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();   //Close connection
            }
        }


        public void AddEachNvL2ToL3(string userId, ClsNhanVien nv, string userNameL3, int levelQuanLyL3, SqlTransaction trans)
        {
            try
            {

                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@ParentId", userNameL3);
                param[1] = new SqlParameter("@Parent_Level", levelQuanLyL3);
                param[2] = new SqlParameter("@ChildId", nv.Username);
                param[3] = new SqlParameter("@Child_Level", nv.LevelQuanLy);
                param[4] = new SqlParameter("@CreaterId", Int16.Parse(userId));


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PAddL3ToL4, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }


        /// <summary>
        /// Process L1 to L2
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="maNvUnilever"></param>
        /// <returns></returns>

        public DataTable GetNvl1InL2(string userName, string maNvUnilever)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@MaNVUnilever", maNvUnilever);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvL1InL2, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable GetNvl1Available(string fName, string lName, string maNvUnilever, string userName, string cardNo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@FIRSTNAME", fName);
                sqlParams[2] = new SqlParameter("@LASTNAME", lName);
                sqlParams[3] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                sqlParams[4] = new SqlParameter("@CardNo", cardNo);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvQuanlyLevel1Available, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void AddNvL1ToL2(string userId, List<ClsNhanVien> lstnv, string userNameL2, int levelQuanLyL2)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = GetConnection(clsCommon.GetConnectionString());
                //Open connection
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();

                foreach (var item in lstnv)
                {
                    AddEachNvL3ToL4(userId, item, userNameL2, levelQuanLyL2, trans);
                }

                trans.Commit();//Commit
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                if (trans != null)
                    trans.Rollback();   //Rollback data
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();   //Close connection
            }
        }


        public void AddEachNvL1ToL2(string userId, ClsNhanVien nv, string userNameL2, int levelQuanLyL2, SqlTransaction trans)
        {
            try
            {

                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@ParentId", userNameL2);
                param[1] = new SqlParameter("@Parent_Level", levelQuanLyL2);
                param[2] = new SqlParameter("@ChildId", nv.Username);
                param[3] = new SqlParameter("@Child_Level", nv.LevelQuanLy);
                param[4] = new SqlParameter("@CreaterId", Int16.Parse(userId));


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PAddL3ToL4, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }


        /// <summary>
        /// Process L0 to L1
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="maNvUnilever"></param>
        /// <returns></returns>

        public DataTable GetNvl0InL1(string userName, string maNvUnilever)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@MaNVUnilever", maNvUnilever);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvL0InL1, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable GetNvl0Available(string fName, string lName, string maNvUnilever, string userName, string cardNo)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@USERNAME", userName);
                sqlParams[1] = new SqlParameter("@FIRSTNAME", fName);
                sqlParams[2] = new SqlParameter("@LASTNAME", lName);
                sqlParams[3] = new SqlParameter("@MaNVUnilever", maNvUnilever);
                sqlParams[4] = new SqlParameter("@CardNo", cardNo);
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNvQuanlyLevel0Available, sqlParams);


            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void AddNvL0ToL1(string userId, List<ClsNhanVien> lstnv, string userNameL1, int levelQuanLyL1)
        {

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = GetConnection(clsCommon.GetConnectionString());
                //Open connection
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();

                foreach (var item in lstnv)
                {
                    AddEachNvL3ToL4(userId, item, userNameL1, levelQuanLyL1, trans);
                }

                trans.Commit();//Commit
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());
                if (trans != null)
                    trans.Rollback();   //Rollback data
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();   //Close connection
            }
        }


        public void AddEachNvL0ToL1(string userId, ClsNhanVien nv, string userNameL1, int levelQuanLyL1, SqlTransaction trans)
        {
            try
            {

                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@ParentId", userNameL1);
                param[1] = new SqlParameter("@Parent_Level", levelQuanLyL1);
                param[2] = new SqlParameter("@ChildId", nv.Username);
                param[3] = new SqlParameter("@Child_Level", nv.LevelQuanLy);
                param[4] = new SqlParameter("@CreaterId", Int16.Parse(userId));


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PAddL3ToL4, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }


        public DataTable LoadNhanVien(string maNv, string tenNv)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@MaNV", maNv);
                param[1] = new SqlParameter("@TenNV", tenNv);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, HUFS_LOADNHANVIEN, param);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new DataTable();
            }
        }


        public bool InsertNhanVien(Employees emp)
        {
            try
            {

                var param = new SqlParameter[18];
                param[0] = new SqlParameter("@SysId", emp.SysId);
                param[1] = new SqlParameter("@EmployeeID", emp.EmployeeID);
                param[2] = new SqlParameter("@FullName", emp.FullName);
                param[3] = new SqlParameter("@GioiTinh", emp.GioiTinh);
                param[4] = new SqlParameter("@MaBHYT", emp.MaBHYT);
                param[5] = new SqlParameter("@NgayThangNamSinh", emp.NgayThangNamSinh);

                param[6] = new SqlParameter("@KhuVuc", emp.KhuVuc);
                param[7] = new SqlParameter("@ViTriLamViec", emp.ViTriLamViec);
                param[8] = new SqlParameter("@Email", emp.Email);
                param[9] = new SqlParameter("@Address", emp.Address);
                param[10] = new SqlParameter("@Phone", emp.Phone);
                param[11] = new SqlParameter("@DepartmentID", emp.DepartmentID);

                param[12] = new SqlParameter("@CreatedDate", emp.CreatedDate);
                param[13] = new SqlParameter("@CreatedBy", emp.CreatedBy);
                param[14] = new SqlParameter("@LastUpdatedDate", emp.LastUpdatedDate);
                param[15] = new SqlParameter("@LastUpdatedBy", emp.LastUpdatedBy);
                param[16] = new SqlParameter("@Status", emp.Status);
                param[17] = new SqlParameter("@CongTy", emp.CongTy);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, InsertThongTinNhanVien, param);

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool LuuCapNhatThongNhanVien(Employees emp)
        {
            try
            {
                var param = new SqlParameter[16];
                param[0] = new SqlParameter("@SysId", emp.SysId);
                param[1] = new SqlParameter("@EmployeeID", emp.EmployeeID);
                param[2] = new SqlParameter("@FullName", emp.FullName);
                param[3] = new SqlParameter("@GioiTinh", emp.GioiTinh);
                param[4] = new SqlParameter("@MaBHYT", emp.MaBHYT);
                param[5] = new SqlParameter("@NgayThangNamSinh", emp.NgayThangNamSinh);

                param[6] = new SqlParameter("@KhuVuc", emp.KhuVuc);
                param[7] = new SqlParameter("@ViTriLamViec", emp.ViTriLamViec);
                param[8] = new SqlParameter("@Email", emp.Email);
                param[9] = new SqlParameter("@Address", emp.Address);
                param[10] = new SqlParameter("@Phone", emp.Phone);
                param[11] = new SqlParameter("@DepartmentID", emp.DepartmentID);


                param[12] = new SqlParameter("@LastUpdatedDate", emp.LastUpdatedDate);
                param[13] = new SqlParameter("@LastUpdatedBy", emp.LastUpdatedBy);
                param[14] = new SqlParameter("@Status", emp.Status);
                param[15] = new SqlParameter("@CongTy", emp.CongTy);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, UpdateThongTinNhanVien, param);


                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool CheckExistEmp(string maNv)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@MaNV", maNv);
                param[1] = new SqlParameter("@TenNV", "");

                if (DataServices.ExecuteDataTable(CommandType.StoredProcedure, HUFS_LOADNHANVIEN, param).Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                };

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

    }
}
