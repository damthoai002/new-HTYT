using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ChinhSachGiaDT
    {
          public string MaChinhSachGia {get;set;}
		  public string TenChinhSachGia {get;set;}
          public DateTime ThoiGianBatDau { get; set; }
          public DateTime ThoiGianKetThuc { get; set; }
		  public bool HoatDong {get;set;}
          public DateTime NgayNgungHoatDong { get; set; }
          public DateTime CreatedDate { get; set; }
		  public string CreatedBy {get;set;}
          public DateTime LastUpdatedDate { get; set; }
          public string LastUpdatedBy { get; set; }
    }
}
