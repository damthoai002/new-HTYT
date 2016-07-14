using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class QuyetDinhNghiPhep
    {
        public string MaBenhNhan { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? SoNgayNghi { get; set; }
        public string LyDo { get; set; }
        public string LyDoChiTiet { get; set; }
        public string DienGiai { get; set; }
        public string ChuThich { get; set; }
    }
}
