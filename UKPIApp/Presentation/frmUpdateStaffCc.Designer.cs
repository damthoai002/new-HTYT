using UKPI.Utils;
namespace UKPI.Presentation
{
    partial class FrmUpdateStaffCc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateStaffCc));
            this.grpStore = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.grdNhanVienProWatch = new UKPI.Controls.DataGridView_RowNum();
            this.ID_Prowatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BADGE_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BADGE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISSUE_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXPIRE_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TSTAMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Outsource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNVUnilever = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNhanVienProWatch)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStore
            // 
            this.grpStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStore.Controls.Add(this.btnSave);
            this.grpStore.Location = new System.Drawing.Point(4, 6);
            this.grpStore.Name = "grpStore";
            this.grpStore.Size = new System.Drawing.Size(1000, 54);
            this.grpStore.TabIndex = 0;
            this.grpStore.TabStop = false;
            this.grpStore.Text = "Thông tin";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::UKPI.Properties.Resources.save_as3;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSave.Location = new System.Drawing.Point(28, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 23);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Cập nhật NV chấm công";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "REGION_NAME";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Region";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DISTRIBUTOR_CODE";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn2.HeaderText = "Distributors ID";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 14;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Press F3 to search";
            this.dataGridViewTextBoxColumn2.Width = 101;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CUST_NAME";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.HeaderText = "Distributor Name";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.ToolTipText = "Press F3 to search";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "STORE_CODE";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn4.HeaderText = "Store ID";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 14;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "STORE_NAME";
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn5.HeaderText = "Store Name";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "STORE_ADDRESS";
            this.dataGridViewTextBoxColumn6.HeaderText = "Store Address";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "UPDATED_ADDRESS";
            this.dataGridViewTextBoxColumn7.HeaderText = "Updated Address";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 130;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "TOWN_NAME";
            this.dataGridViewTextBoxColumn8.HeaderText = "Town";
            this.dataGridViewTextBoxColumn8.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 150;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "URBAN";
            this.dataGridViewTextBoxColumn9.HeaderText = "Urban";
            this.dataGridViewTextBoxColumn9.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            this.dataGridViewTextBoxColumn9.Width = 150;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "PROVINCE_NAME";
            this.dataGridViewTextBoxColumn10.HeaderText = "Province";
            this.dataGridViewTextBoxColumn10.MaxInputLength = 250;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "OUTLET_TYPE_NAME";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn11.HeaderText = "Outlet Classification";
            this.dataGridViewTextBoxColumn11.MaxInputLength = 20;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 150;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "LOCATION";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N3";
            dataGridViewCellStyle7.NullValue = null;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn12.HeaderText = "Location";
            this.dataGridViewTextBoxColumn12.MaxInputLength = 9;
            this.dataGridViewTextBoxColumn12.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn12.Width = 73;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "STAR_CLUB";
            this.dataGridViewTextBoxColumn13.HeaderText = "Star Club";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn13.Visible = false;
            this.dataGridViewTextBoxColumn13.Width = 2;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "TURNOVER";
            this.dataGridViewTextBoxColumn14.HeaderText = "Turnover";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 150;
            // 
            // erp
            // 
            this.erp.ContainerControl = this;
            // 
            // grdNhanVienProWatch
            // 
            this.grdNhanVienProWatch.AllowUserToAddRows = false;
            this.grdNhanVienProWatch.AllowUserToDeleteRows = false;
            this.grdNhanVienProWatch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNhanVienProWatch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdNhanVienProWatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNhanVienProWatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_Prowatch,
            this.LNAME,
            this.FNAME,
            this.MI,
            this.BADGE_STATUS,
            this.BADGE_TYPE,
            this.ISSUE_DATE,
            this.EXPIRE_DATE,
            this.TSTAMP,
            this.Outsource,
            this.Email,
            this.GioiTinh,
            this.MaNVUnilever,
            this.SysId,
            this.Check});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNhanVienProWatch.DefaultCellStyle = dataGridViewCellStyle10;
            this.grdNhanVienProWatch.Location = new System.Drawing.Point(6, 66);
            this.grdNhanVienProWatch.Name = "grdNhanVienProWatch";
            this.grdNhanVienProWatch.RowHeadersWidth = 39;
            this.grdNhanVienProWatch.Size = new System.Drawing.Size(998, 336);
            this.grdNhanVienProWatch.TabIndex = 1;
            this.grdNhanVienProWatch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdNhanVienProWatch_CellClick);
            // 
            // ID_Prowatch
            // 
            this.ID_Prowatch.DataPropertyName = "ID_Prowatch";
            this.ID_Prowatch.HeaderText = "ID_Prowatch";
            this.ID_Prowatch.Name = "ID_Prowatch";
            this.ID_Prowatch.ReadOnly = true;
            this.ID_Prowatch.Visible = false;
            // 
            // LNAME
            // 
            this.LNAME.DataPropertyName = "LNAME";
            this.LNAME.HeaderText = "LNAME";
            this.LNAME.Name = "LNAME";
            this.LNAME.ReadOnly = true;
            // 
            // FNAME
            // 
            this.FNAME.DataPropertyName = "FNAME";
            this.FNAME.HeaderText = "FNAME";
            this.FNAME.Name = "FNAME";
            this.FNAME.ReadOnly = true;
            // 
            // MI
            // 
            this.MI.DataPropertyName = "MI";
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Info;
            this.MI.DefaultCellStyle = dataGridViewCellStyle9;
            this.MI.HeaderText = "MI";
            this.MI.MaxInputLength = 20;
            this.MI.Name = "MI";
            this.MI.ReadOnly = true;
            // 
            // BADGE_STATUS
            // 
            this.BADGE_STATUS.DataPropertyName = "BADGE_STATUS_DESC";
            this.BADGE_STATUS.HeaderText = "BADGE_STATUS";
            this.BADGE_STATUS.Name = "BADGE_STATUS";
            this.BADGE_STATUS.ReadOnly = true;
            // 
            // BADGE_TYPE
            // 
            this.BADGE_TYPE.DataPropertyName = "BADGE_TYPE_DESC";
            this.BADGE_TYPE.HeaderText = "BADGE_TYPE";
            this.BADGE_TYPE.Name = "BADGE_TYPE";
            this.BADGE_TYPE.ReadOnly = true;
            // 
            // ISSUE_DATE
            // 
            this.ISSUE_DATE.DataPropertyName = "ISSUE_DATE";
            this.ISSUE_DATE.HeaderText = "ISSUE_DATE";
            this.ISSUE_DATE.Name = "ISSUE_DATE";
            this.ISSUE_DATE.ReadOnly = true;
            // 
            // EXPIRE_DATE
            // 
            this.EXPIRE_DATE.DataPropertyName = "EXPIRE_DATE";
            this.EXPIRE_DATE.HeaderText = "EXPIRE_DATE";
            this.EXPIRE_DATE.Name = "EXPIRE_DATE";
            this.EXPIRE_DATE.ReadOnly = true;
            // 
            // TSTAMP
            // 
            this.TSTAMP.DataPropertyName = "TSTAMP";
            this.TSTAMP.HeaderText = "TSTAMP";
            this.TSTAMP.Name = "TSTAMP";
            this.TSTAMP.ReadOnly = true;
            // 
            // Outsource
            // 
            this.Outsource.DataPropertyName = "Outsource";
            this.Outsource.HeaderText = "Outsource";
            this.Outsource.Name = "Outsource";
            this.Outsource.ReadOnly = true;
            this.Outsource.Visible = false;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Visible = false;
            // 
            // GioiTinh
            // 
            this.GioiTinh.DataPropertyName = "GioiTinh";
            this.GioiTinh.HeaderText = "GioiTinh";
            this.GioiTinh.Name = "GioiTinh";
            this.GioiTinh.ReadOnly = true;
            this.GioiTinh.Visible = false;
            // 
            // MaNVUnilever
            // 
            this.MaNVUnilever.DataPropertyName = "MaNVUnilever";
            this.MaNVUnilever.HeaderText = "MaNVUnilever";
            this.MaNVUnilever.Name = "MaNVUnilever";
            this.MaNVUnilever.ReadOnly = true;
            this.MaNVUnilever.Visible = false;
            // 
            // SysId
            // 
            this.SysId.DataPropertyName = "SysId";
            this.SysId.HeaderText = "SysId";
            this.SysId.Name = "SysId";
            this.SysId.ReadOnly = true;
            this.SysId.Visible = false;
            // 
            // Check
            // 
            this.Check.DataPropertyName = "Check";
            this.Check.HeaderText = "Check";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Visible = false;
            // 
            // FrmUpdateStaffCc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 414);
            this.Controls.Add(this.grdNhanVienProWatch);
            this.Controls.Add(this.grpStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUpdateStaffCc";
            this.Text = "Lấy NV mới từ prowatch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditStore_FormClosing);
            this.Load += new System.EventHandler(this.frmEditStore_Load);
            this.grpStore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNhanVienProWatch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStore;
        private UKPI.Controls.DataGridView_RowNum grdNhanVienProWatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Prowatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn LNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MI;
        private System.Windows.Forms.DataGridViewTextBoxColumn BADGE_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn BADGE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISSUE_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPIRE_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TSTAMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Outsource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNVUnilever;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check;
    }
}