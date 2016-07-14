using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft.SqlServer.Server;
using UKPI.BusinessObject;
using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.Presentation.ApproveTSLookup
{
    public partial class ReportInDonThuocFull : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ReportInDonThuocFull));
        private readonly clsCommon _common = new clsCommon();
        private readonly ReportBo _reportBo = new ReportBo();
        public string maKhamBenh { get; set; }
        public ReportInDonThuocFull()
        {
            //InitializeComponent();
            BindReport();
        }
        public ReportInDonThuocFull(string maKhambenh)
        {
            this.maKhamBenh = maKhambenh;
            InitializeComponent();
            //BindReport();
        }

        private void BindReport()
        {
            try
            {
                this.reportViewer1.RefreshReport();
                reportViewer1.Reset();
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                LocalReport localReport = reportViewer1.LocalReport;

                var dir = System.IO.Directory.GetCurrentDirectory() + "\\Presentation\\reports\\";

                localReport.ReportPath = dir + "ReportInDonThuocFull.rdlc";

                DataTable _tbToaThuoc = new DataTable();

                _tbToaThuoc = _reportBo.GetToaThuoc(this.maKhamBenh);

                // Create a report data source for the sales order data
                ReportDataSource dsToaThuoc = new ReportDataSource();
                dsToaThuoc.Name = "DataSet1";
                dsToaThuoc.Value = _tbToaThuoc;

                localReport.DataSources.Add(dsToaThuoc);



                // Refresh the report
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }

        }
        private void ReportInDonThuocFull_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.Reset();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = reportViewer1.LocalReport;

            var dir = System.IO.Directory.GetCurrentDirectory() + "\\Presentation\\reports\\";

            localReport.ReportPath = dir + "ReportInDonThuocFull.rdlc";

            DataTable _tbToaThuoc = new DataTable();

            _tbToaThuoc = _reportBo.GetToaThuoc(this.maKhamBenh);

            // Create a report data source for the sales order data
            ReportDataSource dsToaThuoc = new ReportDataSource();
            dsToaThuoc.Name = "DataSet1";
            dsToaThuoc.Value = _tbToaThuoc;

            localReport.DataSources.Add(dsToaThuoc);



            // Refresh the report
            reportViewer1.RefreshReport();

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }


    }
}
