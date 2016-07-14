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
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using UKPI.Controls;
namespace UKPI.Presentation
{
    public partial class frmViewBenhNhanKhambenh : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(frmViewBenhNhanKhambenh));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        private readonly ShareEntityDao _shareEntityDao = new ShareEntityDao();
        private readonly ThongTinKhamBenhDao _thongTinKhamBenhDao = new ThongTinKhamBenhDao();
        private readonly ChotTonKhoDao _chotTonKhoDao = new ChotTonKhoDao();
        QuyetDinhNghiPhep quyetDinhNghiPhep;
        private Dictionary<int, string> danhSachThuoc = new Dictionary<int, string>();
        private List<ThongTinGiaoDich> listCurrentTransactions = new List<ThongTinGiaoDich>();

        readonly System.Data.DataTable _dt = null;
        ComboBox cbm;
        System.Windows.Forms.TextBox autoText;
        DataGridViewCell currentCell;
        //private ComboBox cellComboBox;
        private int _checkRowsCount = 0;

        // Declare constants
        private const string FieldCheck = "colCheck";
        private const String Check = "CHECK";
        private const String ValueTrue = "Y";
        private const String ValueFalse = "N";
        private static string MakhamBenh = "";
       

        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        readonly DataGridViewColumn _originalColumns;
        private DataTable _dtApproveTimesheet;
       
        #endregion



        public frmViewBenhNhanKhambenh()
        {

            InitializeComponent();
            SetDefauldValue();
            this.Text = "Thông tin khám bệnh của bệnh nhân";
           
        }



        public frmViewBenhNhanKhambenh(string maKhamBenh)
        { 
            InitializeComponent();
            MakhamBenh = maKhamBenh;
            SetDefauldValue();
            this.Text = "Thông tin khám bệnh của bệnh nhân";
            LoadThongTinBenhNhan(maKhamBenh);
            LoaThongTinThuoc(maKhamBenh);
        }

       

        private void LoaThongTinThuoc(string maKhamBenh)
        {
            DataTable tb = _thongTinKhamBenhDao.GetThongTinChiTietThuocKhamBenh(maKhamBenh);
            grdToaThuoc.DataSource = tb;
        }

        private void LoadThongTinBenhNhan(string maKhamBenh)
        {
            DataTable tb = _thongTinKhamBenhDao.GetThongTinBenhNhanKhamBenh(maKhamBenh);
            txtMaNhanVien.Text = tb.Rows[0]["MaNhanVien"].ToString();
            txtBenhNhan.Text = tb.Rows[0]["BenhNhan"].ToString();
            dtpNgayKham.Value = DateTime.Parse( tb.Rows[0]["NgayKham"].ToString());
            txtMaBHYT.Text = tb.Rows[0]["MaBHYT"].ToString();
            txtKhuVuc.Text = tb.Rows[0]["KhuVuc"].ToString();
            txtNhomBenh.Text = tb.Rows[0]["NhomBenh"].ToString();
            txtGioiTinh.Text = tb.Rows[0]["GioiTinh"].ToString();
            txtNamSinh.Text = tb.Rows[0]["NamSinh"].ToString();
            txtBoPhan.Text = tb.Rows[0]["BoPhan"].ToString();
            txtICD.Text = tb.Rows[0]["MaICD"].ToString();
            txtDienGiaiICD.Text = tb.Rows[0]["ChanDoanBanDau"].ToString();

            txtTongTienBH.Text = tb.Rows[0]["TTBHYT"].ToString();
            txtTongTienBangChu.Text = tb.Rows[0]["TongTienBangChu"].ToString();

        }


        void grdStores_Sorted(object sender, EventArgs e)
        {
            //this.ProcessDataRow();
        }

        private void GetParam()
        {
        }

        private void SetDefauldValue()
        {
            BindPhongKham();           
            BuildGridViewRow();        
            txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
            txtTienKhamBenh.Text = System.Configuration.ConfigurationManager.AppSettings["GiaKhamBenh"];
        }
        private void LoadThongTinBenhNhan()
        {
            ThongTinBenhNhan ttBenhNhan = _thongTinKhamBenhDao.GetThongTinBenhNhan(clsSystemConfig.UserName);
            txtBenhNhan.Text = ttBenhNhan.FullName;
            txtMaNhanVien.Text = ttBenhNhan.EmployeeID;
            txtMaBHYT.Text = ttBenhNhan.MaBHYT;
            txtNamSinh.Text = ttBenhNhan.NamSinh.ToString();
            txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
   
        }

  
        private void BindBenhNhanInfo()
        {
            string maNhanVien = txtMaNhanVien.Text;
            if (!string.IsNullOrEmpty(maNhanVien))
            {
                List<ThongTinBenhNhan> listBenhNhan = _thongTinKhamBenhDao.SearchThongTinBenhNhan(maNhanVien, string.Empty);
                if (listBenhNhan != null && listBenhNhan.Count == 1)
                {
                    txtBenhNhan.BackColor = Color.White;
                    ThongTinBenhNhan ttbn = listBenhNhan[0];
                    txtBenhNhan.Text = ttbn.FullName;
                    txtMaNhanVien.Text = ttbn.EmployeeID;
                    txtMaBHYT.Text = ttbn.MaBHYT;
                    txtNamSinh.Text = ttbn.NamSinh.ToString();
                    txtCongTy.Text = System.Configuration.ConfigurationManager.AppSettings["Company"];
                    List<GioiTinh> listGoiTinh = _shareEntityDao.LoadGioiTinh();
                    List<BoPhan> listBoPhan = _shareEntityDao.LoadDanhSachBoPhan();
                    List<KhuVuc> listKhuVuc = _shareEntityDao.LoadDanhSachKhuVuc();
                
                }
                else
                {
                   
                }
            }
            else
            {
                
            }
        }

       
        private void BindPhongKham()
        {

            List<PhongKham> listPhongKham = _shareEntityDao.LoadDanhSachPhongKham();
            cbbPhongKham.DataSource = listPhongKham;
            string currentKho = System.Configuration.ConfigurationManager.AppSettings["RCLINIC00002"];
            int currentIndex = listPhongKham.FindIndex(a => a.RoomID == currentKho);
            cbbPhongKham.SelectedIndex = currentIndex;



        }
        
        private void BuildGridViewRow()
        {

           
        }

        private void btnQuyetDinh_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTiepTucKham_Click(object sender, EventArgs e)
        {
                
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (txtMaBHYT.Text != "")
                {
                    ReportInDonThuocFull frmChild = new ReportInDonThuocFull(MakhamBenh);
                    frmChild.Show();
                }


                ReportInDonThuocBH frmChildBh = new ReportInDonThuocBH(MakhamBenh);
                frmChildBh.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

        }

       
        private void CreateTextFromNumber()
        {
             MakeToString _mk ;
            var temp = txtTongTienBH.Text;
            var check = false;
            for (var i = 0; i < temp.Length; i++)
            {
                check = Char.IsLetter(temp, i);
                break;
            }
            if (!check & temp.Length <= 15)
            {
                _mk = new MakeToString(Convert.ToDouble(temp));
                _mk.BlockProcessing();

                //lblblock1.Text = Convert.ToString(_mk.BlockNum[0]);
                //lblblock2.Text = Convert.ToString(_mk.BlockNum[1]);
                //lblblock3.Text = Convert.ToString(_mk.BlockNum[2]);
                //lblblock4.Text = Convert.ToString(_mk.BlockNum[3]);
                //lblblock5.Text = Convert.ToString(_mk.BlockNum[4]);
                txtTongTienBangChu.Text = _mk.ReadThis() + " " + "đồng";
            }
        }

        private void grdToaThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}