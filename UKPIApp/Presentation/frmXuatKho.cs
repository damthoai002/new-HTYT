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
    public partial class frmXuatKho : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmXuatKho));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly ThongTinNhapKhoDao _thongTinNhapKhoDao = new ThongTinNhapKhoDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep;
        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        DataGridViewCell currentCell;
        private DateTimePicker cellDateTimePicker;
        private int _checkRowsCount = 0;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();


        #endregion

        #region Constructors

        public frmXuatKho()
        {

            InitializeComponent();
            // grdToaThuoc.AutoGenerateColumns = false;
            //clsTitleManager.InitTitle(this);

            cbbPhongKham.Enabled = false;
            SetDefauldValue();
            this.Text = "XUẤT LẺ THUỐC";

        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            cellDateTimePicker.Visible = false;
        }
        private void SetDefauldValue()
        {
            //    BuildGridViewRow();
            BindPhongKham();
            // LoadThongTinXuatKho();
            BuildGridViewRow();
        }

        private void BindPhongKham()
        {
            //cbbPhongKham.DataSource = _shareEntityDao.LoadDanhSachPhongKham();
            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
            cbbPhongKham.DataSource = listPhongKham;
            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            int currentIndex = listPhongKham.FindIndex(a => a.RoomID == currentKho);
            cbbPhongKham.SelectedIndex = currentIndex;
        }
        private void LoadThongTinXuatKho()
        {

        }

        private void BuildGridViewRow()
        {
            List<ThongTinThuocKhamBenh> lstThuoc = _shareEntityDao.LoadThongTinThuocForKhamBenh(cbbPhongKham.SelectedValue.ToString());

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Width = 60;

            grdToaThuoc.Columns.Add(checkBoxColumn);

            DataGridViewComboBoxColumn col1 = new DataGridViewComboBoxColumn();
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
            cachUongColumn.Visible = false;
            cachUongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(cachUongColumn);


            DataGridViewTextBoxColumn cachDungColumn = new DataGridViewTextBoxColumn();
            cachDungColumn.Width = 230;
            cachDungColumn.HeaderText = "Chi tiết";
            cachDungColumn.ReadOnly = false;
            cachDungColumn.Visible = false;
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

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //string titleText = grdToaThuoc.Columns[8].HeaderText;
            int columnIndex = 0;
            currentCell = this.grdToaThuoc.CurrentCell;
            //if (autoText != null)
            //{
            //    autoText.AutoCompleteCustomSource = null;
            //}
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

                    }
                    catch
                    {

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
        public void cbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Invoke method if the selection changed event occurs
            BeginInvoke(new MethodInvoker(EndEdit));
        }
        private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdToaThuoc.CurrentCell;
            bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
            if (e.ColumnIndex == 3 && isValidMaThuoc)
            {
                System.Drawing.Rectangle tempRect = grdToaThuoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                cellDateTimePicker.Location = tempRect.Location;

                cellDateTimePicker.Width = tempRect.Width;

                cellDateTimePicker.Visible = true;

            }

        }
        void cellDateTimePickerValueChanged(object sender, EventArgs e)
        {
            grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Value.ToString("dd-MM-yyyy");//convert the date as per your format
            cellDateTimePicker.Visible = false;
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
            //Decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["GiaKhamBenh"].ToString());
            txtTongThanhTien.Text = Math.Round(total, 0).ToString();



        }

        private void Export()
        {
            try
            {
                var dtStoreList = grdToaThuoc.DataSource as System.Data.DataTable;
                if (dtStoreList == null)
                {
                    return;
                }
                // Open Save dialog
                using (var saveDlg = new SaveFileDialog())
                {
                    saveDlg.AddExtension = true;
                    saveDlg.Filter = "Excel 2007 Workbook (*.xlsx)|*.xlsx|Excel 97 - 2003 Workbook (*.xls)|*.xls";
                    if (saveDlg.ShowDialog(this) != DialogResult.OK) return;
                    Cursor.Current = Cursors.WaitCursor;

                    // Build Selected Stores as DataTable
                    DataTable dtSelectedStores = dtStoreList.Clone();

                    for (int i = 0; i < dtStoreList.Rows.Count; i++)
                    {
                        dtSelectedStores.ImportRow(dtStoreList.Rows[i]);
                    }



                    // Execute export
                    var exporter = new XuatKhoExporter(true);
                    exporter.AddExportTable(dtSelectedStores);
                    exporter.Export(saveDlg.FileName);

                    MessageBox.Show(clsResources.GetMessage("messages.exportStore.EditStore") + Environment.NewLine + saveDlg.FileName,
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Show(clsResources.GetMessage("errors.unknown"),
                    clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        #endregion


        private void btnExport_Click(object sender, EventArgs e)
        {
            // this.Export();
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {

        }
        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {
            this.quyetDinhNghiPhep = qd;
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            Export();
        }

        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            try
            {
                List<WareHouse> lstWh = buildXuatKhoThuoc();
                if (lstWh.Count > 0)
                {
                    _thongTinKhamBenhDao.XuatKhoThuoc(lstWh);
                    grdToaThuoc.Rows.Clear();
                    BuildGridViewRow();
                }
                else
                {
                    MessageBox.Show("Hãy chọn thuốc trước khi xuất kho!");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show("Xuất thuốc lỗi, xin vui lòng thực hiện lại!");
            }
        }

        public List<WareHouse> buildXuatKhoThuoc()
        {

            if (grdToaThuoc.Rows.Count > 0)
            {
                List<WareHouse> listWareHouse = new List<WareHouse>();
                for (int i = 0; i < grdToaThuoc.Rows.Count; i++)
                {
                    WareHouse wh = new WareHouse();

                    if ((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue == "")
                        continue;
                    wh.MedicineName = (string)grdToaThuoc.Rows[i].Cells[33].FormattedValue;
                    wh.MaThuocHienThi =(string)grdToaThuoc.Rows[i].Cells[2].FormattedValue;
                    wh.MaThuocHeThong = grdToaThuoc.Rows[i].Cells[2].Value.ToString();
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
                    wh.MaLienHe = "XUATKHOLE";
                   // wh.HamLuong = (string)grdToaThuoc.Rows[i].Cells[8].FormattedValue;
                    wh.MaDienGiaiWarehouse = clsCommon.WareHouseType.XUKH;

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
                    wh.TrangThai = 2;
                    wh.TrangThaiDienGiai = clsCommon.WareHouseType.XUATKHOLE;

                    listWareHouse.Add(wh);
                }
                return listWareHouse;
            }
            else
            {
                return new List<WareHouse>();
            }

        }
    }
}