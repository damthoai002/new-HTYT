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
    public class NhanVienUsersBo
    {
        private readonly NhanVienUsersDao _nhanVienUsersDao = new NhanVienUsersDao();
        private clsCommon _common = new clsCommon();
        /// <summary>
        /// Gets all active display set, both basic and extra display set
        /// </summary>
        /// <returns></returns>

        public DataTable SearchNhanVienChamCong(string lName, string fName, string maNvUnilever, string loaiNv, string cardNo)
        {
            return _nhanVienUsersDao.GetNhanVienChamCong(lName, fName, maNvUnilever, loaiNv, cardNo);
        }

        public DataTable GetNhanVienQuanLy()
        {
            return _nhanVienUsersDao.GetNhanVienQuanLy();

        }
        public void InsertNvQuanLy(string strSysId, string userId)
        {
            _nhanVienUsersDao.InsertNvQuanLy(strSysId, userId);
        }
        public void RemoveNvQuanLy(List<ClsNhanVienUser> lstNvQl)
        {
            _nhanVienUsersDao.RemoveNvQl(lstNvQl);
        }

        public DataTable GetUserAvailible()
        {
            return _nhanVienUsersDao.GetUserAvailible();
        }

        public DataTable GetNvUser()
        {
            return _nhanVienUsersDao.GetNvUser();
        }
    }
}
