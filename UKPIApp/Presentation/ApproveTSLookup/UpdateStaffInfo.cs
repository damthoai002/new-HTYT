using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Windows.Forms;
using UKPI.BusinessObject;
using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.Presentation.ApproveTSLookup
{
    public partial class UpdateStaffInfo : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(UpdateStaffInfo));
        public ClsNhanVien ObjNhanVien { get; set; }
        private readonly clsCommon _common = new clsCommon();
        private readonly NhanVienBo _nhanVienBo = new NhanVienBo();

        public UpdateStaffInfo()
        {
            InitializeComponent();

        }
        public UpdateStaffInfo(ClsNhanVien nhanVien)
        {
            this.ObjNhanVien = nhanVien;
            InitializeComponent();
            BindGioiTinh();
            BindData();

        }

        private void BindGioiTinh()
        {
            cboGioiTinh.ValueMember = "value";
            cboGioiTinh.DisplayMember = "name";
            cboGioiTinh.DataSource = _common.GetGioiTinh();
        }

        private void BindData()
        {
            txtFName.Text = ObjNhanVien.FNAME;
            txtLName.Text = ObjNhanVien.LNAME;
            txtMI.Text = ObjNhanVien.MI;
            txtMaNvUnilever.Text = ObjNhanVien.MaNVUnilever;
            txtEmail.Text = ObjNhanVien.Email;
            cboGioiTinh.SelectedText = ObjNhanVien.GioiTinh == true ? "Nam" : "Nữ";

        }

        private bool ValidatedData()
        {
            ep.Clear();

            string maNvUnilever = txtMaNvUnilever.Text;

            if (maNvUnilever.Length == 0)
            {
                ep.SetError(txtMaNvUnilever, clsResources.GetMessage("errors.required", txtMaNvUnilever.Text));
                txtMaNvUnilever.Focus();
                return false;
            }


            if (!_common.IsLetterAndDigit(maNvUnilever))
            {
                ep.SetError(txtMaNvUnilever, clsResources.GetMessage("errors.string.specialChar", txtMaNvUnilever.Text));
                txtMaNvUnilever.Focus();
                return false;
            }

            if (!CheckMaNvUnilerver(maNvUnilever))
            {
                ep.SetError(txtMaNvUnilever, clsResources.GetMessage("errors.string.ExistData", txtMaNvUnilever.Text, MaNvUnilever.Name));
                txtMaNvUnilever.Focus();
                return false;
            }

            string email = txtEmail.Text;
            if (email.Length == 0)
            {
                ep.SetError(txtEmail, clsResources.GetMessage("errors.required", txtEmail.Text));
                txtEmail.Focus();
                return false;
            }

            if (!_common.IsEmail(email))
            {
                ep.SetError(txtEmail, clsResources.GetMessage("errors.email", txtEmail.Text));
                txtEmail.Focus();
                return false;
            }

            if (!CheckEmail(email))
            {
                ep.SetError(txtEmail, clsResources.GetMessage("errors.string.ExistData", txtEmail.Text, lblEmail.Text));
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        private bool CheckMaNvUnilerver(string maNvUnilever)
        {
            return _nhanVienBo.CheckMaNvUnilerver(maNvUnilever);
        }

        private bool CheckEmail(string email)
        {
            return _nhanVienBo.CheckEmail(email);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatedData())
                {
                    //Update information for NV
                    ObjNhanVien.MaNVUnilever = txtMaNvUnilever.Text.Trim();
                    ObjNhanVien.Email = txtEmail.Text.ToLower();
                    ObjNhanVien.GioiTinh = cboGioiTinh.Text == "Nam" ? true : false;
                    ObjNhanVien.IsDataCC = true;
                    _nhanVienBo.UpdateNhanVienCc(ObjNhanVien);

                    MessageBox.Show(clsResources.GetMessage("messages.UpdateStaffInfo.SaveSucess"),
                       clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


    }
}
