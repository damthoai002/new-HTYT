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
    public partial class frmSearchNhanVienNghiPhep : Form
    {


        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmSearchNhanVienNghiPhep));

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
        frmNghiPhep parentForm;
        private ShareEntityDao _shareEntityDao = new ShareEntityDao();
        int currentRowIndex = -1;

        public frmSearchNhanVienNghiPhep()
        {

            InitializeComponent();
            grdBenhNhan.AutoGenerateColumns = false;
            //clsTitleManager.InitTitle(this);
            this.Text = "TÌM KIẾM NHÂN VIÊN";
            btnChon.Visible = false;
            btnSearch_Click(null,null);
        }

        public void SetParentForm(frmNghiPhep parent)
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

        private bool IsNhanVienSelected()
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string maBenhNhan = txtMaBenhNhan.Text;
            string tenBenhNhan = txtTenBenhNhan.Text;
            listBenhNhan = _thongTinKhamBenhDao.SearchThongTinBenhNhan(maBenhNhan, tenBenhNhan);
            if(listBenhNhan != null && listBenhNhan.Count > 0)
            {
                grdBenhNhan.DataSource = listBenhNhan;
                btnChon.Visible = true; 
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (!IsNhanVienSelected() || currentRowIndex == -1)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmSearchNhanVienNghiPhep.SelectNhanVien"), clsResources.GetMessage("messages.frmSearchNhanVienNghiPhep.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else {
                if (currentRowIndex != -1)
                {
                    ThongTinBenhNhan ttbn = listBenhNhan[currentRowIndex];
                    parentForm.SetMaNhanVien(ttbn.EmployeeID);
                    parentForm.SetTenBenhNhan(ttbn.FullName);
                    parentForm.SetMaBHYT(ttbn.MaBHYT);
                    parentForm.SetBoPhan(ttbn.TenBoPhan);
                    parentForm.SetGioiTinh(ttbn.GioiTinh);
                    parentForm.SetKhuVuc(ttbn.KhuVuc);
                    parentForm.SetNamSinh(ttbn.NamSinh.ToString());
                    //parentForm.SetCongTy(string.Empty);
                    this.Close();
                }
            }
        }

        private void txtMaBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                this.btnSearch_Click(null,null);
            
            }

        }

        private void txtTenBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                this.btnSearch_Click(null, null);

            }
        }

        private void grdBenhNhan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnChon_Click(null, null);
        }



       

       
     
     

      





     



      
    }
}