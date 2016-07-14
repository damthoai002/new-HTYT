using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using log4net;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Runtime.InteropServices;
using Excel;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Mail;
using UKPI.Presentation;
using UKPI.ValueObject;
using UKPI.Controls;
using FPT.Component.ExcelPlus;
using DataTable = System.Data.DataTable;

namespace UKPI.Utils
{
    /// <summary>
    /// Summary description for Common.
    /// </summary>
    /// <remarks>
    /// Author:			Nguyen Minh Duc. G3.
    /// Created date:	14/05/2006
    /// </remarks>
    public class clsCommon
    {
        private static log4net.ILog log = LogManager.GetLogger(typeof(clsCommon));
        public clsCommon()
        {
            string strBrush = ConfigurationManager.AppSettings["WriteArea"];
            switch (strBrush)
            {
                case "LemonChiffon":
                    brColor = new SolidBrush(Color.LemonChiffon);
                    break;
                case "OrangeRed":
                    brColor = new SolidBrush(Color.OrangeRed);
                    break;
                case "Tomato":
                    brColor = new SolidBrush(Color.Tomato);
                    break;
                case "SkyBlue":
                    brColor = new SolidBrush(Color.SkyBlue);
                    break;
                default:
                    brColor = new SolidBrush(Color.Moccasin);
                    break;
            }
        }
        //public static Brush brColor = new SolidBrush(Color.LemonChiffon);
        public static Brush brColor = new SolidBrush(Color.Moccasin);
        public static Brush txtColor = new SolidBrush(Color.DarkBlue);
        #region Window Control Utilities
        //Region Window Control Utilities use static methods

        /// <summary>
        /// Remove all toolbar
        /// </summary>
        /// <param name="frm"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        public static void RemoveAllToolBar(Form frm)
        {
            ToolBar tb = null;
            foreach (Control ctrl in frm.Controls)
            {
                tb = ctrl as ToolBar;
                if (tb != null)
                {
                    frm.Controls.Remove(tb);
                    tb.Dispose();
                }
            }
        }
        /// <summary>
        /// Register the control that user only input number
        /// </summary>
        /// <param name="control"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        public static void RegNumberOnly(Control control)
        {
            control.KeyPress += new KeyPressEventHandler(Control_OnKeyPress);
        }

        /// <summary>
        /// Register the control that user only input positive numeric
        /// </summary>
        /// <param name="control"></param>
        /// <remarks>
        /// Created: ThienDLV	22/10/2007
        /// </remarks>
        public static void RegPosNumericOnly(Control control)
        {
            control.KeyPress += new KeyPressEventHandler(Control_OnKeyPress_PosNumeric);
        }

        /// <summary>
        /// Handle the control that user only input number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        private static void Control_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!(chr >= '0' && chr <= '9' || chr == 8 || chr == 13 || chr == ';'))
                e.Handled = true;
        }

        /// <summary>
        /// Handle the control that user only inputs positive numeric
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Created: ThienDLV	22/10/2007
        /// </remarks>
        private static void Control_OnKeyPress_PosNumeric(object sender, KeyPressEventArgs e)
        {
            string s = ((System.Windows.Forms.TextBox)sender).Text;
            char chr = e.KeyChar;
            if (!(chr >= '0' && chr <= '9' || chr == 8 || chr == 13 || chr == '.'))
            {
                e.Handled = true;
            }
            else
            {
                s += chr.ToString();
                if (chr == '.')
                    try
                    {
                        Convert.ToDouble(s + "0");
                    }
                    catch
                    {
                        e.Handled = true;
                    }
            }
        }

        /// <summary>
        /// Register the data grid that auto resize column width
        /// </summary>
        /// <param name="grd"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        public static void RegAutoSizeCol(DataGrid grd)
        {
            DataGrid_Resize(grd, null);
            //Thanhnq comment: fix loi maximize man hinh thi mo man hinh khac bi loi.
            //grd.Resize -=new EventHandler(DataGrid_Resize);	
            //grd.Resize +=new EventHandler(DataGrid_Resize);			
        }

        /// <summary>
        /// Handle the data grid that auto resize column width
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        private static void DataGrid_Resize(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            DataGrid grd = (DataGrid)sender;

            //			DataGridTableStyle grdStyle = grd.TableStyles[0];
            //			if(grdStyle == null || grdStyle.GridColumnStyles == null || grdStyle.GridColumnStyles.Count == 0)
            //				return;

            grd.BeginInit();

            foreach (DataGridTableStyle grdStyle in grd.TableStyles)
            {

                int width = grd.Width - 56;//53;//real is 56

                GridColumnStylesCollection cols = grdStyle.GridColumnStyles;

                //calculate total of col
                int oldwidth = 0;
                foreach (DataGridColumnStyle col in cols)
                {
                    oldwidth = oldwidth + col.Width;
                }

                if (oldwidth == 0)
                    return;

                int count = grdStyle.GridColumnStyles.Count;

                double scale = 1.0 * width / oldwidth;
                foreach (DataGridColumnStyle col in cols)
                {
                    if (col.Width != 0)
                        col.Width = (int)(col.Width * scale);
                }

            }

            grd.EndInit();

        }


        /// <summary>
        /// Add week to ComboBox
        /// </summary>
        /// <param name="cbo"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        public static void AddWeekToCombo(ComboBox cbo)
        {
            cbo.Items.Add("");
            for (int week = 1; week <= 52; week++)
            {
                //cbo.Items.Add(week.ToString().PadLeft(2, '0'));				
                cbo.Items.Add(week.ToString());
            }
            cbo.MaxDropDownItems = 18;
        }

