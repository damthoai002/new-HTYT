using System;
using System.Collections.Generic;
using System.Text;

namespace UKPI.ValueObject
{
    public class ClsNhanVien
    {
        public long SysId { get; set; }
        public string MaNVUnilever { get; set; }
        public string MaLoaiNhanVien { get; set; }
        public string Location { get; set; }
        public string CardNo { get; set; }
        public string LNAME { get; set; }
        public string FNAME { get; set; }
        public string MI { get; set; }
        public bool GioiTinh { get; set; }
        public string Email { get; set; }
        public string BADGE_STATUS_DESC { get; set; }
        public string BADGE_TYPE_DESC { get; set; }

        public string ISSUE_DATE { get; set; }
        public string EXPIRE_DATE { get; set; }
        public bool IsDataCC { get; set; }
        public bool IsOutsource { get; set; }
        public string OutsourceId { get; set; }
        public string CreateDate { get; set; }
        public string CreaterId { get; set; }
        public string LastUpDate { get; set; }
        public string lastUpdateId { get; set; }
        public bool IsActive { get; set; }

        //User  
        public string Username { get; set; }
        public string Password   { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PwdChgDate { get; set; }
        public string Status { get; set; }
        public string URoleId { get; set; }
        public string Description { get; set; }
        public int LevelQuanLy { get; set; }



    }
}
