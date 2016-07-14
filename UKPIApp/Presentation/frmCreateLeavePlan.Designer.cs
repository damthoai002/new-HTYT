using UKPI.Utils;
namespace UKPI.Presentation
{
    partial class FrmCreateLeavePlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateLeavePlan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpStore = new System.Windows.Forms.GroupBox();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.cboLyDo2 = new System.Windows.Forms.ComboBox();
            this.cboLyDo1 = new System.Windows.Forms.ComboBox();
            this.dtpToHour = new System.Windows.Forms.DateTimePicker();
            this.dtpToDay = new System.Windows.Forms.DateTimePicker();
            this.dtpFromHour = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDay = new System.Windows.Forms.DateTimePicker();
            this.lblLeaveToDay = new System.Windows.Forms.Label();
            this.txtDienGiai = new System.Windows.Forms.TextBox();
            this.txtTruongNhom = new System.Windows.Forms.TextBox();
            this.lblTruongNhom = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblLyDo2 = new System.Windows.Forms.Label();
            this.lblDienGiai = new System.Windows.Forms.Label();
            this.lblLyDo1 = new System.Windows.Forms.Label();
            this.lblLeaveFromDay = new System.Windows.Forms.Label();
            this.lblTeamLead = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUseDateSearch = new System.Windows.Forms.CheckBox();
            this.lblNgayNghi = new System.Windows.Forms.Label();
            this.lblTenNhanVien = new System.Windows.Forms.Label();
            this.txtTenNhanVien = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpNgayNghi = new System.Windows.Forms.DateTimePicker();
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.grdNghiPhep = new UKPI.Controls.DataGridView_RowNum();
            this.MaGiaoDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNVNghi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNVDuyet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TuNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DenNgay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LyDo1ChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LyDo2ChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayDuyet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DienGiai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStore.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNghiPhep)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStore
            // 
            this.grpStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStore.Controls.Add(this.cboNhanVien);
            this.grpStore.Controls.Add(this.cboLyDo2);
            this.grpStore.Controls.Add(this.cboLyDo1);
            this.grpStore.Controls.Add(this.dtpToHour);
            this.grpStore.Controls.Add(this.dtpToDay);
            this.grpStore.Controls.Add(this.dtpFromHour);
            this.grpStore.Controls.Add(this.dtpFromDay);
            this.grpStore.Controls.Add(this.lblLeaveToDay);
            this.grpStore.Controls.Add(this.txtDienGiai);
            this.grpStore.Controls.Add(this.txtTruongNhom);
            this.grpStore.Controls.Add(this.lblTruongNhom);
            this.grpStore.Controls.Add(this.btnAddNew);
            this.grpStore.Controls.Add(this.lblNhanVien);
            this.grpStore.Controls.Add(this.lblLyDo2);
            this.grpStore.Controls.Add(this.lblDienGiai);
            this.grpStore.Controls.Add(this.lblLyDo1);
            this.grpStore.Controls.Add(this.lblLeaveFromDay);
            this.grpStore.Controls.Add(this.lblTeamLead);
            this.grpStore.Location = new System.Drawing.Point(4, 6);
            this.grpStore.Name = "grpStore";
            this.grpStore.Size = new System.Drawing.Size(1000, 151);
            this.grpStore.TabIndex = 0;
            this.grpStore.TabStop = false;
            this.grpStore.Text = "Thông Tin Đăng Ky Phép";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(367, 16);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(174, 21);
            this.cboNhanVien.TabIndex = 36;
            // 
            // cboLyDo2
            // 
            this.cboLyDo2.FormattingEnabled = true;
            this.cboLyDo2.Location = new System.Drawing.Point(367, 77);
            this.cboLyDo2.Name = "cboLyDo2";
            this.cboLyDo2.Size = new System.Drawing.Size(174, 21);
            this.cboLyDo2.TabIndex = 36;
            this.cboLyDo2.Visible = false;
            // 
            // cboLyDo1
            // 
            this.cboLyDo1.FormattingEnabled = true;
            this.cboLyDo1.Location = new System.Drawing.Point(114, 77);
            this.cboLyDo1.Name = "cboLyDo1";
            this.cboLyDo1.Size = new System.Drawing.Size(174, 21);
            this.cboLyDo1.TabIndex = 36;
            // 
            // dtpToHour
            // 
            this.dtpToHour.Location = new System.Drawing.Point(696, 46);
            this.dtpToHour.Name = "dtpToHour";
            this.dtpToHour.Size = new System.Drawing.Size(75, 20);
            this.dtpToHour.TabIndex = 35;
            // 
            // dtpToDay
            // 
            this.dtpToDay.Location = new System.Drawing.Point(367, 46);
            this.dtpToDay.Name = "dtpToDay";
            this.dtpToDay.Size = new System.Drawing.Size(174, 20);
            this.dtpToDay.TabIndex = 34;
            // 
            // dtpFromHour
            // 
            this.dtpFromHour.Location = new System.Drawing.Point(615, 46);
            this.dtpFromHour.Name = "dtpFromHour";
            this.dtpFromHour.Size = new System.Drawing.Size(75, 20);
            this.dtpFromHour.TabIndex = 35;
            // 
            // dtpFromDay
            // 
            this.dtpFromDay.Location = new System.Drawing.Point(114, 46);
            this.dtpFromDay.Name = "dtpFromDay";
            this.dtpFromDay.Size = new System.Drawing.Size(174, 20);
            this.dtpFromDay.TabIndex = 34;
            this.dtpFromDay.ValueChanged += new System.EventHandler(this.dtpFromDay_ValueChanged);
            // 
            // lblLeaveToDay
            // 
            this.lblLeaveToDay.AutoSize = true;
            this.lblLeaveToDay.Location = new System.Drawing.Point(310, 49);
            this.lblLeaveToDay.Name = "lblLeaveToDay";
            this.lblLeaveToDay.Size = new System.Drawing.Size(55, 13);
            this.lblLeaveToDay.TabIndex = 33;
            this.lblLeaveToDay.Text = "Đến Ngay";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(114, 104);
            this.txtDienGiai.MaxLength = 250;
            this.txtDienGiai.Multiline = true;
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(429, 40);
            this.txtDienGiai.TabIndex = 31;
            // 
            // txtTruongNhom
            // 
            this.txtTruongNhom.Enabled = false;
            this.txtTruongNhom.Location = new System.Drawing.Point(114, 17);
            this.txtTruongNhom.Name = "txtTruongNhom";
            this.txtTruongNhom.Size = new System.Drawing.Size(174, 20);
            this.txtTruongNhom.TabIndex = 31;
            // 
            // lblTruongNhom
            // 
            this.lblTruongNhom.AutoSize = true;
            this.lblTruongNhom.Location = new System.Drawing.Point(41, 20);
            this.lblTruongNhom.Name = "lblTruongNhom";
            this.lblTruongNhom.Size = new System.Drawing.Size(72, 13);
            this.lblTruongNhom.TabIndex = 26;
            this.lblTruongNhom.Text = "Truong Nhóm";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddNew.Location = new System.Drawing.Point(555, 121);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(86, 23);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.Text = " Lưu";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(308, 19);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(57, 13);
            this.lblNhanVien.TabIndex = 14;
            this.lblNhanVien.Text = "Nhân Viên";
            // 
            // lblLyDo2
            // 
            this.lblLyDo2.AutoSize = true;
            this.lblLyDo2.Location = new System.Drawing.Point(320, 81);
            this.lblLyDo2.Name = "lblLyDo2";
            this.lblLyDo2.Size = new System.Drawing.Size(44, 13);
            this.lblLyDo2.TabIndex = 4;
            this.lblLyDo2.Text = "Lý Do 2";
            this.lblLyDo2.Visible = false;
            // 
            // lblDienGiai
            // 
            this.lblDienGiai.AutoSize = true;
            this.lblDienGiai.Location = new System.Drawing.Point(61, 107);
            this.lblDienGiai.Name = "lblDienGiai";
            this.lblDienGiai.Size = new System.Drawing.Size(50, 13);
            this.lblDienGiai.TabIndex = 4;
            this.lblDienGiai.Text = "Diễn Giải";
            // 
            // lblLyDo1
            // 
            this.lblLyDo1.AutoSize = true;
            this.lblLyDo1.Location = new System.Drawing.Point(67, 80);
            this.lblLyDo1.Name = "lblLyDo1";
            this.lblLyDo1.Size = new System.Drawing.Size(44, 13);
            this.lblLyDo1.TabIndex = 4;
            this.lblLyDo1.Text = "Lý Do 1";
            // 
            // lblLeaveFromDay
            // 
            this.lblLeaveFromDay.AutoSize = true;
            this.lblLeaveFromDay.Location = new System.Drawing.Point(10, 49);
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
            this.groupBox1.Controls.Add(this.chkUseDateSearch);
            this.groupBox1.Controls.Add(this.lblNgayNghi);
            this.groupBox1.Controls.Add(this.lblTenNhanVien);
            this.groupBox1.Controls.Add(this.txtTenNhanVien);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.dtpNgayNghi);
            this.groupBox1.Location = new System.Drawing.Point(4, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Tìm Kiếm";
            // 
            // chkUseDateSearch
            // 
            this.chkUseDateSearch.AutoSize = true;
            this.chkUseDateSearch.Location = new System.Drawing.Point(595, 17);
            this.chkUseDateSearch.Name = "chkUseDateSearch";
            this.chkUseDateSearch.Size = new System.Drawing.Size(15, 14);
            this.chkUseDateSearch.TabIndex = 35;
            this.chkUseDateSearch.UseVisualStyleBackColor = true;
            this.chkUseDateSearch.CheckedChanged += new System.EventHandler(this.chkUseDateSearch_CheckedChanged);
            // 
            // lblNgayNghi
            // 
            this.lblNgayNghi.AutoSize = true;
            this.lblNgayNghi.Location = new System.Drawing.Point(390, 17);
            this.lblNgayNghi.Name = "lblNgayNghi";
            this.lblNgayNghi.Size = new System.Drawing.Size(55, 13);
            this.lblNgayNghi.TabIndex = 4;
            this.lblNgayNghi.Text = "Ngay nghi";
            // 
            // lblTenNhanVien
            // 
            this.lblTenNhanVien.AutoSize = true;
            this.lblTenNhanVien.Location = new System.Drawing.Point(114, 16);
            this.lblTenNhanVien.Name = "lblTenNhanVien";
            this.lblTenNhanVien.Size = new System.Drawing.Size(79, 13);
            this.lblTenNhanVien.TabIndex = 4;
            this.lblTenNhanVien.Text = "Tên Nhân Viên";
            // 
            // txtTenNhanVien
            // 
            this.txtTenNhanVien.Location = new System.Drawing.Point(196, 13);
            this.txtTenNhanVien.Name = "txtTenNhanVien";
            this.txtTenNhanVien.Size = new System.Drawing.Size(174, 20);
            this.txtTenNhanVien.TabIndex = 31;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.Location = new System.Drawing.Point(653, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpNgayNghi
            // 
            this.dtpNgayNghi.Location = new System.Drawing.Point(451, 14);
            this.dtpNgayNghi.Name = "dtpNgayNghi";
            this.dtpNgayNghi.Size = new System.Drawing.Size(137, 20);
            this.dtpNgayNghi.TabIndex = 34;
            this.dtpNgayNghi.ValueChanged += new System.EventHandler(this.dtpFromDay_ValueChanged);
            // 
            // erp
            // 
            this.erp.ContainerControl = this;
            // 
            // grdNghiPhep
            // 
            this.grdNghiPhep.AllowUserToAddRows = false;
            this.grdNghiPhep.AllowUserToDeleteRows = false;
            this.grdNghiPhep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNghiPhep.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdNghiPhep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNghiPhep.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaGiaoDich,
            this.TenNVNghi,
            this.TenNVDuyet,
            this.TuNgay,
            this.DenNgay,
            this.LyDo1ChiTiet,
            this.LyDo2ChiTiet,
            this.NgayDuyet,
            this.DienGiai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNghiPhep.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdNghiPhep.Location = new System.Drawing.Point(4, 228);
            this.grdNghiPhep.Name = "grdNghiPhep";
            this.grdNghiPhep.ReadOnly = true;
            this.grdNghiPhep.RowHeadersWidth = 39;
            this.grdNghiPhep.Size = new System.Drawing.Size(1000, 183);
            this.grdNghiPhep.TabIndex = 1;
            // 
            // MaGiaoDich
            // 
            this.MaGiaoDich.DataPropertyName = "MaGiaoDich";
            this.MaGiaoDich.HeaderText = "Mã Giao Dịch";
            this.MaGiaoDich.Name = "MaGiaoDich";
            this.MaGiaoDich.ReadOnly = true;
            // 
            // TenNVNghi
            // 
            this.TenNVNghi.DataPropertyName = "TenNVNghi";
            this.TenNVNghi.HeaderText = "Nhân Viên";
            this.TenNVNghi.Name = "TenNVNghi";
            this.TenNVNghi.ReadOnly = true;
            // 
            // TenNVDuyet
            // 
            this.TenNVDuyet.DataPropertyName = "TenNVDuyet";
            this.TenNVDuyet.HeaderText = "Trưởng Nhóm";
            this.TenNVDuyet.Name = "TenNVDuyet";
            this.TenNVDuyet.ReadOnly = true;
            // 
            // TuNgay
            // 
            this.TuNgay.DataPropertyName = "TuNgay";
            this.TuNgay.HeaderText = "Từ Ngày";
            this.TuNgay.Name = "TuNgay";
            this.TuNgay.ReadOnly = true;
            // 
            // DenNgay
            // 
            this.DenNgay.DataPropertyName = "DenNgay";
            this.DenNgay.HeaderText = "Đến Ngày";
            this.DenNgay.Name = "DenNgay";
            this.DenNgay.ReadOnly = true;
            // 
            // LyDo1ChiTiet
            // 
            this.LyDo1ChiTiet.DataPropertyName = "LyDo1ChiTiet";
            this.LyDo1ChiTiet.HeaderText = "Lý Do 1";
            this.LyDo1ChiTiet.Name = "LyDo1ChiTiet";
            this.LyDo1ChiTiet.ReadOnly = true;
            // 
            // LyDo2ChiTiet
            // 
            this.LyDo2ChiTiet.DataPropertyName = "LyDo2ChiTiet";
            this.LyDo2ChiTiet.HeaderText = "Lý Do 2";
            this.LyDo2ChiTiet.Name = "LyDo2ChiTiet";
            this.LyDo2ChiTiet.ReadOnly = true;
            this.LyDo2ChiTiet.Visible = false;
            // 
            // NgayDuyet
            // 
            this.NgayDuyet.DataPropertyName = "NgayDuyet";
            this.NgayDuyet.HeaderText = "Ngày Duyệt";
            this.NgayDuyet.Name = "NgayDuyet";
            this.NgayDuyet.ReadOnly = true;
            // 
            // DienGiai
            // 
            this.DienGiai.DataPropertyName = "DienGiai";
            this.DienGiai.HeaderText = "Diễn giải";
            this.DienGiai.Name = "DienGiai";
            this.DienGiai.ReadOnly = true;
            // 
            // FrmCreateLeavePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 414);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdNghiPhep);
            this.Controls.Add(this.grpStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCreateLeavePlan";
            this.Text = "Quản lý ca làm việc";
            this.grpStore.ResumeLayout(false);
            this.grpStore.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNghiPhep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStore;
        private System.Windows.Forms.Label lblLeaveFromDay;
        private System.Windows.Forms.Label lblTeamLead;
        private UKPI.Controls.DataGridView_RowNum grdNghiPhep;
        private System.Windows.Forms.Label lblTruongNhom;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtTruongNhom;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblLeaveToDay;
        private System.Windows.Forms.DateTimePicker dtpToHour;
        private System.Windows.Forms.DateTimePicker dtpToDay;
        private System.Windows.Forms.DateTimePicker dtpFromHour;
        private System.Windows.Forms.DateTimePicker dtpFromDay;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.ComboBox cboLyDo2;
        private System.Windows.Forms.ComboBox cboLyDo1;
        private System.Windows.Forms.TextBox txtDienGiai;
        private System.Windows.Forms.Label lblLyDo2;
        private System.Windows.Forms.Label lblDienGiai;
        private System.Windows.Forms.Label lblLyDo1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTenNhanVien;
        private System.Windows.Forms.TextBox txtTenNhanVien;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaGiaoDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNVNghi;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNVDuyet;
        private System.Windows.Forms.DataGridViewTextBoxColumn TuNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn DenNgay;
        private System.Windows.Forms.DataGridViewTextBoxColumn LyDo1ChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn LyDo2ChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayDuyet;
        private System.Windows.Forms.DataGridViewTextBoxColumn DienGiai;
        private System.Windows.Forms.Label lblNgayNghi;
        private System.Windows.Forms.DateTimePicker dtpNgayNghi;
        private System.Windows.Forms.CheckBox chkUseDateSearch;
    }
}