using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Filter;

namespace UKPI.ValueObject
{
    public class ThongTinBenhNhan
    {
        public string EmployeeID {get;set;}
        public string FullName {get;set;}
        public string GioiTinh {get;set;}
        public string MaBHYT {get;set;}
        public string KhuVuc {get;set;}
        public string NamSinh {get;set;}
        public string CongTy {get;set;}
        public int BoPhan {get;set;}
        public string TenBoPhan { get; set; }
    }
}
