using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ChotTonKhoDetail
    {
        public long Id { get; set; }
        public string MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string MaNhapKho { get; set; }
        public bool BaoHiem { get; set; }
        public string DonViTinh { get; set; }
        public string HanDung { get; set; }
        public string NhomThuoc { get; set; }
        public string MaThuocYTeHienThi { get; set; }
        public long SoLuongTon { get; set; }
        public long SoLuongThucTe { get; set; }
        public long SoLuongChenhLech { get; set; }
        public string LoaiChenhLech { get; set; }
        public string MaChotTonHeader { get; set; }
        public long MaNhapKhoDetail { get; set; }
    }
}
