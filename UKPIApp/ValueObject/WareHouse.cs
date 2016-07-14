using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class WareHouse
    {
       
        public string MaDienGiaiWarehouse { get; set; }
        public string MaThuocHeThong { get; set; }
        public string MaThuocHienThi { get; set; }
        public DateTime HanSuDung { get; set; }
        public string MaKho { get; set; }
        public string MaLienHe { get; set; }
        public int NhapXuat { get; set; }
        public string SttMaHoaTheoKQDTSoQDStt { get; set; }
        public string TenDonViSYT_BV { get; set; }
        public string NgayHieuLuc { get; set; }
        public string PhanNhomTheoTCHTVaTCCN { get; set; }
        public string STTTheoDMTCuaBYT { get; set; }
        public string TenThanhPhanThuoc { get; set; }
        public string MedicineName { get; set; }
        public int MaCachUong { get; set; }
        public string CachUong { get; set; }
        public string DangBaoCheDuongUong { get; set; }
        public string HamLuong { get; set; }
        public string DangTrinhBay { get; set; }
        public string NhaSanXuat { get; set; }
        public string QuocGia { get; set; }
        public string SoDKHoacGPKD { get; set; }
        public string DonViTinh { get; set; }
        public bool ThuocBaoHiem { get; set; }
        public decimal GiaMuaVao { get; set; }
        public decimal GiaThanhToanBHYT { get; set; }
        public string SoLuongNgoaiTru { get; set; }
        public string SoLuongNoiTru { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }
        public string NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public string LoaiThanhToan { get; set; }
        public string LoaiThanhToan_Sub { get; set; }
        public bool IsActive { get; set; }
        public string PhongKham { get; set; }
        public int TrangThai { get; set; }
        public string TrangThaiDienGiai { get; set; }
        public string NhomThuoc { get; set; }
    }
}
