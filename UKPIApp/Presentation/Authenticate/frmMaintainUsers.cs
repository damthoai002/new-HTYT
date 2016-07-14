using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using UKPI.BusinessObject;
using UKPI.Utils;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for frmMaintainUsers.
	/// </summary>
	public class frmMaintainUsers : System.Windows.Forms.Form
	{
		private DataTable dt = new DataTable("FPT_ENV_AUT_USER");
        private CurrencyManager _manager = null;
		private clsAutUserBO bo = new clsAutUserBO();

		#region Window Control

		private DotNetSkin.SkinControls.SkinButton btnEdit;
		private DotNetSkin.SkinControls.SkinButton btnAdd;
		private System.Windows.Forms.DataGrid grd;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn colUserName;
		private System.Windows.Forms.DataGridTextBoxColumn colFirstName;
		private System.Windows.Forms.DataGridTextBoxColumn colLastName;
		private System.Windows.Forms.DataGridTextBoxColumn colStatus;
		private System.Windows.Forms.DataGridTextBoxColumn colURoleID;
        private System.Windows.Forms.ImageList imgs;
		private System.Windows.Forms.GroupBox grpSearch;
		private System.Windows.Forms.ComboBox cboURoleID;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.Label lblFirstName;
		private System.Windows.Forms.Label lblLastName;
		private System.Windows.Forms.Label lblURoleID;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ComboBox cboStatus;
		private DotNetSkin.SkinControls.SkinButton btnSearch;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.DataGridTextBoxColumn colEmail;
		private System.Windows.Forms.GroupBox grpButton;
		private DotNetSkin.SkinControls.SkinButton btnClose;
		private DotNetSkin.SkinControls.SkinButton btnDelete;
        private DataGridTextBoxColumn colPhone;
		private System.ComponentModel.IContainer components;
		#endregion Window Control

		#region Constructors and Destructors
		public frmMaintainUsers()
		{
			InitializeComponent();

			InitData();

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
		#endregion Constructors and Destructors

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaintainUsers));
            this.grd = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.colUserName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colFirstName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colLastName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colURoleID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnEdit = new DotNetSkin.SkinControls.SkinButton();
            this.imgs = new System.Windows.Forms.ImageList(this.components);
            this.btnAdd = new DotNetSkin.SkinControls.SkinButton();
            this.btnDelete = new DotNetSkin.SkinControls.SkinButton();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnSearch = new DotNetSkin.SkinControls.SkinButton();
            this.cboURoleID = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblURoleID = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.btnClose = new DotNetSkin.SkinControls.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.grpSearch.SuspendLayout();
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
            this.grd.Location = new System.Drawing.Point(8, 120);
            this.grd.Name = "grd";
            this.grd.ReadOnly = true;
            this.grd.Size = new System.Drawing.Size(716, 320);
            this.grd.TabIndex = 1;
            this.grd.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.AliceBlue;
            this.dataGridTableStyle1.DataGrid = this.grd;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.colUserName,
            this.colFirstName,
            this.colLastName,
            this.colEmail,
            this.colPhone,
            this.colStatus,
            this.colURoleID});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "FPT_ENV_AUT_USER";
            this.dataGridTableStyle1.ReadOnly = true;
            // 
            // colUserName
            // 
            this.colUserName.Format = "";
            this.colUserName.FormatInfo = null;
            this.colUserName.HeaderText = "USERNAME";
            this.colUserName.MappingName = "USERNAME";
            this.colUserName.Width = 75;
            // 
            // colFirstName
            // 
            this.colFirstName.Format = "";
            this.colFirstName.FormatInfo = null;
            this.colFirstName.HeaderText = "FIRSTNAME";
            this.colFirstName.MappingName = "FIRSTNAME";
            this.colFirstName.Width = 75;
            // 
            // colLastName
            // 
            this.colLastName.Format = "";
            this.colLastName.FormatInfo = null;
            this.colLastName.HeaderText = "LASTNAME";
            this.colLastName.MappingName = "LASTNAME";
            this.colLastName.Width = 75;
            // 
            // colEmail
            // 
            this.colEmail.Format = "";
            this.colEmail.FormatInfo = null;
            this.colEmail.HeaderText = "Email";
            this.colEmail.MappingName = "EMAIL";
            this.colEmail.Width = 75;
            // 
            // colPhone
            // 
            this.colPhone.Format = "";
            this.colPhone.FormatInfo = null;
            this.colPhone.HeaderText = "PHONE";
            this.colPhone.MappingName = "PHONE";
            this.colPhone.Width = 75;
            // 
            // colStatus
            // 
            this.colStatus.Format = "";
            this.colStatus.FormatInfo = null;
            this.colStatus.HeaderText = "STATUS";
            this.colStatus.MappingName = "STATUS";
            this.colStatus.Width = 70;
            // 
            // colURoleID
            // 
            this.colURoleID.Format = "";
            this.colURoleID.FormatInfo = null;
            this.colURoleID.HeaderText = "UROLE_ID";
            this.colURoleID.MappingName = "UROLE_ID";
            this.colURoleID.Width = 70;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(389, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(95, 24);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "  Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // imgs
            // 
            this.imgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgs.ImageStream")));
            this.imgs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgs.Images.SetKeyName(0, "");
            this.imgs.Images.SetKeyName(1, "");
            this.imgs.Images.SetKeyName(2, "");
            this.imgs.Images.SetKeyName(3, "");
            this.imgs.Images.SetKeyName(4, "");
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(277, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 24);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "  Add";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(501, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "  Remove";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSearch.Controls.Add(this.txtEmail);
            this.grpSearch.Controls.Add(this.lblEmail);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.cboURoleID);
            this.grpSearch.Controls.Add(this.txtUserName);
            this.grpSearch.Controls.Add(this.txtFirstName);
            this.grpSearch.Controls.Add(this.txtLastName);
            this.grpSearch.Controls.Add(this.lblUserName);
            this.grpSearch.Controls.Add(this.lblFirstName);
            this.grpSearch.Controls.Add(this.lblLastName);
            this.grpSearch.Controls.Add(this.lblURoleID);
            this.grpSearch.Controls.Add(this.lblStatus);
            this.grpSearch.Controls.Add(this.cboStatus);
            this.grpSearch.Location = new System.Drawing.Point(8, 8);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(716, 104);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(357, 16);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(165, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(297, 16);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblEmail.Size = new System.Drawing.Size(60, 20);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email";
            // 
            // btnSearch
            // 
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(553, 72);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "   Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboURoleID
            // 
            this.cboURoleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURoleID.Location = new System.Drawing.Point(101, 72);
            this.cboURoleID.Name = "cboURoleID";
            this.cboURoleID.Size = new System.Drawing.Size(168, 21);
            this.cboURoleID.TabIndex = 9;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(101, 16);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(168, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(101, 44);
            this.txtFirstName.MaxLength = 50;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(168, 20);
            this.txtFirstName.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(357, 44);
            this.txtLastName.MaxLength = 50;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(165, 20);
            this.txtLastName.TabIndex = 7;
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(15, 17);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUserName.Size = new System.Drawing.Size(83, 20);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User name";
            // 
            // lblFirstName
            // 
            this.lblFirstName.Location = new System.Drawing.Point(15, 45);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFirstName.Size = new System.Drawing.Size(83, 20);
            this.lblFirstName.TabIndex = 4;
            this.lblFirstName.Text = "First name";
            // 
            // lblLastName
            // 
            this.lblLastName.Location = new System.Drawing.Point(297, 45);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblLastName.Size = new System.Drawing.Size(60, 20);
            this.lblLastName.TabIndex = 6;
            this.lblLastName.Text = "Last name";
            // 
            // lblURoleID
            // 
            this.lblURoleID.Location = new System.Drawing.Point(14, 72);
            this.lblURoleID.Name = "lblURoleID";
            this.lblURoleID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblURoleID.Size = new System.Drawing.Size(83, 20);
            this.lblURoleID.TabIndex = 8;
            this.lblURoleID.Text = "Role";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(299, 72);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Location = new System.Drawing.Point(357, 72);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(165, 21);
            this.cboStatus.TabIndex = 11;
            // 
            // grpButton
            // 
            this.grpButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpButton.Controls.Add(this.btnClose);
            this.grpButton.Controls.Add(this.btnAdd);
            this.grpButton.Controls.Add(this.btnDelete);
            this.grpButton.Controls.Add(this.btnEdit);
            this.grpButton.Location = new System.Drawing.Point(8, 444);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(716, 52);
            this.grpButton.TabIndex = 2;
            this.grpButton.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(608, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 24);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMaintainUsers
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(732, 502);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.grd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMaintainUsers";
            this.Text = "Maintain users";
            this.Activated += new System.EventHandler(this.frmMaintainUsers_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpButton.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Init data
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void InitData()
		{
			DataTable dtUserRole = bo.LoadAllUserRole();
			dtUserRole.Rows.InsertAt(dtUserRole.NewRow(), 0);

			cboURoleID.ValueMember = "UROLE_ID";
            cboURoleID.DisplayMember = "ROLE_NAME";
			cboURoleID.DataSource = dtUserRole;


			DataTable dtStatus = bo.LoadAllStatus();
			dtStatus.Rows.InsertAt(dtStatus.NewRow(), 0);

			cboStatus.ValueMember = "Value";
			cboStatus.DisplayMember = "Name";
			cboStatus.DataSource = dtStatus;
		}

		/// <summary>
		/// Delete selected user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(_manager == null || _manager.Position < 0 )
				return;

			DataRowView rview = (DataRowView)_manager.Current;
			DataRow row = rview.Row;
			string username = (string)row["USERNAME"];
			if(MessageBox.Show(clsResources.GetMessage("warnings.delete", ""), clsResources.GetMessage("warnings.general"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try
				{
					if(bo.Delete(username) > 0)
					{
						dt.Rows.Remove(row);
						MessageBox.Show(clsResources.GetMessage("messages.delete.success", ""), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
						MessageBox.Show(clsResources.GetMessage("messages.delete.fail", clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error));
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// Add new user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			frmAddUser frm = new frmAddUser();
			clsFormManager.ShowMDIChild(frm, this);
			//frm.ShowDialog();
			//InitData();
		}

		/// <summary>
		/// Edit infomation of selected user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if(_manager == null || _manager.Position < 0 )
				return;

			DataRowView rview = (DataRowView)_manager.Current;
			DataRow row = rview.Row;
			string username = (string)row["USERNAME"];

			frmAddUser frm = new frmAddUser(username);
			clsFormManager.ShowDialogMDIChild(frm);
			//frm.ShowDialog();
			DataRow user = frm.User;
			if(user != null)
			{
				row["USERNAME"] = user["USERNAME"];
				row["FIRSTNAME"] = user["FIRSTNAME"];
				row["LASTNAME"] = user["LASTNAME"];
				row["EMAIL"] = user["EMAIL"];
				row["STATUS"] = user["STATUS"];
				row["UROLE_ID"] = user["UROLE_ID"];
			}
			row.AcceptChanges();
			//InitData();
		}

		/// <summary>
		/// Search data by keywords
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
//			string roleID = ((DataRowView)cboURoleID.SelectedItem)["UROLE_ID"].ToString();
//			string status = ((DataRowView)cboStatus.SelectedItem)["Value"].ToString();
			string roleID = cboURoleID.SelectedValue.ToString();
            string status = cboStatus.SelectedValue.ToString();

			dt.Rows.Clear();
			dt = bo.Search(dt, txtUserName.Text.Trim(), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text.Trim() , roleID, status);
			if(_manager == null)
			{
				_manager = (CurrencyManager)this.BindingContext[dt];
				grd.DataSource = dt;
				clsCommon.RegAutoSizeCol(grd);
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
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Focus to the first control on activated events 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void frmMaintainUsers_Activated(object sender, System.EventArgs e)
		{
            txtUserName.Focus();
		}
	}
}
