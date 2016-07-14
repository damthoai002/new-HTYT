namespace UKPI.Presentation.ApproveTSLookup
{
    partial class ReportInDonThuocBH
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
            this.htytDataSet11 = new UKPI.HTYTHomebaseDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.htytDataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // htytDataSet11
            // 
            this.htytDataSet11.DataSetName = "HTYTDataSet1";
            this.htytDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(13, 13);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(983, 537);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReportInDonThuocBH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportInDonThuocBH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Dơn Thuốc";
            this.Load += new System.EventHandler(this.ReportInDonThuocBH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.htytDataSet11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HTYTHomebaseDataSet htytDataSet11;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;



    }
}