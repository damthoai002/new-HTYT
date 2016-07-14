using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ClsCalamViec
    {

        public long SysId { get; set; }
        public string MaCaLamViec { get; set; }
        public string GioBatDau { get; set; }
        public string GioKetThuc { get; set; }
        public string ThoiGianLamViec { get; set; }
        public string MoTa { get; set; }
        public string CreateDate { get; set; }
        public string CreaterId { get; set; }
        public string LastUpDate { get; set; }
        public string lastUpdateId { get; set; }
        public bool Is_Active { get; set; }


    }
}
