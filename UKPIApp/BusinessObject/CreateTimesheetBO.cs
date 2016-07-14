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
    public class CreateTimesheetBo
    {
        private static log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CreateTimesheetBo));
        private CreatedTimesheetDao _createTimesheetDao = new CreatedTimesheetDao();
        private clsCommon _common = new clsCommon();
        /// <summary>
        /// Kiem tra ton tai lich lam viec
        ///  </summary>
        /// <returns></returns>
        public bool CheckExistTimesheet(long nhomId, string truongNhomId, string tuNgay, string denNgay, string isOutsource)
        {
            var table = _createTimesheetDao.CheckExistLichLamViec(nhomId, truongNhomId, tuNgay, denNgay, isOutsource);
            return table.Rows.Count > 0;
        }
        public bool CheckExistCalamViecNhom(long nhomId, string tuNgay, string denNgay)
        {
            var table = _createTimesheetDao.CheckExistCalamViecNhom(nhomId, tuNgay, denNgay);
            return table.Rows.Count > 0;
        }
        public void CreateTimesheet(ClsCreateTimesheet t)
        {
            _createTimesheetDao.DoCreateTimesheet(t);

        }

        public void DeleteTimesheet(ClsCreateTimesheet t)
        {
            _createTimesheetDao.DoDeleteTimesheet(t);

        }
        public DataTable ViewTimesheet(long nhomId, string truongNhomId, string tuNgay, string denNgay)
        {
            return _createTimesheetDao.ViewTimesheet(nhomId, truongNhomId, tuNgay, denNgay);

        }
        public DataTable GetNhomTruong(string truongNhomId)
        {
            return _createTimesheetDao.GetNhomTruong(truongNhomId);

        }

        public DataTable GetNhomByNhomTruong(string truongNhomId)
        {
            return _createTimesheetDao.GetNhomByNhomTruong(truongNhomId);

        }

        public void CreateShiftForTeam(int nhom, string truongNhom, int tuTuan, int denTuan, int year,  string dauDocTheVao, string dauDocTheRa)
        {
            _createTimesheetDao.CreateShiftForTeam(nhom, truongNhom, tuTuan, denTuan, year,  dauDocTheVao, dauDocTheRa);
        }

        public void CreateShiftForTeamHanhChinh(int nhom, string truongNhom, int tuTuan, int denTuan, int year, string dauDocTheVao, string dauDocTheRa)
        {
            _createTimesheetDao.CreateShiftForTeamHanhChinh(nhom, truongNhom, tuTuan, denTuan, year, dauDocTheVao, dauDocTheRa);
        }

        public DataTable GetShiftForTeam(int nhom, int tuTuan, int denTuan, int year)
        {
            return _createTimesheetDao.GetShiftForTeam(nhom, tuTuan, denTuan, year);
        }

        public void UpdateShiftForTeam(DataRow row)
        {
            try
            {
                _createTimesheetDao.UpdateShiftForTeam(row);
            }
            catch (System.Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public DataTable GetCcLichLamViec(string tuan, string tuNgay, string denNgay, string maTruongNhom, string nhomId)
        {
            return _createTimesheetDao.GetCcLichLamViec(tuan, tuNgay, denNgay, maTruongNhom, nhomId);
        }

        public void RemoveTimesheets(List<ClsLichLamViec> lstLichLamViecs)
        {

            _createTimesheetDao.RemoveTimesheets(lstLichLamViecs);
        }

        public void AddOneTimesheet(List<ClsCreateTimesheet> lstTimesheets)
        {

            _createTimesheetDao.AddOneTimesheet(lstTimesheets);
        }

        public DataTable GetNhanVienNhomAvailable(ClsCreateTimesheet item)
        {
            return _createTimesheetDao.GetNhanVienNhomAvailable(item);
        }
        public DataTable GetNhanVienNhomAvailable(ClsCreateTimesheet item, string tenNhanVien, string hoNhanVien)
        {
            return _createTimesheetDao.GetNhanVienNhomAvailable(item, tenNhanVien, hoNhanVien);
        }

        public DataTable GetDauDocThe()
        {
            return _createTimesheetDao.GetDauDocThe();
        }

        public DataTable GetDaLamViec()
        {

            return _createTimesheetDao.GetDaLamViec();
        }

        public DataTable GetMonthOfSystem()
        {
            return _createTimesheetDao.GetMonthOfSystem();

        }

    }
}
