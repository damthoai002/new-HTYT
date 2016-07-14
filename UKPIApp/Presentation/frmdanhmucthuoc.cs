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
    public partial class frmdanhmucthuoc : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmdanhmucthuoc));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        //private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly QuanLyThuocDao _quanLyThuocDao = new QuanLyThuocDao();
        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        DataGridViewCell currentCell;
        private DateTimePicker cellDateTimePicker;
        private int _checkRowsCount = 0;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();

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

        private List<ThongTinThuoc> listThuoc = new List<ThongTinThuoc>();
        private List<ThongTinThuoc> listThuocPaging = new List<ThongTinThuoc>();
        private ThongTinThuoc selectedThuoc;

        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;
        int currentRowIndex = -1;

        private int CurrentPage = 1;
        int PagesCount = 1;
        int pageRows = 10;
        #endregion

        #region Constructors

        public frmdanhmucthuoc()
        {

            InitializeComponent();

            SetDefauldValue();
            this.Text = "DANH MỤC THUỐC";
            this.AutoScroll = true;

        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            cellDateTimePicker.Visible = false;
        }
        private void SetDefauldValue()
        {
            //    BuildGridViewRow();
            grdToaThuoc.AutoGenerateColumns = false;
            grdToaThuoc.CellDoubleClick += grdToaThuoc_CellDoubleClick;
            btnUpdate.Enabled = false;

            cbbDonViTinh.DataSource = _shareEntityDao.LoadDonViTinh();
            txtNhaSanXuat.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNhaSanXuat.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection nsxDataCollection = new AutoCompleteStringCollection();
            addNhaSanXuatItems(nsxDataCollection);
            txtNhaSanXuat.AutoCompleteCustomSource = nsxDataCollection;

            txtQuocGia.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtQuocGia.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection qgDataCollection = new AutoCompleteStringCollection();
            addQUocGiaItems(qgDataCollection);
            txtQuocGia.AutoCompleteCustomSource = qgDataCollection;

    

            txtNhomThuoc.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNhomThuoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection nhomThuocDataCollection = new AutoCompleteStringCollection();
            addNhomThuocItems(nhomThuocDataCollection);
            txtNhomThuoc.AutoCompleteCustomSource = nhomThuocDataCollection;

            LoadDanhMucThuoc(string.Empty, string.Empty);

            dtpNgayHieuLuc.Format = DateTimePickerFormat.Custom;
            dtpNgayHieuLuc.CustomFormat = "dd/MM/yyyy";
            dtpNgayHieuLuc.Value = DateTime.Now;
            dtpNgayHieuLuc.Enabled = true;
            ckbChonNgayHieuLuc.Checked = true;
            cbHoatDong.Checked = true;
            LoadCachDung();

        }

        private void LoadCachDung()
        {
            DataTable tb = _shareEntityDao.LoadCachDung();
            cboCachDung.ValueMember = "MaUongThuoc";
            cboCachDung.DisplayMember = "CachUongThuoc";
            cboCachDung.DataSource = tb;
        }


        private void LoadDanhMucThuoc(string maThuocYTe, string tenThuoc)
        {
            // ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            listThuoc = _quanLyThuocDao.LoadDanhMucThuoc(maThuocYTe, tenThuoc);
            grdToaThuoc.DataSource = listThuoc;



        }
        private void addNhomThuocItems(AutoCompleteStringCollection col)
        {
            List<NhomThuoc> listNhomThuoc = _shareEntityDao.LoadThongTinNhomThuoc();
            for (int i = 0; i < listNhomThuoc.Count; i++)
            {
                col.Add(listNhomThuoc[i].TenNhomThuoc);
            }
        }
        private void addThongTinThuocItems(AutoCompleteStringCollection col1, AutoCompleteStringCollection col2)
        {
            List<ThongTinThuocTomLuoc> listThongTinThuoc = _shareEntityDao.LoadAllThongTinThuoc();
            for (int i = 0; i < listThongTinThuoc.Count; i++)
            {
                col1.Add(listThongTinThuoc[i].MaThuocYTeHienThi);
                col2.Add(listThongTinThuoc[i].MedicineName);
            }
        }
        private void addNhaSanXuatItems(AutoCompleteStringCollection col)
        {
            List<NhaSanXuat> listNhaSanXuat = _shareEntityDao.LoadNhaSanXuat();
            for (int i = 0; i < listNhaSanXuat.Count; i++)
            {
                col.Add(listNhaSanXuat[i].TenNhaSanXuat);
            }
        }
        private void addQUocGiaItems(AutoCompleteStringCollection col)
        {
            List<QuocGia> listQuocGia = _shareEntityDao.LoadQuocGia();
            for (int i = 0; i < listQuocGia.Count; i++)
            {
                col.Add(listQuocGia[i].TenQuocGia);
            }
        }
        private void BuildGridViewRow()
        {
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Width = 60;
            grdToaThuoc.Columns.Add(checkBoxColumn);


            DataGridViewTextBoxColumn tenThuocColumn = new DataGridViewTextBoxColumn();
            tenThuocColumn.HeaderText = "Tên thuốc";
            tenThuocColumn.Width = 145;
            tenThuocColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(tenThuocColumn);

            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.Width = 140;
            col.HeaderText = "Mã thuốc";
            col.DataSource = _shareEntityDao.LoadThongTinThuoc();
            col.DisplayMember = "MedicineID";
            col.ValueMember = "MedicineID";
            grdToaThuoc.Columns.Add(col);

            DataGridViewTextBoxColumn hanSuDungColumn = new DataGridViewTextBoxColumn();
            hanSuDungColumn.Width = 130;
            hanSuDungColumn.HeaderText = "Hạn sử dụng";
            grdToaThuoc.Columns.Add(hanSuDungColumn);

            DataGridViewCheckBoxColumn baoHiemColumn = new DataGridViewCheckBoxColumn();
            baoHiemColumn.Width = 100;
            baoHiemColumn.HeaderText = "Thuốc BH";
            baoHiemColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(baoHiemColumn);

            DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
            soLuongColumn.Width = 130;
            soLuongColumn.HeaderText = "Số lượng";
            grdToaThuoc.Columns.Add(soLuongColumn);



            DataGridViewTextBoxColumn giaNhapColumn = new DataGridViewTextBoxColumn();
            giaNhapColumn.Width = 130;
            giaNhapColumn.HeaderText = "Giá thời diểm nhập";
            giaNhapColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(giaNhapColumn);

            DataGridViewTextBoxColumn giaTTColumn = new DataGridViewTextBoxColumn();
            giaTTColumn.Width = 130;
            giaTTColumn.HeaderText = "Giá TT";
            giaTTColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(giaTTColumn);


            DataGridViewTextBoxColumn giaSTColumn = new DataGridViewTextBoxColumn();
            giaSTColumn.Width = 130;
            giaSTColumn.HeaderText = "Giá ST";
            giaSTColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(giaSTColumn);

            DataGridViewTextBoxColumn thanhTienColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn.Width = 130;
            thanhTienColumn.HeaderText = "Thành tiến";
            thanhTienColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(thanhTienColumn);

            DataGridViewTextBoxColumn SttMaHoaTheoKQDTSoQDSttColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn.Width = 130;
            thanhTienColumn.HeaderText = "STT mã hóa theo KQĐT (số QĐ.STT)";
            thanhTienColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(SttMaHoaTheoKQDTSoQDSttColumn);

            DataGridViewTextBoxColumn TenDonViSYT_BVColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn.Width = 130;
            thanhTienColumn.HeaderText = "Tên đơn vị (SYT/BV)";
            thanhTienColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(TenDonViSYT_BVColumn);



            grdToaThuoc.CellClick += grdToaThuoc_CellDoubleClick;
            int rowIndex = this.grdToaThuoc.Rows.Add(1);
            var row = this.grdToaThuoc.Rows[rowIndex];

        }

        private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdToaThuoc.CurrentCell;
            if (currentCell != null)
            {
                currentRowIndex = currentCell.RowIndex;
                selectedThuoc = listThuoc[currentRowIndex];
                LoadThongTinThuocToForm(selectedThuoc);
                txtMaThuoc.ReadOnly = true;
                btnUpdate.Enabled = true;
                btnLuu.Enabled = false;
            }
            else
            {
                currentRowIndex = -1;
            }

        }
        private void ResetFormThongTinThuoc()
        {
            txtMaThuoc.Text = string.Empty;
            txtTenThuoc.Text = string.Empty;
            txtSoTT.Text = string.Empty;
            txtTenThanhPhanThuoc.Text = string.Empty;
            cbbDonViTinh.SelectedIndex = 0;
            cbBaoHiem.Checked = false;
            txtSttMHTKQDT.Text = string.Empty;
            txtDonVi.Text = string.Empty;
            dtpNgayHieuLuc.Value = DateTime.Now;
            txtPhanNhom.Text = string.Empty;
            txtGiaDNBanVAT.Text = string.Empty;
            txtGiaThucBan.Text = string.Empty;
            txtHamLuong.Text = string.Empty;
            txtSoDangKy.Text = string.Empty;
            txtDangBaoChe.Text = string.Empty;
            txtNhaSanXuat.Text = string.Empty;
            txtQuocGia.Text = string.Empty;
            cbHoatDong.Checked = false;
            txtNhomThuoc.Text = string.Empty;
            txtHeSoAnToan.Text = string.Empty;
        }
        private void LoadThongTinThuocToForm(ThongTinThuoc selectedThuoc)
        {
            txtMaThuoc.Text = selectedThuoc.MaThuocYTe;
            txtTenThuoc.Text = selectedThuoc.MedicineName;
            txtSoTT.Text = selectedThuoc.STTTheoDMTCuaBYT;
            txtTenThanhPhanThuoc.Text = selectedThuoc.TenThanhPhanThuoc;
            cbbDonViTinh.SelectedValue = selectedThuoc.DonViTinh;
            cbBaoHiem.Checked = selectedThuoc.BaoHiem;

            dtpNgayHieuLuc.Value = selectedThuoc.NgayHieuLuc.Trim() == "" ? DateTime.Now : DateTime.Parse(selectedThuoc.NgayHieuLuc);
            ckbChonNgayHieuLuc.Checked = selectedThuoc.NgayHieuLuc.Trim() != "";
            txtSttMHTKQDT.Text = selectedThuoc.SttMaHoaTheoKQDTSoQDStt;
            txtDonVi.Text = selectedThuoc.TenDonViSYT_BV;
            txtPhanNhom.Text = selectedThuoc.PhanNhomTheoTCHTVaTCCN;
            cboCachDung.SelectedValue = selectedThuoc.CachUong;

            txtGiaDNBanVAT.Text = selectedThuoc.GiaDNBanVAT.ToString();
            txtGiaThucBan.Text = selectedThuoc.GiaThucBan.ToString();
            txtHamLuong.Text = selectedThuoc.HamLuong;
            txtSoDangKy.Text = selectedThuoc.SoDKHoacGPKD;
            txtDangBaoChe.Text = selectedThuoc.DangBaoCheDuongUong;
            txtNhaSanXuat.Text = selectedThuoc.NhaSanXuat;
            txtQuocGia.Text = selectedThuoc.QuocGia;
            cbHoatDong.Checked = selectedThuoc.HoatDong;
            txtHeSoAnToan.Text = selectedThuoc.HeSoAnToan.ToString();
            txtNhomThuoc.Text = selectedThuoc.NhomThuoc;
        }
        private ThongTinThuoc BuildThongTinThuoc(bool isNew)
        {
            ThongTinThuoc thongTinThuoc = new ThongTinThuoc();
            if (isNew)
            {
                thongTinThuoc.MedicineID = string.Empty;
                thongTinThuoc.CreatedBy = clsSystemConfig.UserName;
            }
            else
            {
                thongTinThuoc.MedicineID = selectedThuoc.MedicineID;
                thongTinThuoc.CreatedBy = string.Empty;
            }
            thongTinThuoc.MaThuocYTe = txtMaThuoc.Text;
            thongTinThuoc.MedicineName = txtTenThuoc.Text;
            thongTinThuoc.STTTheoDMTCuaBYT = txtSoTT.Text;
            thongTinThuoc.TenThanhPhanThuoc = txtTenThanhPhanThuoc.Text;
            thongTinThuoc.DonViTinh = (int)cbbDonViTinh.SelectedValue;
            thongTinThuoc.BaoHiem = cbBaoHiem.Checked;


            thongTinThuoc.GiaDNMua = decimal.Parse("0");
            thongTinThuoc.GiaDNMuaVAT = decimal.Parse("0");
            thongTinThuoc.GiaThucMua = decimal.Parse("0");
            thongTinThuoc.GiaDNBan = decimal.Parse("0");
            thongTinThuoc.GiaDNBanVAT = decimal.Parse("0");
            thongTinThuoc.GiaThucBan = decimal.Parse("0");

            thongTinThuoc.HamLuong = txtHamLuong.Text;
            thongTinThuoc.SoDKHoacGPKD = txtSoDangKy.Text;
            thongTinThuoc.DangBaoCheDuongUong = txtDangBaoChe.Text;
            thongTinThuoc.NhaSanXuat = txtNhaSanXuat.Text;
            thongTinThuoc.QuocGia = txtQuocGia.Text;
            thongTinThuoc.HoatDong = cbHoatDong.Checked;
            thongTinThuoc.LastUpdatedBy = clsSystemConfig.UserName;
            thongTinThuoc.HeSoAnToan = int.Parse(txtHeSoAnToan.Text);
            thongTinThuoc.NhomThuoc = txtNhomThuoc.Text;
            thongTinThuoc.CachUong = Int16.Parse( cboCachDung.SelectedValue.ToString());

            thongTinThuoc.SttMaHoaTheoKQDTSoQDStt = txtSttMHTKQDT.Text;
            thongTinThuoc.PhanNhomTheoTCHTVaTCCN = txtPhanNhom.Text;
            thongTinThuoc.TenDonViSYT_BV = txtDonVi.Text;
            thongTinThuoc.NgayHieuLuc = ckbChonNgayHieuLuc.Checked ? dtpNgayHieuLuc.Value.ToString("yyyy-MM-dd") : string.Empty;

            return thongTinThuoc;
        }
        void cellDateTimePickerValueChanged(object sender, EventArgs e)
        {
            grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Value.ToString("dd-MM-yyyy");//convert the date as per your format
            cellDateTimePicker.Visible = false;
        }


        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (!CheckThongTinGiaThuoc())
            //{
            //    MessageBox.Show("Thông tin giá thuốc không hợp lệ");
            //    return;
            //}
            //if (!CheckThongTinGiaThuocVAT())
            //{
            //    MessageBox.Show("Thông tin giá thuốc VAT không hợp lệ");
            //    return;
            //}
            if (!CheckThongTinHeSoAnToan())
            {
                MessageBox.Show("Hệ số an toàn không hợp lệ");
                return;
            }
            ThongTinThuoc tttCapNhat = BuildThongTinThuoc(false);
            //if (_quanLyThuocDao.CheckThuocExist(tttCapNhat.MaThuocYTe, tttCapNhat.BaoHiem) == 1)
            //{
            //    MessageBox.Show("Mã thuốc đã tồn tại. Vui lòng chọn mã khác");
            //    return;
            //}
            if (string.IsNullOrEmpty(tttCapNhat.MaThuocYTe))
            {
                MessageBox.Show("Vui lòng nhập thông tin thuốc");
                return;
            }

            if (_quanLyThuocDao.LuuCapNhatThongTinThuoc(tttCapNhat))
            {
                LoadDanhMucThuoc(txtsMaThuoc.Text, txtsTenThuoc.Text);
                ResetFormThongTinThuoc();
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Không thể cập nhật thông tin thuốc");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaThuoc.ReadOnly = false;
            btnUpdate.Enabled = false;
            btnLuu.Enabled = true;
            cbHoatDong.Checked = true;
            ResetFormThongTinThuoc();
        }
        private bool CheckThongTinGiaThuoc()
        {
            bool result = true;
            //try
            //{
            //    decimal giaDNMua = decimal.Parse("0");
            //    decimal giaDNMuaVAT = decimal.Parse(txtGiaDNMuaVAT.Text);
            //    decimal giaThucMua = decimal.Parse(txtGiaThucMua.Text);
            //    decimal giaDNBan = decimal.Parse(txtGiaDNBan.Text);
            //    decimal giaDNBanVAT = decimal.Parse(txtGiaDNBanVAT.Text);
            //    decimal giaThucBan = decimal.Parse(txtGiaThucBan.Text);
            //    if ((giaDNMua < 0) || (giaDNMuaVAT < 0) || (giaThucMua < 0) || (giaDNBan < 0) || (giaDNBanVAT < 0) || (giaThucBan < 0))
            //    {
            //        result = false;
            //    }
            //}
            //catch
            //{
            //    result = false;
            //}
            return result;
        }

        private bool CheckThongTinGiaThuocVAT()
        {
            bool result = true;
            //try
            //{
            //    decimal giaDNMua = decimal.Parse(txtGiaDNMua.Text);
            //    decimal giaDNMuaVAT = decimal.Parse(txtGiaDNMuaVAT.Text);
            //    decimal giaDNBan = decimal.Parse(txtGiaDNBan.Text);
            //    decimal giaDNBanVAT = decimal.Parse(txtGiaDNBanVAT.Text);
            //    if (giaDNMua > giaDNMuaVAT || giaDNBan > giaDNBanVAT)
            //    {
            //        result = false;
            //    }
            //}
            //catch
            //{
            //    result = false;
            //}
            return result;
        }

        private bool CheckThongTinHeSoAnToan()
        {
            bool result = true;
            try
            {
                int heSoAnToan = int.Parse(txtHeSoAnToan.Text);
                if (heSoAnToan <= 0)
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //if (!CheckThongTinGiaThuoc())
            //{
            //    MessageBox.Show("Thông tin giá thuốc không hợp lệ");
            //    return;
            //}
            //if (!CheckThongTinGiaThuocVAT())
            //{
            //    MessageBox.Show("Thông tin giá thuốc VAT không hợp lệ");
            //    return;
            //}
            if (!CheckThongTinHeSoAnToan())
            {
                MessageBox.Show("Hệ số an toàn không hợp lệ");
                return;
            }
            ThongTinThuoc tttCapNhat = BuildThongTinThuoc(true);
            if (_quanLyThuocDao.CheckThuocExist(tttCapNhat.MaThuocYTe, tttCapNhat.BaoHiem) == 1)
            {
                MessageBox.Show("Mã thuốc đã tồn tại. Vui lòng chọn mã khác");
                return;
            }
            if (string.IsNullOrEmpty(tttCapNhat.MaThuocYTe))
            {
                MessageBox.Show("Vui lòng nhập thông tin thuốc");
                return;
            }

            if (_quanLyThuocDao.LuuCapNhatThongTinThuoc(tttCapNhat))
            {
                LoadDanhMucThuoc(txtsMaThuoc.Text, txtsTenThuoc.Text);
                ResetFormThongTinThuoc();
                MessageBox.Show("Lưu thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi trong khi lưu");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDanhMucThuoc(txtsMaThuoc.Text, txtsTenThuoc.Text);
        }

        private void ckbChonNgayHieuLuc_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbChonNgayHieuLuc.Checked)
            {
                dtpNgayHieuLuc.Enabled = true;
            }
            else
            {
                dtpNgayHieuLuc.Enabled = false;
            }
        }

        private void txtsMaThuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoadDanhMucThuoc(txtsMaThuoc.Text, txtsTenThuoc.Text);
            }
        }

        private void txtsTenThuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoadDanhMucThuoc(txtsMaThuoc.Text, txtsTenThuoc.Text);
            }
        }

        private void txtsMaThuoc_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtsTenThuoc_Enter(object sender, EventArgs e)
        {
           
        }


    }
}