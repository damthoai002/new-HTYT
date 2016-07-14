using UKPI.Utils;
namespace UKPI.Presentation
{
    partial class frmBaoCaoBenhNhanNamNu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaoCaoBenhNhanNamNu));
            this.btnRunReport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblQuy = new System.Windows.Forms.Label();
            this.txtQuy = new System.Windows.Forms.TextBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.txtNam = new System.Windows.Forms.TextBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.grbThongTinTimKiem = new System.Windows.Forms.GroupBox();
            this.lblPhongKham = new System.Windows.Forms.Label();
            this.ckbBaoCaoTheoQuyNam = new System.Windows.Forms.CheckBox();
            this.cbbPhongKham = new System.Windows.Forms.ComboBox();
            this.ckbBaoCaoTheoNgay = new System.Windows.Forms.CheckBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.rvBaoCaoTTBHYT = new Microsoft.Reporting.WinForms.ReportViewer();
            this.grbThongTinTimKiem.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRunReport
            // 
            this.btnRunReport.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnRunReport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRunReport.Location = new System.Drawing.Point(12, 127);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new System.Drawing.Size(117, 23);
            this.btnRunReport.TabIndex = 78;
            this.btnRunReport.Text = "Chạy Báo Cáo";
            this.btnRunReport.UseVisualStyleBackColor = true;
            this.btnRunReport.Click += new System.EventHandler(this.btnRunReport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::UKPI.Properties.Resources.import;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExport.Location = new System.Drawing.Point(1061, 595);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 28);
            this.btnExport.TabIndex = 79;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // lblQuy
            // 
            this.lblQuy.AutoSize = true;
            this.lblQuy.Location = new System.Drawing.Point(26, 23);
            this.lblQuy.Name = "lblQuy";
            this.lblQuy.Size = new System.Drawing.Size(38, 13);
            this.lblQuy.TabIndex = 80;
            this.lblQuy.Text = "Tháng";
            // 
            // txtQuy
            // 
            this.txtQuy.Location = new System.Drawing.Point(66, 19);
            this.txtQuy.Name = "txtQuy";
            this.txtQuy.Size = new System.Drawing.Size(68, 20);
            this.txtQuy.TabIndex = 81;
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(32, 45);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(29, 13);
            this.lblNam.TabIndex = 82;
            this.lblNam.Text = "Năm";
            // 
            // txtNam
            // 
            this.txtNam.Location = new System.Drawing.Point(66, 42);
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(68, 20);
            this.txtNam.TabIndex = 83;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(15, 71);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(48, 13);
            this.lblTuNgay.TabIndex = 84;
            this.lblTuNgay.Text = "Từ Ngày";
            // 
            // grbThongTinTimKiem
            // 
            this.grbThongTinTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbThongTinTimKiem.Controls.Add(this.lblPhongKham);
            this.grbThongTinTimKiem.Controls.Add(this.ckbBaoCaoTheoQuyNam);
            this.grbThongTinTimKiem.Controls.Add(this.cbbPhongKham);
            this.grbThongTinTimKiem.Controls.Add(this.ckbBaoCaoTheoNgay);
            this.grbThongTinTimKiem.Controls.Add(this.dtpDenNgay);
            this.grbThongTinTimKiem.Controls.Add(this.lblDenNgay);
            this.grbThongTinTimKiem.Controls.Add(this.dtpTuNgay);
            this.grbThongTinTimKiem.Controls.Add(this.lblTuNgay);
            this.grbThongTinTimKiem.Controls.Add(this.txtNam);
            this.grbThongTinTimKiem.Controls.Add(this.btnRunReport);
            this.grbThongTinTimKiem.Controls.Add(this.lblNam);
            this.grbThongTinTimKiem.Controls.Add(this.txtQuy);
            this.grbThongTinTimKiem.Controls.Add(this.lblQuy);
            this.grbThongTinTimKiem.Location = new System.Drawing.Point(12, 12);
            this.grbThongTinTimKiem.Name = "grbThongTinTimKiem";
            this.grbThongTinTimKiem.Size = new System.Drawing.Size(1189, 161);
            this.grbThongTinTimKiem.TabIndex = 86;
            this.grbThongTinTimKiem.TabStop = false;
            this.grbThongTinTimKiem.Text = "Thông tin tìm kiếm";
            // 
            // lblPhongKham
            // 
            this.lblPhongKham.AutoSize = true;
            this.lblPhongKham.Location = new System.Drawing.Point(317, 20);
            this.lblPhongKham.Name = "lblPhongKham";
            this.lblPhongKham.Size = new System.Drawing.Size(68, 13);
            this.lblPhongKham.TabIndex = 88;
            this.lblPhongKham.Text = "Phòng Khám";
            // 
            // ckbBaoCaoTheoQuyNam
            // 
            this.ckbBaoCaoTheoQuyNam.AutoSize = true;
            this.ckbBaoCaoTheoQuyNam.Location = new System.Drawing.Point(136, 20);
            this.ckbBaoCaoTheoQuyNam.Name = "ckbBaoCaoTheoQuyNam";
            this.ckbBaoCaoTheoQuyNam.Size = new System.Drawing.Size(141, 17);
            this.ckbBaoCaoTheoQuyNam.TabIndex = 89;
            this.ckbBaoCaoTheoQuyNam.Text = "Báo cáo theo quý / năm";
            this.ckbBaoCaoTheoQuyNam.UseVisualStyleBackColor = true;
            this.ckbBaoCaoTheoQuyNam.CheckedChanged += new System.EventHandler(this.ckbBaoCaoTheoQuyNam_CheckedChanged);
            // 
            // cbbPhongKham
            // 
            this.cbbPhongKham.DisplayMember = "RoomName";
            this.cbbPhongKham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPhongKham.DropDownWidth = 130;
            this.cbbPhongKham.Enabled = false;
            this.cbbPhongKham.FormattingEnabled = true;
            this.cbbPhongKham.Location = new System.Drawing.Point(388, 17);
            this.cbbPhongKham.Name = "cbbPhongKham";
            this.cbbPhongKham.Size = new System.Drawing.Size(156, 21);
            this.cbbPhongKham.TabIndex = 87;
            this.cbbPhongKham.ValueMember = "RoomID";
            // 
            // ckbBaoCaoTheoNgay
            // 
            this.ckbBaoCaoTheoNgay.AutoSize = true;
            this.ckbBaoCaoTheoNgay.Location = new System.Drawing.Point(203, 69);
            this.ckbBaoCaoTheoNgay.Name = "ckbBaoCaoTheoNgay";
            this.ckbBaoCaoTheoNgay.Size = new System.Drawing.Size(116, 17);
            this.ckbBaoCaoTheoNgay.TabIndex = 88;
            this.ckbBaoCaoTheoNgay.Text = "Báo cáo theo ngày";
            this.ckbBaoCaoTheoNgay.UseVisualStyleBackColor = true;
            this.ckbBaoCaoTheoNgay.CheckedChanged += new System.EventHandler(this.ckbBaoCaoTheoNgay_CheckedChanged);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(66, 92);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(134, 20);
            this.dtpDenNgay.TabIndex = 87;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(9, 95);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(55, 13);
            this.lblDenNgay.TabIndex = 86;
            this.lblDenNgay.Text = "Đến Ngày";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(66, 68);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(134, 20);
            this.dtpTuNgay.TabIndex = 85;
            // 
            // rvBaoCaoTTBHYT
            // 
            this.rvBaoCaoTTBHYT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rvBaoCaoTTBHYT.LocalReport.ReportEmbeddedResource = "UKPI.Presentation.Reports.BaoCaoThuocTDTTBHYT.rdlc";
            this.rvBaoCaoTTBHYT.Location = new System.Drawing.Point(13, 180);
            this.rvBaoCaoTTBHYT.Name = "rvBaoCaoTTBHYT";
            this.rvBaoCaoTTBHYT.Size = new System.Drawing.Size(1188, 456);
            this.rvBaoCaoTTBHYT.TabIndex = 87;
            // 
            // frmBaoCaoBenhNhanNamNu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 648);
            this.Controls.Add(this.rvBaoCaoTTBHYT);
            this.Controls.Add(this.grbThongTinTimKiem);
            this.Controls.Add(this.btnExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBaoCaoBenhNhanNamNu";
            this.Text = "Báo cáo bệnh nhân nam nữ";
            this.Load += new System.EventHandler(this.frmBaoCaoBenhNhanNamNu_Load);
            this.grbThongTinTimKiem.ResumeLayout(false);
            this.grbThongTinTimKiem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRunReport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblQuy;
        private System.Windows.Forms.TextBox txtQuy;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.TextBox txtNam;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.GroupBox grbThongTinTimKiem;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.CheckBox ckbBaoCaoTheoNgay;
        private System.Windows.Forms.CheckBox ckbBaoCaoTheoQuyNam;
        private System.Windows.Forms.Label lblPhongKham;
        private System.Windows.Forms.ComboBox cbbPhongKham;
        private Microsoft.Reporting.WinForms.ReportViewer rvBaoCaoTTBHYT;
    }
}