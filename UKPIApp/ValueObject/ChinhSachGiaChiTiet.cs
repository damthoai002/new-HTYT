using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ChinhSachGiaChiTiet
    {
        public long Id { get; set; }
        public string MaChinhSachGia { get; set; }
        public string TenChinhSachGia { get; set; }
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public decimal GiaDNMua { get; set; }
        public decimal GiaDNMuaVAT { get; set; }
        public decimal GiaThucMua { get; set; }
        public decimal GiaDNBan { get; set; }
        public decimal GiaDNBanVAT { get; set; }
        public decimal GiaThucBan { get; set; }
        public string DienGiai { get; set; }
        public int DonViTinh { get; set; }
        public bool HoatDong { get; set; }
        public bool BaoHiem { get; set; }
        public string MaThuocYTeHienThi { get; set; }

        public bool IsDeleted { get; set; }
    }
}
