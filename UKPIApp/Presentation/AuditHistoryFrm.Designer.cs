namespace UKPI.Presentation
{
    partial class AuditHistoryFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.cboChannel = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.cboStore = new System.Windows.Forms.ComboBox();
            this.txtStoreValue = new System.Windows.Forms.TextBox();
            this.lblStoreID = new System.Windows.Forms.Label();
            this.txtDistributors = new System.Windows.Forms.TextBox();
            this.lblDistributor = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grdStoresValid = new UKPI.Controls.DataGridView_RowNum();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_RowNum1 = new UKPI.Controls.DataGridView_RowNum();
            this.ColTimePeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAuditDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAuditResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdStoresValid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RowNum1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboRegion
            // 
            this.cboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegion.DropDownWidth = 120;
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(255, 6);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(135, 21);
            this.cboRegion.TabIndex = 7;
            // 
            // cboChannel
            // 
            this.cboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChannel.DropDownWidth = 130;
            this.cboChannel.FormattingEnabled = true;
            this.cboChannel.Location = new System.Drawing.Point(72, 6);
            this.cboChannel.Name = "cboChannel";
            this.cboChannel.Size = new System.Drawing.Size(121, 21);
            this.cboChannel.TabIndex = 5;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(208, 9);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(41, 13);
            this.lblRegion.TabIndex = 6;
            this.lblRegion.Text = "Region";
            // 
            // lblChannel
            // 
            this.lblChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(12, 9);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(46, 13);
            this.lblChannel.TabIndex = 4;
            this.lblChannel.Text = "Channel";
            // 
            // cboStore
            // 
            this.cboStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStore.DropDownWidth = 110;
            this.cboStore.FormattingEnabled = true;
            this.cboStore.Location = new System.Drawing.Point(72, 36);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(121, 21);
            this.cboStore.TabIndex = 32;
            // 
            // txtStoreValue
            // 
            this.txtStoreValue.Location = new System.Drawing.Point(211, 36);
            this.txtStoreValue.MaxLength = 250;
            this.txtStoreValue.Name = "txtStoreValue";
            this.txtStoreValue.Size = new System.Drawing.Size(179, 20);
            this.txtStoreValue.TabIndex = 33;
            // 
            // lblStoreID
            // 
            this.lblStoreID.AutoSize = true;
            this.lblStoreID.Location = new System.Drawing.Point(12, 39);
            this.lblStoreID.Name = "lblStoreID";
            this.lblStoreID.Size = new System.Drawing.Size(32, 13);
            this.lblStoreID.TabIndex = 31;
            this.lblStoreID.Text = "Store";
            // 
            // txtDistributors
            // 
            this.txtDistributors.BackColor = System.Drawing.SystemColors.Window;
            this.txtDistributors.Location = new System.Drawing.Point(474, 6);
            this.txtDistributors.MaxLength = 250;
            this.txtDistributors.Name = "txtDistributors";
            this.txtDistributors.ReadOnly = true;
            this.txtDistributors.Size = new System.Drawing.Size(330, 20);
            this.txtDistributors.TabIndex = 30;
            this.txtDistributors.Text = "(Press F3 to select distributors)";
            // 
            // lblDistributor
            // 
            this.lblDistributor.AutoSize = true;
            this.lblDistributor.Location = new System.Drawing.Point(414, 9);
            this.lblDistributor.Name = "lblDistributor";
            this.lblDistributor.Size = new System.Drawing.Size(54, 13);
            this.lblDistributor.TabIndex = 29;
            this.lblDistributor.Text = "Distributor";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Image = global::UKPI.Properties.Resources.btnSearch2;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.Location = new System.Drawing.Point(417, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 34;
            this.btnSearch.Text = "     Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // grdStoresValid
            // 
            this.grdStoresValid.AllowUserToAddRows = false;
            this.grdStoresValid.AllowUserToDeleteRows = false;
            this.grdStoresValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdStoresValid.BackgroundColor = System.Drawing.Color.White;
            this.grdStoresValid.ColumnHeadersHeight = 31;
            this.grdStoresValid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn41,
            this.dataGridViewTextBoxColumn42});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdStoresValid.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdStoresValid.Location = new System.Drawing.Point(15, 63);
            this.grdStoresValid.Name = "grdStoresValid";
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.grdStoresValid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grdStoresValid.Size = new System.Drawing.Size(789, 190);
            this.grdStoresValid.TabIndex = 35;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn23.DataPropertyName = "DISTRIBUTOR_CODE";
            this.dataGridViewTextBoxColumn23.Frozen = true;
            this.dataGridViewTextBoxColumn23.HeaderText = "Distributors ID";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn24.DataPropertyName = "DISTRIBUTOR_NAME";
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            this.dataGridViewTextBoxColumn24.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn24.Frozen = true;
            this.dataGridViewTextBoxColumn24.HeaderText = "Distributor Name";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 160;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "STORE_CODE";
            this.dataGridViewTextBoxColumn25.Frozen = true;
            this.dataGridViewTextBoxColumn25.HeaderText = "Store ID";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Width = 140;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "STORE_NAME";
            this.dataGridViewTextBoxColumn26.Frozen = true;
            this.dataGridViewTextBoxColumn26.HeaderText = "Store Name";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.Width = 150;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.DataPropertyName = "UPDATED_STORE_NAME";
            this.dataGridViewTextBoxColumn27.HeaderText = "Updated Store Name";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.Width = 180;
            // 
            // dataGridViewTextBoxColumn28
            // 
            this.dataGridViewTextBoxColumn28.DataPropertyName = "STORE_ADDRESS1";
            this.dataGridViewTextBoxColumn28.HeaderText = "Store Address 1";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.Width = 200;
            // 
            // dataGridViewTextBoxColumn29
            // 
            this.dataGridViewTextBoxColumn29.DataPropertyName = "STORE_ADDRESS2";
            this.dataGridViewTextBoxColumn29.HeaderText = "Store Address 2";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.Width = 200;
            // 
            // dataGridViewTextBoxColumn30
            // 
            this.dataGridViewTextBoxColumn30.DataPropertyName = "STORE_ADDRESS3";
            this.dataGridViewTextBoxColumn30.HeaderText = "Store Address 3";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.Width = 200;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "SALE_SUP_ID";
            this.dataGridViewTextBoxColumn33.HeaderText = "Sales Sup ID";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.DataPropertyName = "SALE_SUP_NAME";
            this.dataGridViewTextBoxColumn34.HeaderText = "Sales Sup Name";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Width = 200;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.DataPropertyName = "OUTLET_TYPE_NAME";
            this.dataGridViewTextBoxColumn37.HeaderText = "Outlet Classfication";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.Width = 150;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.DataPropertyName = "STAR_CLUB";
            this.dataGridViewTextBoxColumn40.HeaderText = "Perfect Store";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn41
            // 
            this.dataGridViewTextBoxColumn41.DataPropertyName = "PS_TYPE_NAME";
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            this.dataGridViewTextBoxColumn41.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn41.HeaderText = "PS Type";
            this.dataGridViewTextBoxColumn41.Name = "dataGridViewTextBoxColumn41";
            this.dataGridViewTextBoxColumn41.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn42
            // 
            this.dataGridViewTextBoxColumn42.DataPropertyName = "TURNOVER";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn42.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn42.HeaderText = "Turnover";
            this.dataGridViewTextBoxColumn42.Name = "dataGridViewTextBoxColumn42";
            this.dataGridViewTextBoxColumn42.ReadOnly = true;
            // 
            // dataGridView_RowNum1
            // 
            this.dataGridView_RowNum1.AllowUserToAddRows = false;
            this.dataGridView_RowNum1.AllowUserToDeleteRows = false;
            this.dataGridView_RowNum1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_RowNum1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_RowNum1.ColumnHeadersHeight = 31;
            this.dataGridView_RowNum1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTimePeriod,
            this.ColAuditDate,
            this.ColAuditResult});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_RowNum1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_RowNum1.Location = new System.Drawing.Point(15, 261);
            this.dataGridView_RowNum1.Name = "dataGridView_RowNum1";
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.dataGridView_RowNum1.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView_RowNum1.Size = new System.Drawing.Size(789, 190);
            this.dataGridView_RowNum1.TabIndex = 35;
            // 
            // ColTimePeriod
            // 
            this.ColTimePeriod.HeaderText = "TimePeriod";
            this.ColTimePeriod.Name = "ColTimePeriod";
            // 
            // ColAuditDate
            // 
            this.ColAuditDate.HeaderText = "Audit Date";
            this.ColAuditDate.Name = "ColAuditDate";
            // 
            // ColAuditResult
            // 
            this.ColAuditResult.HeaderText = "Audit Result";
            this.ColAuditResult.Name = "ColAuditResult";
            // 
            // AuditHistoryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 463);
            this.Controls.Add(this.dataGridView_RowNum1);
            this.Controls.Add(this.grdStoresValid);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboStore);
            this.Controls.Add(this.txtStoreValue);
            this.Controls.Add(this.lblStoreID);
            this.Controls.Add(this.txtDistributors);
            this.Controls.Add(this.lblDistributor);
            this.Controls.Add(this.cboRegion);
            this.Controls.Add(this.cboChannel);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.lblChannel);
            this.Name = "AuditHistoryFrm";
            this.Text = "Audit history";
            ((System.ComponentModel.ISupportInitialize)(this.grdStoresValid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RowNum1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.ComboBox cboChannel;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.ComboBox cboStore;
        private System.Windows.Forms.TextBox txtStoreValue;
        private System.Windows.Forms.Label lblStoreID;
        private System.Windows.Forms.TextBox txtDistributors;
        private System.Windows.Forms.Label lblDistributor;
        private System.Windows.Forms.Button btnSearch;
        private UKPI.Controls.DataGridView_RowNum grdStoresValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private UKPI.Controls.DataGridView_RowNum dataGridView_RowNum1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTimePeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAuditDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAuditResult;

    }
}