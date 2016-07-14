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
using UKPI.BusinessObject.Authenticate;
using UKPI.Core;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;
using System.Net.Mail;
using System.Net;

namespace UKPI.Presentation
{
    public partial class FrmCreateLeavePlan : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmCreateLeavePlan));

        private clsBaseBO _bo = new clsBaseBO();
        private ClsNgayNghiHopLeBo _ngayNghiHopLeBo = new ClsNgayNghiHopLeBo();

        private readonly clsCommon _common = new clsCommon();


        #endregion

        #region Constructors

        public FrmCreateLeavePlan()
        {

            InitializeComponent();

            clsTitleManager.InitTitle(this);

            BindTimes();
            BindTruongNhom();
            BindNhanVien();
            BindLyDo();
            BindNghiPhep(clsSystemConfig.UserName, "");

        }

        private void BindNghiPhep(string maTruongNhom, string tenNhanVien)
        {
            DataTable tb = _ngayNghiHopLeBo.GetNgaynghiPhep(maTruongNhom, tenNhanVien);

            grdNghiPhep.DataSource = tb;
        }

        private void BindLyDo()
        {
            //cboLyDo1.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLyDo1.ValueMember = "value";
            cboLyDo1.DisplayMember = "Name";
            cboLyDo1.DataSource = _common.GetLyDoNghi();

            cboLyDo2.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLyDo2.ValueMember = "value";
            cboLyDo2.DisplayMember = "Name";
            cboLyDo2.DataSource = _common.GetLyDoNghi();
        }

        private void BindNhanVien()
        {
            cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNhanVien.ValueMember = "MaNhanVien";
            cboNhanVien.DisplayMember = "Ten";
            cboNhanVien.DataSource = _ngayNghiHopLeBo.GetNhanVienNghiPhep(clsSystemConfig.UserName);


        }

        private void BindTruongNhom()
        {
            txtTruongNhom.Text = clsSystemConfig.FullName + " - " + clsSystemConfig.UserName;
        }


        private void BindTimes()
        {
            dtpFromDay.Format = DateTimePickerFormat.Custom;
            dtpFromDay.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;

            dtpToDay.Format = DateTimePickerFormat.Custom;
            dtpToDay.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;


            dtpFromHour.Visible = false;
            dtpFromHour.Format = DateTimePickerFormat.Custom;
            dtpFromHour.CustomFormat = clsCommon.ApproveTimesheet.TimeFormat;
            dtpFromHour.ShowUpDown = true;
            dtpFromHour.Text = "00:00";

            dtpToHour.Visible = false;
            dtpToHour.Format = DateTimePickerFormat.Custom;
            dtpToHour.CustomFormat = clsCommon.ApproveTimesheet.TimeFormat;
            dtpToHour.ShowUpDown = true;
            dtpToHour.Text = "23:59";


            chkUseDateSearch.Checked = false;
            dtpNgayNghi.Format = DateTimePickerFormat.Custom;
            dtpNgayNghi.CustomFormat = clsCommon.ApproveTimesheet.DateFormatDisplay;

            if (chkUseDateSearch.Checked)
            {
                dtpNgayNghi.Enabled = true;

            }
            else
            {
                dtpNgayNghi.Enabled = false;
            }

        }


        #endregion

        #region Handle events

        public bool CheckValidDate()
        {

            return true;
        }

        #endregion
        public string GetItemToUnActive()
        {
            const string strSysId = "";

            var table = grdNghiPhep.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => row["Check"].ToString() == "1").Aggregate(strSysId, (current, row) => current + "," + row[clsCommon.Group.MaNhom].ToString());
        }

        public List<ClsTeam> GetTeamToUnActive()
        {
            var lstLichLamViec = new List<ClsTeam>();

            var table = grdNghiPhep.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;

            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where row["Check"].ToString() == "1"
                                    select new ClsTeam
                                    {
                                        TruongNhomId = row[clsCommon.Group.MaTruongNhom].ToString(),
                                        NhomId = row[clsCommon.Group.MaNhom].ToString()
                                    });

            return lstLichLamViec;
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidatedDate())
                {
                    string maTruongNhom = clsSystemConfig.UserName;
                    string tentruongNhom = clsSystemConfig.FullName;
                    string maNhanVien = cboNhanVien.SelectedValue.ToString();
                    string tenNhanVien = cboNhanVien.Text;
                    string dienGiai = txtDienGiai.Text;
                    string lydo1 = cboLyDo1.SelectedValue.ToString();
                    string lydo1ChiTiet = cboLyDo1.Text;
                    string lydo2 = cboLyDo2.SelectedValue.ToString();
                    string lydo2ChiTiet = cboLyDo2.Text;
                    string ngayDuyet = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb);

                    DateTime tuNgay = dtpFromDay.Value;
                    DateTime denNgay = dtpToDay.Value;
                    string maGiaoDich = clsCommon.NgayNghi.GetMaGiaoDich(maNhanVien, clsCommon.NgayNghi.NghiPhep);
                    _ngayNghiHopLeBo.TaoNgayNghiPhep(maTruongNhom, maGiaoDich, tuNgay, denNgay, lydo1, lydo1ChiTiet,
                        lydo2, lydo2ChiTiet, dienGiai, Int32.Parse(maNhanVien), tenNhanVien, tentruongNhom, ngayDuyet,
                        true);
                    //check valid data in CC cham cong and update is off = true. return status true or false to build email.

                    bool isCapNhatChamCong = CapNhanChamCong(maNhanVien, maTruongNhom, maGiaoDich);
                    // build email and send for nhanvien va truong nhom.

                    // SendMailForTruongNhomAndNhanVien(isCapNhatChamCong, tenNhanVien, tentruongNhom, maNhanVien, maTruongNhom,dtpFromDay.Text,dtpToDay.Text);

                    MessageBox.Show(clsResources.GetMessage("messages.FrmCreateLeavePlan.save.success"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BindNghiPhep(maTruongNhom, "");
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateLeavePlan.save.UnSuccess"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log.Error(ex.Message, ex);
            }

        }

        private void SendMailForTruongNhomAndNhanVien(bool isCapNhatChamCong, string tenNhanVien, string tentruongNhom, string maNhanVien, string maTruongNhom, string tuNgay, string denNgay)
        {
            DataTable dt = _ngayNghiHopLeBo.GetEmail(maNhanVien, maTruongNhom);
            string emailNhanVien = dt.Rows[0][0].ToString();
            string emailTruongNhom = dt.Rows[0][1].ToString();
            string body = "";
            if (isCapNhatChamCong)
            {
                body = BuildBody(true, tenNhanVien, tentruongNhom, tuNgay, denNgay, emailTruongNhom);
                SendEmail(emailTruongNhom, emailNhanVien, clsResources.GetMessage("messages.FrmCreateLeavePlan.EmailSubject"), body);

            }
            else
            {
                body = BuildBody(false, tenNhanVien, tentruongNhom, tuNgay, denNgay, emailTruongNhom);
                SendEmail(emailTruongNhom, emailNhanVien, clsResources.GetMessage("messages.FrmCreateLeavePlan.EmailSubject"), body);
            }
        }

        private string BuildBody(bool isCapNhatChamCong, string tenNhanVien, string tenTruongNhom, string tuNgay, string denNgay, string emailTruongNhom)
        {
            string body = "";
            if (isCapNhatChamCong)
            {
                body = "<p>Chào " + tenNhanVien + " </p></br></br></br> ";
                body += "<p>Email Thông báo đăng ký ngày nghĩ cho bạn: </p></br>";
                body += "<p>Ngày " + tuNgay + " đến ngày " + denNgay + ". Được trưởng nhóm " + tenTruongNhom + " đăng ký thành công.</p></br></br>";
                body += "<p>Ngày " + tuNgay + " đến ngày " + denNgay + ". Được cập nhật trong bảng chấm công của bạn.</p>";
                body += "<p>Thân ái</p>";
            }
            else
            {
                body = "<p>Chào " + tenNhanVien + " </p></br></br> ";
                body += "<p>Email Thông báo đăng ký ngày nghĩ cho bạn: </p></br>";
                body += "<p>Ngày " + tuNgay + " đến ngày " + denNgay + ". Được trưởng nhóm " + tenTruongNhom + " đăng ký thành công.</p></br></br>";
                body += "<p>Ngày " + tuNgay + " đến ngày " + denNgay + "đã được duyệt trong bảng chấm công của bạn.</p></br></br>";
                body += "<p>Liên hệ với trưởng nhóm " + tenTruongNhom + " email: " + emailTruongNhom + " để được hỗ trơ.</p>";
                body += "<p>Thân ái</p>";
            }

            return body;
        }

        private bool CapNhanChamCong(string maNv, string maTruongNhom, string maGiaoDich)
        {
            bool isCheck = _ngayNghiHopLeBo.CheckApprovedForNgayNghi(maNv, maTruongNhom, maGiaoDich);
            if (isCheck)
            {
                _ngayNghiHopLeBo.UpdateIsOffForNgayNghiNhanVien(maNv, maTruongNhom, maGiaoDich);
                return true;
            }
            else
            {
                return false;
            }


        }




        public bool CheckValidDateDataToSearch()
        {
            return true;
        }

        private void btnCreateShiftForTeam_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateLeavePlan.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateLeavePlan.unSuccess"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        private bool ValidatedDate()
        {
            erp.Clear();

            DateTime fromdate = dtpFromDay.Value;
            DateTime toDate = dtpToDay.Value;
            string maNv = cboNhanVien.SelectedValue.ToString();
            string maTruongNhom = clsSystemConfig.UserName;


            if (fromdate.Date > toDate.Date)
            {
                erp.SetError(dtpToDay, clsResources.GetMessage("messages.FrmCreateLeavePlan.DateValid", dtpFromDay.Text, dtpToDay.Text));
                dtpToDay.Focus();
                return false;
            }

            if (_ngayNghiHopLeBo.CheckExistDateOff(fromdate.ToString(clsCommon.ApproveTimesheet.DateFormatDb), toDate.ToString(clsCommon.ApproveTimesheet.DateFormatDb), maNv, maTruongNhom))
            {
                erp.SetError(dtpFromDay, clsResources.GetMessage("messages.FrmCreateLeavePlan.DateExists", dtpFromDay.Text, dtpToDay.Text));
                dtpFromDay.Focus();
                return false;
            }

            return true;
        }

        private void dtpFromDay_ValueChanged(object sender, EventArgs e)
        {
            dtpToDay.Value = dtpFromDay.Value;
        }



        public void SendEmail(string mailFrom, string mailTo, string subject, string body)
        {
            try
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
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ValidatedTenNhanVien())
            {
                var ngayNghi = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                if (chkUseDateSearch.Checked)
                {
                    ngayNghi = dtpNgayNghi.Value.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                    grdNghiPhep.DataSource = _ngayNghiHopLeBo.GetNgaynghiPhep(clsSystemConfig.UserName, txtTenNhanVien.Text);
                }
                else
                {
                    grdNghiPhep.DataSource = _ngayNghiHopLeBo.GetNgaynghiPhep(clsSystemConfig.UserName, txtTenNhanVien.Text);
                }
                
            }
        }

        private bool ValidatedTenNhanVien()
        {
            erp.Clear();

            string tenNv = txtTenNhanVien.Text;

            return true;
        }

        private void chkUseDateSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDateSearch.Checked)
            {
                dtpNgayNghi.Enabled = true;

            }
            else
            {
                dtpNgayNghi.Enabled = false;
            }
        }

    }
}