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
    public partial class frmChotTonKhoDetail : Form
    {


        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmChotTonKhoDetail));

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
        List<ChotTonKhoDetail> listCHotTonKhoDetail = new List<ChotTonKhoDetail>();
        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;



        //Parent component
        frmChotTonKho parentForm;
        private ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private ChotTonKhoDao _chotTonKhoDao = new ChotTonKhoDao();
        int currentRowIndex = -1;
        ChotTonKhoHeader currentChotTonKhoHeader;
        List<TrangThai> listTrangThai = new List<TrangThai>();
        public frmChotTonKhoDetail()
        {

            InitializeComponent();
            BindKhoThuoc();
            grdBenhNhan.AutoGenerateColumns = false;
           // clsTitleManager.InitTitle(this);
            this.Text = "CHỐT TỒN KHO CHI TIẾT";
           // btnTaoPhieu.Visible = false;
            grdBenhNhan.CellValueChanged += grdBenhNhan_CellValueChanged;
            this.FormClosing +=frmChotTonKhoDetail_FormClosing;
        }

        private void BindKhoThuoc()
        {
            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
            cbbKhoThuoc.ValueMember = "RoomId";
            cbbKhoThuoc.DisplayMember = "RoomName";
            cbbKhoThuoc.DataSource = listPhongKham;
            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            int currentIndex = listPhongKham.FindIndex(a => a.RoomID == currentKho);
            cbbKhoThuoc.SelectedIndex = currentIndex;
        }

        private void frmChotTonKhoDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(parentForm != null)
            {
                parentForm.ReloadSearchResult();
            }
        }
        private void SetValueToForm()
        {
            if (currentChotTonKhoHeader == null)
            {
                //List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
                txtMaCHotTonKho.Text = System.Configuration.ConfigurationManager.AppSettings["MaCHotTon"] + DateTime.Now.ToString("yyyyMMddHHmmss");
                //string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
                //var firstOrDefault = listPhongKham.FirstOrDefault(a => a.RoomID == currentKho);
                //if (firstOrDefault != null)
                //    txtKho.Text = firstOrDefault.RoomName;
                //;
                txtNguoiXacNhan.Text = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
                txtNguoiDieuChinh.Text = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
                ccbTrangThai.SelectedIndex = 0;
                btnTinhTonKho.Enabled = false;
                btnChotTon.Enabled = false;
                btnXacNhan.Enabled = false;
            }
            else
            {
                txtMaCHotTonKho.Text = currentChotTonKhoHeader.MaChotTonKho;
               // txtKho.Text = currentChotTonKhoHeader.TenKho;
                txtDienGiai.Text = currentChotTonKhoHeader.DienGiai;
                dpNgayTaoPhieu.Value = currentChotTonKhoHeader.NgayTaoPhieu;
                ccbTrangThai.SelectedText = currentChotTonKhoHeader.Status;
                ccbTrangThai.SelectedIndex = listTrangThai.FindIndex(f => f.TenTrangThai == currentChotTonKhoHeader.Status);
                txtNguoiXacNhan.Text = currentChotTonKhoHeader.NguoiXacNhan;
                txtNguoiDieuChinh.Text = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
                if(currentChotTonKhoHeader.CurrentWorkflow == 0) //moi luu
                {
                    btnTinhTonKho.Enabled = true;
                    btnExcel.Enabled = false;
                    btnXacNhan.Enabled = false;
                    btnChotTon.Enabled = false;

                    txtDienGiai.ReadOnly = false;
                    dpNgayTaoPhieu.Enabled = true;
                    ccbTrangThai.Enabled = true;
                }
                else if(currentChotTonKhoHeader.CurrentWorkflow == 1)// da tinh chot ton
                {
                    btnLuu.Enabled = true;
                    btnTinhTonKho.Enabled = false;
                   btnExcel.Enabled = true;
                    btnXacNhan.Enabled = true;
                    btnChotTon.Enabled = false;

                    txtDienGiai.ReadOnly = false;
                    dpNgayTaoPhieu.Enabled = true;
                    ccbTrangThai.Enabled = true;

                    listCHotTonKhoDetail = _chotTonKhoDao.LoadChotTonKhoDetail(currentChotTonKhoHeader.MaChotTonKho);
                    grdBenhNhan.DataSource = listCHotTonKhoDetail;
                }
                else if (currentChotTonKhoHeader.CurrentWorkflow == 2)// da xac nhan
                {
                    btnLuu.Enabled = true;
                    btnTinhTonKho.Enabled = false;
                    btnExcel.Enabled = true;
                    btnXacNhan.Enabled = true;
                    btnChotTon.Enabled = true;
                    txtDienGiai.ReadOnly = false;
                    dpNgayTaoPhieu.Enabled = true;
                    ccbTrangThai.Enabled = true;

                    listCHotTonKhoDetail = _chotTonKhoDao.LoadChotTonKhoDetail(currentChotTonKhoHeader.MaChotTonKho);
                    grdBenhNhan.DataSource = listCHotTonKhoDetail;
                }
                else if(currentChotTonKhoHeader.CurrentWorkflow == 3)
                {
                    btnLuu.Enabled = false;
                    btnTinhTonKho.Enabled = false;
                    btnExcel.Enabled = true;
                    btnXacNhan.Enabled = false;
                    btnChotTon.Enabled = false;
                    txtDienGiai.ReadOnly = true;
                    dpNgayTaoPhieu.Enabled = false;
                    ccbTrangThai.Enabled = false;
                    listCHotTonKhoDetail = _chotTonKhoDao.LoadChotTonKhoDetail(currentChotTonKhoHeader.MaChotTonKho);
                    grdBenhNhan.DataSource = listCHotTonKhoDetail;
                }
            }
        }
        public void SetCurrentChotTonKhoHeader(ChotTonKhoHeader value)
        {
            currentChotTonKhoHeader = value;
            SetDefaultValue();
            SetValueToForm();
        }
        private void SetDefaultValue()
        {
            string listTrangThaiConfig = System.Configuration.ConfigurationManager.AppSettings["ListTrangThai"];
            string[] list = listTrangThaiConfig.Split(',');
            for (int i = 0; i < list.Length; i++) {
                listTrangThai.Add(new TrangThai { MaTrangThai = list[i], TenTrangThai = list[i] });
             }
            ccbTrangThai.DataSource = listTrangThai;
        }
        public void SetParentForm(frmChotTonKho parent)
        {
            this.parentForm = parent;
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
            
        }
        private ChotTonKhoHeader BuildChotTonKhoHeader()
        {
            ChotTonKhoHeader ctkh = new ChotTonKhoHeader();
            ctkh.MaChotTonKho = txtMaCHotTonKho.Text;
            ctkh.IsDeleted = false;
            ctkh.TenKho = cbbKhoThuoc.Text;
            ctkh.MaKho = cbbKhoThuoc.SelectedValue.ToString();
            ctkh.DienGiai = txtDienGiai.Text;
            ctkh.NgayTaoPhieu = dpNgayTaoPhieu.Value;
            ctkh.NguoiXacNhan = txtNguoiXacNhan.Text;
            ctkh.NguoiDieuChinh = txtNguoiDieuChinh.Text;
            ctkh.Status = ((TrangThai)ccbTrangThai.SelectedItem).TenTrangThai;
            if(currentChotTonKhoHeader == null)
            {
                ctkh.CreatedDate = DateTime.Now;
                ctkh.ModifiedDate = DateTime.Now;
                ctkh.Creator = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
                ctkh.LastModifier = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
            }
            else
            {
                ctkh.CreatedDate = currentChotTonKhoHeader.CreatedDate;
                ctkh.ModifiedDate = DateTime.Now;
                ctkh.Creator = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
                ctkh.LastModifier = clsSystemConfig.UserName + "-" + clsSystemConfig.FullName;
            }
            
            return ctkh;
            
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            ChotTonKhoHeader ctkh = BuildChotTonKhoHeader();
            if (ctkh != null)
            {
                if (_chotTonKhoDao.ProcessChotTonKhoHeader(ctkh))
                {
                    btnTinhTonKho.Enabled = true;
                    DialogResult result = MessageBox.Show("Dữ liệu đã được cập nhật thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Có lỗi trong quá trình cập nhật dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnTinhTonKho_Click(object sender, EventArgs e)
        {
            listCHotTonKhoDetail = _chotTonKhoDao.ProcessChotTonKhoDetailTinhTonKho(txtMaCHotTonKho.Text, cbbKhoThuoc.SelectedValue.ToString());
            if(listCHotTonKhoDetail ==  null)
            {
                DialogResult result = MessageBox.Show("Có lỗi trong quá trình tính chốt tồn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (listCHotTonKhoDetail != null && listCHotTonKhoDetail.Count == 0)
            {
                DialogResult result = MessageBox.Show("Không có thuốc tồn kho để chốt tồn", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (listCHotTonKhoDetail != null && listCHotTonKhoDetail.Count > 0)
            {
                grdBenhNhan.DataSource = listCHotTonKhoDetail;
                btnXacNhan.Enabled = true;
                btnTinhTonKho.Enabled = false;
            }
            
        }
        private void grdBenhNhan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = this.grdBenhNhan.CurrentCell;
            // thay doi so luong thuoc
            if (currentCell != null && currentCell.ColumnIndex == 11)
            {
                long currentSoLuongThucTe = 0;
                long currentSoLuongTon = 0;
                string currentLoaiChenhLech = "";
                try
                {
                    currentSoLuongThucTe = this.grdBenhNhan[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? long.Parse(this.grdBenhNhan[currentCell.ColumnIndex, currentCell.RowIndex].Value.ToString()) : 0;
                    if (currentSoLuongThucTe <= 0)
                    {
                        DialogResult result = MessageBox.Show("Số lượng thực tế không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    currentSoLuongTon = this.grdBenhNhan[currentCell.ColumnIndex, currentCell.RowIndex].Value != null ? long.Parse(this.grdBenhNhan[currentCell.ColumnIndex - 1, currentCell.RowIndex].Value.ToString()) : 0;
                    long doLech = currentSoLuongThucTe - currentSoLuongTon;
                    if (doLech > 0)
                    {
                        currentLoaiChenhLech = "+";
                    }
                    else if (doLech == 0)
                    {
                        currentLoaiChenhLech = "=";
                    }
                    else
                    {
                        currentLoaiChenhLech = "-";
                    }
                    this.grdBenhNhan[currentCell.ColumnIndex + 1, currentCell.RowIndex].Value = Math.Abs(doLech);
                    this.grdBenhNhan[currentCell.ColumnIndex + 2, currentCell.RowIndex].Value = currentLoaiChenhLech;
                    return;
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Số lượng thực tế không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
                return;
        }

        private List<ChotTonKhoDetail> BuildListXacNhanCHotTonKhoDetail()
        {
            List<ChotTonKhoDetail> list = new List<ChotTonKhoDetail>();
            if (grdBenhNhan.Rows.Count > 0)
            {
                for (int i = 0; i < grdBenhNhan.Rows.Count; i++)
                {
                    ChotTonKhoDetail ctkd = new ChotTonKhoDetail();
                    //string.IsNullOrEmpty((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue) ? 0 : long.Parse((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue);
                    ctkd.Id = long.Parse((string)grdBenhNhan.Rows[i].Cells[0].FormattedValue);
                   // ctkd.SoLuongTon = long.Parse((string)grdBenhNhan.Rows[i].Cells[10].FormattedValue);
                    ctkd.SoLuongThucTe = long.Parse((string)grdBenhNhan.Rows[i].Cells[11].FormattedValue);
                    ctkd.SoLuongChenhLech = long.Parse((string)grdBenhNhan.Rows[i].Cells[12].FormattedValue);
                    ctkd.LoaiChenhLech = (string)grdBenhNhan.Rows[i].Cells[13].FormattedValue;
                   // ctkd.MaNhapKhoDetail = long.Parse((string)grdBenhNhan.Rows[i].Cells[14].FormattedValue);
                    list.Add(ctkd);
                }
            }
            return list;
        }

        private List<ChotTonKhoDetail> BuildListChotTonCHotTonKhoDetail()
        {
            List<ChotTonKhoDetail> list = new List<ChotTonKhoDetail>();
            if (grdBenhNhan.Rows.Count > 0)
            {
                for (int i = 0; i < grdBenhNhan.Rows.Count; i++)
                {
                    ChotTonKhoDetail ctkd = new ChotTonKhoDetail();
                    //string.IsNullOrEmpty((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue) ? 0 : long.Parse((string)grdToaThuoc.Rows[i].Cells[1].FormattedValue);
                    ctkd.Id = long.Parse((string)grdBenhNhan.Rows[i].Cells[0].FormattedValue);
                    ctkd.SoLuongTon = long.Parse((string)grdBenhNhan.Rows[i].Cells[10].FormattedValue);
                    ctkd.SoLuongThucTe = long.Parse((string)grdBenhNhan.Rows[i].Cells[11].FormattedValue);
                    ctkd.SoLuongChenhLech = long.Parse((string)grdBenhNhan.Rows[i].Cells[12].FormattedValue);
                    ctkd.LoaiChenhLech = (string)grdBenhNhan.Rows[i].Cells[13].FormattedValue;
                    ctkd.MaNhapKhoDetail = long.Parse((string)grdBenhNhan.Rows[i].Cells[14].FormattedValue);
                    list.Add(ctkd);
                }
            }
            return list;
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            List<ChotTonKhoDetail> list = BuildListXacNhanCHotTonKhoDetail();
            if(_chotTonKhoDao.ProcessChotTonKhoDetailXacNhan(list,txtMaCHotTonKho.Text) == true)
            {
                DialogResult result = MessageBox.Show("Xác nhận chốt tốn kho thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnChotTon.Enabled = true;
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Có lỗi trong khi xác nhận chốt tốn kho", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnChotTon_Click(object sender, EventArgs e)
        {
            List<ChotTonKhoDetail> list = BuildListChotTonCHotTonKhoDetail();
            if (_chotTonKhoDao.ProcessChotTonKhoDetailChotTon(list, txtMaCHotTonKho.Text) == true)
            {
                DialogResult result = MessageBox.Show("Chốt tốn kho thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnChotTon.Enabled = false;
                btnLuu.Enabled = false;
                btnXacNhan.Enabled = false;
                btnTinhTonKho.Enabled = false;
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Có lỗi trong khi chốt tốn kho", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void Export()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var lstChotTon = grdBenhNhan.DataSource as List<ChotTonKhoDetail>;
                if (lstChotTon == null)
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
                    // Execute export
                    string strMaChotTon = txtMaCHotTonKho.Text;
                    string strChiTiet = txtDienGiai.Text;
                    string stringNgay = dpNgayTaoPhieu.Text;

                    var exporter = new EditApproveTimesheetExporter(true);
                    exporter.AddExportTable(lstChotTon, strMaChotTon, strChiTiet, stringNgay);
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Export();
        }


    }
}