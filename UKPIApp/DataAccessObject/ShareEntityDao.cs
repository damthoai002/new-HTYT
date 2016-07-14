using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UKPI.Utils;
using UKPI.ValueObject;


namespace UKPI.DataAccessObject
{
    public class ShareEntityDao : clsBaseDAO
    {

        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(ShareEntityDao));
        private static string m_strConn = clsCommon.GetConnectionString();
        private const string p_HUFS_LoadThongTinThuoc = "p_HUFS_LoadThongTinThuoc";
        private const string p_HUFS_LoadThongTinThuocForNhapKho = "p_HUFS_LoadThongTinThuocForNhapKho";
        private const string p_HUFS_LoadAllThongTinThuoc = "p_HUFS_LoadAllThongTinThuoc";
        private const string p_HUFS_LoadThongTinThuocTheoMaThuocYTe = "p_HUFS_LoadThongTinThuocTheoMaThuocYTe";
        private const string p_HUFS_LoadThongTinThuocForDictionary = "p_HUFS_LoadThongTinThuocForDictionary";
        private const string p_HUFS_LoadThongTinThuocForKhamBenh = "p_HUFS_LoadThongTinThuocForKhamBenh";
        private const string p_LoadCachDung = "p_LoadCachDung";

        public List<PhongKham> LoadDanhSachPhongKham()
        {
            List<PhongKham> arrs = new List<PhongKham>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  RoomID,RoomName FROM HUFS_ROOM_CLINIC", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    PhongKham pk = new PhongKham();
                    pk.RoomID = (string)reader[0];
                    pk.RoomName = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<GioiTinh> LoadGioiTinh()
        {
            List<GioiTinh> arrs = new List<GioiTinh>();
            arrs.Add(new GioiTinh { Name = "Nam" });
            arrs.Add(new GioiTinh { Name = "Nữ" });
            arrs.Add(new GioiTinh { Name = "Khác" });
            return arrs;
        }

        public List<GioiTinh> LoadGioiTinhNhanVien()
        {
            List<GioiTinh> arrs = new List<GioiTinh>();
            arrs.Add(new GioiTinh { Id = 1, Name = "Nam" });
            arrs.Add(new GioiTinh { Id = 2, Name = "Nữ" });
            arrs.Add(new GioiTinh { Id = 3, Name = "Khác" });
            return arrs;
        }


        public List<BoPhan> LoadDanhSachBoPhan()
        {
            List<BoPhan> arrs = new List<BoPhan>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(" SELECT 0 AS MaBoPhan , N'Khác' AS TenBoPhan UNION  SELECT  MaBoPhan,TenBoPhan FROM HUFS_BOPHAN", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    BoPhan pk = new BoPhan();
                    pk.MaBoPhan = (int)reader[0];
                    pk.TenBoPhan = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<BoPhan> LoadDanhSachBoPhanForReport()
        {
            List<BoPhan> arrs = new List<BoPhan>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(" SELECT  -1 AS MaBoPhan , N'Tất cả' AS TenBoPhan UNION   SELECT  0 AS MaBoPhan , N'Khác' AS TenBoPhan UNION  SELECT  MaBoPhan,TenBoPhan FROM HUFS_BOPHAN", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    BoPhan pk = new BoPhan();
                    pk.MaBoPhan = (int)reader[0];
                    pk.TenBoPhan = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<KhuVuc> LoadDanhSachKhuVuc()
        {
            List<KhuVuc> arrs = new List<KhuVuc>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT 0 AS MaKhuVuc, N'Khác' AS TenKhuVuc UNION SELECT  MaKhuVuc,TenKhuVuc FROM HUFS_KHUVUC", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    KhuVuc pk = new KhuVuc();
                    pk.MaKhuVuc = (int)reader[0];
                    pk.TenKhuVuc = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<KhuVuc> LoadDanhSachKhuVucForReport()
        {
            List<KhuVuc> arrs = new List<KhuVuc>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT -1 AS MaKhuVuc, N'Tất cả' AS TenKhuVuc UNION SELECT  0 AS MaKhuVuc, N'Khác' AS TenKhuVuc UNION SELECT  MaKhuVuc,TenKhuVuc FROM HUFS_KHUVUC", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    KhuVuc pk = new KhuVuc();
                    pk.MaKhuVuc = (int)reader[0];
                    pk.TenKhuVuc = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<NhomBenh> LoadDanhSachNhomBenh()
        {
            List<NhomBenh> arrs = new List<NhomBenh>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT 0 AS MaNhomBenh , N'Bệnh khác' AS TenNhomBenh , N'Bệnh khác' AS ChuanDoan Union Select   MaNhomBenh,TenNhomBenh, ChuanDoan FROM HUFS_NHOMBENH", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    NhomBenh pk = new NhomBenh();
                    pk.MaNhomBenh = (int)reader[0];
                    pk.TenNhomBenh = (string)reader[1];
                    pk.ChuanDoan = (string)reader[2];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<NhomBenh> LoadDanhSachNhomBenhForReport()
        {
            List<NhomBenh> arrs = new List<NhomBenh>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT -1 AS MaNhomBenh , N'Tất cả' AS TenNhomBenh , N'Tất cả' AS ChuanDoan Union  SELECT 0 AS MaNhomBenh , N'Bệnh khác' AS TenNhomBenh , N'Bệnh khác' AS ChuanDoan Union Select   MaNhomBenh,TenNhomBenh, ChuanDoan FROM HUFS_NHOMBENH", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    NhomBenh pk = new NhomBenh();
                    pk.MaNhomBenh = (int)reader[0];
                    pk.TenNhomBenh = (string)reader[1];
                    pk.ChuanDoan = (string)reader[2];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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


        public List<MaICD> LoadDanhSachMaICD()
        {
            List<MaICD> arrs = new List<MaICD>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  MaICD, DienGiai + ', ( ' + MaICD + ' )'  AS DienGiai  FROM HUFS_ICD", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MaICD pk = new MaICD();
                    pk.Ma = (string)reader[0];
                    pk.DienGiai = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<MaICD> LoadDanhSachMaICDForReport()
        {
            List<MaICD> arrs = new List<MaICD>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT 0 AS  MaICD, N'Tất cả'  AS DienGiai UNION SELECT  MaICD, DienGiai + ', ( ' + MaICD + ' )'  AS DienGiai  FROM HUFS_ICD", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    MaICD pk = new MaICD();
                    pk.Ma = (string)reader[0];
                    pk.DienGiai = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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
        /*
        public List<ThongTinThuoc> LoadThongTinThuoc()
        {
            List<ThongTinThuoc> arrs = new List<ThongTinThuoc>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT MedicineID,MedicineName,Description,Status,KhuyenMai,"+
                                    " GiaDNMua ,GiaDNMuaVAT,GiaThucMua,GiaKMMua,GiaDNBan,GiaThucBan, "+
                                    " MaKM,BaoHiem,MaChinhSachGia,DienGiai,STTTheoDMTCuaBYT,TenThanhPhanThuoc,HamLuong "+
                                    ",SoDKHoacGPKD,DangBaoCheDuongUong,NhaSanXuat,QuocGia,DonViTinh,HoatDong,CachUong,Flag,GiaDNBanVAT,TenDonViTinh,CachUongThuoc,MaThuocYTe,(CASE WHEN BaoHiem = 1 AND [MaThuocYTe] IN (SELECT a.MaThuocYTe FROM HUFS_MEDICINE a WHERE a.BaoHiem = 0 AND a.MaThuocYTe = MaThuocYTe) THEN [MaThuocYTe]+'_bh' " +
                                  " ELSE [MaThuocYTe] " +
                                  " END) AS [MaThuocYTeHienThi] " +
                                    " FROM HUFS_MEDICINE "+
                                    " INNER JOIN HUFS_DONVITINH ON HUFS_MEDICINE.DonViTinh = HUFS_DONVITINH.MaDonViTinh " +
                                    " INNER JOIN HUFS_CACHUONGTHUOC ON HUFS_MEDICINE.CachUong = HUFS_CACHUONGTHUOC.MaUongThuoc "+
                                    " ORDER BY MedicineID "
                                    , con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    ThongTinThuoc pk = new ThongTinThuoc();
                    pk.MedicineID = (string)reader[0];
                    pk.MedicineName = (string)reader[1];
                    pk.Description = (string)reader[2];
                    pk.Status = (bool)reader[3];
                    pk.KhuyenMai = (bool)reader[4];
                    pk.GiaDNMua = (double)reader[5];
                    pk.GiaDNMuaVAT = (double)reader[6];
                    pk.GiaThucMua = (double)reader[7];
                    pk.GiaKMMua = (double)reader[8];
                    pk.GiaDNBanVAT = (double)reader[9];
                    pk.GiaThucBan = (double)reader[10];
                 //   pk.MaKM = (string)reader[11];
                    pk.BaoHiem = (bool)reader[12];
                    pk.MaChinhSachGia = (string)reader[13];
                    pk.DienGiai = (string)reader[14];
                    pk.STTTheoDMTCuaBYT = (string)reader[15];
                    pk.TenThanhPhanThuoc = (string)reader[16];
                    pk.HamLuong = (string)reader[17];
                    pk.SoDKHoacGPKD = (string)reader[18];
                    pk.DangBaoCheDuongUong = (string)reader[19];
                    pk.NhaSanXuat = (string)reader[20];
                    pk.QuocGia = (string)reader[21];
                    pk.DonViTinh = (int)reader[22];
                    pk.HoatDong = (bool)reader[23];
                    pk.CachUong = (int)reader[24];
                    pk.Flag = (int)reader[25];
                    pk.GiaDNBan = (double)reader[26];
                    pk.TenDonViTinh = (string)reader[27];
                    pk.CachUongThuoc = (string)reader[28];
                    pk.MaThuocYTe = (string)reader[29];
                    pk.MaThuocYTeHienThi = (string)reader[30];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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
        */
        public List<ThongTinThuoc> LoadThongTinThuoc()
        {
            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadThongTinThuoc);
                return this.ConvertDataTableToList<ThongTinThuoc>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public List<ThongTinThuoc> LoadThongTinThuocForNhapKho()
        {
            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadThongTinThuoc);
                return this.ConvertDataTableToList<ThongTinThuoc>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }


        public List<ThongTinThuocKhamBenh> LoadThongTinThuocForKhamBenh(string maKho)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaKho", maKho);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadThongTinThuocForKhamBenh,Params);
                return this.ConvertDataTableToList<ThongTinThuocKhamBenh>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public List<ThongTinThuocTomLuoc> LoadAllThongTinThuoc()
        {
            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadAllThongTinThuoc);
                return this.ConvertDataTableToList<ThongTinThuocTomLuoc>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
        public List<ThongTinThuoc> LoadThongTinThuocTheoMaThuocYTe(string maThuocYTe, bool baoHiem)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@maThuocYTe", maThuocYTe);
                Params[1] = new SqlParameter("@baoHiem", baoHiem);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadThongTinThuocTheoMaThuocYTe, Params);
                List<ThongTinThuoc> list = this.ConvertDataTableToList<ThongTinThuoc>(dtResult);
                return list;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public List<ThongTinThuocDictionary> LoadThongTinThuocForDictionary()
        {
            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_LoadThongTinThuocForDictionary);
                return this.ConvertDataTableToList<ThongTinThuocDictionary>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public Dictionary<CustomKey, string> BuildTuDienThuoc()
        {
            List<ThongTinThuocDictionary> list = LoadThongTinThuocForDictionary();
            Dictionary<CustomKey, string> dic = new Dictionary<CustomKey, string>(new CustomKey.EqualityComparer());
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    CustomKey key = new CustomKey(list[i].MaThuocYTeHienThi, list[i].BaoHiem);
                    string value = list[i].MedicineID;
                    if (!dic.ContainsKey(key))
                        dic.Add(key, value);
                    else
                        continue;
                }
            }
            return dic;
        }

        public string LoadMaThuocThucTheoMaThuocYTe(string maThuocYTe, bool baoHiem)
        {
            List<ThongTinThuoc> list = LoadThongTinThuocTheoMaThuocYTe(maThuocYTe, baoHiem);
            if (list != null && list.Count == 1)
            {
                return list[0].MedicineID;
            }
            else
                return null;
        }

        public List<CachUongThuoc> LoadThongTinCachUongThuoc()
        {
            List<CachUongThuoc> arrs = new List<CachUongThuoc>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT MaUongThuoc,CachUongThuoc FROM HUFS_CACHUONGTHUOC", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    CachUongThuoc pk = new CachUongThuoc();
                    pk.MaUongThuoc = (int)reader[0];
                    pk.CachUong = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<LyDo> LoadLyDo()
        {
            List<LyDo> arrs = new List<LyDo>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  ReasonID,ReasonName FROM HUFS_REASON", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    LyDo pk = new LyDo();
                    pk.ReasonID = (int)reader[0];
                    pk.ReasonName = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<DonViTinh> LoadDonViTinh()
        {
            List<DonViTinh> arrs = new List<DonViTinh>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  MaDonViTinh, TenDonViTinh FROM HUFS_DONVITINH", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    DonViTinh pk = new DonViTinh();
                    pk.MaDonViTinh = (int)reader[0];
                    pk.TenDonViTinh = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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
        public List<LyDoChiTiet> LoadLyDoChiTiet(int reasonId)
        {
            List<LyDoChiTiet> arrs = new List<LyDoChiTiet>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  ReasonDetailID,ReasonID,ReasonDetailName FROM HUFS_REASON_DETAIL WHERE ReasonID = @ReasonID", con);
                cmd.Parameters.Add("@ReasonID", SqlDbType.Int).Value = reasonId;
                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    LyDoChiTiet pk = new LyDoChiTiet();
                    pk.ReasonDetailID = (int)reader[0];
                    pk.ReasonID = (int)reader[1];
                    pk.ReasonDetailName = (string)reader[2];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<CachDungChiTiet> LoadCachDungChiTiet()
        {
            List<CachDungChiTiet> arrs = new List<CachDungChiTiet>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT ID, CachDungChiTiet FROM HUFS_CACHDUNGCHITIET WHERE  Active = 1 ", con);
                //cmd.Parameters.Add("@ReasonID", SqlDbType.Int).Value = reasonId;
                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    CachDungChiTiet pk = new CachDungChiTiet();
                    pk.ID    = (int)reader[0];
                    pk.CachDung = reader[1].ToString();
 
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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


        public DataTable LoadCachDung()
        {
            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_LoadCachDung);

                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new DataTable();
            }
        }


        public List<NhaSanXuat> LoadNhaSanXuat()
        {
            List<NhaSanXuat> arrs = new List<NhaSanXuat>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  MaNhaSanXuat,TenNhaSanXuat FROM HUFS_NHASANXUAT", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    NhaSanXuat pk = new NhaSanXuat();
                    pk.MaNhaSanXuat = (int)reader[0];
                    pk.TenNhaSanXuat = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<QuocGia> LoadQuocGia()
        {
            List<QuocGia> arrs = new List<QuocGia>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT  MaQuocGia,TenQuocGia FROM HUFS_QUOCGIA", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    QuocGia pk = new QuocGia();
                    pk.MaQuocGia = (int)reader[0];
                    pk.TenQuocGia = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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

        public List<NhomThuoc> LoadThongTinNhomThuoc()
        {
            List<NhomThuoc> arrs = new List<NhomThuoc>();
            SqlConnection con = Connection;
            SqlDataReader reader = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT MaNhomThuoc,TenNhomThuoc FROM HUFS_NHOMTHUOC", con);

                if (con.State != ConnectionState.Open)
                    con.Open();

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    NhomThuoc pk = new NhomThuoc();
                    pk.MaNhomThuoc = (long)reader[0];
                    pk.TenNhomThuoc = (string)reader[1];
                    arrs.Add(pk);
                }

                reader.Close();
                return arrs;
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
    }

}