        /// <summary>
        /// Add year to ComboBox
        /// </summary>
        /// <param name="cbo"></param>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        public static void AddYearToCombo(ComboBox cbo)
        {
            //DateTime date = new DateTime();  DucND comment
            int year = DateTime.Now.Year;
            year = year - 2;
            cbo.Items.Add("");
            for (int i = 0; i < 5; i++)
            {
                cbo.Items.Add(year.ToString().PadLeft(4, '0'));
                year++;
            }
        }

        /// <summary>
        /// Load all regions
        /// </summary>
        /// <param name="cbo"></param>
        /// <remarks>
        /// Author		:	ThienDLV
        /// Created day	:	26/10/2007
        /// </remarks>
        /// <summary>
        /// Load all regions
        /// </summary>
        public static System.Data.DataTable LoadRegions()
        {
            string strConn = clsCommon.GetConnectionString();
            string strSql = "SELECT REGION_CODE, REGION_NAME FROM FPT_ENV_REGION_HIERARCHY";
            try
            {
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message, ex);
                //MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion

        #region Hilight DataGrid (Rows and colums)

        /// <summary>
        /// Hiligh column of table that they can Edit
        /// </summary>
        /// <param name="dt">int put a ref parameter</param>
        /// <remarks>
        /// Auther		: Cuongvd G3 Fsoft HCM
        /// created day	: 17/01/207
        /// </remarks>
        public void HilighColumn(ref DataGrid grd, System.Drawing.Color color)
        {
            if (grd.TableStyles[0].GridColumnStyles.Count <= 0)
                return;
            DataGridTableStyle grdNew = new DataGridTableStyle();

            for (int i = 0; i < grd.TableStyles[0].GridColumnStyles.Count; i++)
            {
                if (grd.TableStyles[0].GridColumnStyles[i].ReadOnly == false && (grd.TableStyles[0].GridColumnStyles[i] is DataGridTextBoxColumn))
                {
                    DataGridTextBoxColumn colTextBox = (DataGridTextBoxColumn)grd.TableStyles[0].GridColumnStyles[i];
                    FormattableTextBoxColumn colAdd = new FormattableTextBoxColumn();
                    colAdd.MappingName = grd.TableStyles[0].GridColumnStyles[i].MappingName;
                    colAdd.Width = grd.TableStyles[0].GridColumnStyles[i].Width;
                    colAdd.ReadOnly = false;
                    colAdd.TextBox.MaxLength = colTextBox.TextBox.MaxLength;
                    RegNumberOnly(colAdd.TextBox);
                    colAdd.NullText = "";
                    colAdd.HeaderText = grd.TableStyles[0].GridColumnStyles[i].HeaderText;
                    colAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
                    //arlCol.Add(colAdd);
                    grdNew.GridColumnStyles.Add(colAdd);
                }
                else
                    if (grd.TableStyles[0].GridColumnStyles[i] is DataGridBoolColumn)
                    {
                        DataGridBoolColumn colBool = (DataGridBoolColumn)grd.TableStyles[0].GridColumnStyles[i];
                        FormattableBooleanColumn colBoolAdd = new FormattableBooleanColumn();
                        colBoolAdd.MappingName = colBool.MappingName;
                        colBoolAdd.ReadOnly = false;
                        colBoolAdd.TrueValue = colBool.TrueValue; ;
                        colBoolAdd.FalseValue = colBool.FalseValue; ;
                        colBoolAdd.AllowNull = colBool.AllowNull;
                        colBoolAdd.HeaderText = colBool.HeaderText;
                        colBoolAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
                        //arlCol.Add(colBoolAdd);
                        grdNew.GridColumnStyles.Add(colBoolAdd);
                    }
                    else
                        grdNew.GridColumnStyles.Add(grd.TableStyles[0].GridColumnStyles[i]);
                //arlCol.Add(grd.TableStyles[0].GridColumnStyles[i]);
            }
            grdNew.HeaderBackColor = grd.TableStyles[0].HeaderBackColor;
            grdNew.AlternatingBackColor = grd.TableStyles[0].AlternatingBackColor;
            grd.TableStyles.RemoveAt(0);
            grd.TableStyles.Add(grdNew);

            //			foreach(object ojb in arlCol)
            //			{
            //				grd.TableStyles[0].GridColumnStyles.Add(ojb);
            //			}
        }
        /// <summary>
        /// Overload 2 Cuongvd
        /// Without color
        /// </summary>
        /// <param name="grd"></param>
        /// 
        public void HilighColumn(ref DataGrid grd)
        {
            if (grd.TableStyles[0].GridColumnStyles.Count <= 0)
                return;
            DataGridTableStyle grdNew = new DataGridTableStyle();

            for (int i = 0; i < grd.TableStyles[0].GridColumnStyles.Count; i++)
            {
                if (grd.TableStyles[0].GridColumnStyles[i].ReadOnly == false && (grd.TableStyles[0].GridColumnStyles[i] is DataGridTextBoxColumn))
                {
                    DataGridTextBoxColumn colTextbox = (DataGridTextBoxColumn)grd.TableStyles[0].GridColumnStyles[i];
                    FormattableTextBoxColumn colAdd = new FormattableTextBoxColumn();
                    colAdd.MappingName = grd.TableStyles[0].GridColumnStyles[i].MappingName;
                    colAdd.ReadOnly = false;
                    colAdd.TextBox.MaxLength = colTextbox.TextBox.MaxLength;
                    colAdd.NullText = colTextbox.NullText;
                    clsCommon.RegNumberOnly(colAdd.TextBox);
                    colAdd.HeaderText = grd.TableStyles[0].GridColumnStyles[i].HeaderText;
                    colAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
                    //arlCol.Add(colAdd);
                    grdNew.GridColumnStyles.Add(colAdd);


                }
                else
                    if (grd.TableStyles[0].GridColumnStyles[i] is DataGridBoolColumn)
                    {
                        DataGridBoolColumn colBool = (DataGridBoolColumn)grd.TableStyles[0].GridColumnStyles[i];
                        FormattableBooleanColumn colBoolAdd = new FormattableBooleanColumn();
                        colBoolAdd.MappingName = colBool.MappingName;
                        colBoolAdd.ReadOnly = false;
                        colBoolAdd.TrueValue = colBool.TrueValue; ;
                        colBoolAdd.FalseValue = colBool.FalseValue;
                        colBoolAdd.AllowNull = colBool.AllowNull;
                        colBoolAdd.HeaderText = colBool.HeaderText;
                        colBoolAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
                        grdNew.GridColumnStyles.Add(colBoolAdd);
                    }
                    else
                        grdNew.GridColumnStyles.Add(grd.TableStyles[0].GridColumnStyles[i]);
            }
            grdNew.HeaderBackColor = grd.TableStyles[0].HeaderBackColor;
            grd.TableStyles.Clear();
            grd.TableStyles.Add(grdNew);
        }
        /// <summary>
        /// for evnet change backcolor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Created by	: cuongvd G3 fsoftHCM
        /// create date	: 18-jan-2007
        /// </remarks>
        private void SetHeaderCellFormat(object sender, DataGridFormatCellEventArgs e)
        {
            try
            {
                e.BackBrush = brColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        public class ApproveTimesheet
        {
            public const string DateFormatDb = "yyyyMMdd";
            public const string DateFormatDisplay = "dd/MM/yyyy";
            public const string DateFormatForDtp = "MM/dd/yyyy";
            public const string TimeFormat = "hh:mm";

            public const string All = "All";
            public const string PheDuyet = "Đã Phê Duyêt";
            public const string ChuaPheDuyet = "Chưa Phê Duyệt";
            public const string On = "On";
            public const string Off = "Off";
            public const string Ca1 = "Ca 1";
            public const string Ca2 = "Ca 2";
            public const string Ca3 = "Ca 3";





        }

        public class Parameter
        {
            public const string HanChotDuyetcong = "HAN_CHOT_DUYETCONG";
            public const string HanChotDitre = "HAN_CHOT_DITRE";
            public const string HanChotVesom = "HAN_CHOT_VESOM";
            public const string ChuanTinhCong = "CHUAN_TINHCONG";
            public const string HanMucTinhOt = "HAN_MUC_TINH_OT";

            public const string ParamName = "Param_Name";
            public const string ParamType = "Param_Type";
            public const string ParamValue = "Param_Value";
            public const string ParamGroup = "Param_Group";
            public const string Status = "Status";
            public const string Description = "Description";

        }

        public class CreateTimesheet
        {
            public const string SysId = "SysId";
            public const string TruongNhom = "TenTruongNhom";
            public const string MaTruongNhom = "TruongNhomId";
            public const string Outsource = "IsOutsource";
            public const string Nhom = "TenNhom";
            public const string NhomId = "NhomId";
            public const string LoaiNhom = "LoaiNhom";

            public const string L0XacNhan = "L0XacNhanTen";
            public const string L0XacNhanMa = "L0XacNhanId";
            public const string L1XacNhan = "L1XacNhanTen";
            public const string L1XacNhanMa = "L1XacNhanId";
            public const string L2XacNhan = "L2XacNhanTen";
            public const string L2XacNhanMa = "L2XacNhanId";
            public const string L3XacNhan = "L3XacNhanTen";
            public const string L3XacNhanMa = "L3XacNhanId";
            public const string L4XacNhan = "L4XacNhanTen";
            public const string L4XacNhanMa = "L4XacNhanId";
            public const string TuanLamViec = "TuanLamViec";

        }

        public class Group
        {

            public const string Outsource = "IsOutsource";
            public const string IsOt = "IsOt";
            public const string Nhom = "TenNhom";
            public const string NhomId = "SysId";
            public const string MoTa = "MoTa";
            public const string LoaiNhom = "LoaiNhom";
            public const string HanhChinh = "Hành Chính";
            public const string CongNhan = "Công Nhân";
            public const string MaNhanVien = "MaNhanVien";
            public const string UserName = "USERNAME";

            public const string MaNhom = "MaNhom";
            public const string MaTruongNhom = "MaTruongNhom";



        }

        public class CaLamViecNhom
        {

            public const string SysId = "SysId";
            public const string MaNhom = "MaNhom";
            public const string NgayVao = "NgayVao";
            public const string MaCaHeThong = "MaCaHeThong";
            public const string MaCaLaViec = "MaCaLaViec";
            public const string DauDocTheVaoId = "DauDocTheVaoId";
            public const string DauDocTheRaId = "DauDocTheRaId";
            public const string Tuan = "Tuan";
            public const string ThuTrongTuan = "ThuTrongTuan";
            public const string IsOff = "IsOff";
            public const string IsOffCaution = "IsOff_Caution";
            public const string IsOT = "IsOT";
            public const string PhanXuongName = "PhanXuongName";
            public const string PhanXuongId = "PhanXuongId";
            public const string CreateDate = "CreateDate";
            public const string CreaterId = "CreaterId";
            public const string LastUpDate = "LastUpDate";
            public const string lastUpdateId = "lastUpdateId";
            public const string Is_Active = "Is_Active";



        }

        public class NhanVien
        {
            public const string SysId = "SysId";
            public const string ID_Prowatch = "ID_Prowatch";
            public const string MaNVUnilever = "MaNVUnilever";
            public const string MaLoaiNhanVien = "MaLoaiNhanVien";
            public const string Location = "Location";
            public const string CardNo = "CardNo";
            public const string LNAME = "LNAME";
            public const string FNAME = "FNAME";
            public const string MI = "MI";
            public const string GioiTinh = "GioiTinh";
            public const string Email = "Email";
            public const string BADGE_STATUS = "BADGE_STATUS";
            public const string BADGE_STATUS_DESC = "BADGE_STATUS_DESC";
            public const string BADGE_TYPE = "BADGE_TYPE";
            public const string BADGE_TYPE_DESC = "BADGE_TYPE_DESC";
            public const string ISSUE_DATE = "ISSUE_DATE";
            public const string EXPIRE_DATE = "EXPIRE_DATE";
            public const string TSTAMP = "TSTAMP";
            public const string IsDataCC = "IsDataCC";
            public const string IsOutsource = "IsOutsource";
            public const string OutsourceId = "OutsourceId";
            public const string CreateDate = "CreateDate";
            public const string CreaterId = "CreaterId";
            public const string LastUpDate = "LastUpDate";
            public const string lastUpdateId = "lastUpdateId";
            public const string Is_Active = "Is_Active";
        }

        public class WareHouseType {
            public const string XUKH = "XUKH"; //XUẤT KHÁC
            public const string NHKH = "NHKH"; //NHẬP KHÁC
            public const string NHMU = "NHMU"; //NHẬP MUA
            public const string XUPH = "XUPH"; //XUẤT PHÁT THUỐC
            public const string NHCH = "NHCH"; //NHẬP CHUYỂN KHO NỘI BỘ
            public const string XUCH = "XUCH"; //XUẤT CHUYỂN KHO NỘI BỘ
            public const string XUDC = "XUDC"; //XUẤT ĐIỀU CHỈNH KHO NỘI BỘ
            public const string NHDC = "NHDC"; //NHẬP ĐIỀU CHỈNH
            public const string TKKH = "TKKH"; //THỐNG KÊ KHO
            public const string XACNHAN = "Xác Nhận"; //Xác Nhận khám bệnh
            public const string LUUIN = "Lưu in"; //luu in khám bệnh
            public const string TANDUOC = "TD";
            public const string XUATKHOLE = "Xuất kho lẻ";
            public const string YHOCCOTRUYEN = "YHCT"; 
        }

        public class NgayNghi
        {

            public const string NghiPhep = "PHE";
            public const string TaiNan = "TAI";
            public const string ThaiSan = "THAI";

            public static string GetMaGiaoDich(string maNV, string loaiNghiPhep)
            {
                return loaiNghiPhep + DateTime.Now.ToString("yyyyMMddHHmmssfff") + maNV;
            }


        }

        public static string FormatDateToString(DateTime date)
        {
            return date.ToString(ApproveTimesheet.DateFormatDb);
        }

        public static DateTime FormatStringToDateTime(string date)
        {
            return DateTime.ParseExact(date, ApproveTimesheet.DateFormatDb, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static DateTime FormatStringToDateTime(string date, string format)
        {
            return DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string FormatDateToDisplay(DateTime date)
        {
            return date.ToString(ApproveTimesheet.DateFormatDisplay);
        }

        public static string FormatDateToDisplay(string date)
        {
            if (!CheckDateDisplay(date))
            {
                var strDate = DateTime.ParseExact(date.Trim(), ApproveTimesheet.DateFormatDb, System.Globalization.CultureInfo.InvariantCulture);
                return strDate.ToString(ApproveTimesheet.DateFormatDisplay);
            }
            else
            {
                return date;
            }

        }

        public static bool CheckDateDisplay(string date)
        {

            try
            {
                var dtmDate = DateTime.ParseExact(date.Trim(), ApproveTimesheet.DateFormatDisplay, System.Globalization.CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public static double CalOtTime(string startHour, string endHour, string standardHour)
        {
            var otTTime = 0.00;

            var startTime = TimeSpan.Parse(startHour.Replace("h",":"));
            var endTime = TimeSpan.Parse(endHour.Replace("h", ":"));

            otTTime = (endTime - startTime).TotalHours > double.Parse(standardHour) ? (endTime - startTime).TotalHours - double.Parse(standardHour) : 0.00;


            return otTTime;
        }


        public static string CheckTimeValue(string time)
        {
            try
            {
                var strTime = TimeSpan.Parse(time.Replace("h",":"));
                return strTime.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Kill Excel process that created by UKPI
        /// </summary>
        /// <param name="preExcelProcesses"></param>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public void KillProcess(Process[] preExcelProcesses)
        {
            Process[] excelProcesses = Process.GetProcessesByName("EXCEL");
            foreach (Process process in excelProcesses)
            {
                if (!Contain(preExcelProcesses, process))
                {
                    process.Kill();
                }
            }
        }

        public static int GetWeek(DateTime dtm)
        {
            System.Globalization.GregorianCalendar gCalendar = new System.Globalization.GregorianCalendar();
            return gCalendar.GetWeekOfYear(dtm, System.Globalization.CalendarWeekRule.FirstDay, System.DayOfWeek.Monday);
        }

        /// <summary>
        /// Check whether processes contains process
        /// </summary>
        /// <param name="processes"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        private bool Contain(Process[] processes, Process process)
        {
            foreach (Process p in processes)
            {
                if (p.Id == process.Id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get Header Title and Index of table of DataGridTableStyle
        /// </summary>
        /// <param name="view"></param>
        /// <param name="cols"></param>
        /// <param name="headers"></param>
        /// <param name="indexes"></param>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public void GetExportInfo(DataView view, GridColumnStylesCollection cols, ref string[] headers, ref int[] indexes)
        {
            int count = cols.Count;
            headers = new string[count];
            indexes = new int[count];
            System.Data.DataTable dt = view.Table;

            for (int i = 0; i < count; i++)
            {
                headers[i] = cols[i].HeaderText;
                indexes[i] = GetColumnIndex(dt, cols[i].MappingName);
            }
        }

        /// <summary>
        /// Parse string to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int GetInt(string value)
        {
            try
            {
                int i = int.Parse(value);
                return i;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Check whether the email is valid.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool IsEmail(string email)
        {
            email = email.Trim();
            if (email == null || email.Length < 5)
            {
                return false;
            }
            int index = email.IndexOf('@');
            if (email.IndexOf('@', index + 1) >= 0)
            {
                return false;
            }
            if (index < 1 || index >= email.Length - 3)
            {
                return false;
            }
            index = email.IndexOf('.', index + 2);
            if (index < 0 || index >= email.Length - 1)
            {
                return false;
            }

            int length = email.Length;
            for (int i = 0; i < length; i++)
            {
                if (!IsValidCharMail(email[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check whether the string just contain character and number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool IsLetterAndDigit(string value)
        {
            return value.Where(chr => chr != ' ').All(char.IsLetterOrDigit);
        }

        public bool IsDigit(string value)
        {
            return value.Where(chr => chr != '*' || chr != ' ').All(char.IsDigit);
        }

        public bool IsLetterAndDigitExceptWc(string value)
        {
            //   bool blnResult = true;
            //   char[] arrChr = value.ToCharArray();
            //for (int i = 0; i < arrChr.Length; i++)
            //   {
            //       if (arrChr[i].ToString() != " " || arrChr[i] != ' ')
            //           {
            //               blnResult = chr.IsLetterOrDigit;
            //           }

            //   }


            return value.Where(chr => chr != '*' || chr.ToString() != " ").All(char.IsLetterOrDigit);
        }


        /// <summary>
        /// Check whether this character is a valid email character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool IsValidCharMail(char chr)
        {
            if ((chr >= 'A' && chr <= 'Z') || (chr >= 'a' && chr <= 'z')
                || (chr >= '0' && chr <= '9') || chr == '.' || chr == '@'
                || chr == '-' || chr == '_')
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Parse string to double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public double GetDouble(string value)
        {
            try
            {
                double i = double.Parse(value);
                return i;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Clear errors of all DataRows of DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public void ClearErrors(System.Data.DataTable dt)
        {
            if (dt == null)
                return;

            foreach (DataRow row in dt.Rows)
                row.ClearErrors();
        }
        /// <summary>
        /// check whether the value just contains the number character.
        /// </summary>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool IsOnlyContainNumber(string value)
        {
            foreach (char chr in value)
            {
                if (!char.IsNumber(chr))
                    return false;
            }
            return true;
        }

        public static bool IsNumeric(string s)
        {
            try
            {
                Convert.ToInt32(s);
                int Numeric = int.Parse(s);
                if (Numeric == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                //return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFloat(string s)
        {
            try
            {
                float Numeric = float.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Is Numeric Positive
        /// Creator: Sonlv
        /// Create Date: 08/04/2010
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsNumericPositive(String strValue)
        {
            if (string.IsNullOrEmpty(strValue.Trim()))
            {
                return true;
            }

            try
            {
                if (Convert.ToDouble(strValue) > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static int StringToInt(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Select distinct valuemember From a DataTable
        /// </summary>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        /// <param name="dt"></param>
        /// <param name="ValueMember"></param>
        /// <param name="DisplayMember"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public System.Data.DataTable GetDistict(System.Data.DataTable dt, string ValueMember, string DisplayMember)
        {
            System.Data.DataTable dtDistinct = new System.Data.DataTable();

            if (ValueMember == DisplayMember)
            {
                dtDistinct.Columns.Add(ValueMember, dt.Columns[ValueMember].GetType());

                int count = dt.Rows.Count;
                if (count == 0) return dtDistinct;

                DataView view = new DataView(dt);
                view.Sort = DisplayMember;

                DataRow row = dtDistinct.NewRow();
                row[ValueMember] = view[0][ValueMember];
                dtDistinct.Rows.Add(row);

                for (int i = 1; i < count; i++)
                {
                    if (!view[i][ValueMember].Equals(view[i - 1][ValueMember]))
                    {
                        row = dtDistinct.NewRow();
                        row[ValueMember] = view[i][ValueMember];
                        dtDistinct.Rows.Add(row);
                    }
                }
            }
            else
            {
                dtDistinct.Columns.Add(ValueMember, dt.Columns[ValueMember].GetType());
                dtDistinct.Columns.Add(DisplayMember, dt.Columns[DisplayMember].GetType());

                int count = dt.Rows.Count;
                if (count == 0) return dtDistinct;

                DataView view = new DataView(dt);
                view.Sort = DisplayMember;

                DataRow row = dtDistinct.NewRow();
                row[ValueMember] = view[0][ValueMember];
                row[DisplayMember] = view[0][DisplayMember];
                dtDistinct.Rows.Add(row);

                for (int i = 1; i < count; i++)
                {
                    if (!view[i][ValueMember].Equals(view[i - 1][ValueMember]))
                    {
                        row = dtDistinct.NewRow();
                        row[ValueMember] = view[i][ValueMember];
                        row[DisplayMember] = view[i][DisplayMember];
                        dtDistinct.Rows.Add(row);
                    }
                }
            }

            return dtDistinct;
        }

        /// <summary>
        /// format datetime
        /// </summary>
        /// <param name="date">format YYYYMMDD</param>
        /// <returns>format MM/DD/YYYY</returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public string FormatDate(string date)
        {
            if (date == null || date.Length != 8)
                return date;

            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);

            return day + "/" + month + "/" + year;
        }
        /// <summary>
        /// Vu Dinh Cuong G3
        /// 24/01/2007
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatddMMyyy(DateTime date)
        {
            //			if(date == null )//|| date.Length != 8)
            //				return "01/01/1900";
            string year = "";
            string month = ""; string day = "";
            try
            {
                year = date.Year.ToString();
                month = date.Month.ToString();
                day = date.Day.ToString();
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
            }
            if (day.Length < 2)
                day = "0" + day;
            if (month.Length < 2)
                month = "0" + month;
            return day + "/" + month + "/" + year;
        }

        /// <summary>
        /// Change string date format "DD/MM/YYYY" to format "YYYYMMDD"
        /// </summary>
        /// <remarks>
        /// Author:		Nguyen Bao Nguyen G3
        /// Modified:	17-Apr-2006
        /// </remarks>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetDateYMD(string date)
        {
            if (date == null || date.Length != 10)
                return "";

            string year = date.Substring(6, 4);
            string month = date.Substring(3, 2);
            string day = date.Substring(0, 2);

            return year + month + day;
        }

        /// <summary>
        /// Replace ' by ''
        /// </summary>
        /// <remarks>
        /// Author		:	Nguyen Minh Duc G3
        /// Created day	:	04/10/2006
        /// </remarks>
        /// <param name="value"></param>
        /// <returns></returns>
        public string EncodeString(string value)
        {
            if (value == null || value.Length == 0)
                return value;
            return value.Replace("'", "''");
        }

        /// <summary>
        /// replace ' by '', * by %, ? by _
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public string EncodeKeyword(string keyword)
        {
            if (keyword == null || keyword.Length == 0)
                return "";
            keyword = keyword.Replace("'", "''");
            keyword = keyword.Replace("?", "_");
            if (keyword[0] == '*')
            {
                keyword = keyword.Replace('*', '%');
            }
            keyword = keyword + "%";
            return keyword;
        }

        /// <summary>
        /// Get column index by column name
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int GetColumnIndex(System.Data.DataTable dt, string colName)
        {
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == colName)
                    return i;
                i++;
            }
            return -1;
        }



        /// <summary>
        /// Get index of the first week
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public int GetFirstWeek(System.Data.DataTable dt)
        {
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.StartsWith("WEEK"))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Get the index of first active week
        /// </summary>
        /// <param name="proWeeks"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>


        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static bool ZipFile(string path)
        {
            try
            {
                //ten file zip
                string strZipFilename = "";
                //password
                string strPass = "";
                //duong dan file xml can zip
                string[] filenames = System.IO.Directory.GetFiles(path, "*.xml");
                clsCryptography genPass = new clsCryptography();

                foreach (string file in filenames)
                {
                    //lay file nam xml , thay extension thang .zip
                    strZipFilename = file.Substring(0, file.Length - 4) + ".zip";
                    //generate password
                    strPass = genPass.GenPWDByFilename(strZipFilename);
                    //zip file
                    clsZip.ZipFiles(file, strZipFilename, strPass);
                    //xoa file xml sau khi da zip xong
                    System.IO.File.Delete(file);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add by KienTNT
        /// </summary>
        /// <param name="path">Full File name without extention</param>
        /// <returns></returns>
        public static bool ZipWithFullFileName(string path)
        {
            try
            {
                //ten file zip
                string strZipFilename = "";
                //password
                string strPass = "";
                //duong dan file xml can zip
                string[] filenames = System.IO.Directory.GetFiles(path, "*.xml");
                clsCryptography genPass = new clsCryptography();

                foreach (string file in filenames)
                {
                    //lay file nam xml , thay extension thang .zip
                    strZipFilename = file.Substring(0, file.Length - 4) + ".zip";
                    //generate password
                    strPass = genPass.GenPWDByFullFilename(strZipFilename);
                    //zip file
                    clsZip.ZipFiles(file, strZipFilename, strPass);
                    //xoa file xml sau khi da zip xong
                    System.IO.File.Delete(file);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region - Prevent Special charater, Numberic -
        private static void ControlOnKeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (!((chr >= 'A' && chr <= 'Z') || (chr >= 'a' && chr <= 'z') || chr == 8 || chr == 13))
                e.Handled = true;
        }


        public static void RegCharater(Control control)
        {
            control.KeyPress += new KeyPressEventHandler(ControlOnKeyPress);
        }

        #endregion

        //Duylnk 02-11-2009 Created:
        public static int GetCurrentULVWeek()
        {
            try
            {
                string strCurrentDate = DateTime.Now.ToString("yyyyMMdd");
                string cmdText = "SELECT CLM_CYCLE FROM FPT_ENV_CALENDAR_MASTER WHERE CLM_TYPE = 'W' AND " + strCurrentDate + " BETWEEN CLM_START_DATE AND CLM_END_DATE";

                //			StreamWriter sw = new StreamWriter("D:\\abc.txt");
                //			sw.Write(cmdText);
                //			sw.Close();
                SqlConnection con = DataAccessObject.clsBaseDAO.Connection;
                SqlCommand cmd = new SqlCommand(cmdText, con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static int GetCurrentULVYear()
        {
            try
            {
                string strCurrentDate = DateTime.Now.ToString("yyyyMMdd");
                string cmdText = "SELECT CLM_YEAR FROM FPT_ENV_CALENDAR_MASTER WHERE CLM_TYPE = 'W' AND " + strCurrentDate + " BETWEEN CLM_START_DATE AND CLM_END_DATE";
                SqlConnection con = DataAccessObject.clsBaseDAO.Connection;
                SqlCommand cmd = new SqlCommand(cmdText, DataAccessObject.clsBaseDAO.Connection);
                if (con.State != ConnectionState.Open)
                    con.Open();
                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        //end Duylnk 02-11-2009 Created:

        /// <summary>
        /// Kiem tra co phai la kieu decimal?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDecimal(string s)
        {
            try
            {
                Convert.ToDecimal(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Kiem tra co phai la kieu ngay ?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDate(string s)
        {
            try
            {
                Convert.ToDateTime(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region - API method -
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(Int32 hWnd, ref IntPtr lpdwProcessId);
        #endregion

        public static void KillExcelApplication(Excel.Application excelApp)
        {
            #region - release excel instance -
            IntPtr xlsApplicationProcessID = new IntPtr(0);
            GetWindowThreadProcessId(excelApp.Hwnd, ref xlsApplicationProcessID);
            System.Diagnostics.Process.GetProcessById(Convert.ToInt32(xlsApplicationProcessID.ToString())).Kill();
            #endregion
        }
        public static void CreateFolder(string strPath)
        {
            try//create folder recursive from root to node
            {
                strPath = strPath.Replace("////", "//");

                string[] strTokens = strPath.Split('\\');
                strPath = strTokens[0];
                for (int i = 1; i < strTokens.Length; i++)
                {
                    strPath += "\\" + strTokens[i];
                    strPath = strPath.Trim();
                    DirectoryInfo folderInfo = new DirectoryInfo(strPath);
                    if (!folderInfo.Exists)
                        Directory.CreateDirectory(strPath);
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public static int GetMin(params int[] values)
        {
            int min = values[0];
            for (int index = 1; index < values.Length; index++)
            {
                if (values[index] < min)
                {
                    min = values[index];
                }
            }

            return min;
        }

        #region DataTable and DataRow

        public static void CopyDataRowValues(DataRow sourceRow, DataRow destinationRow)
        {
            object[] values = sourceRow.ItemArray;

            destinationRow.ItemArray = values;
        }

        //public static void CopyDataRowValues_byColumnNames(DataRow sourceRow, DataRow destinationRow, List<string> columnNames)
        //{
        //    //Get columns index
        //    List<int> colIndexs = new List<int>();
        //    for(int i = 0; i < sourceRow.Table.Columns.Count; i++)
        //    {
        //        DataColumn col = sourceRow.Table.Columns[i];

        //        if (columnNames.Contains(col.ColumnName))
        //        {
        //            colIndexs.Add(i);
        //        }
        //    }

        //    object[] values = sourceRow.ItemArray;

        //    object[] newValues = new object[values.Length - colIndexs.Count];

        //    destinationRow.ItemArray = values;
        //}

        public static System.Data.DataTable CopyDataTableStructure(System.Data.DataTable source)
        {
            System.Data.DataTable newDt = source.Copy();
            newDt.Rows.Clear();

            return newDt;
        }

        /// <summary>
        /// Check whether all value in row is null or empty
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool IsEmptyRow(DataRow row)
        {
            int colCount = row.Table.Columns.Count;

            for (int i = 0; i < colCount; i++)
            {
                object objValue = row[i];

                if (!string.IsNullOrEmpty(objValue.ToString()))
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// add order column to table
        /// Creator: Sonlv
        /// Create Date: 18/04/2010
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strColumnName"></param>
        public static void AddOrderColumn(System.Data.DataTable dt, String strColumnName, bool bAcceptChanges)
        {
            if (dt != null)
            {
                if (dt.Columns[strColumnName] == null)
                {
                    dt.Columns.Add(strColumnName);
                }

                int i = 1;

                foreach (DataRow row in dt.Rows)
                {
                    row[strColumnName] = i;

                    i++;
                }
            }
            if (bAcceptChanges)
            {
                dt.AcceptChanges();
            }
        }

        /// <summary>
        /// Check all colName in list exist in datatable.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="colNames"></param>
        /// <returns></returns>
        public static bool CheckDataTableColumns(System.Data.DataTable dt, List<string> colNames)
        {
            if (dt == null)
            {
                //throw new Exception(clsResources.GetMessage("messages.importStore.noData"));
                return false;
            }

            //if (dt.Columns.Count < colNames.Count)
            //{
            //    //throw new Exception(clsResources.GetMessage("messages.importStore.ColumnError"));
            //    return false;
            //}

            foreach (string colName in colNames)
            {
                if (!dt.Columns.Contains(colName))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Replace NULL value with dafault value
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        public static void ReplaceNullToDefaultValue(System.Data.DataTable dt, string fieldName, string defaultValue)
        {
            foreach (DataRow row in dt.Rows)
            {
                object obj = row[fieldName];
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    row[fieldName] = defaultValue;
                }
            }
        }

        /// <summary>
        /// ReLocate Select All Checkbox
        /// Creator: Sonlv
        /// Create Date: 26/04/2010
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="chkDelete"></param>
        public static void ReLocateSelectAllCheckbox(int iRowHeadersWidth, int iGridLeft, int iGridTop, System.Windows.Forms.CheckBox chkDelete)
        {
            chkDelete.Left = iRowHeadersWidth + iGridLeft + 15;
            chkDelete.Top = iGridTop + 15;

        }

        #endregion DataTable and DataRow

        //DongTC
        public static bool TestCharacter(char ch)
        {
            char[] a = { '+', '-', '.', ',', '/', '?', '"', ':', '\\', ';', '=', '\'', '%', '#', '*' };
            foreach (char c in a)
            {
                if (c.Equals(ch))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        //DongTC
        public bool TestInputCharacterForTextbox(System.Windows.Forms.TextBox txt)
        {
            if (txt.Text.Length > 0)
            {
                foreach (char ch in txt.Text)
                {
                    if (TestCharacter(ch))
                    {
                        txt.Text = txt.Text.Remove(txt.Text.IndexOf(ch));
                        MessageBox.Show("Don't input this character.", "Messages");
                        break;
                        //  return true; 
                    }
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        //DongTC
        public static System.Data.DataTable RemoveDuplicateRows(System.Data.DataTable dt, string colName)
        {
            try
            {
                Hashtable htable = new Hashtable();
                ArrayList duplicateList = new ArrayList();
                foreach (DataRow row in dt.Rows)
                {
                    if (htable.Contains(row[colName]))
                        duplicateList.Add(row);
                    else
                        htable.Add(row[colName], string.Empty);
                }

                foreach (DataRow Row in duplicateList)
                    dt.Rows.Remove(Row);


                return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public List<int> GetMonthNo()
        {
            var lstMonth = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                lstMonth.Add(i);
            }
            return lstMonth;
        }

        public List<int> GetWeekNo(int year)
        {
            var lstWeek = new List<int>();
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 1, 1);
            DateTime date2 = new DateTime(year, 12, 31);

            var totalDayNo = (date2 - date1).TotalDays;
            var totalWeek = totalDayNo / 7;

            for (int i = 1; i <= totalWeek; i++)
            {
                lstWeek.Add(i);
            }

            return lstWeek;
        }

        public List<int> GetYearNo()
        {

            var lstYears = new List<int>();
            lstYears.Add(DateTime.Now.Year);
            lstYears.Add(DateTime.Now.Year - 1);
            return lstYears;
        }

        public DateTime GetStartDateOfMonth(int year, int month)
        {
            return DateTime.Now.Day == 1 ? new DateTime(year, month, 1).AddMonths(-1) : new DateTime(year, month, 1);
        }

        public DateTime GetEndDateOfMonth(int year, int month)
        {

            var lastDatebuild = DateTime.Now;
            if (DateTime.Now.Year == year && DateTime.Now.Month == month)
            {
                lastDatebuild = DateTime.Now.Day == 1 ? new DateTime(year, month, DateTime.Now.Day).AddDays(-1) : new DateTime(year, month, DateTime.Now.Day - 1);

            }
            else if (DateTime.Now.Month > month)
            {
                var firstDayOfNextMonth = new DateTime(year, month, 1).AddMonths(1);
                lastDatebuild = firstDayOfNextMonth.AddDays(-1);
            }

            return lastDatebuild;
        }


        public DateTime GetFirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public DateTime GetEndDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);

            return result.AddDays(3);


        }

        public System.Data.DataTable GetStatusApprove()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");

            tb.Rows.Add(new object[] { -1, ApproveTimesheet.All });
            tb.Rows.Add(new object[] { 0, ApproveTimesheet.ChuaPheDuyet });
            tb.Rows.Add(new object[] { 1, ApproveTimesheet.PheDuyet });

            return tb;
        }
        public System.Data.DataTable GetOnOff()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");

            tb.Rows.Add(new object[] { 0, ApproveTimesheet.All });
            tb.Rows.Add(new object[] { 1, ApproveTimesheet.On });
            tb.Rows.Add(new object[] { 2, ApproveTimesheet.Off });

            return tb;
        }
        public System.Data.DataTable GetShift()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");

            tb.Rows.Add(new object[] { 0, ApproveTimesheet.All });
            tb.Rows.Add(new object[] { 1, ApproveTimesheet.Ca1 });
            tb.Rows.Add(new object[] { 2, ApproveTimesheet.Ca2 });
            tb.Rows.Add(new object[] { 3, ApproveTimesheet.Ca3 });
            return tb;
        }


        public System.Data.DataTable GetLoaiNhom()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");
            tb.Rows.Add(new object[] { 0, Group.CongNhan });
            tb.Rows.Add(new object[] { 1, Group.HanhChinh });
            return tb;
        }

        public System.Data.DataTable GetHoatDong()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");
            tb.Rows.Add(new object[] { 0, "No" });
            tb.Rows.Add(new object[] { 1, "Yes" });
            return tb;
        }


        public System.Data.DataTable GetLyDoNghi()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");
            tb.Rows.Add(new object[] { 0, "Nghĩ Phép" });
            tb.Rows.Add(new object[] { 1, "Nghĩ Thai Sản" });
            tb.Rows.Add(new object[] { 2, "Nghĩ Tai Nạn" });
            return tb;
        }
        public System.Data.DataTable GetLoaiNgayNghi()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");
            tb.Rows.Add(new object[] { 0, "Chủ Nhật" });
            tb.Rows.Add(new object[] { 1, "Ngày Nghỉ Trong Năm" });
            tb.Rows.Add(new object[] { 2, "Thứ 7" });
            return tb;
        }
        public void SendEmail(string mailFrom, string mailTo, string subject, string body)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(mailFrom);
            message.To.Add(new MailAddress(mailTo));
            message.CC.Add(new MailAddress(mailFrom));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("damthoai002@gmail.com", "damthoai");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);


        }

        public System.Data.DataTable GetGioiTinh()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");
            tb.Rows.Add(new object[] { 0, "Nữ" });
            tb.Rows.Add(new object[] { 1, "Nam" });
            return tb;
        }


        public System.Data.DataTable GetLevelApproved()
        {
            var tb = new System.Data.DataTable();
            tb.Columns.Add("value");
            tb.Columns.Add("Name");
            tb.Rows.Add(new object[] { 1, "Cấp 1" });
            tb.Rows.Add(new object[] { 2, "Cấp 2" });
            tb.Rows.Add(new object[] { 3, "Cấp HR" });
            return tb;
        }


        public DateTime GetDateOfDb()
        {
            return DateTime.Parse(DataServices.ExecuteScalar(CommandType.StoredProcedure, "p_GetDateOfDb").ToString());
        }

        public DataTable GetLyDoOt()
        {
            try
            {
                return DataServices.ExecuteDataTable(CommandType.StoredProcedure, "p_GetLyDoOT");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
           
        }

    }
}
