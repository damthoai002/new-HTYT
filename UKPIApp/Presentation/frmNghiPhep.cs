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
    public partial class frmNghiPhep : Form
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmNghiPhep));

        private clsBaseBO _bo = new clsBaseBO();
        private ChamCongLichLamViecBo _ccllv = new ChamCongLichLamViecBo();
        private CreateTimesheetBo _tsBo = new CreateTimesheetBo();
        private readonly clsCommon _common = new clsCommon();
        readonly System.Data.DataTable _dt = null;
        private readonly NgayNghiDao _ngayNghiDao = new NgayNghiDao();


        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;


        //Parent component
        frmKhambenh parentForm;
        private ShareEntityDao _shareEntityDao = new ShareEntityDao();

        public frmNghiPhep()
        {

            InitializeComponent();

            //  clsTitleManager.InitTitle(this);

            SetDefauldValue();

            this.Text = "QUYẾT ĐỊNH NGHỈ PHÉP CÓ BẢO HIỂM";

        }

        public void SetParentForm(frmKhambenh parent)
        {
            //this.parentForm = parent;
        }

        private void BindNgayNghi()
        {
            grvNgayNghi.DataSource = _ngayNghiDao.GetNgayNghi();

        }
        private void SetDefauldValue()
        {

            BindLyDo();
            BindNgayNghi();
            //txtBenhNhan.Text = clsSystemConfig.UserName;
        }
        private void BindLyDo()
        {
            cbbLyDo.DataSource = _shareEntityDao.LoadLyDo();
        }

        public void SetMaNhanVien(string maNhanVien)
        {
            txtMaNv.Text = maNhanVien;
        }
        public void SetTenBenhNhan(string tenBenhNhan)
        {
            txtBenhNhan.BackColor = Color.White;
            txtBenhNhan.Text = tenBenhNhan;
        }
        public void SetMaBHYT(string maBHYT)
        {
            txtMaBHYT.Text = maBHYT;
        }
        public void SetNamSinh(string namSinh)
        {
            txtNamSinh.Text = namSinh;
        }

        public void SetGioiTinh(string gioiTinh)
        {
            txtGioiTinh.Text = gioiTinh;

        }
        public void SetBoPhan(string boPhan)
        {
            txtBoPhan.Text = boPhan;
        }
        public void SetKhuVuc(string khuVuc)
        {
            txtKhuVuc.Text = khuVuc;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBenhNhan.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn bệnh nhân!");
                return;
            }

            if (txtSysId.Text == "")
            {
                List<NgayNghiKhamBenh> lstnnkb = new List<NgayNghiKhamBenh>();


                NgayNghiKhamBenh nnkb = new NgayNghiKhamBenh();

                nnkb.MaNv = txtMaNv.Text;
                nnkb.TenNV = txtBenhNhan.Text;
                nnkb.GioiTinh = txtGioiTinh.Text;
                nnkb.SoNgayNghi = txtSndn.Text;
                nnkb.NgayNghiTu = dtpTuNgay.Value.ToString("yyyyMMddhhmmss");
                nnkb.NgayNghiDen = dtpDenNgay.Value.ToString("yyyyMMddhhmmss");
                nnkb.LyDo = this.cbbLyDo.Text;
                nnkb.LyDoChiTiet = this.cbbLyDoChiTiet.Text;
                nnkb.ChuThich = txtChuThich.Text;
                nnkb.DienGiai = txtDienGiai.Text;
                nnkb.NguoiTao = clsSystemConfig.UserName;
                nnkb.NgayTao = DateTime.Now.ToString("yyyyMMddhhmmss");
                nnkb.IsActive = true;
                lstnnkb.Add(nnkb);

                _ngayNghiDao.NhapNgaynghi(lstnnkb);
                BindNgayNghi();
                MessageBox.Show("Đăng ký ngày phép thành công!");
                //this.Close();
                ClearForm();
            }
            else
            {
                NgayNghiKhamBenh nnkb = new NgayNghiKhamBenh();
                nnkb.SysId = Int32.Parse(txtSysId.Text);
                nnkb.MaNv = txtMaNv.Text;
                nnkb.TenNV = txtBenhNhan.Text;
                nnkb.GioiTinh = txtGioiTinh.Text;
                nnkb.SoNgayNghi = txtSndn.Text;
                nnkb.NgayNghiTu = dtpTuNgay.Value.ToString("yyyyMMddhhmmss");
                nnkb.NgayNghiDen = dtpDenNgay.Value.ToString("yyyyMMddhhmmss");
                nnkb.LyDo = this.cbbLyDo.Text;
                nnkb.LyDoChiTiet = this.cbbLyDoChiTiet.Text;
                nnkb.ChuThich = txtChuThich.Text;
                nnkb.DienGiai = txtDienGiai.Text;
                nnkb.NguoiTao = clsSystemConfig.UserName;
                nnkb.NgayTao = DateTime.Now.ToString("yyyyMMddhhmmss");
                nnkb.IsActive = ckbActive.Checked;
                _ngayNghiDao.UpdateNgayNghi(nnkb);

                BindNgayNghi();
                MessageBox.Show("Cập nhật ngày phép thành công!");
                ClearForm();

            }


        }

        private void ClearForm()
        {
            txtBenhNhan.Text = "";
            txtBoPhan.Text = "";
            txtChuThich.Text = "";
            txtDienGiai.Text = "";
            txtGioiTinh.Text = "";
            txtMaBHYT.Text = "";
            txtMaNv.Text = "";
            txtNamSinh.Text = "";
            txtSndn.Text = "";
            ckbActive.Enabled = false;
            ckbActive.Checked = true;
        }


        private void cbbLyDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbLyDoChiTiet.DataSource = _shareEntityDao.LoadLyDoChiTiet((int)cbbLyDo.SelectedValue);
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value > dtpDenNgay.Value)
            {
                DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmNghiPhep.Waring"), clsResources.GetMessage("messages.frmNghiPhep.Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtSndn.Text = ((dtpDenNgay.Value.Date - dtpTuNgay.Value.Date).TotalDays + 1).ToString();
            }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDenNgay.Value < dtpTuNgay.Value)
            {
                DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmNghiPhep.Waring1"), clsResources.GetMessage("messages.frmNghiPhep.Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtSndn.Text = ((dtpDenNgay.Value.Date - dtpTuNgay.Value.Date).TotalDays + 1).ToString();
            }

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            frmSearchNhanVienNghiPhep a = new frmSearchNhanVienNghiPhep();
            a.SetParentForm(this);
            a.Show();
        }

        private void grvNgayNghi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show("aaaa");
            int row = e.RowIndex;
            DataTable tb = grvNgayNghi.DataSource as DataTable;
            txtSysId.Text = tb.Rows[row]["SysId"].ToString();
            txtBenhNhan.Text = tb.Rows[row]["TenNV"].ToString();
            txtMaNv.Text = tb.Rows[row]["MaNv"].ToString();

            txtSndn.Text = tb.Rows[row]["SoNgayNghi"].ToString();

            txtChuThich.Text = tb.Rows[row]["ChuThich"].ToString();
            txtDienGiai.Text = tb.Rows[row]["DienGiai"].ToString();

            cbbLyDo.Text = tb.Rows[row]["LyDo"].ToString();
            cbbLyDoChiTiet.Text = tb.Rows[row]["LyDoChiTiet"].ToString();
            CultureInfo provider = CultureInfo.InvariantCulture;
            var tuNgay = DateTime.ParseExact(tb.Rows[row]["NgayNghiTu"].ToString(), "dd/MM/yyyy", provider);
            var denNgay = DateTime.ParseExact(tb.Rows[row]["NgayNghiDen"].ToString(), "dd/MM/yyyy", provider);

            dtpTuNgay.Value = tuNgay;
            dtpDenNgay.Value = denNgay;
            ckbActive.Checked = bool.Parse(tb.Rows[row]["IsActive"].ToString());
            ckbActive.Enabled = true;
        }




    }
}