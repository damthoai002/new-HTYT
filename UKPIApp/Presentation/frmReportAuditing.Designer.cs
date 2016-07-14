namespace UKPI.Presentation
{
    partial class frmReportAuditing
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportAuditing));
            this.grpStore = new System.Windows.Forms.GroupBox();
            this.lblTimeperiod = new System.Windows.Forms.Label();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.cboPSType = new System.Windows.Forms.ComboBox();
            this.lblPSType = new System.Windows.Forms.Label();
            this.cboStarclub = new System.Windows.Forms.ComboBox();
            this.lblStarclub = new System.Windows.Forms.Label();
            this.cboStore = new System.Windows.Forms.ComboBox();
            this.txtDistributor = new System.Windows.Forms.TextBox();
            this.txtStoreID = new System.Windows.Forms.TextBox();
            this.lblDistributor = new System.Windows.Forms.Label();
            this.lblStoreID = new System.Windows.Forms.Label();
            this.cboTown = new System.Windows.Forms.ComboBox();
            this.lblTown = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboUrban = new System.Windows.Forms.ComboBox();
            this.lblUrban = new System.Windows.Forms.Label();
            this.cboProvince = new System.Windows.Forms.ComboBox();
            this.lblProvince = new System.Windows.Forms.Label();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.cboChannel = new System.Windows.Forms.ComboBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.grplist = new System.Windows.Forms.GroupBox();
            this.grdStores = new UKPI.Controls.DataGridView_RowNum();
            this.REGION_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISTRIBUTOR_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISTRIBUTOR_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STORE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STORE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STORE_ADDRESS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOWN_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROVINCE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaleSupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaleSupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPerfectStoreName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URBAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTLET_TYPE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAR_CLUB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PS_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TURNOVER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIconic_HPC_Street = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIconicHPCMarket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIconicFoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExtraIconicFoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrbanHPCStreet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrbanHPCMarket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrbanPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrbanFoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExtraUrbanFoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuralHPCStreet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuralHPCMarket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuralFoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExtraRuralFoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkVNeseUnsigned = new System.Windows.Forms.CheckBox();
            this.txtValueFind = new UKPI.Controls.SelectAllSupplyTextBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.cboStoreOption = new System.Windows.Forms.ComboBox();
            this.btnOExport = new System.Windows.Forms.Button();
            this.grpStore.SuspendLayout();
            this.grplist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStore
            // 
            this.grpStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStore.Controls.Add(this.lblTimeperiod);
            this.grpStore.Controls.Add(this.cboMonth);
            this.grpStore.Controls.Add(this.cboYear);
            this.grpStore.Controls.Add(this.cboPSType);
            this.grpStore.Controls.Add(this.lblPSType);
            this.grpStore.Controls.Add(this.cboStarclub);
            this.grpStore.Controls.Add(this.lblStarclub);
            this.grpStore.Controls.Add(this.cboStore);
            this.grpStore.Controls.Add(this.txtDistributor);
            this.grpStore.Controls.Add(this.txtStoreID);
            this.grpStore.Controls.Add(this.lblDistributor);
            this.grpStore.Controls.Add(this.lblStoreID);
            this.grpStore.Controls.Add(this.cboTown);
            this.grpStore.Controls.Add(this.lblTown);
            this.grpStore.Controls.Add(this.btnSearch);
            this.grpStore.Controls.Add(this.cboUrban);
            this.grpStore.Controls.Add(this.lblUrban);
            this.grpStore.Controls.Add(this.cboProvince);
            this.grpStore.Controls.Add(this.lblProvince);
            this.grpStore.Controls.Add(this.cboRegion);
            this.grpStore.Controls.Add(this.lblRegion);
            this.grpStore.Controls.Add(this.cboChannel);
            this.grpStore.Controls.Add(this.lblChannel);
            this.grpStore.Location = new System.Drawing.Point(12, 5);
            this.grpStore.Name = "grpStore";
            this.grpStore.Size = new System.Drawing.Size(981, 86);
            this.grpStore.TabIndex = 14;
            this.grpStore.TabStop = false;
            this.grpStore.Text = "Store filter";
            // 
            // lblTimeperiod
            // 
            this.lblTimeperiod.AutoSize = true;
            this.lblTimeperiod.Location = new System.Drawing.Point(672, 58);
            this.lblTimeperiod.Name = "lblTimeperiod";
            this.lblTimeperiod.Size = new System.Drawing.Size(59, 13);
            this.lblTimeperiod.TabIndex = 29;
            this.lblTimeperiod.Text = "Timeperiod";
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(802, 55);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(42, 21);
            this.cboMonth.TabIndex = 31;
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(737, 55);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(67, 21);
            this.cboYear.TabIndex = 30;
            // 
            // cboPSType
            // 
            this.cboPSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPSType.DropDownWidth = 100;
            this.cboPSType.FormattingEnabled = true;
            this.cboPSType.Location = new System.Drawing.Point(891, 20);
            this.cboPSType.Name = "cboPSType";
            this.cboPSType.Size = new System.Drawing.Size(85, 21);
            this.cboPSType.TabIndex = 28;
            // 
            // lblPSType
            // 
            this.lblPSType.AutoSize = true;
            this.lblPSType.Location = new System.Drawing.Point(837, 23);
            this.lblPSType.Name = "lblPSType";
            this.lblPSType.Size = new System.Drawing.Size(48, 13);
            this.lblPSType.TabIndex = 27;
            this.lblPSType.Text = "PS Type";
            // 
            // cboStarclub
            // 
            this.cboStarclub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStarclub.FormattingEnabled = true;
            this.cboStarclub.Items.AddRange(new object[] {
            "",
            "YES",
            "NO"});
            this.cboStarclub.Location = new System.Drawing.Point(782, 20);
            this.cboStarclub.Name = "cboStarclub";
            this.cboStarclub.Size = new System.Drawing.Size(45, 21);
            this.cboStarclub.TabIndex = 26;
            // 
            // lblStarclub
            // 
            this.lblStarclub.AutoSize = true;
            this.lblStarclub.Location = new System.Drawing.Point(730, 23);
            this.lblStarclub.Name = "lblStarclub";
            this.lblStarclub.Size = new System.Drawing.Size(46, 13);
            this.lblStarclub.TabIndex = 25;
            this.lblStarclub.Text = "Starclub";
            // 
            // cboStore
            // 
            this.cboStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStore.DropDownWidth = 110;
            this.cboStore.FormattingEnabled = true;
            this.cboStore.Location = new System.Drawing.Point(372, 55);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(100, 21);
            this.cboStore.TabIndex = 22;
            // 
            // txtDistributor
            // 
            this.txtDistributor.Location = new System.Drawing.Point(71, 55);
            this.txtDistributor.MaxLength = 250;
            this.txtDistributor.Name = "txtDistributor";
            this.txtDistributor.Size = new System.Drawing.Size(243, 20);
            this.txtDistributor.TabIndex = 21;
            this.txtDistributor.Text = "(Press F3 to select Distributors)";
            // 
            // txtStoreID
            // 
            this.txtStoreID.Location = new System.Drawing.Point(479, 55);
            this.txtStoreID.MaxLength = 250;
            this.txtStoreID.Name = "txtStoreID";
            this.txtStoreID.Size = new System.Drawing.Size(180, 20);
            this.txtStoreID.TabIndex = 17;
            // 
            // lblDistributor
            // 
            this.lblDistributor.AutoSize = true;
            this.lblDistributor.Location = new System.Drawing.Point(6, 58);
            this.lblDistributor.Name = "lblDistributor";
            this.lblDistributor.Size = new System.Drawing.Size(59, 13);
            this.lblDistributor.TabIndex = 14;
            this.lblDistributor.Text = "Distributors";
            // 
            // lblStoreID
            // 
            this.lblStoreID.AutoSize = true;
            this.lblStoreID.Location = new System.Drawing.Point(334, 58);
            this.lblStoreID.Name = "lblStoreID";
            this.lblStoreID.Size = new System.Drawing.Size(32, 13);
            this.lblStoreID.TabIndex = 16;
            this.lblStoreID.Text = "Store";
            // 
            // cboTown
            // 
            this.cboTown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTown.DropDownWidth = 130;
            this.cboTown.FormattingEnabled = true;
            this.cboTown.Location = new System.Drawing.Point(514, 20);
            this.cboTown.Name = "cboTown";
            this.cboTown.Size = new System.Drawing.Size(105, 21);
            this.cboTown.TabIndex = 7;
            // 
            // lblTown
            // 
            this.lblTown.AutoSize = true;
            this.lblTown.Location = new System.Drawing.Point(478, 23);
            this.lblTown.Name = "lblTown";
            this.lblTown.Size = new System.Drawing.Size(34, 13);
            this.lblTown.TabIndex = 6;
            this.lblTown.Text = "Town";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::UKPI.Properties.Resources.btnSearch;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.Location = new System.Drawing.Point(890, 53);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // cboUrban
            // 
            this.cboUrban.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUrban.FormattingEnabled = true;
            this.cboUrban.Items.AddRange(new object[] {
            "",
            "Urban",
            "Rural"});
            this.cboUrban.Location = new System.Drawing.Point(660, 20);
            this.cboUrban.Name = "cboUrban";
            this.cboUrban.Size = new System.Drawing.Size(62, 21);
            this.cboUrban.TabIndex = 9;
            // 
            // lblUrban
            // 
            this.lblUrban.AutoSize = true;
            this.lblUrban.Location = new System.Drawing.Point(625, 23);
            this.lblUrban.Name = "lblUrban";
            this.lblUrban.Size = new System.Drawing.Size(29, 13);
            this.lblUrban.TabIndex = 8;
            this.lblUrban.Text = "Area";
            // 
            // cboProvince
            // 
            this.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvince.DropDownWidth = 160;
            this.cboProvince.FormattingEnabled = true;
            this.cboProvince.Location = new System.Drawing.Point(372, 20);
            this.cboProvince.Name = "cboProvince";
            this.cboProvince.Size = new System.Drawing.Size(100, 21);
            this.cboProvince.TabIndex = 5;
            // 
            // lblProvince
            // 
            this.lblProvince.AutoSize = true;
            this.lblProvince.Location = new System.Drawing.Point(320, 23);
            this.lblProvince.Name = "lblProvince";
            this.lblProvince.Size = new System.Drawing.Size(49, 13);
            this.lblProvince.TabIndex = 4;
            this.lblProvince.Text = "Province";
            // 
            // cboRegion
            // 
            this.cboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegion.DropDownWidth = 130;
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(209, 20);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(105, 21);
            this.cboRegion.TabIndex = 3;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(166, 23);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(41, 13);
            this.lblRegion.TabIndex = 2;
            this.lblRegion.Text = "Region";
            // 
            // cboChannel
            // 
            this.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChannel.DropDownWidth = 130;
            this.cboChannel.FormattingEnabled = true;
            this.cboChannel.Location = new System.Drawing.Point(55, 20);
            this.cboChannel.Name = "cboChannel";
            this.cboChannel.Size = new System.Drawing.Size(105, 21);
            this.cboChannel.TabIndex = 1;
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(6, 23);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(46, 13);
            this.lblChannel.TabIndex = 0;
            this.lblChannel.Text = "Channel";
            // 
            // grplist
            // 
            this.grplist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grplist.Controls.Add(this.grdStores);
            this.grplist.Location = new System.Drawing.Point(12, 97);
            this.grplist.Name = "grplist";
            this.grplist.Size = new System.Drawing.Size(981, 502);
            this.grplist.TabIndex = 15;
            this.grplist.TabStop = false;
            // 
            // grdStores
            // 
            this.grdStores.AllowUserToAddRows = false;
            this.grdStores.AllowUserToDeleteRows = false;
            this.grdStores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdStores.BackgroundColor = System.Drawing.Color.White;
            this.grdStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.REGION_NAME,
            this.DISTRIBUTOR_CODE,
            this.DISTRIBUTOR_NAME,
            this.STORE_CODE,
            this.STORE_NAME,
            this.AuditStatus,
            this.STORE_ADDRESS,
            this.TOWN_NAME,
            this.PROVINCE_NAME,
            this.colSaleSupID,
            this.colSaleSupName,
            this.colPerfectStoreName,
            this.URBAN,
            this.OUTLET_TYPE_NAME,
            this.LOCATION,
            this.STAR_CLUB,
            this.PS_TYPE,
            this.TURNOVER,
            this.colIconic_HPC_Street,
            this.colIconicHPCMarket,
            this.colIconicFoods,
            this.colExtraIconicFoods,
            this.colUrbanHPCStreet,
            this.colUrbanHPCMarket,
            this.colUrbanPC,
            this.colUrbanFoods,
            this.colExtraUrbanFoods,
            this.colRuralHPCStreet,
            this.colRuralHPCMarket,
            this.colRuralFoods,
            this.colExtraRuralFoods});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdStores.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdStores.Location = new System.Drawing.Point(6, 11);
            this.grdStores.Name = "grdStores";
            this.grdStores.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.grdStores.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.grdStores.Size = new System.Drawing.Size(969, 485);
            this.grdStores.TabIndex = 10;
            // 
            // REGION_NAME
            // 
            this.REGION_NAME.DataPropertyName = "REGION_NAME";
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            this.REGION_NAME.DefaultCellStyle = dataGridViewCellStyle1;
            this.REGION_NAME.HeaderText = "Region";
            this.REGION_NAME.Name = "REGION_NAME";
            this.REGION_NAME.ReadOnly = true;
            this.REGION_NAME.Width = 80;
            // 
            // DISTRIBUTOR_CODE
            // 
            this.DISTRIBUTOR_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DISTRIBUTOR_CODE.DataPropertyName = "DISTRIBUTOR_CODE";
            this.DISTRIBUTOR_CODE.HeaderText = "Distributors ID";
            this.DISTRIBUTOR_CODE.Name = "DISTRIBUTOR_CODE";
            this.DISTRIBUTOR_CODE.ReadOnly = true;
            // 
            // DISTRIBUTOR_NAME
            // 
            this.DISTRIBUTOR_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DISTRIBUTOR_NAME.DataPropertyName = "DISTRIBUTOR_NAME";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            this.DISTRIBUTOR_NAME.DefaultCellStyle = dataGridViewCellStyle2;
            this.DISTRIBUTOR_NAME.HeaderText = "Distributor Name";
            this.DISTRIBUTOR_NAME.Name = "DISTRIBUTOR_NAME";
            this.DISTRIBUTOR_NAME.ReadOnly = true;
            this.DISTRIBUTOR_NAME.Width = 220;
            // 
            // STORE_CODE
            // 
            this.STORE_CODE.DataPropertyName = "STORE_CODE";
            this.STORE_CODE.HeaderText = "Store ID";
            this.STORE_CODE.Name = "STORE_CODE";
            this.STORE_CODE.ReadOnly = true;
            this.STORE_CODE.Width = 200;
            // 
            // STORE_NAME
            // 
            this.STORE_NAME.DataPropertyName = "STORE_NAME";
            this.STORE_NAME.HeaderText = "Store Name";
            this.STORE_NAME.Name = "STORE_NAME";
            this.STORE_NAME.ReadOnly = true;
            this.STORE_NAME.Width = 200;
            // 
            // AuditStatus
            // 
            this.AuditStatus.HeaderText = "Audit Status";
            this.AuditStatus.Name = "AuditStatus";
            // 
            // STORE_ADDRESS
            // 
            this.STORE_ADDRESS.DataPropertyName = "STORE_ADDRESS";
            this.STORE_ADDRESS.HeaderText = "Store Address";
            this.STORE_ADDRESS.Name = "STORE_ADDRESS";
            this.STORE_ADDRESS.ReadOnly = true;
            this.STORE_ADDRESS.Width = 200;
            // 
            // TOWN_NAME
            // 
            this.TOWN_NAME.DataPropertyName = "TOWN_NAME";
            this.TOWN_NAME.HeaderText = "Town";
            this.TOWN_NAME.Name = "TOWN_NAME";
            this.TOWN_NAME.ReadOnly = true;
            this.TOWN_NAME.Width = 120;
            // 
            // PROVINCE_NAME
            // 
            this.PROVINCE_NAME.DataPropertyName = "PROVINCE_NAME";
            this.PROVINCE_NAME.HeaderText = "Province";
            this.PROVINCE_NAME.Name = "PROVINCE_NAME";
            this.PROVINCE_NAME.ReadOnly = true;
            this.PROVINCE_NAME.Width = 150;
            // 
            // colSaleSupID
            // 
            this.colSaleSupID.DataPropertyName = "SALE_SUP_ID";
            this.colSaleSupID.HeaderText = "Sales Sup ID";
            this.colSaleSupID.Name = "colSaleSupID";
            // 
            // colSaleSupName
            // 
            this.colSaleSupName.DataPropertyName = "SALE_SUP_NAME";
            this.colSaleSupName.HeaderText = "Sales Sup Name";
            this.colSaleSupName.Name = "colSaleSupName";
            this.colSaleSupName.Width = 170;
            // 
            // colPerfectStoreName
            // 
            this.colPerfectStoreName.DataPropertyName = "PERFECT_STORE_NAME";
            this.colPerfectStoreName.HeaderText = "Perfect Store Name";
            this.colPerfectStoreName.Name = "colPerfectStoreName";
            this.colPerfectStoreName.Width = 170;
            // 
            // URBAN
            // 
            this.URBAN.DataPropertyName = "URBAN";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            this.URBAN.DefaultCellStyle = dataGridViewCellStyle3;
            this.URBAN.HeaderText = "Urban";
            this.URBAN.Name = "URBAN";
            this.URBAN.ReadOnly = true;
            this.URBAN.Width = 61;
            // 
            // OUTLET_TYPE_NAME
            // 
            this.OUTLET_TYPE_NAME.DataPropertyName = "OUTLET_TYPE_NAME";
            this.OUTLET_TYPE_NAME.HeaderText = "Outlet Classification";
            this.OUTLET_TYPE_NAME.Name = "OUTLET_TYPE_NAME";
            this.OUTLET_TYPE_NAME.ReadOnly = true;
            this.OUTLET_TYPE_NAME.Width = 114;
            // 
            // LOCATION
            // 
            this.LOCATION.DataPropertyName = "LOCATION";
            this.LOCATION.HeaderText = "Location";
            this.LOCATION.Name = "LOCATION";
            this.LOCATION.ReadOnly = true;
            this.LOCATION.Width = 73;
            // 
            // STAR_CLUB
            // 
            this.STAR_CLUB.DataPropertyName = "STAR_CLUB";
            this.STAR_CLUB.HeaderText = "Perfect Store";
            this.STAR_CLUB.Name = "STAR_CLUB";
            this.STAR_CLUB.ReadOnly = true;
            this.STAR_CLUB.Width = 70;
            // 
            // PS_TYPE
            // 
            this.PS_TYPE.DataPropertyName = "PS_TYPE_NAME";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            this.PS_TYPE.DefaultCellStyle = dataGridViewCellStyle4;
            this.PS_TYPE.HeaderText = "PS Type";
            this.PS_TYPE.Name = "PS_TYPE";
            this.PS_TYPE.ReadOnly = true;
            // 
            // TURNOVER
            // 
            this.TURNOVER.DataPropertyName = "TURNOVER";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TURNOVER.DefaultCellStyle = dataGridViewCellStyle5;
            this.TURNOVER.HeaderText = "Turnover";
            this.TURNOVER.Name = "TURNOVER";
            this.TURNOVER.ReadOnly = true;
            // 
            // colIconic_HPC_Street
            // 
            this.colIconic_HPC_Street.DataPropertyName = "ICONIC_HPC_STREET";
            this.colIconic_HPC_Street.HeaderText = "Iconic HPC Street";
            this.colIconic_HPC_Street.Name = "colIconic_HPC_Street";
            this.colIconic_HPC_Street.Width = 70;
            // 
            // colIconicHPCMarket
            // 
            this.colIconicHPCMarket.DataPropertyName = "ICONIC_HPC_MARKET";
            this.colIconicHPCMarket.HeaderText = "Iconic HPC Market";
            this.colIconicHPCMarket.Name = "colIconicHPCMarket";
            this.colIconicHPCMarket.Width = 70;
            // 
            // colIconicFoods
            // 
            this.colIconicFoods.DataPropertyName = "ICONIC_FOODS";
            this.colIconicFoods.HeaderText = "Iconic Foods";
            this.colIconicFoods.Name = "colIconicFoods";
            this.colIconicFoods.Width = 70;
            // 
            // colExtraIconicFoods
            // 
            this.colExtraIconicFoods.DataPropertyName = "ICONIC_FOODS_EXTRA";
            this.colExtraIconicFoods.HeaderText = "Extra Iconic Foods";
            this.colExtraIconicFoods.Name = "colExtraIconicFoods";
            this.colExtraIconicFoods.Width = 70;
            // 
            // colUrbanHPCStreet
            // 
            this.colUrbanHPCStreet.DataPropertyName = "URBAN_HPC_STREET";
            this.colUrbanHPCStreet.HeaderText = "Urban HPC Street";
            this.colUrbanHPCStreet.Name = "colUrbanHPCStreet";
            this.colUrbanHPCStreet.Width = 70;
            // 
            // colUrbanHPCMarket
            // 
            this.colUrbanHPCMarket.DataPropertyName = "URBAN_HPC_MARKET";
            this.colUrbanHPCMarket.HeaderText = "Urban HPC Market";
            this.colUrbanHPCMarket.Name = "colUrbanHPCMarket";
            this.colUrbanHPCMarket.Width = 70;
            // 
            // colUrbanPC
            // 
            this.colUrbanPC.DataPropertyName = "URBAN_PC";
            this.colUrbanPC.HeaderText = "Urban PC";
            this.colUrbanPC.Name = "colUrbanPC";
            this.colUrbanPC.Width = 70;
            // 
            // colUrbanFoods
            // 
            this.colUrbanFoods.DataPropertyName = "URBAN_FOODS";
            this.colUrbanFoods.HeaderText = "Urban Foods";
            this.colUrbanFoods.Name = "colUrbanFoods";
            this.colUrbanFoods.Width = 70;
            // 
            // colExtraUrbanFoods
            // 
            this.colExtraUrbanFoods.DataPropertyName = "URBAN_FOODS_EXTRA";
            this.colExtraUrbanFoods.HeaderText = "Extra Urban Foods";
            this.colExtraUrbanFoods.Name = "colExtraUrbanFoods";
            this.colExtraUrbanFoods.Width = 70;
            // 
            // colRuralHPCStreet
            // 
            this.colRuralHPCStreet.DataPropertyName = "RURAL_HPC_STREET";
            this.colRuralHPCStreet.HeaderText = "Rural HPC Street";
            this.colRuralHPCStreet.Name = "colRuralHPCStreet";
            this.colRuralHPCStreet.Width = 70;
            // 
            // colRuralHPCMarket
            // 
            this.colRuralHPCMarket.DataPropertyName = "RURAL_HPC_MARKET";
            this.colRuralHPCMarket.HeaderText = "Rural HPC Market";
            this.colRuralHPCMarket.Name = "colRuralHPCMarket";
            this.colRuralHPCMarket.Width = 70;
            // 
            // colRuralFoods
            // 
            this.colRuralFoods.DataPropertyName = "RURAL_FOODS";
            this.colRuralFoods.HeaderText = "Rural Foods";
            this.colRuralFoods.Name = "colRuralFoods";
            this.colRuralFoods.Width = 70;
            // 
            // colExtraRuralFoods
            // 
            this.colExtraRuralFoods.DataPropertyName = "RURAL_FOODS_EXTRA";
            this.colExtraRuralFoods.HeaderText = "Extra Rural Foods";
            this.colExtraRuralFoods.Name = "colExtraRuralFoods";
            this.colExtraRuralFoods.Width = 70;
            // 
            // chkVNeseUnsigned
            // 
            this.chkVNeseUnsigned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkVNeseUnsigned.AutoSize = true;
            this.chkVNeseUnsigned.Location = new System.Drawing.Point(770, 615);
            this.chkVNeseUnsigned.Name = "chkVNeseUnsigned";
            this.chkVNeseUnsigned.Size = new System.Drawing.Size(127, 17);
            this.chkVNeseUnsigned.TabIndex = 20;
            this.chkVNeseUnsigned.Text = "Vietnamese unsigned";
            this.chkVNeseUnsigned.UseVisualStyleBackColor = true;
            // 
            // txtValueFind
            // 
            this.txtValueFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtValueFind.Location = new System.Drawing.Point(138, 613);
            this.txtValueFind.MaxLength = 250;
            this.txtValueFind.Name = "txtValueFind";
            this.txtValueFind.Size = new System.Drawing.Size(250, 20);
            this.txtValueFind.TabIndex = 18;
            // 
            // btnGoto
            // 
            this.btnGoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGoto.Enabled = false;
            this.btnGoto.Image = global::UKPI.Properties.Resources.btnSearch2;
            this.btnGoto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoto.Location = new System.Drawing.Point(394, 611);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(85, 23);
            this.btnGoto.TabIndex = 19;
            this.btnGoto.Text = "Go to";
            this.btnGoto.UseVisualStyleBackColor = true;
            // 
            // cboStoreOption
            // 
            this.cboStoreOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboStoreOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStoreOption.FormattingEnabled = true;
            this.cboStoreOption.Location = new System.Drawing.Point(12, 613);
            this.cboStoreOption.Name = "cboStoreOption";
            this.cboStoreOption.Size = new System.Drawing.Size(120, 21);
            this.cboStoreOption.TabIndex = 17;
            // 
            // btnOExport
            // 
            this.btnOExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOExport.Image = ((System.Drawing.Image)(resources.GetObject("btnOExport.Image")));
            this.btnOExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOExport.Location = new System.Drawing.Point(903, 611);
            this.btnOExport.Name = "btnOExport";
            this.btnOExport.Size = new System.Drawing.Size(85, 23);
            this.btnOExport.TabIndex = 16;
            this.btnOExport.Text = "Export";
            this.btnOExport.UseVisualStyleBackColor = true;
            // 
            // frmReportAuditing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 642);
            this.Controls.Add(this.chkVNeseUnsigned);
            this.Controls.Add(this.grplist);
            this.Controls.Add(this.txtValueFind);
            this.Controls.Add(this.grpStore);
            this.Controls.Add(this.btnGoto);
            this.Controls.Add(this.cboStoreOption);
            this.Controls.Add(this.btnOExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportAuditing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Auditing";
            this.grpStore.ResumeLayout(false);
            this.grpStore.PerformLayout();
            this.grplist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStore;
        private System.Windows.Forms.Label lblTimeperiod;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.ComboBox cboPSType;
        private System.Windows.Forms.Label lblPSType;
        private System.Windows.Forms.ComboBox cboStarclub;
        private System.Windows.Forms.Label lblStarclub;
        private System.Windows.Forms.ComboBox cboStore;
        private System.Windows.Forms.TextBox txtDistributor;
        private System.Windows.Forms.TextBox txtStoreID;
        private System.Windows.Forms.Label lblDistributor;
        private System.Windows.Forms.Label lblStoreID;
        private System.Windows.Forms.ComboBox cboTown;
        private System.Windows.Forms.Label lblTown;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboUrban;
        private System.Windows.Forms.Label lblUrban;
        private System.Windows.Forms.ComboBox cboProvince;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.ComboBox cboChannel;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.GroupBox grplist;
        private UKPI.Controls.DataGridView_RowNum grdStores;
        private System.Windows.Forms.DataGridViewTextBoxColumn REGION_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DISTRIBUTOR_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DISTRIBUTOR_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn STORE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STORE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn STORE_ADDRESS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOWN_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROVINCE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleSupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleSupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPerfectStoreName;
        private System.Windows.Forms.DataGridViewTextBoxColumn URBAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTLET_TYPE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAR_CLUB;
        private System.Windows.Forms.DataGridViewTextBoxColumn PS_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TURNOVER;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIconic_HPC_Street;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIconicHPCMarket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIconicFoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtraIconicFoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrbanHPCStreet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrbanHPCMarket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrbanPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrbanFoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtraUrbanFoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuralHPCStreet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuralHPCMarket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuralFoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtraRuralFoods;
        private System.Windows.Forms.CheckBox chkVNeseUnsigned;
        private UKPI.Controls.SelectAllSupplyTextBox txtValueFind;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.ComboBox cboStoreOption;
        private System.Windows.Forms.Button btnOExport;
    }
}