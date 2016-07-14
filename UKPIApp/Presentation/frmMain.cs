using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using UKPI.Presentation;
using UKPI.DataAccessObject;
using UKPI.BusinessObject;
using UKPI.Utils;
using UKPI.Controls;
using DotNetSkin.SkinControls;
using PureComponents.NicePanel;
using MT.WindowsUI.NavigationPane;

namespace UKPI
{
	/// <summary>
	/// Summary description for frmMain
	/// </summary>
	/// <remarks>
	/// Author: Nguyễn Minh Đức. G3.
	/// </remarks>
	public class frmMain : System.Windows.Forms.Form
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(frmMain));
		
		private System.ComponentModel.IContainer components;
		//private Control pnlToolBar;
        //private Panel pnlToolBar;

        #region system tray importing thread variable
        
        private bool m_stopImportingProgress = false;
        private bool m_abortImportingProgress = false;
        private bool m_isOnImportingProgress = false;

        private bool m_forceAbortImportingProgress = false;

        private Thread m_systemTrayImportingThread = null;

        #endregion system tray importing thread variable

        #region Window Control

        private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.ToolTip tip;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip systemTrayContextMenu;
        private ToolStripMenuItem showMainFormToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem stopImportingProgressToolStripMenuItem;
        private ToolStripMenuItem startImportingProgressToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exiToolStripMenuItem;
        private System.Windows.Forms.Timer importProgressTimer;
		private System.Windows.Forms.MainMenu mnuMain;
        //Add by KienTNT for To-Do list bar
        System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer();
        NavigateBar ToDoBar = null;
        int count = 0;
        int iTimer;
        ToDoListBO BO = null;
        //
		#endregion Window Control

		public frmMain()
		{
			InitializeComponent();

            //re-set timer by config
            ResetImportingTimer();

			clsAutUserBO bo = new clsAutUserBO();
			DataTable dt = bo.GetCommonMenu();
			mnuMain = BuildMenu(dt);
			this.Menu = mnuMain;
            BO = new ToDoListBO();
            iTimer = BO.GetTimer();

			#region Old
//			if(clsStyleManager.Aqua)
//				pnlToolBar = clsStyleManager.CreateMainPanel();
//			else
//			{
//				pnlToolBar = new Panel();
//				((Panel)pnlToolBar).DockPadding.Top = 1;
//				((Panel)pnlToolBar).DockPadding.Bottom = 1;
//			}
//			pnlToolBar.Height = 24;
//			pnlToolBar.Dock = DockStyle.Top;
//			pnlToolBar.Visible = false;
//
//			this.Controls.Add(pnlToolBar);

			//clsTitleManager.InitTitle(this, mnuMain);
			#endregion

			try
			{
				string filename = clsSystemConfig.ImageFolder + ConfigurationManager.AppSettings["Background"];
                //if(File.Exists(filename))
                //{
                //    this.BackgroundImage = Image.FromFile(clsSystemConfig.ImageFolder + ConfigurationManager.AppSettings["Background"]);
                //}
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
			}
		}

        //Set timer interval by value getted in config file
        private void ResetImportingTimer()
        {
            string configInterVal = ConfigurationManager.AppSettings["ImportTimerIntervalBySecond"];
            int intervalBySecond = -1;

            if (int.TryParse(configInterVal, out intervalBySecond))
            {
                this.importProgressTimer.Interval = intervalBySecond * 1000;
            }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.systemTrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stopImportingProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startImportingProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importProgressTimer = new System.Windows.Forms.Timer(this.components);
            this.systemTrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.Text = "File";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.systemTrayContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "HHCC";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // systemTrayContextMenu
            // 
            this.systemTrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainFormToolStripMenuItem,
            this.toolStripSeparator1,
            this.stopImportingProgressToolStripMenuItem,
            this.startImportingProgressToolStripMenuItem,
            this.toolStripSeparator2,
            this.exiToolStripMenuItem});
            this.systemTrayContextMenu.Name = "systemTrayContextMenu";
            this.systemTrayContextMenu.Size = new System.Drawing.Size(203, 104);
            // 
            // showMainFormToolStripMenuItem
            // 
            this.showMainFormToolStripMenuItem.Enabled = false;
            this.showMainFormToolStripMenuItem.Name = "showMainFormToolStripMenuItem";
            this.showMainFormToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.showMainFormToolStripMenuItem.Text = "Show main form";
            this.showMainFormToolStripMenuItem.Click += new System.EventHandler(this.showMainFormToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // stopImportingProgressToolStripMenuItem
            // 
            this.stopImportingProgressToolStripMenuItem.Name = "stopImportingProgressToolStripMenuItem";
            this.stopImportingProgressToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.stopImportingProgressToolStripMenuItem.Text = "Stop importing progress";
            this.stopImportingProgressToolStripMenuItem.Click += new System.EventHandler(this.stopImportingProgressToolStripMenuItem_Click);
            // 
            // startImportingProgressToolStripMenuItem
            // 
            this.startImportingProgressToolStripMenuItem.Name = "startImportingProgressToolStripMenuItem";
            this.startImportingProgressToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.startImportingProgressToolStripMenuItem.Text = "Start importing progress";
            this.startImportingProgressToolStripMenuItem.Click += new System.EventHandler(this.startImportingProgressToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // exiToolStripMenuItem
            // 
            this.exiToolStripMenuItem.Name = "exiToolStripMenuItem";
            this.exiToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.exiToolStripMenuItem.Text = "Exit";
            this.exiToolStripMenuItem.Click += new System.EventHandler(this.exiToolStripMenuItem_Click);
            // 
            // importProgressTimer
            // 
            this.importProgressTimer.Interval = 300000;
            this.importProgressTimer.Tick += new System.EventHandler(this.importProgressTimer_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(656, 457);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "HHCC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.systemTrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		[STAThread]
		static void Main() 
		{
			if(clsSystemConfig.Init())
			{
                bool bRunningResult = UKPI.Utils.SingleInstance.SingleApplication.Run(new frmMain());
                //Application.Run(new frmToDoList());

                if (bRunningResult == false)
                {
                    MessageBox.Show(clsResources.GetMessage("messages.System.SystemRunning"),
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
			}
			else
			{
				MessageBox.Show(clsSystemConfig.GetMessage(), "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		//private static string TOOLBAR_BUTTON_INDEX = "TOOLBAR_BUTTON_INDEX";
		private static string TOOLBAR_NAME = "TOOLBAR_NAME";
		private static string FORM_ID = "FORM_ID";
		private static string FORM_NAME = "FORM_NAME";
		private static string MENU_NAME = "MENU_NAME";
		private static string MENU_ZORDER = "MENU_ZORDER";
		//private static string DESCRIPTION = "DESCRIPTION";
		//private static string MENU_PID = "MENU_PID";
		private static string ICON_NAME = "ICON_NAME";
		private static string AssemblyName = "UKPI.Presentation.";
		private static string frmMainName = "frmMain.";

		/// <summary>
		/// Build dynamic menu
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private MainMenu BuildMenu(DataTable dt)
		{
			var mainMenu = new MainMenu();

			DataRow[]rows;
			rows = dt.Select("MENU_PID = 0", MENU_ZORDER);
			MDMenuItem item;
			Font font = this.Font;
			foreach(DataRow row in rows)
			{
				item = new MDMenuItem
				{
				    ID = int.Parse(row[FORM_ID].ToString()),
				    Name = row[MENU_NAME].ToString(),
				    FormName = AssemblyName + row[FORM_NAME].ToString(),
				    Text = clsResources.GetTitle(frmMainName + row[MENU_NAME].ToString()),
				    Font = font
				};

			    if(item.Name == clsConstants.MENU_WINDOWS)
				{
					item.MdiList = true;
				}

				mainMenu.MenuItems.Add(item);
				AddSub(item, dt);
			}
			return mainMenu;
		}

		/// <summary>
		/// Add sub menu item on recursive
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="dt"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void AddSub(MDMenuItem parent, DataTable dt)
		{
			DataRow [] rows = dt.Select(string.Format("MENU_PID = {0}", parent.ID), MENU_ZORDER);
			if(rows.Length > 0)
			{
				MDMenuItem item;
				string folder = clsSystemConfig.IconFolder;
				string iconName = null;
				Size size = new Size(16, 16);
				foreach(DataRow row in rows)
				{
					item = new MDMenuItem();
					item.Font = parent.Font;
					item.OwnerDraw = true;
					item.Name = row[MENU_NAME].ToString();
					iconName = row[ICON_NAME].ToString();
					if(iconName.Length > 0)
					{
						string filename = folder + iconName;
						if(File.Exists(filename))
						{
							try
							{
								Icon icon = new Icon(filename);
								item.Icon = icon;
							}
							catch(Exception ex)
							{
								log.Error(ex.Message, ex);
							}
						}
					}
					if(item.Name == clsConstants.MENU_SEPARATE)
					{
						item.OwnerDraw = false;
						item.Text = clsConstants.MINUS;
						parent.MenuItems.Add(item);
					}
					else if(item.Name == clsConstants.MAXIMIZED)
					{
						item.Text = clsResources.GetTitle(frmMainName + row[MENU_NAME].ToString());

						parent.MenuItems.Add(item);
						item.Checked = clsFormManager.Maximized;
						item.Click += new EventHandler(MenuItem_OnClick);
					}
					else if(item.Name == clsConstants.SYSTEM_STYLE)
					{
						item.Text = clsResources.GetTitle(frmMainName + row[MENU_NAME].ToString());

						parent.MenuItems.Add(item);
						item.Checked = clsStyleManager.SystemStyle;
						item.Click += new EventHandler(MenuItem_OnClick);
					}
					else
					{
						item.ID = int.Parse(row[FORM_ID].ToString());
						string formName = row[FORM_NAME].ToString();
						if(formName.Length > 0)
							item.FormName = AssemblyName + formName;
						else
							item.FormName = "";

						item.Text = clsResources.GetTitle(frmMainName + row[MENU_NAME].ToString());
						parent.MenuItems.Add(item);
						AddSub(item, dt);
					}
				}
			}
			else
			{
				parent.Click +=new EventHandler(MenuItem_OnClick);
			}
		}

		/// <summary>
		/// Handle events when click on menu Item (Open window and other action).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void MenuItem_OnClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Excel files|*.xls";
            openFileDlg.Title = "Choose a Excel file to import";

			string formName = "";

			MDMenuItem item = (MDMenuItem)sender;
			if(clsConstants.EXIT == item.Name)
			{
				this.Close();
			}
            else if (clsConstants.MENU_HELP_TOPIC == item.Name)
            {

            }
            else if (clsConstants.WINDOW_CASCADE == item.Name)
            {
                this.LayoutMdi(MdiLayout.Cascade);
            }
            else if (clsConstants.WINDOW_TILE_HOZ == item.Name)
            {
                this.LayoutMdi(MdiLayout.TileHorizontal);
            }
            else if (clsConstants.WINDOW_TILE_VERT == item.Name)
            {
                this.LayoutMdi(MdiLayout.TileVertical);
            }
            else if (clsConstants.WINDOW_CLOSE_ALL == item.Name)
            {
                CloseAllWindow();
            }
            else if (clsConstants.SYSTEM_STYLE == item.Name)
            {
                item.Checked = !item.Checked;
                clsStyleManager.SystemStyle = item.Checked;
            }
            else if (clsConstants.COLOR_FOCUS_CONTROL == item.Name)
            {
                item.Checked = !item.Checked;
                clsStyleManager.ColorFocusControl = item.Checked;
            }
            else if (clsConstants.MAXIMIZED == item.Name)
            {
                item.Checked = !item.Checked;
                clsFormManager.Maximized = item.Checked;
            }
            else if (clsConstants.SET_ENGLISH_LANGUAGE == item.Name)
            {
                clsResources.Init(clsConstants.ENGLISH);

                clsTitleManager.InitToolBarToolTip(this);
                //				tip.RemoveAll();
                //				foreach(Control ctrl in pnlToolBar.Controls)
                //				{
                //					tip.SetToolTip(ctrl, clsResources.GetTitle(frmMainName + ctrl.Name));
                //				}
                clsTitleManager.InitTitle(this, this.Menu);
                foreach (Form frm in this.MdiChildren)
                {
                    clsTitleManager.InitTitle(frm);
                }
            }
            else if (clsConstants.SET_VIETNAMESE_LANGUAGE == item.Name)
            {
                clsResources.Init(clsConstants.VIETNAMESE);
                clsTitleManager.InitToolBarToolTip(this);
                //				tip.RemoveAll();
                //				foreach(Control ctrl in pnlToolBar.Controls)
                //				{
                //					tip.SetToolTip(ctrl, clsResources.GetTitle(frmMainName + ctrl.Name));
                //				}
                clsTitleManager.InitTitle(this, this.Menu);
                foreach (Form frm in this.MdiChildren)
                {
                    clsTitleManager.InitTitle(frm);
                }
            }
            else if (clsConstants.LOG_OUT == item.Name)            
            {
                //Sonlv 
                stopImportingProgressToolStripMenuItem.Enabled = false;
                startImportingProgressToolStripMenuItem.Enabled = false;

                if (MessageBox.Show(clsResources.GetMessage("messages.logout"), clsResources.GetMessage("messages.general"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //PhongNTT - abort import progress
                    AbortSystemTrayImportingThread();
                    
                    CloseAllWindow();
                    this.Text = clsResources.GetTitle("frmMain.Title");
                    this.Menu = mnuMain;
                    //pnlToolBar.Visible = false;
                    clsCommon.RemoveAllToolBar(this);
                    //KienTNT - remove todo list
                    this.Controls.Remove(ToDoBar);
                    mnuLogin_Click(null, null);
                }
            }
            else if (clsConstants.IMPORT_STORE == item.Name)
            {
                //Process import store data
                if (openFileDlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                   // clsStoreBO boStore = new clsStoreBO();
                    
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (openFileDlg.FileName != null && openFileDlg.CheckFileExists == true)
                        {

                            string[] errorMsgs = null;
                            //boStore.ImportStoreDataFromFile(openFileDlg.FileName, false, out errorMsgs);
                           
                            if (errorMsgs.Length == 0)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show(clsResources.GetMessage("messages.importStore.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                string msg = clsResources.GetMessage("messages.importStore.finishWithRowsError") + Environment.NewLine +
                                    string.Join(Environment.NewLine, errorMsgs);

                                MessageBox.Show(msg, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            else if (clsConstants.IMPORT_STORE_FROM_DMS == item.Name)
            {
                //Process import store data by data from DMS
                if (openFileDlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    //clsStoreBO boStore = new clsStoreBO();

                    try
                    {
                        if (openFileDlg.FileName != null && openFileDlg.CheckFileExists == true)
                        {

                            string[] errorMsgs = null;
                          //  boStore.ImportStoreDataFromFile(openFileDlg.FileName, true, out errorMsgs);

                            if (errorMsgs.Length == 0)
                            {
                                MessageBox.Show(clsResources.GetMessage("messages.importStore.success"), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string msg = clsResources.GetMessage("messages.importStore.finishWithRowsError") + Environment.NewLine +
                                    string.Join(Environment.NewLine, errorMsgs);

                                MessageBox.Show(msg, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, clsResources.GetMessage("errors.general"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (clsConstants.IMPORT_PRODUCT == item.Name)
            {
                //Process import product data
            }
            else if (clsConstants.IMPORT_DISTRIBUTOR == item.Name)
            {
                //Process import distributor data
            }
            else if (clsConstants.IMPORT_BP == item.Name)
            {
                //Process import branch personnel data
            }
            else if (clsConstants.EXPORT_KPI == item.Name)
            {
                //Process export KPI data
            }
            else
            {
                formName = item.FormName;
                if (formName.Length > 0 && formName != AssemblyName)
                {
                    if (formName == "UKPI.Presentation.frmLogin")
                    {
                        mnuLogin_Click(null, null);
                    }
                    else
                    {
                        Form frm = null;
                        try
                        {

                           frm = (Form)Activator.CreateInstance(Type.GetType(formName), null);

                         //   frm = (Form)Activator.CreateInstance(Type.GetType("UKPI.Presentation.FrmApproveTimesheetL1"), null);
                            
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Message, ex);
                            MessageBox.Show(clsResources.GetMessage("errors.form.create", formName));
                        }
                        if (frm != null)
                        {
                            clsFormManager.ShowMDIChild(frm);
                        }
                    }
                }
            }
		}

		/// <summary>
		/// Create tool bar
		/// </summary>
		/// <param name="dt"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void CreateToolBar(DataTable dt)
		{
			clsCommon.RemoveAllToolBar(this);
			ArrayList ctrls = new ArrayList();
			string otbName = null;
			string tbName = "";
			string description = null;
			string iconName = null;
			string filename;
			string folder = clsSystemConfig.IconFolder;
			Size size = new Size(16, 16);
			ToolBar tb = null;
			MDToolBarButton tbb = null;
			ImageList imgs = null;
			DataRow []rows = dt.Select("TOOLBAR_BUTTON_INDEX >= 0", "TOOLBAR_NAME, TOOLBAR_BUTTON_INDEX");
			int index = 0;
			foreach(DataRow row in rows)
			{
				tbb = new MDToolBarButton();
				tbName = row[TOOLBAR_NAME].ToString();
				iconName = row[ICON_NAME].ToString();
				if(tbName != otbName)
				{
					tb = new ToolBar();
					tb.AutoSize = false;
					tb.Height = size.Height + 10;
					tb.ButtonSize = size;
					imgs = new ImageList();
					imgs.ColorDepth = ColorDepth.Depth32Bit;
					imgs.ImageSize = size;
					index = 0;
					ctrls.Add(tb);
					otbName = tbName;
					tb.ImageList = imgs;
					tb.ButtonClick+=new ToolBarButtonClickEventHandler(ToolBarButtonOnClick);
				}
				tbb.Name = row[MENU_NAME].ToString();
				tbb.FormName = AssemblyName + row[FORM_NAME].ToString();
				description = clsResources.GetTitle(frmMainName + tbb.Name);
				tbb.ToolTipText = description;
				if(iconName.Length > 0)
				{
					filename = folder + iconName;
					if(File.Exists(filename))
					{
						try
						{
							Icon icon = new Icon(filename);
							Icon icon2 = new Icon(icon, size);
							//Image icon2 = Image.FromFile(filename);
							imgs.Images.Add(icon2);
							tbb.ImageIndex = index;
							index++;
						}
						catch(Exception ex)
						{
							log.Error(ex.Message, ex);
						}
					}
				}

				tb.Buttons.Add(tbb);
			}
			foreach(Control ctrl in ctrls)
				this.Controls.Add(ctrl);
			#region Old
			//			pnlToolBar.Controls.Clear();
			//			SkinButton btn = null;
			//			Size size = new Size(23, 22);
			//			ContentAlignment align = ContentAlignment.MiddleCenter;
			//
			//			if(clsStyleManager.Aqua)
			//			{
			//				size = new Size(34, 22);
			//				align = ContentAlignment.MiddleCenter;
			//				//pnlToolBar.DockPadding.Bottom = 0;
			//			}
			//
			//			string description = null;
			//			string iconName = null;
			//			string folder = clsSystemConfig.IconFolder;
			//			foreach(DataRow row in rows)
			//			{
			//				btn = new SkinButton();
			//
			//				btn.SuspendLayout();
			//				btn.Stardard = clsStyleManager.Aqua;
			//
			//				btn.Name = row[MENU_NAME].ToString();
			//				btn.Tag = AssemblyName + row[FORM_NAME].ToString();
			//				btn.Size = size;//new Size(33, 24);
			//				btn.Dock = DockStyle.Left;
			//				iconName = row[ICON_NAME].ToString();
			//				description = clsResources.GetTitle(frmMainName + btn.Name);//row[DESCRIPTION].ToString();
			//				if(description.Length > 0)
			//					tip.SetToolTip(btn, description);
			//				if(iconName.Length > 0)
			//				{
			//					string filename = folder + iconName;
			//					if(File.Exists(filename))
			//					{
			//						try
			//						{
			//							//Need to resize to (24, 24)
			//							Image icon = Image.FromFile(filename);
			//							btn.ImageAlign = align;//ContentAlignment.TopLeft;
			//							btn.Image = icon;
			//						}
			//						catch(Exception ex)
			//						{
			//							log.Error(ex.Message, ex);
			//						}
			//					}
			//				}
			//				btn.ResumeLayout(false);
			//				pnlToolBar.Controls.Add(btn);
			//				btn.Click +=new EventHandler(ButtonOnClick);
			//			}
			//			if(pnlToolBar.Controls.Count > 0)
			//				pnlToolBar.Visible = true;
			//			else
			//				pnlToolBar.Visible = false;
			#endregion
		}

		/// <summary>
		/// Close all MDI Children Windows
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public void CloseAllWindow()
		{
			while(this.MdiChildren.Length > 0)
			{
				foreach(Form frm in this.MdiChildren)
				{
					if(frm.Visible)
						frm.Close();
				}
			}
		}

		/// <summary>
		/// Open login form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
        /// Mofified date: 2010/01/05 - PhongNTT
		/// </remarks>
		private void mnuLogin_Click(object sender, System.EventArgs e)
		{
			int LoginResult = clsConstants.PASSWORD_WRONG;
			frmLogin frm = new frmLogin();
			if(frm.ShowDialog() == DialogResult.OK)
			{
				LoginResult = frm.LoginResult;
			}

			if(LoginResult == clsConstants.LOGIN_SUCCESS)
			{
				//SingleInstance.SingleApplication.Run(new frmMain());
				clsAutUserBO bo = new clsAutUserBO();
                DataTable dt = bo.GetAuthority();
				this.Menu = BuildMenu(dt);
				CreateToolBar(dt);
				bo.ClearAuthority();
				this.Text = clsResources.GetTitle("frmMain.Title") + " - " + clsResources.GetMessage("messages.username.title", clsSystemConfig.UserName);
				clsFormManager.MainForm = this;

                //LoadToDoPane();
                //LoadToDoList();
                //LoadToDoList();

                //Start importing thread
                StartSystemTrayImportingThread();

                //show notify icon on system tray
                notifyIcon.Visible = true;
                importProgressTimer.Enabled = true;

                //End - Start importing thread
                //Sonlv - Visible
                this.Menu.MenuItems["mnuFile"].MenuItems["mnuLogin"].Visible = false;
			}

		}

        #region ToDo List navigation bar - KienTNT
        //Load ToDo list bar
        private void LoadToDoList()
        {
            try
            {
                clock.Tick += new EventHandler(clock_Tick);
                clock.Interval = 1000;
                //NavigateBarButton
                NavigateBarButton btnTodo = new NavigateBarButton();
                btnTodo.RelatedControl = new cpoTodoList();
                btnTodo.Caption = "To-Do List";
                btnTodo.CaptionDescription = "To-Do List";
                btnTodo.Image = Properties.Resources.home;
                btnTodo.Enabled = true;
                btnTodo.Key = "ToDo";
                btnTodo.IsShowCaptionImage = false;

                //NavigateBar
                ToDoBar = new NavigateBar();
                ToDoBar.Dock = DockStyle.Left;
                ToDoBar.Location = new Point(20, 20);
                ToDoBar.NavigateBarButtons.Add(btnTodo);
                ToDoBar.NavigateBarColorTable = NavigateBarColorTable.SystemColor;
                //ToDoBar.ClientSizeChanged+=new EventHandler(ToDoBar_ClientSizeChanged);
                ToDoBar.IsShowCollapsibleScreen = true;
                ToDoBar.CollapsedScreenWidth = 150;
                ToDoBar.CollapsibleWidth = 32;
                ToDoBar.ChangeCollapseMode(true);
                ToDoBar.OnNavigateBarCollapseModeChanged += new NavigateBar.OnNavigateBarCollapseModeChangedEventHandler(ToDoBar_OnNavigateBarCollapseModeChanged);
                this.Controls.Add(ToDoBar);
            }catch(Exception ex){
                log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Timer will tick when bar is collapsed (not show)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clock_Tick(object sender, EventArgs e)
        {
            try
            {
                if (count == iTimer * 60)// compute iTimer like minutes
                {
                    ((cpoTodoList)ToDoBar.NavigateBarButtons[0].RelatedControl).Reload();
                    //LoadToDo(true);// Reload form without load Timer
                    count = 0;
                }
                else
                {
                    count++;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                count = 0;
                return;
            }
        }

        /// <summary>
        /// Update todo list each time user click to collapse out toto list
        /// </summary>
        /// <param name="isCollap"></param>
        private void ToDoBar_OnNavigateBarCollapseModeChanged(bool isCollap)
        {
            if (!isCollap)
            {
                ((cpoTodoList)ToDoBar.NavigateBarButtons[0].RelatedControl).Reload();
            }
            else
            {
                clock.Start();
                count = 0;
            }

        }

        /// <summary>
        /// Load to-do list form and set position
        /// </summary>
        //private void LoadToDoList()
        //{

        //    frmToDoList.ShowTodoList(this);
        //}


        //private void LoadToDoPane()
        //{
        //    NavigateBar todoBar = new NavigateBar();
        //    todoBar.Dock = DockStyle.Right;
        //    todoBar.NavigateBarButtons.Add(new NavigateBarButton("To-Do List", Properties.Resources.btnSearch, new frmToDoList()));
        //    todoBar.NavigateBarColorTable = NavigateBarColorTable.SystemColor;
        //    todoBar.IsShowCollapsibleScreen = false;
        //    todoBar.CollapsedScreenWidth = 140;
        //    todoBar.ChangeCollapseMode(true);

        //    Controls.Add(todoBar);
        //} 
        #endregion 


		/// <summary>
		/// Open login form on load events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void frmMain_Load(object sender, System.EventArgs e)
		{
			mnuLogin_Click(null, null);
		}

		/// <summary>
		/// Question before exit application. Do not exit application if user chooses "No".
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(MessageBox.Show(clsResources.GetMessage("messages.exit"), clsResources.GetMessage("messages.general"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Application.Exit();
			}
			else
			{
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Handle event when click on Tool bar button (Open new window).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private void ToolBarButtonOnClick(object sender, ToolBarButtonClickEventArgs e)
		{
			MDToolBarButton tbb = e.Button as MDToolBarButton;
			if(tbb == null)
				return;

			string formName = tbb.FormName;
			Form frm = null;
			try
			{
				if(formName != AssemblyName)
					frm = (Form)Activator.CreateInstance(Type.GetType(formName), null);
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				MessageBox.Show(clsResources.GetMessage("errors.form.create", formName));
			}
			if(frm != null)
			{
				clsFormManager.ShowMDIChild(frm);
			}
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            AbortSystemTrayImportingThread();

            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (notifyIcon.Visible)
                {
                    Hide();

                    showMainFormToolStripMenuItem.Enabled = true;
                }
            }
        }

        #region For System Tray Importing Thread

        private void StartSystemTrayImportingThread()
        {
            m_systemTrayImportingThread = new Thread(new ThreadStart(this.SystemTrayImport));

            m_systemTrayImportingThread.Start();
            while (!m_systemTrayImportingThread.IsAlive) ;

            ControlMenuItems(true);
        }

        private void StopSystemTrayImportingThread()
        {
            m_stopImportingProgress = true;

            ControlMenuItems(false);
        }

        private void ResumeSystemTrayImportingThread()
        {
            //Set STOP FLAG to OFF
            m_stopImportingProgress = false;

            if ((m_systemTrayImportingThread.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
            {
                m_systemTrayImportingThread.Resume();
                //while (!m_systemTrayImportingThread.IsAlive) ;

                ControlMenuItems(true);
            }
        }

        private void AbortSystemTrayImportingThread()
        {
            if (m_systemTrayImportingThread != null)
            {
                //if ((m_systemTrayImportingThread.ThreadState & ThreadState.Unstarted) != ThreadState.Unstarted
                //    && (m_systemTrayImportingThread.ThreadState & ThreadState.Stopped) != ThreadState.Stopped
                //    && (m_systemTrayImportingThread.ThreadState & ThreadState.AbortRequested) != ThreadState.AbortRequested
                //    && (m_systemTrayImportingThread.ThreadState & ThreadState.Aborted) != ThreadState.Aborted)
                //{
                //    m_abortImportingProgress = true;
                //    if ((m_systemTrayImportingThread.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
                //    {
                //        //m_systemTrayImportingThread.Resume();
                //    }
                //    else
                //    {
                //        m_systemTrayImportingThread.Abort();
                //        m_systemTrayImportingThread.Join();
                //    }
                //}

                m_forceAbortImportingProgress = true;

                if ((m_systemTrayImportingThread.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
                {
                    m_systemTrayImportingThread.Resume();
                }

                while ((m_systemTrayImportingThread.ThreadState & ThreadState.Stopped) != ThreadState.Stopped)
                {
                }

                //bool aborted = true;
            }
        }
        
        private void SystemTrayImport()
        {
            try
            {
                string importFolderPath = ConfigurationManager.AppSettings["StoreImportFolderPath"];
                string importWorkingFolderPath = Path.Combine(importFolderPath, "Working");
                string importCompleteFolderPath = Path.Combine(importFolderPath, "Completed");
                string importErrorFolderPath = Path.Combine(importFolderPath, "Error");

                Thread.Sleep(5000);

                while (true)
                {
                    if (m_forceAbortImportingProgress)
                    {
                        m_systemTrayImportingThread.Abort();
                        m_systemTrayImportingThread.Join();
                    }

                    //Ensure thread live as leas 2 seconds
                    Thread.Sleep(2000);

                    //Set status is importing
                    m_isOnImportingProgress = true;

                    //If STOP FLAG is on
                    //if (m_stopImportingProgress)
                    //{
                    //    m_systemTrayImportingThread.Suspend();
                    //}

                    if (!m_abortImportingProgress && !m_stopImportingProgress)
                    {
                        //import data here
                        //Thread.Sleep(5000);

                        string importedFileWorkingPath = string.Empty;
                        string importedFilePath = string.Empty;

                        try
                        {
                            string[] filePaths = GetImportedFiles(importFolderPath);

                            if (filePaths != null)
                            {
                                if (filePaths.Length > 0)
                                {
                                    importedFilePath = filePaths[0];

                                    importedFileWorkingPath = CreateDirAndMoveFile(importedFilePath, importWorkingFolderPath, false, true);

                                   // clsStoreBO storeBo = new clsStoreBO();

                                    string[] errorMsgs = null;

                                    //DongTC comment 
                                   // storeBo.ImportStoreDataFromFile(importedFileWorkingPath, false, out errorMsgs);

                                    if (errorMsgs.Length > 0)
                                    {
                                        string msg = clsResources.GetMessage("messages.importStore.finishWithRowsError") + Environment.NewLine +
                                            string.Join(Environment.NewLine, errorMsgs);

                                        //MessageBox.Show(msg, clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        log.Info(msg);
                                    }

                                    //Move file when finish importing
                                    CreateDirAndMoveFile(importedFileWorkingPath, importCompleteFolderPath, true, true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Debug("UKPI.frmMain.SystemTrayImport: Error with file '" + importedFilePath + "' --> " + ex.Message);

                            if (importedFileWorkingPath != string.Empty)
                            {
                                //Move file when ERROR importing
                                CreateDirAndMoveFile(importedFileWorkingPath, importErrorFolderPath, true, true);
                            }
                        }
                    }

                    //Set status is finish importing
                    m_isOnImportingProgress = false;
                    
                    if (m_forceAbortImportingProgress)
                    {
                        m_systemTrayImportingThread.Abort();
                        m_systemTrayImportingThread.Join();
                    }

                    //Sleep until called by Timer
                    m_systemTrayImportingThread.Suspend();
                }
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message);
            }
        }

        private string[] GetImportedFiles(string importFolderPath)
        {
            if (Directory.Exists(importFolderPath))
            {
                return Directory.GetFiles(importFolderPath, "*.xls");
            }
            else
            {
                log.Error("Import folder '" + importFolderPath + "' is not exist.");
                
                StopSystemTrayImportingThread();

                return null;
            }
        }

        private string CreateDirAndMoveFile(string filePath, string desFolderPath, bool addTimeStamp, bool overWrite)
        {
            if (!Directory.Exists(desFolderPath))
            {
                Directory.CreateDirectory(desFolderPath);
            }

            string fileName = Path.GetFileName(filePath);

            if (addTimeStamp)
            {
                string ext = Path.GetExtension(fileName);
                fileName = Path.GetFileNameWithoutExtension(fileName);

                fileName = fileName + DateTime.Now.ToString("_yyyyMMdd_hhmmssffff");
                fileName = fileName + ext;
            }

            string desFilePath = Path.Combine(desFolderPath, fileName);

            if (overWrite)
            {
                File.Delete(desFilePath);
            }
            File.Move(filePath, desFilePath);

            return desFilePath;
        }

        private void ControlMenuItems(bool isImportThreadStarted)
        {
            if (this.Menu.MenuItems["mnuStoreData"] == null || this.Menu.MenuItems["mnuStoreData"].MenuItems["mnuImportStore"] == null)
            {
                stopImportingProgressToolStripMenuItem.Enabled = false;
                startImportingProgressToolStripMenuItem.Enabled = false;
                return;
            }

            MenuItem importStoresItem = this.Menu.MenuItems["mnuStoreData"].MenuItems["mnuImportStore"];
            
            //Importing thread is running
            if (isImportThreadStarted)
            {
                //System tray context menu
                stopImportingProgressToolStripMenuItem.Enabled = true;
                startImportingProgressToolStripMenuItem.Enabled = false;

                //Main form's menu
                if (importStoresItem != null)
                {
                    importStoresItem.Enabled = false;
                    importStoresItem.Visible = false;
                }
            }
            else  //Importing thread was suspending
            {
                //System tray context menu
                stopImportingProgressToolStripMenuItem.Enabled = false;
                startImportingProgressToolStripMenuItem.Enabled = true;

                //Main form's menu
                if (importStoresItem != null)
                {
                    importStoresItem.Enabled = true;
                    importStoresItem.Visible = true;
                }
            }
        }

        #endregion For System Tray Importing Thread

        #region context menu event handler

        private void exiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void showMainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainFormFromNotifyIcon();
        }

        #endregion context menu event handler

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowMainFormFromNotifyIcon();
            }
        }

        private void ShowMainFormFromNotifyIcon()
        {
            Show();
            WindowState = FormWindowState.Maximized;

            showMainFormToolStripMenuItem.Enabled = false;
        }

        private void stopImportingProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_isOnImportingProgress)
            {
                MessageBox.Show(clsResources.GetMessage("frmMain.stopImportProcess"), clsResources.GetMessage("frmMain.Information"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            StopSystemTrayImportingThread();
        }

        private void startImportingProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResumeSystemTrayImportingThread();
        }

        private void importProgressTimer_Tick(object sender, EventArgs e)
        {
            if (!m_stopImportingProgress)
            {
                if (!m_isOnImportingProgress)
                {
                    ResumeSystemTrayImportingThread();
                }
            }
        }
    }
}