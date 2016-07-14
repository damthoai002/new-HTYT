using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.DataAccessObject
{
    public class ThongTinKhamBenhDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ThongTinKhamBenhDao));
        private const string p_HUFS_GetMaxMaKhamBenh = "p_HUFS_GetMaxMaKhamBenh";
        private const string p_HUFS_TinhSoLuongThuoc = "p_HUFS_TinhSoLuongThuoc";
        private const string p_HUFS_insertDataForTransaction = "p_HUFS_insertDataForTransaction";
        private const string p_HUFS_InsertThongTinKhamBenh = "p_HUFS_InsertThongTinKhamBenh";
        private const string p_HUFS_searchThongTinBenhNhan = "p_HUFS_searchThongTinBenhNhan";
        private const string p_HUFS_TinhSoLuongThuocTrongKho = "p_HUFS_TinhSoLuongThuocTrongKho";
        private const string p_HUFS_insertDataForTransactionTheoKho = "p_HUFS_insertDataForTransactionTheoKho";
        private const string p_HUFS_ProcessTransactionXuatKho = "p_HUFS_ProcessTransactionXuatKho";
        private const string p_HUFS_RollbackThongTinKhamBenh = "p_HUFS_RollbackThongTinKhamBenh";
        private const string p_HUFS_GetMaKhamBenhForPrint = "p_HUFS_GetMaKhamBenhForPrint";
        private const string p_HUFS_CapNhatWareHouse = "p_HUFS_CapNhatWareHouse";
        private const string GetThongTinKhamBenhBenhNhan = "GetThongTinKhamBenhBenhNhan";
        private const string GetThongTinThuocKhamBenh = "GetThongTinThuocKhamBenh";

        public ThongTinBenhNhan GetThongTinBenhNhan(string maBenhNhan)
        {
            ThongTinBenhNhan info = new ThongTinBenhNhan();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT EmployeeID,FullName,GioiTinh,MaBHYT,YEAR(NgayThangNamSinh) AS NamSinh,KhuVuc,DepartmentID FROM HUFS_EMPLOYEE WHERE EmployeeID = @MaBenhNhan", con);
                cmd.Parameters.Add("@MaBenhNhan", SqlDbType.VarChar, 50).Value = maBenhNhan;
                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    PhongKham pk = new PhongKham();
                    info.EmployeeID = reader[0] != null ? (string)reader[0] : "";
                    info.FullName = reader[1] != null ? (string)reader[1] : "";
                    info.GioiTinh = reader[2] != null ? (string)reader[2] : "";
                    info.MaBHYT = reader[3] != null ? (string)reader[3] : "";
                    info.NamSinh = reader[4] != null ? (string)reader[4] : "";
                    info.KhuVuc = reader[5] != null ? (string)reader[5] : "";
                    info.BoPhan = reader[6] != null ? (int)reader[6] : 0;
                    break;
                }

                reader.Close();
                return info;
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int CheckSoLuongThuocTrongKho(string maThuoc, int soLuongXuat)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@maThuoc", maThuoc);
                Params[1] = new SqlParameter("@soluongxuat", soLuongXuat);
                int soLuong = -1;
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_TinhSoLuongThuoc, Params);
                foreach (DataRow dr in dtResult.Rows)
                {
                    soLuong = int.Parse(dr["Result"].ToString());
                    break;
                }
                return soLuong;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return -1;
            }
        }

        public int CheckSoLuongThuocTrongKho(string maThuoc, long soLuongXuat, string tenKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@maThuoc", maThuoc);
                Params[1] = new SqlParameter("@soluongxuat", soLuongXuat);
                Params[2] = new SqlParameter("@tenKho", tenKho);
                int soLuong = -1;
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_TinhSoLuongThuocTrongKho, Params);
                foreach (DataRow dr in dtResult.Rows)
                {
                    soLuong = int.Parse(dr["Result"].ToString());
                    break;
                }
                return soLuong;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return -1;
            }
        }
        private bool InsertThongTinGiaoDich(string maThuoc, double soLuong, bool nhapKho, bool xuatKho, string maKhamBenh)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[5];
                Params[0] = new SqlParameter("@MaThuoc", maThuoc);
                Params[1] = new SqlParameter("@Soluong", soLuong);
                Params[2] = new SqlParameter("@NhapKho", nhapKho);
                Params[3] = new SqlParameter("@XuatKho", xuatKho);
                Params[4] = new SqlParameter("@MaKhamBenh", maKhamBenh);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_insertDataForTransaction, Params);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool ProcessGiaoDichXuatKho(string listTransaction)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@ListTransaction", listTransaction);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessTransactionXuatKho, Params);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private List<ThongTinGiaoDich> InsertThongTinGiaoDich(string maThuoc, long soLuong, bool nhapKho, bool xuatKho, string maKhamBenh, string tenKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[6];
                Params[0] = new SqlParameter("@MaThuoc", maThuoc);
                Params[1] = new SqlParameter("@Soluong", soLuong);
                Params[2] = new SqlParameter("@NhapKho", nhapKho);
                Params[3] = new SqlParameter("@XuatKho", xuatKho);
                Params[4] = new SqlParameter("@MaKhamBenh", maKhamBenh);
                Params[5] = new SqlParameter("@TenKho", tenKho);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_insertDataForTransactionTheoKho, Params);
                return this.ConvertDataTableToList<ThongTinGiaoDich>(dtResult);
            }
            catch
            {
                return null;
            }
        }
        /*
        private bool InsertThongTinGiaoDich(string maThuoc, double soLuong, bool nhapKho, bool xuatKho, string maKhamBenh,string tenKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[6];
                Params[0] = new SqlParameter("@MaThuoc", maThuoc);
                Params[1] = new SqlParameter("@Soluong", soLuong);
                Params[2] = new SqlParameter("@NhapKho", nhapKho);
                Params[3] = new SqlParameter("@XuatKho", xuatKho);
                Params[4] = new SqlParameter("@MaKhamBenh", maKhamBenh);
                Params[5] = new SqlParameter("@TenKho", tenKho);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_insertDataForTransactionTheoKho, Params);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SaveThongTinKhamBenh(ThongTinKhamBenh thongTinKhamBenh)
        {

            string conStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    SqlParameter[] Params = new SqlParameter[24];
                    Params[0] = new SqlParameter("@MaKhamBenh", thongTinKhamBenh.MaKhamBenh);
                    Params[1] = new SqlParameter("@PhongKhamBenh", thongTinKhamBenh.PhongKhamBenh);
                    Params[2] = new SqlParameter("@NgayKhamBenh", thongTinKhamBenh.NgayKhamBenh);
                    Params[3] = new SqlParameter("@BenhNhan", thongTinKhamBenh.BenhNhan);
                    Params[4] = new SqlParameter("@MaBenhNhan", thongTinKhamBenh.MaBenhNhan);
                    Params[5] = new SqlParameter("@GioiTinh", thongTinKhamBenh.GioiTinh);
                    Params[6] = new SqlParameter("@NamSinh", thongTinKhamBenh.NamSinh);
                    Params[7] = new SqlParameter("@BoPhan", thongTinKhamBenh.BoPhan);
                    Params[8] = new SqlParameter("@CongTy", thongTinKhamBenh.CongTy);
                    Params[9] = new SqlParameter("@KhuVuc", thongTinKhamBenh.KhuVuc);
                    Params[10] = new SqlParameter("@NhomBenh", thongTinKhamBenh.NhomBenh);
                    Params[11] = new SqlParameter("@ChuanDoan", thongTinKhamBenh.ChuanDoan);
                    Params[12] = new SqlParameter("@QuyetDinhNghi", thongTinKhamBenh.QuyetDinhNghi);
                    Params[13] = new SqlParameter("@TuNgay", thongTinKhamBenh.QuyetDinhNghiPhep.TuNgay);
                    Params[14] = new SqlParameter("@DenNgay", thongTinKhamBenh.QuyetDinhNghiPhep.DenNgay);
                    Params[15] = new SqlParameter("@LyDo", thongTinKhamBenh.QuyetDinhNghiPhep.LyDo);
                    Params[16] = new SqlParameter("@DienGiai", thongTinKhamBenh.QuyetDinhNghiPhep.DienGiai);
                    Params[17] = new SqlParameter("@SoNgayNghi", thongTinKhamBenh.QuyetDinhNghiPhep.SoNgayNghi);
                    Params[18] = new SqlParameter("@TongTien", thongTinKhamBenh.TongTien);
                    Params[19] = new SqlParameter("@MaBHYT", thongTinKhamBenh.MaBHYT);
                    Params[20] = new SqlParameter("@MaICD", thongTinKhamBenh.MaICD);
                    Params[21] = new SqlParameter("@DienGiaiICD", thongTinKhamBenh.DienGiaiICD);
                    Params[22] = new SqlParameter("@LyDoChiTiet", thongTinKhamBenh.QuyetDinhNghiPhep.LyDoChiTiet);
                    Params[23] = new SqlParameter("@ChuThich", thongTinKhamBenh.QuyetDinhNghiPhep.ChuThich);


                    DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_InsertThongTinKhamBenh, Params);
                    if (thongTinKhamBenh.ThongTinToaThuoc != null && thongTinKhamBenh.ThongTinToaThuoc.Count > 0)
                    {
                        this.BulkInsert(ConvertToDataTable(thongTinKhamBenh.ThongTinToaThuoc), "HUFS_KHAMBENH_DETAIL");
                        for (int i = 0; i < thongTinKhamBenh.ThongTinToaThuoc.Count; i++)
                        {
                            bool result = InsertThongTinGiaoDich(thongTinKhamBenh.ThongTinToaThuoc[i].MaThuoc,
                                                                 double.Parse(thongTinKhamBenh.ThongTinToaThuoc[i].SoLuong),
                                                                 false,
                                                                 true,
                                                                 thongTinKhamBenh.ThongTinToaThuoc[i].MaKhamBenh, System.Configuration.ConfigurationManager.AppSettings["RCLINIC00001"]);
                            if (!result)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    log.Error(ex.Message, ex);
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;


        }
        */
        public List<ThongTinGiaoDich> XacNhanThongTinKhamBenh(ThongTinKhamBenh thongTinKhamBenh)
        {

            string conStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            List<ThongTinGiaoDich> list = new List<ThongTinGiaoDich>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                //SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    SqlParameter[] Params = new SqlParameter[25];
                    Params[0] = new SqlParameter("@MaKhamBenh", thongTinKhamBenh.MaKhamBenh);
                    Params[1] = new SqlParameter("@PhongKhamBenh", thongTinKhamBenh.PhongKhamBenh);
                    Params[2] = new SqlParameter("@NgayKhamBenh", thongTinKhamBenh.NgayKhamBenh);
                    Params[3] = new SqlParameter("@BenhNhan", thongTinKhamBenh.BenhNhan);
                    Params[4] = new SqlParameter("@MaBenhNhan", thongTinKhamBenh.MaBenhNhan);
                    Params[5] = new SqlParameter("@GioiTinh", thongTinKhamBenh.GioiTinh);
                    Params[6] = new SqlParameter("@NamSinh", thongTinKhamBenh.NamSinh);
                    Params[7] = new SqlParameter("@BoPhan", thongTinKhamBenh.BoPhan);
                    Params[8] = new SqlParameter("@CongTy", thongTinKhamBenh.CongTy);
                    Params[9] = new SqlParameter("@KhuVuc", thongTinKhamBenh.KhuVuc);
                    Params[10] = new SqlParameter("@NhomBenh", thongTinKhamBenh.NhomBenh);
                    Params[11] = new SqlParameter("@ChuanDoan", thongTinKhamBenh.ChuanDoan);
                    Params[12] = new SqlParameter("@QuyetDinhNghi", thongTinKhamBenh.QuyetDinhNghi);
                    Params[13] = new SqlParameter("@TuNgay", thongTinKhamBenh.QuyetDinhNghiPhep.TuNgay);
                    Params[14] = new SqlParameter("@DenNgay", thongTinKhamBenh.QuyetDinhNghiPhep.DenNgay);
                    Params[15] = new SqlParameter("@LyDo", thongTinKhamBenh.QuyetDinhNghiPhep.LyDo);
                    Params[16] = new SqlParameter("@DienGiai", thongTinKhamBenh.QuyetDinhNghiPhep.DienGiai);
                    Params[17] = new SqlParameter("@SoNgayNghi", thongTinKhamBenh.QuyetDinhNghiPhep.SoNgayNghi);
                    Params[18] = new SqlParameter("@TongTien", thongTinKhamBenh.TongTien);
                    Params[19] = new SqlParameter("@MaBHYT", thongTinKhamBenh.MaBHYT);
                    Params[20] = new SqlParameter("@MaICD", thongTinKhamBenh.MaICD);
                    Params[21] = new SqlParameter("@DienGiaiICD", thongTinKhamBenh.DienGiaiICD);
                    Params[22] = new SqlParameter("@LyDoChiTiet", thongTinKhamBenh.QuyetDinhNghiPhep.LyDoChiTiet);
                    Params[23] = new SqlParameter("@ChuThich", thongTinKhamBenh.QuyetDinhNghiPhep.ChuThich);
                    Params[24] = new SqlParameter("@TongTienBangChu", thongTinKhamBenh.TongTienBangChu);

                    DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_InsertThongTinKhamBenh, Params);
                    if (thongTinKhamBenh.ThongTinToaThuoc != null && thongTinKhamBenh.ThongTinToaThuoc.Count > 0)
                    {
                        this.BulkInsert(ConvertToDataTable(thongTinKhamBenh.ThongTinToaThuoc), "HUFS_KHAMBENH_DETAIL");

                        // ConvertToDataTable(thongTinKhamBenh.lstWareHouse);
                        this.BulkInsert(ConvertToDataTable(thongTinKhamBenh.lstWareHouse), "HUFS_WAREHOUSE");

                        for (int i = 0; i < thongTinKhamBenh.ThongTinToaThuoc.Count; i++)
                        {
                            List<ThongTinGiaoDich> listi = InsertThongTinGiaoDich(thongTinKhamBenh.ThongTinToaThuoc[i].MaThuoc,
                                                                 thongTinKhamBenh.ThongTinToaThuoc[i].SoLuong,
                                                                 false,
                                                                 true,
                                                                 thongTinKhamBenh.ThongTinToaThuoc[i].MaKhamBenh, thongTinKhamBenh.PhongKhamBenh);
                            if (listi != null && listi.Count > 0)
                            {
                                list.AddRange(listi);
                            }
                        }
                    }

                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    try
                    {
                        SqlParameter[] RollbackParams = new SqlParameter[1];
                        RollbackParams[0] = new SqlParameter("@MaKhamBenh", thongTinKhamBenh.MaKhamBenh);
                        DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_RollbackThongTinKhamBenh, RollbackParams);
                    }
                    catch
                    {

                    }
                    log.Error(ex.Message, ex);
                    return null;
                }
                finally
                {
                    con.Close();
                }
            }
            return list;


        }

        public bool ProcessGiaoDichKhamBenh(List<ThongTinGiaoDich> listTransaction)
        {
            string strListTransaction = "";
            for (int i = 0; i < listTransaction.Count; i++)
            {
                if (i == 0)
                    strListTransaction += listTransaction[i].MaTransaction.ToString();
                else
                    strListTransaction += "," + listTransaction[i].MaTransaction.ToString();
            }
            return ProcessGiaoDichXuatKho(strListTransaction);

        }
        public List<ThongTinBenhNhan> SearchThongTinBenhNhan(string maBenhNhan, string tenBenhNhan)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaBenhNhan", maBenhNhan);
                Params[1] = new SqlParameter("@TenBenhNhan", tenBenhNhan);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_searchThongTinBenhNhan, Params);

                return this.ConvertDataTableToList<ThongTinBenhNhan>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
        public string GetMaxMaKhamBenh()
        {

            try
            {
                string maxMaKhamBenh = "";
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_GetMaxMaKhamBenh);
                foreach (DataRow dr in dtResult.Rows)
                {
                    maxMaKhamBenh = dr["MaxMaKhamBenh"].ToString();
                }
                return maxMaKhamBenh;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return "";
            }
        }

        public string GetMaKhamBenhForPrint(long maTransaction)
        {

            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaTransaction", maTransaction);

                string MaKhamBenh = "";

                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_GetMaKhamBenhForPrint, Params);
                foreach (DataRow dr in dtResult.Rows)
                {
                    MaKhamBenh = dr["MaKhamBenh"].ToString();
                }
                return MaKhamBenh;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return "";
            }
        }

        public string GenerateNewMaKhamKhamBenh()
        {
            string currentMaxMaKhamBenh = GetMaxMaKhamBenh();
            //MKB000000000040
            int lenght = currentMaxMaKhamBenh.Length;
            int prefixLenght = KeyPrefix.MaKhamBenh.Length;

            string currentNumber = !string.IsNullOrEmpty(currentMaxMaKhamBenh) ? currentMaxMaKhamBenh.Remove(0, prefixLenght) : "0";
            long keyNumber = 0;
            try
            {
                keyNumber = long.Parse(currentNumber);
            }
            catch
            {

            }
            keyNumber += 1;

            int newNumberLenght = keyNumber.ToString().Length;
            string paddingKey = "";
            for (int i = 0; i < lenght - (prefixLenght + newNumberLenght); i++)
            {
                paddingKey += "0";
            }

            string newKey = "";
            newKey = KeyPrefix.MaKhamBenh + paddingKey + keyNumber.ToString();
            return newKey;
        }

        public void CapNhatTrangThaiWareHouse(string maGiaoDich, int trangThai, string trangThaiDienGiai)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@MaGiaoDich", maGiaoDich);
                Params[1] = new SqlParameter("@TrangThai", trangThai);
                Params[2] = new SqlParameter("@TrangThaiDienGiai", trangThaiDienGiai);


                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_CapNhatWareHouse, Params);


            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }

        }

        public void XuatKhoThuoc(List<WareHouse> lstWh)
        {

            string conStr = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            List<ThongTinGiaoDich> list = new List<ThongTinGiaoDich>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                //SqlTransaction transaction = con.BeginTransaction();
                try
                {

                    this.BulkInsert(ConvertToDataTable(lstWh), "HUFS_WAREHOUSE");
                    for (int i = 0; i < lstWh.Count; i++)
                    {
                        List<ThongTinGiaoDich> listi = InsertThongTinGiaoDich(lstWh[i].MaThuocHienThi,
                                                                     long.Parse(lstWh[i].SoLuongNgoaiTru),
                                                                     false,
                                                                     true,
                                                                     lstWh[i].MaLienHe
                                                                     , lstWh[i].MaKho);
                        if (listi != null && listi.Count > 0)
                        {
                            list.AddRange(listi);
                        }

                        ProcessGiaoDichKhamBenh(listi);
                    }
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();

                    log.Error(ex.Message, ex);

                }
                finally
                {
                    con.Close();
                }
            }
        }

        public DataTable GetThongTinBenhNhanKhamBenh
(string maKhamBenh)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaKhamBenh", maKhamBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, GetThongTinKhamBenhBenhNhan, Params);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new DataTable();
            }
        }
        public DataTable GetThongTinChiTietThuocKhamBenh(string maKhamBenh)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaKhamBenh", maKhamBenh);

                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, GetThongTinThuocKhamBenh, Params);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new DataTable();
            }
        }

    }
}
