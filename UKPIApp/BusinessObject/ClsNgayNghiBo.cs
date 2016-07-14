using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net.Util;
using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class ClsNgayNghiBo
    {
        private static log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ClsNgayNghiBo));
        private ClsNgayNghiDao _ngayNghiDao = new ClsNgayNghiDao();
        private clsCommon _common = new clsCommon();
        /// <summary>
        /// Kiem tra ton tai lich lam viec
        ///  </summary>
        /// <returns></returns>
        public DataTable GetNgayNghi(int nam)
        {
            var table = _ngayNghiDao.GetNgayNghi(nam);
            return table;
        }
        public void TaoNgayNghiChuNhat(string truongNhomId, int year, string moTa)
        {
            _ngayNghiDao.TaoNgayNghiChuNhat(truongNhomId, year, moTa);

        }
        public void TaoNgayNghiTrongNam(string maNgayNghi, DateTime ngayBatDau, DateTime ngayKetThuc, string mota, string createId)
        {
            _ngayNghiDao.TaoNgayNghiTrongNam(maNgayNghi, ngayBatDau, ngayKetThuc, mota, createId);

        }

        public void TaoNgayNghiThu7(string truongNhomId, int year, string moTa)
        {
            _ngayNghiDao.TaoNgayNghiThu7(truongNhomId, year, moTa);

        }


        public void NgungSuDungNgayNghi(string sysId)
        {

            _ngayNghiDao.NgungSuDungNgayNghi(sysId);
        }

    }
}
