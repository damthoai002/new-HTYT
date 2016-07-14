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
using log4net.Util;
using UKPI.BusinessObject;
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class FrmManageStaffInGroup : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmManageStaffInGroup));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        readonly System.Data.DataTable _dt = null;
        CurrencyManager _manager = null;
        private int _checkRowsCount = 0;
        CreatedTimesheet bo = new CreatedTimesheet();
        DataTable dt = null;
        private DataTable dtNhom = null;

        public bool Success { get; set; }

        #endregion

        #region Constructors

        public FrmManageStaffInGroup()
        {

            InitializeComponent();
            clsTitleManager.InitTitle(this);
            SetDefauldValue();
            BindWeekToCreateTimesheet();
            BindNhom();
            BindNhanVienNhom();
            BindNhanVienCc();
            SetDefauldValue();
            BindLoaiNhanVien();
        }

        private void BindLoaiNhanVien()
        {
            cboLoaiNhanVien.ValueMember = "value";
            cboLoaiNhanVien.DisplayMember = "Name";
            cboLoaiNhanVien.DataSource = _common.GetLoaiNhom();
        }

        private void BindNhanVienCc()
        {

            SearchNhanVienCc();
        }

        private void BindNhom()
        {
            var nhom = new ManageStaffOfGroupBo();
            dtNhom = nhom.GetNhomByNhomTruong(clsSystemConfig.UserName);
            cboNhom.ValueMember = clsCommon.Group.NhomId;
            cboNhom.DisplayMember = clsCommon.Group.Nhom;
            cboNhom.DataSource = dtNhom;
            cboNhom.DropDownStyle = ComboBoxStyle.DropDownList;

            txtTruongNhom.Text = clsSystemConfig.FullName;
            txtUId.Text = clsSystemConfig.MaNVUnilever;
            if (dtNhom.Rows.Count > 0)
            {
                txtOursource.Text = (bool)dtNhom.Rows[0][clsCommon.Group.Outsource] == true ? "Yes" : "No";
                txtGroupType.Text = dtNhom.Rows[0][clsCommon.Group.LoaiNhom].ToString() == "1" ? clsCommon.Group.HanhChinh : clsCommon.Group.CongNhan;
                cboLoaiNhanVien.SelectedText = dtNhom.Rows[0][clsCommon.Group.LoaiNhom].ToString() == "1" ? clsCommon.Group.HanhChinh : clsCommon.Group.CongNhan;

            }

        }



        private void BindNhanVienNhom()
        {
            var nhom = cboNhom.Text == "" ? "0" : cboNhom.SelectedValue.ToString(); //lay nhom Id trong cbo nhom
            var group = new ManageStaffOfGroupBo();
            grdTVNhom.DataSource = group.GetThanhVienNhom(nhom);
        }


        private void SetDefauldValue()
        {
            txtCurrentWeek.Text = clsCommon.GetWeek(DateTime.Now).ToString();
            txtWeekApply.Text = clsCommon.GetWeek(DateTime.Now).ToString();
            btnAddNV.Enabled = cboNhom.Text != "";
            btnRemoveNV.Enabled = cboNhom.Text != "";



        }

        public void BindWeekToCreateTimesheet()
        {
        }





        private void InitControls()
        {
            try
            {
                this.grdTVNhom.AutoGenerateColumns = false;
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

            var table = grdTVNhom.DataSource as System.Data.DataTable;
            if (table == null) return strError;


            var lstError = (from DataRow row in table.Rows
                            where (clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Vao_L1].ToString()) == "" ||
                            clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Ra_L1].ToString()) == "") &&
                             (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false
                            select String.Format(clsResources.GetMessage("message.FrmApproveTimesheet.LineInvalidTime"), row[ChamCongLichLamViecDAO.Vao_L1].ToString(), row[ChamCongLichLamViecDAO.Ra_L1].ToString()) + "\n"
                          );

            return lstError.Aggregate(strError, (current, strEr) => current + current);
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

            var table = grdTVNhom.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false && (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == false).Aggregate(strSysId, (current, row) => current + "," + row[ChamCongLichLamViecDAO.SysID].ToString());
        }
        private void btnTimNVNhom_Click(object sender, EventArgs e)
        {

        }
        private void btnTimNVCC_Click(object sender, EventArgs e)
        {
            SearchNhanVienCc();
        }

        private void SearchNhanVienCc()
        {
            if (ValidateData())
            {
                var ten = txtNameNVCC.Text;
                var ho = txtFName.Text;
                var loaiNhom = cboLoaiNhanVien.Text == clsCommon.Group.CongNhan ? "0" : "1";
                var maThe = txtCardNo.Text;
                var maNvUnilever = txtMaNvUnilever.Text.Trim();

                var groupBo = new ManageStaffOfGroupBo();
                dgvNVCC.DataSource = groupBo.SearchNvCc(ten, ho,  loaiNhom, maThe, maNvUnilever);
            }
        }

        private void btnAddNV_Click(object sender, EventArgs e)
        {
            var sysId = GetNvAddToGroup();
            if (sysId.Length != 0)
            {
                sysId = sysId.Substring(1, sysId.Length - 1);
                var maNhom = cboNhom.SelectedValue.ToString();
                var maTruongNhom = clsSystemConfig.MaNhanVien.ToString();

                var groupBo = new ManageStaffOfGroupBo();
                groupBo.AppNvToGroup(sysId, maNhom, maTruongNhom);

                MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffInGroup.Addsuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Add nv to group
                BindNhanVienCc();
                BindNhanVienNhom();

            }
            else
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffInGroup.NoDataFound"),
                              clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public string GetNvAddToGroup()
        {

            const string strSysId = "";

            var table = dgvNVCC.DataSource as System.Data.DataTable;
            return table == null ? strSysId : table.Rows.Cast<DataRow>().Where(row => (row["Check"].ToString() == "1")).Aggregate(strSysId, (current, row) => current + "," + row[clsCommon.Group.MaNhanVien].ToString());

        }
        public string GetNvRemoveToGroup()
        {

            const string strSysId = "";

            var table = grdTVNhom.DataSource as System.Data.DataTable;
            return table == null ? strSysId : table.Rows.Cast<DataRow>().Where(row => (row["Check"].ToString() == "1")).Aggregate(strSysId, (current, row) => current + "," + row[clsCommon.Group.MaNhanVien].ToString());

        }
        private void btnRemoveNV_Click(object sender, EventArgs e)
        {
            var sysId = GetNvRemoveToGroup();
            if (sysId.Length != 0)
            {
                //Add nv to group
                sysId = sysId.Substring(1, sysId.Length - 1);
                var maNhom = cboNhom.SelectedValue.ToString();
                var maTruongNhom = clsSystemConfig.MaNhanVien.ToString();

                var groupBo = new ManageStaffOfGroupBo();
                groupBo.RemoveNvToGroup(sysId, maNhom, maTruongNhom);

                MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffInGroup.Removesuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindNhanVienCc();
                BindNhanVienNhom();

            }
            else
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffInGroup.NoDataFound"),
                              clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool ValidateData()
        {
            var common = new clsCommon();
            erp.Clear();
            return true;
        }

    }
}