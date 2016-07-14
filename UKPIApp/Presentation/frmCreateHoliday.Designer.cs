using UKPI.Utils;
namespace UKPI.Presentation
{
    partial class FrmCreateHoliday
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateHoliday));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpStore = new System.Windows.Forms.GroupBox();
            this.txtNam = new System.Windows.Forms.TextBox();
            this.cboLoaiNgayNghi = new System.Windows.Forms.ComboBox();
            this.dtpToDay = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDay = new System.Windows.Forms.DateTimePicker();
            this.lblLeaveToDay = new System.Windows.Forms.Label();
            this.txtDienGiai = new System.Windows.Forms.TextBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.lblLoaiNgayNghi = new System.Windows.Forms.Label();
            this.btnUnActive = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lblDienGiai = new System.Windows.Forms.Label();
            this.lblLeaveFromDay = new System.Windows.Forms.Label();
            this.lblTeamLead = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNamSearch = new System.Windows.Forms.Label();
            this.txtNamSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.grdNgayNghi = new UKPI.Controls.DataGridView_RowNum();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SysId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNgayNghi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayBatDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayKetThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStore.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNgayNghi)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStore
            // 
            this.grpStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStore.Controls.Add(this.txtNam);
            this.grpStore.Controls.Add(this.cboLoaiNgayNghi);
            this.grpStore.Controls.Add(this.dtpToDay);
            this.grpStore.Controls.Add(this.dtpFromDay);
            this.grpStore.Controls.Add(this.lblLeaveToDay);
            this.grpStore.Controls.Add(this.txtDienGiai);
            this.grpStore.Controls.Add(this.lblNam);
            this.grpStore.Controls.Add(this.lblLoaiNgayNghi);
            this.grpStore.Controls.Add(this.btnUnActive);
            this.grpStore.Controls.Add(this.btnAddNew);
            this.grpStore.Controls.Add(this.lblDienGiai);
            this.grpStore.Controls.Add(this.lblLeaveFromDay);
            this.grpStore.Controls.Add(this.lblTeamLead);
            this.grpStore.Location = new System.Drawing.Point(4, 6);
            this.grpStore.Name = "grpStore";
            this.grpStore.Size = new System.Drawing.Size(1000, 126);
            this.grpStore.TabIndex = 0;
            this.grpStore.TabStop = false;
            this.grpStore.Text = "Tạo ngày nghĩ trong năm";
            // 
            // txtNam
            // 
            this.txtNam.Location = new System.Drawing.Point(341, 19);
            this.txtNam.MaxLength = 4;
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(128, 20);
            this.txtNam.TabIndex = 36;
            // 
            // cboLoaiNgayNghi
            // 
            this.cboLoaiNgayNghi.FormattingEnabled = true;
            this.cboLoaiNgayNghi.Location = new System.Drawing.Point(114, 19);
            this.cboLoaiNgayNghi.Name = "cboLoaiNgayNghi";
            this.cboLoaiNgayNghi.Size = new System.Drawing.Size(128, 21);
            this.cboLoaiNgayNghi.TabIndex = 35;
            this.cboLoaiNgayNghi.SelectedIndexChanged += new System.EventHandler(this.cboLoaiNgayNghi_SelectedIndexChanged);
            // 
            // dtpToDay
            // 
            this.dtpToDay.Location = new System.Drawing.Point(341, 46);
            this.dtpToDay.Name = "dtpToDay";
            this.dtpToDay.Size = new System.Drawing.Size(128, 20);
            this.dtpToDay.TabIndex = 34;
            // 
            // dtpFromDay
            // 
            this.dtpFromDay.Location = new System.Drawing.Point(114, 46);
            this.dtpFromDay.Name = "dtpFromDay";
            this.dtpFromDay.Size = new System.Drawing.Size(128, 20);
            this.dtpFromDay.TabIndex = 34;
            this.dtpFromDay.ValueChanged += new System.EventHandler(this.dtpFromDay_ValueChanged);
            // 
            // lblLeaveToDay
            // 
            this.lblLeaveToDay.AutoSize = true;
            this.lblLeaveToDay.Location = new System.Drawing.Point(283, 50);
            this.lblLeaveToDay.Name = "lblLeaveToDay";
            this.lblLeaveToDay.Size = new System.Drawing.Size(55, 13);
            this.lblLeaveToDay.TabIndex = 33;
            this.lblLeaveToDay.Text = "Đến Ngay";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(114, 77);
            this.txtDienGiai.MaxLength = 250;
            this.txtDienGiai.Multiline = true;
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(355, 40);
            this.txtDienGiai.TabIndex = 31;
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(308, 22);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(29, 13);
            this.lblNam.TabIndex = 26;
            this.lblNam.Text = "Năm";
            // 
            // lblLoaiNgayNghi
            // 
            this.lblLoaiNgayNghi.AutoSize = true;
            this.lblLoaiNgayNghi.Location = new System.Drawing.Point(29, 22);
            this.lblLoaiNgayNghi.Name = "lblLoaiNgayNghi";
            this.lblLoaiNgayNghi.Size = new System.Drawing.Size(83, 13);
            this.lblLoaiNgayNghi.TabIndex = 26;
            this.lblLoaiNgayNghi.Text = "Loại Ngày Nghĩ";
            // 
            // btnUnActive
            // 
            this.btnUnActive.Image = ((System.Drawing.Image)(resources.GetObject("btnUnActive.Image")));
            this.btnUnActive.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUnActive.Location = new System.Drawing.Point(582, 94);
            this.btnUnActive.Name = "btnUnActive";
            this.btnUnActive.Size = new System.Drawing.Size(140, 23);
            this.btnUnActive.TabIndex = 11;
            this.btnUnActive.Text = "Ngưng sử dụng";
            this.btnUnActive.UseVisualStyleBackColor = true;
            this.btnUnActive.Click += new System.EventHandler(this.btnUnActive_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddNew.Location = new System.Drawing.Point(482, 94);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(86, 23);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.Text = " Lưu";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lblDienGiai
            // 
            this.lblDienGiai.AutoSize = true;
            this.lblDienGiai.Location = new System.Drawing.Point(61, 79);
            this.lblDienGiai.Name = "lblDienGiai";
            this.lblDienGiai.Size = new System.Drawing.Size(50, 13);
            this.lblDienGiai.TabIndex = 4;
            this.lblDienGiai.Text = "Diễn Giải";
            // 
            // lblLeaveFromDay
            // 
            this.lblLeaveFromDay.AutoSize = true;
            this.lblLeaveFromDay.Location = new System.Drawing.Point(9, 48);
            this.lblLeaveFromDay.Name = "lblLeaveFromDay";
            this.lblLeaveFromDay.Size = new System.Drawing.Size(101, 13);
            this.lblLeaveFromDay.TabIndex = 4;
            this.lblLeaveFromDay.Text = "Chọn Nghỉ Từ Ngày";
            // 
            // lblTeamLead
            // 
            this.lblTeamLead.AutoSize = true;
            this.lblTeamLead.Location = new System.Drawing.Point(318, 17);
            this.lblTeamLead.Name = "lblTeamLead";
            this.lblTeamLead.Size = new System.Drawing.Size(0, 13);
            this.lblTeamLead.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblNamSearch);
            this.groupBox1.Controls.Add(this.txtNamSearch);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(4, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Tìm Kiếm";
            // 
            // lblNamSearch
            // 
            this.lblNamSearch.AutoSize = true;
            this.lblNamSearch.Location = new System.Drawing.Point(164, 16);
            this.lblNamSearch.Name = "lblNamSearch";
            this.lblNamSearch.Size = new System.Drawing.Size(29, 13);
            this.lblNamSearch.TabIndex = 4;
            this.lblNamSearch.Text = "Năm";
            // 
            // txtNamSearch
            // 
            this.txtNamSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtNamSearch.Location = new System.Drawing.Point(196, 13);
            this.txtNamSearch.MaxLength = 4;
            this.txtNamSearch.Name = "txtNamSearch";
            this.txtNamSearch.Size = new System.Drawing.Size(174, 20);
            this.txtNamSearch.TabIndex = 31;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.Location = new System.Drawing.Point(384, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // erp
            // 
            this.erp.ContainerControl = this;
            // 
            // grdNgayNghi
            // 
            this.grdNgayNghi.AllowUserToAddRows = false;
            this.grdNgayNghi.AllowUserToDeleteRows = false;
            this.grdNgayNghi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNgayNghi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdNgayNghi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNgayNghi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Is_Active});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNgayNghi.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdNgayNghi.Location = new System.Drawing.Point(4, 185);
            this.grdNgayNghi.Name = "grdNgayNghi";
            this.grdNgayNghi.RowHeadersWidth = 39;
            this.grdNgayNghi.Size = new System.Drawing.Size(1000, 226);
            this.grdNgayNghi.TabIndex = 1;
            // 
            // Check
            // 
            this.Check.DataPropertyName = "Check";
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SysId";
            this.dataGridViewTextBoxColumn1.HeaderText = "SysId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "MaNgayNghi";
            this.dataGridViewTextBoxColumn2.HeaderText = "Mã Ngày Nghỉ";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NgayBatDau";
            this.dataGridViewTextBoxColumn3.HeaderText = "Ngày Bắt Đầu";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "NgayKetThuc";
            this.dataGridViewTextBoxColumn4.HeaderText = "Ngày Kết Thúc";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "MoTa";
            this.dataGridViewTextBoxColumn5.HeaderText = "Mô Tả";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 250;
            // 
            // Is_Active
            // 
            this.Is_Active.DataPropertyName = "Is_Active";
            this.Is_Active.HeaderText = "Sử Dụng";
            this.Is_Active.Name = "Is_Active";
            this.Is_Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Is_Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Is_Active.Visible = false;
            this.Is_Active.Width = 80;
            // 
            // SysId
            // 
            this.SysId.DataPropertyName = "SysId";
            this.SysId.HeaderText = "SysId";
            this.SysId.Name = "SysId";
            this.SysId.Visible = false;
            // 
            // MaNgayNghi
            // 
            this.MaNgayNghi.DataPropertyName = "MaNgayNghi";
            this.MaNgayNghi.HeaderText = "Mã Ngày Nghỉ";
            this.MaNgayNghi.Name = "MaNgayNghi";
            // 
            // NgayBatDau
            // 
            this.NgayBatDau.DataPropertyName = "NgayBatDau";
            this.NgayBatDau.HeaderText = "Ngày Bắt Đầu";
            this.NgayBatDau.Name = "NgayBatDau";
            this.NgayBatDau.Width = 120;
            // 
            // NgayKetThuc
            // 
            this.NgayKetThuc.DataPropertyName = "NgayKetThuc";
            this.NgayKetThuc.HeaderText = "Ngày Kết Thúc";
            this.NgayKetThuc.Name = "NgayKetThuc";
            this.NgayKetThuc.Width = 120;
            // 
            // MoTa
            // 
            this.MoTa.DataPropertyName = "MoTa";
            this.MoTa.HeaderText = "Mô Tả";
            this.MoTa.Name = "MoTa";
            this.MoTa.Width = 250;
            // 
            // FrmCreateHoliday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 414);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdNgayNghi);
            this.Controls.Add(this.grpStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCreateHoliday";
            this.Text = "Quản lý ca làm việc";
            this.grpStore.ResumeLayout(false);
            this.grpStore.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNgayNghi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStore;
        private System.Windows.Forms.Label lblLeaveFromDay;
        private System.Windows.Forms.Label lblTeamLead;
        private System.Windows.Forms.Label lblLoaiNgayNghi;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DateTimePicker dtpFromDay;
        private System.Windows.Forms.TextBox txtDienGiai;
        private System.Windows.Forms.Label lblDienGiai;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNamSearch;
        private System.Windows.Forms.TextBox txtNamSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtNam;
        private System.Windows.Forms.ComboBox cboLoaiNgayNghi;
        private System.Windows.Forms.DateTimePicker dtpToDay;
        private System.Windows.Forms.Label lblLeaveToDay;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNgayNghi;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayBatDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayKetThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoTa;
        private System.Windows.Forms.Button btnUnActive;
        private Controls.DataGridView_RowNum grdNgayNghi;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Is_Active;
    }
}