using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ThongTinKhamBenh
    {
        public string MaKhamBenh { get; set; }
        public string PhongKhamBenh { get; set; }
        public DateTime NgayKhamBenh { get; set; }
        public string BenhNhan { get; set; }
        public string MaBenhNhan { get; set; }
        public string GioiTinh { get; set; }
        public string NamSinh { get; set; }
        public string BoPhan { get; set; }
        public string CongTy { get; set; }
        public string KhuVuc { get; set; }
        public string NhomBenh { get; set; }
        public string ChuanDoan { get; set; }
        public bool QuyetDinhNghi { get; set; }
        public QuyetDinhNghiPhep QuyetDinhNghiPhep { get; set; }
        public string TongTien { get; set; }
        public string MaBHYT { get; set; }
        public string MaICD { get; set; }
        public string DienGiaiICD { get; set; }
        public List<ThongTinDonThuocKhamBenh> ThongTinToaThuoc { get; set; }
        public List<WareHouse> lstWareHouse { get; set; }

        public string TongTienBangChu { get; set; }


    }
}
