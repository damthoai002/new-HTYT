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

namespace UKPI.Presentation.ApproveTSLookup
{
    public partial class RejectedTimesheet : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(RejectedTimesheet));
        public string StrSysId { get; set; }

        public RejectedTimesheet()
        {
            InitializeComponent();
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRejected.Text == "")
                {
                    MessageBox.Show(this, clsResources.GetMessage("errors.ApproverTimesheet.Blank"),
                       clsResources.GetMessage("message.Others.Message"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ep.SetError(txtRejected, clsResources.GetMessage("errors.ApproverTimesheet.Blank"));
                    txtRejected.Focus();
                    return;
                }
                var note = clsSystemConfig.UserName + ": " + txtRejected.Text + " - ";
                var lastupdate = DateTime.Now.ToString(clsCommon.ApproveTimesheet.DateFormatDb);
                var lastUpId = clsSystemConfig.UserName;
                var level = clsSystemConfig.LevelQuanLy.ToString();
                var lichLamViec = new ChamCongLichLamViecBo();
                lichLamViec.RejectedChamCongLichLamViec(this.StrSysId, note, lastupdate, lastUpId, level);
                MessageBox.Show(clsResources.GetMessage("message.RejectSuccessfull"),
                       clsResources.GetMessage("warnings.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message);
                this.Close();
            }


        }
    }
}
