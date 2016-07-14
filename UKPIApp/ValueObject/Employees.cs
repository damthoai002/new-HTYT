using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class Employees
    {
            
            public long SysId { get; set; }
            public string EmployeeID { get; set; }
            public string LName { get; set; }
            public string FName { get; set; }
            public string FullName { get; set; }
            public int GioiTinh { get; set; }
            public string MaBHYT { get; set; }
            public string NgayThangNamSinh { get; set; }
            public string KhuVuc { get; set; }
            public string ViTriLamViec { get; set; }
            public string MI { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public int DepartmentID { get; set; }        
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public DateTime LastUpdatedDate { get; set; }
            public string LastUpdatedBy { get; set; }
            public bool Status { get; set; }
            public string InsuranceID { get; set; }
            public string CongTy { get; set; }

            //public List<T> MyProperty { get; set; }

    }
}
