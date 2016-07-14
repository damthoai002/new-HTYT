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
    public partial class FrmMapUserLevel : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmMapUserLevel));

        private readonly clsCommon _common = new clsCommon();


        // Declare private fields
        private readonly NhanVienBo _nhanVienBo = new NhanVienBo();

        #endregion

        #region Constructors

        public FrmMapUserLevel()
        {

            InitializeComponent();


            clsTitleManager.InitTitle(this);

            BindControl();

            BindNhanVienHr();
        }

        private void BindControl()
        {
            //btnSave.Enabled = grdNhanVienProWatch.Rows.Count > 0;
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

                BindNhanVienHr();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }
        }

        private void BindNhanVienHr()
        {

            string lName = txtLName.Text.Trim();
            string fName = txtFName.Text.Trim();
            string maNvUnilever = txtMaNvUnilever.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string cardNo = txtCardNo.Text.Trim();

            DataTable tb;
            tb = _nhanVienBo.GetNhanVienHr(fName, lName, maNvUnilever, userName, cardNo);
            grdNhanVienProWatch.DataSource = tb;
        }

        private void grdNhanVienProWatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewColumn = grdNhanVienProWatch.Columns["AddL2"];
            if (dataGridViewColumn != null && e.ColumnIndex == dataGridViewColumn.Index)
            {

                ClsNhanVien nv = new ClsNhanVien();
                nv.SysId = Int32.Parse(grdNhanVienProWatch.Rows[e.RowIndex].Cells["SysId"].Value.ToString());
                nv.LNAME = grdNhanVienProWatch.Rows[e.RowIndex].Cells["LName"].Value.ToString();
                nv.FNAME = grdNhanVienProWatch.Rows[e.RowIndex].Cells["FName"].Value.ToString();
                nv.MaNVUnilever = grdNhanVienProWatch.Rows[e.RowIndex].Cells["MaNVUnilever"].Value.ToString();
                nv.Username = grdNhanVienProWatch.Rows[e.RowIndex].Cells["USERNAME"].Value.ToString();
                nv.CardNo = grdNhanVienProWatch.Rows[e.RowIndex].Cells["CardNo"].Value.ToString();
                
                var frmAddL2ForL3 = new AddL2ForL3(nv);
                frmAddL2ForL3.ShowDialog();

            }
        }




    }
}