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
using System.Windows.Forms.VisualStyles;
using Excel;
using FPT.Component.ExcelPlus;
using UKPI.BusinessObject;
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class FrmCreateTimesheet : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmCreateTimesheet));

        private clsBaseBO _bo = new clsBaseBO();
        private readonly clsCommon _common = new clsCommon();
        readonly System.Data.DataTable _dt = null;
        CurrencyManager _manager = null;
        private int _checkRowsCount = 0;
        private CreateTimesheetBo _ctsBo = new CreateTimesheetBo();
        DataTable dt = null;
        private DataTable dtNhom = null;

        //param value.
        private String parHanChotDuyetCong = "";
        private String parHanChotDitre = "";
        private String parHanChotVeSom = "";
        private String parChuanTinhCong = "";
        private String parHanMucTinhOt = "";
        public bool Success { get; set; }

        // Declare private fields
        private ChamCongLichLamViecBo _lichLamViecBo = new ChamCongLichLamViecBo();

        //
        readonly DataGridViewColumn[] _originalColumns;
        private DataTable _dtApproveTimesheet;

        #endregion

        #region Constructors

        public FrmCreateTimesheet()
        {

            InitializeComponent();

            clsTitleManager.InitTitle(this);
            GetParam();
            SetDefauldValue();
            BindWeekToCreateTimesheet();
            BindNhom();
            BindNhanVien();
            grdStores.Sorted += grdStores_Sorted;
            InitData();
        }

        private void BindNhom()
        {
            var timesheet = new CreateTimesheetBo();
            dtNhom = timesheet.GetNhomByNhomTruong(clsSystemConfig.UserName);
            cboNhom.ValueMember = clsCommon.CreateTimesheet.NhomId;
            cboNhom.DisplayMember = clsCommon.CreateTimesheet.Nhom;
            cboNhom.DataSource = dtNhom;
            cboNhom.DropDownStyle = ComboBoxStyle.DropDownList;


            for (var i = 0; i < dtNhom.Rows.Count; i++)
            {
                if (dtNhom.Rows[i][clsCommon.CreateTimesheet.NhomId].ToString() == cboNhom.SelectedValue.ToString())
                {
                    txtOutsource.Text = (bool)dtNhom.Rows[i][clsCommon.CreateTimesheet.Outsource] == true ? "Yes" : "No";
                }
            }


        }

        private void InitData()
        {

        }

        private void BindNhanVien()
        {
            var nhom = cboNhom.Text != "" ? Int32.Parse(cboNhom.SelectedValue.ToString()) : 0; //lay nhom Id trong cbo nhom
            var truongNhom = clsSystemConfig.UserName;
            var chamcong = new ChamCongLichLamViecBo();
            grdStores.DataSource = chamcong.GetNhanVien(nhom, truongNhom);
            grdStores.ReadOnly = true;
        }

        void grdStores_Sorted(object sender, EventArgs e)
        {

        }

        private void GetParam()
        {
            parHanChotDuyetCong = ParameterBo.GetParamByName(clsCommon.Parameter.HanChotDuyetcong).ParamValue;
            parHanChotDitre = ParameterBo.GetParamByName(clsCommon.Parameter.HanChotDitre).ParamValue;
            parHanChotVeSom = ParameterBo.GetParamByName(clsCommon.Parameter.HanChotVesom).ParamValue;
            parChuanTinhCong = ParameterBo.GetParamByName(clsCommon.Parameter.ChuanTinhCong).ParamValue;
            parHanMucTinhOt = ParameterBo.GetParamByName(clsCommon.Parameter.HanMucTinhOt).ParamValue;
        }

        private void SetDefauldValue()
        {
            var tsBo = new CreateTimesheetBo();
            ClsCreateTimesheet ts;
            var tbts = tsBo.GetNhomTruong(clsSystemConfig.UserName);
            if (tbts.Rows.Count > 0)
            {
                ts = new ClsCreateTimesheet(tbts);
            }
            else
            {
                ts = new ClsCreateTimesheet();
            }



            txtL1PheDuyet.Text = ts.L1XacNhanTen;
            txtL1PheDuyetMa.Text = ts.L1XacNhanId;
            txtL2PheDuyet.Text = ts.L2XacNhanTen;
            txtL2PheDuyetMa.Text = ts.L2XacNhanId;
            txtL3PheDuyet.Text = ts.L3XacNhanTen;
            txtL3PheDuyetMa.Text = ts.L3XacNhanId;

            txtMaTruongNhom.Text = clsSystemConfig.UserName;
            txtTruongNhom.Text = ts.TenTruongNhom;


            txtCurrentWeek.Text = DateTime.Now.Month.ToString();

        }

        public void BindWeekToCreateTimesheet()
        {
            cboLichLamViecTuan.ValueMember = "Year";
            cboLichLamViecTuan.DisplayMember = "Month";
            cboLichLamViecTuan.DataSource = _ctsBo.GetMonthOfSystem();
        }





        private void InitControls()
        {
            try
            {
                this.grdStores.AutoGenerateColumns = false;
                // Init controls' status
                //txtDistributors.Text = DISTRIBUTORS_DEFAUT;

                // Read file config
                this.OnGridDataSourceChanged();
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }





        private void OnGridDataSourceChanged()
        {
            if (grdStores.RowCount == 0)
            {


            }
            else
            {
                // Check all stores and enable Delete button


            }
        }


        public string ValidatedDateToSave()
        {
            var strError = "";

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return strError;


            var lstError = (from DataRow row in table.Rows
                            where (clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Vao_L1].ToString()) == "" ||
                            clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Ra_L1].ToString()) == "") &&
                             (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false
                            select String.Format(clsResources.GetMessage("message.FrmApproveTimesheet.LineInvalidTime"), row[ChamCongLichLamViecDAO.Vao_L1].ToString(), row[ChamCongLichLamViecDAO.Ra_L1].ToString()) + "\n"
                          );

            return lstError.Aggregate(strError, (current, strEr) => current + current);
        }

        public List<ClsLichLamViec> GetDataToSave()
        {
            var lstLichLamViec = new List<ClsLichLamViec>();

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;

            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Vao_L1].ToString()) != "" &&
                                    clsCommon.CheckTimeValue(row[ChamCongLichLamViecDAO.Ra_L1].ToString()) != "" &&
                                    (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == true
                                     && (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false
                                    select new ClsLichLamViec
                                    {
                                        SysId = long.Parse(row[ChamCongLichLamViecDAO.SysID].ToString()),
                                        Vao_L1 = row[ChamCongLichLamViecDAO.Vao_L1].ToString(),
                                        Ra_L1 = row[ChamCongLichLamViecDAO.Ra_L1].ToString(),
                                        OTL1 = (bool)row[ChamCongLichLamViecDAO.CoDangKyOT] == true ? clsCommon.CalOtTime(row[ChamCongLichLamViecDAO.Vao_L1].ToString(), row[ChamCongLichLamViecDAO.Ra_L1].ToString(), parChuanTinhCong).ToString(CultureInfo.InvariantCulture) : "",
                                        Note = row[ChamCongLichLamViecDAO.Note].ToString(),
                                        LastUpDate = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                                        lastUpdateId = clsSystemConfig.UserName
                                    });

            return lstLichLamViec;
        }


        #endregion

        #region Handle events








        private void frmEditStore_Load(object sender, EventArgs e)
        {
            InitControls();
        }




        /// <summary>
        /// check exchange grid when close form
        /// Creator: Sonlv
        /// Create Date: 23/03/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var table = grdStores.DataSource as System.Data.DataTable;
            //if (table == null)
            //{
            //    return;
            //}
            //if (table.GetChanges() == null) return;
            //var dialogResult = MessageBox.Show(clsResources.GetMessage("message.Others.DataHasChanged") + Environment.NewLine
            //                                            + clsResources.GetMessage("message.Others.WantToSaveTheChanges"), clsResources.GetMessage("frmEditStore.Messages.Edit_Store"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //switch (dialogResult)
            //{
            //    case DialogResult.Cancel:
            //        e.Cancel = true;
            //        break;
            //    case DialogResult.No:
            //        this.Hide();
            //        break;
            //    default:
            //        if (!this.OnSaveClick())
            //        {
            //            e.Cancel = true;
            //        }
            //        break;
            //}
        }





        #endregion


        public List<ClsLichLamViec> GetDataToApproveSave()
        {
            var lstLichLamViec = new List<ClsLichLamViec>();

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return lstLichLamViec;
            lstLichLamViec.AddRange(from DataRow row in table.Rows
                                    where (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == true
                                    select new ClsLichLamViec
                                    {
                                        SysId = long.Parse(row[ChamCongLichLamViecDAO.SysID].ToString()),
                                        L0XacNhan = true,
                                        L0XacNhan_Date = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                                        L0XacNhan_Id = clsSystemConfig.MaNhanVien.ToString(),
                                        L0XacNhan_TenNgan = clsSystemConfig.FullName,
                                        LastUpDate = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                                        lastUpdateId = clsSystemConfig.MaNhanVien.ToString(),
                                        Note = row[ChamCongLichLamViecDAO.Note].ToString()
                                    });

            return lstLichLamViec;
        }


        public string GetItemToGetTimesheet()
        {
            const string strSysId = "";

            var table = grdStores.DataSource as System.Data.DataTable;
            if (table == null) return strSysId;

            return table.Rows.Cast<DataRow>().Where(row => (bool)row[ChamCongLichLamViecDAO.L1XacNhan] == false && (bool)row[ChamCongLichLamViecDAO.DaLayDuLieuChamCong] == false).Aggregate(strSysId, (current, row) => current + "," + row[ChamCongLichLamViecDAO.SysID].ToString());
        }


        /// <summary>
        /// Check whether data of user is valid
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:			Nguyen Minh Duc. G3.
        /// Created date:	14/05/2006
        /// </remarks>
        public bool ValidateData()
        {
           
            exp.Clear();
            exp.SetError(txtTruongNhom, "");

            var strTruongNhom = txtTruongNhom.Text;
            if (strTruongNhom.Length == 0)
            {
                //row.SetColumnError("TruongNhom", clsResources.GetMessage("errors.required", lblTruongNhom.Text));
                exp.SetError(txtTruongNhom, clsResources.GetMessage("errors.required", lblTruongNhom.Text));
                txtTruongNhom.Focus();
                return false;
            }


            var strMaTruongNhom = txtMaTruongNhom.Text;
            if (strMaTruongNhom.Length == 0)
            {
                // row.SetColumnError(clsCommon.CreateTimesheet.MaTruongNhom, clsResources.GetMessage("errors.required", lblMaTruongNhom.Text));
                exp.SetError(txtMaTruongNhom, clsResources.GetMessage("errors.required", lblMaTruongNhom.Text));
                return false;
            }
            var strOutsource = txtOutsource.Text;
            if (strOutsource.Length == 0)
            {
                // row.SetColumnError(clsCommon.CreateTimesheet.Outsource, clsResources.GetMessage("errors.required", lblOutsource.Text));

                exp.SetError(txtOutsource, clsResources.GetMessage("errors.required", lblOutsource.Text));
                txtOutsource.Focus();
                return false;
            }




            var strL1XacNhan = txtL1PheDuyet.Text;
            if (strL1XacNhan.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L1XacNhan, clsResources.GetMessage("errors.required", lblL1Ten.Text));
                exp.SetError(txtL1PheDuyet, clsResources.GetMessage("errors.required", lblL1Ten.Text));
                return false;
            }
            var strL1XacNhanMaSo = txtL1PheDuyetMa.Text;
            if (strL1XacNhanMaSo.Length == 0)
            {
                // row.SetColumnError(clsCommon.CreateTimesheet.L1XacNhanMa, clsResources.GetMessage("errors.required", lblL1MaSo.Text));
                exp.SetError(txtL1PheDuyetMa, clsResources.GetMessage("errors.required", lblL1MaSo.Text));
                return false;
            }


            var strL2XacNhan = txtL2PheDuyet.Text;
            if (strL2XacNhan.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L2XacNhan, clsResources.GetMessage("errors.required", lblL2Ten.Text));
                exp.SetError(txtL2PheDuyet, clsResources.GetMessage("errors.required", lblL2Ten.Text));
                return false;
            }
            var strL2XacNhanMaSo = txtL2PheDuyetMa.Text;
            if (strL2XacNhanMaSo.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L2XacNhanMa, clsResources.GetMessage("errors.required", lblL2MaSo.Text));
                exp.SetError(txtL2PheDuyetMa, clsResources.GetMessage("errors.required", lblL2MaSo.Text));
                return false;
            }

            var strL3XacNhan = txtL3PheDuyet.Text;
            if (strL3XacNhan.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L3XacNhan, clsResources.GetMessage("errors.required", lblL3Ten.Text));
                exp.SetError(txtL3PheDuyet, clsResources.GetMessage("errors.required", lblL3Ten.Text));
                return false;
            }
            var strL3XacNhanMaSo = txtL3PheDuyetMa.Text;
            if (strL3XacNhanMaSo.Length == 0)
            {
                //row.SetColumnError(clsCommon.CreateTimesheet.L3XacNhanMa, clsResources.GetMessage("errors.required", lblL3MaSo.Text));
                exp.SetError(txtL3PheDuyetMa, clsResources.GetMessage("errors.required", lblL3MaSo.Text));
                return false;
            }


            return true;
        }

        private void btnTaoLich_Click(object sender, EventArgs e)
        {
            
            if (!ValidateData()) return;
            try
            {
                var firstDateOfMonth = new DateTime(Int16.Parse(cboLichLamViecTuan.SelectedValue.ToString()), Int16.Parse(cboLichLamViecTuan.Text), 1);

                var endDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

                var firstWeek = clsCommon.GetWeek(firstDateOfMonth);
                var lastWeek = clsCommon.GetWeek(endDateOfMonth);

                var lstTs = new List<ClsCreateTimesheet>();

                for (int i = firstWeek; i <= lastWeek; i++)
                {
                    lstTs.Add(new ClsCreateTimesheet
                    {
                        NhomId = cboNhom.Text != "" ? long.Parse(cboNhom.SelectedValue.ToString()) : 0,
                        TenNhom = cboNhom.Text,
                        TruongNhomId = (txtMaTruongNhom.Text),
                        TenTruongNhom = txtTruongNhom.Text,
                        L1XacNhanId = (txtL1PheDuyetMa.Text),
                        L1XacNhanTen = txtL1PheDuyet.Text,
                        L2XacNhanId = (txtL2PheDuyetMa.Text),
                        L2XacNhanTen = txtL2PheDuyet.Text,
                        L3XacNhanId = (txtL3PheDuyetMa.Text),
                        L3XacNhanTen = txtL3PheDuyet.Text,
                        TuanLamViec = i.ToString(),
                        IsOutsource = txtOutsource.Text == "Yes" ? "1" : "0"
                    });
                }

                

                if (!_ctsBo.CheckExistCalamViecNhom(lstTs[0].NhomId, firstDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDb), endDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDb)))
                {
                    MessageBox.Show(String.Format(clsResources.GetMessage("messages.FrmCreateTimesheet.NotExistCaLamViec"), lstTs[0].TenNhom, lstTs[0].TuanLamViec), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_ctsBo.CheckExistTimesheet(lstTs[0].NhomId, lstTs[0].TruongNhomId, firstDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDb), endDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDb) , lstTs[0].IsOutsource))//tao lich lam viec cho nhom thanh cong
                {
                    var dialogResult = MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheet.WantToContinue"),
                        clsResources.GetMessage("messages.general"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (dialogResult)
                    {
                        case DialogResult.Cancel:
                            // e.Cancel = true;
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            //delete timesheet and overwrite new ts
                            for (int i = 0; i < lstTs.Count; i++)
                            {
                                _ctsBo.DeleteTimesheet(lstTs[i]);
                                _ctsBo.CreateTimesheet(lstTs[i]);
                            }
                            MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheet.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }


                }
                else //tao lich that bai
                {
                    for (int i = 0; i < lstTs.Count; i++)
                    {
                        _ctsBo.DeleteTimesheet(lstTs[i]);
                        _ctsBo.CreateTimesheet(lstTs[i]);
                    }
                    MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheet.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(clsResources.GetMessage("messages.FrmCreateTimesheet.fail") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < dtNhom.Rows.Count; i++)
            {
                if (dtNhom.Rows[i][clsCommon.CreateTimesheet.NhomId].ToString() == cboNhom.SelectedValue.ToString())
                {
                    txtOutsource.Text = (bool)dtNhom.Rows[i][clsCommon.CreateTimesheet.Outsource] == true ? "Yes" : "No";
                }
            }

            BindNhanVien();


        }

        private void btnXemLich_Click(object sender, EventArgs e)
        {
            var firstDateOfMonth = new DateTime(Int16.Parse(cboLichLamViecTuan.SelectedValue.ToString()), Int16.Parse(cboLichLamViecTuan.Text), 1);

            var endDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            var frmViewTimesheet = new ReviewTimesheets(new ClsCreateTimesheet
            {
                NhomId = cboNhom.Text != "" ? long.Parse(cboNhom.SelectedValue.ToString()) : 0,
                TenNhom = cboNhom.Text,
                TruongNhomId = (txtMaTruongNhom.Text),
                TenTruongNhom = txtTruongNhom.Text,

                L1XacNhanId = (txtL1PheDuyetMa.Text),
                L1XacNhanTen = txtL1PheDuyet.Text,
                L2XacNhanId = (txtL2PheDuyetMa.Text),
                L2XacNhanTen = txtL2PheDuyet.Text,
                L3XacNhanId = (txtL3PheDuyetMa.Text),
                L3XacNhanTen = txtL3PheDuyet.Text,

                TuNgay = firstDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                DenNgay = endDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDb),
                IsOutsource = txtOutsource.Text == "Yes" ? "1" : "0"
            });
            frmViewTimesheet.ShowDialog();
        }

        private void SetFromToDate()
        {
            var firstDateOfMonth = new DateTime(Int16.Parse(cboLichLamViecTuan.SelectedValue.ToString()), Int16.Parse(cboLichLamViecTuan.Text), 1);

            var endDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            lblFromDay.Text = firstDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDisplay);
            lblToDate.Text = endDateOfMonth.ToString(clsCommon.ApproveTimesheet.DateFormatDisplay);
        }

        private void cboLichLamViecTuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFromToDate();
        }
    }
}