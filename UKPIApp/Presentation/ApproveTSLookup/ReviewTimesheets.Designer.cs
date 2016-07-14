namespace UKPI.Presentation.ApproveTSLookup
{
    partial class ReviewTimesheets
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
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.grdStores = new UKPI.Controls.DataGridView_RowNum();
            this.lblListTimesheet = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTuan = new System.Windows.Forms.Label();
            this.TenTruongNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNVUnilever = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tuần = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ngày = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thuTrongTuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOutsource = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsOT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.L0XacNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L1XacNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L2XacNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L3XacNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.L4XacNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).BeginInit();
            this.SuspendLayout();
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // grdStores
            // 
            this.grdStores.AllowUserToAddRows = false;
            this.grdStores.AllowUserToDeleteRows = false;
            this.grdStores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdStores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenTruongNhom,
            this.MaNVUnilever,
            this.TenNhanVien,
            this.Nhom,
            this.Tuần,
            this.Ngày,
            this.Ca,
            this.thuTrongTuan,
            this.IsOutsource,
            this.IsOT,
            this.L0XacNhan,
            this.L1XacNhan,
            this.L2XacNhan,
            this.L3XacNhan,
            this.L4XacNhan});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdStores.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdStores.Location = new System.Drawing.Point(12, 68);
            this.grdStores.Name = "grdStores";
            this.grdStores.RowHeadersWidth = 39;
            this.grdStores.Size = new System.Drawing.Size(760, 282);
            this.grdStores.TabIndex = 2;
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
            this.lblListTimesheet.Size = new System.Drawing.Size(213, 19);
            this.lblListTimesheet.TabIndex = 3;
            this.lblListTimesheet.Text = "DANH SÁCH LỊCH LÀM VIỆC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lịch Làm Việc Cho Tuần: ";
            // 
            // lblTuan
            // 
            this.lblTuan.AutoSize = true;
            this.lblTuan.Location = new System.Drawing.Point(158, 37);
            this.lblTuan.Name = "lblTuan";
            this.lblTuan.Size = new System.Drawing.Size(13, 13);
            this.lblTuan.TabIndex = 4;
            this.lblTuan.Text = "1";
            // 
            // TenTruongNhom
            // 
            this.TenTruongNhom.DataPropertyName = "TruongNhom-TenNgan";
            this.TenTruongNhom.HeaderText = "Trưởng Nhóm";
            this.TenTruongNhom.Name = "TenTruongNhom";
            this.TenTruongNhom.Width = 140;
            // 
            // MaNVUnilever
            // 
            this.MaNVUnilever.DataPropertyName = "MaNVUnilever";
            this.MaNVUnilever.HeaderText = "Mã NV Unilever";
            this.MaNVUnilever.Name = "MaNVUnilever";
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
            this.Nhom.DataPropertyName = "Nhom-TenNgan";
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
            // Ca
            // 
            this.Ca.DataPropertyName = "Ca-DienGiai";
            this.Ca.HeaderText = "Ca";
            this.Ca.Name = "Ca";
            // 
            // thuTrongTuan
            // 
            this.thuTrongTuan.DataPropertyName = "NgayTrongTuan";
            this.thuTrongTuan.HeaderText = "Thứ Trong Tuần";
            this.thuTrongTuan.Name = "thuTrongTuan";
            this.thuTrongTuan.Width = 70;
            // 
            // IsOutsource
            // 
            this.IsOutsource.DataPropertyName = "IsOutSource";
            this.IsOutsource.HeaderText = "IsOutsource";
            this.IsOutsource.Name = "IsOutsource";
            this.IsOutsource.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsOutsource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsOutsource.Width = 80;
            // 
            // IsOT
            // 
            this.IsOT.DataPropertyName = "CoDangKyOT";
            this.IsOT.HeaderText = "IsOT";
            this.IsOT.Name = "IsOT";
            this.IsOT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsOT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsOT.Width = 50;
            // 
            // L0XacNhan
            // 
            this.L0XacNhan.DataPropertyName = "L0XacNhan-TenNgan";
            this.L0XacNhan.HeaderText = "L0 Xác Nhận";
            this.L0XacNhan.Name = "L0XacNhan";
            this.L0XacNhan.Visible = false;
            // 
            // L1XacNhan
            // 
            this.L1XacNhan.DataPropertyName = "L1XacNhan-TenNgan";
            this.L1XacNhan.HeaderText = "L1 Xác Nhận";
            this.L1XacNhan.Name = "L1XacNhan";
            // 
            // L2XacNhan
            // 
            this.L2XacNhan.DataPropertyName = "L2XacNhan-TenNgan";
            this.L2XacNhan.HeaderText = "L2 Xác Nhận";
            this.L2XacNhan.Name = "L2XacNhan";
            // 
            // L3XacNhan
            // 
            this.L3XacNhan.DataPropertyName = "L3XacNhan-TenNgan";
            this.L3XacNhan.HeaderText = "L3 Xác Nhận";
            this.L3XacNhan.Name = "L3XacNhan";
            // 
            // L4XacNhan
            // 
            this.L4XacNhan.DataPropertyName = "L4XacNhan-TenNgan";
            this.L4XacNhan.HeaderText = "L4 Xác Nhận";
            this.L4XacNhan.Name = "L4XacNhan";
            this.L4XacNhan.Visible = false;
            // 
            // ReviewTimesheets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 362);
            this.Controls.Add(this.lblTuan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblListTimesheet);
            this.Controls.Add(this.grdStores);
            this.Name = "ReviewTimesheets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReviewTimesheets";
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider ep;
        private Controls.DataGridView_RowNum grdStores;
        private System.Windows.Forms.Label lblListTimesheet;
        private System.Windows.Forms.Label lblTuan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTruongNhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNVUnilever;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tuần;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngày;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ca;
        private System.Windows.Forms.DataGridViewTextBoxColumn thuTrongTuan;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOutsource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn L0XacNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn L1XacNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn L2XacNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn L3XacNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn L4XacNhan;
    }
}