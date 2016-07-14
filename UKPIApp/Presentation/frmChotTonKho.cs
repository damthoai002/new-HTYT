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
    public partial class frmChotTonKho : Form
    {


        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmChotTonKho));

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
        List<ChotTonKhoHeader> listChotTonKho = new List<ChotTonKhoHeader>();
        ChotTonKhoHeader currentChotTon = null;
        private ChotTonKhoDao _chotTonKhoDao = new ChotTonKhoDao();
        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;



        //Parent component
        frmKhambenh parentForm;
        private ShareEntityDao _shareEntityDao = new ShareEntityDao();
        int currentRowIndex = -1;

        public frmChotTonKho()
        {

            InitializeComponent();
            grdBenhNhan.AutoGenerateColumns = false;
           // clsTitleManager.InitTitle(this);
            this.Text = "CHỐT TỒN KHO";
           // btnTaoPhieu.Visible = false;
            SetDefaultValue();
        }
        private void SetDefaultValue()
        {
            string listTrangThai = System.Configuration.ConfigurationManager.AppSettings["ListTrangThai"];
            string[] list = listTrangThai.Split(',');
            ccbTrangThai.Items.Add(new TrangThai { MaTrangThai = "", TenTrangThai = "" });
            for (int i = 0; i < list.Length; i++) {
                ccbTrangThai.Items.Add(new TrangThai { MaTrangThai = list[i], TenTrangThai = list[i] });
             }
            ccbTrangThai.SelectedIndex = 0;

            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();

            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            var firstOrDefault = listPhongKham.FirstOrDefault(a => a.RoomID == currentKho);
            if (firstOrDefault != null)
                txtKho.Text = firstOrDefault.RoomName;
            ;
            
    
        }
        public void SetParentForm(frmKhambenh parent)
        {
            this.parentForm = parent;
        }
        private void DeselectOrtherCheckbox(int currentRowIndex)
        {
            for (int i = 0; i < grdBenhNhan.Rows.Count; i++)
            {
                if (i != currentRowIndex)
                {
                    grdBenhNhan.Rows[i].Cells[0].Value = false;
                }
                else
                    continue;
            }
        }

        private bool IsChotTonSelected()
        {
            bool result = false;
            for (int i = 0; i < grdBenhNhan.Rows.Count; i++)
            {

                if (grdBenhNhan.Rows[i].Cells[0].Value != null && (bool)grdBenhNhan.Rows[i].Cells[0].Value == true)
                {
                    result = true;
                    break;
                }
                else
                    continue;
            }
            return true;
        }
        private void grdBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdBenhNhan.CurrentCell;

            if (currentCell != null)
            {
                grdBenhNhan.Rows[currentCell.RowIndex].Cells[0].Value = true;
                DeselectOrtherCheckbox(currentCell.RowIndex);
                currentRowIndex = currentCell.RowIndex;
            }
            else {
                currentRowIndex = -1;
            }
        }
        public void ReloadSearchResult()
        {
            GetChotTonResult();
        }
        private void GetChotTonResult()
        {
            string maChotTonKho = txtMaCHotTonKho.Text;
            string dienGiai = txtDienGiai.Text;
            string tenKho = txtKho.Text;
            DateTime ngayTaoPhieu = dpNgayTaoPhieu.Value;
            string status = ((TrangThai)(ccbTrangThai.SelectedItem)).TenTrangThai;
            bool isUseDate = ckUseDate.Checked;
            listChotTonKho = _chotTonKhoDao.SearchChotTonKho(maChotTonKho, dienGiai, tenKho, ngayTaoPhieu, status, isUseDate);
            if (listChotTonKho != null && listChotTonKho.Count > 0)
            {
                grdBenhNhan.DataSource = listChotTonKho;
            }
            else
            {
                listChotTonKho = new List<ChotTonKhoHeader>();
                grdBenhNhan.DataSource = listChotTonKho;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetChotTonResult();
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            frmChotTonKhoDetail detail = new frmChotTonKhoDetail();
            detail.SetCurrentChotTonKhoHeader(null);
            detail.SetParentForm(this);
            detail.ShowDialog();
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            if(!IsChotTonSelected() || currentRowIndex == -1)
            {
                DialogResult result = MessageBox.Show("Bạn chưa chọn chốt tồn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                currentChotTon = listChotTonKho[currentRowIndex];
                frmChotTonKhoDetail detail = new frmChotTonKhoDetail();
                detail.SetCurrentChotTonKhoHeader(currentChotTon);
                detail.SetParentForm(this);
                detail.ShowDialog();
            }
        }



       

       
     
     

      





     



      
    }
}