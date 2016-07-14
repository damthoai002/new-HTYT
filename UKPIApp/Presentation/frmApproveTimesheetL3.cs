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

namespace UKPI.Presentation
{
    public partial class FrmApproveTimesheetL3 : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmApproveTimesheetL3));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        readonly System.Data.DataTable _dt = null;
        private ChamCongLichLamViecBo _ccllv = new ChamCongLichLamViecBo();
        private CreateTimesheetBo _tsBo = new CreateTimesheetBo();
        private int _checkRowsCount = 0;

        //param value.
        private String parHanChotDuyetCong = "";
        private String parChuanTinhCong = "";



        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();
      
        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors

        public FrmApproveTimesheetL3()
        {

            InitializeComponent();

            clsTitleManager.InitTitle(this);
            GetParam();
            SetDefauldValue();

            // Save original columns
            _originalColumns = new DataGridViewColumn[grdStores.Columns.Count];
            grdStores.Columns.CopyTo(_originalColumns, 0);
            grdStores.Sorted += grdStores_Sorted;
        }

        void grdStores_Sorted(object sender, EventArgs e)
        {
            this.ProcessDataRow();
        }

        private void GetParam()
        {
            parHanChotDuyetCong = ParameterBo.GetParamByName(clsCommon.Parameter.HanChotDuyetcong).ParamValue;
            parChuanTinhCong = ParameterBo.GetParamByName(clsCommon.Parameter.ChuanTinhCong).ParamValue;
        }

        private void SetDefauldValue()
        {
            BindYear();
            BindMonth();
            SetDefauldFilterTime();
            BindWeek();
            SetFromToDate();
            BindStatus();
            BindOnOff();
            BindShift();
            BindTeamLead();
            BindTeamLeadL1();
        }

        private void BindTeamLeadL1()
        {
            DataTable tbL1 = _ccllv.GetTruongNhomL1ByL3(clsSystemConfig.UserName);

            cboTruongNhomL1.ValueMember = "USERNAME";
            cboTruongNhomL1.DisplayMember = "QLL1";
            cboTruongNhomL1.DataSource = tbL1;

        }

        private void BindTeamLead()
        {

          
        }

        private void BindShift()
        {
            cboShift.ValueMember = "Value";
            cboShift.DisplayMember = "Name";
            cboShift.DataSource = _common.GetShift();
            cboShift.SelectedItem = clsCommon.ApproveTimesheet.All;
        }

        private void BindOnOff()
        {
            cboOnOff.ValueMember = "Value";
            cboOnOff.DisplayMember = "Name";
            cboOnOff.DataSource = _common.GetOnOff();
            cboOnOff.SelectedItem = clsCommon.ApproveTimesheet.All;
        }

        private void BindStatus()
        {
            cboStatus.ValueMember = "Value";
            cboStatus.DisplayMember = "Name";
            cboStatus.DataSource = _common.GetStatusApprove();
            cboStatus.SelectedItem = clsCommon.ApproveTimesheet.All;

        }

        private void SetDefauldFilterTime()
        {
            radMonth.Checked = true;
            cboWeek.Enabled = false;
        }

        private void BindYear()
        {

            cboYear.DataSource = _common.GetYearNo();
            cboYear.SelectedItem = DateTime.Now.Year;
        }

        private void SetFromToDate()
        {
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat =clsCommon.ApproveTimesheet.DateFormatDisplay;

            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat =clsCommon.ApproveTimesheet.DateFormatDisplay;

            if (cboMonth.Enabled == true)
            {
                dtpFromDate.Value = _common.GetStartDateOfMonth(Int16.Parse(cboYear.SelectedItem.ToString()),
                    Int16.Parse(cboMonth.SelectedItem.ToString()));
                dtpToDate.Value = _common.GetEndDateOfMonth(Int16.Parse(cboYear.SelectedItem.ToString()),
                Int16.Parse(cboMonth.SelectedItem.ToString()));
            }
            else
            {
                dtpFromDate.Value = _common.GetFirstDateOfWeekISO8601(Int16.Parse(cboYear.SelectedItem.ToString()), Int16.Parse(cboWeek.SelectedItem.ToString()));
                dtpToDate.Value = _common.GetEndDateOfWeekISO8601(Int16.Parse(cboYear.SelectedItem.ToString()), Int16.Parse(cboWeek.SelectedItem.ToString()));

            }


        }

        private void BindWeek()
        {
            cboWeek.DataSource = _common.GetWeekNo(DateTime.Now.Year);
            cboWeek.SelectedItem = clsCommon.GetWeek(DateTime.Now);
        }

        private void BindMonth()
        {
            cboMonth.DataSource = _common.GetMonthNo();
            cboMonth.SelectedItem = DateTime.Now.Month;
        }

        private void InitControls()
        {
            try
            {
                this.grdStores.AutoGenerateColumns = false;
                // Init controls' status
                //txtDistributors.Text = DISTRIBUTORS_DEFAUT;
                btnExport.Enabled = false;

                // Read file config
                this.OnGridDataSourceChanged();
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }



        private void OnSearchLichLamViec()
        {
            try

            {
                this.Cursor = Cursors.WaitCursor;

                // Get current TimePeriod
                //TimePeriod currentTimePeriod = new TimePeriod(int.Parse(cmbTimeperiodYear.SelectedItem.ToString()),
                //                    int.Parse(cmbTimeperiodMonth.SelectedItem.ToString()));

                // Rebuild columns of DataGridView
                this.grdStores.DataSource = null;
                //this.RebuildDisplaySetColumns(currentTimePeriod);
                this.grdStores.Refresh();
                var week = cboWeek.Enabled == true ? cboWeek.SelectedItem.ToString() : "";
                var fromDate = dtpFromDate.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb, CultureInfo.InvariantCulture);
                var toDate = dtpToDate.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb, CultureInfo.InvariantCulture);
                var teamLead = cboTruongNhomL1.SelectedValue.ToString();
                var team = txtTeamId.Text;
                var status = cboStatus.SelectedValue.ToString() != "-1" ? cboStatus.SelectedValue.ToString() : "";
                var onOff = cboOnOff.SelectedValue.ToString() != "0" ? cboOnOff.SelectedText : "";
                var shift = cboShift.SelectedValue.ToString() != "0" ? cboShift.SelectedValue.ToString() : "";

                // Search stores
                //System.Data.DataTable dt = _lichLamViecBO.GetStoresWithinDisplayRegistration(channel, region, province, town, area, perfectStore,
                //                            psType, distributors, storeCode, storeName, storeAddress, currentTimePeriod);
                var lichLamViec = new ChamCongLichLamViecBo();
                var table = lichLamViec.GetChamCongLichLamViecL3(week, fromDate, toDate, teamLead, onOff, shift, status,
                    team);

                grdStores.DataSource = table;

                // Show message if there is no store found
                if (grdStores.RowCount == 0)
                {
                    MessageBox.Show(clsResources.GetMessage("message.FrmApproveTimesheet.Nodata"),
                        clsResources.GetMessage("warnings.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                table.AcceptChanges();
                grdStores.Refresh();
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
                this.Cursor = Cursors.Default;

                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void OnGridDataSourceChanged()
        {
            
            if (grdStores.RowCount == 0)
            {

                btnExport.Enabled = false;
                btnReject.Enabled = false;
               // btnSave.Enabled = false;
                btnApprove.Enabled = false;
            }
            else
            {
                // Check all stores and enable Delete button

                btnExport.Enabled = true;
                btnReject.Enabled = true;
                btnApprove.Enabled = true;
            }
        }

      
        private bool OnSaveClick()
        {
            try
            {
                var dtStores = grdStores.DataSource as System.Data.DataTable;
                if (dtStores == null || dtStores.Rows.Count == 0)
                {
                    return true;
                }

                // There is no change
                if (dtStores.GetChanges() == null)
                {
                    return true;
                }


                // Get TimePeriod

                Cursor.Current = Cursors.WaitCursor;
                var lichLamViec = new ChamCongLichLamViecBo();
                lichLamViec.UpdateChamCongLichLamViec(GetDataToSave());

                grdStores.Refresh();
                System.Windows.Forms.Application.DoEvents();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            return true;
        }
        public string ValidatedDateToSave()
        {
            const string strError = "";

            var table = grdStores.DataSource as System.Data.DataTable;
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

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;

            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Vao_L1].ToString()) != "" &&
                                    clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Ra_L1].ToString()) != "" &&
                                    (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == true
                                     && (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false
                                    select new ClsLichLamViec
                                    {
                                        SysId = long.Parse(row[ChamCongLichLamViecDAO.SysID].ToString()),
                                        Vao_L1 = row[ChamCongLichLamViecDAO.Vao_L1].ToString(),
                                        Ra_L1 = row[ChamCongLichLamViecDAO.Ra_L1].ToString(),
                                        OTL1 = (bool)row[ChamCongLichLamViecDAO.CoDangKyOT] == true ? clsCommon.CalOtTime(row[ChamCongLichLamViecDAO.Vao_L1].ToString(), row[ChamCongLichLamViecDAO.Ra_L1].ToString(), parChuanTinhCong).ToString(CultureInfo.InvariantCulture) : "",
                                        Note = row[ChamCongLichLamViecDAO.Note].ToString(),
                                        LastUpDate = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                                        lastUpdateId = clsSystemConfig.MaNhanVien.ToString()
                                    });

            return lstLichLamViec;
        }



        private bool IsDisplayRegistrationCell(DataGridViewCell cell)
        {
            // Current cell belong to Original colums (store information columns)
            return _originalColumns.All(t => cell.OwningColumn.Name != t.Name);

            // Current cell belong to display registration columns
        }


        private void Export()
        {
            try
            {
                var dtStoreList = grdStores.DataSource as System.Data.DataTable;
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
                    var exporter = new EditApproveTimesheetExporter(true);
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

        #region Handle events

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void SearchData()
        {

            if (CheckValidDate().Length == 0 || CheckValidDate().Length == -1)
            {
                BindDataToGrid();
            }
            else
            {
                MessageBox.Show(CheckValidDate());
            }

        }


        public string CheckValidDate()
        {
            var strError = "";
            var parHanChot = parHanChotDuyetCong.Split('@')[1];
            var rangeFistDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Int16.Parse(parHanChot));
            var rangeLastDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25);

            var fromDate = dtpFromDate.Value;
            var toDate = dtpToDate.Value;
            var month = cboMonth.SelectedItem.ToString();
            if (toDate < fromDate)
            {
                strError = strError + clsResources.GetMessage("message.FrmApproveTimesheet.ToDateInvalid") + "\n";
            }
            if (toDate >= DateTime.Now)
            {
                strError = strError + clsResources.GetMessage("message.FrmApproveTimesheet.ToDateInvalid") + "\n";
            }
            //if (DateTime.Now > rangeFistDate && DateTime.Now <= rangeLastDate && toDate > rangeFistDate)
            //{
            //    strError = strError + clsResources.GetMessage("message.FrmApproveTimesheet.ToDateOutOfRange").Replace("{0}", rangeFistDate.ToString("dd/MM/yyyy")) + "\n";
            //}
            if (Int16.Parse(month) > DateTime.Now.Month)
            {
                strError = strError + clsResources.GetMessage("message.FrmApproveTimesheet.MonthInvalid") + "\n";
            }

            return strError;
        }

        private void BindDataToGrid()
        {
            this.OnSearchLichLamViec();
            this.ProcessDataRow();
        }



        private void ProcessDataRow()
        {
            for (var i = 0; i < grdStores.Rows.Count; i++)
            {
                //var aa = grdStores.Rows[i].Cells[0].Value;


                if ((bool)grdStores.Rows[i].Cells[0].Value == true)
                {
                    grdStores.Rows[i].ReadOnly = true;
                    grdStores.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                }

                grdStores.Rows[i].Cells[ChamCongLichLamViecDAO.Ngay].Value =
                    clsCommon.FormatDateToDisplay(grdStores.Rows[i].Cells[ChamCongLichLamViecDAO.Ngay].Value.ToString());
            }

        }

        private void grdStores_DataSourceChanged(object sender, EventArgs e)
        {
            this.OnGridDataSourceChanged();
        }

        private void grdStore_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (grdStores.Columns[e.ColumnIndex].Name.ToUpper() == "COLTURNOVER")
            {
                MessageBox.Show(clsResources.GetMessage("frmEditStore.Messages.TurnOver"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Current cell is a display registration one or not
                if (this.IsDisplayRegistrationCell(grdStores[e.ColumnIndex, e.RowIndex]))
                {
                    MessageBox.Show(this, clsResources.GetMessage("errors.EditStore.InvalidDisplayRegistrationValue"),
                        clsResources.GetMessage("message.Others.Message"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
                }
            }
        }

        private void frmEditStore_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void frmEditStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var table = grdStores.DataSource as System.Data.DataTable;
            //if (table == null)
            //{
            //    return;
            //}
            //if (table.GetChanges() == null) return;
            //var dialogResult = MessageBox.Show(clsResources.GetMessage("message.Others.DataHasChanged") + Environment.NewLine
            //                                            + clsResources.GetMessage("frmApproveTimesheet.Messages.WantToSaveTheChanges"), clsResources.GetMessage("frmEditStore.Messages.Edit_Store"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //switch (dialogResult)
            //{
            //    case DialogResult.Cancel:
            //        e.Cancel = true;
            //        break;
            //    case DialogResult.No:
            //        this.Hide();
            //        break;
            //    default:
            //        if (!this.OnSaveClick())
            //        {
            //            e.Cancel = true;
            //        }
            //        break;
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Export();
        }

        #endregion

        private void radMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (radMonth.Checked != true) return;
            cboWeek.Enabled = false;
            cboMonth.Enabled = true;
            SetFromToDate();
        }

        private void radWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (radWeek.Checked != true) return;
            cboWeek.Enabled = true;
            cboMonth.Enabled = false;
            SetFromToDate();
        }

        private void cboWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFromToDate();
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFromToDate();
        }

        private void btnApproveSave_Click(object sender, EventArgs e)
        {
            if (this.OnApprove())
            {
                MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                SearchData();
            }


        }

        public void SendEmailForL4()
        {
            try
            {
                string tenTruongNhom = clsSystemConfig.FullName;
                DataTable tb = _lichLamViecBo.GetEmailOfLeverApprove(Int32.Parse(clsSystemConfig.MaNhanVien.ToString()), 3);
                string emailL4 = tb.Rows[0]["Email"].ToString();
                string tenNVQLL4 = tb.Rows[0]["FullName"].ToString();

                string body = "";

                body = "<p>Chào " + tenNVQLL4 + " </p></br></br></br> ";
                body += "<p>Email Thông báo phê duyệt chấm công: </p></br>";
                body += "<p>Trưởng nhóm:  " + tenTruongNhom + " đả phê duyệt chấm công cho nhóm:  " + txtTeam.Text + " từ ngày: " + dtpFromDate.Text + " đến ngày: " + dtpToDate.Text + ".</p></br></br>";
                body += "<p>Đăng nhập vào hệ thống chấm công của bạn để xem và tiếp tục phê duyệt chấm công cho nhóm : " + txtTeam.Text + ".</p>";
                body += "<p>Thân ái</p>";

                _common.SendEmail("", emailL4, "Email Thông báo phê duyệt Chấm công.", body);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }

        }

        public List<ClsLichLamViec> GetDataToApproveSave()
        {
            var lstLichLamViec = new List<ClsLichLamViec>();

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;
            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == true
                                    select new ClsLichLamViec
                                    {
                                        SysId = long.Parse(row[ChamCongLichLamViecDAO.SysID].ToString()),
                                        L3XacNhan = true,
                                        L3XacNhan_Date = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                                        L3XacNhan_Id = clsSystemConfig.MaNhanVien.ToString(),
                                        L3XacNhan_TenNgan = clsSystemConfig.FullName,
                                        LastUpDate = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                                        lastUpdateId = clsSystemConfig.MaNhanVien.ToString(),
                                        Note = row[ChamCongLichLamViecDAO.Note].ToString()
                                    });

            return lstLichLamViec;
        }

        private bool OnApprove()
        {
            try
            {
                var dtStores = grdStores.DataSource as System.Data.DataTable;
                if (dtStores == null || dtStores.Rows.Count == 0)
                {
                    return true;
                }

                // Get TimePeriod

                Cursor.Current = Cursors.WaitCursor;
                var lichLamViec = new ChamCongLichLamViecBo();
                var lstChamCong = GetDataToApproveSave();
                lichLamViec.XacNhanChamCongLichLamViecL3(lstChamCong);

                grdStores.Refresh();
                System.Windows.Forms.Application.DoEvents();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            return true;
        }

        public string GetItemToGetTimesheet()
        {
            const string strSysId = "";

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => (bool)row[ChamCongLichLamViecDAO.L2XacNhan] == false).Aggregate(strSysId, (current, row) => current + "," + row[ChamCongLichLamViecDAO.SysID].ToString());
        }

        public string GetItemToRejectedTimesheet()
        {
            const string strSysId = "";

            var table = grdStores.DataSource as System.Data.DataTable;
            return table == null ? strSysId : table.Rows.Cast<DataRow>().Where(row => (row["Check"].ToString() == "1")).Aggregate(strSysId, (current, row) => current + "," + row[ChamCongLichLamViecDAO.SysID].ToString());
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            var strSysId = GetItemToRejectedTimesheet();
            if (strSysId.Length <= 0)
            {
                MessageBox.Show(clsResources.GetMessage("message.FrmApproveTimesheet.NoDataFoundToRejected"),
                              clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var dialogResult = MessageBox.Show(clsResources.GetMessage("message.FrmApproveTimesheet.ConfirmToRejected") , clsResources.GetMessage("Message.frmApproveTimesheet.ApproveTS"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dialogResult)
                {
                    case DialogResult.Cancel:
                       // e.Cancel = true;
                        break;
                    case DialogResult.No:
                        break;
                    default: 
                        this.OnRejectedTs(strSysId);
                        break;
                }

            }
        }

        private void OnRejectedTs(string strSysId)
        {

            var frmRejectedTimesheet = new RejectedTimesheet();
            frmRejectedTimesheet.StrSysId = strSysId.Substring(1,strSysId.Length - 1);
            frmRejectedTimesheet.Show();
           // SearchData();
            frmRejectedTimesheet.Closed += frmRejectedTimesheet_Closed;
        }

        void frmRejectedTimesheet_Closed(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cboTruongNhomL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable teamTb = _tsBo.GetNhomByNhomTruong(cboTruongNhomL1.SelectedValue.ToString());
            if (teamTb.Rows.Count > 0)
            {
                txtTeam.Text = teamTb.Rows[0][clsCommon.CreateTimesheet.Nhom].ToString();
                txtTeamId.Text = teamTb.Rows[0][clsCommon.CreateTimesheet.NhomId].ToString();
            }
        }

    }
}