using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using UKPI.BusinessObject;
using UKPI.Utils;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for frmChangePWD.
	/// </summary>
	public class frmChangePWD : System.Windows.Forms.Form
	{
		private clsAutUserBO bo = new clsAutUserBO();
		private bool bln_Success = false;

		#region Window Control
		private System.Windows.Forms.Label lblOldPassword;
		private System.Windows.Forms.Label lblNewPassword;
		private System.Windows.Forms.Label lblConfirmPassword;
		private System.Windows.Forms.TextBox txtOldPassword;
		private System.Windows.Forms.TextBox txtNewPassword;
		private System.Windows.Forms.TextBox txtConfirmPassword;
		private DotNetSkin.SkinControls.SkinButton btnOK;
		private DotNetSkin.SkinControls.SkinButton btnCancel;
		private System.Windows.Forms.ErrorProvider ep;
		private System.Windows.Forms.ImageList imgs;
		private System.ComponentModel.IContainer components;
		#endregion Window Control

		#region Contructor and Destructor
		public frmChangePWD()
		{
			InitializeComponent();

			clsTitleManager.InitTitle(this);
			txtOldPassword.Focus();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePWD));
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new DotNetSkin.SkinControls.SkinButton();
            this.btnCancel = new DotNetSkin.SkinControls.SkinButton();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.imgs = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(32, 29);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(71, 13);
            this.lblOldPassword.TabIndex = 5;
            this.lblOldPassword.Text = "Old password";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(32, 56);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(77, 13);
            this.lblNewPassword.TabIndex = 6;
            this.lblNewPassword.Text = "New password";
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(32, 85);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(113, 13);
            this.lblConfirmPassword.TabIndex = 7;
            this.lblConfirmPassword.Text = "Confirm new password";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(162, 28);
            this.txtOldPassword.MaxLength = 20;
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(180, 20);
            this.txtOldPassword.TabIndex = 0;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(162, 56);
            this.txtNewPassword.MaxLength = 20;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(180, 20);
            this.txtNewPassword.TabIndex = 1;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(162, 84);
            this.txtConfirmPassword.MaxLength = 20;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(180, 20);
            this.txtConfirmPassword.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(96, 123);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(208, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // imgs
            // 
            this.imgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgs.ImageStream")));
            this.imgs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgs.Images.SetKeyName(0, "");
            this.imgs.Images.SetKeyName(1, "");
            // 
            // frmChangePWD
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(378, 164);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePWD";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change password";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Activated += new System.EventHandler(this.frmChangePWD_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Return true if update successfully. Otherwise return false
		/// </summary>
		public bool Success
		{
			get{return bln_Success;}
			set{bln_Success = value;}
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

		/// <summary>
		/// Change password. Update database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(ValidateData())
			{
				try
				{
					if(bo.ChangePassword(clsSystemConfig.UserName, txtOldPassword.Text, txtNewPassword.Text) > 0)
					{
						MessageBox.Show(clsResources.GetMessage("messages.changepassword.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
						Success = true;
						this.Close();
					}
					else
					{
						MessageBox.Show(clsResources.GetMessage("messages.changepassword.fail"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

				}
				catch(Exception ex)
				{
					MessageBox.Show(clsResources.GetMessage("messages.changepassword.fail") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// Check whether data is valid.
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public bool ValidateData()
		{
			bool valid = true;
			ep.SetError(txtOldPassword, "");
			ep.SetError(txtNewPassword, "");
			ep.SetError(txtConfirmPassword, "");

			if(txtOldPassword.Text.Length == 0)
			{
				ep.SetError(txtOldPassword, clsResources.GetMessage("errors.required", lblOldPassword.Text));
				txtOldPassword.Focus();
				valid = false;
				return valid;
			}

			if(txtNewPassword.Text.Length == 0)
			{
				ep.SetError(txtNewPassword, clsResources.GetMessage("errors.required", txtNewPassword.Text));
				txtNewPassword.Focus();
				valid = false;
				return valid;
			}

			if(txtConfirmPassword.Text != txtNewPassword.Text)
			{
				ep.SetError(txtConfirmPassword, clsResources.GetMessage("errors.required", lblConfirmPassword.Text));
				txtConfirmPassword.Focus();
				valid = false;
				return valid;
			}

			return valid;
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
		private void frmChangePWD_Activated(object sender, System.EventArgs e)
		{
			txtOldPassword.Focus();
		}
	}
}
