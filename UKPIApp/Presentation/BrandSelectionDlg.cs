using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UKPI.Utils;
using UKPI.BusinessObject;
using UKPI.DataAccessObject;

namespace UKPI.Presentation
{
    public partial class BrandSelectionDlg : Form
    {
        private ProductBO _productBO = new ProductBO();
        private clsCommon _common = new clsCommon();

        public BrandSelectionDlg()
        {
            InitializeComponent();
            this.grdBrand.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchBrand();
        }

        private void SearchBrand()
        {
            string marketID = _common.EncodeString(txtMarketID.Text.Trim());
            string marketName = _common.EncodeString(txtMarketName.Text.Trim());
            string brandID = _common.EncodeString(txtBrandID.Text.Trim());
            string brandName = _common.EncodeString(txtBrandName.Text.Trim());

            DataTable dtBrand = _productBO.GetBrand(marketID, marketName, brandID, brandName);
            grdBrand.DataSource = dtBrand;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Gets selected Brand with schema: MARKET_ID, MARKET_NAME, BRAND_ID, BRAND_NAME
        /// </summary>
        public System.Data.DataRow SelectedBrand
        {
            get
            {
                if (grdBrand.CurrentRow == null)
                {
                    return null;
                }

                return (grdBrand.CurrentRow.DataBoundItem as DataRowView).Row;
            }
        }

        private void BrandSelectionDlg_Load(object sender, EventArgs e)
        {
            this.SearchBrand();
        }
    }
}
