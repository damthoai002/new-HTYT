using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ThongTinNhapKhoDetail
    {
        public string Chon {get;set;}
        public string TenThuoc {get;set;}
        public string MaThuoc {get;set;}
        public bool ThuocBH {get;set;}
        public long SoLuong {get;set;}
        public decimal GiaThoiDiemNhap {get;set;}
        public decimal GiaTT { get; set; }
        public decimal GiaST { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaNhapKho {get;set;}
        public string LoThuoc {get;set;}
        public DateTime HanSuDung { get; set; }
        public string HamLuong { get; set; }
    }
}
