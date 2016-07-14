using System;
using System.Collections.Generic;
using System.Text;

namespace UKPI.ValueObject
{
    public class ClsNhanVienUser
    {
        public int SysId { get; set; }
        public int MaNhanVien { get; set; }
        public string UserName { get; set; }
        public int MaNhom { get; set; }
        public string CreateDate { get; set; }
        public string CreaterId { get; set; }
        public string LastUpDate { get; set; }
        public string lastUpdateId { get; set; }
        public bool Is_Active { get; set; }

    }
}
