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
    public partial class FrmNVQL_Users : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmNVQL_Users));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        // Declare private fields
        private readonly NhanVienUsersBo _nhanVienUserBo = new NhanVienUsersBo();
        #endregion

        #region Constructors
        public FrmNVQL_Users()
        {
            InitializeComponent(); 
            BindLoaiNhanVien();
            BindNhanVienCc();
            BindNhanVienQuanLy();
            BindUser();
           
            clsTitleManager.InitTitle(this);
        }

        private void BindLoaiNhanVien()
        {
            cboLoaiNhanVien.ValueMember = "value";
            cboLoaiNhanVien.DisplayMember = "Name";
            cboLoaiNhanVien.DataSource = _common.GetLoaiNhom();
        }

        private void BindUser()
        {
            dgdNhanVienQuanLy.DataSource = _nhanVienUserBo.GetNvUser();
        }

        private void BindNhanVienQuanLy()
        {
            //grdTVNhom.DataSource = _nhanVienUserBo.GetNhanVienQuanLy();
        }

        private void InitControls()
        {
            try
            {
                
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

        #endregion
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


        private void btnTimNVCC_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                BindNhanVienCc();
            }

        }
        private void BindNhanVienCc()
        {
            var ten = txtNameNVCC.Text;
            var ho = txtFName.Text;
            var maNv = txtSysId.Text == "" ? "0" : txtSysId.Text;
            var loaiNhom = cboLoaiNhanVien.Text == clsCommon.Group.CongNhan ? "0" : "1";
            var maThe = txtCardNo.Text;
            dgvNVCC.DataSource = _nhanVienUserBo.SearchNhanVienChamCong(ten, ho, maNv, loaiNhom, maThe);
        }
   
        public string GetNvAddToGroup()
        {

            const string strSysId = "";

            var table = dgvNVCC.DataSource as System.Data.DataTable;
            return table == null ? strSysId : table.Rows.Cast<DataRow>().Where(row => (row["Check"].ToString() == "1")).Aggregate(strSysId, (current, row) => current + "," + row[clsCommon.Group.MaNhanVien].ToString());

        }

        public bool ValidateData()
        {
            var common = new clsCommon();
            erp.Clear();




            return true;
        }

        private void dgvNVCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewColumn = dgvNVCC.Columns["btnAddUser"];
            if (dataGridViewColumn != null && e.ColumnIndex == dataGridViewColumn.Index)
            {
                //Do Something with your button.
                //MessageBox.Show(dgvNVCC.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString());

                var nhanVienId = Int16.Parse(dgvNVCC.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString());
                var frmAddStaffUser = new frmAddStaffUser(nhanVienId);
                frmAddStaffUser.Show();
                frmAddStaffUser.Closed += frmAddStaffUser_Closed;
            }
        }

        void frmAddStaffUser_Closed(object sender, EventArgs e)
        {
            BindUser();
        }
    }
}