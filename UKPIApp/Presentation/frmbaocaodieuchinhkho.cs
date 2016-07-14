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
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using UKPI.Controls;
namespace UKPI.Presentation
{
    public partial class Frmbaocaodieuchinhkho : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Frmbaocaodieuchinhkho));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
   


    
        #endregion

        #region Constructors

        public Frmbaocaodieuchinhkho()
        {

            InitializeComponent();
            grdToaThuoc.AutoGenerateColumns = false;
            clsTitleManager.InitTitle(this);
          
            this.Text = "BÁO CÁO THỐNG KÊ KHO THUỐC";

            BindDefaultData();
        }

        private void BindDefaultData()
        {
           //// dtpNgayTKK.Format = new cust
           // dtpNgayTKK.Format = DateTimePickerFormat.Custom;
           // dtpNgayTKK.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;

        }



        private void Export()
        {
            try
            {
                var dtStoreList = grdToaThuoc.DataSource as System.Data.DataTable;
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
                    var exporter = new TonKhoExporter(true);
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

      
        private void btnExport_Click(object sender, EventArgs e)
        {
           // this.Export();
        }


      

  
        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {
         
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            Export();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
       

    }
}