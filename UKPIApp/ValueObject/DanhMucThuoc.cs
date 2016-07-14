using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class DanhMucThuoc
    {
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string STTTheoDMTCuaBYT { get; set; }
        public string TenThanhPhanThuoc { get; set; }
        public string HamLuong { get; set; }
        public string SoDKHoacGPKD { get; set; }
        public string DangBaoCheDuongUong { get; set; }
        public string NhaSanXuat { get; set; }
        public string QuocGia { get; set; }
        public int DonViTinh { get; set; }
        public bool HoatDong { get; set; }
    }
}
