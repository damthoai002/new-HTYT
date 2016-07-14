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
    public partial class frmnhapkhothuoc : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmnhapkhothuoc));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly ThongTinNhapKhoDao _thongTinNhapKhoDao = new ThongTinNhapKhoDao();
        private readonly ChotTonKhoDao _chotTonKhoDao = new ChotTonKhoDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep;
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


        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors

        public frmnhapkhothuoc()
        {

            InitializeComponent();

            clsTitleManager.InitTitle(this);
            this.cellDateTimePicker = new DateTimePicker();
            this.cellDateTimePicker.Format = DateTimePickerFormat.Custom;
            this.cellDateTimePicker.Width = 100;
            this.cellDateTimePicker.CustomFormat = "dd-MM-yyyy";
            //this.cellDateTimePicker.ValueChanged += new EventHandler(cellDateTimePickerValueChanged);
            this.cellDateTimePicker.ValueChanged += new EventHandler(cellDateTimePicker_ValueChanged);
            // this.cellDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);  
            this.cellDateTimePicker.Visible = false;
            this.grdToaThuoc.Controls.Add(cellDateTimePicker);
            cbbPhongKham.Enabled = false;
            SetDefauldValue();
            this.Text = "NHẬP KHO THUỐC";
            // Save original columns
            // _originalColumns = new DataGridViewColumn[grdStores.Columns.Count;
            // grdStores.Columns.CopyTo(_originalColumns, 0);
            // grdStores.Sorted += grdStores_Sorted;
            if (_chotTonKhoDao.CheckChotTonDangHoatDong(cbbPhongKham.SelectedValue.ToString()) > 0)
            {
                DialogResult result = MessageBox.Show("Kho đang được chốt tồn. Vui lòng thực hiện sau", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLuuIn.Enabled = false;
            }
            else
            {
                btnLuuIn.Enabled = true;
            }
        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            cellDateTimePicker.Visible = false;
        }
        private void SetDefauldValue()
        {
            BindPhongKham();
            BuildGridViewRow();
            LoadThongTinNhanVien();
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

        private void LoadThongTinNhanVien()
        {
            // ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            txtMaNhanVienNhap.Text = clsSystemConfig.UserName;
            txtNhanVienNhap.Text = clsSystemConfig.FullName;
        }

        private void BuildGridViewRow()
        {
            List<ThongTinThuoc> lstThuoc = _shareEntityDao.LoadThongTinThuoc();


            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Width = 60;
            checkBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(checkBoxColumn);


            //DataGridViewComboBoxColumn col1 = new DataGridViewComboBoxColumn();
            //col1.Width = 160;
            //col1.HeaderText = "Tên Thuốc";
            //col1.DataSource = lstThuoc;
            //col1.DisplayMember = "MedicineName";
            //col1.ValueMember = "MedicineID";
            //col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            //grdToaThuoc.Columns.Add(col1);

            DataGridViewTextBoxColumn tenThuocColumn = new DataGridViewTextBoxColumn();
            tenThuocColumn.HeaderText = "Tên thuốc";
            tenThuocColumn.Width = 145;
            tenThuocColumn.ReadOnly = true;
            tenThuocColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(tenThuocColumn);



            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.Width = 140;
            col.HeaderText = "Mã thuốc";
            col.DataSource = lstThuoc;
            col.DisplayMember = "MaThuocYTeHienThi";
            col.ValueMember = "MedicineID";
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(col);

            DataGridViewTextBoxColumn hanSuDungColumn = new DataGridViewTextBoxColumn();
            hanSuDungColumn.Width = 100;
            hanSuDungColumn.HeaderText = "Hạn sử dụng";
            hanSuDungColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(hanSuDungColumn);

            DataGridViewCheckBoxColumn baoHiemColumn = new DataGridViewCheckBoxColumn();
            baoHiemColumn.Width = 100;
            baoHiemColumn.HeaderText = "Thuốc BH";
            baoHiemColumn.ReadOnly = true;
            baoHiemColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(baoHiemColumn);

            DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
            soLuongColumn.Width = 130;
            soLuongColumn.HeaderText = "Số lượng";
            soLuongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(soLuongColumn);

            //DataGridViewTextBoxColumn hamLuongColumn = new DataGridViewTextBoxColumn();
            //hamLuongColumn.Width = 120;
            //hamLuongColumn.HeaderText = "Hàm lượng";
            //grdToaThuoc.Columns.Add(hamLuongColumn);

            DataGridViewTextBoxColumn giaNhapColumn = new DataGridViewTextBoxColumn();
            giaNhapColumn.Width = 130;
            giaNhapColumn.HeaderText = "Giá thời diểm nhập";
            giaNhapColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            giaNhapColumn.Visible = false ;
            grdToaThuoc.Columns.Add(giaNhapColumn);

            DataGridViewTextBoxColumn giaTTColumn = new DataGridViewTextBoxColumn();
            giaTTColumn.Width = 130;
            giaTTColumn.HeaderText = "Giá mua vào";
            giaTTColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // giaTTColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(giaTTColumn);

            //DataGridViewComboBoxColumn cachUongColumn = new DataGridViewComboBoxColumn();
            //cachUongColumn.Width = 130;
            //cachUongColumn.HeaderText = "Cách uống";
            //cachUongColumn.DataSource = _shareEntityDao.LoadThongTinCachUongThuoc();
            //cachUongColumn.DisplayMember = "CachUong";
            //cachUongColumn.ValueMember = "MaUongThuoc";
            //grdToaThuoc.Columns.Add(cachUongColumn);
            DataGridViewTextBoxColumn giaSTColumn = new DataGridViewTextBoxColumn();
            giaSTColumn.Width = 130;
            giaSTColumn.HeaderText = "Giá mua vào có thuế";
            giaSTColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //giaSTColumn.ReadOnly = true;
            grdToaThuoc.Columns.Add(giaSTColumn);

            DataGridViewTextBoxColumn hamLuongColumn = new DataGridViewTextBoxColumn();
            hamLuongColumn.Width = 130;
            hamLuongColumn.HeaderText = "Hàm lượng";
            hamLuongColumn.ReadOnly = true;
            hamLuongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(hamLuongColumn);

            DataGridViewTextBoxColumn thanhTienColumn = new DataGridViewTextBoxColumn();
            thanhTienColumn.Width = 130;
            thanhTienColumn.HeaderText = "Thành tiến";
            thanhTienColumn.ReadOnly = true;
            thanhTienColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            grdToaThuoc.Columns.Add(thanhTienColumn);

            grdToaThuoc.CellBeginEdit += this.dataGridView1_CellBeginEdit;
            grdToaThuoc.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            grdToaThuoc.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            grdToaThuoc.CellValueChanged += grdToaThuoc_CellValueChanged;
            grdToaThuoc.CellClick += grdToaThuoc_CellDoubleClick;
            int rowIndex = this.grdToaThuoc.Rows.Add(1);
            var row = this.grdToaThuoc.Rows[rowIndex];

        }

        private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //currentCell = this.grdToaThuoc.CurrentCell;
            //bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
            //if (e.ColumnIndex == 3 && isValidMaThuoc)
            //{
            //    System.Drawing.Rectangle tempRect = grdToaThuoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            //    cellDateTimePicker.Location = tempRect.Location;

            //    cellDateTimePicker.Width = tempRect.Width;

            //    cellDateTimePicker.Visible = true;

            //}

        }
        void cellDateTimePickerValueChanged(object sender, EventArgs e)
        {
            //grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]);//convert the date as per your format
            //cellDateTimePicker.Visible = false;
        }


        private void Export()
        {

        }


        #endregion


        private void btnExport_Click(object sender, EventArgs e)
        {
            // this.Export();
        }


        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.Waring"), clsResources.GetMessage("messages.frmnhapkhothuoc.Title"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                grpThongTinKhamBenh.Enabled = false;
                btnXoaThuoc.Enabled = false;
            }

        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            DialogResult warningMessage = MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.WarningMessage"), clsResources.GetMessage("messages.frmnhapkhothuoc.SuccessTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (warningMessage == DialogResult.Yes)
            {

                if (!ValidateThongSoNhapKho())
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.ValidateThongSoNhapKho"), clsResources.GetMessage("messages.frmnhapkhothuoc.Title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ThongTinNhapKho thongTinNhapKho = BuildThongTinNhapKho();
                if (thongTinNhapKho != null)
                {
                    List<ThongTinNhapKhoDetail> listThongTinNhapKhoDetail = BuildThongTinNhapKhoDetail(thongTinNhapKho.MaNhapKho);
                    if (listThongTinNhapKhoDetail != null && listThongTinNhapKhoDetail.Count > 0)
                    {
                        if (_thongTinNhapKhoDao.SaveThongTinNhapKho(thongTinNhapKho, listThongTinNhapKhoDetail))
                        {
                            DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.Success"), clsResources.GetMessage("messages.frmnhapkhothuoc.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (result == DialogResult.OK)
                            {
                                grdToaThuoc.Rows.Clear();
                                grdToaThuoc.Rows.Add(1);
                                cellDateTimePicker.Visible = false;
                                txtDonViCungCap.Clear();
                                txtMaSoHDD.Clear();
                                txtMaDonViCungCap.Clear();
                            }
                            return;
                        }
                        else
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.Error"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                  
                }
            }
        }
        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {
            this.quyetDinhNghiPhep = qd;
        }
       

        private bool ValidateThongSoNhapKho()
        {
            bool isValid = true;
            if (txtMaSoHDD.Text == "")
            {
                txtMaSoHDD.BackColor = Color.Red;
                isValid = false;
            }
            if (txtDonViCungCap.Text == "")
            {
                txtDonViCungCap.BackColor = Color.Red;
                isValid = false;
            }
            if (txtMaDonViCungCap.Text == "")
            {
                txtMaDonViCungCap.BackColor = Color.Red;
                isValid = false;
            }
            if (!isValid)
            {

            }
            else
            {
                txtMaSoHDD.BackColor = Color.White;
                txtDonViCungCap.BackColor = Color.White;
                txtMaDonViCungCap.BackColor = Color.White;
            }
            return isValid;
        }
        private ThongTinNhapKho BuildThongTinNhapKho()
        {
            ThongTinNhapKho thongTinNhapKho = new ThongTinNhapKho();
            thongTinNhapKho.MaNhapKho = _thongTinNhapKhoDao.GenerateNewMaNhapKho();
            thongTinNhapKho.PhongKhamKho = cbbPhongKham.Text;
            thongTinNhapKho.MaKho = cbbPhongKham.SelectedValue.ToString();
            thongTinNhapKho.NgayNhapKho = dtpNgayNhapKho.Value;
            thongTinNhapKho.TongTienHD = txtTongThanhTien.Text;
            thongTinNhapKho.MaNhanVien = txtMaNhanVienNhap.Text;
            thongTinNhapKho.TenNhanVien = txtNhanVienNhap.Text;
            thongTinNhapKho.MaHDD = txtMaSoHDD.Text;
            thongTinNhapKho.DonViCungCap = txtDonViCungCap.Text;
            thongTinNhapKho.MaSoDVCungCap = txtMaDonViCungCap.Text;
            return thongTinNhapKho;
        }

        private List<ThongTinNhapKhoDetail> BuildThongTinNhapKhoDetail(string maNhapKho)
        {
            List<string> listmaThuoc = new List<string>();
            Dictionary<CustomKey, string> dic = _shareEntityDao.BuildTuDienThuoc();
            List<ThongTinNhapKhoDetail> listThongTinNhapKhoDetail = new List<ThongTinNhapKhoDetail>();
            if (grdToaThuoc.Rows.Count > 0)
            {

                for (int i = 0; i < grdToaThuoc.Rows.Count; i++)
                {

                    ThongTinNhapKhoDetail thongTinNhapKhoDetail = new ThongTinNhapKhoDetail();
                    if ((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue == "")
                        continue;
                    thongTinNhapKhoDetail.TenThuoc = (string)grdToaThuoc.Rows[i].Cells[1].FormattedValue;
                    thongTinNhapKhoDetail.MaThuoc = (string)grdToaThuoc.Rows[i].Cells[2].FormattedValue;
                    string hanSuDung = grdToaThuoc.Rows[i].Cells[3].FormattedValue.ToString();
                    if (hanSuDung == "")
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckHanSuDungThuoc"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    //DateTime dt = DateTime.ParseExact(hanSuDung.Replace("-",""), "ddMMyyyy",
                    //              CultureInfo.InvariantCulture);
                    //dt.ToString("yyyyMMdd");
                    string strHanSuDung = (string)grdToaThuoc.Rows[i].Cells[3].FormattedValue;
                    thongTinNhapKhoDetail.HanSuDung = DateTime.ParseExact(strHanSuDung, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                    thongTinNhapKhoDetail.ThuocBH = (bool)grdToaThuoc.Rows[i].Cells[4].FormattedValue;
                    thongTinNhapKhoDetail.MaNhapKho = maNhapKho;
                    thongTinNhapKhoDetail.LoThuoc = DateTime.Now.ToString("yyyyMMddHHmmss");
                    thongTinNhapKhoDetail.HamLuong = (string)grdToaThuoc.Rows[i].Cells[9].FormattedValue;

                    CustomKey ck = new CustomKey(thongTinNhapKhoDetail.MaThuoc, (bool)grdToaThuoc.Rows[i].Cells[4].FormattedValue);
                    thongTinNhapKhoDetail.MaThuoc = dic[ck];
                    try
                    {
                        string strThanhTien = (string)grdToaThuoc.Rows[i].Cells[10].FormattedValue;
                        thongTinNhapKhoDetail.ThanhTien = decimal.Parse(strThanhTien);
                    }
                    catch { }
                    try
                    {
                        string strSoLuong = (string)grdToaThuoc.Rows[i].Cells[5].FormattedValue;
                        thongTinNhapKhoDetail.SoLuong = int.Parse(strSoLuong);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    try
                    {
                        string strGiaTT = (string)grdToaThuoc.Rows[i].Cells[7].FormattedValue;
                        thongTinNhapKhoDetail.GiaTT = decimal.Parse(strGiaTT);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaTruocThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    try
                    {
                        string strGiaST = (string)grdToaThuoc.Rows[i].Cells[8].FormattedValue;
                        thongTinNhapKhoDetail.GiaST = decimal.Parse(strGiaST);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaSauThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    try
                    {
                        string strGiaThoiDiemNhap = (string)grdToaThuoc.Rows[i].Cells[6].FormattedValue;
                        thongTinNhapKhoDetail.GiaThoiDiemNhap = decimal.Parse(strGiaThoiDiemNhap);
                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaMua"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    if (!listmaThuoc.Contains(thongTinNhapKhoDetail.MaThuoc))
                    {
                        listmaThuoc.Add(thongTinNhapKhoDetail.MaThuoc);
                    }
                    else
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckTrungLapThuoc"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    listThongTinNhapKhoDetail.Add(thongTinNhapKhoDetail);

                }

            }
            return listThongTinNhapKhoDetail;
        }
        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            for (int i = grdToaThuoc.Rows.Count - 1; i > 0; i--)
            {
                if ((bool)grdToaThuoc.Rows[i].Cells[0].FormattedValue)
                {
                    grdToaThuoc.Rows.RemoveAt(i);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdToaThuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void grdToaThuoc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdToaThuoc.CurrentCell;
            //MessageBox.Show(currentCell.ColumnIndex.ToString());
            
            if (currentCell != null && currentCell.ColumnIndex == 5)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null && this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString() != "";
                if (isValidMaThuoc && isValidSoLuongThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? int.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;
                        if (currentSoLuong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaSauThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdToaThuoc[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            //Check gia sau thue
            if (currentCell != null && currentCell.ColumnIndex == 8)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdToaThuoc[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value != null && this.grdToaThuoc[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value.ToString() != "";
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaSauThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (isValidMaThuoc && isValidSoLuongThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdToaThuoc[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value != null ? int.Parse(this.grdToaThuoc[currentCell.ColumnIndex - 3, currentCell.RowIndex].Value.ToString()) : 0;
                        if (currentSoLuong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            //check gia thoi diem nhap
            if (currentCell != null && currentCell.ColumnIndex == 6)
            {
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;

                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaMua"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            //check gia truoc thue
            if (currentCell != null && currentCell.ColumnIndex == 7)
            {
                int currentSoLuong = 0;
                bool isValidMaThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value != null && this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString() != "";
                bool isValidSoLuongThuoc = this.grdToaThuoc[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value != null && this.grdToaThuoc[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value.ToString() != "";
              
                decimal currentGia = 0;
                try
                {
                    currentGia = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? decimal.Parse(this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;
                }
                catch
                {
                    currentGia = 0;
                }

                if (currentGia <= 0)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidGiaTruocThue"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (isValidMaThuoc && isValidSoLuongThuoc)
                {
                    try
                    {
                        currentSoLuong = this.grdToaThuoc[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value != null ? int.Parse(this.grdToaThuoc[currentCell.ColumnIndex - 2, currentCell.RowIndex].Value.ToString()) : 0;
                        if (currentSoLuong <= 0)
                        {
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    catch
                    {
                        MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckValidSoLuong"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                decimal currentTienThuoc = currentSoLuong * currentGia;
                // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            return;
        }

        private void CalculateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in grdToaThuoc.Rows)
            {
                if (row.Cells[10].Value != null)
                {
                    total += decimal.Parse(row.Cells[10].Value.ToString());
                }
            }

            txtTongThanhTien.Text = total.ToString();
        }

        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if ((grdToaThuoc.Focused) && grdToaThuoc.CurrentCell.ColumnIndex == 3)
                {
                    cellDateTimePicker.Location = grdToaThuoc.GetCellDisplayRectangle(grdToaThuoc.CurrentCell.ColumnIndex, grdToaThuoc.CurrentCell.RowIndex, false).Location;
                    cellDateTimePicker.Visible = true;
                    if (grdToaThuoc.CurrentCell.Value != null)
                    {
                        string hanSuDung = grdToaThuoc.CurrentCell.FormattedValue.ToString();
                        cellDateTimePicker.Value = DateTime.ParseExact(hanSuDung, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        cellDateTimePicker.Value = DateTime.Today;
                    }
                }
                else
                {
                    cellDateTimePicker.Visible = false;
                }

              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cbm != null)
            {
                // Here we will remove the subscription for selected index changed
                cbm.SelectedIndexChanged -= new EventHandler(cbm_SelectedIndexChanged);
            }
            try
            {
                if ((grdToaThuoc.Focused) && grdToaThuoc.CurrentCell.ColumnIndex == 3)
                {
                    grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["DateFormat"]);//convert the date as per your format//cellDateTimePicker.Value;
                }

                if ((grdToaThuoc.Focused) && grdToaThuoc.CurrentCell.ColumnIndex == 7)
                {
                    var giaMuaTT = decimal.Parse( grdToaThuoc.CurrentCell.Value.ToString());
                    if (giaMuaTT > 0)
                    {
                        this.grdToaThuoc[8, currentCell.RowIndex].Value = (giaMuaTT * decimal.Parse("1.05")).ToString();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cellDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Text;
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Here try to add subscription for selected index changed event
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

        void cbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Invoke method if the selection changed event occurs
            BeginInvoke(new MethodInvoker(EndEdit));
        }

        void EndEdit()
        {
            // Change the content of appropriate cell when selected index changes
            if (cbm != null)
            {
                ThongTinThuoc ttt = cbm.SelectedItem as ThongTinThuoc;
                //DataRowView drv = cbm.SelectedItem as DataRowView;
                if (ttt != null)
                {
                    //  string item = this.grdToaThuoc[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString();
                    if (currentCell.ColumnIndex == 2)
                    {
                        //     MessageBox.Show(ttt.MedicineName);
                        //case 1: chua co thong tin thuoc cho row
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
                            MessageBox.Show(clsResources.GetMessage("messages.frmnhapkhothuoc.CheckTrungLapThuoc1"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value = ttt.MedicineName;
                        this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = ttt.BaoHiem;
                        this.grdToaThuoc[currentCell.ColumnIndex + 4, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                        this.grdToaThuoc[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = ttt.GiaDNMua;
                        this.grdToaThuoc[currentCell.ColumnIndex + 6, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                        this.grdToaThuoc[currentCell.ColumnIndex + 7, currentCell.RowIndex].Value = ttt.HamLuong;
                    }
                    if (currentCell.ColumnIndex == 2 && (currentCell.RowIndex == grdToaThuoc.Rows.Count - 1))
                    {
                        grdToaThuoc.Rows.Add(1);
                    }

                }
            }
        }






    }
}