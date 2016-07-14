using UKPI.Utils;
namespace UKPI.Presentation
{
    partial class FrmManageShiftForTeam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageShiftForTeam));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpStore = new System.Windows.Forms.GroupBox();
            this.lblDNCalamviec = new System.Windows.Forms.Label();
            this.lblDauDocTheRa = new System.Windows.Forms.Label();
            this.cboDauDocTheRa = new System.Windows.Forms.ComboBox();
            this.cboDauDocTheVao = new System.Windows.Forms.ComboBox();
            this.cboDenTuan = new System.Windows.Forms.ComboBox();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.lblToWeek = new System.Windows.Forms.Label();
            this.txtLoaiNhom = new System.Windows.Forms.TextBox();
            this.txtNhomId = new System.Windows.Forms.TextBox();
            this.txtOutsource = new System.Windows.Forms.TextBox();
            this.txtNhom = new System.Windows.Forms.TextBox();
            this.btnViewShiftForTeam = new System.Windows.Forms.Button();
            this.btnCreateShiftForTeam = new System.Windows.Forms.Button();
            this.lblNhom = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lblDauDocTheVao = new System.Windows.Forms.Label();
            this.lblOutsource = new System.Windows.Forms.Label();
            this.lblFromWeekVal = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblTeamLead = new System.Windows.Forms.Label();
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvDNCaLamViec = new UKPI.Controls.DataGridView_RowNum();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeekDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaLamViec = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grdTeam = new UKPI.Controls.DataGridView_RowNum();
            this.SysId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThuTrongTuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaCaHeThong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaCaLaViec = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PhanXuongName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhanXuongId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DauDocTheVaoId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DauDocTheRaId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Tuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayVao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOff_Caution = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDNCaLamViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTeam)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStore
            // 
            this.grpStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStore.Controls.Add(this.lblDNCalamviec);
            this.grpStore.Controls.Add(this.lblDauDocTheRa);
            this.grpStore.Controls.Add(this.cboDauDocTheRa);
            this.grpStore.Controls.Add(this.cboDauDocTheVao);
            this.grpStore.Controls.Add(this.cboDenTuan);
            this.grpStore.Controls.Add(this.cboThang);
            this.grpStore.Controls.Add(this.lblToWeek);
            this.grpStore.Controls.Add(this.txtLoaiNhom);
            this.grpStore.Controls.Add(this.txtNhomId);
            this.grpStore.Controls.Add(this.txtOutsource);
            this.grpStore.Controls.Add(this.txtNhom);
            this.grpStore.Controls.Add(this.btnViewShiftForTeam);
            this.grpStore.Controls.Add(this.btnCreateShiftForTeam);
            this.grpStore.Controls.Add(this.lblNhom);
            this.grpStore.Controls.Add(this.btnAddNew);
            this.grpStore.Controls.Add(this.lblDauDocTheVao);
            this.grpStore.Controls.Add(this.lblOutsource);
            this.grpStore.Controls.Add(this.lblFromWeekVal);
            this.grpStore.Controls.Add(this.lblMonth);
            this.grpStore.Controls.Add(this.lblTeamLead);
            this.grpStore.Location = new System.Drawing.Point(4, 6);
            this.grpStore.Name = "grpStore";
            this.grpStore.Size = new System.Drawing.Size(1000, 115);
            this.grpStore.TabIndex = 0;
            this.grpStore.TabStop = false;
            this.grpStore.Text = "Thông tin ca làm việc nhóm";
            // 
            // lblDNCalamviec
            // 
            this.lblDNCalamviec.AutoSize = true;
            this.lblDNCalamviec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNCalamviec.Location = new System.Drawing.Point(721, 88);
            this.lblDNCalamviec.Name = "lblDNCalamviec";
            this.lblDNCalamviec.Size = new System.Drawing.Size(206, 17);
            this.lblDNCalamviec.TabIndex = 37;
            this.lblDNCalamviec.Text = "Ca làm việc trong tuần mẫu";
            this.lblDNCalamviec.Visible = false;
            // 
            // lblDauDocTheRa
            // 
            this.lblDauDocTheRa.AutoSize = true;
            this.lblDauDocTheRa.Location = new System.Drawing.Point(506, 50);
            this.lblDauDocTheRa.Name = "lblDauDocTheRa";
            this.lblDauDocTheRa.Size = new System.Drawing.Size(89, 13);
            this.lblDauDocTheRa.TabIndex = 36;
            this.lblDauDocTheRa.Text = "Đầu Đọc Thẻ Ra";
            this.lblDauDocTheRa.Visible = false;
            // 
            // cboDauDocTheRa
            // 
            this.cboDauDocTheRa.FormattingEnabled = true;
            this.cboDauDocTheRa.Location = new System.Drawing.Point(597, 46);
            this.cboDauDocTheRa.Name = "cboDauDocTheRa";
            this.cboDauDocTheRa.Size = new System.Drawing.Size(121, 21);
            this.cboDauDocTheRa.TabIndex = 35;
            this.cboDauDocTheRa.Visible = false;
            // 
            // cboDauDocTheVao
            // 
            this.cboDauDocTheVao.FormattingEnabled = true;
            this.cboDauDocTheVao.Location = new System.Drawing.Point(376, 46);
            this.cboDauDocTheVao.Name = "cboDauDocTheVao";
            this.cboDauDocTheVao.Size = new System.Drawing.Size(121, 21);
            this.cboDauDocTheVao.TabIndex = 35;
            this.cboDauDocTheVao.Visible = false;
            // 
            // cboDenTuan
            // 
            this.cboDenTuan.FormattingEnabled = true;
            this.cboDenTuan.Location = new System.Drawing.Point(801, 41);
            this.cboDenTuan.Name = "cboDenTuan";
            this.cboDenTuan.Size = new System.Drawing.Size(57, 21);
            this.cboDenTuan.TabIndex = 34;
            this.cboDenTuan.Visible = false;
            // 
            // cboThang
            // 
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(101, 46);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(173, 21);
            this.cboThang.TabIndex = 34;
            // 
            // lblToWeek
            // 
            this.lblToWeek.AutoSize = true;
            this.lblToWeek.Location = new System.Drawing.Point(743, 45);
            this.lblToWeek.Name = "lblToWeek";
            this.lblToWeek.Size = new System.Drawing.Size(55, 13);
            this.lblToWeek.TabIndex = 33;
            this.lblToWeek.Text = "Đến Tuần";
            this.lblToWeek.Visible = false;
            // 
            // txtLoaiNhom
            // 
            this.txtLoaiNhom.Enabled = false;
            this.txtLoaiNhom.Location = new System.Drawing.Point(597, 17);
            this.txtLoaiNhom.Name = "txtLoaiNhom";
            this.txtLoaiNhom.Size = new System.Drawing.Size(42, 20);
            this.txtLoaiNhom.TabIndex = 31;
            this.txtLoaiNhom.Visible = false;
            // 
            // txtNhomId
            // 
            this.txtNhomId.Enabled = false;
            this.txtNhomId.Location = new System.Drawing.Point(526, 17);
            this.txtNhomId.Name = "txtNhomId";
            this.txtNhomId.Size = new System.Drawing.Size(42, 20);
            this.txtNhomId.TabIndex = 31;
            this.txtNhomId.Visible = false;
            // 
            // txtOutsource
            // 
            this.txtOutsource.Enabled = false;
            this.txtOutsource.Location = new System.Drawing.Point(351, 17);
            this.txtOutsource.Name = "txtOutsource";
            this.txtOutsource.Size = new System.Drawing.Size(121, 20);
            this.txtOutsource.TabIndex = 31;
            // 
            // txtNhom
            // 
            this.txtNhom.Enabled = false;
            this.txtNhom.Location = new System.Drawing.Point(101, 17);
            this.txtNhom.Name = "txtNhom";
            this.txtNhom.Size = new System.Drawing.Size(174, 20);
            this.txtNhom.TabIndex = 31;
            // 
            // btnViewShiftForTeam
            // 
            this.btnViewShiftForTeam.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnViewShiftForTeam.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnViewShiftForTeam.Location = new System.Drawing.Point(376, 79);
            this.btnViewShiftForTeam.Name = "btnViewShiftForTeam";
            this.btnViewShiftForTeam.Size = new System.Drawing.Size(185, 23);
            this.btnViewShiftForTeam.TabIndex = 25;
            this.btnViewShiftForTeam.Text = "Xem Ca Làm Việc Nhóm";
            this.btnViewShiftForTeam.UseVisualStyleBackColor = true;
            this.btnViewShiftForTeam.Click += new System.EventHandler(this.btnViewShiftForTeam_Click);
            // 
            // btnCreateShiftForTeam
            // 
            this.btnCreateShiftForTeam.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnCreateShiftForTeam.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreateShiftForTeam.Location = new System.Drawing.Point(93, 80);
            this.btnCreateShiftForTeam.Name = "btnCreateShiftForTeam";
            this.btnCreateShiftForTeam.Size = new System.Drawing.Size(185, 23);
            this.btnCreateShiftForTeam.TabIndex = 25;
            this.btnCreateShiftForTeam.Text = "Tạo Ca Làm Việc Cho Nhóm";
            this.btnCreateShiftForTeam.UseVisualStyleBackColor = true;
            this.btnCreateShiftForTeam.Click += new System.EventHandler(this.btnCreateShiftForTeam_Click);
            // 
            // lblNhom
            // 
            this.lblNhom.AutoSize = true;
            this.lblNhom.Location = new System.Drawing.Point(63, 20);
            this.lblNhom.Name = "lblNhom";
            this.lblNhom.Size = new System.Drawing.Size(35, 13);
            this.lblNhom.TabIndex = 26;
            this.lblNhom.Text = "Nhóm";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddNew.Location = new System.Drawing.Point(284, 80);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(86, 23);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.Text = " Lưu";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lblDauDocTheVao
            // 
            this.lblDauDocTheVao.AutoSize = true;
            this.lblDauDocTheVao.Location = new System.Drawing.Point(280, 49);
            this.lblDauDocTheVao.Name = "lblDauDocTheVao";
            this.lblDauDocTheVao.Size = new System.Drawing.Size(94, 13);
            this.lblDauDocTheVao.TabIndex = 14;
            this.lblDauDocTheVao.Text = "Đầu Đọc Thẻ Vào";
            this.lblDauDocTheVao.Visible = false;
            // 
            // lblOutsource
            // 
            this.lblOutsource.AutoSize = true;
            this.lblOutsource.Location = new System.Drawing.Point(292, 20);
            this.lblOutsource.Name = "lblOutsource";
            this.lblOutsource.Size = new System.Drawing.Size(56, 13);
            this.lblOutsource.TabIndex = 14;
            this.lblOutsource.Text = "Outsource";
            // 
            // lblFromWeekVal
            // 
            this.lblFromWeekVal.AutoSize = true;
            this.lblFromWeekVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromWeekVal.Location = new System.Drawing.Point(98, 49);
            this.lblFromWeekVal.Name = "lblFromWeekVal";
            this.lblFromWeekVal.Size = new System.Drawing.Size(55, 13);
            this.lblFromWeekVal.TabIndex = 4;
            this.lblFromWeekVal.Text = "Từ Tuần";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(61, 50);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(38, 13);
            this.lblMonth.TabIndex = 4;
            this.lblMonth.Text = "Tháng";
            // 
            // lblTeamLead
            // 
            this.lblTeamLead.AutoSize = true;
            this.lblTeamLead.Location = new System.Drawing.Point(318, 17);
            this.lblTeamLead.Name = "lblTeamLead";
            this.lblTeamLead.Size = new System.Drawing.Size(0, 13);
            this.lblTeamLead.TabIndex = 2;
            // 
            // erp
            // 
            this.erp.ContainerControl = this;
            // 
            // dgvDNCaLamViec
            // 
            this.dgvDNCaLamViec.AllowUserToAddRows = false;
            this.dgvDNCaLamViec.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDNCaLamViec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDNCaLamViec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDNCaLamViec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.WeekDay,
            this.dataGridViewTextBoxColumn2,
            this.CaLamViec});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDNCaLamViec.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDNCaLamViec.Location = new System.Drawing.Point(728, 127);
            this.dgvDNCaLamViec.Name = "dgvDNCaLamViec";
            this.dgvDNCaLamViec.RowHeadersWidth = 39;
            this.dgvDNCaLamViec.Size = new System.Drawing.Size(551, 284);
            this.dgvDNCaLamViec.TabIndex = 1;
            this.dgvDNCaLamViec.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SysId";
            this.dataGridViewTextBoxColumn1.HeaderText = "SysId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // WeekDay
            // 
            this.WeekDay.DataPropertyName = "ThuTrongTuan";
            this.WeekDay.HeaderText = "WeekDay";
            this.WeekDay.Name = "WeekDay";
            this.WeekDay.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ThuTrongTuanDetail";
            this.dataGridViewTextBoxColumn2.HeaderText = "Thứ Trong Tuần";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CaLamViec
            // 
            this.CaLamViec.DataPropertyName = "CalamViecId";
            this.CaLamViec.HeaderText = "Ca Làm Việc";
            this.CaLamViec.Name = "CaLamViec";
            this.CaLamViec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CaLamViec.Width = 160;
            // 
            // grdTeam
            // 
            this.grdTeam.AllowUserToAddRows = false;
            this.grdTeam.AllowUserToDeleteRows = false;
            this.grdTeam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTeam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdTeam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTeam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SysId,
            this.MaNhom,
            this.TenNhom,
            this.ThuTrongTuan,
            this.MaCaHeThong,
            this.MaCaLaViec,
            this.PhanXuongName,
            this.PhanXuongId,
            this.IsOT,
            this.DauDocTheVaoId,
            this.DauDocTheRaId,
            this.Tuan,
            this.NgayVao,
            this.IsOff,
            this.IsOff_Caution,
            this.Is_Active});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTeam.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdTeam.Location = new System.Drawing.Point(4, 127);
            this.grdTeam.Name = "grdTeam";
            this.grdTeam.RowHeadersWidth = 39;
            this.grdTeam.Size = new System.Drawing.Size(994, 284);
            this.grdTeam.TabIndex = 1;
            // 
            // SysId
            // 
            this.SysId.DataPropertyName = "SysId";
            this.SysId.HeaderText = "SysId";
            this.SysId.Name = "SysId";
            this.SysId.ReadOnly = true;
            this.SysId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SysId.Visible = false;
            // 
            // MaNhom
            // 
            this.MaNhom.DataPropertyName = "MaNhom";
            this.MaNhom.HeaderText = "MaNhom";
            this.MaNhom.Name = "MaNhom";
            this.MaNhom.ReadOnly = true;
            this.MaNhom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaNhom.Visible = false;
            // 
            // TenNhom
            // 
            this.TenNhom.DataPropertyName = "TenNhom";
            this.TenNhom.HeaderText = "Tên Nhóm";
            this.TenNhom.Name = "TenNhom";
            this.TenNhom.ReadOnly = true;
            this.TenNhom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ThuTrongTuan
            // 
            this.ThuTrongTuan.DataPropertyName = "ThuTrongTuan";
            this.ThuTrongTuan.HeaderText = "Thứ";
            this.ThuTrongTuan.Name = "ThuTrongTuan";
            this.ThuTrongTuan.ReadOnly = true;
            this.ThuTrongTuan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MaCaHeThong
            // 
            this.MaCaHeThong.DataPropertyName = "MaCaHeThong";
            this.MaCaHeThong.HeaderText = "CaHeThong";
            this.MaCaHeThong.Name = "MaCaHeThong";
            this.MaCaHeThong.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MaCaHeThong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaCaHeThong.Visible = false;
            // 
            // MaCaLaViec
            // 
            this.MaCaLaViec.DataPropertyName = "MaCaLaViec";
            this.MaCaLaViec.HeaderText = "Ca";
            this.MaCaLaViec.Name = "MaCaLaViec";
            this.MaCaLaViec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PhanXuongName
            // 
            this.PhanXuongName.DataPropertyName = "PhanXuongName";
            this.PhanXuongName.HeaderText = "Phân Xưởng";
            this.PhanXuongName.Name = "PhanXuongName";
            this.PhanXuongName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PhanXuongId
            // 
            this.PhanXuongId.DataPropertyName = "PhanXuongId";
            this.PhanXuongId.HeaderText = "PhanXuongId";
            this.PhanXuongId.Name = "PhanXuongId";
            this.PhanXuongId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PhanXuongId.Visible = false;
            // 
            // IsOT
            // 
            this.IsOT.DataPropertyName = "IsOT";
            this.IsOT.HeaderText = "IsOT";
            this.IsOT.Name = "IsOT";
            this.IsOT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DauDocTheVaoId
            // 
            this.DauDocTheVaoId.DataPropertyName = "DauDocTheVaoId";
            this.DauDocTheVaoId.HeaderText = "Đầu Đọc Thẻ Vào";
            this.DauDocTheVaoId.Name = "DauDocTheVaoId";
            this.DauDocTheVaoId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DauDocTheVaoId.Visible = false;
            // 
            // DauDocTheRaId
            // 
            this.DauDocTheRaId.DataPropertyName = "DauDocTheRaId";
            this.DauDocTheRaId.HeaderText = "Đầu Đọc Thẻ Ra";
            this.DauDocTheRaId.Name = "DauDocTheRaId";
            this.DauDocTheRaId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DauDocTheRaId.Visible = false;
            // 
            // Tuan
            // 
            this.Tuan.DataPropertyName = "Tuan";
            this.Tuan.HeaderText = "Tuần";
            this.Tuan.Name = "Tuan";
            this.Tuan.ReadOnly = true;
            this.Tuan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NgayVao
            // 
            this.NgayVao.DataPropertyName = "NgayVao";
            this.NgayVao.HeaderText = "Ngày";
            this.NgayVao.Name = "NgayVao";
            this.NgayVao.ReadOnly = true;
            this.NgayVao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsOff
            // 
            this.IsOff.DataPropertyName = "IsOff";
            this.IsOff.HeaderText = "IsOff";
            this.IsOff.Name = "IsOff";
            this.IsOff.ReadOnly = true;
            this.IsOff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IsOff.Visible = false;
            // 
            // IsOff_Caution
            // 
            this.IsOff_Caution.DataPropertyName = "IsOff_Caution";
            this.IsOff_Caution.HeaderText = "Ngày nghỉ";
            this.IsOff_Caution.Name = "IsOff_Caution";
            this.IsOff_Caution.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Is_Active
            // 
            this.Is_Active.DataPropertyName = "Is_Active";
            this.Is_Active.HeaderText = "IsActive";
            this.Is_Active.Name = "Is_Active";
            this.Is_Active.ReadOnly = true;
            this.Is_Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Is_Active.Visible = false;
            // 
            // FrmManageShiftForTeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 414);
            this.Controls.Add(this.dgvDNCaLamViec);
            this.Controls.Add(this.grdTeam);
            this.Controls.Add(this.grpStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmManageShiftForTeam";
            this.Text = "Quản lý ca làm việc";
            this.grpStore.ResumeLayout(false);
            this.grpStore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDNCaLamViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTeam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStore;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblTeamLead;
        private UKPI.Controls.DataGridView_RowNum grdTeam;



        private System.Windows.Forms.Button btnCreateShiftForTeam;
        private System.Windows.Forms.Label lblNhom;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtNhom;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.Label lblOutsource;
        private System.Windows.Forms.Label lblToWeek;
        private System.Windows.Forms.Label lblFromWeekVal;
        private System.Windows.Forms.TextBox txtOutsource;
        private System.Windows.Forms.TextBox txtNhomId;
        private System.Windows.Forms.Button btnViewShiftForTeam;
        private System.Windows.Forms.ComboBox cboDenTuan;
        private System.Windows.Forms.ComboBox cboThang;
        private Controls.DataGridView_RowNum dgvDNCaLamViec;
        private System.Windows.Forms.ComboBox cboDauDocTheRa;
        private System.Windows.Forms.ComboBox cboDauDocTheVao;
        private System.Windows.Forms.Label lblDauDocTheVao;
        private System.Windows.Forms.Label lblDauDocTheRa;
        private System.Windows.Forms.Label lblDNCalamviec;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeekDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn CaLamViec;
        private System.Windows.Forms.TextBox txtLoaiNhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThuTrongTuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaCaHeThong;
        private System.Windows.Forms.DataGridViewComboBoxColumn MaCaLaViec;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhanXuongName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhanXuongId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOT;
        private System.Windows.Forms.DataGridViewComboBoxColumn DauDocTheVaoId;
        private System.Windows.Forms.DataGridViewComboBoxColumn DauDocTheRaId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayVao;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOff;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOff_Caution;
        private System.Windows.Forms.DataGridViewTextBoxColumn Is_Active;
    }
}