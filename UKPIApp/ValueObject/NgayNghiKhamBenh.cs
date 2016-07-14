using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class NgayNghiKhamBenh
    {

        public int SysId { get; set; }
        public string MaNv { get; set; }
        public string TenNV { get; set; }
        public string GioiTinh { get; set; }
        public string NgayNghiTu { get; set; }
        public string NgayNghiDen { get; set; }
        public string SoNgayNghi { get; set; }
        public string LyDoChiTiet { get; set; }
        public string LyDo { get; set; }
        public string DienGiai { get; set; }
        public string ChuThich { get; set; }
        public string NguoiTao { get; set; }
        public string NgayTao { get; set; }  
        public bool IsActive { get; set; }


    }
}
