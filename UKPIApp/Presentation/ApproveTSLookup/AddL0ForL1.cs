using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.DynamicData;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using UKPI.BusinessObject;
using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.Presentation.ApproveTSLookup
{
    public partial class AddL0ForL1 : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AddL0ForL1));
        public ClsNhanVien ObjNhanvien { get; set; }
        private readonly clsCommon _common = new clsCommon();
        private readonly NhanVienBo _nvBo = new NhanVienBo();

        public AddL0ForL1()
        {
            InitializeComponent();

        }
        public AddL0ForL1(ClsNhanVien nv)
        {
            this.ObjNhanvien = nv;
            InitializeComponent();
            BindData();
            BindNvL0InL1();
            BindNvL0Available();
        }

        private void BindData()
        {
            txtFNameL4.Text = ObjNhanvien.FNAME;
            txtLNameL4.Text = ObjNhanvien.LNAME;
            txtUserName.Text = ObjNhanvien.Username;
            txtMaNvUnilverL4.Text = ObjNhanvien.MaNVUnilever;

        }


        private void BindNvL0InL1()
        {
            try
            {
                string userNameL1 = ObjNhanvien.Username;
                string maNvUnileverL1 = ObjNhanvien.MaNVUnilever;

                grdNhanVien.DataSource = _nvBo.GetNvl0InL1(userNameL1, maNvUnileverL1);


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }

        }

        private void BindNvL0Available()
        {
            try
            {
                string userNameL1= txtUserNamel3.Text.Trim();
                string maNvUnileverL1 = txtMaNvUnilever.Text.Trim();
                string fName = txtFName.Text.Trim();
                string lName = txtLName.Text.Trim();
                string cardNo = txtCardNo.Text.Trim();

                grdL3Available.DataSource = _nvBo.GetNvl0Available(fName, lName, maNvUnileverL1, userNameL1, cardNo);


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }

        }

        private bool ValidatedData()
        {


            return true;
        }


        private List<ClsNhanVien> GetNhanVienChecked()
        {
            List<ClsNhanVien> lstNhanVien = new List<ClsNhanVien>();

            var table = grdL3Available.DataSource as System.Data.DataTable;
            if (table == null) return lstNhanVien;
            lstNhanVien.AddRange(from DataRow row in table.Rows
                                 where row["Check"].ToString() == "1"
                                 select new ClsNhanVien
                                    {
                                        SysId = Int16.Parse(row["SysId"].ToString()),
                                        Username = row["USERNAME"].ToString(),
                                        LevelQuanLy = 0

                                    });


            return lstNhanVien;

        }

        private void btnTimNVProWatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatedData())
                {
                    BindNvL0Available();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void btnAddL3ForL4_Click(object sender, EventArgs e)
        {
            try
            {
                var lstNvL1 = GetNhanVienChecked();
                if (lstNvL1.Count > 0)
                {
                    string userId = clsSystemConfig.MaNhanVien.ToString();
                    string userNameL1 = txtUserName.Text;
                    int leveQuanLyL1 = 1;
                    _nvBo.AddNvL3ToL4(userId, lstNvL1, userNameL1, leveQuanLyL1);


                    MessageBox.Show(clsResources.GetMessage("messages.AddL0ForL1.success"),
                       clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BindNvL0Available();
                    BindNvL0InL1();
                }
                else
                {
                    MessageBox.Show(clsResources.GetMessage("message.AddL3ForL4.Nodata"),
                        clsResources.GetMessage("warnings.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw ex;
            }
        }

    }
}
