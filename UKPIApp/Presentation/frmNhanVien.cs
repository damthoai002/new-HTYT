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
using UKPI.Controls;
namespace UKPI.Presentation
{
    public partial class frmNhanVien : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmNhanVien));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();

        private readonly NhanVienBo _nhanVienBo = new NhanVienBo();

        #endregion

        #region Constructors

        public frmNhanVien()
        {

            InitializeComponent();

            SetDefauldValue();
            this.Text = "QUẢN LÝ NHÂN VIÊN";
            this.AutoScroll = true;

        }


        private void SetDefauldValue()
        {
            ckbHoatDong.Checked = true;
            LoadNhanVien("", "");
            BindGioiTinh();
            BindBoPhan();
            BindKhuVuc();
        }

        private void LoadNhanVien(string maNv, string tenNv)
        {
            grdNhanVien.DataSource = _nhanVienBo.LoadNhanVien(maNv, tenNv);
        }

        private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }
        private void ResetFormThongTinNhanVien()
        {
            txtMaNV.Text = string.Empty;
            txtTenNV.Text = string.Empty;
            cboGioiTinh.SelectedIndex = -1;
            txtMaBHYT.Text = string.Empty;
            cbbKhuVuc.SelectedIndex = 0;
            txtNamSinh.Text = string.Empty;
            cbbKhuVuc.SelectedIndex = 0;
            cbbBoPhan.SelectedIndex = 0;

            txtNoiLamViec.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            txtCongTy.Text = string.Empty;
        }
        private void LoadThongTinThuocToForm(ThongTinThuoc selectedThuoc)
        {
            //txtMaNV.Text = selectedThuoc.MaThuocYTe;
            //txtTenNV.Text = selectedThuoc.MedicineName;
            //txtSoTT.Text = selectedThuoc.STTTheoDMTCuaBYT;
            //txtMaBHYT.Text = selectedThuoc.TenThanhPhanThuoc;
            //cbbKhuVuc.SelectedValue = selectedThuoc.DonViTinh;
            //cbBaoHiem.Checked = selectedThuoc.BaoHiem;

            //dtpNgayHieuLuc.Value = selectedThuoc.NgayHieuLuc.Trim() == "" ? DateTime.Now : DateTime.Parse(selectedThuoc.NgayHieuLuc);
            //ckbChonNgayHieuLuc.Checked = selectedThuoc.NgayHieuLuc.Trim() != "";
            //txtSttMHTKQDT.Text = selectedThuoc.SttMaHoaTheoKQDTSoQDStt;
            //txtDonVi.Text = selectedThuoc.TenDonViSYT_BV;
            //txtPhanNhom.Text = selectedThuoc.PhanNhomTheoTCHTVaTCCN;


            //txtGiaDNBanVAT.Text = selectedThuoc.GiaDNBanVAT.ToString();
            //txtGiaThucBan.Text = selectedThuoc.GiaThucBan.ToString();
            //txtNoiLamViec.Text = selectedThuoc.HamLuong;
            //txtEmail.Text = selectedThuoc.SoDKHoacGPKD;
            //txtDiaChi.Text = selectedThuoc.DangBaoCheDuongUong;
            //txtDienThoai.Text = selectedThuoc.NhaSanXuat;
            //txtQuocGia.Text = selectedThuoc.QuocGia;
            //cbHoatDong.Checked = selectedThuoc.HoatDong;
            //txtHeSoAnToan.Text = selectedThuoc.HeSoAnToan.ToString();
            //txtNhomThuoc.Text = selectedThuoc.NhomThuoc;
        }
        private Employees BuildThongNhanVien(bool isNew)
        {
            Employees nv = new Employees();
            if (isNew)
            {
                nv.SysId = long.Parse("0");
                nv.EmployeeID = txtMaNV.Text;
                nv.FullName = txtTenNV.Text;
                nv.GioiTinh = Int32.Parse(cboGioiTinh.SelectedValue.ToString());
                nv.MaBHYT = txtMaBHYT.Text;
                nv.NgayThangNamSinh = txtNamSinh.Text;
                nv.KhuVuc = cbbKhuVuc.Text;
                nv.DepartmentID = Int32.Parse(cbbBoPhan.SelectedValue.ToString());
                nv.ViTriLamViec = txtNoiLamViec.Text;
                nv.Email = txtEmail.Text;
                nv.Address = txtDiaChi.Text;
                nv.Phone = txtDienThoai.Text;
                nv.CongTy = txtCongTy.Text;
                nv.CreatedBy = clsSystemConfig.UserName;
                nv.CreatedDate = DateTime.Now.ToString();
                nv.LastUpdatedDate = DateTime.Now;
                nv.LastUpdatedBy = clsSystemConfig.UserName;
                nv.Status = ckbHoatDong.Checked;
            }
            else
            {
                nv.SysId = long.Parse(txtSysId.Text);
                nv.EmployeeID = txtMaNV.Text;
                nv.FullName = txtTenNV.Text;
                nv.GioiTinh = Int32.Parse(cboGioiTinh.SelectedValue.ToString());
                nv.MaBHYT = txtMaBHYT.Text;
                nv.NgayThangNamSinh = txtNamSinh.Text;
                nv.KhuVuc = cbbKhuVuc.Text;
                nv.DepartmentID = Int32.Parse(cbbBoPhan.SelectedValue.ToString());
                nv.ViTriLamViec = txtNoiLamViec.Text;
                nv.Email = txtEmail.Text;
                nv.Address = txtDiaChi.Text;
                nv.Phone = txtDienThoai.Text;
                nv.CreatedBy = clsSystemConfig.UserName;
                nv.CreatedDate = DateTime.Now.ToString();
                nv.LastUpdatedDate = DateTime.Now;
                nv.LastUpdatedBy = clsSystemConfig.UserName;
                nv.Status = ckbHoatDong.Checked;
                nv.CongTy = txtCongTy.Text;
            }

            return nv;
        }

        private void BindGioiTinh()
        {
            cboGioiTinh.DisplayMember = "Name";
            cboGioiTinh.ValueMember = "Id";
            cboGioiTinh.DataSource = _shareEntityDao.LoadGioiTinhNhanVien();
        }
        private void BindBoPhan()
        {
            cbbBoPhan.ValueMember = "MaBoPhan";
            cbbBoPhan.DisplayMember = "TenBoPhan";
            cbbBoPhan.DataSource = _shareEntityDao.LoadDanhSachBoPhan();
        }
        private void BindKhuVuc()
        {
            cbbKhuVuc.ValueMember = "MaKhuVuc";
            cbbKhuVuc.DisplayMember = "TenKhuVuc";
            cbbKhuVuc.DataSource = _shareEntityDao.LoadDanhSachKhuVuc();
        }

        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Employees emp = BuildThongNhanVien(false);

            if (_nhanVienBo.LuuCapNhatThongNhanVien(emp))
            {

                ResetFormThongTinNhanVien();
                MessageBox.Show("Cập nhật thành công");
                grdNhanVien.DataSource = _nhanVienBo.LoadNhanVien(txtsMaNV.Text, txtsTenNV.Text);
            }
            else
            {
                MessageBox.Show("Không thể cập nhật thông tin thuốc");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            //txtMaNV.ReadOnly = false;
            btnUpdate.Enabled = false;
            btnLuu.Enabled = true;
            txtMaNV.Enabled = true;
            ResetFormThongTinNhanVien();
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_nhanVienBo.CheckExistEmp(txtMaNV.Text))
            {
                Employees emp = BuildThongNhanVien(true);
                if (_nhanVienBo.InsertNhanVien(emp))
                {
                    LoadNhanVien(txtsMaNV.Text, txtsTenNV.Text);
                    ResetFormThongTinNhanVien();
                    MessageBox.Show("Lưu thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi trong khi lưu");
                }
            }
            else
            {
                MessageBox.Show("Mã nhân viên đả tồn tại trong hệ thống!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadNhanVien(txtsMaNV.Text, txtsTenNV.Text);
        }

        private void ckbChonNgayHieuLuc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grdNhanVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Employees emp = new Employees();
            DataTable tb = grdNhanVien.DataSource as DataTable;

            txtMaNV.Text = tb.Rows[e.RowIndex]["MaNV"].ToString();
            txtTenNV.Text = tb.Rows[e.RowIndex]["TenNV"].ToString();
            cboGioiTinh.Text = tb.Rows[e.RowIndex]["GioiTinh"].ToString();//Int16.Parse( tb.Rows[e.RowIndex]["MaGT"].ToString());
            txtMaBHYT.Text = tb.Rows[e.RowIndex]["MaBHYT"].ToString();
            txtNamSinh.Text = tb.Rows[e.RowIndex]["NamSinh"].ToString();
            cbbKhuVuc.Text = tb.Rows[e.RowIndex]["KhuVuc"].ToString();
            cbbBoPhan.Text = tb.Rows[e.RowIndex]["BoPhan"].ToString();
            txtNoiLamViec.Text = tb.Rows[e.RowIndex]["NoiLamViec"].ToString();
            txtEmail.Text = tb.Rows[e.RowIndex]["Email"].ToString();
            txtDiaChi.Text = tb.Rows[e.RowIndex]["DiaChi"].ToString();
            txtDienThoai.Text = tb.Rows[e.RowIndex]["DienThoai"].ToString();
            ckbHoatDong.Checked = (bool)tb.Rows[e.RowIndex]["HoatDong"];
            txtSysId.Text = tb.Rows[e.RowIndex]["SysId"].ToString();
            btnLuu.Enabled = false;
            btnUpdate.Enabled = true;
            txtMaNV.Enabled = false;
            txtCongTy.Text = tb.Rows[e.RowIndex]["CongTy"].ToString();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {

        }


    }
}