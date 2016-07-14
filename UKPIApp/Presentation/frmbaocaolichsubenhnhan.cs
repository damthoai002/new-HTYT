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
using Microsoft.Reporting.WinForms;
namespace UKPI.Presentation
{
    public partial class frmbaocaolichsubenhnhan : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmbaocaolichsubenhnhan));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ReportBo _reportBo = new ReportBo();

        #endregion

        #region Constructors

        public frmbaocaolichsubenhnhan()
        {
            InitializeComponent();
            SetDefauldValue();
            this.Text = "Báo cáo lịch sử bệnh nhân";
        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
           //cellDateTimePicker.Visible = false;
        } 
        private void SetDefauldValue()
        {
            SetDateValue();
            SetNhomBenh();
            SetBoPhan();
            SetKhuVuc();

        }

        private void SetKhuVuc()
        {
            cbbKhuVuc.DataSource = _shareEntityDao.LoadDanhSachKhuVucForReport();
        }

        private void SetBoPhan()
        {
            cbbBoPhan.DataSource = _shareEntityDao.LoadDanhSachBoPhanForReport();
        }

        private void SetNhomBenh()
        {
            cbbNhomBenh.DataSource = _shareEntityDao.LoadDanhSachNhomBenhForReport();
        }

        private void SetDateValue()
        {
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Format = DateTimePickerFormat.Custom;

            dtpDenNgay.CustomFormat = "dd-MM-yyyy";
            dtpTuNgay.CustomFormat = "dd-MM-yyyy";
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);
        }

        
        private void LoadThongTinXuatKho()
        {
           // ThongTinBenhNhan ttNhanVien = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
           // grdToaThuoc.DataSource = _thongTinNhapKhoDao.LoadThongTinXuatKho("");
        }
        
        private void BuildGridViewRow()
        {
           

        }

         private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
     
 
        }
         void cellDateTimePickerValueChanged(object sender, EventArgs e)
         {
             //grdToaThuoc.CurrentCell.Value = cellDateTimePicker.Value.ToString("dd-MM-yyyy");//convert the date as per your format
             //cellDateTimePicker.Visible = false;
         }

      
     
       
       

        private void Export()
        {
            
        }


        #endregion

      
        private void btnExport_Click(object sender, EventArgs e)
        {
           // this.Export();
        }

      

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            Export();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           // grdToaThuoc.DataSource = _baoCaoYTeDao.LoadThongTinLichSuBenhNhan(txtMaBenhNhan.Text, txtMaBHYT.Text, txtTenBenhNhan.Text);
            RunReport();
        }

        private void RunReport()
        {
            this.rvBaoCaoLSBN.RefreshReport();
            rvBaoCaoLSBN.Reset();
            rvBaoCaoLSBN.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = rvBaoCaoLSBN.LocalReport;
            var dir = System.IO.Directory.GetCurrentDirectory() + "\\Presentation\\reports\\";

            localReport.ReportPath = dir + "BaoCaoLichSuBenhNhan.rdlc";

            DataTable _tbToaThuoc = new DataTable();

            string tenBenhNhan = txtTenBenhNhan.Text;
            string maBenhNhan = txtMaBenhNhan.Text;
            string maBHYT = txtMaBHYT.Text;
            string khuVuc = cbbKhuVuc.Text != "Tất cả" ? cbbKhuVuc.Text : "";
            string boPhan = cbbBoPhan.Text != "Tất cả" ? cbbBoPhan.Text :"";
            string nhomBenh = cbbNhomBenh.Text != "Tất cả" ? cbbNhomBenh.Text: "" ;
            string tuNgay = dtpTuNgay.Value.ToString("yyyy-MM-dd");
            string denNgay = dtpDenNgay.Value.ToString("yyyy-MM-dd");
            string maBenh = txtMaBenh.Text;
            string tenBenh = txtTenBenh.Text;

            _tbToaThuoc = _reportBo.baoCaoLichSuBenhNhan( maBenhNhan, tenBenhNhan, maBHYT, tuNgay, denNgay, khuVuc, boPhan, nhomBenh, maBenh, tenBenh);

            // Create a report data source for the sales order data
            ReportDataSource dsToaThuoc = new ReportDataSource();
            dsToaThuoc.Name = "dsBaoCaoLichSuBenhNhan";
            dsToaThuoc.Value = _tbToaThuoc;

            localReport.DataSources.Add(dsToaThuoc);
            // Refresh the report
            rvBaoCaoLSBN.RefreshReport();
            this.rvBaoCaoLSBN.RefreshReport();
        }



        private void cbbBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmbaocaolichsubenhnhan_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
            this.rvBaoCaoLSBN.RefreshReport();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
       

    }
}