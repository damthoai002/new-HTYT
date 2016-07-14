using UKPI.Utils;
using UKPI.Controls;
namespace UKPI.Presentation
{
    partial class frmViewBenhNhanKhambenh
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewBenhNhanKhambenh));
            this.grpThongTinKhamBenh = new System.Windows.Forms.GroupBox();
            this.txtICD = new System.Windows.Forms.TextBox();
            this.txtBoPhan = new System.Windows.Forms.TextBox();
            this.txtGioiTinh = new System.Windows.Forms.TextBox();
            this.txtNhomBenh = new System.Windows.Forms.TextBox();
            this.txtKhuVuc = new System.Windows.Forms.TextBox();
            this.lblDonvi = new System.Windows.Forms.Label();
            this.txtTienKhamBenh = new System.Windows.Forms.TextBox();
            this.lblTienKham = new System.Windows.Forms.Label();
            this.txtDienGiaiICD = new System.Windows.Forms.TextBox();
            this.lblMaICD = new System.Windows.Forms.Label();
            this.txtSndn = new System.Windows.Forms.TextBox();
            this.txtChanDoan = new System.Windows.Forms.TextBox();
            this.lblSndn = new System.Windows.Forms.Label();
            this.lblChanDoan = new System.Windows.Forms.Label();
            this.lblNhomBenh = new System.Windows.Forms.Label();
            this.txtCongTy = new System.Windows.Forms.TextBox();
            this.txtNamSinh = new System.Windows.Forms.TextBox();
            this.txtMaBHYT = new System.Windows.Forms.TextBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.lblCongTy = new System.Windows.Forms.Label();
            this.lblBoPhan = new System.Windows.Forms.Label();
            this.lblNamSinh = new System.Windows.Forms.Label();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblMaBHYT = new System.Windows.Forms.Label();
            this.txtMaNhanVien = new System.Windows.Forms.TextBox();
            this.txtBenhNhan = new System.Windows.Forms.TextBox();
            this.lblMaNhanVien = new System.Windows.Forms.Label();
            this.lblBenhNhan = new System.Windows.Forms.Label();
            this.lblNgayKham = new System.Windows.Forms.Label();
            this.lblPhongKham = new System.Windows.Forms.Label();
            this.dtpNgayKham = new System.Windows.Forms.DateTimePicker();
            this.cbbPhongKham = new System.Windows.Forms.ComboBox();
            this.btnDong = new System.Windows.Forms.Button();
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
            this.btnIn = new System.Windows.Forms.Button();
            this.htytDataSet1 = new UKPI.HTYTHomebaseDataSet();
            this.lblTongTienBangChu = new System.Windows.Forms.Label();
            this.txtTongTienBangChu = new System.Windows.Forms.TextBox();
            this.txtTongTienBH = new System.Windows.Forms.TextBox();
            this.lblTongThanhTienBH = new System.Windows.Forms.Label();
            this.grdToaThuoc = new UKPI.Controls.DataGridView_RowNum();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThuocBH = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HamLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaTTBHYT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CachUong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CachUongChiTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTienTTBHYT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpThongTinKhamBenh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htytDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdToaThuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // grpThongTinKhamBenh
            // 
            this.grpThongTinKhamBenh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTinKhamBenh.Controls.Add(this.txtICD);
            this.grpThongTinKhamBenh.Controls.Add(this.txtBoPhan);
            this.grpThongTinKhamBenh.Controls.Add(this.txtGioiTinh);
            this.grpThongTinKhamBenh.Controls.Add(this.txtNhomBenh);
            this.grpThongTinKhamBenh.Controls.Add(this.txtKhuVuc);
            this.grpThongTinKhamBenh.Controls.Add(this.lblDonvi);
            this.grpThongTinKhamBenh.Controls.Add(this.txtTienKhamBenh);
            this.grpThongTinKhamBenh.Controls.Add(this.lblTienKham);
            this.grpThongTinKhamBenh.Controls.Add(this.txtDienGiaiICD);
            this.grpThongTinKhamBenh.Controls.Add(this.lblMaICD);
            this.grpThongTinKhamBenh.Controls.Add(this.txtSndn);
            this.grpThongTinKhamBenh.Controls.Add(this.txtChanDoan);
            this.grpThongTinKhamBenh.Controls.Add(this.lblSndn);
            this.grpThongTinKhamBenh.Controls.Add(this.lblChanDoan);
            this.grpThongTinKhamBenh.Controls.Add(this.lblNhomBenh);
            this.grpThongTinKhamBenh.Controls.Add(this.txtCongTy);
            this.grpThongTinKhamBenh.Controls.Add(this.txtNamSinh);
            this.grpThongTinKhamBenh.Controls.Add(this.txtMaBHYT);
            this.grpThongTinKhamBenh.Controls.Add(this.lblKhuVuc);
            this.grpThongTinKhamBenh.Controls.Add(this.lblCongTy);
            this.grpThongTinKhamBenh.Controls.Add(this.lblBoPhan);
            this.grpThongTinKhamBenh.Controls.Add(this.lblNamSinh);
            this.grpThongTinKhamBenh.Controls.Add(this.lblGioiTinh);
            this.grpThongTinKhamBenh.Controls.Add(this.lblMaBHYT);
            this.grpThongTinKhamBenh.Controls.Add(this.txtMaNhanVien);
            this.grpThongTinKhamBenh.Controls.Add(this.txtBenhNhan);
            this.grpThongTinKhamBenh.Controls.Add(this.lblMaNhanVien);
            this.grpThongTinKhamBenh.Controls.Add(this.lblBenhNhan);
            this.grpThongTinKhamBenh.Controls.Add(this.lblNgayKham);
            this.grpThongTinKhamBenh.Controls.Add(this.lblPhongKham);
            this.grpThongTinKhamBenh.Controls.Add(this.dtpNgayKham);
            this.grpThongTinKhamBenh.Controls.Add(this.cbbPhongKham);
            this.grpThongTinKhamBenh.Location = new System.Drawing.Point(4, 6);
            this.grpThongTinKhamBenh.Name = "grpThongTinKhamBenh";
            this.grpThongTinKhamBenh.Size = new System.Drawing.Size(1203, 181);
            this.grpThongTinKhamBenh.TabIndex = 23;
            this.grpThongTinKhamBenh.TabStop = false;
            this.grpThongTinKhamBenh.Text = "Thông tin khám bệnh";
            // 
            // txtICD
            // 
            this.txtICD.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtICD.Enabled = false;
            this.txtICD.Location = new System.Drawing.Point(751, 19);
            this.txtICD.Name = "txtICD";
            this.txtICD.Size = new System.Drawing.Size(446, 20);
            this.txtICD.TabIndex = 81;
            // 
            // txtBoPhan
            // 
            this.txtBoPhan.BackColor = System.Drawing.Color.White;
            this.txtBoPhan.Enabled = false;
            this.txtBoPhan.Location = new System.Drawing.Point(403, 100);
            this.txtBoPhan.Name = "txtBoPhan";
            this.txtBoPhan.Size = new System.Drawing.Size(156, 20);
            this.txtBoPhan.TabIndex = 80;
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.BackColor = System.Drawing.Color.White;
            this.txtGioiTinh.Enabled = false;
            this.txtGioiTinh.Location = new System.Drawing.Point(405, 47);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.Size = new System.Drawing.Size(156, 20);
            this.txtGioiTinh.TabIndex = 79;
            // 
            // txtNhomBenh
            // 
            this.txtNhomBenh.BackColor = System.Drawing.Color.White;
            this.txtNhomBenh.Enabled = false;
            this.txtNhomBenh.Location = new System.Drawing.Point(405, 21);
            this.txtNhomBenh.Name = "txtNhomBenh";
            this.txtNhomBenh.Size = new System.Drawing.Size(156, 20);
            this.txtNhomBenh.TabIndex = 78;
            // 
            // txtKhuVuc
            // 
            this.txtKhuVuc.BackColor = System.Drawing.Color.White;
            this.txtKhuVuc.Enabled = false;
            this.txtKhuVuc.Location = new System.Drawing.Point(101, 152);
            this.txtKhuVuc.Name = "txtKhuVuc";
            this.txtKhuVuc.Size = new System.Drawing.Size(156, 20);
            this.txtKhuVuc.TabIndex = 77;
            // 
            // lblDonvi
            // 
            this.lblDonvi.AutoSize = true;
            this.lblDonvi.Location = new System.Drawing.Point(512, 155);
            this.lblDonvi.Name = "lblDonvi";
            this.lblDonvi.Size = new System.Drawing.Size(38, 13);
            this.lblDonvi.TabIndex = 76;
            this.lblDonvi.Text = "(đồng)";
            // 
            // txtTienKhamBenh
            // 
            this.txtTienKhamBenh.Enabled = false;
            this.txtTienKhamBenh.Location = new System.Drawing.Point(403, 152);
            this.txtTienKhamBenh.Name = "txtTienKhamBenh";
            this.txtTienKhamBenh.ReadOnly = true;
            this.txtTienKhamBenh.Size = new System.Drawing.Size(103, 20);
            this.txtTienKhamBenh.TabIndex = 75;
            // 
            // lblTienKham
            // 
            this.lblTienKham.AutoSize = true;
            this.lblTienKham.Location = new System.Drawing.Point(285, 155);
            this.lblTienKham.Name = "lblTienKham";
            this.lblTienKham.Size = new System.Drawing.Size(115, 13);
            this.lblTienKham.TabIndex = 74;
            this.lblTienKham.Text = "Tiền khám bệnh/1 lượt";
            // 
            // txtDienGiaiICD
            // 
            this.txtDienGiaiICD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienGiaiICD.Enabled = false;
            this.txtDienGiaiICD.Location = new System.Drawing.Point(751, 44);
            this.txtDienGiaiICD.MaxLength = 250;
            this.txtDienGiaiICD.Multiline = true;
            this.txtDienGiaiICD.Name = "txtDienGiaiICD";
            this.txtDienGiaiICD.Size = new System.Drawing.Size(446, 66);
            this.txtDienGiaiICD.TabIndex = 14;
            // 
            // lblMaICD
            // 
            this.lblMaICD.AutoSize = true;
            this.lblMaICD.Location = new System.Drawing.Point(705, 22);
            this.lblMaICD.Name = "lblMaICD";
            this.lblMaICD.Size = new System.Drawing.Size(43, 13);
            this.lblMaICD.TabIndex = 72;
            this.lblMaICD.Text = "Mã ICD";
            // 
            // txtSndn
            // 
            this.txtSndn.Enabled = false;
            this.txtSndn.Location = new System.Drawing.Point(768, 195);
            this.txtSndn.Name = "txtSndn";
            this.txtSndn.Size = new System.Drawing.Size(103, 20);
            this.txtSndn.TabIndex = 66;
            this.txtSndn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSndn.Visible = false;
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Location = new System.Drawing.Point(751, 218);
            this.txtChanDoan.MaxLength = 250;
            this.txtChanDoan.Multiline = true;
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Size = new System.Drawing.Size(265, 25);
            this.txtChanDoan.TabIndex = 12;
            this.txtChanDoan.Visible = false;
            // 
            // lblSndn
            // 
            this.lblSndn.AutoSize = true;
            this.lblSndn.Location = new System.Drawing.Point(669, 198);
            this.lblSndn.Name = "lblSndn";
            this.lblSndn.Size = new System.Drawing.Size(97, 13);
            this.lblSndn.TabIndex = 60;
            this.lblSndn.Text = "Số ngày được nghỉ";
            this.lblSndn.Visible = false;
            // 
            // lblChanDoan
            // 
            this.lblChanDoan.AutoSize = true;
            this.lblChanDoan.Location = new System.Drawing.Point(688, 47);
            this.lblChanDoan.Name = "lblChanDoan";
            this.lblChanDoan.Size = new System.Drawing.Size(60, 13);
            this.lblChanDoan.TabIndex = 56;
            this.lblChanDoan.Text = "Chẩn đoán";
            // 
            // lblNhomBenh
            // 
            this.lblNhomBenh.AutoSize = true;
            this.lblNhomBenh.Location = new System.Drawing.Point(338, 24);
            this.lblNhomBenh.Name = "lblNhomBenh";
            this.lblNhomBenh.Size = new System.Drawing.Size(62, 13);
            this.lblNhomBenh.TabIndex = 55;
            this.lblNhomBenh.Text = "Nhóm bệnh";
            // 
            // txtCongTy
            // 
            this.txtCongTy.Enabled = false;
            this.txtCongTy.Location = new System.Drawing.Point(403, 126);
            this.txtCongTy.Name = "txtCongTy";
            this.txtCongTy.Size = new System.Drawing.Size(265, 20);
            this.txtCongTy.TabIndex = 9;
            // 
            // txtNamSinh
            // 
            this.txtNamSinh.Enabled = false;
            this.txtNamSinh.Location = new System.Drawing.Point(403, 74);
            this.txtNamSinh.Name = "txtNamSinh";
            this.txtNamSinh.Size = new System.Drawing.Size(156, 20);
            this.txtNamSinh.TabIndex = 7;
            // 
            // txtMaBHYT
            // 
            this.txtMaBHYT.BackColor = System.Drawing.Color.White;
            this.txtMaBHYT.Enabled = false;
            this.txtMaBHYT.Location = new System.Drawing.Point(101, 124);
            this.txtMaBHYT.Name = "txtMaBHYT";
            this.txtMaBHYT.Size = new System.Drawing.Size(208, 20);
            this.txtMaBHYT.TabIndex = 5;
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Location = new System.Drawing.Point(51, 154);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(47, 13);
            this.lblKhuVuc.TabIndex = 48;
            this.lblKhuVuc.Text = "Khu vực";
            // 
            // lblCongTy
            // 
            this.lblCongTy.AutoSize = true;
            this.lblCongTy.Location = new System.Drawing.Point(337, 129);
            this.lblCongTy.Name = "lblCongTy";
            this.lblCongTy.Size = new System.Drawing.Size(65, 13);
            this.lblCongTy.TabIndex = 47;
            this.lblCongTy.Text = "Noi lam viec";
            // 
            // lblBoPhan
            // 
            this.lblBoPhan.AutoSize = true;
            this.lblBoPhan.Location = new System.Drawing.Point(353, 103);
            this.lblBoPhan.Name = "lblBoPhan";
            this.lblBoPhan.Size = new System.Drawing.Size(47, 13);
            this.lblBoPhan.TabIndex = 46;
            this.lblBoPhan.Text = "Bộ phận";
            // 
            // lblNamSinh
            // 
            this.lblNamSinh.AutoSize = true;
            this.lblNamSinh.Location = new System.Drawing.Point(349, 77);
            this.lblNamSinh.Name = "lblNamSinh";
            this.lblNamSinh.Size = new System.Drawing.Size(51, 13);
            this.lblNamSinh.TabIndex = 45;
            this.lblNamSinh.Text = "Năm sinh";
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new System.Drawing.Point(349, 52);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(51, 13);
            this.lblGioiTinh.TabIndex = 44;
            this.lblGioiTinh.Text = "Giới Tính";
            // 
            // lblMaBHYT
            // 
            this.lblMaBHYT.AutoSize = true;
            this.lblMaBHYT.Location = new System.Drawing.Point(44, 127);
            this.lblMaBHYT.Name = "lblMaBHYT";
            this.lblMaBHYT.Size = new System.Drawing.Size(54, 13);
            this.lblMaBHYT.TabIndex = 43;
            this.lblMaBHYT.Text = "Mã BHYT";
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.Enabled = false;
            this.txtMaNhanVien.Location = new System.Drawing.Point(101, 73);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.Size = new System.Drawing.Size(156, 20);
            this.txtMaNhanVien.TabIndex = 2;
            // 
            // txtBenhNhan
            // 
            this.txtBenhNhan.Enabled = false;
            this.txtBenhNhan.Location = new System.Drawing.Point(101, 99);
            this.txtBenhNhan.Name = "txtBenhNhan";
            this.txtBenhNhan.Size = new System.Drawing.Size(156, 20);
            this.txtBenhNhan.TabIndex = 4;
            // 
            // lblMaNhanVien
            // 
            this.lblMaNhanVien.AutoSize = true;
            this.lblMaNhanVien.Location = new System.Drawing.Point(23, 76);
            this.lblMaNhanVien.Name = "lblMaNhanVien";
            this.lblMaNhanVien.Size = new System.Drawing.Size(75, 13);
            this.lblMaNhanVien.TabIndex = 40;
            this.lblMaNhanVien.Text = "Mã Nhân Viên";
            // 
            // lblBenhNhan
            // 
            this.lblBenhNhan.AutoSize = true;
            this.lblBenhNhan.Location = new System.Drawing.Point(38, 102);
            this.lblBenhNhan.Name = "lblBenhNhan";
            this.lblBenhNhan.Size = new System.Drawing.Size(61, 13);
            this.lblBenhNhan.TabIndex = 29;
            this.lblBenhNhan.Text = "Bệnh Nhân";
            // 
            // lblNgayKham
            // 
            this.lblNgayKham.AutoSize = true;
            this.lblNgayKham.Location = new System.Drawing.Point(36, 49);
            this.lblNgayKham.Name = "lblNgayKham";
            this.lblNgayKham.Size = new System.Drawing.Size(62, 13);
            this.lblNgayKham.TabIndex = 28;
            this.lblNgayKham.Text = "Ngày Khám";
            // 
            // lblPhongKham
            // 
            this.lblPhongKham.AutoSize = true;
            this.lblPhongKham.Location = new System.Drawing.Point(30, 23);
            this.lblPhongKham.Name = "lblPhongKham";
            this.lblPhongKham.Size = new System.Drawing.Size(68, 13);
            this.lblPhongKham.TabIndex = 27;
            this.lblPhongKham.Text = "Phòng Khám";
            // 
            // dtpNgayKham
            // 
            this.dtpNgayKham.CustomFormat = "dd-MM-yyyy";
            this.dtpNgayKham.Enabled = false;
            this.dtpNgayKham.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKham.Location = new System.Drawing.Point(101, 46);
            this.dtpNgayKham.Name = "dtpNgayKham";
            this.dtpNgayKham.Size = new System.Drawing.Size(156, 20);
            this.dtpNgayKham.TabIndex = 1;
            // 
            // cbbPhongKham
            // 
            this.cbbPhongKham.DisplayMember = "RoomName";
            this.cbbPhongKham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPhongKham.DropDownWidth = 130;
            this.cbbPhongKham.Enabled = false;
            this.cbbPhongKham.FormattingEnabled = true;
            this.cbbPhongKham.Location = new System.Drawing.Point(101, 20);
            this.cbbPhongKham.Name = "cbbPhongKham";
            this.cbbPhongKham.Size = new System.Drawing.Size(156, 21);
            this.cbbPhongKham.TabIndex = 0;
            this.cbbPhongKham.ValueMember = "RoomID";
            // 
            // btnDong
            // 
            this.btnDong.Image = global::UKPI.Properties.Resources.btnClose;
            this.btnDong.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDong.Location = new System.Drawing.Point(1115, 204);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(86, 23);
            this.btnDong.TabIndex = 22;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
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
            // btnIn
            // 
            this.btnIn.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnIn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIn.Location = new System.Drawing.Point(1045, 204);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(64, 23);
            this.btnIn.TabIndex = 20;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // htytDataSet1
            // 
            this.htytDataSet1.DataSetName = "HTYTDataSet";
            this.htytDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblTongTienBangChu
            // 
            this.lblTongTienBangChu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienBangChu.AutoSize = true;
            this.lblTongTienBangChu.Location = new System.Drawing.Point(11, 622);
            this.lblTongTienBangChu.Name = "lblTongTienBangChu";
            this.lblTongTienBangChu.Size = new System.Drawing.Size(106, 13);
            this.lblTongTienBangChu.TabIndex = 75;
            this.lblTongTienBangChu.Text = "Tổng Tiền Bằng Chử";
            // 
            // txtTongTienBangChu
            // 
            this.txtTongTienBangChu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongTienBangChu.Location = new System.Drawing.Point(121, 619);
            this.txtTongTienBangChu.Name = "txtTongTienBangChu";
            this.txtTongTienBangChu.Size = new System.Drawing.Size(849, 20);
            this.txtTongTienBangChu.TabIndex = 77;
            // 
            // txtTongTienBH
            // 
            this.txtTongTienBH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongTienBH.Enabled = false;
            this.txtTongTienBH.Location = new System.Drawing.Point(1080, 616);
            this.txtTongTienBH.Name = "txtTongTienBH";
            this.txtTongTienBH.Size = new System.Drawing.Size(127, 20);
            this.txtTongTienBH.TabIndex = 79;
            this.txtTongTienBH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongThanhTienBH
            // 
            this.lblTongThanhTienBH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongThanhTienBH.AutoSize = true;
            this.lblTongThanhTienBH.Location = new System.Drawing.Point(976, 620);
            this.lblTongThanhTienBH.Name = "lblTongThanhTienBH";
            this.lblTongThanhTienBH.Size = new System.Drawing.Size(100, 13);
            this.lblTongThanhTienBH.TabIndex = 78;
            this.lblTongThanhTienBH.Text = "Tổng thành tiền BH";
            // 
            // grdToaThuoc
            // 
            this.grdToaThuoc.AllowUserToAddRows = false;
            this.grdToaThuoc.AllowUserToDeleteRows = false;
            this.grdToaThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdToaThuoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdToaThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdToaThuoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.MaThuoc,
            this.TenThuoc,
            this.DonViTinh,
            this.ThuocBH,
            this.HamLuong,
            this.Gia,
            this.GiaTTBHYT,
            this.SoLuong,
            this.CachUong,
            this.CachUongChiTiet,
            this.ThanhTien,
            this.ThanhTienTTBHYT});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdToaThuoc.DefaultCellStyle = dataGridViewCellStyle9;
            this.grdToaThuoc.Location = new System.Drawing.Point(4, 233);
            this.grdToaThuoc.Name = "grdToaThuoc";
            this.grdToaThuoc.RowHeadersWidth = 39;
            this.grdToaThuoc.Size = new System.Drawing.Size(1203, 366);
            this.grdToaThuoc.TabIndex = 23;
            this.grdToaThuoc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdToaThuoc_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // MaThuoc
            // 
            this.MaThuoc.DataPropertyName = "MaThuoc";
            this.MaThuoc.HeaderText = "Mã thuốc";
            this.MaThuoc.Name = "MaThuoc";
            this.MaThuoc.ReadOnly = true;
            // 
            // TenThuoc
            // 
            this.TenThuoc.DataPropertyName = "TenThuoc";
            this.TenThuoc.HeaderText = "Tên Thuốc";
            this.TenThuoc.Name = "TenThuoc";
            this.TenThuoc.ReadOnly = true;
            // 
            // DonViTinh
            // 
            this.DonViTinh.DataPropertyName = "DonViTinh";
            this.DonViTinh.HeaderText = "Đơn vị tính";
            this.DonViTinh.Name = "DonViTinh";
            this.DonViTinh.ReadOnly = true;
            // 
            // ThuocBH
            // 
            this.ThuocBH.DataPropertyName = "ThuocBH";
            this.ThuocBH.HeaderText = "Bảo hiểm";
            this.ThuocBH.Name = "ThuocBH";
            this.ThuocBH.ReadOnly = true;
            this.ThuocBH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // HamLuong
            // 
            this.HamLuong.DataPropertyName = "HamLuong";
            this.HamLuong.HeaderText = "Hàm lượng";
            this.HamLuong.Name = "HamLuong";
            this.HamLuong.ReadOnly = true;
            this.HamLuong.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.HamLuong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Gia
            // 
            this.Gia.DataPropertyName = "Gia";
            this.Gia.HeaderText = "Giá (VAT)";
            this.Gia.Name = "Gia";
            this.Gia.ReadOnly = true;
            this.Gia.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Gia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GiaTTBHYT
            // 
            this.GiaTTBHYT.DataPropertyName = "GiaTTBHYT";
            this.GiaTTBHYT.HeaderText = "Giá TT BH";
            this.GiaTTBHYT.Name = "GiaTTBHYT";
            this.GiaTTBHYT.ReadOnly = true;
            this.GiaTTBHYT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GiaTTBHYT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SoLuong
            // 
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            this.SoLuong.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SoLuong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CachUong
            // 
            this.CachUong.DataPropertyName = "CachUong";
            this.CachUong.HeaderText = "Cách dùng";
            this.CachUong.Name = "CachUong";
            this.CachUong.ReadOnly = true;
            this.CachUong.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CachUong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CachUongChiTiet
            // 
            this.CachUongChiTiet.DataPropertyName = "CachUongChiTiet";
            this.CachUongChiTiet.HeaderText = "Cách dùng chi tiết";
            this.CachUongChiTiet.Name = "CachUongChiTiet";
            this.CachUongChiTiet.ReadOnly = true;
            this.CachUongChiTiet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CachUongChiTiet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ThanhTien
            // 
            this.ThanhTien.DataPropertyName = "ThanhTien";
            this.ThanhTien.HeaderText = "Thành tiền";
            this.ThanhTien.Name = "ThanhTien";
            this.ThanhTien.ReadOnly = true;
            this.ThanhTien.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ThanhTien.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ThanhTienTTBHYT
            // 
            this.ThanhTienTTBHYT.DataPropertyName = "ThanhTienTTBHYT";
            this.ThanhTienTTBHYT.HeaderText = "Thành tiền BH";
            this.ThanhTienTTBHYT.Name = "ThanhTienTTBHYT";
            this.ThanhTienTTBHYT.ReadOnly = true;
            this.ThanhTienTTBHYT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ThanhTienTTBHYT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmViewBenhNhanKhambenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 648);
            this.Controls.Add(this.txtTongTienBH);
            this.Controls.Add(this.lblTongThanhTienBH);
            this.Controls.Add(this.txtTongTienBangChu);
            this.Controls.Add(this.lblTongTienBangChu);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.grdToaThuoc);
            this.Controls.Add(this.grpThongTinKhamBenh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewBenhNhanKhambenh";
            this.Text = "KHÁM BỆNH CÓ BẢO HIỂM";
            this.grpThongTinKhamBenh.ResumeLayout(false);
            this.grpThongTinKhamBenh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htytDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdToaThuoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTinKhamBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn URBAN;
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
        private System.Windows.Forms.Label lblPhongKham;
        private System.Windows.Forms.Label lblBenhNhan;
        private System.Windows.Forms.Label lblNgayKham;
        private System.Windows.Forms.ComboBox cbbPhongKham;
        private System.Windows.Forms.DateTimePicker dtpNgayKham;
        private System.Windows.Forms.Label lblMaNhanVien;
        private System.Windows.Forms.TextBox txtMaNhanVien;
        private System.Windows.Forms.TextBox txtBenhNhan;
        private System.Windows.Forms.Label lblMaBHYT;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.Label lblCongTy;
        private System.Windows.Forms.Label lblBoPhan;
        private System.Windows.Forms.Label lblNamSinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.TextBox txtMaBHYT;
        private System.Windows.Forms.TextBox txtNamSinh;
        private System.Windows.Forms.TextBox txtCongTy;
        private System.Windows.Forms.Label lblNhomBenh;
        private System.Windows.Forms.Label lblSndn;
        private System.Windows.Forms.Label lblChanDoan;
        private System.Windows.Forms.TextBox txtChanDoan;
        private System.Windows.Forms.TextBox txtSndn;
        private System.Windows.Forms.Label lblMaICD;
        private System.Windows.Forms.TextBox txtDienGiaiICD;
        private System.Windows.Forms.Button btnDong;
        private Controls.DataGridView_RowNum grdToaThuoc;
        private System.Windows.Forms.Button btnIn;
        private HTYTHomebaseDataSet htytDataSet1;
        private System.Windows.Forms.TextBox txtTienKhamBenh;
        private System.Windows.Forms.Label lblTienKham;
        private System.Windows.Forms.Label lblDonvi;
        private System.Windows.Forms.Label lblTongTienBangChu;
        private System.Windows.Forms.TextBox txtTongTienBangChu;
        private System.Windows.Forms.TextBox txtTongTienBH;
        private System.Windows.Forms.Label lblTongThanhTienBH;
        private System.Windows.Forms.TextBox txtKhuVuc;
        private System.Windows.Forms.TextBox txtICD;
        private System.Windows.Forms.TextBox txtBoPhan;
        private System.Windows.Forms.TextBox txtGioiTinh;
        private System.Windows.Forms.TextBox txtNhomBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonViTinh;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ThuocBH;
        private System.Windows.Forms.DataGridViewTextBoxColumn HamLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gia;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaTTBHYT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn CachUong;
        private System.Windows.Forms.DataGridViewTextBoxColumn CachUongChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTienTTBHYT;
    }
}