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
	/// Summary description for frmAddUser.
	/// </summary>
	public class frmAddUser : System.Windows.Forms.Form
	{
		DataTable dt = null;
		CurrencyManager _manager = null;
		clsAutUserBO bo = new clsAutUserBO();

		private bool bln_IsEdit = false;
		private bool bln_Success = false;
		private string m_strUserName = "";

		#region Window Control
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtConfirmPassword;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.DateTimePicker txtStartDate;
        private System.Windows.Forms.DateTimePicker txtEndDate;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblConfirmPassword;
		private System.Windows.Forms.Label lblFirstName;
		private System.Windows.Forms.Label lblLastName;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.Label lblStartDate;
		private System.Windows.Forms.Label lblEndDate;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.Label lblDescription;
		private DotNetSkin.SkinControls.SkinButton btnSave;
		private System.Windows.Forms.ComboBox cboURoleID;
		private System.Windows.Forms.ErrorProvider ep;
		private System.Windows.Forms.Label lblURoleID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblPhone;
        private DotNetSkin.SkinControls.SkinButton btnCancel;
		#endregion Window Control

        private GroupBox grbButton;
        private DotNetSkin.SkinControls.SkinButton skinButton1;
        private IContainer components;

		#region Contructor and Destructor
		public frmAddUser()
		{
			InitializeComponent();
			InitData();
			clsTitleManager.InitTitle(this);
		}

		public frmAddUser(string username)
		{
			InitializeComponent();
			InitData(username);
			m_strUserName = username;
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
		#endregion Contructor and Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUser));
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblURoleID = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtEndDate = new System.Windows.Forms.DateTimePicker();
            this.cboURoleID = new System.Windows.Forms.ComboBox();
            this.btnSave = new DotNetSkin.SkinControls.SkinButton();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new DotNetSkin.SkinControls.SkinButton();
            this.grbButton = new System.Windows.Forms.GroupBox();
            this.skinButton1 = new DotNetSkin.SkinControls.SkinButton();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.grbButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(19, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUserName.Size = new System.Drawing.Size(100, 20);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "User name";
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(19, 48);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPassword.Size = new System.Drawing.Size(100, 20);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password";
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.Location = new System.Drawing.Point(19, 76);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblConfirmPassword.Size = new System.Drawing.Size(100, 20);
            this.lblConfirmPassword.TabIndex = 0;
            this.lblConfirmPassword.Text = "Cornfirm password";
            // 
            // lblFirstName
            // 
            this.lblFirstName.Location = new System.Drawing.Point(19, 104);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFirstName.Size = new System.Drawing.Size(100, 20);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "First name";
            // 
            // lblLastName
            // 
            this.lblLastName.Location = new System.Drawing.Point(19, 132);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblLastName.Size = new System.Drawing.Size(100, 20);
            this.lblLastName.TabIndex = 0;
            this.lblLastName.Text = "Last name";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(19, 160);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblAddress.Size = new System.Drawing.Size(100, 20);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Address";
            // 
            // lblStartDate
            // 
            this.lblStartDate.Location = new System.Drawing.Point(19, 244);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStartDate.Size = new System.Drawing.Size(100, 20);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "From date";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(19, 272);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblEndDate.Size = new System.Drawing.Size(100, 20);
            this.lblEndDate.TabIndex = 0;
            this.lblEndDate.Text = "To date";
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(19, 188);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblEmail.Size = new System.Drawing.Size(100, 20);
            this.lblEmail.TabIndex = 26;
            this.lblEmail.Text = "Email";
            // 
            // lblURoleID
            // 
            this.lblURoleID.Location = new System.Drawing.Point(19, 300);
            this.lblURoleID.Name = "lblURoleID";
            this.lblURoleID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblURoleID.Size = new System.Drawing.Size(100, 20);
            this.lblURoleID.TabIndex = 24;
            this.lblURoleID.Text = "Role";
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(19, 216);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPhone.Size = new System.Drawing.Size(100, 20);
            this.lblPhone.TabIndex = 28;
            this.lblPhone.Text = "Phone";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(19, 331);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDescription.Size = new System.Drawing.Size(100, 20);
            this.lblDescription.TabIndex = 23;
            this.lblDescription.Text = "Description";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(124, 20);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(144, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(124, 48);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(144, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(124, 76);
            this.txtConfirmPassword.MaxLength = 20;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(144, 20);
            this.txtConfirmPassword.TabIndex = 2;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(124, 104);
            this.txtFirstName.MaxLength = 50;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(144, 20);
            this.txtFirstName.TabIndex = 3;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(124, 132);
            this.txtLastName.MaxLength = 50;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(144, 20);
            this.txtLastName.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(124, 160);
            this.txtAddress.MaxLength = 255;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(144, 20);
            this.txtAddress.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(124, 188);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(144, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(124, 216);
            this.txtPhone.MaxLength = 50;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(144, 20);
            this.txtPhone.TabIndex = 7;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(124, 332);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(296, 64);
            this.txtDescription.TabIndex = 12;
            // 
            // txtStartDate
            // 
            this.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtStartDate.Location = new System.Drawing.Point(124, 244);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(96, 20);
            this.txtStartDate.TabIndex = 8;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtEndDate.Location = new System.Drawing.Point(124, 272);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(96, 20);
            this.txtEndDate.TabIndex = 9;
            // 
            // cboURoleID
            // 
            this.cboURoleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURoleID.Location = new System.Drawing.Point(124, 296);
            this.cboURoleID.Name = "cboURoleID";
            this.cboURoleID.Size = new System.Drawing.Size(148, 21);
            this.cboURoleID.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(211, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(272, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(8, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(272, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(272, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(8, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(272, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(272, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(8, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(276, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(8, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(312, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grbButton
            // 
            this.grbButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbButton.Controls.Add(this.skinButton1);
            this.grbButton.Controls.Add(this.btnSave);
            this.grbButton.Controls.Add(this.btnCancel);
            this.grbButton.Location = new System.Drawing.Point(12, 428);
            this.grbButton.Name = "grbButton";
            this.grbButton.Size = new System.Drawing.Size(418, 52);
            this.grbButton.TabIndex = 29;
            this.grbButton.TabStop = false;
            // 
            // skinButton1
            // 
            this.skinButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButton1.Image = ((System.Drawing.Image)(resources.GetObject("skinButton1.Image")));
            this.skinButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.skinButton1.Location = new System.Drawing.Point(116, 17);
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.Size = new System.Drawing.Size(85, 23);
            this.skinButton1.TabIndex = 15;
            this.skinButton1.Text = "Cấp QL";
            // 
            // frmAddUser
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(442, 486);
            this.Controls.Add(this.grbButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblURoleID);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.txtStartDate);
            this.Controls.Add(this.txtEndDate);
            this.Controls.Add(this.cboURoleID);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblPhone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddUser";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "frmAddUser";
            this.Activated += new System.EventHandler(this.frmAddUser_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.grbButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// User Name
		/// </summary>
		public string UserName
		{
			get{return m_strUserName;}
		}

		/// <summary>
		/// Return true if update successfully. Otherwise return false
		/// </summary>
		public bool Success
		{
			get{return bln_Success;}
			set{bln_Success = value;}
		}

		/// <summary>
		/// User
		/// </summary>
		public DataRow User
		{
			get
			{
				if(_manager == null || _manager.Position < 0)
					return null;
				else
				{
					DataRowView rview = (DataRowView)_manager.Current;
					return rview.Row;
				}
			}
		}

		/// <summary>
		/// Init data
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void InitData()
		{
			dt = bo.GetSchemaTable();
			_manager = (CurrencyManager)this.BindingContext[dt];
			_manager.AddNew();
			_manager.Position = 0;
			BindDataToControl();
			bln_IsEdit = false;
			ep.DataSource = dt;
		}

		/// <summary>
		/// Load user by UserName
		/// </summary>
		/// <param name="username"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void InitData(string username)
		{
			try
			{
				clsCryptography crypto = new clsCryptography();

				dt = bo.GetOne(username);
				_manager = (CurrencyManager)this.BindingContext[dt];
				_manager.Position = 0;
				
				BindDataToControl();
				txtUserName.ReadOnly = true;
				bln_IsEdit = true;
				ep.DataSource = dt;
				
				dt.Rows[0]["PASSWORD"] = crypto.Decode(dt.Rows[0]["PASSWORD"].ToString());
				dt.Rows[0]["OLDPASSWORD"] = crypto.Decode(dt.Rows[0]["OLDPASSWORD"].ToString());
				txtConfirmPassword.Text = dt.Rows[0]["PASSWORD"].ToString();

				dt.AcceptChanges();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Bind data of datatable to Control
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void BindDataToControl()
		{
			DataTable dtUserRole = bo.LoadAllUserRole();
			//dtUserRole.Rows.InsertAt(dtUserRole.NewRow(), 0);

			cboURoleID.ValueMember = "UROLE_ID";
			cboURoleID.DisplayMember = "ROLE_NAME";
			cboURoleID.DataSource = dtUserRole;


			DataTable dtStatus = bo.LoadAllStatus();
			//dtStatus.Rows.InsertAt(dtStatus.NewRow(), 0);

			//cboStatus.ValueMember = "Value";
			//cboStatus.DisplayMember = "Name";
			//cboStatus.DataSource = dtStatus;


			txtUserName.DataBindings.Clear();
			txtPassword.DataBindings.Clear();
			txtFirstName.DataBindings.Clear();
			txtLastName.DataBindings.Clear();
			txtEmail.DataBindings.Clear();
			txtAddress.DataBindings.Clear();
			txtPhone.DataBindings.Clear();
			txtStartDate.DataBindings.Clear();
			txtEndDate.DataBindings.Clear();
			//cboStatus.DataBindings.Clear();
			cboURoleID.DataBindings.Clear();
			txtDescription.DataBindings.Clear();

			txtUserName.DataBindings.Add("Text", dt, "USERNAME");
			txtPassword.DataBindings.Add("Text", dt, "PASSWORD");
			txtFirstName.DataBindings.Add("Text", dt, "FIRSTNAME");
			txtLastName.DataBindings.Add("Text", dt, "LASTNAME");
			txtEmail.DataBindings.Add("Text", dt, "EMAIL");
			txtAddress.DataBindings.Add("Text", dt, "ADDRESS");
			txtPhone.DataBindings.Add("Text", dt, "PHONE");
			txtStartDate.DataBindings.Add("Value", dt, "START_DATE");
			txtEndDate.DataBindings.Add("Value", dt, "END_DATE");
			//cboStatus.DataBindings.Add("SelectedValue", dt, "STATUS");
			cboURoleID.DataBindings.Add("SelectedValue", dt, "UROLE_ID");
			txtDescription.DataBindings.Add("Text", dt, "DESCRIPTION");
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
			DataRowView rview = (DataRowView)_manager.Current;
			DataRow row = rview.Row;
			clsCommon common = new clsCommon();

			row.ClearErrors();
			ep.SetError(txtConfirmPassword, "");

			string userName = row["USERNAME"].ToString();
			if(userName.Length == 0)
			{
				row.SetColumnError("USERNAME", clsResources.GetMessage("errors.required", lblUserName.Text));
				txtUserName.Focus();
				return false;
			}
			else if(!common.IsLetterAndDigit(userName))
			{
				row.SetColumnError("USERNAME", clsResources.GetMessage("errors.string.specialChar", lblUserName.Text));
				txtUserName.Focus();
				return false;
			}

			if(row["PASSWORD"].ToString().Length == 0)
			{
				row.SetColumnError("PASSWORD", clsResources.GetMessage("errors.required", lblPassword.Text));
				txtPassword.Focus();
				return false;
			}

			if(txtConfirmPassword.Text != txtPassword.Text)
			{
				ep.SetError(txtConfirmPassword, clsResources.GetMessage("errors.compare.equal", lblConfirmPassword.Text, lblPassword.Text));
				return false;
			}

			if(row["FIRSTNAME"].ToString().Length == 0)
			{
				row.SetColumnError("FIRSTNAME", clsResources.GetMessage("errors.required", lblFirstName.Text));
				txtFirstName.Focus();
				return false;
			}

			if(row["LASTNAME"].ToString().Length == 0)
			{
				row.SetColumnError("LASTNAME", clsResources.GetMessage("errors.required", lblLastName.Text));
				txtFirstName.Focus();
				return false;
			}

			if(row["EMAIL"] == DBNull.Value)
			{
				row.SetColumnError("EMAIL", clsResources.GetMessage("errors.required", lblEmail.Text));
				txtEmail.Focus();
				return false;
			}
			else if(row["EMAIL"].ToString().Length > 0 && !common.IsEmail(row["EMAIL"].ToString()))
			{
				row.SetColumnError("EMAIL", clsResources.GetMessage("errors.email", lblAddress.Text));
				txtAddress.Focus();
				return false;
			}

			if(row["ADDRESS"] == DBNull.Value)
			{
				row.SetColumnError("ADDRESS", clsResources.GetMessage("errors.required", lblAddress.Text));
				txtAddress.Focus();
				return false;
			}

			if(row["PHONE"] == DBNull.Value)
			{
				row.SetColumnError("PHONE", clsResources.GetMessage("errors.required", lblPhone.Text));
				txtPhone.Focus();
				return false;
			}
            if (!clsCommon.IsNumericPositive(row["PHONE"].ToString()))
            {
                row.SetColumnError("PHONE", clsResources.GetMessage("frmEditUser.CheckPhone.GreaterEqualZeroNumber", "PHONE", lblPhone.Text));
                txtPhone.Focus();
                return false;
            }

			if(txtStartDate.Value > txtEndDate.Value)
			{
				row.SetColumnError("END_DATE", clsResources.GetMessage("errors.compare.datetime", lblStartDate.Text, lblEndDate.Text));
				txtStartDate.Focus();
				return false;
			}

			if(row["UROLE_ID"] == DBNull.Value)
			{
				row.SetColumnError("UROLE_ID", clsResources.GetMessage("errors.required", lblURoleID.Text));
				cboURoleID.Focus();
				return false;
			}

            //if(row["STATUS"] == DBNull.Value)
            //{
            //    row.SetColumnError("STATUS", clsResources.GetMessage("errors.required", lblStatus.Text));
            //    cboURoleID.Focus();
            //    return false;
            //}

			if(row["DESCRIPTION"] == DBNull.Value)
			{
				row.SetColumnError("DESCRIPTION", clsResources.GetMessage("errors.required", lblDescription.Text));
				txtDescription.Focus();
				return false;
			}

			if(!bln_IsEdit && bo.Exist(row["USERNAME"].ToString()))
			{
				row.SetColumnError("USERNAME", clsResources.GetMessage("errors.userName.exist"));
				txtUserName.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// Save data into database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(_manager != null)
				_manager.EndCurrentEdit();

			//ep.SetError(txtConfirmPassword, null);
			if(txtPassword.Text != txtConfirmPassword.Text)
			{
				ep.SetError(txtConfirmPassword, clsResources.GetMessage("errors.compare.equal", lblConfirmPassword.Text, lblPassword.Text));
				return;
			}

			if(User != null && User.RowState == DataRowState.Unchanged)
				return;

			if(bln_IsEdit)
			{
				txtUserName.Text = txtUserName.Text.Trim();
				if(ValidateData())
				{
					try
					{
						if(bo.Update(User) > 0)
						{
							MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
							Success = true;
							this.Close();
						}
						else
						{
							MessageBox.Show(clsResources.GetMessage("messages.save.fail"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
							Success = false;
						}
					}
					catch(Exception ex)
					{
						MessageBox.Show(clsResources.GetMessage("messages.save.fail") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			else
			{
				txtUserName.Text = txtUserName.Text.Trim();
				if(ValidateData())
				{
					try
					{
						if(bo.Insert(User) > 0)
						{
							MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
							Success = true;
							//Do du lieu
							//_manager.RemoveAt(0);
							//dt.AcceptChanges();
							//dt.Rows.Clear();
							_manager.AddNew();
							//dt.AcceptChanges();
							txtConfirmPassword.Text = "";
							//dt.Rows.Add(dt.NewRow());
							//_manager.Position = 0;
							//cboStatus.SelectedIndex = -1;
							//cboURoleID.SelectedIndex = -1;
							//this.Close();
						}
						else
						{
							MessageBox.Show(clsResources.GetMessage("messages.save.fail"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
							Success = false;
						}
					}
					catch(Exception ex)
					{
						MessageBox.Show(clsResources.GetMessage("messages.save.fail") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
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
		private void frmAddUser_Activated(object sender, System.EventArgs e)
		{
			txtUserName.Focus();
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
	}
}
