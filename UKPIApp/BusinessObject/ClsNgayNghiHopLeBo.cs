using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net.Util;
using UKPI.DataAccessObject;
using UKPI.Presentation;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class ClsNgayNghiHopLeBo
    {
        private static log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ClsNgayNghiHopLeBo));
        private ClsNgayNghiHopLeDao _ngayNghiHopLeDao = new ClsNgayNghiHopLeDao();
        private clsCommon _common = new clsCommon();
        /// <summary>
        /// Kiem tra ton tai lich lam viec
        ///  </summary>
        /// <returns></returns>
        public DataTable GetNhanVienNghiPhep(string truongNhomId)
        {
            var table = _ngayNghiHopLeDao.GetNhanVienNghiPhep(truongNhomId);
            return table;
        }
        public DataTable GetNhanVienNghiThaiSan(string truongNhomId)
        {
           return _ngayNghiHopLeDao.GetNhanVienNghiThaiSan(truongNhomId);

        }
        public void TaoNgayNghiPhep(
                    string truongNhom,
                    string maGiaoDich,
                    DateTime tuNgay,
                    DateTime denNgay,
                    string lyDo1,
                    string lyDo1ChiTiet,
                    string lyDo2,
                    string lyDo2ChiTiet,
                    string dienGiai,
                    int maNvNghi,
                    string tenNvNghi,
                    string tenNvDuyet,
                    string ngayDuyet,
                    bool status
      )
        {
            _ngayNghiHopLeDao.TaoNgayNghiPhep(
                 truongNhom,
                     maGiaoDich,
                     tuNgay,
                     denNgay,
                     lyDo1,
                     lyDo1ChiTiet,
                     lyDo2,
                     lyDo2ChiTiet,
                     dienGiai,
                     maNvNghi,
                     tenNvNghi,
                     tenNvDuyet,
                     ngayDuyet,
                     status
                    );

        }

        public bool CheckApprovedForNgayNghi(string maNv, string maTruongNhom, string maGiaoDich)
        {
            DataTable tb = _ngayNghiHopLeDao.CheckApprovedForNgayNghi(maNv, maTruongNhom, maGiaoDich);
            if (tb.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }


        public void UpdateIsOffForNgayNghiNhanVien(string maNv, string maTruongNhom, string maGiaoDich)
        {
            _ngayNghiHopLeDao.UpdateIsOffForNgayNghiNhanVien(maNv, maTruongNhom, maGiaoDich);
        }

        public DataTable GetEmail(string maNhanVien, string maTruongNhom)
        {
            return _ngayNghiHopLeDao.GetEmailNhanVien(maNhanVien, maTruongNhom);

        }

        public bool CheckExistDateOff(string fromDate, string toDate, string maTruongNhom, string maNV)
        {
            DataTable tb = _ngayNghiHopLeDao.CheckExistDateOff(fromDate, toDate, maNV, maTruongNhom);
            return tb.Rows.Count > 0;
        }

        public DataTable GetNgaynghiPhep(string maTruongNhom, string tenNhanVien)
        {
            return _ngayNghiHopLeDao.GetNgaynghiPhep(maTruongNhom, tenNhanVien);
        }

        public DataTable GetNgaynghiTaiNan(string maTruongNhom, string tenNhanVien)
        {
            return _ngayNghiHopLeDao.GetNgaynghiTaiNan(maTruongNhom, tenNhanVien);
        }
        public void TaoNgayNghiTaiNan(
             string truongNhom,
             string maGiaoDich,
             DateTime tuNgay,
             DateTime denNgay,
             string lyDo1,
             string lyDo1ChiTiet,
             string lyDo2,
             string lyDo2ChiTiet,
             string dienGiai,
             int maNvNghi,
             string tenNvNghi,
             string tenNvDuyet,
             string ngayDuyet,
             bool status
     )
        {
            _ngayNghiHopLeDao.TaoNgayNghiTaiNan(
                 truongNhom,
                     maGiaoDich,
                     tuNgay,
                     denNgay,
                     lyDo1,
                     lyDo1ChiTiet,
                     lyDo2,
                     lyDo2ChiTiet,
                     dienGiai,
                     maNvNghi,
                     tenNvNghi,
                     tenNvDuyet,
                     ngayDuyet,
                     status
                    );

        }
 public DataTable GetNgaynghiThaiSan(string maTruongNhom, string tenNhanVien, string ngayNghi)
        {
            return _ngayNghiHopLeDao.GetNgaynghiThaiSan(maTruongNhom, tenNhanVien, ngayNghi);
        }
        public void TaoNgayNghiThaiSan(
             string truongNhom,
             string maGiaoDich,
             DateTime tuNgay,
             DateTime denNgay,
             string lyDo1,
             string lyDo1ChiTiet,
             string lyDo2,
             string lyDo2ChiTiet,
             string dienGiai,
             int maNvNghi,
             string tenNvNghi,
             string tenNvDuyet,
             string ngayDuyet,
             bool status
     )
        {
            _ngayNghiHopLeDao.TaoNgayNghiThaiSan(
                 truongNhom,
                     maGiaoDich,
                     tuNgay,
                     denNgay,
                     lyDo1,
                     lyDo1ChiTiet,
                     lyDo2,
                     lyDo2ChiTiet,
                     dienGiai,
                     maNvNghi,
                     tenNvNghi,
                     tenNvDuyet,
                     ngayDuyet,
                     status
                    );

        }

    }
}
