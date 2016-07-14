using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.DataAccessObject
{
    public class QuanLyThuocDao : clsBaseDAO
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(QuanLyThuocDao));
        private const string HUFS_SelectDanhMucThuoc = "HUFS_SelectDanhMucThuoc";
        private const string HUFS_DanhMucThuoc = "HUFS_DanhMucThuoc";
        private const string p_HUFS_GetAllChinhSachGia = "p_HUFS_GetAllChinhSachGiaNew";
        private const string p_HUFS_GetMaxMaChinhSachGia = "p_HUFS_GetMaxMaChinhSachGia";
        private const string p_HUFS_UpdateChinhSachGia = "p_HUFS_UpdateChinhSachGia";
        private const string HUFS_GetChinhSachGiaChiTiet = "HUFS_GetChinhSachGiaChiTiet";
        private const string HUFS_ChinhSachGiaChiTiet = "HUFS_ChinhSachGiaChiTiet";
        private const string p_HUFS_CheckThuocExist = "p_HUFS_CheckThuocExist";
        private const string p_HUFS_ProcessDanhMucThuoc = "p_HUFS_ProcessDanhMucThuoc";
        private const string p_HUFS_ProcessChinhSachGiaChiTiet = "p_HUFS_ProcessChinhSachGiaChiTiet";
        private const string p_HUFS_ProcessMarkDeleteChinhSachGiaChiTiet = "p_HUFS_ProcessMarkDeleteChinhSachGiaChiTiet";
        private const string p_HUFS_CheckOverlapChinhSachGia = "p_HUFS_CheckOverlapChinhSachGia";
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();

        public bool LuuCapNhatThongTinThuoc(ThongTinThuoc thongTinThuoc)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[28];
                Params[0] = new SqlParameter("@MedicineID", thongTinThuoc.MedicineID);
                Params[1] = new SqlParameter("@MedicineName", thongTinThuoc.MedicineName);
                Params[2] = new SqlParameter("@STTTheoDMTCuaBYT", thongTinThuoc.STTTheoDMTCuaBYT);
                Params[3] = new SqlParameter("@TenThanhPhanThuoc", thongTinThuoc.TenThanhPhanThuoc);
                Params[4] = new SqlParameter("@DonViTinh", thongTinThuoc.DonViTinh);
                Params[5] = new SqlParameter("@BaoHiem", thongTinThuoc.BaoHiem);
                Params[6] = new SqlParameter("@GiaDNMua", thongTinThuoc.GiaDNMua);
                Params[7] = new SqlParameter("@GiaDNMuaVAT", thongTinThuoc.GiaDNMuaVAT);
                Params[8] = new SqlParameter("@GiaThucMua", thongTinThuoc.GiaThucMua);
                Params[9] = new SqlParameter("@GiaDNBan", thongTinThuoc.GiaDNBan);
                Params[10] = new SqlParameter("@GiaDNBanVAT", thongTinThuoc.GiaDNBanVAT);
                Params[11] = new SqlParameter("@GiaThucBan", thongTinThuoc.GiaThucBan);
                Params[12] = new SqlParameter("@HamLuong", thongTinThuoc.HamLuong);
                Params[13] = new SqlParameter("@SoDKHoacGPKD", thongTinThuoc.SoDKHoacGPKD);
                Params[14] = new SqlParameter("@DangBaoCheDuongUong", thongTinThuoc.DangBaoCheDuongUong);
                Params[15] = new SqlParameter("@NhaSanXuat", thongTinThuoc.NhaSanXuat);
                Params[16] = new SqlParameter("@QuocGia", thongTinThuoc.QuocGia);
                Params[17] = new SqlParameter("@HoatDong", thongTinThuoc.HoatDong);
                Params[18] = new SqlParameter("@CreatedBy", thongTinThuoc.CreatedBy);
                Params[19] = new SqlParameter("@LastUpdatedBy", thongTinThuoc.LastUpdatedBy);
                Params[20] = new SqlParameter("@MaThuocYTe", thongTinThuoc.MaThuocYTe);
                Params[21] = new SqlParameter("@HeSoAnToan", thongTinThuoc.HeSoAnToan);
                Params[22] = new SqlParameter("@NhomThuoc", thongTinThuoc.NhomThuoc);

                Params[23] = new SqlParameter("@SttMaHoaTheoKQDTSoQDStt", thongTinThuoc.SttMaHoaTheoKQDTSoQDStt);
                Params[24] = new SqlParameter("@TenDonViSYT_BV", thongTinThuoc.TenDonViSYT_BV);
                Params[25] = new SqlParameter("@NgayHieuLuc", thongTinThuoc.NgayHieuLuc);
                Params[26] = new SqlParameter("@PhanNhomTheoTCHTVaTCCN", thongTinThuoc.PhanNhomTheoTCHTVaTCCN);
                Params[27] = new SqlParameter("@CachUong", thongTinThuoc.CachUong);

                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessDanhMucThuoc, Params);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int CheckThuocExist(string maThuocYTe, bool baoHiem)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaThuocYTe", maThuocYTe);
                Params[1] = new SqlParameter("@BaoHiem", baoHiem);
                int soLuong = -1;
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_CheckThuocExist, Params);
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
        public List<ThongTinThuoc> LoadDanhMucThuoc(string maThuocYTe, string tenThuoc)
        {
            //try
            //{
            //    var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, HUFS_SelectDanhMucThuoc);
            //    return dtResult;
            //}
            //catch (Exception ex)
            //{
            //    log.Error(ex.Message, ex);
            //    return null;
            //}

            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@MaThuocYTe", maThuocYTe);
                Params[1] = new SqlParameter("@TenThuoc", tenThuoc);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, HUFS_SelectDanhMucThuoc,Params);
                return this.ConvertDataTableToList<ThongTinThuoc>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public string GetMaxMaChinhSachGia()
        {

            try
            {
                string maxMaChinhSachGia = "";
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_GetMaxMaChinhSachGia);
                foreach (DataRow dr in dtResult.Rows)
                {
                    maxMaChinhSachGia = dr["MaxMaChinhSachGia"].ToString();
                    break;
                }
                return maxMaChinhSachGia;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return "";
            }
        }
        public List<ChinhSachGiaChiTiet> GetChinhSachGiaChiTiet(string maChinhSachGia)
        {

            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@MaChinhSachGia", maChinhSachGia);
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, HUFS_GetChinhSachGiaChiTiet, Params);
                return this.ConvertDataTableToList<ChinhSachGiaChiTiet>(dtResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
        public bool UpdateChinhGiaChiTiet(List<ChinhSachGiaChiTiet> listChinhSachGiaChiTiet)
        {
            try
            {
                for (int i = 0; i < listChinhSachGiaChiTiet.Count; i++)
                {
                    SqlParameter[] Params = new SqlParameter[8];
                    Params[0] = new SqlParameter("@MaChinhSachGia", listChinhSachGiaChiTiet[i].MaChinhSachGia);
                    Params[1] = new SqlParameter("@TenThuoc", listChinhSachGiaChiTiet[i].MedicineName);
                    Params[2] = new SqlParameter("@MaThuoc", listChinhSachGiaChiTiet[i].MedicineID);
                    Params[3] = new SqlParameter("@GiaDNMua", listChinhSachGiaChiTiet[i].GiaDNMua);
                    Params[4] = new SqlParameter("@GiaDNMuaVAT", listChinhSachGiaChiTiet[i].GiaDNMuaVAT);
                    Params[5] = new SqlParameter("@DonViTinh", listChinhSachGiaChiTiet[i].DonViTinh);
                    Params[6] = new SqlParameter("@DienGiai", listChinhSachGiaChiTiet[i].DienGiai);
                    Params[7] = new SqlParameter("@HoatDong", listChinhSachGiaChiTiet[i].HoatDong);
                    DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, HUFS_ChinhSachGiaChiTiet, Params);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool ProcessChinhGiaChiTiet(List<ChinhSachGiaChiTiet> listChinhSachGiaChiTiet)
        {
            try
            {
                for (int i = 0; i < listChinhSachGiaChiTiet.Count; i++)
                {
                    SqlParameter[] Params = new SqlParameter[16];
                    Params[0] = new SqlParameter("@Id", listChinhSachGiaChiTiet[i].Id);
                    Params[1] = new SqlParameter("@MaChinhSachGia", listChinhSachGiaChiTiet[i].MaChinhSachGia);
                    Params[2] = new SqlParameter("@TenChinhSachGia", listChinhSachGiaChiTiet[i].TenChinhSachGia);
                    Params[3] = new SqlParameter("@MedicineID", listChinhSachGiaChiTiet[i].MedicineID);
                    Params[4] = new SqlParameter("@MedicineName", listChinhSachGiaChiTiet[i].MedicineName);
                    Params[5] = new SqlParameter("@GiaDNMua", listChinhSachGiaChiTiet[i].GiaDNMua);
                    Params[6] = new SqlParameter("@GiaDNMuaVAT", listChinhSachGiaChiTiet[i].GiaDNMuaVAT);
                    Params[7] = new SqlParameter("@GiaThucMua", listChinhSachGiaChiTiet[i].GiaThucMua);
                    Params[8] = new SqlParameter("@GiaDNBan", listChinhSachGiaChiTiet[i].GiaDNBan);
                    Params[9] = new SqlParameter("@GiaDNBanVAT", listChinhSachGiaChiTiet[i].GiaDNBanVAT);
                    Params[10] = new SqlParameter("@GiaThucBan", listChinhSachGiaChiTiet[i].GiaThucBan);
                    Params[11] = new SqlParameter("@MaThuocYTeHienThi", listChinhSachGiaChiTiet[i].MaThuocYTeHienThi);
                    Params[12] = new SqlParameter("@BaoHiem", listChinhSachGiaChiTiet[i].BaoHiem);
                    Params[13] = new SqlParameter("@DonViTinh", listChinhSachGiaChiTiet[i].DonViTinh);
                    Params[14] = new SqlParameter("@DienGiai", listChinhSachGiaChiTiet[i].DienGiai);
                    Params[15] = new SqlParameter("@HoatDong", listChinhSachGiaChiTiet[i].HoatDong);
                    DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessChinhSachGiaChiTiet, Params);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool ProcessMarkDeleteChinhGiaChiTiet(List<ChinhSachGiaChiTiet> oldListChinhSachGiaChiTiet,List<ChinhSachGiaChiTiet> listChinhSachGiaChiTiet)
        {
            try
            {
                List<ChinhSachGiaChiTiet> deleteList = new List<ChinhSachGiaChiTiet>();
                for (int i = 0; i < oldListChinhSachGiaChiTiet.Count; i++)
                {
                    if (listChinhSachGiaChiTiet.Where(x => x.Id == oldListChinhSachGiaChiTiet[i].Id).FirstOrDefault() == null)
                        deleteList.Add(oldListChinhSachGiaChiTiet[i]);
                }
                if (deleteList.Count > 0)
                {
                    string strId = "";
                    for (int i = 0; i < deleteList.Count; i++)
                    {
                        if (i == 0 )
                            strId += deleteList[i].Id.ToString();
                        else
                            strId += "," + deleteList[i].Id.ToString();
                    }
                    SqlParameter[] Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@ListId", strId);
                    DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_ProcessMarkDeleteChinhSachGiaChiTiet, Params);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }
        public bool SaveChinhSachGia(ChinhSachGiaDT chinhSachGia)
        {
            try
            {
                List<ChinhSachGiaDT> listChinhSachGiaDT = new List<ChinhSachGiaDT>();
                listChinhSachGiaDT.Add(chinhSachGia);
                this.BulkInsert(ConvertToDataTable(listChinhSachGiaDT), "HUFS_CHINHSACH_HEADER");
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }
        public bool UpdateChinhSachGia(ChinhSachGiaDT chinhSachGia)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[8];
                Params[0] = new SqlParameter("@MaChinhSachGia", chinhSachGia.MaChinhSachGia);
                Params[1] = new SqlParameter("@TenChinhSachGia", chinhSachGia.TenChinhSachGia);
                Params[2] = new SqlParameter("@ThoiGianBatDau", chinhSachGia.ThoiGianBatDau);
                Params[3] = new SqlParameter("@ThoiGianKetThuc", chinhSachGia.ThoiGianKetThuc);
                Params[4] = new SqlParameter("@HoatDong", chinhSachGia.HoatDong);
                Params[5] = new SqlParameter("@NgayNgungHoatDong", chinhSachGia.NgayNgungHoatDong);
                Params[6] = new SqlParameter("@LastUpdatedDate", chinhSachGia.LastUpdatedDate);
                Params[7] = new SqlParameter("@LastUpdatedBy", chinhSachGia.LastUpdatedBy);
                DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, p_HUFS_UpdateChinhSachGia, Params);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }
        public string GenerateNewMaChinhSachGia()
        {
            string currentMaxMaChinhSachGia = GetMaxMaChinhSachGia();
            //MKB000000000040
            int lenght = currentMaxMaChinhSachGia.Length;
            int prefixLenght = KeyPrefix.MaChinhSachGia.Length;

            string currentNumber = !string.IsNullOrEmpty(currentMaxMaChinhSachGia) ? currentMaxMaChinhSachGia.Remove(0, prefixLenght):"0";
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
            newKey = KeyPrefix.MaChinhSachGia + paddingKey + keyNumber.ToString();
            return newKey;
        }

        public DataTable LoadChinhSachGia()
        {
            try
            {
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_GetAllChinhSachGia);
                return dtResult;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
        public int CheckOverlapChinhSachGia(string maChinhSachGia,DateTime thoiGianNatDau,DateTime ngayNgungHoatDong)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@MaChinhSachGia", maChinhSachGia);
                Params[1] = new SqlParameter("@ThoiGianBatDau", thoiGianNatDau);
                Params[2] = new SqlParameter("@NgayNgungHoatDong", ngayNgungHoatDong);
                int overLap = -1;
                var dtResult = DataServices.ExecuteDataTable(CommandType.StoredProcedure, p_HUFS_CheckOverlapChinhSachGia, Params);
                foreach (DataRow dr in dtResult.Rows)
                {
                    overLap = int.Parse(dr["Result"].ToString());
                    break;
                }
                return overLap;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return -1;
            }
        }
        public bool LuuThongTinDanhMucThuoc(DataTable tblDanhMucThuoc)
        {
            List<DanhMucThuoc> listDanhMucThuoc = ConvertDataTableToList<DanhMucThuoc>(tblDanhMucThuoc);
            Dictionary<CustomKey, string> dic = _shareEntityDao.BuildTuDienThuoc();
            try
            {
                for (int i = 0; i < listDanhMucThuoc.Count; i++)
                {
                    DanhMucThuoc danhMucThuoc = listDanhMucThuoc[i];
                    SqlParameter[] Params = new SqlParameter[11];
                    Params[0] = new SqlParameter("@TenThuoc", danhMucThuoc.MedicineName);
                    Params[1] = new SqlParameter("@MaThuoc", danhMucThuoc.MedicineID);
                    Params[2] = new SqlParameter("@STTTheoDMTCuaBYT", danhMucThuoc.STTTheoDMTCuaBYT);
                    Params[3] = new SqlParameter("@TenThanhPhanThuoc", danhMucThuoc.TenThanhPhanThuoc);
                    Params[4] = new SqlParameter("@HamLuong", danhMucThuoc.HamLuong);
                    Params[5] = new SqlParameter("@SoDKHoacGPKD", danhMucThuoc.SoDKHoacGPKD);
                    Params[6] = new SqlParameter("@DangBaoCheDuongUong", danhMucThuoc.DangBaoCheDuongUong);
                    Params[7] = new SqlParameter("@NhaSanXuat", danhMucThuoc.NhaSanXuat);
                    Params[8] = new SqlParameter("@QuocGia", danhMucThuoc.QuocGia);
                    Params[9] = new SqlParameter("@DonViTinh", danhMucThuoc.DonViTinh);
                    Params[10] = new SqlParameter("@HoatDong", danhMucThuoc.HoatDong);
                    DataServices.ExecuteStoredProcedure(CommandType.StoredProcedure, HUFS_DanhMucThuoc, Params);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
            return true;
        }
    }
}
