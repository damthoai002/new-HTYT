using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using UKPI.BusinessObject.Authenticate;
using UKPI.Utils;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for frmHistoryTracking.
	/// </summary>
	public class frmHistoryTracking : System.Windows.Forms.Form
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(frmHistoryTracking));
		private int iTableIndex =0;private int iOperationIndex =0;
		private string userName =""; private string createdTime =""; private string updatedTime ="";
		private bool change = true;
		#region Windows variables
		private CurrencyManager m_manager = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private DotNetSkin.SkinControls.SkinButton btnClose;
		private System.Windows.Forms.Button btnSeach;
		private System.Windows.Forms.TextBox txtUPLIFTUser;
		private System.Windows.Forms.DateTimePicker dtpLastUpdate;
		private System.Windows.Forms.ComboBox cboTable;
		private System.Windows.Forms.ComboBox cboOperation;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblTable;
		private System.Windows.Forms.Label lblOperation;
		private System.Windows.Forms.Label lblCreateDate;
		private System.Windows.Forms.Label lblLastUpdate;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.DateTimePicker dtpCreateTime;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.DataGrid grdDetail;
		private System.Windows.Forms.Panel pnlMain;
		private clsTrackingUserBO tk_BO = new clsTrackingUserBO();
		private System.Windows.Forms.DataGrid grdGeneral;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn colCreate_User;
		private System.Windows.Forms.DataGridTextBoxColumn colCreate_Date;
		private System.Windows.Forms.DataGridTextBoxColumn colUpdate_User;
		private System.Windows.Forms.DataGridTextBoxColumn colUpdate_Time;
		private System.Windows.Forms.DataGridTextBoxColumn colTableName;
		private System.Windows.Forms.DataGridTableStyle stlDetail;
		private System.Windows.Forms.DataGridTextBoxColumn column1;
		private System.Windows.Forms.DataGridTextBoxColumn column2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		public frmHistoryTracking()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			clsCommon.RegAutoSizeCol(grdGeneral);
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmHistoryTracking));
			this.btnSeach = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dtpCreateTime = new System.Windows.Forms.DateTimePicker();
			this.dtpLastUpdate = new System.Windows.Forms.DateTimePicker();
			this.cboTable = new System.Windows.Forms.ComboBox();
			this.cboOperation = new System.Windows.Forms.ComboBox();
			this.txtUPLIFTUser = new System.Windows.Forms.TextBox();
			this.lblTable = new System.Windows.Forms.Label();
			this.lblOperation = new System.Windows.Forms.Label();
			this.lblCreateDate = new System.Windows.Forms.Label();
			this.lblLastUpdate = new System.Windows.Forms.Label();
			this.lblUserName = new System.Windows.Forms.Label();
			this.btnClose = new DotNetSkin.SkinControls.SkinButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.grdDetail = new System.Windows.Forms.DataGrid();
			this.stlDetail = new System.Windows.Forms.DataGridTableStyle();
			this.column1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.column2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.grdGeneral = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.colCreate_User = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colCreate_Date = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colUpdate_User = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colUpdate_Time = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colTableName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdGeneral)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSeach
			// 
			this.btnSeach.Image = ((System.Drawing.Image)(resources.GetObject("btnSeach.Image")));
			this.btnSeach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSeach.Location = new System.Drawing.Point(616, 26);
			this.btnSeach.Name = "btnSeach";
			this.btnSeach.Size = new System.Drawing.Size(88, 24);
			this.btnSeach.TabIndex = 0;
			this.btnSeach.Text = "Search";
			this.btnSeach.Click += new System.EventHandler(this.btnSeach_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.dtpCreateTime);
			this.groupBox1.Controls.Add(this.dtpLastUpdate);
			this.groupBox1.Controls.Add(this.cboTable);
			this.groupBox1.Controls.Add(this.cboOperation);
			this.groupBox1.Controls.Add(this.txtUPLIFTUser);
			this.groupBox1.Controls.Add(this.lblTable);
			this.groupBox1.Controls.Add(this.lblOperation);
			this.groupBox1.Controls.Add(this.lblCreateDate);
			this.groupBox1.Controls.Add(this.lblLastUpdate);
			this.groupBox1.Controls.Add(this.lblUserName);
			this.groupBox1.Controls.Add(this.btnSeach);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(776, 72);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// dtpCreateTime
			// 
			this.dtpCreateTime.Checked = false;
			this.dtpCreateTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpCreateTime.Location = new System.Drawing.Point(80, 40);
			this.dtpCreateTime.Name = "dtpCreateTime";
			this.dtpCreateTime.ShowCheckBox = true;
			this.dtpCreateTime.Size = new System.Drawing.Size(120, 20);
			this.dtpCreateTime.TabIndex = 3;
			this.dtpCreateTime.ValueChanged += new System.EventHandler(this.dtpCreateTime_ValueChanged);
			// 
			// dtpLastUpdate
			// 
			this.dtpLastUpdate.Checked = false;
			this.dtpLastUpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpLastUpdate.Location = new System.Drawing.Point(288, 40);
			this.dtpLastUpdate.Name = "dtpLastUpdate";
			this.dtpLastUpdate.ShowCheckBox = true;
			this.dtpLastUpdate.Size = new System.Drawing.Size(120, 20);
			this.dtpLastUpdate.TabIndex = 4;
			this.dtpLastUpdate.ValueChanged += new System.EventHandler(this.dtpLastUpdate_ValueChanged);
			// 
			// cboTable
			// 
			this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTable.Items.AddRange(new object[] {
														  "[ALL]",
														  "FPT_ENV_DELIVERY_WEIGHT",
														  "FPT_ENV_SKU_PRIORITY",
														  "FPT_ENV_SKU_MASTER",
														  "FPT_ENV_PROMOTION",
														  "FPT_ENV_PROMOTION_WEEK",
														  "FPT_ENV_PROMOTION_CUST_WEEK",
														  "FPT_ENV_PROMOTION_CUST",
														  "FPT_ENV_PROMOTION_REGION",
														  "FPT_ENV_SKU_PRIORITY_DETAIL",
														  "FPT_ENV_PROMOTION_CUST",
														  "FPT_ENV_DEFINE_ORDER_SPLIT_HEADER",
														  "FPT_ENV_DEFINE_ORDER_SPLIT_DETAIL",
														  "FPT_ENV_SP_SPECIAL",
														  "FPT_ENV_SP_STANDARD",
														  "FPT_ENV_PROMOTION_REGION_SWAP",
														  "FPT_ENV_PROMOTION_CUST_SWAP"});
			this.cboTable.Location = new System.Drawing.Point(360, 16);
			this.cboTable.Name = "cboTable";
			this.cboTable.Size = new System.Drawing.Size(232, 21);
			this.cboTable.TabIndex = 2;
			// 
			// cboOperation
			// 
			this.cboOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperation.Items.AddRange(new object[] {
															  "[ALL]",
															  "Added",
															  "Modified"});
			this.cboOperation.Location = new System.Drawing.Point(472, 40);
			this.cboOperation.Name = "cboOperation";
			this.cboOperation.Size = new System.Drawing.Size(120, 21);
			this.cboOperation.TabIndex = 5;
			// 
			// txtUPLIFTUser
			// 
			this.txtUPLIFTUser.Location = new System.Drawing.Point(80, 16);
			this.txtUPLIFTUser.MaxLength = 200;
			this.txtUPLIFTUser.Name = "txtUPLIFTUser";
			this.txtUPLIFTUser.Size = new System.Drawing.Size(176, 20);
			this.txtUPLIFTUser.TabIndex = 1;
			this.txtUPLIFTUser.Text = "";
			// 
			// lblTable
			// 
			this.lblTable.Location = new System.Drawing.Point(291, 14);
			this.lblTable.Name = "lblTable";
			this.lblTable.TabIndex = 5;
			this.lblTable.Text = "Table Name";
			this.lblTable.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lblOperation
			// 
			this.lblOperation.Location = new System.Drawing.Point(416, 39);
			this.lblOperation.Name = "lblOperation";
			this.lblOperation.TabIndex = 4;
			this.lblOperation.Text = "Operation";
			this.lblOperation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lblCreateDate
			// 
			this.lblCreateDate.Location = new System.Drawing.Point(8, 38);
			this.lblCreateDate.Name = "lblCreateDate";
			this.lblCreateDate.TabIndex = 3;
			this.lblCreateDate.Text = "Created Date";
			this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lblLastUpdate
			// 
			this.lblLastUpdate.Location = new System.Drawing.Point(216, 38);
			this.lblLastUpdate.Name = "lblLastUpdate";
			this.lblLastUpdate.TabIndex = 2;
			this.lblLastUpdate.Text = "Last Updated";
			this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lblUserName
			// 
			this.lblUserName.Location = new System.Drawing.Point(8, 14);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.TabIndex = 1;
			this.lblUserName.Text = "User Name";
			this.lblUserName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
			this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnClose.Location = new System.Drawing.Point(672, 16);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Location = new System.Drawing.Point(8, 440);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(776, 48);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 184);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(776, 3);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// grdDetail
			// 
			this.grdDetail.DataMember = "";
			this.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdDetail.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grdDetail.Location = new System.Drawing.Point(0, 184);
			this.grdDetail.Name = "grdDetail";
			this.grdDetail.ReadOnly = true;
			this.grdDetail.Size = new System.Drawing.Size(776, 160);
			this.grdDetail.TabIndex = 2;
			this.grdDetail.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								  this.stlDetail});
			// 
			// stlDetail
			// 
			this.stlDetail.DataGrid = this.grdDetail;
			this.stlDetail.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																										this.column1,
																										this.column2});
			this.stlDetail.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.stlDetail.MappingName = "";
			// 
			// column1
			// 
			this.column1.Format = "";
			this.column1.FormatInfo = null;
			this.column1.MappingName = "";
			this.column1.Width = 75;
			// 
			// column2
			// 
			this.column2.Format = "";
			this.column2.FormatInfo = null;
			this.column2.MappingName = "";
			this.column2.Width = 75;
			// 
			// grdGeneral
			// 
			this.grdGeneral.DataMember = "";
			this.grdGeneral.Dock = System.Windows.Forms.DockStyle.Top;
			this.grdGeneral.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grdGeneral.Location = new System.Drawing.Point(0, 0);
			this.grdGeneral.Name = "grdGeneral";
			this.grdGeneral.ReadOnly = true;
			this.grdGeneral.Size = new System.Drawing.Size(776, 184);
			this.grdGeneral.TabIndex = 1;
			this.grdGeneral.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								   this.dataGridTableStyle1});
			this.grdGeneral.Click += new System.EventHandler(this.grdGeneral_Click);
			this.grdGeneral.CurrentCellChanged += new System.EventHandler(this.grdGeneral_CurrentCellChanged);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.grdGeneral;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.colCreate_User,
																												  this.colCreate_Date,
																												  this.colUpdate_User,
																												  this.colUpdate_Time,
																												  this.colTableName});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "";
			// 
			// colCreate_User
			// 
			this.colCreate_User.Format = "";
			this.colCreate_User.FormatInfo = null;
			this.colCreate_User.HeaderText = "Created By";
			this.colCreate_User.MappingName = "CREATE_USER";
			this.colCreate_User.ReadOnly = true;
			this.colCreate_User.Width = 75;
			// 
			// colCreate_Date
			// 
			this.colCreate_Date.Format = "";
			this.colCreate_Date.FormatInfo = null;
			this.colCreate_Date.HeaderText = "Created Date";
			this.colCreate_Date.MappingName = "CREATE_TIME";
			this.colCreate_Date.ReadOnly = true;
			this.colCreate_Date.Width = 75;
			// 
			// colUpdate_User
			// 
			this.colUpdate_User.Format = "";
			this.colUpdate_User.FormatInfo = null;
			this.colUpdate_User.HeaderText = "Last Update By";
			this.colUpdate_User.MappingName = "UPDATE_USER";
			this.colUpdate_User.ReadOnly = true;
			this.colUpdate_User.Width = 75;
			// 
			// colUpdate_Time
			// 
			this.colUpdate_Time.Format = "";
			this.colUpdate_Time.FormatInfo = null;
			this.colUpdate_Time.HeaderText = "Last Update Time";
			this.colUpdate_Time.MappingName = "UPDATE_TIME";
			this.colUpdate_Time.ReadOnly = true;
			this.colUpdate_Time.Width = 75;
			// 
			// colTableName
			// 
			this.colTableName.Format = "";
			this.colTableName.FormatInfo = null;
			this.colTableName.HeaderText = "Table Name";
			this.colTableName.MappingName = "TABLE_NAME";
			this.colTableName.ReadOnly = true;
			this.colTableName.Width = 75;
			// 
			// pnlMain
			// 
			this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.Controls.Add(this.splitter1);
			this.pnlMain.Controls.Add(this.grdDetail);
			this.pnlMain.Controls.Add(this.grdGeneral);
			this.pnlMain.Location = new System.Drawing.Point(8, 88);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(776, 344);
			this.pnlMain.TabIndex = 5;
			// 
			// frmHistoryTracking
			// 
			this.AcceptButton = this.btnSeach;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(792, 493);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pnlMain);
			this.Name = "frmHistoryTracking";
			this.Text = "History Tracking";
			this.Load += new System.EventHandler(this.frmHistoryTracking_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdGeneral)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Visual DataGrid
		private void CheckChange()
		{
			if(txtUPLIFTUser.Text.Trim() != userName)
			{
				change = true;
				return;
			}
			if(cboTable.SelectedIndex != iTableIndex)
			{
				change = true;
				return;
			}
			if(cboOperation.SelectedIndex != iOperationIndex)
			{
				change = true;
				return;}
			if(txtUPLIFTUser.Text != userName)
			{
				change = true;
				return;}
		}
		private void DataGridResize(ref DataGrid grd, int iColumnCount)
		{
			grd.BeginInit();
			foreach(DataGridTableStyle grdStyle in grd.TableStyles)
			{
				try
				{
					int width = grd.Width - 56;//53;//real is 56
					GridColumnStylesCollection cols = grdStyle.GridColumnStyles;
					int oldwidth = 0;
					for(byte i =0;i <iColumnCount;i++)
					{
						oldwidth = oldwidth +cols[i].Width;// col.Width;
					}
					if(oldwidth == 0)
						return;
					int count = grdStyle.GridColumnStyles.Count;
					double scale = 1.0*width/oldwidth;
					foreach(DataGridColumnStyle col in cols)
					{
						col.Width = (int)(col.Width * scale);
					}
				}
				catch(Exception ex)
				{
					log.Error(ex.Message,ex);
				}
			}
			grd.EndInit();
		}

		
		private void ShowDetail(DataTable dt)
		{
			// Clear datasource
			grdDetail.DataSource = null;
			CreateColumn(dt);
			int iColumnCount = InitHeaderText(dt);
			DataGridResize(ref grdDetail,iColumnCount);
			grdDetail.DataSource = dt;
		}
		private void CreateColumn(DataTable dt)
		{
			int iColumnCount = dt.Columns.Count - 4;//Exclude four columns of tracking
			grdDetail.TableStyles[0].GridColumnStyles.Clear();// Repare for add new columns collections
			for(byte i = 0; i < iColumnCount; i++)
			{
				DataGridTextBoxColumn  col = new DataGridTextBoxColumn();
				col.NullText = "";
				grdDetail.TableStyles[0].GridColumnStyles.Add(col);
			} 
		}
		private int InitHeaderText(DataTable dt)
		{
			int iColumIndex =0;
			ClearMappingName(ref grdDetail);
			foreach(DataColumn col in dt.Columns)
			{
				if(!col.Caption.Equals("CREATE_USER") && !col.Caption.Equals("CREATE_TIME") && !col.Caption.Equals("UPDATE_USER") && !col.Caption.Equals("UPDATE_TIME")&&
					!col.Caption.Equals("CREATED_BY")&& !col.Caption.Equals("CREATED_DATE") && !col.Caption.Equals("LAST_UPDATED_BY") && !col.Caption.Equals("LAST_UPDATED_DATE"))
				{
					try
					{
						grdDetail.TableStyles[0].GridColumnStyles[iColumIndex].HeaderText = col.Caption;
						// set Mapping data
						grdDetail.TableStyles[0].GridColumnStyles[iColumIndex].MappingName = col.Caption;
					}
					catch(Exception ex)
					{
						log.Error(ex.Message,ex);
					}
					iColumIndex ++;
				}
			}
			return iColumIndex;
		}
		private void ClearMappingName(ref DataGrid grd)
		{
			for(byte i =0; i < grd.TableStyles[0].GridColumnStyles.Count;i++)
			{
				grd.TableStyles[0].GridColumnStyles[i].MappingName = null;
			}
		}
		#endregion

		#region Event
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnSeach_Click(object sender, System.EventArgs e)
		{CheckChange();
			if(change)
			{
				#region for trace the change
				change = false;
				if(cboTable.SelectedIndex == -1) cboTable.SelectedIndex =0;
				if(cboOperation.SelectedIndex == -1) cboOperation.SelectedIndex =0;
				userName = txtUPLIFTUser.Text; if(dtpCreateTime.Checked)createdTime = dtpCreateTime.Value.ToString();
				if(dtpLastUpdate.Checked)updatedTime = dtpLastUpdate.Value.ToString();
				iTableIndex = cboTable.SelectedIndex; iOperationIndex = cboOperation.SelectedIndex;
				#endregion
				string createDate ="";string updateDate = "";
				if(dtpCreateTime.Checked)
					createDate = clsCommon.FormatddMMyyy(dtpCreateTime.Value);
				if(dtpLastUpdate.Checked)
					updateDate = clsCommon.FormatddMMyyy(dtpLastUpdate.Value);
				DataTable dt = tk_BO.SearchTrackingUser(txtUPLIFTUser.Text.Trim(),cboTable.Text.Trim(),
					cboOperation.Text.Trim(),createDate,updateDate);
				if(dt!= null)
				{
					m_manager = (CurrencyManager)this.BindingContext[dt];
					dt.DefaultView.AllowNew = false;
					grdGeneral.DataSource = dt;
				}
				else
					grdGeneral.DataSource = null;
				grdDetail.DataSource = null;
			}
		}
		private void grdGeneral_Click(object sender, System.EventArgs e)
		{
			int iRowsIndex = m_manager.Position;
			if(iRowsIndex >=0)
			{
				try
				{
					grdGeneral.Select(iRowsIndex);
					string createUser = grdGeneral[iRowsIndex,0].ToString();
					string createTime = grdGeneral[iRowsIndex,1].ToString();
					string updateUser = grdGeneral[iRowsIndex,2].ToString();
					string updateTime = grdGeneral[iRowsIndex,3].ToString();
					string tableName = grdGeneral[iRowsIndex,4].ToString();
					DataTable dt = tk_BO.TrackingDetail(createUser,createTime,updateUser,updateTime,tableName);
					if(dt != null)
					{
//						m_manager = (CurrencyManager)this.BindingContext[dt];
//						dt.DefaultView.AllowNew = false;
						ShowDetail(dt);
					}
						
				}
				catch(Exception ex)
				{
					log.Error(ex.Message,ex);
				}
			}
		}
		private void grdGeneral_CurrentCellChanged(object sender, System.EventArgs e)
		{
			
			int iRowsIndex = m_manager.Position;
			grdGeneral_Click(sender,e);
		}
		private void dtpCreateTime_ValueChanged(object sender, System.EventArgs e)
		{
			change = true;
		}

		private void dtpLastUpdate_ValueChanged(object sender, System.EventArgs e)
		{
			change = true;
		}
		private void frmHistoryTracking_Load(object sender, System.EventArgs e)
		{
			txtUPLIFTUser.Focus();
		}
		#endregion

		
	}
}
