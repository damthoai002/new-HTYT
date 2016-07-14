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
    public partial class FrmUpdateStaffCc : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmUpdateStaffCc));

        private readonly clsCommon _common = new clsCommon();
        private readonly ClsNhanVien _nhanVien = new ClsNhanVien();

        // Declare private fields
        private readonly NhanVienBo _nhanVienBo = new NhanVienBo();

        #endregion

        #region Constructors

        public FrmUpdateStaffCc()
        {

            InitializeComponent();


            clsTitleManager.InitTitle(this);

            BindControl();
            BindNhanVienProWatch();
        }

        private void BindControl()
        {
            btnSave.Enabled = grdNhanVienProWatch.Rows.Count > 0;
        }


        private void InitControls()
        {

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

        private void BindNhanVienProWatch()
        {
            DataTable tb;
            tb = _nhanVienBo.SearchNhanVienChamCong("","","",0);
            grdNhanVienProWatch.DataSource = tb;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdNhanVienProWatch.Rows.Count > 0)
                {
                    var frmUpdateStaffInfo = new UpdateStaffInfo(_nhanVien);
                    frmUpdateStaffInfo.Show();

                    frmUpdateStaffInfo.FormClosed += frmUpdateStaffInfo_FormClosed;
                    //MessageBox.Show(clsResources.GetMessage("messages.FrmUpdateStaffCc.SaveSucess"),
                    //    clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.FrmUpdateStaffCc.NoDataToSave"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }

        void frmUpdateStaffInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            BindNhanVienProWatch();
        }

        private void grdNhanVienProWatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = true;

            var aa = e.RowIndex;
            _nhanVien.SysId = Int32.Parse(grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.SysId].Value.ToString());
            _nhanVien.LNAME = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.LNAME].Value.ToString();
            _nhanVien.FNAME = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.FNAME].Value.ToString();
            _nhanVien.MI = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.MI].Value.ToString();
            _nhanVien.BADGE_STATUS_DESC = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.BADGE_STATUS].Value.ToString();
            _nhanVien.BADGE_TYPE_DESC = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.BADGE_TYPE].Value.ToString();
            _nhanVien.ISSUE_DATE = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.ISSUE_DATE].Value.ToString();
            _nhanVien.EXPIRE_DATE = grdNhanVienProWatch.Rows[e.RowIndex].Cells[clsCommon.NhanVien.EXPIRE_DATE].Value.ToString();
            _nhanVien.GioiTinh = false;
        }



    }
}