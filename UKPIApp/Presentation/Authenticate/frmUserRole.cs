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
	/// Summary description for frmRole.
	/// </summary>
	public class frmUserRole : System.Windows.Forms.Form
	{
		DataTable dt = null;
		clsUserRoleBO bo = new clsUserRoleBO();
		CurrencyManager m_manager = null;

		#region Window Control

		private System.Windows.Forms.TextBox txtURoleID;
		private System.Windows.Forms.Label lblURoleID;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private DotNetSkin.SkinControls.SkinButton btnSearch;
		private DotNetSkin.SkinControls.SkinButton btnAdd;
		private DotNetSkin.SkinControls.SkinButton btnDelete;
		private DotNetSkin.SkinControls.SkinButton btnClose;
		private DotNetSkin.SkinControls.SkinButton btnSave;
		private System.Windows.Forms.GroupBox grpSearch;
		private System.Windows.Forms.GroupBox grpButton;
		private System.Windows.Forms.DataGrid grd;
		private System.Windows.Forms.DataGridTextBoxColumn colURoleID;
		private System.Windows.Forms.DataGridTextBoxColumn colRoleName;
		private DotNetSkin.SkinControls.SkinButton btnCancel;
		private DotNetSkin.SkinControls.SkinButton btnPolicy;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion Window Control

		public frmUserRole()
		{
			InitializeComponent();
            clsTitleManager.InitTitle(this);

            InitData();

            btnAdd.Click +=new EventHandler(EventListener);
            btnDelete.Click += new EventHandler(EventListener);
            btnSave.Click += new EventHandler(EventListener);
            btnCancel.Click +=new EventHandler(EventListener);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRole));
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new DotNetSkin.SkinControls.SkinButton();
            this.txtURoleID = new System.Windows.Forms.TextBox();
            this.lblURoleID = new System.Windows.Forms.Label();
            this.grd = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.colURoleID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.colRoleName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.btnPolicy = new DotNetSkin.SkinControls.SkinButton();
            this.btnCancel = new DotNetSkin.SkinControls.SkinButton();
            this.btnAdd = new DotNetSkin.SkinControls.SkinButton();
            this.btnDelete = new DotNetSkin.SkinControls.SkinButton();
            this.btnSave = new DotNetSkin.SkinControls.SkinButton();
            this.btnClose = new DotNetSkin.SkinControls.SkinButton();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.grpButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSearch
            // 
            this.grpSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.txtURoleID);
            this.grpSearch.Controls.Add(this.lblURoleID);
            this.grpSearch.Location = new System.Drawing.Point(7, 5);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(641, 53);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(256, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtURoleID
            // 
            this.txtURoleID.Location = new System.Drawing.Point(65, 19);
            this.txtURoleID.MaxLength = 14;
            this.txtURoleID.Name = "txtURoleID";
            this.txtURoleID.Size = new System.Drawing.Size(165, 20);
            this.txtURoleID.TabIndex = 1;
            // 
            // lblURoleID
            // 
            this.lblURoleID.Location = new System.Drawing.Point(13, 21);
            this.lblURoleID.Name = "lblURoleID";
            this.lblURoleID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblURoleID.Size = new System.Drawing.Size(50, 16);
            this.lblURoleID.TabIndex = 0;
            this.lblURoleID.Text = "Role ID";
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
            this.grd.Location = new System.Drawing.Point(7, 66);
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(641, 325);
            this.grd.TabIndex = 1;
            this.grd.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.grd;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.colURoleID,
            this.colRoleName});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "FPT_ENV_AUT_USERROLE";
            // 
            // colURoleID
            // 
            this.colURoleID.Format = "";
            this.colURoleID.FormatInfo = null;
            this.colURoleID.HeaderText = "Role ID";
            this.colURoleID.MappingName = "UROLE_ID";
            this.colURoleID.NullText = "";
            this.colURoleID.Width = 102;
            // 
            // colRoleName
            // 
            this.colRoleName.Format = "";
            this.colRoleName.FormatInfo = null;
            this.colRoleName.HeaderText = "Role name";
            this.colRoleName.MappingName = "ROLE_NAME";
            this.colRoleName.NullText = "";
            this.colRoleName.Width = 208;
            // 
            // grpButton
            // 
            this.grpButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpButton.Controls.Add(this.btnPolicy);
            this.grpButton.Controls.Add(this.btnCancel);
            this.grpButton.Controls.Add(this.btnAdd);
            this.grpButton.Controls.Add(this.btnDelete);
            this.grpButton.Controls.Add(this.btnSave);
            this.grpButton.Controls.Add(this.btnClose);
            this.grpButton.Location = new System.Drawing.Point(7, 396);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(641, 49);
            this.grpButton.TabIndex = 2;
            this.grpButton.TabStop = false;
            // 
            // btnPolicy
            // 
            this.btnPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPolicy.Image = ((System.Drawing.Image)(resources.GetObject("btnPolicy.Image")));
            this.btnPolicy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPolicy.Location = new System.Drawing.Point(45, 15);
            this.btnPolicy.Name = "btnPolicy";
            this.btnPolicy.Size = new System.Drawing.Size(85, 23);
            this.btnPolicy.TabIndex = 5;
            this.btnPolicy.Text = "Policy";
            this.btnPolicy.Click += new System.EventHandler(this.btnPolicy_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(441, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = " Cancel";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(144, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(243, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(342, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(540, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUserRole
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(656, 453);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.grpSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserRole";
            this.Text = "Define Role";
            this.Activated += new System.EventHandler(this.frmUserRole_Activated);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.grpButton.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Update all datarow by state
		/// </summary>
		/// <param name="dt"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void UpdateAll(DataTable dt)
		{
			
			try
			{
				if(ValidateData())
				{
					int i = bo.UpdateAll(dt);
					dt.AcceptChanges();
					MessageBox.Show(clsResources.GetMessage("messages.save.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch(Exception ex)
			{
                MessageBox.Show(clsResources.GetMessage("messages.save.fail") + "\r\nDetail: " + ex.Message, clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Check whether data is valid
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public bool ValidateData()
		{
			if(dt == null || dt.Rows.Count == 0)
				return true;

			bool valid = true;
			foreach(DataRow row in dt.Rows)
			{
				if(row.RowState != DataRowState.Unchanged && !ValidateData(row))
					valid = false;
			}
			return valid;
		}

		/// <summary>
		/// Check wheather this DataRow is valid.
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		private bool ValidateData(DataRow row)
		{
            bool valid = true;
            if (row.RowState == DataRowState.Deleted)
                return true;

            row.ClearErrors();

            if (row["UROLE_ID"] == DBNull.Value || row["UROLE_ID"].ToString().Length == 0)
            {
                row.SetColumnError("UROLE_ID", clsResources.GetMessage("errors.required", colURoleID.HeaderText));
                valid = false;
            }

            if (row["ROLE_NAME"] == DBNull.Value || row["ROLE_NAME"].ToString().Length == 0)
            {
                row.SetColumnError("ROLE_NAME", clsResources.GetMessage("errors.required", colRoleName.HeaderText));
                valid = false;
            }

			return valid;
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
			dt.DefaultView.AllowNew = false;
			dt.DefaultView.AllowDelete = false;

			m_manager = (CurrencyManager)this.BindingContext[dt];

			grd.DataSource = dt;
		}

		/// <summary>
		/// Search Role user URoleID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			dt.Clear();
			dt = bo.Search(dt, txtURoleID.Text.Trim());
		}

		/// <summary>
		/// Get the current DataRow
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public DataRow GetCurrentRow()
		{
			DataRowView rview = (DataRowView)m_manager.Current;
			return rview.Row;
		}

		/// <summary>
		/// Handle events when click on btnAdd, btnDelete, btnSave, btnCancel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void EventListener(object sender, EventArgs e)
		{
			if(m_manager == null)
				return;

			if(sender == btnAdd)
			{
                dt.DefaultView.AllowNew = true;
                m_manager.AddNew();
                grd.Focus();
                dt.DefaultView.AllowNew = false;
                return;
			}

			if(m_manager.Position < 0)
				return;

            m_manager.EndCurrentEdit();

			
			if(sender == btnDelete)
			{
				if(m_manager.Position == -1)
					return;

				if(MessageBox.Show(
					clsResources.GetMessage("warnings.delete", ""), 
					clsResources.GetMessage("warnings.general"), 
					MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question) == DialogResult.Yes
					)
				{
					dt.DefaultView.AllowDelete = true;
					int i = m_manager.Position;
					m_manager.Position=0;
					m_manager.RemoveAt(i);
					dt.DefaultView.AllowDelete = false;
					try
					{
						if(i<m_manager.Count)
							m_manager.Position=i;
						else
							m_manager.Position=i-1;
					}
					catch{}
				}
			}
			if(sender == btnCancel)
			{
				dt.RejectChanges();
				DataRow[]rows = dt.GetErrors();
				foreach(DataRow row in rows)
					row.ClearErrors();
			}
			else if(sender == btnSave)
			{
				UpdateAll(dt);
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
		/// Set Policy for the selected Role
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void btnPolicy_Click(object sender, System.EventArgs e)
		{
			if(m_manager == null || m_manager.Position < 0)
				return;
			DataRowView rview = (DataRowView)m_manager.Current;
			DataRow row = rview.Row;
			if(row.RowState == DataRowState.Added)
			{
				MessageBox.Show(clsResources.GetMessage("warnings.policy"), clsResources.GetMessage("warnings.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string uRoleID = row["UROLE_ID"].ToString();
			frmAutPolicy frm = new frmAutPolicy(uRoleID);
			clsFormManager.ShowMDIChild(frm, this);
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
		private void frmUserRole_Activated(object sender, System.EventArgs e)
		{
			txtURoleID.Focus();
		}

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
	}
}
