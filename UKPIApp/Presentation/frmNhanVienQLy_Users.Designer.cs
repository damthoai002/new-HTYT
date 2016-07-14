using UKPI.Utils;
namespace UKPI.Presentation
{
    partial class FrmNVQL_Users
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNVQL_Users));
            this.grpStore = new System.Windows.Forms.GroupBox();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.lblLoaiNV = new System.Windows.Forms.Label();
            this.lblMaNVUnilever = new System.Windows.Forms.Label();
            this.lblFName = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.lblNvUser = new System.Windows.Forms.Label();
            this.lblNvCC = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.txtSysId = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtNameNVCC = new System.Windows.Forms.TextBox();
            this.btnTimNVCC = new System.Windows.Forms.Button();
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
            this.cboLoaiNhanVien = new System.Windows.Forms.ComboBox();
            this.dgdNhanVienQuanLy = new UKPI.Controls.DataGridView_RowNum();
            this.MaNhanVien1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNVUnilever1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNVCC = new UKPI.Controls.DataGridView_RowNum();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAddUser = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MaNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNVUnileverCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNoCc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdNhanVienQuanLy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNVCC)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStore
            // 
            this.grpStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStore.Controls.Add(this.cboLoaiNhanVien);
            this.grpStore.Controls.Add(this.lblCardNo);
            this.grpStore.Controls.Add(this.lblLoaiNV);
            this.grpStore.Controls.Add(this.lblMaNVUnilever);
            this.grpStore.Controls.Add(this.lblFName);
            this.grpStore.Controls.Add(this.lblLName);
            this.grpStore.Controls.Add(this.lblNvUser);
            this.grpStore.Controls.Add(this.lblNvCC);
            this.grpStore.Controls.Add(this.txtCardNo);
            this.grpStore.Controls.Add(this.txtSysId);
            this.grpStore.Controls.Add(this.txtFName);
            this.grpStore.Controls.Add(this.txtNameNVCC);
            this.grpStore.Controls.Add(this.btnTimNVCC);
            this.grpStore.Location = new System.Drawing.Point(4, 6);
            this.grpStore.Name = "grpStore";
            this.grpStore.Size = new System.Drawing.Size(1000, 136);
            this.grpStore.TabIndex = 0;
            this.grpStore.TabStop = false;
            this.grpStore.Text = "Thông tin chung";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Location = new System.Drawing.Point(255, 47);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(44, 13);
            this.lblCardNo.TabIndex = 42;
            this.lblCardNo.Text = "Mã Thẻ";
            // 
            // lblLoaiNV
            // 
            this.lblLoaiNV.AutoSize = true;
            this.lblLoaiNV.Location = new System.Drawing.Point(254, 21);
            this.lblLoaiNV.Name = "lblLoaiNV";
            this.lblLoaiNV.Size = new System.Drawing.Size(45, 13);
            this.lblLoaiNV.TabIndex = 42;
            this.lblLoaiNV.Text = "Loại NV";
            // 
            // lblMaNVUnilever
            // 
            this.lblMaNVUnilever.AutoSize = true;
            this.lblMaNVUnilever.Location = new System.Drawing.Point(22, 68);
            this.lblMaNVUnilever.Name = "lblMaNVUnilever";
            this.lblMaNVUnilever.Size = new System.Drawing.Size(82, 13);
            this.lblMaNVUnilever.TabIndex = 42;
            this.lblMaNVUnilever.Text = "Mã NV Unilever";
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Location = new System.Drawing.Point(82, 45);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(21, 13);
            this.lblFName.TabIndex = 42;
            this.lblFName.Text = "Họ";
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Location = new System.Drawing.Point(77, 22);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(26, 13);
            this.lblLName.TabIndex = 42;
            this.lblLName.Text = "Tên";
            // 
            // lblNvUser
            // 
            this.lblNvUser.AutoSize = true;
            this.lblNvUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNvUser.Location = new System.Drawing.Point(667, 115);
            this.lblNvUser.Name = "lblNvUser";
            this.lblNvUser.Size = new System.Drawing.Size(170, 17);
            this.lblNvUser.TabIndex = 41;
            this.lblNvUser.Text = "Nhân Viên Chấm Công";
            // 
            // lblNvCC
            // 
            this.lblNvCC.AutoSize = true;
            this.lblNvCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNvCC.Location = new System.Drawing.Point(4, 117);
            this.lblNvCC.Name = "lblNvCC";
            this.lblNvCC.Size = new System.Drawing.Size(170, 17);
            this.lblNvCC.TabIndex = 41;
            this.lblNvCC.Text = "Nhân Viên Chấm Công";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(301, 44);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(144, 20);
            this.txtCardNo.TabIndex = 34;
            // 
            // txtSysId
            // 
            this.txtSysId.Location = new System.Drawing.Point(106, 65);
            this.txtSysId.Name = "txtSysId";
            this.txtSysId.Size = new System.Drawing.Size(144, 20);
            this.txtSysId.TabIndex = 34;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(106, 42);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(144, 20);
            this.txtFName.TabIndex = 34;
            // 
            // txtNameNVCC
            // 
            this.txtNameNVCC.Location = new System.Drawing.Point(106, 19);
            this.txtNameNVCC.Name = "txtNameNVCC";
            this.txtNameNVCC.Size = new System.Drawing.Size(144, 20);
            this.txtNameNVCC.TabIndex = 34;
            // 
            // btnTimNVCC
            // 
            this.btnTimNVCC.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnTimNVCC.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnTimNVCC.Location = new System.Drawing.Point(258, 88);
            this.btnTimNVCC.Name = "btnTimNVCC";
            this.btnTimNVCC.Size = new System.Drawing.Size(90, 23);
            this.btnTimNVCC.TabIndex = 32;
            this.btnTimNVCC.Text = "Tìm Kiếm";
            this.btnTimNVCC.UseVisualStyleBackColor = true;
            this.btnTimNVCC.Click += new System.EventHandler(this.btnTimNVCC_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // erp
            // 
            this.erp.ContainerControl = this;
            // 
            // cboLoaiNhanVien
            // 
            this.cboLoaiNhanVien.FormattingEnabled = true;
            this.cboLoaiNhanVien.Location = new System.Drawing.Point(301, 18);
            this.cboLoaiNhanVien.Name = "cboLoaiNhanVien";
            this.cboLoaiNhanVien.Size = new System.Drawing.Size(144, 21);
            this.cboLoaiNhanVien.TabIndex = 43;
            // 
            // dgdNhanVienQuanLy
            // 
            this.dgdNhanVienQuanLy.AllowUserToAddRows = false;
            this.dgdNhanVienQuanLy.AllowUserToDeleteRows = false;
            this.dgdNhanVienQuanLy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdNhanVienQuanLy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgdNhanVienQuanLy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdNhanVienQuanLy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaNhanVien1,
            this.MaNVUnilever1,
            this.UserName,
            this.LNAME,
            this.FNAME,
            this.CardNo1,
            this.Level});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgdNhanVienQuanLy.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgdNhanVienQuanLy.Location = new System.Drawing.Point(674, 148);
            this.dgdNhanVienQuanLy.Name = "dgdNhanVienQuanLy";
            this.dgdNhanVienQuanLy.RowHeadersWidth = 39;
            this.dgdNhanVienQuanLy.Size = new System.Drawing.Size(660, 258);
            this.dgdNhanVienQuanLy.TabIndex = 3;
            // 
            // MaNhanVien1
            // 
            this.MaNhanVien1.DataPropertyName = "MaNhanVien";
            this.MaNhanVien1.HeaderText = "Mã Nhân Viên";
            this.MaNhanVien1.Name = "MaNhanVien1";
            this.MaNhanVien1.ReadOnly = true;
            this.MaNhanVien1.Visible = false;
            this.MaNhanVien1.Width = 140;
            // 
            // MaNVUnilever1
            // 
            this.MaNVUnilever1.DataPropertyName = "MaNVUnilever";
            this.MaNVUnilever1.HeaderText = "Mã NV Unilever";
            this.MaNVUnilever1.Name = "MaNVUnilever1";
            this.MaNVUnilever1.ReadOnly = true;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "USERNAME";
            this.UserName.HeaderText = "Tên Đăng Nhập";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // LNAME
            // 
            this.LNAME.DataPropertyName = "LNAME";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            this.LNAME.DefaultCellStyle = dataGridViewCellStyle2;
            this.LNAME.HeaderText = "Tên NV";
            this.LNAME.MaxInputLength = 20;
            this.LNAME.Name = "LNAME";
            this.LNAME.ReadOnly = true;
            this.LNAME.Width = 140;
            // 
            // FNAME
            // 
            this.FNAME.DataPropertyName = "FNAME";
            this.FNAME.HeaderText = "Họ NV";
            this.FNAME.Name = "FNAME";
            this.FNAME.ReadOnly = true;
            // 
            // CardNo1
            // 
            this.CardNo1.DataPropertyName = "CardNo";
            this.CardNo1.HeaderText = "Mã Thẻ";
            this.CardNo1.Name = "CardNo1";
            this.CardNo1.ReadOnly = true;
            // 
            // Level
            // 
            this.Level.DataPropertyName = "LevelQuanLy";
            this.Level.HeaderText = "Cấp Bật";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            // 
            // dgvNVCC
            // 
            this.dgvNVCC.AllowUserToAddRows = false;
            this.dgvNVCC.AllowUserToDeleteRows = false;
            this.dgvNVCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNVCC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvNVCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNVCC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.btnAddUser,
            this.MaNhanVien,
            this.MaNVUnileverCC,
            this.Ten,
            this.Ho,
            this.CardNoCc});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNVCC.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNVCC.Location = new System.Drawing.Point(4, 148);
            this.dgvNVCC.Name = "dgvNVCC";
            this.dgvNVCC.RowHeadersWidth = 39;
            this.dgvNVCC.Size = new System.Drawing.Size(640, 258);
            this.dgvNVCC.TabIndex = 2;
            this.dgvNVCC.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNVCC_CellClick);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Check";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Visible = false;
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // btnAddUser
            // 
            this.btnAddUser.DataPropertyName = "tao";
            this.btnAddUser.HeaderText = "Tạo Người Dùng";
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Text = "Tạo";
            this.btnAddUser.ToolTipText = "Tạo ";
            this.btnAddUser.Width = 120;
            // 
            // MaNhanVien
            // 
            this.MaNhanVien.DataPropertyName = "MaNhanVien";
            this.MaNhanVien.HeaderText = "Mã Nhân Viên";
            this.MaNhanVien.Name = "MaNhanVien";
            this.MaNhanVien.ReadOnly = true;
            this.MaNhanVien.Visible = false;
            this.MaNhanVien.Width = 140;
            // 
            // MaNVUnileverCC
            // 
            this.MaNVUnileverCC.DataPropertyName = "MaNVUnilever";
            this.MaNVUnileverCC.HeaderText = "Mã NV Unilever";
            this.MaNVUnileverCC.Name = "MaNVUnileverCC";
            this.MaNVUnileverCC.ReadOnly = true;
            // 
            // Ten
            // 
            this.Ten.DataPropertyName = "Ten";
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            this.Ten.DefaultCellStyle = dataGridViewCellStyle5;
            this.Ten.HeaderText = "Tên NV";
            this.Ten.MaxInputLength = 20;
            this.Ten.Name = "Ten";
            this.Ten.ReadOnly = true;
            this.Ten.Width = 140;
            // 
            // Ho
            // 
            this.Ho.DataPropertyName = "HoNV";
            this.Ho.HeaderText = "Họ NV";
            this.Ho.Name = "Ho";
            this.Ho.ReadOnly = true;
            // 
            // CardNoCc
            // 
            this.CardNoCc.DataPropertyName = "CardNo";
            this.CardNoCc.HeaderText = "Mã Thẻ";
            this.CardNoCc.Name = "CardNoCc";
            this.CardNoCc.ReadOnly = true;
            // 
            // FrmNVQL_Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 414);
            this.Controls.Add(this.dgdNhanVienQuanLy);
            this.Controls.Add(this.dgvNVCC);
            this.Controls.Add(this.grpStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmNVQL_Users";
            this.Text = "Tạo Người Dùng Quản Lý";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditStore_FormClosing);
            this.Load += new System.EventHandler(this.frmEditStore_Load);
            this.grpStore.ResumeLayout(false);
            this.grpStore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdNhanVienQuanLy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNVCC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStore;
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
        private System.Windows.Forms.Button btnTimNVCC;
        private System.Windows.Forms.TextBox txtNameNVCC;
        private Controls.DataGridView_RowNum dgvNVCC;
        private System.Windows.Forms.Label lblNvCC;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label lblLoaiNV;
        private System.Windows.Forms.Label lblMaNVUnilever;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.TextBox txtSysId;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.ErrorProvider erp;
        private Controls.DataGridView_RowNum dgdNhanVienQuanLy;
        private System.Windows.Forms.Label lblNvUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhanVien1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNVUnilever1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn btnAddUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNVUnileverCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ho;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNoCc;
        private System.Windows.Forms.ComboBox cboLoaiNhanVien;
    }
}