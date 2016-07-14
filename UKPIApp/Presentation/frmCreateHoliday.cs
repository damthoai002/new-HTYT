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
using UKPI.BusinessObject.Authenticate;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class FrmCreateHoliday : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmCreateHoliday));

        private clsBaseBO _bo = new clsBaseBO();
        private CreateTimesheetBo _ctsBo = new CreateTimesheetBo();
        private readonly ClsNgayNghiBo _ngayNghiBo = new ClsNgayNghiBo();

        private readonly clsCommon _common = new clsCommon();


        #endregion

        #region Constructors

        public FrmCreateHoliday()
        {

            InitializeComponent();
            clsTitleManager.InitTitle(this);
            BindTimes();
            BindLoaiNgayNghi();
            BindNgayNghi();

        }

        private void BindNgayNghi()
        {
            try
            {

                int nam = Int32.Parse(txtNamSearch.Text);
                grdNgayNghi.DataSource = _ngayNghiBo.GetNgayNghi(nam);
                ProcessDatarows();

                btnUnActive.Enabled = grdNgayNghi.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }

            // var nvTb = new 
        }

        public void ProcessDatarows()
        {

            try
            {
                if (grdNgayNghi.Rows.Count > 0)
                {
                    for (int i = 0; i < grdNgayNghi.Rows.Count; i++)
                    {
                        if (!(bool)grdNgayNghi.Rows[i].Cells["Is_Active"].Value)
                        {
                            grdNgayNghi.Rows[i].ReadOnly = true;
                            grdNgayNghi.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        private void BindLoaiNgayNghi()
        {
            //txtTruongNhom.Text = clsSystemConfig.FullName;
            cboLoaiNgayNghi.ValueMember = "value";
            cboLoaiNgayNghi.DisplayMember = "name";
            cboLoaiNgayNghi.DataSource = _common.GetLoaiNgayNghi();
            BuildFromToDay();

        }

        private void BuildFromToDay()
        {
            if (cboLoaiNgayNghi.SelectedValue.ToString() == "0")
            {
                DateTime toDate = new DateTime(DateTime.Now.Year, 12, 31);

                dtpFromDay.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString(clsCommon.ApproveTimesheet.DateFormatForDtp);
                dtpToDay.Text = toDate.ToString(clsCommon.ApproveTimesheet.DateFormatForDtp);

                dtpToDay.Enabled = false;
                dtpFromDay.Enabled = false;
                txtNam.Enabled = false;

            }
            else
            {
                dtpFromDay.Text = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatForDtp);
                dtpToDay.Text = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatForDtp);

                dtpToDay.Enabled = true;
                dtpFromDay.Enabled = true;
                txtNam.Enabled = true;

            }
        }


        private void BindTimes()
        {
            dtpFromDay.Format = DateTimePickerFormat.Custom;
            dtpFromDay.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;

            dtpToDay.Format = DateTimePickerFormat.Custom;
            dtpToDay.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;
            txtNam.Text = DateTime.Now.Year.ToString();
            txtNamSearch.Text = DateTime.Now.Year.ToString();
            cboLoaiNgayNghi.DropDownStyle = ComboBoxStyle.DropDownList;

            //dtpYear.Format = DateTimePickerFormat.Custom;
            //dtpYear.CustomFormat = "yyyy";
            //dtpYear.ShowUpDown = true; 
        }


        #endregion

        #region Handle events

        public bool CheckValidDate()
        {

            return true;
        }

        #endregion
        public string GetItemToUnActive()
        {
            const string strSysId = "";

            var table = grdNgayNghi.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => row["Check"].ToString() == "1").Aggregate(strSysId, (current, row) => current + "," + row[clsCommon.Group.MaNhom].ToString());
        }

        public List<ClsTeam> GetTeamToUnActive()
        {
            var lstLichLamViec = new List<ClsTeam>();

            var table = grdNgayNghi.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;

            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where row["Check"].ToString() == "1"
                                    select new ClsTeam
                                    {
                                        TruongNhomId = row[clsCommon.Group.MaTruongNhom].ToString(),
                                        NhomId = row[clsCommon.Group.MaNhom].ToString()
                                    });

            return lstLichLamViec;
        }
       

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatedData())
                {
                    string loaiNgayNghi = cboLoaiNgayNghi.SelectedValue.ToString();
                    int nam = Int32.Parse(txtNam.Text);
                    DateTime fromDate = clsCommon.FormatStringToDateTime(dtpFromDay.Text, clsCommon.ApproveTimesheet.DateFormatDisplay);
                    DateTime toDate = clsCommon.FormatStringToDateTime(dtpToDay.Text, clsCommon.ApproveTimesheet.DateFormatDisplay);
                    string mota = txtDienGiai.Text;

                    if (loaiNgayNghi == "0")
                    {
                        _ngayNghiBo.TaoNgayNghiChuNhat(clsSystemConfig.MaNhanVien.ToString(), nam, mota);
                    }
                    else if (loaiNgayNghi == "2")
                    {
                        _ngayNghiBo.TaoNgayNghiThu7(clsSystemConfig.MaNhanVien.ToString(), nam, mota);
                    }
                    else
                    {
                        _ngayNghiBo.TaoNgayNghiTrongNam(loaiNgayNghi, fromDate, toDate, mota, clsSystemConfig.MaNhanVien.ToString());
                    }

                    MessageBox.Show(clsResources.GetMessage("messages.FrmCreateHoliday.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindNgayNghi();
                    txtDienGiai.Text = "";
                }

            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateHoliday.save.UnSuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }




        public bool CheckValidDateDataToSearch()
        {
            return true;
        }


        private void cboLoaiNgayNghi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildFromToDay();
        }

        private void dtpFromDay_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDay.Text != "")
            {
                dtpToDay.Text = clsCommon.FormatStringToDateTime(dtpFromDay.Text, clsCommon.ApproveTimesheet.DateFormatDisplay).ToString(clsCommon.ApproveTimesheet.DateFormatForDtp);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatedDataSearch())
                {
                    BindNgayNghi();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }


        }

        public bool ValidatedData()
        {
            erp.Clear();
            string year = txtNam.Text;
            if (!_common.IsDigit(year) || year.Length == 0)
            {
                erp.SetError(txtNam, clsResources.GetMessage("errors.number", txtNam.Text));
                txtNam.Focus();
                return false;
            }
            DateTime fromdate = dtpFromDay.Value;
            DateTime toDate = dtpToDay.Value;
  
            if (fromdate.Date > toDate.Date)
            {
                erp.SetError(dtpToDay, clsResources.GetMessage("messages.FrmCreateLeavePlan.DateValid", dtpFromDay.Text, dtpToDay.Text));
                dtpToDay.Focus();
                return false;
            }

            string dienGiai = txtDienGiai.Text;
            if (dienGiai.Length == 0)
            {
                erp.SetError(txtDienGiai, clsResources.GetMessage("errors.number", txtDienGiai.Text));
                txtDienGiai.Focus();
                return false;
            }
            return true;
        }
        public bool ValidatedDataSearch()
        {
            erp.Clear();
            string year = txtNamSearch.Text;
            if (!_common.IsDigit(year) || year.Length == 0)
            {
                erp.SetError(txtNamSearch, clsResources.GetMessage("", txtNamSearch.Text));
                txtNamSearch.Focus();
                return false;
            }
            return true;
        }

        private void btnUnActive_Click(object sender, EventArgs e)
        {
            try
            {
                string sysId = GetNgayNghiToNgungSd();
                if (sysId.Length > 0)
                {
                    sysId = sysId.Substring(1, sysId.Length - 1);

                    _ngayNghiBo.NgungSuDungNgayNghi(sysId);
                    MessageBox.Show(clsResources.GetMessage("messages.FrmCreateHoliday.NgungSdsuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindNgayNghi();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateHoliday.NgungSdUnsuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log.Error(ex.Message, ex);
            }
        }

        public string GetNgayNghiToNgungSd()
        {
            const string strSysId = "";

            var table = grdNgayNghi.DataSource as System.Data.DataTable;
            return table == null ? strSysId : table.Rows.Cast<DataRow>().Where(row => (row["Check"].ToString() == "1")).Aggregate(strSysId, (current, row) => current + "," + row[ChamCongLichLamViecDAO.SysID].ToString());

        }


    }
}