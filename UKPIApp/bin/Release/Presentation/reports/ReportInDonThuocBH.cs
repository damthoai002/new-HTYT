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
    public partial class ReportInDonThuocBH : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ReportInDonThuocBH));
        private readonly clsCommon _common = new clsCommon();
        private readonly ReportBo _reportBo = new ReportBo();
        private string maKhamBenh = "";

        public ReportInDonThuocBH()
        {
            InitializeComponent();
            //BindReport();
        }

        public ReportInDonThuocBH(string makhamBenh)
        {
            InitializeComponent();
            this.maKhamBenh = makhamBenh;
           
        }

       





        private void ReportInDonThuocBH_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.Reset();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = reportViewer1.LocalReport;
            var dir = System.IO.Directory.GetCurrentDirectory() + "\\Presentation\\reports\\";

            localReport.ReportPath = dir + "ReportInDonThuoc.rdlc";

            DataTable _tbToaThuoc = new DataTable();

            _tbToaThuoc = _reportBo.GetToaThuocBh(this.maKhamBenh);

            // Create a report data source for the sales order data
            ReportDataSource dsToaThuoc = new ReportDataSource();
            dsToaThuoc.Name = "DataSet1";
            dsToaThuoc.Value = _tbToaThuoc;

            localReport.DataSources.Add(dsToaThuoc);
            // Refresh the report
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }


    }
}
