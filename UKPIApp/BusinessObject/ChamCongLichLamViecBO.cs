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
    public class ChamCongLichLamViecBo
    {
        private ChamCongLichLamViecDAO _chamCongLichLamViecDao = new ChamCongLichLamViecDAO();
        private clsCommon _common = new clsCommon();
        /// <summary>
        /// Gets all active display set, both basic and extra display set
        /// </summary>
        /// <returns></returns>
        public DataTable GetChamCongLichLamViecL0(string tuan, string tuNgay, string denNgay,
            string truongNhom, string onOff, string ca, string l0XacNhan, string nhomId)
        {
            return _chamCongLichLamViecDao.GetLichLamViecL0(tuan, tuNgay, denNgay,
             truongNhom, onOff, ca, l0XacNhan, nhomId);
        }

        /// <summary>
        /// Gets all active display set, both basic and extra display set
        /// </summary>
        /// <returns></returns>
        public DataTable GetChamCongLichLamViec(string tuan, string tuNgay, string denNgay,
            string truongNhom, string onOff, string ca, string l1XacNhan, string nhomId)
        {
            return _chamCongLichLamViecDao.GetLichLamViec(tuan, tuNgay, denNgay,
             truongNhom, onOff, ca, l1XacNhan, nhomId);
        }
        public DataTable GetNhanVien(int nhom, string truongNhom)
        {
            return _chamCongLichLamViecDao.GetNhanVien(nhom, truongNhom);
        }

        public DataTable GetChamCongLichLamViecL2(string tuan, string tuNgay, string denNgay,
                 string truongNhom, string onOff, string ca, string l2XacNhan, string nhomId)
        {
            return _chamCongLichLamViecDao.GetLichLamViecL2(tuan, tuNgay, denNgay,
             truongNhom, onOff, ca, l2XacNhan, nhomId);
        }


        public DataTable GetChamCongLichLamViecL3(string tuan, string tuNgay, string denNgay,
                        string truongNhom, string onOff, string ca, string l3XacNhan, string nhomId)
        {
            return _chamCongLichLamViecDao.GetLichLamViecL3(tuan, tuNgay, denNgay,
             truongNhom, onOff, ca, l3XacNhan, nhomId);
        }

        public void UpdateChamCongLichLamViec(List<ClsLichLamViec> items)
        {
            _chamCongLichLamViecDao.SaveLichLamViec(items);
        }
        public void XacNhanChamCongLichLamViec(List<ClsLichLamViec> items)
        {
            _chamCongLichLamViecDao.XacNhanLichLamViec(items);
        }
        public void XacNhanChamCongLichLamViecL0(List<ClsLichLamViec> items)
        {
            _chamCongLichLamViecDao.XacNhanLichLamViecL0(items);
        }
        public void LayDuLieuChamCongVaLuu(string lastUpdatedate, string lastUpId,
            string tuan, string tuNgay, string denNgay,
            string maTruongNhom, string ca, string nhomId)
        {

            _chamCongLichLamViecDao.LayDuLieuChamCongVaLuu(lastUpdatedate, lastUpId, tuan, tuNgay, denNgay, maTruongNhom, ca, nhomId);
        }

        public void LayDuLieuChamCongVaLuuL0(string lastUpdatedate, string lastUpId,
         string tuan, string tuNgay, string denNgay,
         string maTruongNhom, string ca, string nhomId)
        {

            _chamCongLichLamViecDao.LayDuLieuChamCongVaLuuL0(lastUpdatedate, lastUpId, tuan, tuNgay, denNgay, maTruongNhom, ca, nhomId);
        }

        public void XacNhanChamCongLichLamViecL2(List<ClsLichLamViec> items)
        {
            _chamCongLichLamViecDao.XacNhanLichLamViecL2(items);
        }
        public void XacNhanChamCongLichLamViecL3(List<ClsLichLamViec> items)
        {
            _chamCongLichLamViecDao.XacNhanLichLamViecL3(items);
        }
        public void XacNhanChamCongLichLamViecL4(List<ClsLichLamViec> items)
        {
            _chamCongLichLamViecDao.XacNhanLichLamViecL4(items);
        }

        public void RejectedChamCongLichLamViec(string sysId, string note, string lastUpdate, string lastUpId, string level)
        {
            _chamCongLichLamViecDao.RejectLichLamViec(sysId, note, lastUpdate, lastUpId,level);
        }

        public DataTable GetEmailOfLeverApprove(int sysId, int lever)
        {
            return _chamCongLichLamViecDao.GetEmailOfLeverApprove(sysId, lever);
        }

        public DataTable GetTruongNhomL1(string userL2)
        {
            return _chamCongLichLamViecDao.GetTruongNhomL1(userL2);
        }

        public DataTable GetTruongNhomL1ByL3(string userL3)
        {
            return _chamCongLichLamViecDao.GetTruongNhomL1ByL3(userL3);
        }

        public DataTable GetTruongNhomL1ByL4(string userL4)
        {
            return _chamCongLichLamViecDao.GetTruongNhomL1ByL4(userL4);
        }

        public DataTable GetTruongNhomL0ByL1(string userL1)
        {

            return _chamCongLichLamViecDao.GetTruongNhomL0ByL1(userL1);
        }


    }
}
