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
    public partial class FrmManageShiftForTeam : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmManageShiftForTeam));

        private clsBaseBO _bo = new clsBaseBO();
        private CreateTimesheetBo _ctsBo = new CreateTimesheetBo();

        private readonly clsCommon _common = new clsCommon();


        #endregion

        #region Constructors

        public FrmManageShiftForTeam()
        {

            InitializeComponent();
            clsTitleManager.InitTitle(this);
            BindOutsource();
            InitDataControl();
            BinWeek();
            BindDauDocThe();
            BindCaLamViec();

        }

        private void BindCaLamViec()
        {
            dgvDNCaLamViec.DataSource = _ctsBo.GetDaLamViec();
        }

        private void BindDauDocThe()
        {
            cboDauDocTheRa.ValueMember = "DESCRP";
            cboDauDocTheVao.ValueMember = "DESCRP";

            cboDauDocTheVao.DisplayMember = "DESCRP";
            cboDauDocTheRa.DisplayMember = "DESCRP";

            cboDauDocTheRa.DataSource = _ctsBo.GetDauDocThe();
            cboDauDocTheVao.DataSource = _ctsBo.GetDauDocThe();
        }
        public void InitDataControl()
        {
            try
            {
                var clvDao = new CaLamViecDao();

                DataRow rowtype;

                MaCaLaViec.ValueMember = "MaCaLamViec";
                MaCaLaViec.DisplayMember = "MaCaLamViec";
                MaCaLaViec.DataSource = clvDao.GetCalamViec();

                DauDocTheRaId.ValueMember = "DESCRP";
                DauDocTheRaId.DisplayMember = "DESCRP";
                DauDocTheRaId.DataSource = clvDao.GetDauDocThe();

                DauDocTheVaoId.ValueMember = "DESCRP";
                DauDocTheVaoId.DisplayMember = "DESCRP";
                DauDocTheVaoId.DataSource = clvDao.GetDauDocThe();


                CaLamViec.ValueMember = "MaCaLamViec";
                CaLamViec.DisplayMember = "MaCaLamViec";
                CaLamViec.DataSource = clvDao.GetCalamViec();

            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }

        public void BinWeek()
        {

            cboThang.ValueMember = "Year";
            cboThang.DisplayMember = "Month";
            cboThang.DataSource = _ctsBo.GetMonthOfSystem();


            btnAddNew.Enabled = txtNhom.Text != "";
            btnCreateShiftForTeam.Enabled = txtNhom.Text != "";
            btnViewShiftForTeam.Enabled = txtNhom.Text != "";

        }
        private void BindOutsource()
        {
            var timesheet = new CreateTimesheetBo();
            var dtNhom = timesheet.GetNhomByNhomTruong(clsSystemConfig.UserName);
            var curWeekNo = clsCommon.GetWeek(DateTime.Now);
            if (curWeekNo == 53)
            {
                curWeekNo = 0;
            }
            lblFromWeekVal.Text = (curWeekNo + 1).ToString();

            for (var i = 0; i < dtNhom.Rows.Count; i++)
            {
                txtNhom.Text = dtNhom.Rows[i][clsCommon.CreateTimesheet.Nhom].ToString();
                txtNhomId.Text = dtNhom.Rows[i][clsCommon.CreateTimesheet.NhomId].ToString();
                txtOutsource.Text = (bool)dtNhom.Rows[i][clsCommon.CreateTimesheet.Outsource] == true ? "Yes" : "No";
                txtLoaiNhom.Text = dtNhom.Rows[i][clsCommon.CreateTimesheet.LoaiNhom].ToString();
            }

        }


        #endregion

        #region Handle events

        public bool CheckValidDate()
        {
            erp.Clear();
            return true;
        }

        #endregion
        public string GetItemToUnActive()
        {
            const string strSysId = "";

            var table = grdTeam.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => row["Check"].ToString() == "1").Aggregate(strSysId, (current, row) => current + "," + row[clsCommon.Group.MaNhom].ToString());
        }

        public List<ClsTeam> GetTeamToUnActive()
        {
            var lstLichLamViec = new List<ClsTeam>();

            var table = grdTeam.DataSource as System.Data.DataTable;
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

        private void BindDateToGrid()
        {
            try
            {
                var nhom = Int16.Parse(txtNhomId.Text);
                var truongNhom = Int32.Parse(clsSystemConfig.MaNhanVien.ToString());
                var tuTuan = Int16.Parse(cboThang.SelectedItem.ToString());
                var denTuan = Int16.Parse(cboDenTuan.SelectedItem.ToString());

                var year = DateTime.Now.Year;


                BindDateToGrid(nhom, tuTuan, denTuan, year);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }


        }

        private void BindDateToGrid(int nhom, int tuTuan, int denTuan, int year)
        {
            var ctsBo = new CreateTimesheetBo();
            grdTeam.DataSource = ctsBo.GetShiftForTeam(nhom, tuTuan, denTuan, year);

            FormatGrid();


        }

        private void FormatGrid()
        {
            for (var i = 0; i < grdTeam.Rows.Count; i++)
            {

                if ((bool)grdTeam.Rows[i].Cells["IsOff"].Value)
                {
                    grdTeam.Rows[i].ReadOnly = true;
                    grdTeam.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                }

                grdTeam.Rows[i].Cells[clsCommon.CaLamViecNhom.NgayVao].Value =
                    clsCommon.FormatDateToDisplay(grdTeam.Rows[i].Cells[clsCommon.CaLamViecNhom.NgayVao].Value.ToString());
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            var dt = grdTeam.DataSource as DataTable;
            try
            {

                if (grdTeam.DataSource != null)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            _ctsBo.UpdateShiftForTeam(row);
                        }

                    }
                    FormatGrid();

                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageShiftForTeam.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageShiftForTeam.save.UnSuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

            FormatGrid();
        }

        public bool CheckValidDateDataToSearch()
        {
            return true;
        }

        private void btnCreateShiftForTeam_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTuanLamViec())
                {
                    var nhom = Int16.Parse(txtNhomId.Text);
                    var truongNhom = (clsSystemConfig.UserName);

                    var dauDocTheVao = cboDauDocTheVao.Text;
                    var dauDocTheRa = cboDauDocTheRa.Text;
                    var month = Int16.Parse(cboThang.Text);
                    var year = Int16.Parse(cboThang.SelectedValue.ToString());
                    var firstDateOfMonth = new DateTime(year, (month), 1);

                    var endDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

                    var firstWeek = clsCommon.GetWeek(firstDateOfMonth);
                    var lastWeek = clsCommon.GetWeek(endDateOfMonth);


                    if (txtLoaiNhom.Text == "1")
                    {
                        _ctsBo.CreateShiftForTeamHanhChinh(nhom, truongNhom, firstWeek, lastWeek, year, dauDocTheVao, dauDocTheRa);

                    }
                    else
                    {
                        _ctsBo.CreateShiftForTeam(nhom, truongNhom, firstWeek, lastWeek, year, dauDocTheVao, dauDocTheRa);
 
                    }
                    


                    //tuan5, year1, year2, year3, year4, year5);
                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageShiftForTeam.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindDateToGrid(nhom, firstWeek, lastWeek, year);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmManageShiftForTeam.unSuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        private void btnViewShiftForTeam_Click(object sender, EventArgs e)
        {
            try
            {
                var nhom = Int16.Parse(txtNhomId.Text);
                var month = Int16.Parse(cboThang.Text);
                var year = Int16.Parse(cboThang.SelectedValue.ToString());
                var firstDateOfMonth = new DateTime(year, (month), 1);

                var endDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

                var firstWeek = clsCommon.GetWeek(firstDateOfMonth);
                var lastWeek = clsCommon.GetWeek(endDateOfMonth);

                BindDateToGrid(nhom, firstWeek, lastWeek, year);


            }
            catch (Exception ex)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmManageShiftForTeam.unSuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        private bool ValidateTuanLamViec()
        {
            erp.Clear();

            //var tuTuan = Int16.Parse(cboThang.Text);
            //var denTuan = Int16.Parse(cboDenTuan.Text);
            //if (denTuan < tuTuan)
            //{
            //    erp.SetError(cboDenTuan, clsResources.GetMessage("errors.string.FrmManageShiftForTeam.WeekNotMatch", cboDenTuan.SelectedItem.ToString(), cboThang.SelectedItem.ToString()));
            //    cboDenTuan.Focus();
            //    return false;
            //}
            return true;
        }




    }
}