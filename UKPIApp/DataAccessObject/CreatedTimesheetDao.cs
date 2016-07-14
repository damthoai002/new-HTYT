using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using log4net;
using log4net.Filter;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.DataAccessObject
{
    public class CreatedTimesheetDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(CreatedTimesheetDao));

        #region StoreName


        //Constant Store Proceduce
        public const string CreateTimesheetName = "tbCreateTimesheet";
        public const string PCheckExistLichLamViec = "p_KiemTraLichLamViec";
        public const string PKiemTraCaLamViecNhom = "p_KiemTraCaLamViecNhom";

        public const string PTaoLichLamViec = "p_TaoLichLamViec";
        public const string PXoaLichLamViec = "p_XoaLichLamViec";
        public const string PXemLichLamViec = "p_XemLichLamViec";

        public const string PGetNhomByTurongNhom = "p_Get_NhomByTruongNhom";
        public const string PGetTruongNhom = "p_Get_TruongNhom";

        public const string PCreateShiftForTeam = "p_CreateShiftForTeam";
        public const string PGetShiftForTeam = "p_GetShiftForTeam";
        public const string PUpdateShiftForTeam = "p_UpdateShiftForTeam";

        public const string PGetCcLichLamViecManual = "p_Get_CCLichLamViecManual";
        public const string PRemovedCcLichLamViecManual = "p_Removed_CCLichLamViecManual";
        public const string PAddOneLichLamViec = "p_AddOneLichLamViec";
        public const string PGetThanhVienNhomAvailable = "p_GetThanhVienNhomAvailable";
        public const string PGetDauDocThe = "sp_GetDauDocThe";

        public const string PGetCaLamViec = "sp_GetCaLamViec";
        public const string PGetMonthForCreateShiftOfTeam = "p_GetMonthForCreateShiftOfTeam";
        public const string PCreateShiftForTeamHanhChinh = "p_CreateShiftForTeamHanhChinh";



        #endregion
        /// <summary>
        /// CheckExistLichLamViec
        /// </summary>
        /// <param name="nhomId"></param>
        /// <param name="truongNhomId"></param>
        /// <param name="tuanLamViec"></param>
        /// <param name="isOutsource"></param>
        /// <returns></returns>
        public DataTable CheckExistLichLamViec(long nhomId, string truongNhomId, string tuNgay, string denNgay, string isOutsource)
        {
            try
            {
                var sqlParas = new SqlParameter[5];
                sqlParas[0] = new SqlParameter("@NhomId", nhomId);
                sqlParas[1] = new SqlParameter("@TruongNhomId", truongNhomId);
                sqlParas[2] = new SqlParameter("@TuNgay", tuNgay);
                sqlParas[3] = new SqlParameter("@DenNgay", denNgay);
                sqlParas[4] = new SqlParameter("@IsOutsource", isOutsource);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PCheckExistLichLamViec, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable CheckExistCalamViecNhom(long nhomId, string tuNgay, string denNgay)
        {
            try
            {
                var sqlParas = new SqlParameter[3];
                sqlParas[0] = new SqlParameter("@NhomId", nhomId);
                sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
                sqlParas[2] = new SqlParameter("@DenNgay", denNgay);


                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PKiemTraCaLamViecNhom, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public void DoCreateTimesheet(ClsCreateTimesheet t)
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


                CreateTimesheet(t, trans);


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
        public void CreateTimesheet(ClsCreateTimesheet t)
        {
            CreateTimesheet(t, null);
        }

        public void CreateTimesheet(ClsCreateTimesheet t, SqlTransaction trans)
        {
            try
            {
                var param = new SqlParameter[16];
                param[0] = new SqlParameter("@NhomId", t.NhomId);
                param[1] = new SqlParameter("@TenNhom", t.TenNhom);
                param[2] = new SqlParameter("@TruongNhomId", t.TruongNhomId);
                param[3] = new SqlParameter("@TenTruongNhom", t.TenTruongNhom);
                param[4] = new SqlParameter("@L0XacNhanId", t.L0XacNhanId);
                param[5] = new SqlParameter("@L0XacNhanTen", t.L0XacNhanTen);
                param[6] = new SqlParameter("@L1XacNhanId", t.L1XacNhanId);
                param[7] = new SqlParameter("@L1XacNhanTen", t.L1XacNhanTen);

                param[8] = new SqlParameter("@L2XacNhanId", t.L2XacNhanId);
                param[9] = new SqlParameter("@L2XacNhanTen", t.L2XacNhanTen);
                param[10] = new SqlParameter("@L3XacNhanId", t.L3XacNhanId);
                param[11] = new SqlParameter("@L3XacNhanTen", t.L3XacNhanTen);
                param[12] = new SqlParameter("@L4XacNhanId", t.L4XacNhanId);
                param[13] = new SqlParameter("@L4XacNhanTen", t.L4XacNhanTen);
                param[14] = new SqlParameter("@TuanLamViec", t.TuanLamViec);
                param[15] = new SqlParameter("@IsOutsource", t.IsOutsource);


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PTaoLichLamViec, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public void DoDeleteTimesheet(ClsCreateTimesheet t)
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


                DeleteTimesheet(t, trans);


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
        public void DeleteTimesheet(ClsCreateTimesheet t)
        {
            DeleteTimesheet(t, null);
        }

        public void DeleteTimesheet(ClsCreateTimesheet t, SqlTransaction trans)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@NhomId", t.NhomId);
                param[1] = new SqlParameter("@TruongNhomId", t.TruongNhomId);
                param[2] = new SqlParameter("@TuanLamViec", t.TuanLamViec);



                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PXoaLichLamViec, param);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable ViewTimesheet(long nhomId, string truongNhomId, string tuNgay, string denNgay)
        {
            try
            {
                var sqlParas = new SqlParameter[4];
                sqlParas[0] = new SqlParameter("@NhomId", nhomId);
                sqlParas[1] = new SqlParameter("@TruongNhomId", truongNhomId);
                sqlParas[2] = new SqlParameter("@TuNgay", tuNgay);
                sqlParas[3] = new SqlParameter("@DenNgay", denNgay);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PXemLichLamViec, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetNhomTruong(string truongNhomId)
        {
            try
            {
                var sqlParas = new SqlParameter[1];
                sqlParas[0] = new SqlParameter("@TruongNhom", truongNhomId);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetTruongNhom, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public DataTable GetNhomByNhomTruong(string truongNhomId)
        {
            try
            {
                var sqlParas = new SqlParameter[1];
                sqlParas[0] = new SqlParameter("@TruongNhomId", truongNhomId);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetNhomByTurongNhom, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public DataTable GetSchemaTable()
        {

            var dt = new DataTable("TaoLichLamViec");
            dt.Columns.Add(clsCommon.CreateTimesheet.L0XacNhan, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L0XacNhanMa, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L1XacNhan, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L1XacNhanMa, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L2XacNhan, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L2XacNhanMa, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L3XacNhan, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L3XacNhanMa, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L4XacNhan, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.L4XacNhanMa, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.TruongNhom, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.MaTruongNhom, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.Outsource, typeof(string)).DefaultValue = "";
            dt.Columns.Add(clsCommon.CreateTimesheet.Nhom, typeof(string)).DefaultValue = "";


            return dt;

        }


        public void CreateShiftForTeam(int nhom, string truongNhom, int tuTuan, int denTuan, int year, string dauDocTheVao, string dauDocTheRa)
        {

            try
            {

                for (int i = tuTuan; i <= denTuan; i++)
                {
                    CreateShiftForTeam(nhom, truongNhom, i, year,  dauDocTheVao, dauDocTheRa, null);
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());

                throw ex;
            }
        }



        public void CreateShiftForTeamHanhChinh(int nhom, string truongNhom, int tuTuan, int denTuan, int year, string dauDocTheVao, string dauDocTheRa)
        {

            try
            {

                for (int i = tuTuan; i <= denTuan; i++)
                {
                    CreateShiftForTeamHanhChinh(nhom, truongNhom, i, year, dauDocTheVao, dauDocTheRa, null);
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.ToString());

                throw ex;
            }
        }


        public void CreateShiftForTeam(int nhom, string truongNhom, int Tuan, int year,  string dauDocTheVao, string dauDocTheRa, SqlTransaction trans)
        {

            try
            {
                var sqlParas = new SqlParameter[6];
                sqlParas[0] = new SqlParameter("@Nhom", nhom);
                sqlParas[1] = new SqlParameter("@TruongNhom", truongNhom);
                sqlParas[2] = new SqlParameter("@Tuan", Tuan);
                sqlParas[3] = new SqlParameter("@Year", year.ToString());
                sqlParas[4] = new SqlParameter("@DauDocTheVao", dauDocTheVao);
                sqlParas[5] = new SqlParameter("@DauDocTheRa", dauDocTheRa);

                DataServices.ExecuteNonQuery( CommandType.StoredProcedure, PCreateShiftForTeam, sqlParas);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public void CreateShiftForTeamHanhChinh(int nhom, string truongNhom, int Tuan, int year, string dauDocTheVao, string dauDocTheRa, SqlTransaction trans)
        {

            try
            {
                var sqlParas = new SqlParameter[6];
                sqlParas[0] = new SqlParameter("@Nhom", nhom);
                sqlParas[1] = new SqlParameter("@TruongNhom", truongNhom);
                sqlParas[2] = new SqlParameter("@Tuan", Tuan);
                sqlParas[3] = new SqlParameter("@Year", year.ToString());
                sqlParas[4] = new SqlParameter("@DauDocTheVao", dauDocTheVao);
                sqlParas[5] = new SqlParameter("@DauDocTheRa", dauDocTheRa);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PCreateShiftForTeamHanhChinh, sqlParas);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public DataTable GetShiftForTeam(int nhom, int tuTuan, int denTuan, int year)
        {
            try
            {
                var sqlParas = new SqlParameter[4];
                sqlParas[0] = new SqlParameter("@Nhom", nhom);
                sqlParas[1] = new SqlParameter("@TuTuan", tuTuan.ToString());
                sqlParas[2] = new SqlParameter("@DenTuan", denTuan.ToString());
                sqlParas[3] = new SqlParameter("@Year", year.ToString());
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetShiftForTeam, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }


        public void UpdateShiftForTeam(DataRow row)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(clsCommon.GetConnectionString());
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();
                if (row.RowState == DataRowState.Modified)
                {
                    UpdateCaLamViecNhom(trans, row);
                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }


        public void UpdateCaLamViecNhom(SqlTransaction trans, DataRow row)
        {
            try
            {

                var para = new SqlParameter[6];
                para[0] = new SqlParameter("@SysId", row[clsCommon.CaLamViecNhom.SysId]);
                para[1] = new SqlParameter("@CaLamViec", row[clsCommon.CaLamViecNhom.MaCaLaViec]);
                para[2] = new SqlParameter("@DauDocTheVao", row[clsCommon.CaLamViecNhom.DauDocTheVaoId]);
                para[3] = new SqlParameter("@DauDocTheRa", row[clsCommon.CaLamViecNhom.DauDocTheRaId]);
                para[4] = new SqlParameter("@OT", row[clsCommon.CaLamViecNhom.IsOT]);
                para[5] = new SqlParameter("@PhanXuongName", row[clsCommon.CaLamViecNhom.PhanXuongName]);


                DataServices.ExecuteNonQuery(trans, CommandType.StoredProcedure, PUpdateShiftForTeam, para);
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public DataTable GetCcLichLamViec(string tuan, string tuNgay, string denNgay, string maTruongNhom, string nhomId)
        {
            try
            {

                var sqlParas = new SqlParameter[5];
                sqlParas[0] = new SqlParameter("@Tuan", tuan);
                sqlParas[1] = new SqlParameter("@TuNgay", tuNgay);
                sqlParas[2] = new SqlParameter("@DenNgay", denNgay);
                sqlParas[3] = new SqlParameter("@MaTruongNhom", maTruongNhom);
                sqlParas[4] = new SqlParameter("@NhomId", nhomId);



                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetCcLichLamViecManual, sqlParas);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public void RemoveTimesheets(List<ClsLichLamViec> lstLichLamViecs)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(clsCommon.GetConnectionString());
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();
                if (lstLichLamViecs.Count > 0)
                {
                    foreach (var item in lstLichLamViecs)
                    {
                        RemoveLichLamViec(Int32.Parse(item.SysId.ToString()), trans);
                    }

                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public void RemoveLichLamViec(int sysId, SqlTransaction trans)
        {
            try
            {
                var sqlParas = new SqlParameter[1];
                sqlParas[0] = new SqlParameter("@SysId", sysId);

                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PRemovedCcLichLamViecManual, sqlParas);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public void AddOneTimesheet(List<ClsCreateTimesheet> lstTimesheets)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(clsCommon.GetConnectionString());
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                trans = conn.BeginTransaction();
                if (lstTimesheets.Count > 0)
                {
                    foreach (var item in lstTimesheets)
                    {
                        AddOneLichLamViec(item, trans);
                    }

                }
                trans.Commit();
            }
            catch (System.Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }


        public void AddOneLichLamViec(ClsCreateTimesheet item, SqlTransaction trans)
        {
            try
            {
                var sqlParas = new SqlParameter[6];
                sqlParas[0] = new SqlParameter("@TruongNhom_ID", item.TruongNhomId);
                sqlParas[1] = new SqlParameter("@Nhom_Id", item.NhomId);
                sqlParas[2] = new SqlParameter("@NhanVien_Id", item.NhanVienId);
                sqlParas[3] = new SqlParameter("@NhanVien_Ten", item.NhanVienTen);
                sqlParas[4] = new SqlParameter("@Tuan", item.TuanLamViec);
                sqlParas[5] = new SqlParameter("@Ngay", item.NgayLamViec);


                DataServices.ExecuteNonQuery(CommandType.StoredProcedure, PAddOneLichLamViec, sqlParas);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }


        public DataTable GetNhanVienNhomAvailable(ClsCreateTimesheet item)
        {
            try
            {
                var sqlParas = new SqlParameter[8];
                sqlParas[0] = new SqlParameter("@TruongNhom_ID", item.TruongNhomId);
                sqlParas[1] = new SqlParameter("@TenTruongNhom", item.TenTruongNhom);
                sqlParas[2] = new SqlParameter("@Nhom_Id", item.NhomId);
                sqlParas[3] = new SqlParameter("@TenNhom", item.TenNhom);
                sqlParas[4] = new SqlParameter("@Tuan", item.TuanLamViec);
                sqlParas[5] = new SqlParameter("@Ngay", item.NgayLamViec);
                sqlParas[6] = new SqlParameter("@TenNhanVien", "");
                sqlParas[7] = new SqlParameter("@HoNhanVien", "");
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetThanhVienNhomAvailable, sqlParas);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetNhanVienNhomAvailable(ClsCreateTimesheet item, string tenNhanVien, string hoNhanVien)
        {
            try
            {
                tenNhanVien = tenNhanVien.Replace("*", "%").Replace("'", "''");
                hoNhanVien = hoNhanVien.Replace("*", "%").Replace("'", "''");

                var sqlParas = new SqlParameter[8];
                sqlParas[0] = new SqlParameter("@TruongNhom_ID", item.TruongNhomId);
                sqlParas[1] = new SqlParameter("@TenTruongNhom", item.TenTruongNhom);
                sqlParas[2] = new SqlParameter("@Nhom_Id", item.NhomId);
                sqlParas[3] = new SqlParameter("@TenNhom", item.TenNhom);
                sqlParas[4] = new SqlParameter("@Tuan", item.TuanLamViec);
                sqlParas[5] = new SqlParameter("@Ngay", item.NgayLamViec);
                sqlParas[6] = new SqlParameter("@TenNhanVien", tenNhanVien);
                sqlParas[7] = new SqlParameter("@HoNhanVien", hoNhanVien);


                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetThanhVienNhomAvailable, sqlParas);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetDauDocThe()
        {
            try
            {

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetDauDocThe);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
        public DataTable GetDaLamViec()
        {
            try
            {

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetCaLamViec);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

        public DataTable GetMonthOfSystem()
        {
            try
            {

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, PGetMonthForCreateShiftOfTeam);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }

    }
}
