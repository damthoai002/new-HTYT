using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using UKPI.BusinessObject;
using UKPI.Utils;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for frmAutPolicy.
	/// </summary>
	public class frmAutPolicy : System.Windows.Forms.Form
	{
		DataTable dt = null;
		CurrencyManager _manager = null;
		clsAutPolicyBO bo = new clsAutPolicyBO();

		private string strURoleID;
		public string URoleID
		{
			get{return strURoleID;}
			set{strURoleID = value;}
		}

		#region Window Control
		private System.Windows.Forms.DataGrid grd;
		private System.Windows.Forms.GroupBox grpButton;
		private DotNetSkin.SkinControls.SkinButton btnCancel;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
		private System.Windows.Forms.DataGridBoolColumn colSelected;
		private System.Windows.Forms.CheckBox chkSelected;
		private DotNetSkin.SkinControls.SkinButton btnSave;
        private TextBox txtFormName;
        private Label lblFormName;
        private TextBox txtMenuName;
        private Label lblMenuName;
        private TextBox txtDesc;
        private Label lblDesc;
        private DotNetSkin.SkinControls.SkinButton btnSearch;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion Window Control

		public frmAutPolicy(string URoleID)
		{
			InitializeComponent();
			clsCommon.RegAutoSizeCol(grd);
			this.URoleID = URoleID;
			InitData(URoleID);
			clsTitleManager.InitTitle(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutPolicy));
            this.grd = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colSelected = new System.Windows.Forms.DataGridBoolColumn();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.chkSelected = new System.Windows.Forms.CheckBox();
            this.btnCancel = new DotNetSkin.SkinControls.SkinButton();
            this.btnSave = new DotNetSkin.SkinControls.SkinButton();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.lblFormName = new System.Windows.Forms.Label();
            this.txtMenuName = new System.Windows.Forms.TextBox();
            this.lblMenuName = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnSearch = new DotNetSkin.SkinControls.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.grpButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // grd
            // 
            this.grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grd.CaptionVisible = false;
            this.grd.DataMember = "";
            this.grd.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.grd.Location = new System.Drawing.Point(7, 35);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(746, 375);
            this.grd.TabIndex = 0;
            this.grd.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.grd;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn9,
            this.colSelected});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "FPT_ENV_AUT_POLICY";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Form ID";
            this.dataGridTextBoxColumn4.MappingName = "FORM_ID";
            this.dataGridTextBoxColumn4.NullText = "";
            this.dataGridTextBoxColumn4.ReadOnly = true;
            this.dataGridTextBoxColumn4.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Form name";
            this.dataGridTextBoxColumn5.MappingName = "FORM_NAME";
            this.dataGridTextBoxColumn5.NullText = "";
            this.dataGridTextBoxColumn5.ReadOnly = true;
            this.dataGridTextBoxColumn5.Width = 150;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Menu name";
            this.dataGridTextBoxColumn6.MappingName = "MENU_NAME";
            this.dataGridTextBoxColumn6.NullText = "";
            this.dataGridTextBoxColumn6.ReadOnly = true;
            this.dataGridTextBoxColumn6.Width = 150;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "Description";
            this.dataGridTextBoxColumn9.MappingName = "DESCRIPTION";
            this.dataGridTextBoxColumn9.NullText = "";
            this.dataGridTextBoxColumn9.ReadOnly = true;
            this.dataGridTextBoxColumn9.Width = 150;
            // 
            // colSelected
            // 
            this.colSelected.FalseValue = "0";
            this.colSelected.HeaderText = "Selected";
            this.colSelected.MappingName = "Selected";
            this.colSelected.NullText = "";
            this.colSelected.TrueValue = "1";
            this.colSelected.Width = 75;
            // 
            // grpButton
            // 
            this.grpButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpButton.Controls.Add(this.chkSelected);
            this.grpButton.Controls.Add(this.btnCancel);
            this.grpButton.Controls.Add(this.btnSave);
            this.grpButton.Location = new System.Drawing.Point(7, 420);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(747, 52);
            this.grpButton.TabIndex = 9;
            this.grpButton.TabStop = false;
            // 
            // chkSelected
            // 
            this.chkSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelected.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkSelected.Location = new System.Drawing.Point(462, 17);
            this.chkSelected.Name = "chkSelected";
            this.chkSelected.Size = new System.Drawing.Size(71, 24);
            this.chkSelected.TabIndex = 8;
            this.chkSelected.Text = "Check";
            this.chkSelected.CheckedChanged += new System.EventHandler(this.chkSelected_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(643, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(546, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(90, 6);
            this.txtFormName.MaxLength = 12;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(96, 20);
            this.txtFormName.TabIndex = 20;
            // 
            // lblFormName
            // 
            this.lblFormName.Location = new System.Drawing.Point(16, 8);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFormName.Size = new System.Drawing.Size(72, 18);
            this.lblFormName.TabIndex = 13;
            this.lblFormName.Text = "Form Name";
            // 
            // txtMenuName
            // 
            this.txtMenuName.Location = new System.Drawing.Point(285, 6);
            this.txtMenuName.MaxLength = 20;
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.Size = new System.Drawing.Size(96, 20);
            this.txtMenuName.TabIndex = 14;
            // 
            // lblMenuName
            // 
            this.lblMenuName.Location = new System.Drawing.Point(211, 9);
            this.lblMenuName.Name = "lblMenuName";
            this.lblMenuName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMenuName.Size = new System.Drawing.Size(72, 17);
            this.lblMenuName.TabIndex = 15;
            this.lblMenuName.Text = "Menu Name";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(482, 6);
            this.txtDesc.MaxLength = 20;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(96, 20);
            this.txtDesc.TabIndex = 16;
            // 
            // lblDesc
            // 
            this.lblDesc.Location = new System.Drawing.Point(409, 9);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDesc.Size = new System.Drawing.Size(72, 18);
            this.lblDesc.TabIndex = 17;
            this.lblDesc.Text = "Description";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(602, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "   Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmAutPolicy
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(760, 477);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtMenuName);
            this.Controls.Add(this.lblMenuName);
            this.Controls.Add(this.txtFormName);
            this.Controls.Add(this.lblFormName);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.grd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutPolicy";
            this.Text = "frmAutPolicy";
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.grpButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Init Data: Get Role by RoleID
		/// </summary>
		/// <param name="URoleID"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void InitData(string URoleID)
		{
			dt = bo.GetPolicy(URoleID);
			foreach(DataRow row in dt.Rows)
			{
                if (row["UROLE_ID"] == DBNull.Value)
                    row["Selected"] = "0";
                else
                    row["Selected"] = "1";
			}
			dt.AcceptChanges();
			dt.DefaultView.AllowNew = false;
			dt.DefaultView.AllowDelete = false;

			_manager = (CurrencyManager)this.BindingContext[dt];

			grd.DataSource = dt;
		}

		/// <summary>
		/// Handle event CheckedChanged of chkCheck. Check or Uncheck all CheckBox of selected row in datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void chkSelected_CheckedChanged(object sender, System.EventArgs e)
		{
			if(dt == null)
				return;

			string value = "";
			if(chkSelected.Checked)
				value = "1";
			else
				value = "0";

			grd.BeginInit();
			dt.BeginInit();

			DataView view = dt.DefaultView;
			int count = view.Count;

			for(int i = 0; i < count ; i ++ )
			{
                if (grd.IsSelected(i))
					view[i].Row["Selected"] = value;
			}

			dt.EndInit();
			grd.EndInit();
		}

		/// <summary>
		/// Get all FeatureID of added row and deleted row in DataGrid
		/// </summary>
		/// <param name="added"></param>
		/// <param name="deleted"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void GetChanged(ref ArrayList added, ref ArrayList deleted)
		{
			foreach(DataRow row in dt.Rows)
			{
				if(row["UROLE_ID"] == DBNull.Value && row["Selected"].ToString() == "1")
					added.Add(row["FEATURE_ID"].ToString());
				else if(row["UROLE_ID"] != DBNull.Value && row["Selected"].ToString() == "0")
					deleted.Add(row["FEATURE_ID"].ToString());
			}
		}

		/// <summary>
		/// Save all changes into database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			ArrayList added = new ArrayList();
			ArrayList deleted = new ArrayList();
			GetChanged(ref added, ref deleted);
			try
			{
				bo.UpdateAll(URoleID, added, deleted);
				InitData(URoleID);
				MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch(Exception ex)
			{
				MessageBox.Show(clsResources.GetMessage("errors.policy.update") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
		}

		/// <summary>
		/// Close this form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void btnSearch_Click(object sender, EventArgs e)
        {
            filter(txtFormName.Text.Trim(), txtMenuName.Text.Trim(), txtDesc.Text.Trim());
        }

        private void filter(string strFormName, string strMenuName, string strDesc)
        {            
            dt.DefaultView.RowFilter = string.Format("FORM_NAME like '%{0}%' AND MENU_NAME like '%{1}%' AND DESCRIPTION like '%{2}%'", strFormName, strMenuName, strDesc);
        }
	}
}
