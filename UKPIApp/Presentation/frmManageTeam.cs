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
    public partial class FrmManageTeam : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmManageTeam));

        private clsBaseBO _bo = new clsBaseBO();
        private TeamBo _teamBo = new TeamBo();

        private readonly clsCommon _common = new clsCommon();

        private DataTable tbTN = new DataTable();

        #endregion

        #region Constructors

        public FrmManageTeam()
        {

            InitializeComponent();

            clsTitleManager.InitTitle(this);

            BindDataToGrid();
            BindHoatDong();
            BindLoaiNhom();
            BindOutsource();
            BindTruongNhom();
            // Save original columns

            grdTeam.Sorted += grdStores_Sorted;
            this.ProcessDataRow();

        }

        private void BindOutsource()
        {
            cboOutsource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOutsource.ValueMember = "value";
            cboOutsource.DisplayMember = "name";
            cboOutsource.DataSource = _common.GetHoatDong();
        }

        private void BindTruongNhom()
        {
            tbTN = _teamBo.GetTruongNhomForTeam();
            if (tbTN.Rows.Count > 0)
            {
                cboTruongNhom.DropDownStyle = ComboBoxStyle.DropDownList;
                cboTruongNhom.ValueMember = "MaNVUnilever";
                cboTruongNhom.DisplayMember = "FullName";
                cboTruongNhom.DataSource = _teamBo.GetTruongNhomForTeam();
                txtMaso.Text = cboTruongNhom.SelectedValue.ToString();
            }

        }

        private void BindLoaiNhom()
        {
            cboLoaiNhom.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiNhom.ValueMember = "value";
            cboLoaiNhom.DisplayMember = "name";
            cboLoaiNhom.DataSource = _common.GetLoaiNhom();

        }

        private void BindHoatDong()
        {
            cboActive.DropDownStyle = ComboBoxStyle.DropDownList;
            cboActive.ValueMember = "value";
            cboActive.DisplayMember = "name";
            cboActive.DataSource = _common.GetHoatDong();
            cboActive.SelectedValue = 1;
        }

        void grdStores_Sorted(object sender, EventArgs e)
        {
            this.ProcessDataRow();
        }

        #endregion

        #region Handle events

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }




        public bool CheckValidDate()
        {
            erp.Clear();

            var strTenNhom = txtNhom.Text;
            if (strTenNhom.Length == 0)
            {
                erp.SetError(txtNhom, clsResources.GetMessage("errors.required", txtNhom.Text));
                return false;
            }
            if (!_common.IsLetterAndDigit(strTenNhom))
            {
                erp.SetError(txtNhom, clsResources.GetMessage("errors.string.specialChar", txtNhom.Text));
                txtNhom.Focus();
                return false;
            }
            var strNote = txtNote.Text;
            if (!_common.IsLetterAndDigit(strNote))
            {
                erp.SetError(txtNote, clsResources.GetMessage("errors.string.specialChar", txtNote.Text));
                txtNote.Focus();
                return false;
            }


            return true;
        }

        private void BindDataToGrid()
        {
            var teamBo = new TeamBo();
            grdTeam.DataSource = teamBo.GetAllTeams();
        }



        private void ProcessDataRow()
        {
            for (var i = 0; i < grdTeam.Rows.Count; i++)
            {
                //var aa = grdStores.Rows[i].Cells[0].Value;


                if (grdTeam.Rows[i].Cells["IsActive"].Value.ToString() == "1")
                {
                    grdTeam.Rows[i].ReadOnly = true;
                    grdTeam.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                }

                //if ((bool)grdTeam.Rows[i].Cells[ChamCongLichLamViecDAO.DaLayDuLieuChamCong].Value == false)
                //{
                //    grdTeam.Rows[i].ReadOnly = true;
                //    //grdStores.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                //}

                //grdTeam.Rows[i].Cells[ChamCongLichLamViecDAO.Ngay].Value =
                //    clsCommon.FormatDateToDisplay(grdTeam.Rows[i].Cells[ChamCongLichLamViecDAO.Ngay].Value.ToString());
            }

        }


        /// <summary>
        /// check exchange grid when close form
        /// Creator: Sonlv
        /// Create Date: 23/03/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


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
                                        UserName = row[clsCommon.Group.UserName].ToString(),
                                        NhomId = row[clsCommon.Group.MaNhom].ToString()
                                    });

            return lstLichLamViec;
        }


        private void btnSeachTeam_Click(object sender, EventArgs e)
        {
            if (true)//CheckValidDateDataToSearch()
            {
                var ten = txtTenSearch.Text;
                var ho = txtHoSearch.Text;
                var nhom = txtTeamSearch.Text;
                grdTeam.DataSource = _teamBo.SearchTeam(ten, ho, nhom);

                ProcessDataRow();
            }

        }

        private void btnUnActive_Click(object sender, EventArgs e)
        {
            try
            {
                var strSysId = GetItemToUnActive();
                var lstTeam = GetTeamToUnActive();
                if (strSysId.Length > 0)
                {
                    strSysId = strSysId.Substring(1, strSysId.Length - 1);
                    _teamBo.UnActiveTeam(lstTeam, strSysId, clsSystemConfig.MaNhanVien.ToString());
                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageTeam.UnActivesuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindDataToGrid();
                    BindTruongNhom();
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageTeam.NoDataFoundToUnActive"),
                            clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidDate())
                {
                    var team = new ClsTeam();
                    team.TenNhom = txtNhom.Text;
                    team.LoaiNhom = cboLoaiNhom.SelectedValue.ToString();
                    team.IsOt = "0";
                    team.IsOutsource = cboOutsource.SelectedValue.ToString();
                    team.MoTa = txtNote.Text;
                    team.IsActive = cboActive.SelectedValue.ToString();
                    team.UserName = cboTruongNhom.Text.Split('~')[1].Trim();

                    //team.UserName = clsSystemConfig.UserName;

                    _teamBo.CreateTeam(team, clsSystemConfig.UserName);

                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageTeam.AddNewsuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindDataToGrid();
                    BindTruongNhom();
                    txtNhom.Text = "";
                    txtNote.Text = "";
                    this.ProcessDataRow();


                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        private void cboTruongNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaso.Text = cboTruongNhom.SelectedValue.ToString();
        }


        public bool CheckValidDateDataToSearch()
        {
            erp.Clear();

            var ten = txtTenSearch.Text;

            if (!_common.IsLetterAndDigitExceptWc(ten))
            {
                erp.SetError(txtTenSearch, clsResources.GetMessage("errors.string.specialChar", txtTenSearch.Text));
                txtTenSearch.Focus();
                return false;
            }
            var ho = txtHoSearch.Text;
            if (!_common.IsLetterAndDigitExceptWc(ho))
            {
                erp.SetError(txtHoSearch, clsResources.GetMessage("errors.string.specialChar", txtHoSearch.Text));
                txtHoSearch.Focus();
                return false;
            }
            var nhom = txtTeamSearch.Text;
            if (!_common.IsLetterAndDigitExceptWc(nhom))
            {
                erp.SetError(txtTeamSearch, clsResources.GetMessage("errors.string.specialChar", txtTeamSearch.Text));
                txtTeamSearch.Focus();
                return false;
            }

            return true;
        }
    }
}