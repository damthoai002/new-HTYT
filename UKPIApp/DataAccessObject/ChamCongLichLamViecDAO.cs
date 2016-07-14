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
    public class ChamCongLichLamViecDAO : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ChamCongLichLamViecDAO));

        public const string SysID = "SysID";
        public const string TruongNhom_ID = "TruongNhom-ID";
        public const string TruongNhom_TenNgan = "TruongNhom-TenNgan";
        public const string NhanVien_Id = "NhanVien-Id";
        public const string NhanVien_Ten = "NhanVien-Ten";
        public const string NhanVien_Loai_Id = "NhanVien-Loai-Id";
        public const string NhanVien_Loai_Ten = "NhanVien-Loai-Ten";
        public const string Tuan = "Tuan";
        public const string Ngay = "Ngay";
        public const string NgayRa = "NgayRa";
        public const string Ca_Id = "Ca-Id";
        public const string Ca_DienGiai = "Ca-DienGiai";
        public const string NgayTrongTuan = "NgayTrongTuan";
        public const string IsOutSource = "IsOutSource";
        public const string KhuVucLv_Id = "KhuVucLv-Id";
        public const string DauDocTheVao_Id = "DauDocTheVao-Id";
        public const string DauDocTheRa_Id = "DauDocTheRa-Id";
        public const string Vao = "Vao";
        public const string Ra = "Ra";
        public const string Vao_L1 = "Vao-L1";
        public const string Ra_L1 = "Ra-L1";
        public const string On_Off = "On_Off";
        public const string CoDangKyOT = "CoDangKyOT";
        public const string OTHeThongTinh = "OTHeThongTinh";
        public const string OTL1 = "OTL1";
        public const string CoPhep = "CoPhep";
        public const string DuocTinhCong = "DuocTinhCong";
        public const string L0XacNhan = "L0XacNhan";
        public const string L0XacNhan_Id = "L0XacNhan-Id";
        public const string L0XacNhan_TenNgan = "L0XacNhan-TenNgan";
        public const string L0XacNhan_Date = "L0XacNhan-Date";
        public const string L1XacNhan = "L1XacNhan";
        public const string L1XacNhan_Id = "L1XacNhan-Id";
        public const string L1XacNhan_TenNgan = "L1XacNhan-TenNgan";
        public const string L1XacNhan_Date = "L1XacNhan-Date";
        public const string L1XacNhan_GhiChu = "L1XacNhan-GhiChu";
        public const string L2XacNhan = "L2XacNhan";
        public const string L2XacNhan_Id = "L2XacNhan-Id";
        public const string L2XacNhan_TenNgan = "L2XacNhan-TenNgan";
        public const string L2XacNhan_Date = "L2XacNhan-Date";
        public const string L3XacNhan = "L3XacNhan";
        public const string L3XacNhan_Id = "L3XacNhan-Id";
        public const string L3XacNhan_TenNgan = "L3XacNhan-TenNgan";
        public const string L3XacNhan_Date = "L3XacNhan-Date";
        public const string L4XacNhan = "L4XacNhan";
        public const string L4XacNhan_Id = "L4XacNhan-Id";
        public const string L4XacNhan_TenNgan = "L4XacNhan-TenNgan";
        public const string L4XacNhan_Date = "L4XacNhan-Date";
        public const string MaUnilverNVThayThe = "MaUnilverNVThayThe";
        public const string NhanVienThayThe_TenNgan = "NhanVienThayThe-TenNgan";
        public const string DaLayDuLieuChamCong = "DaLayDuLieuChamCong";
        public const string ThoiGianQuyDinhVao = "ThoiGianQuyDinhVao";
        public const string ThoiGianQuyDinhRa = "ThoiGianQuyDinhRa";
        public const string MicCard = "MicCard";
        public const string Note = "Note";
        public const string Is_Active = "Is_Active";
        public const string CreateDate = "CreateDate";
        public const string CreaterId = "CreaterId";
        public const string LastUpDate = "LastUpDate";
        public const string lastUpdateId = "lastUpdateId";

        public const string CaThucTeId = "CaThucTeId";
        public const string CaThucTe = "CaThucTe";
        public const string OTVaoSom = "OTVaoSom";
        public const string OTRaTre = "OTRaTre";

        public const string OTHeSo1 = "OTHeSo1";
        public const string OTHeSo2 = "OTHeSo2";
        public const string OTHeSo3 = "OTHeSo3";
        public const string LyDoOT = "LyDoOT";
        public const string LyDoNghi = "LyDoNghi";


        public const string ChamCong_LichLamViecName = "tb_CC_ChamCong_LichLamviec";

        private const string SP_CHAMCONGLICHLAMVIECL0_SEARCH = "p_Get_ChamCongLichLamViecL0";
        private const string SP_CHAMCONGLICHLAMVIECL0_Approve = "p_Approve_ChamCongLichLamViecL0";

        private const string SP_CHAMCONGLICHLAMVIECL1_SEARCH = "p_Get_ChamCongLichLamViecL1";
        private const string SP_CHAMCONGLICHLAMVIECL1_Update = "p_Update_ChamCongLichLamViecL1";
        private const string SP_CHAMCONGLICHLAMVIECL1_Approve = "p_Approve_ChamCongLichLamViecL1";



        private const string SP_CHAMCONGLICHLAMVIECL2_SEARCH = "p_Get_ChamCongLichLamViecL2";

        private const string SP_CHAMCONGLICHLAMVIECL2_Approve = "p_Approve_ChamCongLichLamViecL2";
        private const string SP_CHAMCONGLICHLAMVIECL3_SEARCH = "p_Get_ChamCongLichLamViecL3";

        private const string SP_CHAMCONGLICHLAMVIECL4_SEARCH = "p_Update_ChamCongLichLamViecL4";

        private const string SP_CHAMCONGLICHLAMVIECL3_Approve = "p_Approve_ChamCongLichLamViecL3";
        private const string SP_CHAMCONGLICHLAMVIECL4_Approve = "p_Approve_ChamCongLichLamViecL4";

        private const string SP_GetNhanVien = "p_Get_NhanVien";
        private const string SP_GetTruongNhom = "p_Get_TruongNhom";
        private const string SP_GetNhomByTruongNhom = "p_Get_NhomByTruongNhom";

        private const string SP_CHAMCONGLICHLAMVIEC_Rejected = "p_Reject_ChamCongLichLamViec";
        private const string SP_CHAMCONGLICHLAMVIECL1_GetTimesheetFromProwatch = "p_GetTSFromProwatch";
        private const string SP_CHAMCONGLICHLAMVIECL0_GetTimesheetFromProwatch = "p_GetTSFromProwatchL0";


        private const string SP_GetEmailOfLeverApprove = "p_GetEmailOfLeverApprove";
        private const string SP_GetTruongNhomL1 = "p_GetTruongNhomL1";

        private const string SP_GetTruongNhomL0ByL1 = "p_GetTruongNhomL0ByL1";
        private const string SP_GetTruongNhomL1ByL3 = "p_GetTruongNhomL1ByL3";
        private const string SP_GetTruongNhomL1ByL4 = "p_GetTruongNhomL1ByL4";

        public DataTable GetLichLamViecL0(string tuan, string tuNgay, string denNgay,
            string truongNhom, string onOff, string ca, string l0XacNhan, string nhomId)
        {
            var sqlParas = new SqlParameter[8];
            sqlParas[0] = new SqlParameter("@Tuan", tuan);
            sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
            sqlParas[2] = new SqlParameter("@DenNgay", denNgay);
            sqlParas[3] = new SqlParameter("@MaTruongNhom", truongNhom);
            sqlParas[4] = new SqlParameter("@OnOff", onOff);
            sqlParas[5] = new SqlParameter("@Ca", ca);
            sqlParas[6] = new SqlParameter("@L0XacNhan", l0XacNhan);
            sqlParas[7] = new SqlParameter("@Nhom_Id", nhomId);

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL0_SEARCH, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetLichLamViec(string tuan, string tuNgay, string denNgay,
            string truongNhom, string onOff, string ca, string l1XacNhan, string nhomId)
        {
            var sqlParas = new SqlParameter[8];
            sqlParas[0] = new SqlParameter("@Tuan", tuan);
            sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
            sqlParas[2] = new SqlParameter("@DenNgay", denNgay);
            sqlParas[3] = new SqlParameter("@MaTruongNhom", truongNhom);
            sqlParas[4] = new SqlParameter("@OnOff", onOff);
            sqlParas[5] = new SqlParameter("@Ca", ca);
            sqlParas[6] = new SqlParameter("@L1XacNhan", l1XacNhan);
            sqlParas[7] = new SqlParameter("@Nhom_Id", nhomId);

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL1_SEARCH, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable GetLichLamViecL2(string tuan, string tuNgay, string denNgay,
                    string truongNhom, string onOff, string ca, string l2XacNhan, string nhomId)
        {
            var sqlParas = new SqlParameter[8];
            sqlParas[0] = new SqlParameter("@Tuan", tuan);
            sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
            sqlParas[2] = new SqlParameter("@DenNgay", denNgay);
            sqlParas[3] = new SqlParameter("@MaTruongNhom", truongNhom);
            sqlParas[4] = new SqlParameter("@OnOff", onOff);
            sqlParas[5] = new SqlParameter("@Ca", ca);
            sqlParas[6] = new SqlParameter("@L2XacNhan", l2XacNhan);
            sqlParas[7] = new SqlParameter("@Nhom_Id", nhomId);

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL2_SEARCH, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetLichLamViecL3(string tuan, string tuNgay, string denNgay,
                         string truongNhom, string onOff, string ca, string l3XacNhan, string nhomId)
        {
            var sqlParas = new SqlParameter[8];
            sqlParas[0] = new SqlParameter("@Tuan", tuan);
            sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
            sqlParas[2] = new SqlParameter("@DenNgay", denNgay);
            sqlParas[3] = new SqlParameter("@MaTruongNhom", truongNhom);
            sqlParas[4] = new SqlParameter("@OnOff", onOff);
            sqlParas[5] = new SqlParameter("@Ca", ca);
            sqlParas[6] = new SqlParameter("@L3XacNhan", l3XacNhan);
            sqlParas[7] = new SqlParameter("@Nhom_Id", nhomId);

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL3_SEARCH, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public DataTable GetLichLamViecL4(string tuan, string tuNgay, string denNgay,
                    string truongNhom, string onOff, string ca, string l4XacNhan, string nhomId)
        {
            var sqlParas = new SqlParameter[8];
            sqlParas[0] = new SqlParameter("@Tuan", tuan);
            sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
            sqlParas[2] = new SqlParameter("@DenNgay", denNgay);
            sqlParas[3] = new SqlParameter("@MaTruongNhom", truongNhom);
            sqlParas[4] = new SqlParameter("@OnOff", onOff);
            sqlParas[5] = new SqlParameter("@Ca", ca);
            sqlParas[6] = new SqlParameter("@L4XacNhan", l4XacNhan);
            sqlParas[7] = new SqlParameter("@Nhom_Id", nhomId);

            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL4_SEARCH, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void SaveLichLamViec(List<ClsLichLamViec> lst)
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

                foreach (var item in lst)
                {
                    SaveData(item, trans);
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

        public void SaveData(ClsLichLamViec item)
        {

            SaveData(item, null);
        }

        public void SaveData(ClsLichLamViec item, SqlTransaction trans)
        {
            try
            {

                var param = new SqlParameter[16];
                param[0] = new SqlParameter("@SysID", item.SysId);
                param[1] = new SqlParameter("@Vao_L1", item.Vao_L1);
                param[2] = new SqlParameter("@Ra_L1", item.Ra_L1);
                param[3] = new SqlParameter("@OTL1", item.OTL1);
                param[4] = new SqlParameter("@Note", item.Note);
                param[5] = new SqlParameter("@LastUpDate", item.LastUpDate);
                param[6] = new SqlParameter("@lastUpdateId", item.lastUpdateId);

                param[7] = new SqlParameter("@CaThucTe", item.CaThucTe);
                param[8] = new SqlParameter("@OtHeSo1", item.OtHeSo1);
                param[9] = new SqlParameter("@OtHeSo2", item.OtHeSo2);
                param[10] = new SqlParameter("@OtHeSo3", item.OtHeSo3);
                param[11] = new SqlParameter("@LyDoOt", item.LyDoOt);
                param[12] = new SqlParameter("@DuocTinhCong", item.DuocTinhCong);
                param[13] = new SqlParameter("@LyDoNghi", item.LyDoNghi);
                param[14] = new SqlParameter("@MaUnilverNvThayThe", item.MaUnilverNvThayThe);
                param[15] = new SqlParameter("@NhanVienThayThe_TenNgan", item.NhanVienThayThe_TenNgan);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL1_Update, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }

        public void XacNhanLichLamViecL0(List<ClsLichLamViec> lst)
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

                foreach (var item in lst)
                {
                    ApproveL0(item, trans);
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


        public void ApproveL0(ClsLichLamViec item)
        {

            ApproveL0(item, null);
        }

        public void ApproveL0(ClsLichLamViec item, SqlTransaction trans)
        {

            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@SysID", item.SysId);
                param[1] = new SqlParameter("@L0XacNhan", item.L0XacNhan);
                param[2] = new SqlParameter("@L0XacNhan_Id", item.L0XacNhan_Id);
                param[3] = new SqlParameter("@L0XacNhan_TenNgan", item.L0XacNhan_TenNgan);
                param[4] = new SqlParameter("@L0XacNhan_Ngay", item.L0XacNhan_Date);
                param[5] = new SqlParameter("@Note", item.Note);
                param[6] = new SqlParameter("@LastUpDate", item.LastUpDate);
                param[7] = new SqlParameter("@lastUpdateId", item.lastUpdateId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL0_Approve, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }






        public void XacNhanLichLamViec(List<ClsLichLamViec> lst)
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

                foreach (var item in lst)
                {
                    Approve(item, trans);
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


        public void Approve(ClsLichLamViec item)
        {

            Approve(item, null);
        }

        public void Approve(ClsLichLamViec item, SqlTransaction trans)
        {

            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@SysID", item.SysId);
                param[1] = new SqlParameter("@L1XacNhan", item.L1XacNhan);
                param[2] = new SqlParameter("@L1XacNhan_Id", item.L1XacNhan_Id);
                param[3] = new SqlParameter("@L1XacNhan_TenNgan", item.L1XacNhan_TenNgan);
                param[4] = new SqlParameter("@L1XacNhan_Ngay", item.L1XacNhan_Date);
                param[5] = new SqlParameter("@Note", item.Note);
                param[6] = new SqlParameter("@LastUpDate", item.LastUpDate);
                param[7] = new SqlParameter("@lastUpdateId", item.lastUpdateId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL1_Approve, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }



        public void XacNhanLichLamViecL2(List<ClsLichLamViec> lst)
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

                foreach (var item in lst)
                {
                    ApproveL2(item, trans);
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

        public void ApproveL2(ClsLichLamViec item)
        {

            ApproveL2(@item, null);
        }

        public void ApproveL2(ClsLichLamViec item, SqlTransaction trans)
        {

            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@SysID", item.SysId);
                param[1] = new SqlParameter("@L2XacNhan", item.L2XacNhan);
                param[2] = new SqlParameter("@L2XacNhan_Id", item.L2XacNhan_Id);
                param[3] = new SqlParameter("@L2XacNhan_TenNgan", item.L2XacNhan_TenNgan);
                param[4] = new SqlParameter("@L2XacNhan_Ngay", item.L2XacNhan_Date);
                param[5] = new SqlParameter("@Note", item.Note);
                param[6] = new SqlParameter("@LastUpDate", item.LastUpDate);
                param[7] = new SqlParameter("@lastUpdateId", item.lastUpdateId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL2_Approve, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }

        public void XacNhanLichLamViecL3(List<ClsLichLamViec> lst)
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

                foreach (var item in lst)
                {
                    ApproveL3(item, trans);
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

        public void ApproveL3(ClsLichLamViec item)
        {

            ApproveL3(@item, null);
        }

        public void ApproveL3(ClsLichLamViec item, SqlTransaction trans)
        {

            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@SysID", item.SysId);
                param[1] = new SqlParameter("@L3XacNhan", item.L3XacNhan);
                param[2] = new SqlParameter("@L3XacNhan_Id", item.L3XacNhan_Id);
                param[3] = new SqlParameter("@L3XacNhan_TenNgan", item.L3XacNhan_TenNgan);
                param[4] = new SqlParameter("@L3XacNhan_Ngay", item.L3XacNhan_Date);
                param[5] = new SqlParameter("@Note", item.Note);
                param[6] = new SqlParameter("@LastUpDate", item.LastUpDate);
                param[7] = new SqlParameter("@lastUpdateId", item.lastUpdateId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL3_Approve, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }


        public void XacNhanLichLamViecL4(List<ClsLichLamViec> lst)
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

                foreach (var item in lst)
                {
                    ApproveL4(item, trans);
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

        public void ApproveL4(ClsLichLamViec item)
        {

            ApproveL4(@item, null);
        }

        public void ApproveL4(ClsLichLamViec item, SqlTransaction trans)
        {

            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@SysID", item.SysId);
                param[1] = new SqlParameter("@L4XacNhan", item.L4XacNhan);
                param[2] = new SqlParameter("@L4XacNhan_Id", item.L4XacNhan_Id);
                param[3] = new SqlParameter("@L4XacNhan_TenNgan", item.L4XacNhan_TenNgan);
                param[4] = new SqlParameter("@L4XacNhan_Ngay", item.L4XacNhan_Date);
                param[5] = new SqlParameter("@Note", item.Note);
                param[6] = new SqlParameter("@LastUpDate", item.LastUpDate);
                param[7] = new SqlParameter("@lastUpdateId", item.lastUpdateId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL4_Approve, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }

        public void RejectLichLamViec(string sysId, string note, string lastUpdate, string lastUpId, string lever)
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


                Rejected(sysId, note, lastUpdate, lastUpId, lever, trans);


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

        public void Rejected(string sysId, string note, string lastUpdate, string lastUpId, string level)
        {

            Rejected(sysId, note, lastUpdate, lastUpId, level, null);
        }

        public void Rejected(string sysId, string note, string lastUpdate, string lastUpId, string level, SqlTransaction trans)
        {

            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@SysID", sysId);
                param[1] = new SqlParameter("@Note", note);
                param[2] = new SqlParameter("@LastUpDate", lastUpdate);
                param[3] = new SqlParameter("@lastUpdateId", lastUpId);
                param[4] = new SqlParameter("@Level", level);


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIEC_Rejected, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }

        }


        public void LayDuLieuChamCongVaLuu(string lastUpdatedate, string lastUpId,
            string tuan, string tuNgay, string denNgay,
            string maTruongNhom, string ca, string nhomId)
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


                GetTimesheetFromProwatch(lastUpdatedate, lastUpId, tuan, tuNgay, denNgay, maTruongNhom, ca, nhomId, trans);


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

        public void GetTimesheetFromProwatch(string lastUpdatedate, string lastUpId, string tuan, string tuNgay, string denNgay,
            string maTruongNhom, string ca, string nhomId)
        {
            GetTimesheetFromProwatch(lastUpdatedate, lastUpId, tuan, tuNgay, denNgay, maTruongNhom, ca, nhomId, null);
        }
        public void GetTimesheetFromProwatch(string lastUpdateDate
            , string lastUpId, string tuan, string tuNgay, string denNgay,
            string maTruongNhom, string ca, string nhomId, SqlTransaction trans)
        {
            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@LastUpDate", lastUpdateDate);
                param[1] = new SqlParameter("@lastUpdateId", lastUpId);
                param[2] = new SqlParameter("@Tuan", tuan);
                param[3] = new SqlParameter("@TuNgay", tuNgay);
                param[4] = new SqlParameter("@DenNgay", denNgay);
                param[5] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                param[6] = new SqlParameter("@Ca", ca);
                param[7] = new SqlParameter("@Nhom_Id", nhomId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL1_GetTimesheetFromProwatch, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }


        public void LayDuLieuChamCongVaLuuL0(string lastUpdatedate, string lastUpId,
      string tuan, string tuNgay, string denNgay,
      string maTruongNhom, string ca, string nhomId)
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


                GetTimesheetFromProwatchL0(lastUpdatedate, lastUpId, tuan, tuNgay, denNgay, maTruongNhom, ca, nhomId, trans);


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


        public void GetTimesheetFromProwatchL0(string lastUpdatedate, string lastUpId, string tuan, string tuNgay, string denNgay,
    string maTruongNhom, string ca, string nhomId)
        {
            GetTimesheetFromProwatchL0(lastUpdatedate, lastUpId, tuan, tuNgay, denNgay, maTruongNhom, ca, nhomId, null);
        }
        public void GetTimesheetFromProwatchL0(string lastUpdateDate
            , string lastUpId, string tuan, string tuNgay, string denNgay,
            string maTruongNhom, string ca, string nhomId, SqlTransaction trans)
        {
            try
            {
                var param = new SqlParameter[8];
                param[0] = new SqlParameter("@LastUpDate", lastUpdateDate);
                param[1] = new SqlParameter("@lastUpdateId", lastUpId);
                param[2] = new SqlParameter("@Tuan", tuan);
                param[3] = new SqlParameter("@TuNgay", tuNgay);
                param[4] = new SqlParameter("@DenNgay", denNgay);
                param[5] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                param[6] = new SqlParameter("@Ca", ca);
                param[7] = new SqlParameter("@Nhom_Id", nhomId);

                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, SP_CHAMCONGLICHLAMVIECL0_GetTimesheetFromProwatch, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }


        public DataTable GetNhanVien(int nhom, string truongNhom)
        {

            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@Nhom", nhom);
                param[1] = new SqlParameter("@TruongNhom", truongNhom);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetNhanVien, param);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetTruongNhom(int sysId)
        {

            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@SysId", sysId);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetTruongNhom);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
        public DataTable GetNhomByTruongNhom(int sysId)
        {

            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@SysId", sysId);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetNhomByTruongNhom);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetEmailOfLeverApprove(int sysId, int lever)
        {

            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@SysId", sysId);
                param[1] = new SqlParameter("@Lever", lever);

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetEmailOfLeverApprove);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetTruongNhomL1(string userL2)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserL2", userL2);


                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetTruongNhomL1, param);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetTruongNhomL1ByL3(string userL3)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserL3", userL3);


                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetTruongNhomL1ByL3, param);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetTruongNhomL1ByL4(string userL4)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserL4", userL4);


                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetTruongNhomL1ByL4, param);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetTruongNhomL0ByL1(string userL1)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserL1", userL1);


                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, SP_GetTruongNhomL0ByL1, param);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }


    }
}
