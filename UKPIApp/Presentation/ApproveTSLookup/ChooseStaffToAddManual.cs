using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Windows.Forms;
using UKPI.BusinessObject;
using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.Presentation.ApproveTSLookup
{
    public partial class ChooseStaffToAddManual : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ChooseStaffToAddManual));
        public ClsCreateTimesheet ObjTimesheet { get; set; }
        private readonly clsCommon _common = new clsCommon();
        private readonly CreateTimesheetBo _createTimesheetBo = new CreateTimesheetBo();

        public ChooseStaffToAddManual()
        {
            InitializeComponent();

        }
        public ChooseStaffToAddManual(ClsCreateTimesheet ts)
        {
            this.ObjTimesheet = ts;
            InitializeComponent();
            BindData();
            BindNhanVienInNhom();
        }

        private void BindData()
        {
            lblTuan.Text = ObjTimesheet.TuanLamViec;
            lblTruongNhom.Text = ObjTimesheet.TenTruongNhom + " - " + ObjTimesheet.TruongNhomId;
            lblNhom.Text = ObjTimesheet.TenNhom;
            lblNgayLamViec.Text = new DateTime(Int32.Parse( ObjTimesheet.NgayLamViec.Substring(0,4)),Int32.Parse( ObjTimesheet.NgayLamViec.Substring(4,2)), Int32.Parse( ObjTimesheet.NgayLamViec.Substring(6,2))).ToString(clsCommon.ApproveTimesheet.DateFormatDisplay);
            lblOutsource.Text = ObjTimesheet.IsOutsource;


        }


        private void BindNhanVienInNhom()
        {
            try
            {
                string tenNhanVien = txtTenNhanVien.Text;
                string hoNhanVien = txtHoNhanVien.Text;
                DataTable tb = new DataTable();
                if (tenNhanVien != "" || hoNhanVien != "")
                {
                    tb = _createTimesheetBo.GetNhanVienNhomAvailable(ObjTimesheet, tenNhanVien, hoNhanVien);
                    grdNhanVien.DataSource = tb;
                }
                else
                {
                    tb = _createTimesheetBo.GetNhanVienNhomAvailable(ObjTimesheet);
                    grdNhanVien.DataSource = tb;
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatedData())
                {
                    BindNhanVienInNhom();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private bool ValidatedData()
        {
            ep.Clear();

            string tenNhanVien = txtTenNhanVien.Text;
            if (!_common.IsLetterAndDigitExceptWc(tenNhanVien))
            {
                ep.SetError(txtTenNhanVien, clsResources.GetMessage("errors.string.specialChar", txtTenNhanVien.Text));
                txtTenNhanVien.Focus();
                return false;
            }
            string hoNhanVien = txtHoNhanVien.Text;
            if (!_common.IsLetterAndDigitExceptWc(hoNhanVien))
            {
                ep.SetError(txtHoNhanVien, clsResources.GetMessage("errors.string.specialChar", txtHoNhanVien.Text));
                txtHoNhanVien.Focus();
                return false;
            }


            return true;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

            try
            {
                List<ClsCreateTimesheet> lstTimesheets = GetNhanVienChecked();

                if (lstTimesheets.Count > 0)
                {
                    _createTimesheetBo.AddOneTimesheet(lstTimesheets);
                    MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheetManual.SaveSuccessful"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindNhanVienInNhom();

                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheetManual.NoDataCheck"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        private List<ClsCreateTimesheet> GetNhanVienChecked()
        {
            List<ClsCreateTimesheet> lstNhanVien = new List<ClsCreateTimesheet>();

            var table = grdNhanVien.DataSource as System.Data.DataTable;
            if (table == null) return lstNhanVien;
            lstNhanVien.AddRange(from DataRow row in table.Rows
                                 where row["Check"].ToString() == "1"
                                 select new ClsCreateTimesheet
                                    {
                                        NhanVienId = row[ChamCongLichLamViecDAO.NhanVien_Id].ToString(),
                                        NhanVienTen = row[ChamCongLichLamViecDAO.NhanVien_Ten].ToString(),
                                        TruongNhomId = ObjTimesheet.TruongNhomId,
                                        TenTruongNhom = ObjTimesheet.TenTruongNhom,
                                        NhomId = ObjTimesheet.NhomId,
                                        TuanLamViec = ObjTimesheet.TuanLamViec,
                                        NgayLamViec = ObjTimesheet.NgayLamViec
                                    });


            return lstNhanVien;

        }

        private void grdNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewColumn = grdNhanVien.Columns["btnAddUser"];
            if (dataGridViewColumn != null && e.ColumnIndex == dataGridViewColumn.Index)
            {
                //Do Something with your button.
                //MessageBox.Show(dgvNVCC.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString());

                
                //var frm = new frmAddStaffUser(nhanVienId);
                //frmAddStaffUser.Show();

            }
        }

    }
}
