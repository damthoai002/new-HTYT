using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UKPI.BusinessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.Presentation.ApproveTSLookup
{
    public partial class ReviewTimesheets : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ReviewTimesheets));
        public ClsCreateTimesheet objTimesheet { get; set; }

        public ReviewTimesheets()
        {
            InitializeComponent();

        }
        public ReviewTimesheets( ClsCreateTimesheet ts)
        {
            this.objTimesheet = ts;
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            lblTuan.Text = objTimesheet.TuanLamViec;
            var ctsBo = new CreateTimesheetBo();
            grdStores.DataSource = ctsBo.ViewTimesheet(this.objTimesheet.NhomId, this.objTimesheet.TruongNhomId,
                this.objTimesheet.TuNgay, this.objTimesheet.DenNgay);
            grdStores.ReadOnly = true;
        }
    }
}
