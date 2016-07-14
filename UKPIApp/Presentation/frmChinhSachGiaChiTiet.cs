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
    public partial class frmChinhSachGiaChiTiet : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmChinhSachGiaChiTiet));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly QuanLyThuocDao _quanLyThuocDao = new QuanLyThuocDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep ;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();
        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        DataGridViewCell currentCell;
        private string maChinhSachGia;
        private string tenChinhSachGia;
        private int _checkRowsCount = 0;
        private bool isRealOnlyForm = false;

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
        private List<DonViTinh> listDonViTinh = new List<DonViTinh>();
        private List<ChinhSachGiaChiTiet> oldListChinhSachGiaChiTiet = new List<ChinhSachGiaChiTiet>();
        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors

        public frmChinhSachGiaChiTiet()
        {

            InitializeComponent();

            clsTitleManager.InitTitle(this);

            this.Text = "CHI TIẾT CHÍNH SÁCH GIÁ";
           // txtMaChinhSachGia.Text = maChinhSachGia;
            // Save original columns
           // _originalColumns = new DataGridViewColumn[grdStores.Columns.Count;
           // grdStores.Columns.CopyTo(_originalColumns, 0);
           // grdStores.Sorted += grdStores_Sorted;
        }

        public void SetMaChinhSachGia(string value)
        {
            this.maChinhSachGia = value;
            txtMaChinhSachGia.Text = value;
            SetDefauldValue();
        }

        public void SetRealOnlyForm(bool value)
        {
            this.isRealOnlyForm = value;
            if (isRealOnlyForm == true)
            {
                grdToaThuoc.ReadOnly = true;
            }
            else
            {
                grdToaThuoc.ReadOnly = false;
            }
        }
        public void SetTenChinhSachGia(string value)
        {
            this.tenChinhSachGia = value;

        }
        
        private void SetDefauldValue()
        {        
            
            BuildGridViewRow();
            listDonViTinh = _shareEntityDao.LoadDonViTinh();
           
        }
       
        
      
        private void BuildGridViewRow()
        {
            List<ChinhSachGiaChiTiet> listChinhSachGiaChiTiet = _quanLyThuocDao.GetChinhSachGiaChiTiet(maChinhSachGia);
            oldListChinhSachGiaChiTiet = listChinhSachGiaChiTiet;
            if (listChinhSachGiaChiTiet == null || listChinhSachGiaChiTiet.Count == 0)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Width = 40;
                checkBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(checkBoxColumn);

                DataGridViewTextBoxColumn maCSGChiTietColumn = new DataGridViewTextBoxColumn();
                maCSGChiTietColumn.Width = 1;
                maCSGChiTietColumn.Visible = false;
                maCSGChiTietColumn.ReadOnly = true;
                maCSGChiTietColumn.DataPropertyName = "Id";
                maCSGChiTietColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(maCSGChiTietColumn);

                DataGridViewTextBoxColumn tenThuocColumn = new DataGridViewTextBoxColumn();
                tenThuocColumn.HeaderText = "Tên thuốc";
                tenThuocColumn.ReadOnly = true;
                tenThuocColumn.Width = 140;
                tenThuocColumn.DataPropertyName = "MedicineName";
                tenThuocColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(tenThuocColumn);


                DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
                col.Width = 140;
                col.HeaderText = "Mã thuốc";
                col.DataSource = _shareEntityDao.LoadThongTinThuoc();
                //col.DisplayMember = "MedicineID";
                //col.ValueMember = "MedicineID";
                col.DataPropertyName = "MedicineID";
                col.DisplayMember = "MaThuocYTeHienThi";
                col.ValueMember = "MedicineID";
                grdToaThuoc.Columns.Add(col);

                DataGridViewCheckBoxColumn baoHiemColumn = new DataGridViewCheckBoxColumn();
                baoHiemColumn.Width = 60;
                baoHiemColumn.HeaderText = "Thuốc BH";
                baoHiemColumn.ReadOnly = true;
                baoHiemColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(baoHiemColumn);

                DataGridViewTextBoxColumn giaDNBanColumn = new DataGridViewTextBoxColumn();
                giaDNBanColumn.Width = 70;
                giaDNBanColumn.HeaderText = "Giá bán";
                giaDNBanColumn.DataPropertyName = "GiaDNBan";
                giaDNBanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                giaDNBanColumn.Visible = false;
                grdToaThuoc.Columns.Add(giaDNBanColumn);

                DataGridViewTextBoxColumn giaDNBanVATColumn = new DataGridViewTextBoxColumn();
                giaDNBanVATColumn.Width = 70;
                giaDNBanVATColumn.HeaderText = "Giá bán VAT";
                giaDNBanVATColumn.DataPropertyName = "GiaDNBanVAT";
                giaDNBanVATColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                giaDNBanVATColumn.Visible = false;
                grdToaThuoc.Columns.Add(giaDNBanVATColumn);

                DataGridViewTextBoxColumn giaThucBanColumn = new DataGridViewTextBoxColumn();
                giaThucBanColumn.Width = 70;
                giaThucBanColumn.HeaderText = "Giá thực bán";
                giaThucBanColumn.DataPropertyName = "GiaThucBan";
                giaThucBanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(giaThucBanColumn);


                DataGridViewTextBoxColumn giaDNMuaColumn = new DataGridViewTextBoxColumn();
                giaDNMuaColumn.Width = 70;
                giaDNMuaColumn.HeaderText = "Giá mua";
                giaDNMuaColumn.DataPropertyName = "GiaDNMua";
                giaDNMuaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
giaDNMuaColumn.Visible = false;
                grdToaThuoc.Columns.Add(giaDNMuaColumn);

                DataGridViewTextBoxColumn giaDNMuaVATColumn = new DataGridViewTextBoxColumn();
                giaDNMuaVATColumn.Width = 70;
                giaDNMuaVATColumn.HeaderText = "Giá mua VAT";
                giaDNMuaVATColumn.DataPropertyName = "GiaDNMuaVAT";
                giaDNMuaVATColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                giaDNMuaVATColumn.Visible = false;
                grdToaThuoc.Columns.Add(giaDNMuaVATColumn);

                DataGridViewTextBoxColumn giaThucMuaColumn = new DataGridViewTextBoxColumn();
                giaThucMuaColumn.Width = 70;
                giaThucMuaColumn.HeaderText = "Giá thực mua";
                giaThucMuaColumn.DataPropertyName = "GiaThucMua";
                giaThucMuaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
           
                grdToaThuoc.Columns.Add(giaThucMuaColumn);
  
                DataGridViewComboBoxColumn donViTinhColumn = new DataGridViewComboBoxColumn();
                donViTinhColumn.Width = 130;
                donViTinhColumn.HeaderText = "Đơn vị tính";
                donViTinhColumn.DataSource = _shareEntityDao.LoadDonViTinh();
                donViTinhColumn.DisplayMember = "TenDonViTinh";
                donViTinhColumn.ValueMember = "MaDonViTinh";
                donViTinhColumn.DataPropertyName = "DonViTinh";
                grdToaThuoc.Columns.Add(donViTinhColumn);

                DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
                soLuongColumn.Width = 150;
                soLuongColumn.HeaderText = "Diễn giải";
                soLuongColumn.DataPropertyName = "DienGiai";
                grdToaThuoc.Columns.Add(soLuongColumn);

                DataGridViewCheckBoxColumn hoatDongColumn = new DataGridViewCheckBoxColumn();
                hoatDongColumn.Width = 100;
                hoatDongColumn.HeaderText = "Hoạt động";
                hoatDongColumn.DataPropertyName = "HoatDong";
                grdToaThuoc.Columns.Add(hoatDongColumn);

                grdToaThuoc.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
                grdToaThuoc.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
                int rowIndex = this.grdToaThuoc.Rows.Add(1);
            }
            else if (listChinhSachGiaChiTiet != null || listChinhSachGiaChiTiet.Count > 0)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Width = 40;
                checkBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(checkBoxColumn);

                DataGridViewTextBoxColumn maCSGChiTietColumn = new DataGridViewTextBoxColumn();
                maCSGChiTietColumn.Width = 1;
                maCSGChiTietColumn.Visible = false;
                maCSGChiTietColumn.ReadOnly = true;
                maCSGChiTietColumn.DataPropertyName = "Id";
                maCSGChiTietColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(maCSGChiTietColumn);

                DataGridViewTextBoxColumn tenThuocColumn = new DataGridViewTextBoxColumn();
                tenThuocColumn.HeaderText = "Tên thuốc";
                tenThuocColumn.ReadOnly = true;

                tenThuocColumn.Width = 140;
                tenThuocColumn.DataPropertyName = "MedicineName";
                tenThuocColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(tenThuocColumn);


                DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
                col.Width = 140;
                col.HeaderText = "Mã thuốc";
                col.DataSource = _shareEntityDao.LoadThongTinThuoc();
                //col.DisplayMember = "MedicineID";
                //col.ValueMember = "MedicineID";
                col.DataPropertyName = "MedicineID";
                col.DisplayMember = "MaThuocYTeHienThi";
                col.ValueMember = "MedicineID";
                grdToaThuoc.Columns.Add(col);

                DataGridViewCheckBoxColumn baoHiemColumn = new DataGridViewCheckBoxColumn();
                baoHiemColumn.Width = 60;
                baoHiemColumn.HeaderText = "Thuốc BH";
                baoHiemColumn.ReadOnly = true;
                baoHiemColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(baoHiemColumn);

                DataGridViewTextBoxColumn giaDNBanColumn = new DataGridViewTextBoxColumn();
                giaDNBanColumn.Width = 70;
                giaDNBanColumn.HeaderText = "Giá bán";
                giaDNBanColumn.DataPropertyName = "GiaDNBan";
                giaDNBanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                giaDNBanColumn.Visible = false;
                grdToaThuoc.Columns.Add(giaDNBanColumn);

                DataGridViewTextBoxColumn giaDNBanVATColumn = new DataGridViewTextBoxColumn();
                giaDNBanVATColumn.Width = 70;
                giaDNBanVATColumn.HeaderText = "Giá bán VAT";
                giaDNBanVATColumn.DataPropertyName = "GiaDNBanVAT";
                giaDNBanVATColumn.Visible = false;
                giaDNBanVATColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(giaDNBanVATColumn);

                DataGridViewTextBoxColumn giaThucBanColumn = new DataGridViewTextBoxColumn();
                giaThucBanColumn.Width = 120;
                giaThucBanColumn.HeaderText = "Giá thanh toán BHYT";
                giaThucBanColumn.DataPropertyName = "GiaThucBan";
                giaThucBanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(giaThucBanColumn);


                DataGridViewTextBoxColumn giaDNMuaColumn = new DataGridViewTextBoxColumn();
                giaDNMuaColumn.Width = 70;
                giaDNMuaColumn.HeaderText = "Giá mua";
                giaDNMuaColumn.Visible = false;
                giaDNMuaColumn.DataPropertyName = "GiaDNMua";
                giaDNMuaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(giaDNMuaColumn);

                DataGridViewTextBoxColumn giaDNMuaVATColumn = new DataGridViewTextBoxColumn();
                giaDNMuaVATColumn.Width = 70;
                giaDNMuaVATColumn.Visible = false;
                giaDNMuaVATColumn.HeaderText = "Giá mua VAT";
                giaDNMuaVATColumn.DataPropertyName = "GiaDNMuaVAT";
                giaDNMuaVATColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(giaDNMuaVATColumn);

                DataGridViewTextBoxColumn giaThucMuaColumn = new DataGridViewTextBoxColumn();
                giaThucMuaColumn.Width = 70;
                giaThucMuaColumn.Visible = false;
                giaThucMuaColumn.HeaderText = "Giá thực mua";
                giaThucMuaColumn.DataPropertyName = "GiaThucMua";
                giaThucMuaColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                grdToaThuoc.Columns.Add(giaThucMuaColumn);

                DataGridViewComboBoxColumn donViTinhColumn = new DataGridViewComboBoxColumn();
                donViTinhColumn.Width = 130;
                donViTinhColumn.HeaderText = "Đơn vị tính";
                donViTinhColumn.DataSource = _shareEntityDao.LoadDonViTinh();
                donViTinhColumn.DisplayMember = "TenDonViTinh";
                donViTinhColumn.ValueMember = "MaDonViTinh";
                donViTinhColumn.DataPropertyName = "DonViTinh";
                grdToaThuoc.Columns.Add(donViTinhColumn);

                DataGridViewTextBoxColumn soLuongColumn = new DataGridViewTextBoxColumn();
                soLuongColumn.Width = 150;
                soLuongColumn.HeaderText = "Diễn giải";
                soLuongColumn.DataPropertyName = "DienGiai";
                grdToaThuoc.Columns.Add(soLuongColumn);

                DataGridViewCheckBoxColumn hoatDongColumn = new DataGridViewCheckBoxColumn();
                hoatDongColumn.Width = 100;
                hoatDongColumn.HeaderText = "Hoạt động";
                hoatDongColumn.DataPropertyName = "HoatDong";
                grdToaThuoc.Columns.Add(hoatDongColumn);

                grdToaThuoc.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
                grdToaThuoc.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

                for (int i = 0; i < listChinhSachGiaChiTiet.Count; i++)
                {
                    this.grdToaThuoc.Rows.Add(1);
                    this.grdToaThuoc[1, i].Value = listChinhSachGiaChiTiet[i].Id;
                    this.grdToaThuoc[2, i].Value = listChinhSachGiaChiTiet[i].MedicineName;
                    this.grdToaThuoc[3, i].Value = listChinhSachGiaChiTiet[i].MedicineID;
                    this.grdToaThuoc[4, i].Value = listChinhSachGiaChiTiet[i].BaoHiem;
                    this.grdToaThuoc[5, i].Value = listChinhSachGiaChiTiet[i].GiaDNBan;
                    this.grdToaThuoc[6, i].Value = listChinhSachGiaChiTiet[i].GiaDNBanVAT;
                    this.grdToaThuoc[7, i].Value = listChinhSachGiaChiTiet[i].GiaThucBan;
                    this.grdToaThuoc[8, i].Value = listChinhSachGiaChiTiet[i].GiaDNMua;
                    this.grdToaThuoc[9, i].Value = listChinhSachGiaChiTiet[i].GiaDNMuaVAT;
                    this.grdToaThuoc[10, i].Value = listChinhSachGiaChiTiet[i].GiaThucMua;
                    this.grdToaThuoc[11, i].Value = listChinhSachGiaChiTiet[i].DonViTinh;
                    this.grdToaThuoc[12, i].Value = listChinhSachGiaChiTiet[i].DienGiai;
                    this.grdToaThuoc[13, i].Value = listChinhSachGiaChiTiet[i].HoatDong;
                }
                this.grdToaThuoc.Rows.Add(1);
            
            }




        }

       

        





        #endregion

        private int GetDonViTinh(string name)
        { 
            for(int i = 0 ;i < listDonViTinh.Count;i++)
            {
                if(listDonViTinh[i].TenDonViTinh == name)
                    return listDonViTinh[i].MaDonViTinh;
                else continue;
            }
            return 0;
        }

      
        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            List<string> listmaThuoc = new List<string>();
            Dictionary<CustomKey, string> dic = _shareEntityDao.BuildTuDienThuoc();
            List<ChinhSachGiaChiTiet> listChinhSachGiaChiTiet = new List<ChinhSachGiaChiTiet>();
            
            for (int i = 0; i < grdToaThuoc.Rows.Count; i++)
            {

                ChinhSachGiaChiTiet chinhSachChiTiet = new ChinhSachGiaChiTiet();
                if ((string)grdToaThuoc.Rows[i].Cells[2].FormattedValue == "")
                    continue;
                
                chinhSachChiTiet.MaChinhSachGia = txtMaChinhSachGia.Text;
                chinhSachChiTiet.Id = string.IsNullOrEmpty((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue) ? 0 : long.Parse((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue);
                chinhSachChiTiet.MedicineID = (string)grdToaThuoc.Rows[i].Cells[3].FormattedValue;
                chinhSachChiTiet.MedicineName = (string)grdToaThuoc.Rows[i].Cells[2].FormattedValue;
                chinhSachChiTiet.BaoHiem = (bool)grdToaThuoc.Rows[i].Cells[4].FormattedValue;
                chinhSachChiTiet.GiaDNBan = decimal.Parse("0.00");//decimal.Parse((string)grdToaThuoc.Rows[i].Cells[5].FormattedValue);
                chinhSachChiTiet.GiaDNBanVAT = decimal.Parse("0.00");//decimal.Parse((string)grdToaThuoc.Rows[i].Cells[6].FormattedValue);
                chinhSachChiTiet.GiaThucBan = decimal.Parse((string)grdToaThuoc.Rows[i].Cells[7].FormattedValue);
                chinhSachChiTiet.GiaDNMua = decimal.Parse("0.00");//decimal.Parse((string)grdToaThuoc.Rows[i].Cells[8].FormattedValue);
                chinhSachChiTiet.GiaDNMuaVAT = decimal.Parse("0.00");//decimal.Parse((string)grdToaThuoc.Rows[i].Cells[9].FormattedValue);
                chinhSachChiTiet.GiaThucMua = decimal.Parse("0.00");//decimal.Parse((string)grdToaThuoc.Rows[i].Cells[10].FormattedValue);
                chinhSachChiTiet.DonViTinh = GetDonViTinh((string)grdToaThuoc.Rows[i].Cells[11].FormattedValue);
                chinhSachChiTiet.DienGiai = (string)grdToaThuoc.Rows[i].Cells[12].FormattedValue;
                chinhSachChiTiet.HoatDong = (bool)grdToaThuoc.Rows[i].Cells[13].FormattedValue;
                chinhSachChiTiet.MaThuocYTeHienThi = (string)grdToaThuoc.Rows[i].Cells[3].FormattedValue;
                chinhSachChiTiet.TenChinhSachGia = tenChinhSachGia;
                CustomKey ck = new CustomKey(chinhSachChiTiet.MedicineID, chinhSachChiTiet.BaoHiem);
                try
                {
                    chinhSachChiTiet.MedicineID = dic[ck];
                }
                catch (Exception)
                {
                    continue;
                }
                //
                if (!listmaThuoc.Contains(chinhSachChiTiet.MedicineID))
                {
                    listmaThuoc.Add(chinhSachChiTiet.MedicineID);
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChitTiet.CheckTrungLapThuoc"), clsResources.GetMessage("messages.frmKhamBenh.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
     
                }
                listChinhSachGiaChiTiet.Add(chinhSachChiTiet);
            }
            if (listChinhSachGiaChiTiet.Count > 0)
            {
                bool updateData = true;
                bool markDelete = true;
                updateData = _quanLyThuocDao.ProcessChinhGiaChiTiet(listChinhSachGiaChiTiet);
                markDelete = _quanLyThuocDao.ProcessMarkDeleteChinhGiaChiTiet(oldListChinhSachGiaChiTiet, listChinhSachGiaChiTiet);
                if (updateData && markDelete)
                {
                    DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.Success"), clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                    return;
                }
                else {
                    MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.Error"), clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
        

        private void btnXoaThuoc_Click(object sender, EventArgs e)
        {
            for (int i = grdToaThuoc.Rows.Count - 1; i >= 0; i--)
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

        
        /*
        private void grdToaThuoc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            currentCell = this.grdToaThuoc.CurrentCell;
            if (currentCell != null && currentCell.ColumnIndex == 6)
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
                            MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.CheckValidSoLuong"), clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //check thuoc trong kho
                        if (isValidSoLuongThuoc)
                        {
                            string maThuoc = this.grdToaThuoc[2, currentCell.RowIndex].Value.ToString();
                            int soLuongThuocTrongKho = _thongTinKhamBenhDao.CheckSoLuongThuocTrongKho(maThuoc, currentSoLuong);
                            if (soLuongThuocTrongKho == -1)
                            {
                                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.CheckSoLuongTrongKho"), clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    catch {
                        MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.CheckValidSoLuong"), clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
                double currentGia = this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value != null ? double.Parse(this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value.ToString()) : 0;
                double currentTienThuoc = currentSoLuong * currentGia;
               // MessageBox.Show("CellChange" + currentTienThuoc.ToString());
                this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value = currentTienThuoc.ToString();
                CalculateTotal();
            }
            return;
        }

        private void CalculateTotal()
        {
            double total = 0;

            foreach (DataGridViewRow row in grdToaThuoc.Rows)
            {
                if (row.Cells[9].Value != null)
                {
                    total += double.Parse(row.Cells[9].Value.ToString());
                }
            }

            txtTongThanhTien.Text = total.ToString();
        }
        */

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cbm != null)
            {
                // Here we will remove the subscription for selected index changed
                cbm.SelectedIndexChanged -= new EventHandler(cbm_SelectedIndexChanged);
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Here try to add subscription for selected index changed event
            if (e.Control is ComboBox)
            {
                cbm = (ComboBox)e.Control;
                if (cbm != null)
                {
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
                    if (currentCell.ColumnIndex == 3)
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
                            MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGiaChiTiet.CheckTrungLapThuoc1"), clsResources.GetMessage("messages.frmnhapkhothuoc.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        this.grdToaThuoc[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value = ttt.MedicineName;
                        this.grdToaThuoc[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value = ttt.BaoHiem;
                        this.grdToaThuoc[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = ttt.GiaDNBan;
                        this.grdToaThuoc[currentCell.ColumnIndex + 3, currentCell.RowIndex].Value = ttt.GiaDNBanVAT;
                        this.grdToaThuoc[currentCell.ColumnIndex + 4, currentCell.RowIndex].Value = ttt.GiaThucBan;
                        this.grdToaThuoc[currentCell.ColumnIndex + 5, currentCell.RowIndex].Value = ttt.GiaDNMua;
                        this.grdToaThuoc[currentCell.ColumnIndex + 6, currentCell.RowIndex].Value = ttt.GiaDNMuaVAT;
                        this.grdToaThuoc[currentCell.ColumnIndex + 7, currentCell.RowIndex].Value = ttt.GiaThucMua;
                        this.grdToaThuoc[currentCell.ColumnIndex + 8, currentCell.RowIndex].Value = ttt.DonViTinh;
                        this.grdToaThuoc[currentCell.ColumnIndex + 9, currentCell.RowIndex].Value = ttt.DienGiai;
                        this.grdToaThuoc[currentCell.ColumnIndex + 10, currentCell.RowIndex].Value = ttt.HoatDong;
                    }
                    if (currentCell.ColumnIndex == 3 && (currentCell.RowIndex == grdToaThuoc.Rows.Count - 1))
                    {
                        grdToaThuoc.Rows.Add(1);
                    }

                }
            }
        }
    }
}