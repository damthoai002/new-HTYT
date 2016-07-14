using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ChotTonKhoHeader
    {

        public string MaChotTonKho { get; set; }
        public string DienGiai { get; set; }
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public DateTime NgayTaoPhieu { get; set; }
        public string StrNgayTaoPhieu { get; set; }
        public string NguoiXacNhan { get; set; }
        public string NguoiDieuChinh { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Creator { get; set; }
        public string LastModifier { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        public int CurrentWorkflow { get; set; }
    }
}
