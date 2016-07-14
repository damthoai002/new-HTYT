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
    public partial class frmChinhSachGia : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmChinhSachGia));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly QuanLyThuocDao _quanLyThuocDao = new QuanLyThuocDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep ;
        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        DataGridViewCell currentCell;
        private int currentRowIndex;
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

        public frmChinhSachGia()
        {

            InitializeComponent();
            grdChinhSachGia.AutoGenerateColumns = false;
         //   grdChinhSachGia.CellClick += grdChinhSachGia_CellClick;
            grdChinhSachGia.CellDoubleClick += grdChinhSachGia_CellDoubleClick;
            //clsTitleManager.InitTitle(this);
            //this.cellDateTimePicker = new DateTimePicker();
            //this.cellDateTimePicker.Format = DateTimePickerFormat.Custom;
            //this.cellDateTimePicker.CustomFormat = "dd-MM-yyyy";
            //this.cellDateTimePicker.ValueChanged += new EventHandler(cellDateTimePickerValueChanged);
            ////this.cellDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);  
            //this.cellDateTimePicker.Visible = false;
            //this.grdChinhSachGia.Controls.Add(cellDateTimePicker);
            SetDefauldValue();
            this.Text = "CHÍNH SÁCH GIÁ";
           // Save original columns
           // _originalColumns = new DataGridViewColumn[grdStores.Columns.Count;
           // grdStores.Columns.CopyTo(_originalColumns, 0);
           // grdStores.Sorted += grdStores_Sorted;
        }
        private void grdChinhSachGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdChinhSachGia.CurrentCell;
            if (currentCell != null && btnLuu.Enabled == false)
            {
                txtMaChinhSachGia.Text = (string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[1].FormattedValue;
                txtTenChinhSachGia.Text = (string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[2].FormattedValue;
                cbHoatDong.Checked = (bool)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[5].FormattedValue;
                dtpThoiGianBatDau.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[3].FormattedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtpThoiGianKetThuc.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[4].FormattedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtpNgayNgungHoatDong.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[6].FormattedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                grdChinhSachGia.Rows[currentCell.RowIndex].Cells[0].Value = true;
                DeselectOrtherCheckbox(currentCell.RowIndex);
                btnCapNhat.Enabled = true;
                btnChiTietChinhSachGia.Enabled = true;
                currentRowIndex = currentCell.RowIndex;
                if (dtpNgayNgungHoatDong.Value <= DateTime.Now.Date || cbHoatDong.Checked == false)
                {
                    SetFormEnable(false);
                }
                else
                {
                    SetFormEnable(true);
                }
            }
        }

        private void SetFormEnable(bool value)
        {
            txtTenChinhSachGia.Enabled = value;
            cbHoatDong.Enabled = value;
            dtpThoiGianBatDau.Enabled = value;
            dtpThoiGianKetThuc.Enabled = value;
            dtpNgayNgungHoatDong.Enabled = value;
            
        }
        private void DeselectOrtherCheckbox(int currentRowIndex)
        {
            for (int i = 0; i < grdChinhSachGia.Rows.Count; i++)
            {
                if (i != currentRowIndex)
                {
                    grdChinhSachGia.Rows[i].Cells[0].Value = false;
                }
                else
                    continue;
            }
        }

        private void DeselectCheckbox()
        {
            for (int i = 0; i < grdChinhSachGia.Rows.Count; i++)
            {      
                    grdChinhSachGia.Rows[i].Cells[0].Value = false;
            }
        }
        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            cellDateTimePicker.Visible = false;
        } 
        private void SetDefauldValue()
        {
       //     BuildGridViewRow();
       //     LoadThongTinNhanVien();
            LoadAllChinhSachGia();
            SetDefaultInputValue();
            btnLuu.Enabled = false;
            btnCapNhat.Enabled = true;
            btnChiTietChinhSachGia.Enabled = true;
            btnThemMoi.Enabled = true;

        }
        private void LoadAllChinhSachGia()
        {
            grdChinhSachGia.DataSource = _quanLyThuocDao.LoadChinhSachGia();
        }

        private void SetDefaultInputValue()
        {
            try
            {
                if (grdChinhSachGia.Rows.Count > 0)
                {
                    txtMaChinhSachGia.Text = (string)grdChinhSachGia.Rows[0].Cells[1].FormattedValue;
                    txtTenChinhSachGia.Text = (string)grdChinhSachGia.Rows[0].Cells[2].FormattedValue;
                    cbHoatDong.Checked = (bool)grdChinhSachGia.Rows[0].Cells[5].FormattedValue;
                    //dtpThoiGianBatDau.Value = DateTime.Parse(((string)grdChinhSachGia.Rows[0].Cells[3].FormattedValue));
                    dtpThoiGianBatDau.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[0].Cells[3].FormattedValue, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                    //dtpThoiGianKetThuc.Value = DateTime.Parse(((string)grdChinhSachGia.Rows[0].Cells[4].FormattedValue));
                    //dtpNgayNgungHoatDong.Value = DateTime.Parse(((string)grdChinhSachGia.Rows[0].Cells[6].FormattedValue));
                    dtpThoiGianKetThuc.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[0].Cells[4].FormattedValue, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);
                    dtpNgayNgungHoatDong.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[0].Cells[6].FormattedValue, System.Configuration.ConfigurationManager.AppSettings["DateFormat"], CultureInfo.InvariantCulture);

                    if (dtpNgayNgungHoatDong.Value <= DateTime.Now.Date || cbHoatDong.Checked == false)
                    {
                        SetFormEnable(false);
                    }
                    else
                    {
                        SetFormEnable(true);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BuildGridViewData()
        {
            

        }

        private void grdChinhSachGia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdChinhSachGia.CurrentCell;
            if (currentCell != null && btnLuu.Enabled == false)
            {
                txtMaChinhSachGia.Text = (string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[1].FormattedValue;
                txtTenChinhSachGia.Text = (string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[2].FormattedValue;
                cbHoatDong.Checked = (bool)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[5].FormattedValue;
                dtpThoiGianBatDau.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[3].FormattedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtpThoiGianKetThuc.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[4].FormattedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dtpNgayNgungHoatDong.Value = DateTime.ParseExact((string)grdChinhSachGia.Rows[currentCell.RowIndex].Cells[6].FormattedValue, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                grdChinhSachGia.Rows[currentCell.RowIndex].Cells[0].Value = true;
                DeselectOrtherCheckbox(currentCell.RowIndex);
                btnCapNhat.Enabled = true;
                btnChiTietChinhSachGia.Enabled = true;
                currentRowIndex = currentCell.RowIndex;
                if (dtpNgayNgungHoatDong.Value <= DateTime.Now.Date || cbHoatDong.Checked == false)
                {
                    SetFormEnable(false);
                }
                else
                {
                    SetFormEnable(true);
                }
            }
        }
         void cellDateTimePickerValueChanged(object sender, EventArgs e)
         {
             //grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Value.ToString("dd-MM-yyyy");//convert the date as per your format
             //cellDateTimePicker.Visible = false;
         }

      
     
       
       

        private void Export()
        {
            //try
            //{
            //    var dtStoreList = grdStores.DataSource as System.Data.DataTable;
            //    if (dtStoreList == null)
            //    {
            //        return;
            //    }
            //    // Open Save dialog
            //    using (var saveDlg = new SaveFileDialog())
            //    {
            //        saveDlg.AddExtension = true;
            //        saveDlg.Filter = "Excel 2007 Workbook (*.xlsx)|*.xlsx|Excel 97 - 2003 Workbook (*.xls)|*.xls";
            //        if (saveDlg.ShowDialog(this) != DialogResult.OK) return;
            //        Cursor.Current = Cursors.WaitCursor;

            //        // Build Selected Stores as DataTable
            //        DataTable dtSelectedStores = dtStoreList.Clone();

            //        for (int i = 0; i < dtStoreList.Rows.Count; i++)
            //        {
            //            dtSelectedStores.ImportRow(dtStoreList.Rows[i]);
            //        }



            //        // Execute export
            //        var exporter = new EditApproveTimesheetExporter(true);
            //        exporter.AddExportTable(dtSelectedStores);
            //        exporter.Export(saveDlg.FileName);

            //        MessageBox.Show(clsResources.GetMessage("messages.exportStore.EditStore") + Environment.NewLine + saveDlg.FileName,
            //            clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex.Message);
            //    MessageBox.Show(clsResources.GetMessage("errors.unknown"),
            //        clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //}
        }


        #endregion

      
        private void btnExport_Click(object sender, EventArgs e)
        {
           // this.Export();
        }


        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            if (txtTenChinhSachGia.Text.Trim() == "")
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.TenChinhSachGia"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpThoiGianBatDau.Value > dtpThoiGianKetThuc.Value)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.NgayBatDau"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpThoiGianKetThuc.Value < dtpThoiGianBatDau.Value)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.NgayKetThuc"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpNgayNgungHoatDong.Value < dtpThoiGianBatDau.Value || dtpNgayNgungHoatDong.Value > dtpThoiGianKetThuc.Value)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.NgayHetHan"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_quanLyThuocDao.CheckOverlapChinhSachGia("", dtpThoiGianBatDau.Value.Date, dtpNgayNgungHoatDong.Value.Date) > 0)
            {
                MessageBox.Show("Chính sách giá trùng lặp thời gian với chính sách giá khác", clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            ChinhSachGiaDT chinhSachGia = new ChinhSachGiaDT();
            chinhSachGia.MaChinhSachGia = txtMaChinhSachGia.Text;
            chinhSachGia.TenChinhSachGia = txtTenChinhSachGia.Text;
            chinhSachGia.HoatDong = cbHoatDong.Checked;
            chinhSachGia.ThoiGianBatDau = dtpThoiGianBatDau.Value.Date;
            chinhSachGia.ThoiGianKetThuc = dtpThoiGianKetThuc.Value.Date;
            chinhSachGia.NgayNgungHoatDong = dtpNgayNgungHoatDong.Value.Date;
            chinhSachGia.CreatedDate = DateTime.Now;
            chinhSachGia.LastUpdatedDate = DateTime.Now;
            chinhSachGia.CreatedBy = clsSystemConfig.UserName;
            chinhSachGia.LastUpdatedBy = clsSystemConfig.UserName;

            if (_quanLyThuocDao.SaveChinhSachGia(chinhSachGia))
            {
                DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.Success"), clsResources.GetMessage("messages.frmChinhSachGia.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    ResetChinhSachGiaSauKhiTao();
                    grdChinhSachGia.DataSource = _quanLyThuocDao.LoadChinhSachGia();
                    btnLuu.Enabled = false;
                }
                return;
            }
            else
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.Error"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ThongTinNhapKho thongTinNhapKho = BuildThongTinNhapKho();
            //if (thongTinNhapKho != null)
            //{
            //    List<ThongTinNhapKhoDetail> listThongTinNhapKhoDetail = BuildThongTinNhapKhoDetail(thongTinNhapKho.MaNhapKho);
            //    if (_thongTinNhapKhoDao.SaveThongTinNhapKho(thongTinNhapKho, listThongTinNhapKhoDetail))
            //    {
            //        DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.Success"), clsResources.GetMessage("messages.frmChinhSachGia.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        if (result == DialogResult.OK)
            //        {
            //            //grdToaThuoc.Rows.Clear();
            //            //grdToaThuoc.Rows.Add(1);
            //        }
            //        return;
            //    }
            //    else {
            //        MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.Error"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = false;
            btnChiTietChinhSachGia.Enabled = false;
            btnLuu.Enabled = true;
            TaoMoiChinhSachGia();
            DeselectCheckbox();
        }

        private void ResetChinhSachGiaSauKhiTao()
        {
            txtMaChinhSachGia.Text = _quanLyThuocDao.GenerateNewMaChinhSachGia();
            txtTenChinhSachGia.Text = "";
            cbHoatDong.Checked = false;
            dtpThoiGianBatDau.Value = DateTime.Now;
            dtpThoiGianKetThuc.Value = DateTime.Now;
            dtpNgayNgungHoatDong.Value = DateTime.Now;
        }
        private void TaoMoiChinhSachGia()
        {
            txtMaChinhSachGia.Text = _quanLyThuocDao.GenerateNewMaChinhSachGia();
            txtTenChinhSachGia.Text = "";
            cbHoatDong.Checked = false;
            dtpNgayNgungHoatDong.Value = DateTime.Now;
            dtpThoiGianKetThuc.Value = DateTime.Now;
            dtpThoiGianBatDau.Value = DateTime.Now;
            cbHoatDong.Checked = true;
            SetFormEnable(true);
        }

        private void dtpThoiGianBatDau_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpThoiGianKetThuc_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpNgayNgungHoatDong_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtTenChinhSachGia.Text.Trim() == "")
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.TenChinhSachGia"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtpThoiGianBatDau.Value > dtpThoiGianKetThuc.Value)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.NgayBatDau"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpThoiGianKetThuc.Value < dtpThoiGianBatDau.Value)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.NgayKetThuc"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpNgayNgungHoatDong.Value < dtpThoiGianBatDau.Value || dtpNgayNgungHoatDong.Value > dtpThoiGianKetThuc.Value)
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.NgayHetHan"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_quanLyThuocDao.CheckOverlapChinhSachGia(txtMaChinhSachGia.Text, dtpThoiGianBatDau.Value.Date, dtpNgayNgungHoatDong.Value.Date) > 0)
            {
                MessageBox.Show("Chính sách giá trùng lặp thời gian với chính sách giá khác", clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            ChinhSachGiaDT chinhSachGia = new ChinhSachGiaDT();
            chinhSachGia.MaChinhSachGia = txtMaChinhSachGia.Text;
            chinhSachGia.TenChinhSachGia = txtTenChinhSachGia.Text;
            chinhSachGia.HoatDong = cbHoatDong.Checked;
            chinhSachGia.ThoiGianBatDau = dtpThoiGianBatDau.Value.Date;
            chinhSachGia.ThoiGianKetThuc = dtpThoiGianKetThuc.Value.Date;
            chinhSachGia.NgayNgungHoatDong = dtpNgayNgungHoatDong.Value.Date;
         //   chinhSachGia.CreatedDate = DateTime.Now;
            chinhSachGia.LastUpdatedDate = DateTime.Now;
           // chinhSachGia.CreatedBy = ttNhanVien.FullName;
            chinhSachGia.LastUpdatedBy = clsSystemConfig.UserName;

            if (_quanLyThuocDao.UpdateChinhSachGia(chinhSachGia))
            {
                DialogResult result = MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.Success"), clsResources.GetMessage("messages.frmChinhSachGia.SuccessTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                   // ResetChinhSachGiaSauKhiTao();
                    grdChinhSachGia.DataSource = _quanLyThuocDao.LoadChinhSachGia();
                    //if (currentRowIndex >= 0)
                    //{
                    //    DeselectOrtherCheckbox(currentRowIndex);
                    //    grdChinhSachGia.Rows[currentCell.RowIndex].Cells[0].Value = true;
                    //}
                    DeselectCheckbox();
                    btnLuu.Enabled = false;
                }
                return;
            }
            else
            {
                MessageBox.Show(clsResources.GetMessage("messages.frmChinhSachGia.Error"), clsResources.GetMessage("messages.frmChinhSachGia.ErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnChiTietChinhSachGia_Click(object sender, EventArgs e)
        {
            frmChinhSachGiaChiTiet frmChiTiet = new frmChinhSachGiaChiTiet();
            frmChiTiet.SetMaChinhSachGia(txtMaChinhSachGia.Text);
            frmChiTiet.SetTenChinhSachGia(txtTenChinhSachGia.Text);
            if (dtpNgayNgungHoatDong.Value <= DateTime.Now.Date || cbHoatDong.Checked == false)
            {
                frmChiTiet.SetRealOnlyForm(true);
            }
            else
            {
                frmChiTiet.SetRealOnlyForm(false);
            }
            frmChiTiet.Show();
        }

        private void cbHoatDong_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHoatDong.Checked == false)
            {
                dtpThoiGianKetThuc.Value = dtpNgayNgungHoatDong.Value;
            }
        }
     

        

 

      
    }
}