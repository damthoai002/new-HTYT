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
using System.Windows.Forms.VisualStyles;
using Excel;
using FPT.Component.ExcelPlus;
using UKPI.BusinessObject;
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class FrmCreateTimesheetManual : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmCreateTimesheetManual));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        readonly System.Data.DataTable _dt = null;
        CurrencyManager _manager = null;
        private int _checkRowsCount = 0;
        CreatedTimesheet bo = new CreatedTimesheet();
        DataTable dt = null;
        private DataTable dtNhom = null;


        public bool Success { get; set; }

        // Declare private fields
        private CreateTimesheetBo _createTimesheetBo = new CreateTimesheetBo();

        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors

        public FrmCreateTimesheetManual()
        {

            InitializeComponent();



            clsTitleManager.InitTitle(this);
            SetDefauldValue();
            BindWeekToCreateTimesheet();
            BindNhom();



            BindNhanVien();
            //MessageBox.Show("fdsafa");

            // Save original columns
            _originalColumns = new DataGridViewColumn[grdTimesheets.Columns.Count];
            grdTimesheets.Columns.CopyTo(_originalColumns, 0);
            grdTimesheets.Sorted += grdStores_Sorted;
            InitData();
        }

        private void BindNhom()
        {
            var timesheet = new CreateTimesheetBo();
            dtNhom = timesheet.GetNhomByNhomTruong(clsSystemConfig.UserName);
            cboNhom.ValueMember = clsCommon.CreateTimesheet.NhomId;
            cboNhom.DisplayMember = clsCommon.CreateTimesheet.Nhom;
            cboNhom.DataSource = dtNhom;
            cboNhom.DropDownStyle = ComboBoxStyle.DropDownList;
            for (var i = 0; i < dtNhom.Rows.Count; i++)
            {
                if (dtNhom.Rows[i][clsCommon.CreateTimesheet.NhomId].ToString() == cboNhom.SelectedValue.ToString())
                {
                    txtOutsource.Text = (bool)dtNhom.Rows[i][clsCommon.CreateTimesheet.Outsource] == true ? "Yes" : "No";
                }
            }

        }

        private void InitData()
        {
            dt = bo.GetSchemaTable();
            _manager = (CurrencyManager)this.BindingContext[dt];
            _manager.AddNew();
            _manager.Position = 0;
            exp.DataSource = dt;
        }



        void grdStores_Sorted(object sender, EventArgs e)
        {

        }


        private void SetDefauldValue()
        {
            //var tsBo = new CreateTimesheetBo();
            //var ts = new ClsCreateTimesheet(tsBo.GetNhomTruong(clsSystemConfig.UserName));

            //txtL0PheDuyet.Text = ts.L0XacNhanTen;
            //txtL0PheDuyetMa.Text = ts.L0XacNhanId.ToString();
            //txtL1PheDuyet.Text = ts.L1XacNhanTen;
            //txtL1PheDuyetMa.Text = ts.L1XacNhanId.ToString();
            //txtL2PheDuyet.Text = ts.L2XacNhanTen;
            //txtL2PheDuyetMa.Text = ts.L2XacNhanId.ToString();
            //txtL3PheDuyet.Text = ts.L3XacNhanTen;
            //txtL3PheDuyetMa.Text = ts.L3XacNhanId.ToString();
            //txtL4PheDuyet.Text = ts.L4XacNhanTen;
            //txtL4PheDuyetMa.Text = ts.L4XacNhanId.ToString();

            //txtMaTruongNhom.Text = ts.TruongNhomId.ToString();
            //txtTruongNhom.Text = ts.TenTruongNhom;


            //txtCurrentWeek.Text = clsCommon.GetWeek(DateTime.Now).ToString();

            var tsBo = new CreateTimesheetBo();
            ClsCreateTimesheet ts;
            var tbts = tsBo.GetNhomTruong(clsSystemConfig.UserName);
            if (tbts.Rows.Count > 0)
            {
                ts = new ClsCreateTimesheet(tbts);
            }
            else
            {
                ts = new ClsCreateTimesheet();
            }


          
            txtL1PheDuyet.Text = ts.L1XacNhanTen;
            txtL1PheDuyetMa.Text = ts.L1XacNhanId;
            txtL2PheDuyet.Text = ts.L2XacNhanTen;
            txtL2PheDuyetMa.Text = ts.L2XacNhanId;
            txtL3PheDuyet.Text = ts.L3XacNhanTen;
            txtL3PheDuyetMa.Text = ts.L3XacNhanId;
            txtMaTruongNhom.Text = ts.TruongNhomId;
            txtTruongNhom.Text = ts.TenTruongNhom;


            txtCurrentWeek.Text = clsCommon.GetWeek(DateTime.Now).ToString();


        }

        public void BindWeekToCreateTimesheet()
        {
            //lstWeek.Add(weekno);
            dtpNgay.Format = DateTimePickerFormat.Custom;
            dtpNgay.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;
            dtpNgay.Value = DateTime.Now.AddDays(1);

        }





        private void InitControls()
        {
            try
            {
                this.grdTimesheets.AutoGenerateColumns = false;
                // Init controls' status
                //txtDistributors.Text = DISTRIBUTORS_DEFAUT;

                // Read file config
                this.OnGridDataSourceChanged();
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void OnGridDataSourceChanged()
        {

        }


        public string ValidatedDateToSave()
        {
            var strError = "";

            var table = grdTimesheets.DataSource as System.Data.DataTable;
            if (table == null) return strError;


            var lstError = (from DataRow row in table.Rows
                            where (clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Vao_L1].ToString()) == "" ||
                            clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Ra_L1].ToString()) == "") &&
                             (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false
                            select String.Format(clsResources.GetMessage("message.FrmApproveTimesheet.LineInvalidTime"), row[ChamCongLichLamViecDAO.Vao_L1].ToString(), row[ChamCongLichLamViecDAO.Ra_L1].ToString()) + "\n"
                          );

            return lstError.Aggregate(strError, (current, strEr) => current + current);
        }

        public List<ClsLichLamViec> GetDataToSave()
        {
            var lstLichLamViec = new List<ClsLichLamViec>();

            var table = grdTimesheets.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;



            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where row["Check"].ToString() == "1" && (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == false
                                    select new ClsLichLamViec
                                    {
                                        SysId = long.Parse(row[ChamCongLichLamViecDAO.SysID].ToString())

                                    });

            return lstLichLamViec;
        }


        #endregion

        #region Handle events


        private void frmEditStore_Load(object sender, EventArgs e)
        {
            InitControls();
        }




        /// <summary>
        /// check exchange grid when close form
        /// Creator: Sonlv
        /// Create Date: 23/03/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditStore_FormClosing(object sender, FormClosingEventArgs e)
        {

        }





        #endregion

        public string GetItemToGetTimesheet()
        {
            const string strSysId = "";

            var table = grdTimesheets.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false && (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == false).Aggregate(strSysId, (current, row) => current + "," + row[ChamCongLichLamViecDAO.SysID].ToString());
        }


        /// <summary>
        /// Check whether data of user is valid
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool ValidateData()
        {
            var rview = (DataRowView)_manager.Current;
            var row = rview.Row;

            row.ClearErrors();
            exp.Clear();
            exp.SetError(txtTruongNhom, "");

            var strTruongNhom = txtTruongNhom.Text;
            if (strTruongNhom.Length == 0)
            {
                //row.SetColumnError("TruongNhom", clsResources.GetMessage("errors.required", lblTruongNhom.Text));
                exp.SetError(txtTruongNhom, clsResources.GetMessage("errors.required", lblTruongNhom.Text));
                txtTruongNhom.Focus();
                return false;
            }

            //var strNhom = txtTenNhom.Text;
            //if (strNhom.Length == 0)
            //{
            //    row.SetColumnError(clsCommon.CreateTimesheet.Nhom, clsResources.GetMessage("errors.required", lblNhom.Text));
            //    exp.SetError(txtTenNhom, clsResources.GetMessage("errors.required", lblNhom.Text));
            //    txtTenNhom.Focus();
            //    return false;
            //}

            var strMaTruongNhom = txtMaTruongNhom.Text;
            if (strMaTruongNhom.Length == 0)
            {
                // row.SetColumnError(clsCommon.CreateTimesheet.MaTruongNhom, clsResources.GetMessage("errors.required", lblMaTruongNhom.Text));
                exp.SetError(txtMaTruongNhom, clsResources.GetMessage("errors.required", lblMaTruongNhom.Text));
                return false;
            }
            var strOutsource = txtOutsource.Text;
            if (strOutsource.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.Outsource, clsResources.GetMessage("errors.required", lblOutsource.Text));

                exp.SetError(txtOutsource, clsResources.GetMessage("errors.required", lblOutsource.Text));
                txtOutsource.Focus();
                return false;
            }

            var strL1XacNhan = txtL1PheDuyet.Text;
            if (strL1XacNhan.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L1XacNhan, clsResources.GetMessage("errors.required", lblL1Ten.Text));
                exp.SetError(txtL1PheDuyet, clsResources.GetMessage("errors.required", lblL1Ten.Text));
                return false;
            }
            var strL1XacNhanMaSo = txtL1PheDuyetMa.Text;
            if (strL1XacNhanMaSo.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L1XacNhanMa, clsResources.GetMessage("errors.required", lblL1MaSo.Text));
                exp.SetError(txtL1PheDuyetMa, clsResources.GetMessage("errors.required", lblL1MaSo.Text));
                return false;
            }


            var strL2XacNhan = txtL2PheDuyet.Text;
            if (strL2XacNhan.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L2XacNhan, clsResources.GetMessage("errors.required", lblL2Ten.Text));
                exp.SetError(txtL2PheDuyet, clsResources.GetMessage("errors.required", lblL2Ten.Text));
                return false;
            }
            var strL2XacNhanMaSo = txtL2PheDuyetMa.Text;
            if (strL2XacNhanMaSo.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L2XacNhanMa, clsResources.GetMessage("errors.required", lblL2MaSo.Text));
                exp.SetError(txtL2PheDuyetMa, clsResources.GetMessage("errors.required", lblL2MaSo.Text));
                return false;
            }

            var strL3XacNhan = txtL3PheDuyet.Text;
            if (strL3XacNhan.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L3XacNhan, clsResources.GetMessage("errors.required", lblL3Ten.Text));
                exp.SetError(txtL3PheDuyet, clsResources.GetMessage("errors.required", lblL3Ten.Text));
                return false;
            }
            var strL3XacNhanMaSo = txtL3PheDuyetMa.Text;
            if (strL3XacNhanMaSo.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L3XacNhanMa, clsResources.GetMessage("errors.required", lblL3MaSo.Text));
                exp.SetError(txtL3PheDuyetMa, clsResources.GetMessage("errors.required", lblL3MaSo.Text));
                return false;
            }

        


            return true;
        }


        private void cboNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < dtNhom.Rows.Count; i++)
            {
                if (dtNhom.Rows[i][clsCommon.CreateTimesheet.NhomId].ToString() == cboNhom.SelectedValue.ToString())
                {
                    txtOutsource.Text = (bool)dtNhom.Rows[i][clsCommon.CreateTimesheet.Outsource] == true ? "Yes" : "No";
                }
            }

            BindNhanVien();


        }



        private void BindNhanVien()
        {

            if (dtpNgay.Value >= DateTime.Now)
            {
                string fromDate = dtpNgay.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                string toDate = dtpNgay.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                string nhom = cboNhom.Text != "" ? cboNhom.SelectedValue.ToString() : "0";
                string truongNhom = clsSystemConfig.UserName;
                grdTimesheets.DataSource = _createTimesheetBo.GetCcLichLamViec("", fromDate, toDate, truongNhom, nhom);
                SetEnableSaveAndAddMore();
            }
            else
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheetManual.DatetimeNotMatch", dtpNgay.Text, DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDisplay)), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void SetEnableSaveAndAddMore()
        {
            if (grdTimesheets.Rows.Count > 0)
            {
                btnAddMore.Enabled = true;
                btnRemove.Enabled = true;

            }
            else
            {
                btnAddMore.Enabled = false;
                btnRemove.Enabled = false;
            }

            FormatGridData();


        }

        private void FormatGridData()
        {
            for (int i = 0; i < grdTimesheets.Rows.Count; i++)
            {
                if ((bool)grdTimesheets.Rows[i].Cells[ChamCongLichLamViecDAO.DaLayDuLieuChamCong].Value == true)
                {
                    grdTimesheets.Rows[i].ReadOnly = true;
                    // grdTimesheets.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                }
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindNhanVien();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<ClsLichLamViec> lstLichLamviecs = GetDataToSave();
            if (lstLichLamviecs.Count > 0)
            {
                //Removed data
                _createTimesheetBo.RemoveTimesheets(lstLichLamviecs);
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheetManual.RemoveSuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindNhanVien();
            }
            else
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheetManual.NoDataCheck"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddMore_Click(object sender, EventArgs e)
        {
            try
            {
                ClsCreateTimesheet timesheet = new ClsCreateTimesheet();
                timesheet.TruongNhomId = clsSystemConfig.UserName;
                timesheet.TenTruongNhom = txtTruongNhom.Text;
                timesheet.NhomId = cboNhom.Text != "" ? Int32.Parse(cboNhom.SelectedValue.ToString()) : 0;
                timesheet.TenNhom = cboNhom.Text;
                timesheet.TuanLamViec = clsCommon.GetWeek(dtpNgay.Value).ToString();
                timesheet.NgayLamViec = dtpNgay.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                timesheet.IsOutsource = txtOutsource.Text;
                ChooseStaffToAddManual chirdFrom = new ChooseStaffToAddManual(timesheet);
                //  chirdFrom.ObjTimesheet = timesheet;

                chirdFrom.Show();

                chirdFrom.Closed += chirdFrom_Closed;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }

        void chirdFrom_Closed(object sender, EventArgs e)
        {
            BindNhanVien();
        }

        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {

            try
            {

                string fromDate = dtpNgay.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                string toDate = dtpNgay.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                string nhom = cboNhom.SelectedValue.ToString();
                string truongNhom = txtMaTruongNhom.Text;
                grdTimesheets.DataSource = _createTimesheetBo.GetCcLichLamViec("", fromDate, toDate, truongNhom, nhom);
                SetEnableSaveAndAddMore();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            //BindNhanVien();
        }
    }
}