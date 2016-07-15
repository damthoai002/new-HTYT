using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit; 

using UKPI.Utils;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label lblClose;
		private System.Windows.Forms.TextBox txtAboutInfo;
		private System.Windows.Forms.Label lblVersion;
        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBox1;
		//application's version
		private string strVersion = "";

		public frmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			//clsStyleManager.ChangeStyle(this);
			clsResources.GetMessage("frmAbout.Content.Lisence");

			Assembly execAssembly = Assembly.GetExecutingAssembly();

			AssemblyName name = execAssembly.GetName();

			// now extract various bits of information
			strVersion = name.Version.Major.ToString() + "." + name.Version.Minor.ToString();
			//get text info from message
			txtAboutInfo.Text = clsResources.GetMessage("frmAbout.txtAboutInfo.Content");
			//enter down 1 line
			txtAboutInfo.Text += Environment.NewLine;
			for(int i = 20; i > 0; i--)
			{
				txtAboutInfo.Text += clsResources.GetMessage("frmAbout.txtAboutInfo.Content_seperate");
			}
			//put version to about form
			txtAboutInfo.Text += clsResources.GetMessage("frmAbout.lblVersion.Content",  Environment.NewLine + Environment.NewLine, strVersion );
			
			lblVersion.Text = clsResources.GetMessage("frmAbout.lblVersion.Content",  "", strVersion );
			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.lblClose = new System.Windows.Forms.Label();
            this.txtAboutInfo = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClose
            // 
            this.lblClose.ForeColor = System.Drawing.SystemColors.Control;
            this.lblClose.Image = ((System.Drawing.Image)(resources.GetObject("lblClose.Image")));
            this.lblClose.Location = new System.Drawing.Point(406, 350);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(56, 24);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = "Close";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            this.lblClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblClose_MouseDown);
            // 
            // txtAboutInfo
            // 
            this.txtAboutInfo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtAboutInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAboutInfo.Location = new System.Drawing.Point(77, 127);
            this.txtAboutInfo.Multiline = true;
            this.txtAboutInfo.Name = "txtAboutInfo";
            this.txtAboutInfo.ReadOnly = true;
            this.txtAboutInfo.Size = new System.Drawing.Size(303, 89);
            this.txtAboutInfo.TabIndex = 2;
            this.txtAboutInfo.Text = resources.GetString("txtAboutInfo.Text");
            this.txtAboutInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAboutInfo.UseWaitCursor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(234, 374);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(192, 16);
            this.lblVersion.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(77, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(303, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 389);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtAboutInfo);
            this.Controls.Add(this.lblClose);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 405);
            this.MinimumSize = new System.Drawing.Size(500, 405);
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void lblClose_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//			lblClose.Image = Image.FromFile("",);
		}

		private void lblClose_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}
	}
}
