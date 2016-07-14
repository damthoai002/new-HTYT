using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UKPI.Utils;


namespace UKPI.ValueObject
{
    public class ClsCreateTimesheet
    {
        public long NhomId { get; set; }
        public string TenNhom { get; set; }
        public string TruongNhomId { get; set; }
        public string TenTruongNhom { get; set; }

        public string NhanVienId { get; set; }
        public string NhanVienTen { get; set; }

        public string L0XacNhanId { get; set; }
        public string L0XacNhanTen { get; set; }
        public string L1XacNhanId { get; set; }
        public string L1XacNhanTen { get; set; }
        public string L2XacNhanId { get; set; }
        public string L2XacNhanTen { get; set; }
        public string L3XacNhanId { get; set; }
        public string L3XacNhanTen { get; set; }
        public string L4XacNhanId { get; set; }
        public string L4XacNhanTen { get; set; }
        public string TuanLamViec { get; set; }
        public string IsOutsource { get; set; }
        public string NgayLamViec { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }

        public string CaThucTeId { get; set; }
        public string CaThucTe { get; set; }



        public ClsCreateTimesheet(DataTable dt)
        {
            this.L0XacNhanId = dt.Rows[0][clsCommon.CreateTimesheet.L0XacNhanMa].ToString();
            this.L0XacNhanTen = dt.Rows[0][clsCommon.CreateTimesheet.L0XacNhan].ToString();
            this.L1XacNhanId = dt.Rows[0][clsCommon.CreateTimesheet.L1XacNhanMa].ToString();
            this.L1XacNhanTen = dt.Rows[0][clsCommon.CreateTimesheet.L1XacNhan].ToString();
            this.L2XacNhanId = dt.Rows[0][clsCommon.CreateTimesheet.L2XacNhanMa].ToString();
            this.L2XacNhanTen = dt.Rows[0][clsCommon.CreateTimesheet.L2XacNhan].ToString();
            this.L3XacNhanId = dt.Rows[0][clsCommon.CreateTimesheet.L3XacNhanMa].ToString();
            this.L3XacNhanTen = dt.Rows[0][clsCommon.CreateTimesheet.L3XacNhan].ToString();
            this.L4XacNhanId = dt.Rows[0][clsCommon.CreateTimesheet.L4XacNhanMa].ToString();
            this.L4XacNhanTen = dt.Rows[0][clsCommon.CreateTimesheet.L4XacNhan].ToString();

            this.TenTruongNhom = dt.Rows[0][clsCommon.CreateTimesheet.TruongNhom].ToString();
            this.TruongNhomId = dt.Rows[0][clsCommon.CreateTimesheet.MaTruongNhom].ToString();
        }

        public ClsCreateTimesheet()
        {
            this.NhomId = 0;
            this.TenNhom = "";
            this.TruongNhomId = "";
            this.TenTruongNhom = "";
            this.NhanVienId = "";
            this.NhanVienTen = "";
            this.L0XacNhanId = "";
            this.L0XacNhanTen = "";
            this.L1XacNhanId = "";
            this.L1XacNhanTen = "";
            this.L2XacNhanId = "";
            this.L2XacNhanTen = "";
            this.L3XacNhanId = "";
            this.L3XacNhanTen = "";
            this.L4XacNhanId = "";
            this.L4XacNhanTen = "";
            this.TuanLamViec = "";
            this.IsOutsource = "";
            this.NgayLamViec = "";
        }
    }
}
