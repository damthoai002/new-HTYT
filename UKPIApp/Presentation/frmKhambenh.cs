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
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using UKPI.Controls;
namespace UKPI.Presentation
{
    public partial class frmKhambenh : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmKhambenh));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly ChotTonKhoDao _chotTonKhoDao = new ChotTonKhoDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();
        private List<ThongTinGiaoDich> listCurrentTransactions = new List<ThongTinGiaoDich>();

        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        System.Windows.Forms.TextBox autoText;
        DataGridViewCell currentCell;
        //private ComboBox cellComboBox;
        private int _checkRowsCount = 0;

        // Declare constants
        private const string FieldCheck = "colCheck";
        private const String Check = "CHECK";
        private const String ValueTrue = "Y";
        private const String ValueFalse = "N";


        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion



        public frmKhambenh()
        {

            InitializeComponent();

            //clsTitleManager.InitTitle(this);

            GetParam();
            SetDefauldValue();
            this.Text = "KHÁM BỆNH";
            if (_chotTonKhoDao.CheckChotTonDangHoatDong(cbbPhongKham.SelectedValue.ToString()) > 0)
            {
                DialogResult result = MessageBox.Show("Kho đang được chốt tồn. Vui lòng thực hiện sau", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSearch.Enabled = false;
            }
            else
            {
                btnSearch.Enabled = true;
            }
        }

        void grdStores_Sorted(object sender, EventArgs e)
        {
            //this.ProcessDataRow();
        }

        private void GetParam()
        {
        }

        private void SetDefauldValue()
        {
            BindPhongKham();
            BindGioiTinh();
            BindBoPhan();
            BindKhuVuc();
            BindNhomBenh();
            BindMaICD();
            BuildGridViewRow();
            btnLuuIn.Enabled = false;

            //cbbMaICD.DropDownStyle = ComboBoxStyle.DropDown;
            //cbbMaICD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            //cbbMaICD.AutoCompleteSource = AutoCompleteSource.ListItems;
         

            cbbNhomBenh.DropDownStyle = ComboBoxStyle.DropDown;
            cbbNhomBenh.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbbNhomBenh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
            txtTienKhamBenh.Text = System.Configuration.ConfigurationManager.AppSettings["GiaKhamBenh"];
        }
        private void LoadThongTinBenhNhan()
        {
            ThongTinBenhNhan ttBenhNhan = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            txtBenhNhan.Text = ttBenhNhan.FullName;
            txtMaNhanVien.Text = ttBenhNhan.EmployeeID;
            txtMaBHYT.Text = ttBenhNhan.MaBHYT;
            txtNamSinh.Text = ttBenhNhan.NamSinh.ToString();
            txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
            cbbBoPhan.SelectedValue = ttBenhNhan.BoPhan;
            cbbGioiTinh.SelectedText = ttBenhNhan.GioiTinh;
            cbbKhuVuc.SelectedText = ttBenhNhan.KhuVuc;
        }

        private void txtMaNhanVien_MouseLeave(object sender, EventArgs e)
        {
            BindBenhNhanInfo();
        }

        private void BindBenhNhanInfo()
        {
            string maNhanVien = txtMaNhanVien.Text;
            if (!string.IsNullOrEmpty(maNhanVien))
            {
                List<ThongTinBenhNhan> listBenhNhan = _thongTinKhamBenhDao.SearchThongTinBenhNhan(maNhanVien, string.Empty);
                if (listBenhNhan != null && listBenhNhan.Count == 1)
                {
                    txtBenhNhan.BackColor = Color.White;
                    ThongTinBenhNhan ttbn = listBenhNhan[0];
                    txtBenhNhan.Text = ttbn.FullName;
                    txtMaNhanVien.Text = ttbn.EmployeeID;
                    txtMaBHYT.Text = ttbn.MaBHYT;
                    txtNamSinh.Text = ttbn.NamSinh.ToString();
                    txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
                    List<GioiTinh> listGoiTinh = _shareEntityDao.LoadGioiTinh();
                    List<BoPhan> listBoPhan = _shareEntityDao.LoadDanhSachBoPhan();
                    List<KhuVuc> listKhuVuc = _shareEntityDao.LoadDanhSachKhuVuc();
                    cbbBoPhan.SelectedIndex = listBoPhan.FindIndex(c => c.MaBoPhan == ttbn.BoPhan);
                    cbbGioiTinh.SelectedIndex = listGoiTinh.FindIndex(c => c.Name == ttbn.GioiTinh);
                    cbbKhuVuc.SelectedIndex = listKhuVuc.FindIndex(c => c.TenKhuVuc == ttbn.KhuVuc);
                }
                else
                {
                    /*
                   // txtBenhNhan.BackColor = Color.Red;
                    txtBenhNhan.Text = string.Empty;
                    txtMaNhanVien.Text = string.Empty;
                    txtMaBHYT.Text = string.Empty;
                    txtNamSinh.Text = string.Empty;
                    txtCongTy.Text = string.Empty;
                    cbbBoPhan.SelectedIndex = -1;
                    cbbGioiTinh.SelectedIndex = -1;
                    cbbKhuVuc.SelectedIndex = -1;
                     */
                }
            }
            else
            {
                //txtBenhNhan.BackColor = Color.Red;
                /*
                txtBenhNhan.Text = string.Empty;
                txtMaNhanVien.Text = string.Empty;
                txtMaBHYT.Text = string.Empty;
                txtNamSinh.Text = string.Empty;
                txtCongTy.Text = string.Empty;
                cbbBoPhan.SelectedIndex = -1;
                cbbGioiTinh.SelectedIndex = -1;
                cbbKhuVuc.SelectedIndex = -1;
                 */
            }
        }
        public void SetMaNhanVien(string maNhanVien)
        {
            txtMaNhanVien.Text = maNhanVien;
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
        public void SetCongTy(string congTy)
        {
            txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
        }
        public void SetGioiTinh(string gioiTinh)
        {
            List<GioiTinh> listGoiTinh = _shareEntityDao.LoadGioiTinh();
            cbbGioiTinh.SelectedIndex = listGoiTinh.FindIndex(c => c.Name == gioiTinh);

        }
        public void SetBoPhan(string boPhan)
        {
            List<BoPhan> listBoPhan = _shareEntityDao.LoadDanhSachBoPhan();
            cbbBoPhan.SelectedIndex = listBoPhan.FindIndex(c => c.TenBoPhan == boPhan);
        }
        public void SetKhuVuc(string khuVuc)
        {
            List<KhuVuc> listKhuVuc = _shareEntityDao.LoadDanhSachKhuVuc();
            cbbKhuVuc.SelectedIndex = listKhuVuc.FindIndex(c => c.TenKhuVuc == khuVuc);
        }
        private void BindPhongKham()
        {

            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
            cbbPhongKham.DataSource = listPhongKham;
            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            int currentIndex = listPhongKham.FindIndex(a => a.RoomID == currentKho);
            cbbPhongKham.SelectedIndex = currentIndex;



        }
        private void BindGioiTinh()
        {
            cbbGioiTinh.DataSource = _shareEntityDao.LoadGioiTinh();
        }
        private void BindBoPhan()
        {
            cbbBoPhan.DataSource = _shareEntityDao.LoadDanhSachBoPhan();
        }
        private void BindKhuVuc()
        {
            cbbKhuVuc.DataSource = _shareEntityDao.LoadDanhSachKhuVuc();
        }
        private void BindNhomBenh()
        {
            cbbNhomBenh.DataSource = _shareEntityDao.LoadDanhSachNhomBenh();
        }
        private void BindMaICD()
        {

            cbbMaICD.ValueMember = "Ma";
            cbbMaICD.DisplayMember = "DienGiai";
            cbbMaICD.DataSource = _shareEntityDao.LoadDanhSachMaICD();
        }
        private void BindGroup()
        {

        }
        private void BuildGridViewRow()
        {

            List<ThongTinThuocKhamBenh> lstThuoc = _shareEntityDao.LoadThongTinThuocForKhamBenh(cbbPhongKham.SelectedValue.ToString());

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Width = 60;

            grdToaThuoc.Columns.Add(checkBoxColumn);

            DataGridViewComboBoxColumn col1 = new DataGridViewComboBoxColumn();
            col1.AutoComplete = true;
            col1.Width = 160;
            col1.HeaderText = "Tên Thuốc";
            col1.DataSource = lstThuoc;
            col1.DisplayMember = "MedicineName";
            col1.ValueMember = "MedicineID";
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(col1);

            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.Width = 70;
            col.HeaderText = "Mã thuốc";
            col.DataSource = lstThuoc;
            col.DisplayMember = "MaThuocYTeHienThi";
            col.ValueMember = "MedicineID";
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(col);

            DataGridViewCheckBoxColumn baoHiemColumn = new DataGridViewCheckBoxColumn();
            baoHiemColumn.Width = 80;
            baoHiemColumn.HeaderText = "Thuốc BH";
            baoHiemColumn.ReadOnly = true;
            baoHiemColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(baoHiemColumn);


            DataGridViewTextBoxColumn soLuongTonKho = new DataGridViewTextBoxColumn();
            soLuongTonKho.Width = 80;
            soLuongTonKho.HeaderText = "Số lượng tồn kho";
            soLuongTonKho.ReadOnly = true;
            soLuongTonKho.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(soLuongTonKho);


            DataGridViewTextBoxColumn donViTinhColumn = new DataGridViewTextBoxColumn();
            donViTinhColumn.Width = 80;
            donViTinhColumn.HeaderText = "Đơn vị tính";
            donViTinhColumn.ReadOnly = true;
            donViTinhColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(donViTinhColumn);

            DataGridViewTextBoxColumn hamLuongColumn = new DataGridViewTextBoxColumn();
            hamLuongColumn.Width = 80;
            hamLuongColumn.HeaderText = "Hàm lượng";
            hamLuongColumn.ReadOnly = false;
            hamLuongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(hamLuongColumn);

            DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
            soLuongColumn.Width = 80;
            soLuongColumn.HeaderText = "Số lượng";
            soLuongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(soLuongColumn);

            DataGridViewTextBoxColumn giaColumn = new DataGridViewTextBoxColumn();
            giaColumn.Width = 90;
            giaColumn.HeaderText = "Giá nhập có VAT";
            giaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(giaColumn);


            DataGridViewTextBoxColumn giaTTBHTColumn = new DataGridViewTextBoxColumn();
            giaTTBHTColumn.Width = 90;
            giaTTBHTColumn.HeaderText = "Giá thanh toán BHYT";
            giaTTBHTColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(giaTTBHTColumn);


            DataGridViewTextBoxColumn cachUongColumn = new DataGridViewTextBoxColumn();
            cachUongColumn.Width = 80;
            cachUongColumn.HeaderText = "Cách dùng";
            cachUongColumn.ReadOnly = false;
            cachUongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(cachUongColumn);


            DataGridViewTextBoxColumn cachDungColumn = new DataGridViewTextBoxColumn();
            cachDungColumn.Width = 230;
            cachDungColumn.HeaderText = "Chi tiết";
            cachDungColumn.ReadOnly = false;
            cachDungColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(cachDungColumn);

            DataGridViewTextBoxColumn thanhTienColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn.Width = 80;
            thanhTienColumn.HeaderText = "Thành tiến ";
            thanhTienColumn.ReadOnly = true;
            thanhTienColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(thanhTienColumn);

            DataGridViewTextBoxColumn thanhTienBHYTColumn = new DataGridViewTextBoxColumn();
            thanhTienBHYTColumn.Width = 80;
            thanhTienBHYTColumn.HeaderText = "Thành tiến BHYT";
            thanhTienBHYTColumn.ReadOnly = true;
            thanhTienBHYTColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(thanhTienBHYTColumn);


            DataGridViewTextBoxColumn maCSGColumn = new DataGridViewTextBoxColumn();
            maCSGColumn.Width = 0;
            maCSGColumn.Visible = false;
            maCSGColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(maCSGColumn);

            DataGridViewTextBoxColumn LoaiThanhToanSubColumn = new DataGridViewTextBoxColumn();
            LoaiThanhToanSubColumn.Width = 0;
            LoaiThanhToanSubColumn.Visible = false;
            LoaiThanhToanSubColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(LoaiThanhToanSubColumn);

            DataGridViewTextBoxColumn LoaiThanhToanColumn = new DataGridViewTextBoxColumn();
            LoaiThanhToanColumn.Width = 0;
            LoaiThanhToanColumn.Visible = false;
            LoaiThanhToanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(LoaiThanhToanColumn);

            DataGridViewTextBoxColumn DangTrinhBayColumn = new DataGridViewTextBoxColumn();
            DangTrinhBayColumn.Width = 0;
            DangTrinhBayColumn.Visible = false;
            DangTrinhBayColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(DangTrinhBayColumn);

            DataGridViewTextBoxColumn PhanNhomTheoTCHTVaTCCNColumn = new DataGridViewTextBoxColumn();
            PhanNhomTheoTCHTVaTCCNColumn.Width = 0;
            PhanNhomTheoTCHTVaTCCNColumn.Visible = false;
            PhanNhomTheoTCHTVaTCCNColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(PhanNhomTheoTCHTVaTCCNColumn);

            DataGridViewTextBoxColumn NgayHieuLucColumn = new DataGridViewTextBoxColumn();
            NgayHieuLucColumn.Width = 0;
            NgayHieuLucColumn.Visible = false;
            NgayHieuLucColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(NgayHieuLucColumn);

            DataGridViewTextBoxColumn TenDonViSYTBVColumn = new DataGridViewTextBoxColumn();
            TenDonViSYTBVColumn.Width = 0;
            TenDonViSYTBVColumn.Visible = false;
            TenDonViSYTBVColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(TenDonViSYTBVColumn);

            DataGridViewTextBoxColumn SttMaHoaTheoKQDTSoQDSttColumn = new DataGridViewTextBoxColumn();
            SttMaHoaTheoKQDTSoQDSttColumn.Width = 0;
            SttMaHoaTheoKQDTSoQDSttColumn.Visible = false;
            SttMaHoaTheoKQDTSoQDSttColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(SttMaHoaTheoKQDTSoQDSttColumn);

            DataGridViewTextBoxColumn NhomThuocColumn = new DataGridViewTextBoxColumn();
            NhomThuocColumn.Width = 0;
            NhomThuocColumn.Visible = false;
            NhomThuocColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(NhomThuocColumn);

            DataGridViewTextBoxColumn HeSoAnToanColumn = new DataGridViewTextBoxColumn();
            HeSoAnToanColumn.Width = 0;
            HeSoAnToanColumn.Visible = false;
            HeSoAnToanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(HeSoAnToanColumn);

            DataGridViewTextBoxColumn NgayNhapKhoColumn = new DataGridViewTextBoxColumn();
            NgayNhapKhoColumn.Width = 0;
            NgayNhapKhoColumn.Visible = false;
            NgayNhapKhoColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(NgayNhapKhoColumn);

            DataGridViewTextBoxColumn HanSuDungColumn = new DataGridViewTextBoxColumn();
            HanSuDungColumn.Width = 0;
            HanSuDungColumn.Visible = false;
            HanSuDungColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(HanSuDungColumn);

            DataGridViewTextBoxColumn QuocGiaColumn = new DataGridViewTextBoxColumn();
            QuocGiaColumn.Width = 0;
            QuocGiaColumn.Visible = false;
            QuocGiaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(QuocGiaColumn);

            DataGridViewTextBoxColumn NhaSanXuatColumn = new DataGridViewTextBoxColumn();
            NhaSanXuatColumn.Width = 0;
            NhaSanXuatColumn.Visible = false;
            NhaSanXuatColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(NhaSanXuatColumn);

            DataGridViewTextBoxColumn DangBaoCheDuongUongColumn = new DataGridViewTextBoxColumn();
            DangBaoCheDuongUongColumn.Width = 0;
            DangBaoCheDuongUongColumn.Visible = false;
            DangBaoCheDuongUongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(DangBaoCheDuongUongColumn);

            DataGridViewTextBoxColumn SoDKHoacGPKDColumn = new DataGridViewTextBoxColumn();
            SoDKHoacGPKDColumn.Width = 0;
            SoDKHoacGPKDColumn.Visible = false;
            SoDKHoacGPKDColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(SoDKHoacGPKDColumn);

            DataGridViewTextBoxColumn TenThanhPhanThuocColumn = new DataGridViewTextBoxColumn();
            TenThanhPhanThuocColumn.Width = 0;
            TenThanhPhanThuocColumn.Visible = false;
            TenThanhPhanThuocColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(TenThanhPhanThuocColumn);

            DataGridViewTextBoxColumn STTTheoDMTCuaBYTColumn = new DataGridViewTextBoxColumn();
            STTTheoDMTCuaBYTColumn.Width = 0;
            STTTheoDMTCuaBYTColumn.Visible = false;
            STTTheoDMTCuaBYTColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(STTTheoDMTCuaBYTColumn);

            DataGridViewTextBoxColumn DienGiaiColumn = new DataGridViewTextBoxColumn();
            DienGiaiColumn.Width = 0;
            DienGiaiColumn.Visible = false;
            DienGiaiColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(DienGiaiColumn);

            DataGridViewTextBoxColumn TENTHUOCColumn = new DataGridViewTextBoxColumn();
            TENTHUOCColumn.Width = 0;
            TENTHUOCColumn.Visible = false;
            TENTHUOCColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(TENTHUOCColumn);

            grdToaThuoc.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            grdToaThuoc.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            grdToaThuoc.CellValueChanged += grdToaThuoc_CellValueChanged;
            //grdToaThuoc.CellClick += grdToaThuoc_CellDoubleClick;
            int rowIndex = this.grdToaThuoc.Rows.Add(1);
            var row = this.grdToaThuoc.Rows[rowIndex];




        }

        private void chkQuyetDinh_CheckedChanged(object sender, EventArgs e)
        {
            btnQuyetDinh.Enabled = true;
        }

        private void lblTongThanhTien_Click(object sender, EventArgs e)
        {

        }
        public void SetThoiGianNghiPhepStart(DateTime value)
        {
            txtTuNgay.Text = value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]); ;
        }
        public void SetThoiGianNghiPhepEnd(DateTime value)
        {
            txtDenNgay.Text = value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]);
        }
        public void SetSoNgayDuocNghi(int? value)
        {
            txtSndn.Text = value.ToString();
        }
        public void SetLyDo(string value)
        {
            txtLyDo.Text = value;
        }
        public void SetDIenGiai(string value)
        {
            txtDienGiaiQDNghi.Text = value;
        }
        private void btnQuyetDinh_Click(object sender, EventArgs e)
        {
            frmNghiPhep a = new frmNghiPhep();
            a.SetParentForm(this);
            a.Show();
        }

        private void cbbNhomBenh_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChanDoan.Text = this.cbbNhomBenh.GetItemText(this.cbbNhomBenh.SelectedItem);
        }

        private void cbbMaICD_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtDienGiaiICD.Text = cbbMaICD.Text; //this.cbbMaICD.GetItemText(this.cbbMaICD.SelectedValue);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTongThanhTien.Text))
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidDonThuoc"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(txtMaNhanVien.Text) && string.IsNullOrEmpty(txtBenhNhan.Text))
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidThongTinNhanVien"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.Waring"), clsResources.GetMessage("messages.frmKhamBenh.Title"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    btnLuuIn.Enabled = true;
                    btnLuuIn.Enabled = true;
                    grpThongTinKhamBenh.Enabled = false;
                    btnXoaThuoc.Enabled = false;
                    listCurrentTransactions = new List<ThongTinGiaoDich>();
                    ThongTinKhamBenh ttkb = BuildThongTinKhamBenh();

                    if (ttkb != null)
                    {
                        List<ThongTinGiaoDich> listTransaction = _thongTinKhamBenhDao.XacNhanThongTinKhamBenh(ttkb);
                        if (listTransaction != null && listTransaction.Count > 0)
                        {
                            //DialogResult result1 = MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.Success"), clsResources.GetMessage("messages.frmKhamBenh.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            grdToaThuoc.ReadOnly = true;
                            btnXacNhan.Enabled = false;
                            listCurrentTransactions = listTransaction;
                            //MessageBox.Show("Xác nhận thành công");
                            return;
                        }
                        else
                        {
                            //MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.Error"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Không Thể Xác nhận");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {


            if (_thongTinKhamBenhDao.ProcessGiaoDichKhamBenh(listCurrentTransactions))
            {
                MessageBox.Show("Lưu thành công");
                grdToaThuoc.ReadOnly = true;
                btnLuuIn.Enabled = false;

                string maKhamBenh = _thongTinKhamBenhDao.GetMaKhamBenhForPrint(listCurrentTransactions[0].MaTransaction);

                _thongTinKhamBenhDao.CapNhatTrangThaiWareHouse(maKhamBenh, 1, clsCommon.WareHouseType.LUUIN);

                if (txtMaBHYT.Text != "")
                {
                    ReportInDonThuocFull frmChild = new ReportInDonThuocFull(maKhamBenh);
                    frmChild.Show();
                }


                ReportInDonThuocBH frmChildBh = new ReportInDonThuocBH(maKhamBenh);
                frmChildBh.Show();

                return;
            }
            else
            {
                MessageBox.Show("Có lỗi trong khi lưu");
                return;
            }



        }

        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {
            this.quyetDinhNghiPhep = qd;
        }

        private ThongTinKhamBenh BuildThongTinKhamBenh()
        {
            List<string> listmaThuoc = new List<string>();
            Dictionary<CustomKey, string> dic = _shareEntityDao.BuildTuDienThuoc();
            ThongTinKhamBenh thongTinKhamBenh = new ThongTinKhamBenh();
            thongTinKhamBenh.MaKhamBenh = _thongTinKhamBenhDao.GenerateNewMaKhamKhamBenh();
            thongTinKhamBenh.PhongKhamBenh = cbbPhongKham.SelectedValue.ToString();
            thongTinKhamBenh.NgayKhamBenh = dtpNgayKham.Value;
            thongTinKhamBenh.BenhNhan = txtBenhNhan.Text;
            thongTinKhamBenh.MaBenhNhan = txtMaNhanVien.Text;
            thongTinKhamBenh.MaBHYT = txtMaBHYT.Text;
            thongTinKhamBenh.GioiTinh = cbbGioiTinh.GetItemText(cbbGioiTinh.SelectedItem);
            thongTinKhamBenh.NamSinh = txtNamSinh.Text;
            thongTinKhamBenh.BoPhan = cbbBoPhan.GetItemText(cbbBoPhan.SelectedItem);
            thongTinKhamBenh.CongTy = txtCongTy.Text;
            thongTinKhamBenh.KhuVuc = cbbKhuVuc.GetItemText(cbbKhuVuc.SelectedItem);
            thongTinKhamBenh.NhomBenh = cbbNhomBenh.GetItemText(cbbNhomBenh.SelectedItem);
            thongTinKhamBenh.ChuanDoan = txtChanDoan.Text;
            thongTinKhamBenh.QuyetDinhNghi = chkQuyetDinh.Checked;
            thongTinKhamBenh.MaICD = cbbMaICD.Text;
            thongTinKhamBenh.DienGiaiICD = txtDienGiaiICD.Text;
            thongTinKhamBenh.TongTien = txtTongThanhTien.Text;
            thongTinKhamBenh.TongTienBangChu = txtTongTienBangChu.Text;
            if (quyetDinhNghiPhep != null)
                thongTinKhamBenh.QuyetDinhNghiPhep = quyetDinhNghiPhep;
            else
            {
                quyetDinhNghiPhep = new QuyetDinhNghiPhep();
                quyetDinhNghiPhep.TuNgay = null;
                quyetDinhNghiPhep.DenNgay = null;
                quyetDinhNghiPhep.LyDo = string.Empty;
                quyetDinhNghiPhep.LyDoChiTiet = string.Empty;
                quyetDinhNghiPhep.DienGiai = string.Empty;
                quyetDinhNghiPhep.SoNgayNghi = null;
                quyetDinhNghiPhep.ChuThich = string.Empty;
                thongTinKhamBenh.QuyetDinhNghiPhep = quyetDinhNghiPhep;
            }

            if (grdToaThuoc.Rows.Count > 0)
            {
                List<ThongTinDonThuocKhamBenh> listDonThuoc = new List<ThongTinDonThuocKhamBenh>();
                for (int i = 0; i < grdToaThuoc.Rows.Count; i++)
                {

                    ThongTinDonThuocKhamBenh thongTinDonThuoc = new ThongTinDonThuocKhamBenh();
                    if ((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue == "")
                        continue;
                    thongTinDonThuoc.TenThuoc = (string)grdToaThuoc.Rows[i].Cells[1].FormattedValue;
                    thongTinDonThuoc.MaThuoc = (string)grdToaThuoc.Rows[i].Cells[2].FormattedValue;
                    thongTinDonThuoc.ThuocBH = (bool)grdToaThuoc.Rows[i].Cells[3].FormattedValue;
                    thongTinDonThuoc.DonViTinh = (string)grdToaThuoc.Rows[i].Cells[5].FormattedValue;
                    thongTinDonThuoc.HamLuong = (string)grdToaThuoc.Rows[i].Cells[6].FormattedValue;
                    thongTinDonThuoc.MaChinhSachGia = (string)grdToaThuoc.Rows[i].Cells[14].FormattedValue;

                    CustomKey ck = new CustomKey(thongTinDonThuoc.MaThuoc, (bool)grdToaThuoc.Rows[i].Cells[3].FormattedValue);
                    thongTinDonThuoc.MaThuoc = dic[ck];
                    try
                    {
                        long checkSoluong = long.Parse((string)grdToaThuoc.Rows[i].Cells[7].FormattedValue);
                        thongTinDonThuoc.SoLuong = checkSoluong;
                        //messages.frmKhamBenh.CheckSoLuongTrongKho
                        if (checkSoluong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidSoLuong") + " với thuốc " + thongTinDonThuoc.TenThuoc, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                        if (_thongTinKhamBenhDao.CheckSoLuongThuocTrongKho(thongTinDonThuoc.MaThuoc, checkSoluong, cbbPhongKham.SelectedValue.ToString()) < 0)
                        {

                            MessageBox.Show("Thuốc " + thongTinDonThuoc.TenThuoc + clsResources.GetMessage("messages.frmKhamBenh.CheckSoLuongTrongKho"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidSoLuong") + " với thuốc " + thongTinDonThuoc.TenThuoc, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }


                    string gia = (string)grdToaThuoc.Rows[i].Cells[8].FormattedValue;
                    string giattbhyt = (string)grdToaThuoc.Rows[i].Cells[9].FormattedValue;


                    decimal currentGia = 0;
                    try
                    {
                        currentGia = decimal.Parse(gia);
                        thongTinDonThuoc.Gia = currentGia;
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidGia") + " với thuốc " + thongTinDonThuoc.TenThuoc, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    decimal currentGiaTTBHYT = 0;
                    try
                    {
                        currentGiaTTBHYT = decimal.Parse(giattbhyt);
                        thongTinDonThuoc.GiaTTBHYT = currentGiaTTBHYT;
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidGia") + " với thuốc " + thongTinDonThuoc.TenThuoc, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    thongTinDonThuoc.CachUong = (string)grdToaThuoc.Rows[i].Cells[10].FormattedValue;
                    thongTinDonThuoc.CachUongChiTiet = (string)grdToaThuoc.Rows[i].Cells[11].FormattedValue;


                    //thongTinDonThuoc.ThanhTien = (decimal)grdToaThuoc.Rows[i].Cells[9].FormattedValue;
                    string thanhTien = (string)grdToaThuoc.Rows[i].Cells[12].FormattedValue;

                    string thanhTienTTBHYT = (string)grdToaThuoc.Rows[i].Cells[13].FormattedValue;

                    thongTinDonThuoc.ThanhTien = decimal.Parse(thanhTien);
                    thongTinDonThuoc.ThanhTienTTBHYT = decimal.Parse(thanhTienTTBHYT);

                    thongTinDonThuoc.MaKhamBenh = thongTinKhamBenh.MaKhamBenh;
                    //if ((bool)grdToaThuoc.Rows[i].Cells[0].FormattedValue)
                    //{
                    //    grdToaThuoc.Rows.RemoveAt(i);
                    //}
                    if (!listmaThuoc.Contains(thongTinDonThuoc.MaThuoc))
                    {
                        listmaThuoc.Add(thongTinDonThuoc.MaThuoc);
                    }
                    else
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckTrungLapThuoc") + " với thuốc " + thongTinDonThuoc.TenThuoc, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    listDonThuoc.Add(thongTinDonThuoc);
                }
                thongTinKhamBenh.ThongTinToaThuoc = listDonThuoc;
            }

            if (grdToaThuoc.Rows.Count > 0)
            {
                List<WareHouse> listWareHouse = new List<WareHouse>();
                for (int i = 0; i < grdToaThuoc.Rows.Count; i++)
                {
                    WareHouse wh = new WareHouse();

                    if ((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue == "")
                        continue;
                    wh.MedicineName = (string)grdToaThuoc.Rows[i].Cells[33].FormattedValue;
                    wh.MaThuocHienThi = (string)grdToaThuoc.Rows[i].Cells[2].FormattedValue;
                    wh.MaThuocHeThong = (string)grdToaThuoc.Rows[i].Cells[2].Value.ToString();
                    wh.ThuocBaoHiem = (bool)grdToaThuoc.Rows[i].Cells[3].FormattedValue;
                    wh.DonViTinh = (string)grdToaThuoc.Rows[i].Cells[5].FormattedValue;
                    wh.HamLuong = (string)grdToaThuoc.Rows[i].Cells[6].FormattedValue;
                    try
                    {
                        long checkSoluong = long.Parse((string)grdToaThuoc.Rows[i].Cells[7].FormattedValue);
                        wh.SoLuongNgoaiTru = checkSoluong.ToString();

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidSoLuong") + " với thuốc " + wh.MedicineName, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    string gia = (string)grdToaThuoc.Rows[i].Cells[8].FormattedValue;
                    string giattbhyt = (string)grdToaThuoc.Rows[i].Cells[9].FormattedValue;


                    decimal currentGia = 0;
                    try
                    {
                        currentGia = decimal.Parse(gia);
                        wh.GiaMuaVao = currentGia;
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidGia") + " với thuốc " + wh.MedicineName, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    decimal currentGiaTTBHYT = 0;
                    try
                    {
                        currentGiaTTBHYT = decimal.Parse(giattbhyt);
                        wh.GiaThanhToanBHYT = currentGiaTTBHYT;
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidGia") + " với thuốc " + wh.MedicineName, clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    wh.CachUong = (string)grdToaThuoc.Rows[i].Cells[10].FormattedValue;
                    string thanhTien = (string)grdToaThuoc.Rows[i].Cells[12].FormattedValue;
                    string thanhTienTTBHYT = (string)grdToaThuoc.Rows[i].Cells[13].FormattedValue;
                    wh.ThanhTien = decimal.Parse(thanhTienTTBHYT);
                    wh.MaLienHe = thongTinKhamBenh.MaKhamBenh;
                   // wh.HamLuong = (string)grdToaThuoc.Rows[i].Cells[8].FormattedValue;
                    wh.MaDienGiaiWarehouse = clsCommon.WareHouseType.XUPH;

                    wh.LoaiThanhToan_Sub = (string)grdToaThuoc.Rows[i].Cells[15].FormattedValue;
                    wh.LoaiThanhToan = (string)grdToaThuoc.Rows[i].Cells[16].FormattedValue;
                    wh.DangTrinhBay = (string)grdToaThuoc.Rows[i].Cells[17].FormattedValue;
                    wh.PhanNhomTheoTCHTVaTCCN = (string)grdToaThuoc.Rows[i].Cells[18].FormattedValue;
                    wh.NgayHieuLuc = (string)grdToaThuoc.Rows[i].Cells[19].FormattedValue;
                    wh.TenDonViSYT_BV = (string)grdToaThuoc.Rows[i].Cells[20].FormattedValue;
                    wh.SttMaHoaTheoKQDTSoQDStt = (string)grdToaThuoc.Rows[i].Cells[21].FormattedValue;
                    wh.NhomThuoc = (string)grdToaThuoc.Rows[i].Cells[22].FormattedValue;
                    wh.HanSuDung = DateTime.Parse(grdToaThuoc.Rows[i].Cells[25].FormattedValue.ToString());
                    wh.QuocGia = (string)grdToaThuoc.Rows[i].Cells[26].FormattedValue;
                    wh.NhaSanXuat = (string)grdToaThuoc.Rows[i].Cells[27].FormattedValue;
                    wh.DangBaoCheDuongUong = (string)grdToaThuoc.Rows[i].Cells[28].FormattedValue;
                    wh.SoDKHoacGPKD = (string)grdToaThuoc.Rows[i].Cells[29].FormattedValue;
                    wh.TenThanhPhanThuoc = (string)grdToaThuoc.Rows[i].Cells[30].FormattedValue;
                    wh.STTTheoDMTCuaBYT = (string)grdToaThuoc.Rows[i].Cells[31].FormattedValue;
                    wh.NgayTao = DateTime.Now.ToString("yyyyMMddhhmmss");
                    wh.NguoiTao = clsSystemConfig.UserName;
                    wh.MaKho = cbbPhongKham.SelectedValue.ToString();
                    wh.PhongKham = cbbPhongKham.Text;
                    wh.GhiChu = (string)grdToaThuoc.Rows[i].Cells[32].FormattedValue;
                    wh.IsActive = true;
                    wh.TrangThai = 0;
                    wh.TrangThaiDienGiai = clsCommon.WareHouseType.XACNHAN;

                    listWareHouse.Add(wh);
                }
                thongTinKhamBenh.lstWareHouse = listWareHouse;
            }


            return thongTinKhamBenh;
        }

        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtMaNhanVien.Text))
            //{
            for (int i = grdToaThuoc.Rows.Count - 1; i > 0; i--)
            {
                if ((bool)grdToaThuoc.Rows[i].Cells[0].FormattedValue)
                {
                    grdToaThuoc.Rows.RemoveAt(i);
                    CalculateTotal();
                }
            }
            //}
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();


            //string maKhamBenh = _thongTinKhamBenhDao.GetMaKhamBenhForPrint(listCurrentTransactions[0].MaTransaction);
            //ReportInDonThuocFull frmChild = new ReportInDonThuocFull(maKhamBenh);

            //frmChild.Show();


        }

        private void grdToaThuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdToaThuoc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            currentCell = this.grdToaThuoc.CurrentCell;
            // thay doi so luong thuoc
            if (currentCell != null && currentCell.ColumnIndex == 7)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null && this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString() != "";
                // if (isValidMaThuoc && isValidSoLuongThuoc)
                if (isValidMaThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? int.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;
                        /* if (currentSoLuong <= 0)
                         {
                             MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidSoLuong"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                             return;
                         }
                         //check thuoc trong kho
                         if (isValidSoLuongThuoc)
                         {
                             string maThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString();
                             int soLuongThuocTrongKho = _thongTinKhamBenhDao.CheckSoLuongThuocTrongKho(maThuoc, currentSoLuong, cbbPhongKham.SelectedValue.ToString());
                             if (soLuongThuocTrongKho < 0)
                             {
                                 MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckSoLuongTrongKho"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 return;
                             }
                         }*/
                    }
                    catch
                    {
                        /*
                        MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidSoLuong"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                         */
                        currentSoLuong = 0;
                    }
                }

                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                decimal currentGiaBH = 0;
                try
                {
                    currentGiaBH = this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGiaBH = 0;
                }

                bool isBaoHiem = true;

                try
                {
                    isBaoHiem = this.grdToaThuoc[currentCell.ColumnIndex - 4, currentCell.RowIndex].Value != null ? bool.Parse(this.grdToaThuoc[currentCell.ColumnIndex - 4, currentCell.RowIndex].Value.ToString()) : false;

                }
                catch
                {
                    isBaoHiem = false;
                }

                decimal currentTienThuocBH = isBaoHiem ? currentSoLuong * (currentGiaBH > currentGia ? currentGia : currentGiaBH) : 0;

                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());



                this.grdToaThuoc[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                this.grdToaThuoc[currentCell.ColumnIndex + 6, currentCell.RowIndex].Value = currentTienThuocBH.ToString();

                CalculateTotal();

            }
            //Thay doi gia thuoc
            if (currentCell != null && currentCell.ColumnIndex == 8)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value != null && this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value.ToString() != "";
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                decimal currentGiaBH = 0;
                try
                {
                    currentGiaBH = this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGiaBH = 0;
                }

                bool isBaoHiem = true;

                try
                {
                    isBaoHiem = this.grdToaThuoc[currentCell.ColumnIndex - 5, currentCell.RowIndex].Value != null ? bool.Parse(this.grdToaThuoc[currentCell.ColumnIndex - 5, currentCell.RowIndex].Value.ToString()) : false;

                }
                catch
                {
                    isBaoHiem = false;
                }


                if (isValidMaThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value != null ? int.Parse(this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value.ToString()) : 0;

                    }
                    catch
                    {

                        currentSoLuong = 0;
                    }
                }

                decimal currentTienThuocBH = isBaoHiem ? currentSoLuong * (currentGiaBH > currentGia ? currentGia : currentGiaBH) : 0;
                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdToaThuoc[currentCell.ColumnIndex + 4, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                this.grdToaThuoc[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = currentTienThuocBH.ToString();

                CalculateTotal();

            }

        }

        private void CalculateTotal()
        {
            decimal total = 0;
            decimal totalBH = 0;

            foreach (DataGridViewRow row in grdToaThuoc.Rows)
            {
                if (row.Cells[12].Value != null)
                {
                    total += decimal.Parse(row.Cells[12].Value.ToString());
                }

                if (row.Cells[13].Value != null)
                {
                    totalBH += decimal.Parse(row.Cells[13].Value.ToString());
                }
            }
            decimal tienKham = Decimal.Parse(txtTienKhamBenh.Text); //Decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["GiaKhamBenh"].ToString());
            txtTongThanhTien.Text = Math.Round(total + tienKham, 0).ToString();


            txtTongTienBH.Text = Math.Round(totalBH + tienKham, 0).ToString();
            CreateTextFromNumber();
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 0;
            currentCell = this.grdToaThuoc.CurrentCell;
            if (currentCell != null)
                columnIndex = currentCell.ColumnIndex;
            if (cbm != null)
            {
                // Here we will remove the subscription for selected index changed
                cbm.SelectedIndexChanged -= new EventHandler(cbm_SelectedIndexChanged);
            }

        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //string titleText = grdToaThuoc.Columns[8].HeaderText;
            int columnIndex = 0;
            currentCell = this.grdToaThuoc.CurrentCell;
            if (autoText != null)
            {
                autoText.AutoCompleteCustomSource = null;
            }
            if (currentCell != null)
                columnIndex = currentCell.ColumnIndex;
            // Here try to add subscription for selected index changed event
            string headerText = grdToaThuoc.Columns[columnIndex].HeaderText;


            if (e.Control is ComboBox)
            {
                cbm = (ComboBox)e.Control;

                if (cbm != null)
                {
                    cbm.DropDownStyle = ComboBoxStyle.DropDown;
                    cbm.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cbm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;

                    cbm.SelectedIndexChanged += new EventHandler(cbm_SelectedIndexChanged);
                }
                currentCell = this.grdToaThuoc.CurrentCell;
            }

            if (columnIndex == 10)//headerText == "Cách dùng"
            {
                autoText = e.Control as System.Windows.Forms.TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }

            }
            if (headerText == "Chi tiết")
            {
                autoText = e.Control as System.Windows.Forms.TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection1 = new AutoCompleteStringCollection();
                    addCachDungChiTiet(DataCollection1);
                    autoText.AutoCompleteCustomSource = DataCollection1;
                }
            }



        }

        public void addItems(AutoCompleteStringCollection col)
        {
            List<CachUongThuoc> listCachUongThuoc = _shareEntityDao.LoadThongTinCachUongThuoc();
            for (int i = 0; i < listCachUongThuoc.Count; i++)
            {
                col.Add(listCachUongThuoc[i].CachUong);
            }
        }

        public void addCachDungChiTiet(AutoCompleteStringCollection col)
        {
            List<CachDungChiTiet> listCachDungChiTiet = _shareEntityDao.LoadCachDungChiTiet();
            for (int i = 0; i < listCachDungChiTiet.Count; i++)
            {
                col.Add(listCachDungChiTiet[i].CachDung);
            }
        }

        public void cbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Invoke method if the selection changed event occurs
            BeginInvoke(new MethodInvoker(EndEdit));
        }
        void TinhTienThuoc()
        {
            if (currentCell != null)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null && this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString() != "";
                //if (isValidMaThuoc && isValidSoLuongThuoc)
                if (isValidMaThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? int.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;

                    }
                    catch
                    {
                        //MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidSoLuong"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //return;
                        currentSoLuong = 0;
                    }
                }

                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }
                /*
                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckValidGia"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                */
                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdToaThuoc[12, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
        }

        void EndEdit()
        {
            // Change the content of appropriate cell when selected index changes
            if (cbm != null)
            {

                if (currentCell != null && currentCell.ColumnIndex == 1)
                {
                    ThongTinThuocKhamBenh ttt = cbm.SelectedItem as ThongTinThuocKhamBenh;
                    //DataRowView drv = cbm.SelectedItem as DataRowView;
                    if (ttt != null)
                    {
                        #region 
                        
                      
                        if (currentCell.ColumnIndex == 1)
                        {
                            if (!danhSachThuoc.ContainsKey(currentCell.RowIndex) && !danhSachThuoc.ContainsValue(ttt.MedicineID))
                            {
                                danhSachThuoc.Add(currentCell.RowIndex, ttt.MedicineID);
                            }
                            else if (danhSachThuoc.ContainsKey(currentCell.RowIndex))
                            {
                                danhSachThuoc.Remove(currentCell.RowIndex);
                                danhSachThuoc.Add(currentCell.RowIndex, ttt.MedicineID);
                            }
                            else
                            {
                                MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckTrungLapThuoc1"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            // this.grdToaThuoc[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value = ttt.MedicineName;
                            this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value = ttt.MedicineID;
                            this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = ttt.BaoHiem;
                            this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value = ttt.SoLuong;

                            this.grdToaThuoc[currentCell.ColumnIndex + 4, currentCell.RowIndex].Value = ttt.TenDonViTinh;
                            this.grdToaThuoc[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = ttt.HamLuong;
                            this.grdToaThuoc[currentCell.ColumnIndex + 7, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                            this.grdToaThuoc[currentCell.ColumnIndex + 8, currentCell.RowIndex].Value = ttt.GiaThucBan;
                            this.grdToaThuoc[currentCell.ColumnIndex + 9, currentCell.RowIndex].Value = ttt.CachUongThuoc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 10, currentCell.RowIndex].Value = ttt.CachDungChiTiet;
                            this.grdToaThuoc[currentCell.ColumnIndex + 13, currentCell.RowIndex].Value = ttt.MaChinhSachGia;


                            this.grdToaThuoc[currentCell.ColumnIndex + 14, currentCell.RowIndex].Value = ttt.LoaiThanhToan_Sub;
                            this.grdToaThuoc[currentCell.ColumnIndex + 15, currentCell.RowIndex].Value = ttt.LoaiThanhToan;
                            this.grdToaThuoc[currentCell.ColumnIndex + 16, currentCell.RowIndex].Value = ttt.DangTrinhBay;
                            this.grdToaThuoc[currentCell.ColumnIndex + 17, currentCell.RowIndex].Value = ttt.PhanNhomTheoTCHTVaTCCN;
                            this.grdToaThuoc[currentCell.ColumnIndex + 18, currentCell.RowIndex].Value = ttt.NgayHieuLuc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 19, currentCell.RowIndex].Value = ttt.TenDonViSYT_BV;
                            this.grdToaThuoc[currentCell.ColumnIndex + 20, currentCell.RowIndex].Value = ttt.SttMaHoaTheoKQDTSoQDStt;

                            this.grdToaThuoc[currentCell.ColumnIndex + 21, currentCell.RowIndex].Value = ttt.NhomThuoc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 22, currentCell.RowIndex].Value = ttt.HeSoAnToan;
                            this.grdToaThuoc[currentCell.ColumnIndex + 23, currentCell.RowIndex].Value = ttt.NgayNhapKho;
                            this.grdToaThuoc[currentCell.ColumnIndex + 24, currentCell.RowIndex].Value = ttt.HanSuDung;
                            this.grdToaThuoc[currentCell.ColumnIndex + 25, currentCell.RowIndex].Value = ttt.QuocGia;
                            this.grdToaThuoc[currentCell.ColumnIndex + 26, currentCell.RowIndex].Value = ttt.NhaSanXuat;
                            this.grdToaThuoc[currentCell.ColumnIndex + 27, currentCell.RowIndex].Value = ttt.DangBaoCheDuongUong;

                            this.grdToaThuoc[currentCell.ColumnIndex + 28, currentCell.RowIndex].Value = ttt.SoDKHoacGPKD;
                            this.grdToaThuoc[currentCell.ColumnIndex + 29, currentCell.RowIndex].Value = ttt.TenThanhPhanThuoc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 30, currentCell.RowIndex].Value = ttt.STTTheoDMTCuaBYT;
                            this.grdToaThuoc[currentCell.ColumnIndex + 31, currentCell.RowIndex].Value = ttt.DienGiai;
                            this.grdToaThuoc[currentCell.ColumnIndex + 32, currentCell.RowIndex].Value = ttt.TENTHUOC;


                            TinhTienThuoc();
                        }
                        if (currentCell.ColumnIndex == 1 && (currentCell.RowIndex == grdToaThuoc.Rows.Count - 1))
                        {
                            grdToaThuoc.Rows.Add(1);
                        }
      #endregion
                    }

                  
                }
                if (currentCell != null && currentCell.ColumnIndex == 2)
                {
                    ThongTinThuocKhamBenh ttt = cbm.SelectedItem as ThongTinThuocKhamBenh;
                    //DataRowView drv = cbm.SelectedItem as DataRowView;
                    if (ttt != null)
                    {
                        //  string item = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString();
                        if (currentCell.ColumnIndex == 2)
                        {
                            //     MessageBox.Show(ttt.MedicineName);
                            if (!danhSachThuoc.ContainsKey(currentCell.RowIndex) && !danhSachThuoc.ContainsValue(ttt.MedicineID))
                            {
                                danhSachThuoc.Add(currentCell.RowIndex, ttt.MedicineID);
                            }
                            else if (danhSachThuoc.ContainsKey(currentCell.RowIndex))
                            {
                                danhSachThuoc.Remove(currentCell.RowIndex);
                                danhSachThuoc.Add(currentCell.RowIndex, ttt.MedicineID);
                            }
                            else
                            {
                                MessageBox.Show(clsResources.GetMessage("messages.frmKhamBenh.CheckTrungLapThuoc1"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value = ttt.MedicineID;
                            this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value = ttt.BaoHiem;
                            this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = ttt.SoLuong;
                            this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value = ttt.TenDonViTinh;

                            this.grdToaThuoc[currentCell.ColumnIndex + 4, currentCell.RowIndex].Value = ttt.HamLuong;
                            this.grdToaThuoc[currentCell.ColumnIndex + 6, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                            this.grdToaThuoc[currentCell.ColumnIndex + 7, currentCell.RowIndex].Value = ttt.GiaThucBan;
                            this.grdToaThuoc[currentCell.ColumnIndex + 8, currentCell.RowIndex].Value = ttt.CachUongThuoc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 9, currentCell.RowIndex].Value = ttt.CachDungChiTiet;
                            this.grdToaThuoc[currentCell.ColumnIndex + 12, currentCell.RowIndex].Value = ttt.MaChinhSachGia;

                            this.grdToaThuoc[currentCell.ColumnIndex + 13, currentCell.RowIndex].Value = ttt.LoaiThanhToan_Sub;
                            this.grdToaThuoc[currentCell.ColumnIndex + 14, currentCell.RowIndex].Value = ttt.LoaiThanhToan;
                            this.grdToaThuoc[currentCell.ColumnIndex + 15, currentCell.RowIndex].Value = ttt.DangTrinhBay;
                            this.grdToaThuoc[currentCell.ColumnIndex + 16, currentCell.RowIndex].Value = ttt.PhanNhomTheoTCHTVaTCCN;
                            this.grdToaThuoc[currentCell.ColumnIndex + 17, currentCell.RowIndex].Value = ttt.NgayHieuLuc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 18, currentCell.RowIndex].Value = ttt.TenDonViSYT_BV;
                            this.grdToaThuoc[currentCell.ColumnIndex + 19, currentCell.RowIndex].Value = ttt.SttMaHoaTheoKQDTSoQDStt;

                            this.grdToaThuoc[currentCell.ColumnIndex + 20, currentCell.RowIndex].Value = ttt.NhomThuoc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 21, currentCell.RowIndex].Value = ttt.HeSoAnToan;
                            this.grdToaThuoc[currentCell.ColumnIndex + 22, currentCell.RowIndex].Value = ttt.NgayNhapKho;
                            this.grdToaThuoc[currentCell.ColumnIndex + 23, currentCell.RowIndex].Value = ttt.HanSuDung;
                            this.grdToaThuoc[currentCell.ColumnIndex + 24, currentCell.RowIndex].Value = ttt.QuocGia;
                            this.grdToaThuoc[currentCell.ColumnIndex + 25, currentCell.RowIndex].Value = ttt.NhaSanXuat;
                            this.grdToaThuoc[currentCell.ColumnIndex + 26, currentCell.RowIndex].Value = ttt.DangBaoCheDuongUong;

                            this.grdToaThuoc[currentCell.ColumnIndex + 27, currentCell.RowIndex].Value = ttt.SoDKHoacGPKD;
                            this.grdToaThuoc[currentCell.ColumnIndex + 28, currentCell.RowIndex].Value = ttt.TenThanhPhanThuoc;
                            this.grdToaThuoc[currentCell.ColumnIndex + 29, currentCell.RowIndex].Value = ttt.STTTheoDMTCuaBYT;
                            this.grdToaThuoc[currentCell.ColumnIndex + 30, currentCell.RowIndex].Value = ttt.DienGiai;
                            this.grdToaThuoc[currentCell.ColumnIndex + 31, currentCell.RowIndex].Value = ttt.TENTHUOC;


                            TinhTienThuoc();
                        }

                        if (currentCell.ColumnIndex == 2 && (currentCell.RowIndex == grdToaThuoc.Rows.Count - 1))
                        {
                            grdToaThuoc.Rows.Add(1);
                        }

                    }
                }





            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            frmSearchNhanVien a = new frmSearchNhanVien();
            a.SetParentForm(this);
            a.Show();
        }

        private void btnTiepTucKham_Click(object sender, EventArgs e)
        {
            grdToaThuoc.Rows.Clear();
            //grdToaThuoc.Rows.Add(1);
            BuildGridViewRow();
            txtTongThanhTien.Text = string.Empty;
            txtBenhNhan.Text = string.Empty;
            txtMaNhanVien.Text = string.Empty;
            grpThongTinKhamBenh.Enabled = true;
            btnXoaThuoc.Enabled = true;
            grdToaThuoc.ReadOnly = false;
            btnXacNhan.Enabled = true;
            btnLuuIn.Enabled = false;
            btnLuuIn.Enabled = false;
            txtMaBHYT.Text = string.Empty;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhamBenh = _thongTinKhamBenhDao.GetMaKhamBenhForPrint(listCurrentTransactions[0].MaTransaction);

                if (txtMaBHYT.Text != "")
                {
                    ReportInDonThuocFull frmChild = new ReportInDonThuocFull(maKhamBenh);
                    frmChild.Show();
                }


                ReportInDonThuocBH frmChildBh = new ReportInDonThuocBH(maKhamBenh);
                frmChildBh.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

        }

        private void txtMaNhanVien_Enter(object sender, EventArgs e)
        {
            BindBenhNhanInfo();
        }

        private void grdToaThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString());
        }

        private void combo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void combo_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboKeyPressed()
        {
            cbbMaICD.DroppedDown = true;

            List<MaICD> originalList = (List<MaICD>)cbbMaICD.DataSource;
            if (originalList == null)
            {
                // backup original list
                originalList = new List<MaICD>();
                //cbbMaICD.Items.CopyTo(originalList, 0);
                cbbMaICD.Tag = originalList;
            }

            // prepare list of matching items
            string s = cbbMaICD.Text.ToLower();
            List<MaICD> newList = originalList;
            if (s.Length > 0)
            {
                newList = newList.FindAll(item => item.DienGiai.ToLower().Contains(s));
            }

            // clear list (loop through it, otherwise the cursor would move to the beginning of the textbox...)
            //while (cbbMaICD.Items.Count > 0)
            //{
            //    cbbMaICD.Items.RemoveAt(0);
            //}

            // re-set list
            //cbbMaICD.Items.AddRange(newList.ToArray());
            cbbMaICD.ValueMember = "Ma";
            cbbMaICD.DisplayMember = "DienGiai";
            cbbMaICD.DataSource = newList;
        }

        private void cbbMaICD_KeyPress(object sender, KeyPressEventArgs e)
        {
           // comboKeyPressed();
        }

        private void cbbMaICD_TextChanged(object sender, EventArgs e)
        {
             //if (cbbMaICD.Text.Length > 0) comboKeyPressed();
        }
        private void CreateTextFromNumber()
        {
             MakeToString _mk ;
            var temp = txtTongTienBH.Text;
            var check = false;
            for (var i = 0; i < temp.Length; i++)
            {
                check = Char.IsLetter(temp, i);
                break;
            }
            if (!check & temp.Length <= 15)
            {
                _mk = new MakeToString(Convert.ToDouble(temp));
                _mk.BlockProcessing();

                //lblblock1.Text = Convert.ToString(_mk.BlockNum[0]);
                //lblblock2.Text = Convert.ToString(_mk.BlockNum[1]);
                //lblblock3.Text = Convert.ToString(_mk.BlockNum[2]);
                //lblblock4.Text = Convert.ToString(_mk.BlockNum[3]);
                //lblblock5.Text = Convert.ToString(_mk.BlockNum[4]);
                txtTongTienBangChu.Text = _mk.ReadThis() + " " + "đồng";
            }
        }



    }
}