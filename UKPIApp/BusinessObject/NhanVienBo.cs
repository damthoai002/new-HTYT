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
    public class NhanVienBo
    {
        private readonly NhanVienDao _nhanVienDao = new NhanVienDao();
        private clsCommon _common = new clsCommon();
        /// <summary>
        /// Gets all active display set, both basic and extra display set
        /// </summary>
        /// <returns></returns>
        public DataTable GetNhanVienProWatch()
        {

            return _nhanVienDao.GetNhanVienProWatch();
        }

        public void InsertNhanVien(int sysId)
        {
            _nhanVienDao.InsertNhanVien(sysId);
        }

        public DataTable SearchNhanVienChamCong(string lName, string fName, string email, int isDataCc)
        {
            return _nhanVienDao.SearchNhanVienChamCong(lName, fName, email, isDataCc);
        }

        public bool CheckMaNvUnilerver(string maNvUnilever)
        {
            DataTable tb = _nhanVienDao.CheckMaNvUnilerver(maNvUnilever);
            if (tb.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckEmail(string email)
        {
            DataTable tb = _nhanVienDao.CheckEmail(email);
            if (tb.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateNhanVienCc(ClsNhanVien objNhanVien)
        {
            _nhanVienDao.UpdateNhanVienCC(objNhanVien);

        }


        public DataTable GetNhanVienHr(string fName, string lName, string maNvUnilever, string userName, string cardNo)
        {
            return _nhanVienDao.GetNhanVienHr(fName, lName, maNvUnilever, userName, cardNo);

        }


        public DataTable GetNvl3InL4(string userName, string maNvUnilever)
        {
            return _nhanVienDao.GetNvl3InL4(userName, maNvUnilever);

        }

        public DataTable GetNvl3Available(string fName, string lName, string maNvUnilever, string userName,
            string cardNo)
        {
            return _nhanVienDao.GetNvl3Available(fName, lName, maNvUnilever, userName, cardNo);
        }

        public void AddNvL3ToL4(string userId, List<ClsNhanVien> lstnv, string userNameL4, int levelQuanLyL4)
        {
            _nhanVienDao.AddNvL3ToL4(userId, lstnv, userNameL4, levelQuanLyL4);

        }


        public DataTable GetNvl2InL3(string userName, string maNvUnilever)
        {
            return _nhanVienDao.GetNvl2InHr(userName, maNvUnilever);

        }


        public DataTable GetNvl2Available(string fName, string lName, string maNvUnilever, string userName,
                  string cardNo)
        {
            return _nhanVienDao.GetNvl2Available(fName, lName, maNvUnilever, userName, cardNo);
        }

        public void AddNvL2ToL3(string userId, List<ClsNhanVien> lstnv, string userNameL3, int levelQuanLyL3)
        {
            _nhanVienDao.AddNvL2ToL3(userId, lstnv, userNameL3, levelQuanLyL3);

        }



        public DataTable GetNvl1InL2(string userName, string maNvUnilever)
        {
            return _nhanVienDao.GetNvl1InL2(userName, maNvUnilever);

        }


        public DataTable GetNvl1Available(string fName, string lName, string maNvUnilever, string userName,
                  string cardNo)
        {
            return _nhanVienDao.GetNvl1Available(fName, lName, maNvUnilever, userName, cardNo);
        }

        public void AddNvL1ToL2(string userId, List<ClsNhanVien> lstnv, string userNameL2, int levelQuanLyL2)
        {
            _nhanVienDao.AddNvL1ToL2(userId, lstnv, userNameL2, levelQuanLyL2);

        }


        public DataTable GetNvl0InL1(string userName, string maNvUnilever)
        {
            return _nhanVienDao.GetNvl0InL1(userName, maNvUnilever);

        }


        public DataTable GetNvl0Available(string fName, string lName, string maNvUnilever, string userName,
                  string cardNo)
        {
            return _nhanVienDao.GetNvl0Available(fName, lName, maNvUnilever, userName, cardNo);
        }

        public void AddNvL0ToL1(string userId, List<ClsNhanVien> lstnv, string userNameL1, int levelQuanLyL1)
        {
            _nhanVienDao.AddNvL0ToL1(userId, lstnv, userNameL1, levelQuanLyL1);

        }

        public DataTable LoadNhanVien(string maNv, string tenNv)
        {

            return _nhanVienDao.LoadNhanVien(maNv, tenNv);

        }

        public bool CheckExistEmp(string maNv)
        {
            return _nhanVienDao.CheckExistEmp(maNv);
        
        }

        public bool InsertNhanVien(Employees emp)
        {
           return _nhanVienDao.InsertNhanVien(emp);
        }
        public bool LuuCapNhatThongNhanVien(Employees emp)
        {
            return _nhanVienDao.LuuCapNhatThongNhanVien(emp);
        }
    }
}
