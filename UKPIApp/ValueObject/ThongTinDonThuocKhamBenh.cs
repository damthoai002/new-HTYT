using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ThongTinDonThuocKhamBenh
    {
        public string Chon { get; set; }
        public string TenThuoc { get; set; }
        public string MaThuoc { get; set; }
        public string DonViTinh { get; set; }
        public string HamLuong { get; set; }
        public bool ThuocBH { get; set; }
        public long SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal GiaTTBHYT { get; set; }
        public string CachUong { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal ThanhTienTTBHYT { get; set; }
        public string MaKhamBenh { get; set; }
        public string CachUongChiTiet { get; set; }

        public string MaChinhSachGia { get; set; }
    }
}
