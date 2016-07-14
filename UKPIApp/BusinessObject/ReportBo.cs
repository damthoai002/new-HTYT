using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class ReportBo
    {
        private ReportDao _reportDao = new ReportDao();
        private clsCommon _common = new clsCommon();


        public DataTable GetToaThuoc(string maKhamBenh)
        {
            return _reportDao.GetToaThuoc(maKhamBenh);
        }

        public DataTable GetToaThuocBh(string maKhamBenh)
        {
            return _reportDao.GetToaThuocBh(maKhamBenh);
        }

        public DataTable baoCaoThuocTanDuocTTBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoThuocTanDuocTTBHYT(kho, quy, nam, tuNgay, denNgay);
        }
        public DataTable baoCaoThuocYHCTTTBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoThuocYHCTTTBHYT(kho, quy, nam, tuNgay, denNgay);
        }

        public DataTable baoCaoBenhNhanNgoaiTruTTBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoBenhNhanNgoaiTruTTBHYT(kho, quy, nam, tuNgay, denNgay);
        }


        public DataTable baoCaoXuatNhapTon(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoXuatNhapTon(kho, quy, nam, tuNgay, denNgay);
        }


        public DataTable baoCaoLichSuBenhNhan(string maBenhNhan, string tenBenhNhan, string maBHYT, string tuNgay, string denNgay, string khuVuc, string boPhan, string nhomBenh, string maBenh, string tenBenh)
        {
            return _reportDao.baoCaoLichSuBenhNhan(maBenhNhan, tenBenhNhan, maBHYT, tuNgay, denNgay, khuVuc, boPhan, nhomBenh, maBenh, tenBenh);
        }
  public DataTable ListBenhNhan(string maBenhNhan, string tenBenhNhan, string maBHYT, string tuNgay, string denNgay, string khuVuc, string boPhan, string nhomBenh, string maBenh, string tenBenh)
        {
            return _reportDao.ListBenhNhan(maBenhNhan, tenBenhNhan, maBHYT, tuNgay, denNgay, khuVuc, boPhan, nhomBenh, maBenh, tenBenh);
        }


        public DataTable baoCaoLichSuKhamBenhVaPhatThuoc(string maBenhNhan, string tenBenhNhan, string maBHYT, string tuNgay, string denNgay, string khuVuc, string boPhan, string nhomBenh, string maBenh, string tenBenh)
        {
            return _reportDao.baoCaoLichSuKhamBenhVaPhatThuoc(maBenhNhan, tenBenhNhan, maBHYT, tuNgay, denNgay, khuVuc, boPhan, nhomBenh, maBenh, tenBenh);
        }

        public DataTable baoCaoKhamBenhBHYT_Thang(string kho, string nam, string thang)
        {
            return _reportDao.baoCaoKhamBenhBHYT_Thang(kho, thang, nam);
        }
        public DataTable baoCaoBenhNhanNamNu(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoBenhNhanNamNu(kho, quy, nam, tuNgay, denNgay);
        }

        public DataTable baoCaoTongTienBHYT(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoTongTienBHYT(kho, quy, nam, tuNgay, denNgay);
        }
        public DataTable baoCaoGhiChuKhac(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoGhiChuKhac(kho, quy, nam, tuNgay, denNgay);
        }
        public DataTable baoCaoTheoDoiNghiOm(string kho, string quy, string nam, string tuNgay, string denNgay)
        {
            return _reportDao.baoCaoTheoDoiNghiOm(kho, quy, nam, tuNgay, denNgay);
        }
    }
}
