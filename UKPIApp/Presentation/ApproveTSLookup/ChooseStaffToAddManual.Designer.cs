namespace UKPI.Presentation.ApproveTSLookup
{
    partial class ChooseStaffToAddManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseStaffToAddManual));
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.grdNhanVien = new UKPI.Controls.DataGridView_RowNum();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TenTruongNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tuần = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ngày = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOutsource = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblListTimesheet = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTuan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOutsource = new System.Windows.Forms.Label();
            this.lblNhom = new System.Windows.Forms.Label();
            this.lblNgayLamViec = new System.Windows.Forms.Label();
            this.lblTruongNhom = new System.Windows.Forms.Label();
            this.txtTenNhanVien = new System.Windows.Forms.TextBox();
            this.txtHoNhanVien = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // grdNhanVien
            // 
            this.grdNhanVien.AllowUserToAddRows = false;
            this.grdNhanVien.AllowUserToDeleteRows = false;
            this.grdNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNhanVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.TenTruongNhom,
            this.MaNhanVien,
            this.TenNhanVien,
            this.Nhom,
            this.Tuần,
            this.Ngày,
            this.IsOutsource});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNhanVien.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdNhanVien.Location = new System.Drawing.Point(12, 164);
            this.grdNhanVien.Name = "grdNhanVien";
            this.grdNhanVien.RowHeadersWidth = 39;
            this.grdNhanVien.Size = new System.Drawing.Size(760, 219);
            this.grdNhanVien.TabIndex = 2;
            this.grdNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdNhanVien_CellClick);
            // 
            // Check
            // 
            this.Check.DataPropertyName = "Check";
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.Width = 40;
            // 
            // TenTruongNhom
            // 
            this.TenTruongNhom.DataPropertyName = "TenTruongNhom";
            this.TenTruongNhom.HeaderText = "Trưởng Nhóm";
            this.TenTruongNhom.Name = "TenTruongNhom";
            this.TenTruongNhom.Width = 140;
            // 
            // MaNhanVien
            // 
            this.MaNhanVien.DataPropertyName = "NhanVien-Id";
            this.MaNhanVien.HeaderText = "Mã NV";
            this.MaNhanVien.Name = "MaNhanVien";
            // 
            // TenNhanVien
            // 
            this.TenNhanVien.DataPropertyName = "NhanVien-Ten";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            this.TenNhanVien.DefaultCellStyle = dataGridViewCellStyle2;
            this.TenNhanVien.HeaderText = "Tên Nhân Viên";
            this.TenNhanVien.MaxInputLength = 20;
            this.TenNhanVien.Name = "TenNhanVien";
            this.TenNhanVien.ReadOnly = true;
            this.TenNhanVien.Width = 140;
            // 
            // Nhom
            // 
            this.Nhom.DataPropertyName = "TenNhom";
            this.Nhom.HeaderText = "Nhóm";
            this.Nhom.Name = "Nhom";
            this.Nhom.Width = 50;
            // 
            // Tuần
            // 
            this.Tuần.DataPropertyName = "Tuan";
            this.Tuần.HeaderText = "Tuần";
            this.Tuần.Name = "Tuần";
            this.Tuần.Width = 50;
            // 
            // Ngày
            // 
            this.Ngày.DataPropertyName = "Ngay";
            this.Ngày.HeaderText = "Ngày";
            this.Ngày.Name = "Ngày";
            this.Ngày.Width = 80;
            // 
            // IsOutsource
            // 
            this.IsOutsource.DataPropertyName = "IsOutsource";
            this.IsOutsource.HeaderText = "IsOutsource";
            this.IsOutsource.Name = "IsOutsource";
            this.IsOutsource.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsOutsource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsOutsource.Width = 80;
            // 
            // lblListTimesheet
            // 
            this.lblListTimesheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListTimesheet.AutoSize = true;
            this.lblListTimesheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblListTimesheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListTimesheet.Location = new System.Drawing.Point(275, 9);
            this.lblListTimesheet.Name = "lblListTimesheet";
            this.lblListTimesheet.Size = new System.Drawing.Size(187, 19);
            this.lblListTimesheet.TabIndex = 3;
            this.lblListTimesheet.Text = "DANH SÁCH NHÂN VIÊN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tuần: ";
            // 
            // lblTuan
            // 
            this.lblTuan.AutoSize = true;
            this.lblTuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuan.Location = new System.Drawing.Point(107, 60);
            this.lblTuan.Name = "lblTuan";
            this.lblTuan.Size = new System.Drawing.Size(14, 13);
            this.lblTuan.TabIndex = 4;
            this.lblTuan.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Trưởng nhóm: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Họ Nhân Viên: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Nhóm: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ngày Làm Việc: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tên Nhân Viên: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(456, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Outsource: ";
            // 
            // lblOutsource
            // 
            this.lblOutsource.AutoSize = true;
            this.lblOutsource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutsource.Location = new System.Drawing.Point(524, 37);
            this.lblOutsource.Name = "lblOutsource";
            this.lblOutsource.Size = new System.Drawing.Size(28, 13);
            this.lblOutsource.TabIndex = 4;
            this.lblOutsource.Text = "Yes";
            // 
            // lblNhom
            // 
            this.lblNhom.AutoSize = true;
            this.lblNhom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhom.Location = new System.Drawing.Point(338, 37);
            this.lblNhom.Name = "lblNhom";
            this.lblNhom.Size = new System.Drawing.Size(28, 13);
            this.lblNhom.TabIndex = 4;
            this.lblNhom.Text = "Yes";
            // 
            // lblNgayLamViec
            // 
            this.lblNgayLamViec.AutoSize = true;
            this.lblNgayLamViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayLamViec.Location = new System.Drawing.Point(338, 62);
            this.lblNgayLamViec.Name = "lblNgayLamViec";
            this.lblNgayLamViec.Size = new System.Drawing.Size(28, 13);
            this.lblNgayLamViec.TabIndex = 4;
            this.lblNgayLamViec.Text = "Yes";
            // 
            // lblTruongNhom
            // 
            this.lblTruongNhom.AutoSize = true;
            this.lblTruongNhom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTruongNhom.Location = new System.Drawing.Point(104, 37);
            this.lblTruongNhom.Name = "lblTruongNhom";
            this.lblTruongNhom.Size = new System.Drawing.Size(28, 13);
            this.lblTruongNhom.TabIndex = 4;
            this.lblTruongNhom.Text = "Yes";
            // 
            // txtTenNhanVien
            // 
            this.txtTenNhanVien.Location = new System.Drawing.Point(113, 88);
            this.txtTenNhanVien.Name = "txtTenNhanVien";
            this.txtTenNhanVien.Size = new System.Drawing.Size(186, 20);
            this.txtTenNhanVien.TabIndex = 6;
            // 
            // txtHoNhanVien
            // 
            this.txtHoNhanVien.Location = new System.Drawing.Point(113, 116);
            this.txtHoNhanVien.Name = "txtHoNhanVien";
            this.txtHoNhanVien.Size = new System.Drawing.Size(186, 20);
            this.txtHoNhanVien.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.Location = new System.Drawing.Point(341, 114);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(108, 23);
            this.btnSearch.TabIndex = 44;
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.Image")));
            this.btnAddNew.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddNew.Location = new System.Drawing.Point(478, 114);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(89, 23);
            this.btnAddNew.TabIndex = 45;
            this.btnAddNew.Text = "Thêm";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // ChooseStaffToAddManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 395);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtHoNhanVien);
            this.Controls.Add(this.txtTenNhanVien);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTruongNhom);
            this.Controls.Add(this.lblNgayLamViec);
            this.Controls.Add(this.lblNhom);
            this.Controls.Add(this.lblOutsource);
            this.Controls.Add(this.lblTuan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblListTimesheet);
            this.Controls.Add(this.grdNhanVien);
            this.Name = "ChooseStaffToAddManual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn Nhân Viên Thêm Vào Lịch Chấm Công";
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNhanVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider ep;
        private Controls.DataGridView_RowNum grdNhanVien;
        private System.Windows.Forms.Label lblListTimesheet;
        private System.Windows.Forms.Label lblTuan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOutsource;
        private System.Windows.Forms.TextBox txtHoNhanVien;
        private System.Windows.Forms.TextBox txtTenNhanVien;
        private System.Windows.Forms.Label lblTruongNhom;
        private System.Windows.Forms.Label lblNgayLamViec;
        private System.Windows.Forms.Label lblNhom;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTruongNhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tuần;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngày;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOutsource;
    }
}