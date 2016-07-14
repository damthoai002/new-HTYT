using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ClsNgayNghiChiTiet
    {
        public long SysId { get; set; }
        public int MaNgayNghi { get; set; }
        public int Nam { get; set; }
        public string Tuan { get; set; }
        public string Ngay { get; set; }
        public string MoTa { get; set; }
        public string IsActive { get; set; }
        public string CreateDate { get; set; }
        public string CreaterId { get; set; }
        public string LastUpDate { get; set; }
        public string LastUpdateId { get; set; }


    }
}
