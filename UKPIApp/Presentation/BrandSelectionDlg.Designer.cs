namespace UKPI.Presentation
{
    partial class BrandSelectionDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrandSelectionDlg));
            this.txtMarketID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMarketName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrandID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBrandName = new System.Windows.Forms.TextBox();
            this.btnSearch = new DotNetSkin.SkinControls.SkinButton();
            this.btnCancel = new DotNetSkin.SkinControls.SkinButton();
            this.btnOk = new DotNetSkin.SkinControls.SkinButton();
            this.grdBrand = new UKPI.Controls.DataGridView_RowNum();
            this.ColMarketID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMarketName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBrandID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdBrand)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMarketID
            // 
            this.txtMarketID.Location = new System.Drawing.Point(72, 6);
            this.txtMarketID.Name = "txtMarketID";
            this.txtMarketID.Size = new System.Drawing.Size(171, 20);
            this.txtMarketID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Market ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Market Name";
            // 
            // txtMarketName
            // 
            this.txtMarketName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMarketName.Location = new System.Drawing.Point(326, 6);
            this.txtMarketName.Name = "txtMarketName";
            this.txtMarketName.Size = new System.Drawing.Size(286, 20);
            this.txtMarketName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Brand ID";
            // 
            // txtBrandID
            // 
            this.txtBrandID.Location = new System.Drawing.Point(72, 39);
            this.txtBrandID.Name = "txtBrandID";
            this.txtBrandID.Size = new System.Drawing.Size(171, 20);
            this.txtBrandID.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Brand Name";
            // 
            // txtBrandName
            // 
            this.txtBrandName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBrandName.Location = new System.Drawing.Point(326, 39);
            this.txtBrandName.Name = "txtBrandName";
            this.txtBrandName.Size = new System.Drawing.Size(286, 20);
            this.txtBrandName.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(527, 65);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 24);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(527, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(436, 339);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "&OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grdBrand
            // 
            this.grdBrand.AllowUserToAddRows = false;
            this.grdBrand.AllowUserToDeleteRows = false;
            this.grdBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBrand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBrand.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMarketID,
            this.ColMarketName,
            this.ColBrandID,
            this.ColBrandName});
            this.grdBrand.Location = new System.Drawing.Point(15, 95);
            this.grdBrand.MultiSelect = false;
            this.grdBrand.Name = "grdBrand";
            this.grdBrand.ReadOnly = true;
            this.grdBrand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBrand.Size = new System.Drawing.Size(597, 238);
            this.grdBrand.TabIndex = 15;
            // 
            // ColMarketID
            // 
            this.ColMarketID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColMarketID.DataPropertyName = "MARKET_ID";
            this.ColMarketID.HeaderText = "Market ID";
            this.ColMarketID.Name = "ColMarketID";
            this.ColMarketID.ReadOnly = true;
            // 
            // ColMarketName
            // 
            this.ColMarketName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColMarketName.DataPropertyName = "MARKET_NAME";
            this.ColMarketName.HeaderText = "Market Name";
            this.ColMarketName.Name = "ColMarketName";
            this.ColMarketName.ReadOnly = true;
            // 
            // ColBrandID
            // 
            this.ColBrandID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColBrandID.DataPropertyName = "BRAND_ID";
            this.ColBrandID.HeaderText = "Brand ID";
            this.ColBrandID.Name = "ColBrandID";
            this.ColBrandID.ReadOnly = true;
            // 
            // ColBrandName
            // 
            this.ColBrandName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColBrandName.DataPropertyName = "BRAND_NAME";
            this.ColBrandName.HeaderText = "Brand Name";
            this.ColBrandName.Name = "ColBrandName";
            this.ColBrandName.ReadOnly = true;
            // 
            // BrandSelectionDlg
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(624, 375);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grdBrand);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBrandName);
            this.Controls.Add(this.txtMarketName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBrandID);
            this.Controls.Add(this.txtMarketID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BrandSelectionDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Brand";
            this.Load += new System.EventHandler(this.BrandSelectionDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBrand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMarketID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMarketName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrandID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBrandName;
        private DotNetSkin.SkinControls.SkinButton btnSearch;
        private UKPI.Controls.DataGridView_RowNum grdBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMarketID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMarketName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBrandID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBrandName;
        private DotNetSkin.SkinControls.SkinButton btnCancel;
        private DotNetSkin.SkinControls.SkinButton btnOk;
    }
}