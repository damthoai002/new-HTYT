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
    public partial class frmbaocaotonkho : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmbaocaotonkho));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly ThongTinNhapKhoDao _thongTinNhapKhoDao = new ThongTinNhapKhoDao();
        private readonly BaoCaoYTeDao _baoCaoYTeDao = new BaoCaoYTeDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep ;
        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        DataGridViewCell currentCell;
        private DateTimePicker cellDateTimePicker;
        private int _checkRowsCount = 0;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();

        // Declare constants
        private const string FieldCheck = "colCheck";
        private const String Check = "CHECK";
        private const String ValueTrue = "Y";
        private const String ValueFalse = "N";
        //param value.
        private String parHanChotDuyetCong = "";
        private String parHanChotDitre = "";
        private String parHanChotVeSom = "";
        private String parChuanTinhCong = "";
        private String parHanMucTinhOt = "";


        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors

        public frmbaocaotonkho()
        {

            InitializeComponent();
          
            //clsTitleManager.InitTitle(this);

            SetDefauldValue();
            this.Text = "BÁO CÁO TỒN KHO";
      
        }

        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            cellDateTimePicker.Visible = false;
        } 
        private void SetDefauldValue()
        {
            
        }

        #endregion     
        private void btnExport_Click(object sender, EventArgs e)
        {
           // this.Export();
        }
        public void SetQuyetDinhNghiPhep(QuyetDinhNghiPhep qd)
        {
            this.quyetDinhNghiPhep = qd;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
         //   grdToaThuoc.DataSource = _baoCaoYTeDao.LoadThongTinLichSuKho(txtKho.Text, txtLoaiThuoc.Text);
            RunReport();
        }
        private void RunReport()
        {
            this.rpBaoCaoTonKho.RefreshReport();
            rpBaoCaoTonKho.Reset();
            rpBaoCaoTonKho.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = rpBaoCaoTonKho.LocalReport;
            var dir = System.IO.Directory.GetCurrentDirectory() + "\\Presentation\\reports\\";

            localReport.ReportPath = dir + "BaoCaoTonKho.rdlc";

            DataTable _tbToaThuoc = new DataTable();



            _tbToaThuoc = _baoCaoYTeDao.LoadThongTinLichSuKho(txtKho.Text, txtLoaiThuoc.Text);

            // Create a report data source for the sales order data
            ReportDataSource dsToaThuoc = new ReportDataSource();
            dsToaThuoc.Name = "dsBaoCaoTonKho";
            dsToaThuoc.Value = _tbToaThuoc;

            localReport.DataSources.Add(dsToaThuoc);
            // Refresh the report
            rpBaoCaoTonKho.RefreshReport();
            this.rpBaoCaoTonKho.RefreshReport();
        }
        private void frmbaocaotonkho_Load(object sender, EventArgs e)
        {

            this.rpBaoCaoTonKho.RefreshReport();
        }
       

    }
}