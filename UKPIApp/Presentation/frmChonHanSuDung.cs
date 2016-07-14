using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using Excel;
using FPT.Component.ExcelPlus;
using UKPI.BusinessObject;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;

namespace UKPI.Presentation
{
    public partial class frmChonHanSuDung : Form
    {


        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmChonHanSuDung));

        private clsBaseBO _bo = new clsBaseBO();
        private ChamCongLichLamViecBo _ccllv = new ChamCongLichLamViecBo();
        private CreateTimesheetBo _tsBo = new CreateTimesheetBo();
        private readonly clsCommon _common = new clsCommon();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        readonly System.Data.DataTable _dt = null;

        private int _checkRowsCount = 0;
        DataGridViewCell currentCell;
        // Declare constants
        private const string FieldCheck = "colCheck";
        private const String Check = "CHECK";
        private const String ValueTrue = "Y";
        private const String ValueFalse = "N";
        //param value.
        private String parHanChotDuyetCong = "";
        private String parHanChotDitre = "";
        private String parHanChotVeSom = "";
        private String parChuanTinhCong = "";
        private String parHanMucTinhOt = "";


        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();
        List<ThongTinBenhNhan> listBenhNhan = new List<ThongTinBenhNhan>();
        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;



        //Parent component
        frmnhapkhothuoc parentForm;
        private ShareEntityDao _shareEntityDao = new ShareEntityDao();
        int currentRowIndex = -1;

        public frmChonHanSuDung()
        {

            InitializeComponent();
            clsTitleManager.InitTitle(this);
            this.Text = "HẠN SỬ DỤNG";
            btnChon.Visible = false;
        }

        public void SetParentForm(frmnhapkhothuoc parent)
        {
            this.parentForm = parent;
        }
      

        private void btnChon_Click(object sender, EventArgs e)
        {
            
        }



       

       
     
     

      





     



      
    }
}