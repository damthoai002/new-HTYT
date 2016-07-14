using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using UKPI.Utils;
using UKPI.BusinessObject;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for frmChangeCustCode.
	/// </summary>
	public class frmChangeCustCode : System.Windows.Forms.Form
	{
		private DotNetSkin.SkinControls.SkinButton btnOK;
		private DotNetSkin.SkinControls.SkinButton btnCancel;
		private System.Windows.Forms.Label lbNewCustCode;
		private System.Windows.Forms.Label lOldCustcode;
		private System.Windows.Forms.TextBox txtOldCustCode;
		private System.Windows.Forms.TextBox txtNewCustCode;

		private System.Windows.Forms.ErrorProvider ep;
		private clsAutUserBO bo = new clsAutUserBO();
        private bool bln_Success = false;
        private IContainer components;

		public frmChangeCustCode()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeCustCode));
            this.lbNewCustCode = new System.Windows.Forms.Label();
            this.txtOldCustCode = new System.Windows.Forms.TextBox();
            this.txtNewCustCode = new System.Windows.Forms.TextBox();
            this.lOldCustcode = new System.Windows.Forms.Label();
            this.btnOK = new DotNetSkin.SkinControls.SkinButton();
            this.btnCancel = new DotNetSkin.SkinControls.SkinButton();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.SuspendLayout();
            // 
            // lbNewCustCode
            // 
            this.lbNewCustCode.AutoSize = true;
            this.lbNewCustCode.Location = new System.Drawing.Point(55, 53);
            this.lbNewCustCode.Name = "lbNewCustCode";
            this.lbNewCustCode.Size = new System.Drawing.Size(105, 13);
            this.lbNewCustCode.TabIndex = 14;
            this.lbNewCustCode.Text = "New customer code:";
            // 
            // txtOldCustCode
            // 
            this.txtOldCustCode.Location = new System.Drawing.Point(162, 22);
            this.txtOldCustCode.Name = "txtOldCustCode";
            this.txtOldCustCode.Size = new System.Drawing.Size(154, 20);
            this.txtOldCustCode.TabIndex = 15;
            // 
            // txtNewCustCode
            // 
            this.txtNewCustCode.Location = new System.Drawing.Point(162, 51);
            this.txtNewCustCode.Name = "txtNewCustCode";
            this.txtNewCustCode.Size = new System.Drawing.Size(155, 20);
            this.txtNewCustCode.TabIndex = 16;
            // 
            // lOldCustcode
            // 
            this.lOldCustcode.AutoSize = true;
            this.lOldCustcode.Location = new System.Drawing.Point(61, 25);
            this.lOldCustcode.Name = "lOldCustcode";
            this.lOldCustcode.Size = new System.Drawing.Size(99, 13);
            this.lOldCustcode.TabIndex = 13;
            this.lOldCustcode.Text = "Old customer code:";
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(86, 92);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 25);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(197, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // frmChangeCustCode
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(376, 140);
            this.Controls.Add(this.lbNewCustCode);
            this.Controls.Add(this.txtOldCustCode);
            this.Controls.Add(this.txtNewCustCode);
            this.Controls.Add(this.lOldCustcode);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChangeCustCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change customer code";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(!ValidateChangeInput())
				return;
			string oldCustCode = txtOldCustCode.Text.Trim();
			string newCustCode = txtNewCustCode.Text.Trim();
			int resultValue = 0;

			try
			{
				Cursor.Current = Cursors.WaitCursor;
				
				resultValue = bo.ChangeCustCode(oldCustCode, newCustCode);
				//----
				if (resultValue == -1)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.CustomerExist"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtOldCustCode.Focus();
				}
				else if (resultValue == -3)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomer"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -20)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_DISTRIBUTOR_HIERARCHY]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				
				//---------------------------------------------------------------------
				else if (resultValue == -21)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_SHIP_TO]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -22)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[TEMP_LACK_SP]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -23)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[RCM]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -24)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_ALLOCATION_FORWARDING]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -25)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_CO_HEADER]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -26)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_CO_TEMP]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -27)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_CUSTOMER_SMS]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -28)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_DEFINE_ORDER_SPLIT_HEADER]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -29)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_DELIVERY_WEIGHT]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -30)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_DISTRIBUTOR_DAILY_SCHEDULE]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -31)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_ORDER_SPLIT_HEADER]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -32)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_PARAMETERS_MOQ_FULL]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -33)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_PPO_HEADER]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -34)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_PPO_HEADER_LACK]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -35)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_PROMOTION_CUST]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -36)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_PROMOTION_CUST_SWAP]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -37)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_PROMOTION_CUST_WEEK]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}			
				else if (resultValue == -38)
				{
					MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_RURAL_SKU]"),clsResources.GetMessage("errors.general"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -39)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_RURAL_SKU_IMPORT_HEADER]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -40)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_SEC_SLS]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -41)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_SP_SPECIAL]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -42)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[FPT_ENV_CO_OTHER]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}		
				else if (resultValue == -43)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomerExist","[UERRM]"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				//---------------------------------------------------------------------
				else if (resultValue == -4)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomer_1"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == -5)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomer_2"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtNewCustCode.Focus();
				}
				else if (resultValue == 0)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomer_3"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);	
					Success = false;
					this.Close();
				}
				else if (resultValue == 1)
				{
                    MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.ChangedCustomer"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
					Success = true;
					this.Close();
				}					
					
				Cursor.Current = Cursors.Default;	
			}
			catch (Exception ex)
			{
				MessageBox.Show(clsResources.GetMessage("messages.changecustcode.fail") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					
			}

		}

		//---------------- check data input before change ---------------
		private bool ValidateChangeInput()
		{
			ep.SetError(txtOldCustCode, "");
			ep.SetError(txtNewCustCode, "");
			if(txtOldCustCode.Text.Trim().Length == 0)
			{
				ep.SetError(txtOldCustCode, clsResources.GetMessage("errors.required", txtOldCustCode.Text));
				txtOldCustCode.Focus();
				return false;
			}
			else if(txtNewCustCode.Text.Trim().Length == 0)
			{
				ep.SetError(txtNewCustCode, clsResources.GetMessage("errors.required", txtNewCustCode.Text));
				txtNewCustCode.Focus();
				return false;
			}
			else if (!ValidateNumber(txtNewCustCode.Text))
			{
                MessageBox.Show(clsResources.GetMessage("frmChangeCustCode.NewCustomer_4"), clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);	
				txtNewCustCode.Focus();
				return false;
			}
			
			return true;

		}

		public bool ValidateNumber(string input )
		{
			foreach ( char c in input )
			{
				if ( ! Char.IsNumber( c ) )
				{
					return false;
				}
			} return true;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Return true if update successfully. Otherwise return false
		/// </summary>
		public bool Success
		{
			get{return bln_Success;}
			set{bln_Success = value;}
		}
	}
}
