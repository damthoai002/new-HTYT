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
    public partial class FrmUpdateStaff : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmUpdateStaff));

        private readonly clsCommon _common = new clsCommon();


        // Declare private fields
        private readonly NhanVienBo _nhanVienBo = new NhanVienBo();

        #endregion

        #region Constructors

        public FrmUpdateStaff()
        {

            InitializeComponent();


            clsTitleManager.InitTitle(this);

            BindControl();
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

        private void btnTimNVProWatch_Click(object sender, EventArgs e)
        {
            try
            {
              
                BindNhanVienProWatch();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }

        private void BindNhanVienProWatch()
        {
            DataTable tb;
            tb = _nhanVienBo.GetNhanVienProWatch();
            if (tb.Rows.Count <= 0)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmUpdateStaff.NoDataToSave"),
                         clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            grdNhanVienProWatch.DataSource = tb;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdNhanVienProWatch.Rows.Count > 0)
                {
                    _nhanVienBo.InsertNhanVien(Int32.Parse(clsSystemConfig.MaNhanVien.ToString()));
                BindNhanVienProWatch();   
                    MessageBox.Show(clsResources.GetMessage("messages.FrmUpdateStaff.SaveSucess"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("messages.FrmUpdateStaff.NoDataToSave"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }



    }
}