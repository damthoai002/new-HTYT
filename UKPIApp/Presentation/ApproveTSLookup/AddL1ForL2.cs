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
    public partial class AddL1ForL2 : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AddL1ForL2));
        public ClsNhanVien ObjNhanvien { get; set; }
        private readonly clsCommon _common = new clsCommon();
        private readonly NhanVienBo _nvBo = new NhanVienBo();

        public AddL1ForL2()
        {
            InitializeComponent();

        }
        public AddL1ForL2(ClsNhanVien nv)
        {
            this.ObjNhanvien = nv;
            InitializeComponent();
            BindData();
            BindNvL1InL2();
            BindNvL1Available();
        }

        private void BindData()
        {
            txtFNameL4.Text = ObjNhanvien.FNAME;
            txtLNameL4.Text = ObjNhanvien.LNAME;
            txtUserName.Text = ObjNhanvien.Username;
            txtMaNvUnilverL4.Text = ObjNhanvien.MaNVUnilever;

        }


        private void BindNvL1InL2()
        {
            try
            {
                string userNameL2 = ObjNhanvien.Username;
                string maNvUnileverL2 = ObjNhanvien.MaNVUnilever;

                grdNhanVien.DataSource = _nvBo.GetNvl1InL2(userNameL2, maNvUnileverL2);


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

            }

        }

        private void BindNvL1Available()
        {
            try
            {
                string userNameL2 = txtUserNamel3.Text.Trim();
                string maNvUnileverL2 = txtMaNvUnilever.Text.Trim();
                string fName = txtFName.Text.Trim();
                string lName = txtLName.Text.Trim();
                string cardNo = txtCardNo.Text.Trim();

                grdL3Available.DataSource = _nvBo.GetNvl1Available(fName, lName, maNvUnileverL2, userNameL2, cardNo);


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
                                        LevelQuanLy = 1

                                    });


            return lstNhanVien;

        }

        private void btnTimNVProWatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatedData())
                {
                    BindNvL1Available();
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
                var lstNvL2 = GetNhanVienChecked();
                if (lstNvL2.Count > 0)
                {
                    string userId = clsSystemConfig.MaNhanVien.ToString();
                    string userNameL2 = txtUserName.Text;
                    int leveQuanLyL2 = 2;
                    _nvBo.AddNvL3ToL4(userId, lstNvL2, userNameL2, leveQuanLyL2);


                    MessageBox.Show(clsResources.GetMessage("messages.AddL1ForL2.success"),
                       clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BindNvL1Available();
                    BindNvL1InL2();
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

        private void grdNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewColumn = grdNhanVien.Columns["AddL3"];
            if (dataGridViewColumn != null && e.ColumnIndex == dataGridViewColumn.Index)
            {

                ClsNhanVien nv = new ClsNhanVien();
                nv.SysId = Int32.Parse(grdNhanVien.Rows[e.RowIndex].Cells["SysIdL4"].Value.ToString());
                nv.LNAME = grdNhanVien.Rows[e.RowIndex].Cells["LNAMEL4"].Value.ToString();
                nv.FNAME = grdNhanVien.Rows[e.RowIndex].Cells["FNAMEL4"].Value.ToString();
                nv.MaNVUnilever = grdNhanVien.Rows[e.RowIndex].Cells["MaNVUnileverL4"].Value.ToString();
                nv.Username = grdNhanVien.Rows[e.RowIndex].Cells["USERNAMEL4"].Value.ToString();
                nv.CardNo = grdNhanVien.Rows[e.RowIndex].Cells["CardNoL4"].Value.ToString();

                var frmAddL0ForL1 = new AddL0ForL1(nv);
                frmAddL0ForL1.ShowDialog();

            }
        }

    }
}
