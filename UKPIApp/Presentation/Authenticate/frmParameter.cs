using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using UKPI;
using UKPI.BusinessObject;
using System.IO;
using UKPI.Utils;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for FPT_ENV_Parameter.
	/// </summary>
	public class frmParameter : System.Windows.Forms.Form
	{
		#region ".NET Code"
		
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGrid grdParam;
		private System.Windows.Forms.LinkLabel lnkBrowse;
		private System.Windows.Forms.TextBox txtParamValue;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtParamName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblParameterValue;
		private System.Windows.Forms.Label lblParameterName;
        private System.Windows.Forms.GroupBox groupBox2;
		private DotNetSkin.SkinControls.SkinButton btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblParameterGroup;
		private DotNetSkin.SkinControls.SkinButton btnView;
        private Label lblParameter;
        private TextBox txtParameter;
        private ComboBox cboParamGroup;
        private DotNetSkin.SkinControls.SkinButton btnSend;
		private DotNetSkin.SkinControls.SkinButton btnUpdate;	

		public frmParameter()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitializeCombo();
            clsTitleManager.InitTitle(this);

			FixPosition();
		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// 
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region "Variable"
		
		private DataTable m_dt = null;
		private CurrencyManager m_manager = null;
		private clsParameterBO bo = new clsParameterBO();
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(frmParameter));

		public DataTable DataSource
		{
			get{return m_dt;}
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParameter));
            this.grdParam = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lnkBrowse = new System.Windows.Forms.LinkLabel();
            this.btnUpdate = new DotNetSkin.SkinControls.SkinButton();
            this.txtParamValue = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtParamName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblParameterValue = new System.Windows.Forms.Label();
            this.lblParameterName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSend = new DotNetSkin.SkinControls.SkinButton();
            this.btnClose = new DotNetSkin.SkinControls.SkinButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboParamGroup = new System.Windows.Forms.ComboBox();
            this.lblParameter = new System.Windows.Forms.Label();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.btnView = new DotNetSkin.SkinControls.SkinButton();
            this.lblParameterGroup = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdParam)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdParam
            // 
            this.grdParam.AllowSorting = false;
            this.grdParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdParam.CaptionVisible = false;
            this.grdParam.DataMember = "";
            this.grdParam.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.grdParam.Location = new System.Drawing.Point(8, 56);
            this.grdParam.Name = "grdParam";
            this.grdParam.ReadOnly = true;
            this.grdParam.Size = new System.Drawing.Size(839, 330);
            this.grdParam.TabIndex = 3;
            this.grdParam.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.grdParam.CurrentCellChanged += new System.EventHandler(this.grdParam_CurrentCellChanged);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AllowSorting = false;
            this.dataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.AliceBlue;
            this.dataGridTableStyle1.DataGrid = this.grdParam;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Tham số";
            this.dataGridTextBoxColumn1.MappingName = "Param_Name";
            this.dataGridTextBoxColumn1.Width = 190;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Giá trị tham số";
            this.dataGridTextBoxColumn2.MappingName = "Param_Value";
            this.dataGridTextBoxColumn2.Width = 135;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Ghi Chú";
            this.dataGridTextBoxColumn3.MappingName = "Description";
            this.dataGridTextBoxColumn3.Width = 400;
            // 
            // lnkBrowse
            // 
            this.lnkBrowse.Location = new System.Drawing.Point(389, 69);
            this.lnkBrowse.Name = "lnkBrowse";
            this.lnkBrowse.Size = new System.Drawing.Size(24, 16);
            this.lnkBrowse.TabIndex = 7;
            this.lnkBrowse.TabStop = true;
            this.lnkBrowse.Text = "[...]";
            this.lnkBrowse.Visible = false;
            this.lnkBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBrowse_LinkClicked);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(643, 62);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(85, 23);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "      Lưu";
            this.btnUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // txtParamValue
            // 
            this.txtParamValue.Location = new System.Drawing.Point(128, 67);
            this.txtParamValue.MaxLength = 250;
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.Size = new System.Drawing.Size(256, 20);
            this.txtParamValue.TabIndex = 6;
            this.txtParamValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParamValue_KeyPress);
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(128, 41);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(256, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // txtParamName
            // 
            this.txtParamName.Enabled = false;
            this.txtParamName.Location = new System.Drawing.Point(128, 13);
            this.txtParamName.Name = "txtParamName";
            this.txtParamName.Size = new System.Drawing.Size(256, 20);
            this.txtParamName.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(24, 44);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(88, 23);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Description";
            // 
            // lblParameterValue
            // 
            this.lblParameterValue.Location = new System.Drawing.Point(24, 71);
            this.lblParameterValue.Name = "lblParameterValue";
            this.lblParameterValue.Size = new System.Drawing.Size(100, 23);
            this.lblParameterValue.TabIndex = 5;
            this.lblParameterValue.Text = "ParameterValue";
            // 
            // lblParameterName
            // 
            this.lblParameterName.Location = new System.Drawing.Point(24, 16);
            this.lblParameterName.Name = "lblParameterName";
            this.lblParameterName.Size = new System.Drawing.Size(100, 23);
            this.lblParameterName.TabIndex = 4;
            this.lblParameterName.Text = "Parameter Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.txtParamName);
            this.groupBox2.Controls.Add(this.lblParameterName);
            this.groupBox2.Controls.Add(this.txtParamValue);
            this.groupBox2.Controls.Add(this.lnkBrowse);
            this.groupBox2.Controls.Add(this.lblDescription);
            this.groupBox2.Controls.Add(this.lblParameterValue);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Location = new System.Drawing.Point(8, 390);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(839, 97);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSend.Location = new System.Drawing.Point(554, 62);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(83, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Send";
            this.btnSend.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(734, 62);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "    Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cboParamGroup);
            this.groupBox3.Controls.Add(this.lblParameter);
            this.groupBox3.Controls.Add(this.txtParameter);
            this.groupBox3.Controls.Add(this.btnView);
            this.groupBox3.Controls.Add(this.lblParameterGroup);
            this.groupBox3.Location = new System.Drawing.Point(8, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(841, 42);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // cboParamGroup
            // 
            this.cboParamGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParamGroup.FormattingEnabled = true;
            this.cboParamGroup.Location = new System.Drawing.Point(509, 12);
            this.cboParamGroup.Name = "cboParamGroup";
            this.cboParamGroup.Size = new System.Drawing.Size(310, 21);
            this.cboParamGroup.TabIndex = 16;
            this.cboParamGroup.Visible = false;
            this.cboParamGroup.SelectedIndexChanged += new System.EventHandler(this.cboParamGroup_SelectedIndexChanged);
            // 
            // lblParameter
            // 
            this.lblParameter.Location = new System.Drawing.Point(11, 16);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(57, 17);
            this.lblParameter.TabIndex = 14;
            this.lblParameter.Text = "Param";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtParameter
            // 
            this.txtParameter.Location = new System.Drawing.Point(69, 13);
            this.txtParameter.MaxLength = 20;
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(113, 20);
            this.txtParameter.TabIndex = 15;
            // 
            // btnView
            // 
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(188, 11);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(85, 23);
            this.btnView.TabIndex = 6;
            this.btnView.Text = "     Search";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblParameterGroup
            // 
            this.lblParameterGroup.Location = new System.Drawing.Point(404, 11);
            this.lblParameterGroup.Name = "lblParameterGroup";
            this.lblParameterGroup.Size = new System.Drawing.Size(104, 23);
            this.lblParameterGroup.TabIndex = 3;
            this.lblParameterGroup.Text = "Parameter Group";
            this.lblParameterGroup.Visible = false;
            // 
            // frmParameter
            // 
            this.AcceptButton = this.btnView;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(856, 495);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grdParam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(632, 432);
            this.Name = "frmParameter";
            this.Text = "UKPI - Parameter Management";
            ((System.ComponentModel.ISupportInitialize)(this.grdParam)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region MainFunctions

		/// <summary>
		///fix position cho cac control
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		
		public void FixPosition()
		{	
			lblParameterGroup.Top = 10;
			cboParamGroup.Top = 10;
			lblParameterGroup.Left = 10;
			cboParamGroup.Left = lblParameterGroup.Right + 5;
			groupBox3.Height = cboParamGroup.Bottom + 12;
			//groupBox1.Top = groupBox3.Bottom + 8;
			//grdParam.Top = groupBox1.Bottom + 8;
			btnView.Top = cboParamGroup.Top - 2;
			lblParameterName.Top = 10;
			txtParamName.Top = 10;
			lblDescription.Top = lblParameterName.Bottom+ 5;
			lblParameterValue.Top = lblDescription.Bottom+5;
			txtDescription.Top = lblDescription.Top;
			txtParamValue.Top = lblParameterValue.Top;
			lblParameterName.Left = 10;
			lblParameterValue.Left = 10;
			lblDescription.Left = 10;
			txtParamName.Left = lblParameterName.Right + 5;
			txtDescription.Left = txtParamName.Left;
			txtParamValue.Left = txtParamName.Left;
			lnkBrowse.Top = txtParamValue.Top +7;
			btnUpdate.Top = txtParamValue.Top - 3;
            btnSend.Top = btnUpdate.Top;
			btnClose.Top = btnUpdate.Top;
		}
		
		/// <summary>
		/// bind du lieu tu datatable vao cac textbox
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>
		
		public void BindDataToControl()
		{
            if (m_dt.Rows.Count > 0)
            {
                txtParamName.Text = m_dt.Rows[m_manager.Position]["PARAM_NAME"].ToString();
                txtDescription.Text = m_dt.Rows[m_manager.Position]["DESCRIPTION"].ToString();
                txtParamValue.Text = m_dt.Rows[m_manager.Position]["PARAM_VALUE"].ToString();
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
		}
		
		/// <summary>
		/// Nhan mot datable tu ham LoadAll, sau do gan vao datasource cua combobox
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Minh Khoa G3
		/// Modified:	18-Apr-2006
		/// </remarks>

		private void InitializeCombo()
		{
			try
			{
				m_dt=bo.LoadAll();
                DataRow dr = m_dt.NewRow();
                dr["PARAM_GROUP"] = "[ALL]";
                m_dt.Rows.InsertAt(dr, 0);
				cboParamGroup.DataSource= DataSource;
				cboParamGroup.DisplayMember= "PARAM_GROUP";
				cboParamGroup.ValueMember= "PARAM_GROUP";
                (cboParamGroup.DataSource as DataTable).DefaultView.Sort = "PARAM_GROUP";
				cboParamGroup.SelectedIndex=0;			
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				MessageBox.Show(clsResources.GetMessage("errors.available"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

		#region Event				
		        
		private void grdParam_CurrentCellChanged(object sender, System.EventArgs e)
		{
			BindDataToControl();
		}
		private void btnView_Click(object sender, System.EventArgs e)
		{
			if(cboParamGroup.Text=="Time")
			{
				lnkBrowse.Enabled=false;
			}
			else
			{
				lnkBrowse.Enabled=true;
			}
            string strgroup = cboParamGroup.Text == "[ALL]" ? "%%" : cboParamGroup.Text;
			m_dt=bo.GetOne(strgroup, txtParameter.Text.Trim());
			grdParam.DataSource=DataSource;
			m_manager = (CurrencyManager)this.BindingContext[DataSource];
			BindDataToControl();
		}
		private void lnkBrowse_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			FolderBrowserDialog fbrdlg = new FolderBrowserDialog();
			fbrdlg.ShowNewFolderButton = true;
			if (fbrdlg.ShowDialog() == DialogResult.OK)
			{
				this.txtParamValue.Text=fbrdlg.SelectedPath;
			}
		}
		private void txtParamValue_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(cboParamGroup.Text=="Time")
			{
				char chr = e.KeyChar;
				if(!(chr >= '1' && chr <= '9' || chr == 8 || chr == 13))
					e.Handled = true;				
			}			
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cmdUpdate_Click(object sender, System.EventArgs e)
		{
			string strValue, strName, strType;
			strValue=txtParamValue.Text;
			strName=txtParamName.Text;
            if (txtParamValue.Text.Trim().Length>0)
            {
                strType=m_dt.Rows[m_manager.Position]["PARAM_TYPE"].ToString();

			    System.Windows.Forms.DialogResult i = MessageBox.Show(clsResources.GetMessage("messages.save"), clsResources.GetMessage("messages.general"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			    if (i.ToString() == "No")
				    return;

			    if (strValue=="")
			    {
				    MessageBox.Show(clsResources.GetMessage("errors.fill", txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				    txtParamValue.Focus();
				    txtParamValue.SelectAll();
				    return;
			    }
    //			if(strType =="s" && m_dt.Rows[m_manager.Position]["PARAM_NAME"].ToString() != "WS_RURAL_SKU_REMINDER_PERIOD") //nghiahbt added 28-May-07
    //			{
    //				strDes = m_dt.Rows[m_manager.Position]["DESCRIPTION"].ToString();
    //				if(strDes.IndexOf("day")!= -1)
    //				{
    //					strValue = strValue.ToUpper();
    //					if(strValue!= "MON" && strValue!= "TUE" && strValue!= "WED"
    //						&& strValue!= "THU" && strValue!= "FRI" && strValue!= "SAT" && strValue!= "SUN")
    //					{
    //						MessageBox.Show(strDes,clsResources.GetMessage("messages.general"),MessageBoxButtons.OK, MessageBoxIcon.Error);
    //						txtParamValue.Focus();
    //						txtParamValue.SelectAll();
    //						return;
    //					
    //					}
    //				}
    //			}
			    if( bo.Validate(strType, strValue) == false)
			    {
                    if (strType == "t")
                        MessageBox.Show(clsResources.GetMessage("errors.timer", txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else 
				    if(strType=="i")
					    MessageBox.Show(clsResources.GetMessage("errors.number",txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				    else if(strType=="d")
                        MessageBox.Show(clsResources.GetMessage("errors.date", txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				    else if(strType=="b")
                        MessageBox.Show(clsResources.GetMessage("errors.bool", txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (strType == "p")
                        MessageBox.Show(clsResources.GetMessage("errors.period", txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (strType == "f")
                        MessageBox.Show(clsResources.GetMessage("errors.float", txtParamValue.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
				    m_dt.RejectChanges();
				    txtParamValue.Focus();
				    txtParamValue.SelectAll();
				    return;
			    }			
    			
			    if (bo.Update(strValue, strName))
			    {
				    m_dt.Rows[m_manager.Position]["PARAM_VALUE"] = txtParamValue.Text;
				    m_dt.AcceptChanges();
				    MessageBox.Show(clsResources.GetMessage("messages.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
			    }
            }
			
		}
		
		#endregion			

      
        private void btnSend_Click(object sender, EventArgs e)
        {
            string strPath = string.Empty;
            if (!bo.SendParameters(ref strPath))
            {
                MessageBox.Show(clsResources.GetMessage("frmParameter.SendParameter.Fail"),
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(string.Format(clsResources.GetMessage("frmParameter.SendParameter.Success"), strPath),
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboParamGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboParamGroup.Text.Equals("DMS Payment"))
            {
                btnSend.Enabled = true;
            }
            else
            {
                btnSend.Enabled = false;
            }
        }


	
    }
}
