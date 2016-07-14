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
    public class ManageStaffOfGroupBo
    {
        private ManageStaffOfGroupDao _manageStaffOfGroupDao = new ManageStaffOfGroupDao();
        private clsCommon _common = new clsCommon();

        public DataTable GetThanhVienNhom(string nhom)
        {
            return _manageStaffOfGroupDao.GetThanhVienNhom(nhom);
        }

        public DataTable GetNhomByNhomTruong(string truongnhom)
        {
            return _manageStaffOfGroupDao.GetNhomByNhomTruong(truongnhom);
        }

        public DataTable GetNvCc()
        {
            return _manageStaffOfGroupDao.GetNvCc();
        }

        public DataTable SearchNvCc(string ten, string ho,string loaiNv, string maThe, string maNvUnilever)
        {
            return _manageStaffOfGroupDao.SearchNvCc( ten,  ho,   loaiNv,  maThe, maNvUnilever);
        }

        public void AppNvToGroup(string sysId,string maNhom,string maTruongNhom)
        {
            _manageStaffOfGroupDao.AppNvToGroup(sysId, maNhom, maTruongNhom);
        }
        public void RemoveNvToGroup(string sysId, string maNhom, string maTruongNhom)
        {
            _manageStaffOfGroupDao.RemoveNvToGroup(sysId, maNhom, maTruongNhom);
        }

      
    }
}
