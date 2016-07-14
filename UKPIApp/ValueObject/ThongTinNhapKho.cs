using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ThongTinNhapKho
    {
      public string MaNhapKho {get;set;}
      public string TenNhanVien {get;set;}
      public string MaNhanVien {get;set;}
      public string PhongKhamKho {get;set;}
        public string MaKho { get; set; }
      public string MaHDD {get;set;}
      public DateTime NgayNhapKho {get;set;}
      public string DonViCungCap {get;set;}
      public string MaSoDVCungCap {get;set;}
      public string TongTienHD { get; set; }
    }
}
