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
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class FrmManageStaffs : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmManageStaffs));

        private readonly clsCommon _common = new clsCommon();


        // Declare private fields
        private readonly NhanVienBo _nhanVienBo = new NhanVienBo();

        #endregion

        #region Constructors

        public FrmManageStaffs()
        {
            InitializeComponent();
            clsTitleManager.InitTitle(this);
            BindControl();
            BindOutsource();
        }

        private void BindOutsource()
        {
            cboOutsource.ValueMember = "value";
            cboOutsource.DisplayMember = "Name";
            cboOutsource.DataSource = _common.GetHoatDong();
        }

        private void BindControl()
        {
            btnSave.Enabled = grdNhanVien.Rows.Count > 0;
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
            tb = _nhanVienBo.GetNhanVienProWatch();
            if (tb.Rows.Count <= 0)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffs.NoDataToSave"),
                         clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            grdNhanVien.DataSource = tb;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdNhanVien.Rows.Count > 0)
                {
                    _nhanVienBo.InsertNhanVien(Int32.Parse(clsSystemConfig.MaNhanVien.ToString()));
                    BindNhanVienProWatch();
                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffs.SaveSucess"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.FrmManageStaffs.NoDataToSave"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }

        private void btnTimNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatedData()) return;
                var lName = txtLName.Text;
                var fName = txtFName.Text;
                var email = txtEmail.Text;
                var isDataCc = Int32.Parse(cboOutsource.SelectedValue.ToString());

                grdNhanVien.DataSource = _nhanVienBo.SearchNhanVienChamCong(lName, fName, email, isDataCc);
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        private bool ValidatedData()
        {
            erp.Clear();

            //var fName = txtFName.Text;
            //if (!_common.IsLetterAndDigitExceptWc(fName))
            //{
            //    erp.SetError(txtFName, clsResources.GetMessage("errors.string.specialChar", txtFName.Text));
            //    txtFName.Focus();
            //    return false;
            //}
            //var lName = txtLName.Text;
            //if (!_common.IsLetterAndDigitExceptWc(lName))
            //{
            //    erp.SetError(txtLName, clsResources.GetMessage("errors.string.specialChar", txtLName.Text));
            //    txtLName.Focus();
            //    return false;
            //}

            //var email = txtEmail.Text;
            //if (!_common.IsLetterAndDigitExceptWc(email))
            //{
            //    erp.SetError(txtEmail, clsResources.GetMessage("errors.string.specialChar", txtEmail.Text));
            //    txtEmail.Focus();
            //    return false;
            //}

            return true;
        }

    }
}