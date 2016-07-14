using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.IO;

using UKPI.Controls;
using PureComponents.NicePanel;
using DotNetSkin.SkinControls;

namespace UKPI.Utils
{
	/// <summary>
	/// Summary description for clsStyleManager.
	/// </summary>
	/// <remarks>
	/// Author: Nguyễn Minh Đức. G3.
	/// Do bi phu thuoc vao cong nghe. Nen con nhieu cho phai Hard code. Li tuong nhat la tat ca Style phai cau hinh tren file XML. Vi toi khong co nhieu thoi gian nen phai Hard code. Sau nay neu co thoi gian thi khong hard code nua.
	/// </remarks>
	public class clsStyleManager
	{
		public clsStyleManager()
		{
		}
		private static Brush brColor;
		protected static bool m_SystemStyle = true;

		public static bool SystemStyle
		{
			get{return m_SystemStyle;}
			set{m_SystemStyle = value;}
		}

		protected static bool m_ColorStyle = false;
		public static bool ColorStyle
		{
			get
			{
				return (!(m_ThemeColor == Style.None));
			}
		}

		protected static bool m_ColorFocusControl = false;

		public static bool ColorFocusControl
		{
			get{return m_ColorFocusControl;}
			set{m_ColorFocusControl = value;}
		}

		protected static bool m_Aqua = false;
		public static bool Aqua
		{
			get{return m_Aqua;}
			set{m_Aqua = value;}
		}

		protected static bool m_AquaImage = false;
		public static bool AquaImage
		{
			get{return m_AquaImage;}
			set{m_AquaImage = value;}
		}

		protected static Style m_ThemeColor = Style.None;
		public static Style ThemeColor
		{
			get{return m_ThemeColor;}
			set{m_ThemeColor = value;}
		}

		/// <summary>
		/// Init
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void Init()
		{
			try
			{
				string str = ConfigurationManager.AppSettings["Style.Color"];
				string filename = null;
				if( str == Style.Blue.ToString() )
				{
					m_ThemeColor = Style.Blue;
					filename = clsSystemConfig.ImageFolder + "mac_button_blue.png";
				}
				else if( str == Style.Cyan.ToString() )
				{
					m_ThemeColor = Style.Cyan;
					filename = clsSystemConfig.ImageFolder + "mac_button_cyan.png";
				}
				else if( str == Style.Maveric.ToString() )
				{
					m_ThemeColor = Style.Maveric;
					filename = clsSystemConfig.ImageFolder + "mac_button_maveric.png";
				}
				else if( str == Style.Silver.ToString() )
				{
					m_ThemeColor = Style.Silver;
					filename = clsSystemConfig.ImageFolder + "mac_button_silver.png";
				}
				else if( str == Style.Orange.ToString() )
				{
					m_ThemeColor = Style.Orange;
					filename = clsSystemConfig.ImageFolder + "mac_button_orange.png";
				}
				else //if( str == Style.Cyan.ToString() )
				{
					m_ThemeColor = Style.None;
				}

				if(filename != null && File.Exists(filename))
				{
					SkinImage.button = new ImageObject(filename);
				}

				str = ConfigurationManager.AppSettings["Button.Style.Aqua"];
				if( str == "true" )
					m_Aqua = true;
				else
					m_Aqua = false;

				str = ConfigurationManager.AppSettings["Button.Style.Aqua.Image"];
				if( str == "true" )
					m_AquaImage = true;
				else
					m_AquaImage = false;

				str = ConfigurationManager.AppSettings["ColorFocusControl"];
				if( str == "true" )
					m_ColorFocusControl = true;
				else
					m_ColorFocusControl = false;

				str = ConfigurationManager.AppSettings["Button.Style.System"];
				if( str == "true" )
					m_SystemStyle = true;
				else
					m_SystemStyle = false;
			}
			catch
			{
			}
		}

		/// <summary>
		/// Flat system all control
		/// </summary>
		/// <param name="control"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void FlatSystem(Control control)
		{
			if(control == null)
				return;

			Button btn = control as Button;
			CheckBox chk  = control as CheckBox;
			RadioButton rad = control as RadioButton;
			GroupBox grp =  control as GroupBox;
			if(btn != null)
			{
				btn.FlatStyle = FlatStyle.System;
				btn.Text = btn.Text.Trim();
			}
			else if(chk != null)
			{
				if(!ColorStyle)
					chk.FlatStyle = FlatStyle.System;
			}
			else if(rad != null)
			{
				if(!ColorStyle)
					rad.FlatStyle = FlatStyle.System;
			}
			else if(grp != null)
			{
				if(!ColorStyle)
					grp.FlatStyle = FlatStyle.System;
				foreach(Control sub in control.Controls)
				{
					FlatSystem(sub);
				}
			}
			else
			{
				foreach(Control sub in control.Controls)
				{
					FlatSystem(sub);
				}
			}
		}

		#region Style Manager
		/// <summary>
		/// Change style of this form
		/// </summary>
		/// <param name="frm"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void ChangeStyle(Form frm)
		{
			if(!ColorStyle)
				return;

			NicePanel pnlMain = null;

			ArrayList arrs = new ArrayList();
			arrs.AddRange(frm.Controls);
			frm.Controls.Clear();
			frm.SuspendLayout();
			
			pnlMain = CreateMainPanel();
			pnlMain.Dock = DockStyle.Fill;
			pnlMain.SuspendLayout();
			frm.Controls.Add(pnlMain);
			frm.BackColor = pnlMain.Style.ContainerStyle.BackColor;
			frm.ResumeLayout();

			frm.SuspendLayout();
			foreach(Control control in arrs)
			{
				GroupBox grp = control as GroupBox;
				NicePanel sub;
				if(grp != null)
				{
					sub = CreateNicePanel();
					sub.Location = control.Location;
					sub.Size = control.Size;

					sub.Anchor = control.Anchor;
					sub.Dock = control.Dock;
					sub.TabIndex = control.TabIndex;

					ChangeStyle(sub);
					pnlMain.Controls.Add(sub);

					SwapStyle(control, sub);
				}
				else
				{
					ChangeStyle(control);
					pnlMain.Controls.Add(control);
				}
			}

			SetStyle(frm);

			pnlMain.ResumeLayout(false);
			frm.ResumeLayout(false);
		}

		/// <summary>
		/// Change style of this control
		/// </summary>
		/// <param name="ctrl"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		protected static void ChangeStyle(Control ctrl)
		{
			if(ContainGroupBox(ctrl))
			{
				ArrayList arrs = new ArrayList();
				arrs.AddRange(ctrl.Controls);
				ctrl.Controls.Clear();
				ctrl.SuspendLayout();

				foreach(Control control in arrs)
				{
					GroupBox grp = control as GroupBox;
					NicePanel sub;
					if(grp != null)
					{
						sub = CreateNicePanel();
						sub.Location = control.Location;
						sub.Size = control.Size;

						sub.Anchor = control.Anchor;
						sub.Dock = control.Dock;
						sub.TabIndex = control.TabIndex;

						ChangeStyle(sub);
						ctrl.Controls.Add(sub);

						SwapStyle(control, sub);
					}
					else
					{
						ChangeStyle(control);
						ctrl.Controls.Add(control);
					}
				}
				ctrl.ResumeLayout(false);
			}
		}

		/// <summary>
		/// Check whether this control contains GroupBox
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		protected static bool ContainGroupBox(Control control)
		{
			GroupBox grp = null;
			foreach(Control sub in control.Controls)
			{
				grp = sub as GroupBox;
				if(grp != null)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Set style of this control
		/// </summary>
		/// <param name="control"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void SetStyle(Control control)
		{
			DataGrid grd = control as DataGrid;
			Label lbl = control as Label;
			Splitter split = control as Splitter;
			CheckBox chk = control as CheckBox;
			RadioButton rad = control as RadioButton;
			TextBox txt = control as TextBox;
			SkinButton skinbtn = control as SkinButton;
            DataGridView grdview = control as DataGridView;
            TabPage tabPage = control as TabPage;
			
			if(lbl != null)
			{
				lbl.BackColor = Color.Transparent;
				//lbl.ForeColor = Color.Blue;//Color.FromArgb(16, 1, 194);
			}
			else if(chk != null)
			{
				if(chk.FlatStyle == FlatStyle.System)
					chk.FlatStyle = FlatStyle.Standard;
			}
			else if(rad != null)
			{
				if(rad.FlatStyle == FlatStyle.System)
					rad.FlatStyle = FlatStyle.Standard;
			}
			else if(skinbtn != null && m_Aqua)
			{
				Font font = new Font("Tahoma", (float)8.25, FontStyle.Bold);
				skinbtn.SuspendLayout();

				skinbtn.Stardard = true;
				//skinbtn.Height = 24;

				skinbtn.Font = font;
				

				if(m_AquaImage)
				{
					skinbtn.Top -= 1;
					if(skinbtn.Height < 26)
					{
						skinbtn.Height = 26;
					}

					//					skinbtn.Left -=4;
					//					if(skinbtn.Width < 95)
					//					{
					//						skinbtn.Width = 95;
					//					}

					//skinbtn.Image = null;
					//skinbtn.ImageList = null;
					skinbtn.ImageAlign = ContentAlignment.MiddleLeft;
					
					skinbtn.Text = " " + skinbtn.Text;
					skinbtn.TextAlign = ContentAlignment.MiddleCenter;//ContentAlignment.TopCenter;
				}
				else
				{
					//skinbtn.Top -= 1;
					if(skinbtn.Height < 25)
					{
						skinbtn.Height = 25;
					}
					skinbtn.Image = null;
					skinbtn.ImageList = null;
					skinbtn.TextAlign = ContentAlignment.MiddleCenter;//.TopCenter;
					skinbtn.Text = skinbtn.Text.Trim();
				}
				if(ThemeColor == Style.Blue)
					skinbtn.ForeColor = Color.DodgerBlue;//CornflowerBlue;
				else if(ThemeColor == Style.Maveric)
					skinbtn.ForeColor = Color.CornflowerBlue;//Color.FromArgb(122, 162, 217);//(104, 144, 199);
				else if(ThemeColor == Style.Silver)
					skinbtn.ForeColor = Color.FromArgb(138, 138, 138);
				else if(ThemeColor == Style.Cyan)
					skinbtn.ForeColor = Color.Green;
				else if(ThemeColor == Style.Orange)
					skinbtn.ForeColor = Color.Brown;


				skinbtn.ResumeLayout(false);
			}
			else if(grd != null)
				SetStyle(grd);
			else if(txt != null)
			{
				if(txt.ReadOnly)
					txt.ForeColor = Color.FromArgb(166,162,147);//(172,168,153);
				txt.BackColor = Color.White;

			}
			else if(split != null)
			{
				split.Width = 2;
				Form frm = control.FindForm();
				if(frm != null)
					split.BackColor = frm.BackColor;
			}
            else if (grdview != null)
                SetStyle(grdview);
            else if (tabPage != null)
            {
                tabPage.BackColor = Color.FromArgb(217, 232, 252);
                tabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                foreach (Control sub in tabPage.Controls)
                {
                    SetStyle(sub);
                }
            }
            else
            {
                foreach (Control sub in control.Controls)
                {
                    SetStyle(sub);
                }
            }
		}

		/// <summary>
		/// Set style of DataGrid
		/// </summary>
		/// <param name="grd"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void SetStyle(DataGrid grd)
		{
			grd.SuspendLayout();
//			if(!grd.ReadOnly)
//				if(grd.TableStyles[0].ReadOnly == false)
//				{
//					brColor = frmMain.brColor;
//					DataGridTableStyle grdNew = new DataGridTableStyle();
//					//			if(grd.ReadOnly == true)
//					//				return;
//					bool checkReadOnlyAll = true;
//					foreach(DataGridTableStyle grdStyle in grd.TableStyles)
//					{
//					
//								
//						foreach(DataGridColumnStyle col in grdStyle.GridColumnStyles)
//						{
//							bool boolAdded = false;
//							DataGridTextBoxColumn txtCol = col as DataGridTextBoxColumn;
//							DataGridBoolColumn boolCol = col as DataGridBoolColumn;
//							if(txtCol != null && txtCol.ReadOnly == false )//&& (grd.TableStyles[0].GridColumnStyles[i] is DataGridTextBoxColumn) )
//							{
//								FormattableTextBoxColumn colAdd = new FormattableTextBoxColumn();
//								colAdd.MappingName = txtCol.MappingName;
//								colAdd.ReadOnly = false;
//								colAdd.TextBox.MaxLength = txtCol.TextBox.MaxLength;
//								colAdd.NullText = txtCol.NullText;
//								clsCommon.RegNumberOnly(colAdd.TextBox);
//								colAdd.HeaderText = txtCol.HeaderText;
//								colAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
//								//arlCol.Add(colAdd);
//								grdNew.GridColumnStyles.Add(colAdd);
//								boolAdded = true;
//								checkReadOnlyAll = false;
//							}
//							if(boolCol!= null && boolCol.ReadOnly == false)
//							{
//								FormattableBooleanColumn colBoolAdd = new FormattableBooleanColumn();
//								colBoolAdd.MappingName = boolCol.MappingName;
//								colBoolAdd.ReadOnly = false;
//								colBoolAdd.TrueValue = boolCol.TrueValue;;
//								colBoolAdd.FalseValue =boolCol.FalseValue;
//								colBoolAdd.AllowNull = boolCol.AllowNull;
//								colBoolAdd.HeaderText = boolCol.HeaderText;
//								colBoolAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
//								grdNew.GridColumnStyles.Add(colBoolAdd);
//								boolAdded = true;
//								checkReadOnlyAll = false;
//							}
//							if(!boolAdded)grdNew.GridColumnStyles.Add(col);
//						}
//								
//					}
//					if(!checkReadOnlyAll)
//					{
//						grdNew.HeaderBackColor = grd.TableStyles[0].HeaderBackColor;
//						grd.TableStyles.RemoveAt(0);
//						grd.TableStyles.Add(grdNew);
//					}
//				}
			if(ThemeColor == Style.Blue)
			{
				grd.BackgroundColor = Color.FromArgb(217, 232, 252);

				grd.SelectionBackColor = SystemColors.ActiveCaption;
				grd.SelectionForeColor = SystemColors.ActiveCaptionText;
				grd.AlternatingBackColor = Color.FromArgb(227, 242, 252);
				grd.BackColor = Color.White;
				grd.ForeColor = Color.DarkBlue;
				grd.GridLineColor = Color.RoyalBlue;
				grd.HeaderBackColor = Color.FromArgb(217, 232, 252);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
				grd.HeaderForeColor = Color.MidnightBlue;

				grd.CaptionBackColor = Color.RoyalBlue;
				grd.CaptionForeColor = Color.Bisque;
				grd.ParentRowsBackColor = Color.Lavender;
				grd.ParentRowsForeColor = Color.MidnightBlue;

				foreach(DataGridTableStyle grdStyle in grd.TableStyles)
				{
					grdStyle.SelectionBackColor = SystemColors.ActiveCaption;
					grdStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
					grdStyle.AlternatingBackColor = Color.FromArgb(227, 242, 252);
					grdStyle.BackColor = Color.White;
					grdStyle.ForeColor = Color.DarkBlue;
					grdStyle.GridLineColor = Color.RoyalBlue;
					grdStyle.HeaderBackColor = Color.FromArgb(217, 232, 252);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
					grdStyle.HeaderForeColor = Color.MidnightBlue;
				}
            }
            #region not blue
            else if(ThemeColor == Style.Silver)
			{
				grd.BackgroundColor = Color.White;//Color.FromArgb(240, 241, 245);

				grd.BackColor = Color.FromArgb(240, 241, 245);
				grd.SelectionBackColor = SystemColors.ActiveCaption;
				grd.SelectionForeColor = SystemColors.ActiveCaptionText;
				grd.AlternatingBackColor = Color.FromArgb(248, 246, 249);//(249, 246, 251);//(246, 243, 249);
				grd.BackColor = Color.White;
				grd.ForeColor = Color.FromArgb(86, 113, 152);//(22, 119, 190);//(27, 142, 228);//(16, 1, 194);
				grd.GridLineColor = Color.FromArgb(194, 194, 194);//(188, 194, 220);//(122, 162, 217);//128
				grd.HeaderBackColor = Color.FromArgb(240, 241, 245);//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
				grd.HeaderForeColor = Color.FromArgb(133, 138, 143);//(0, 60, 165);//Color.MidnightBlue;//Color.FromArgb(0, 0, 81);

				grd.CaptionBackColor = Color.FromArgb(203, 206, 217);//(196, 199, 213);(203, 206, 217)
				grd.CaptionForeColor = Color.WhiteSmoke;//.FromArgb(233, 255, 244);

				foreach(DataGridTableStyle grdStyle in grd.TableStyles)
				{
					grdStyle.BackColor = Color.FromArgb(240, 241, 245);
					grdStyle.SelectionBackColor = SystemColors.ActiveCaption;
					grdStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
					grdStyle.AlternatingBackColor = Color.FromArgb(248, 247, 249);//(249, 246, 251);//(246, 243, 249);
					grdStyle.BackColor = Color.White;
					grdStyle.ForeColor = Color.FromArgb(108, 108, 108);//(86, 113, 152);//(22, 119, 190);//(27, 142, 228);//(16, 1, 194);
					grdStyle.GridLineColor = Color.FromArgb(194, 194, 194);//(188, 194, 220);//(122, 162, 217);//128
					grdStyle.HeaderBackColor = Color.FromArgb(240, 241, 243);//(240, 241, 245);//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
					grdStyle.HeaderForeColor = Color.FromArgb(133, 138, 143);//(0, 60, 165);//Color.MidnightBlue;//Color.FromArgb(0, 0, 81);
				}
			}
			else if(ThemeColor == Style.Maveric)
			{
				grd.BackgroundColor = Color.FromArgb(231, 239, 245);//Color.White;//Color.FromArgb(240, 241, 245);

				grd.BackColor = Color.FromArgb(231, 239, 245);
				grd.SelectionBackColor = SystemColors.ActiveCaption;
				grd.SelectionForeColor = SystemColors.ActiveCaptionText;
				grd.AlternatingBackColor = Color.FromArgb(239, 243, 248);
				grd.BackColor = Color.White;
				grd.ForeColor = Color.MidnightBlue;
				grd.GridLineColor = Color.FromArgb(87, 157, 215);//128
				grd.HeaderBackColor = Color.FromArgb(231, 239, 245);//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
				grd.HeaderForeColor = Color.MidnightBlue;

				grd.CaptionBackColor = Color.FromArgb(231, 239, 245);
				grd.CaptionForeColor = Color.FromArgb(87, 157, 215);

				foreach(DataGridTableStyle grdStyle in grd.TableStyles)
				{
					grdStyle.BackColor = Color.FromArgb(231, 239, 245);
					grdStyle.SelectionBackColor = SystemColors.ActiveCaption;
					grdStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
					grdStyle.AlternatingBackColor = Color.FromArgb(239, 243, 248);
					grdStyle.BackColor = Color.White;
					grdStyle.ForeColor = Color.MidnightBlue;
					grdStyle.GridLineColor = Color.FromArgb(87, 157, 215);//128
					grdStyle.HeaderBackColor = Color.FromArgb(231, 239, 245);//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
					grdStyle.HeaderForeColor = Color.MidnightBlue;
				}
			}
			else if(ThemeColor == Style.Cyan)
			{
				grd.BackgroundColor = Color.FromArgb(233, 255, 244);

				grd.BackColor = Color.GhostWhite;;
				grd.SelectionBackColor = SystemColors.ActiveCaption;
				grd.SelectionForeColor = SystemColors.ActiveCaptionText;
				grd.AlternatingBackColor = Color.FromArgb(233, 255, 244);
				grd.BackColor = Color.White;
				grd.ForeColor = Color.DarkGreen;
				grd.GridLineColor = Color.FromArgb(0, 150, 0);//128
				grd.HeaderBackColor = Color.FromArgb(233, 255, 244);;//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
				grd.HeaderForeColor = Color.DarkGreen;

				grd.CaptionBackColor = Color.FromArgb(137, 197, 179);
				grd.CaptionForeColor = Color.FromArgb(233, 255, 244);

				foreach(DataGridTableStyle grdStyle in grd.TableStyles)
				{
					grdStyle.BackColor = Color.GhostWhite;;
					grdStyle.SelectionBackColor = SystemColors.ActiveCaption;
					grdStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
					grdStyle.AlternatingBackColor = Color.FromArgb(233, 255, 244);
					grdStyle.BackColor = Color.White;
					grdStyle.ForeColor = Color.DarkGreen;
					grdStyle.GridLineColor = Color.FromArgb(0, 150, 0);//128
					grdStyle.HeaderBackColor = Color.FromArgb(233, 255, 244);;//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
					grdStyle.HeaderForeColor = Color.DarkGreen;
				}
			}
			//else if(ThemeColor == Style.Orange)
			else
			{
				grd.BackgroundColor = Color.FromArgb(255, 250, 245);
				grd.AlternatingBackColor = Color.FromArgb(255, 250, 245);

				grd.BackColor = Color.GhostWhite;;
				grd.SelectionBackColor = SystemColors.ActiveCaption;
				grd.SelectionForeColor = SystemColors.ActiveCaptionText;
				grd.AlternatingBackColor = Color.FromArgb(255, 250, 245);
				grd.BackColor = Color.White;
				grd.ForeColor = Color.DarkRed;
				grd.GridLineColor = Color.FromArgb(255, 207, 159);
				grd.HeaderBackColor = Color.FromArgb(255, 250, 245);;//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
				grd.HeaderForeColor = Color.Brown;

				grd.CaptionBackColor = Color.FromArgb(139, 69, 19);
				grd.CaptionForeColor = Color.FromArgb(253, 245, 230);

				foreach(DataGridTableStyle grdStyle in grd.TableStyles)
				{
					grdStyle.BackColor = Color.GhostWhite;;
					grdStyle.SelectionBackColor = SystemColors.ActiveCaption;
					grdStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
					grdStyle.AlternatingBackColor = Color.FromArgb(255, 250, 245);
					grdStyle.BackColor = Color.White;
					grdStyle.ForeColor = Color.DarkRed;
					grdStyle.GridLineColor = Color.FromArgb(255, 207, 159);
					grdStyle.HeaderBackColor = Color.FromArgb(255, 250, 245);;//Color.FromArgb(255, 217, 183);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
					grdStyle.HeaderForeColor = Color.Brown;
				}
			}
            //giong blue
            #endregion

            foreach (DataGridTableStyle grdStyle in grd.TableStyles)
			{
				foreach(DataGridColumnStyle col in grdStyle.GridColumnStyles)
				{
					DataGridTextBoxColumn txtCol = col as DataGridTextBoxColumn;
					if(txtCol != null)
					{
						TextBox txt = txtCol.TextBox;
						if(txt.ReadOnly)
							txt.ForeColor = Color.FromArgb(166,162,147);//(172,168,153);
						txt.BackColor = Color.White;
					}
					
				}
			}
			grd.ResumeLayout();
		}
        /// <summary>
        /// Duylnk added: DataGridView
        /// </summary>
        /// <param name="grd"></param>
        public static void SetStyle(DataGridView grd)
        {
            grd.SuspendLayout();            
            if (ThemeColor == Style.Blue)
            {
                grd.BackgroundColor = Color.FromArgb(227, 242, 252);              
                grd.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(217, 232, 252);//Color.FromArgb(229, 255, 255);//Color.Blue;//Color.MidnightBlue;
                grd.ColumnHeadersDefaultCellStyle.ForeColor = Color.MidnightBlue;
                grd.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(217, 232, 252);

                grd.DefaultCellStyle.BackColor = Color.FromArgb(227, 242, 252);
                grd.DefaultCellStyle.SelectionBackColor = SystemColors.ActiveCaption;
                grd.DefaultCellStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
                
                foreach (DataGridViewColumn grdCol in grd.Columns)
                {
                    if (!grdCol.ReadOnly)
                    {
                        grdCol.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                        grdCol.DefaultCellStyle.SelectionBackColor = SystemColors.ActiveCaption;
                        grdCol.DefaultCellStyle.SelectionForeColor = SystemColors.ActiveCaptionText;
                    }                    
                }
            }          
            
            grd.ResumeLayout();
        }

		public static void SetStyle(DataGrid grd,bool checkReadOnly)
		{
			grd.SuspendLayout();
			// Cuongvd 19/Jan/2007 G3
			brColor = clsCommon.brColor;
			if(checkReadOnly)
			{
				DataGridTableStyle grdNew = new DataGridTableStyle(); 
				grdNew.AllowSorting = grd.AllowSorting;
				//			if(grd.ReadOnly == true)
				//				return;
				bool checkReadOnlyAll = true;
//				foreach(DataGridTableStyle grdStyle in grd.TableStyles)
//				{
					foreach(DataGridColumnStyle col in grd.TableStyles[0].GridColumnStyles)
					{
						bool boolAdded = false;
						DataGridTextBoxColumn txtCol = col as DataGridTextBoxColumn;
						DataGridBoolColumn boolCol = col as DataGridBoolColumn;
						if(txtCol != null && txtCol.ReadOnly == false )//&& (grd.TableStyles[0].GridColumnStyles[i] is DataGridTextBoxColumn) )
						{
							FormattableTextBoxColumn colAdd = new FormattableTextBoxColumn();
							colAdd.MappingName = txtCol.MappingName;
							colAdd.ReadOnly = false;
							colAdd.TextBox.MaxLength = txtCol.TextBox.MaxLength;
							colAdd.Alignment = txtCol.Alignment;
							colAdd.NullText = "";
							colAdd.Width = txtCol.Width;
							clsCommon.RegNumberOnly(colAdd.TextBox);
							colAdd.HeaderText = txtCol.HeaderText;
							colAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
							grdNew.GridColumnStyles.Add(colAdd);
							boolAdded = true;
							checkReadOnlyAll = false;
						}
						if(boolCol!= null && boolCol.ReadOnly == false)
						{
							FormattableBooleanColumn colBoolAdd = new FormattableBooleanColumn();
							colBoolAdd.MappingName = boolCol.MappingName;
							colBoolAdd.ReadOnly = false;
							colBoolAdd.TrueValue = boolCol.TrueValue;;
							colBoolAdd.FalseValue =boolCol.FalseValue;
							colBoolAdd.AllowNull = boolCol.AllowNull;
							colBoolAdd.Width = boolCol.Width;
							colBoolAdd.HeaderText = boolCol.HeaderText;
							colBoolAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
							grdNew.GridColumnStyles.Add(colBoolAdd);
							boolAdded = true;
							checkReadOnlyAll = false;
						}
						if(!boolAdded)grdNew.GridColumnStyles.Add(col);
					}
								
//				}
				if(!checkReadOnlyAll)
				{
					
					grdNew.HeaderBackColor = grd.TableStyles[0].HeaderBackColor;
					grdNew.AlternatingBackColor = grd.TableStyles[0].AlternatingBackColor;
					grd.TableStyles.RemoveAt(0);
					
					grd.TableStyles.Add(grdNew);

				}
				//				 Cuongvd
				//clsCommon.RegAutoSizeCol(grd);
			}
			grd.ResumeLayout(false);
		}
		
//		public FormattableTextBoxColumn GetHilightColumn( DataGridTextBoxColumn colTextbox,bool regNumber)
//		{
//			FormattableTextBoxColumn colAdd = new FormattableTextBoxColumn();
//			colAdd.MappingName = colTextbox.MappingName;
//			colAdd.ReadOnly = false;
//			colAdd.TextBox.MaxLength = colTextbox.TextBox.MaxLength;
//			colAdd.Alignment = colTextbox.Alignment;
//			colAdd.NullText = colTextbox.NullText;
//			if(regNumber)
//				clsCommon.RegNumberOnly(colAdd.TextBox);
//			colAdd.HeaderText = colTextbox.HeaderText;
//			colAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
//			return colAdd;
//		}
//		public FormattableBooleanColumn GetHilightColumn( DataGridBoolColumn colBool)
//		{
//			FormattableBooleanColumn colAdd = new FormattableBooleanColumn();
//			colAdd.MappingName = colBool.MappingName;
//			colAdd.ReadOnly = false;
//			colAdd.AllowNull = colBool.AllowNull;
//			colAdd.TrueValue = colBool.TrueValue;
//			colAdd.FalseValue = colBool.FalseValue;
//			colAdd.NullText = colBool.NullText;
//			colAdd.HeaderText = colBool.HeaderText;
//			colAdd.SetCellFormat += new FormatCellEventHandler(SetHeaderCellFormat);
//			return colAdd;
//		}
		/// <summary>
		/// for evnet change backcolor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Created by	: cuongvd G3 fsoftHCM
		/// create date	: 18-jan-2007
		/// </remarks>
		private static void SetHeaderCellFormat(object sender, DataGridFormatCellEventArgs e)
		{
			try
			{
				e.BackBrush = brColor;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="control"></param>
		/// <param name="panel"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void SwapStyle(Control control, NicePanel panel)
		{
			control.SuspendLayout();
			panel.SuspendLayout();

			panel.HeaderText = control.Text;
			panel.Name = control.Name;

			ArrayList arrs = new ArrayList();
			arrs.AddRange(control.Controls);
			control.Controls.Clear();

			foreach(Control ctrl in arrs)
			{
				panel.Controls.Add(ctrl);
			}
			panel.ResumeLayout(false);
			control.ResumeLayout(false);
		}

		/// <summary>
		/// Create main panel
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static PureComponents.NicePanel.NicePanel CreateMainPanel()
		{
			PureComponents.NicePanel.NicePanel panel = CreateNicePanel();
			panel.SuspendLayout();
			if(m_ColorFocusControl)
				panel.ShowChildFocus = true;
			panel.Style.ContainerStyle.Shape = PureComponents.NicePanel.Shape.Squared;
			panel.Style.ContainerStyle.BorderStyle = PureComponents.NicePanel.BorderStyle.None;
			panel.ResumeLayout(false);
			return panel;
		}

		/// <summary>
		/// Create nice panel.
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static PureComponents.NicePanel.NicePanel CreateNicePanel()
		{
			PureComponents.NicePanel.NicePanel panel;
			panel = new PureComponents.NicePanel.NicePanel();
			panel.SuspendLayout();
				#region Blue
			if(ThemeColor == Style.Blue)
			{
				PureComponents.NicePanel.ContainerImage containerImage1 = new PureComponents.NicePanel.ContainerImage();
				PureComponents.NicePanel.HeaderImage headerImage1 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.HeaderImage headerImage2 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.PanelStyle panelStyle1 = new PureComponents.NicePanel.PanelStyle();
				PureComponents.NicePanel.ContainerStyle containerStyle1 = new PureComponents.NicePanel.ContainerStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle1 = new PureComponents.NicePanel.PanelHeaderStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle2 = new PureComponents.NicePanel.PanelHeaderStyle();
				// 
				// panel
				// 
				panel.BackColor = System.Drawing.Color.Transparent;
				panel.CollapseButton = false;
				containerImage1.Alignment = System.Drawing.ContentAlignment.TopRight;
				containerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.None;
				containerImage1.Image = null;
				containerImage1.Size = PureComponents.NicePanel.ContainerImageSize.Small;
				containerImage1.Transparency = 50;
				panel.ContainerImage = containerImage1;
				panel.ContextMenuButton = false;
				headerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.Applications;
				headerImage1.Image = null;
				panel.FooterImage = headerImage1;
				panel.FooterText = "";
				panel.FooterVisible = false;
				panel.ForeColor = System.Drawing.Color.Black;
				headerImage2.ClipArt = PureComponents.NicePanel.ImageClipArt.Block;
				headerImage2.Image = null;
				panel.HeaderImage = headerImage2;
				panel.HeaderText = "";
				panel.HeaderVisible = false;
				panel.IsExpanded = true;
				panel.Location = new System.Drawing.Point(16, 32);
				panel.Name = "panel";
				panel.OriginalFooterVisible = false;
				panel.OriginalHeight = 0;
				panel.ShowChildFocus = false;
				panel.Size = new System.Drawing.Size(320, 200);
				containerStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(142)), ((System.Byte)(179)), ((System.Byte)(231)));
				containerStyle1.BaseColor = System.Drawing.Color.Transparent;
				containerStyle1.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(1)), ((System.Byte)(45)), ((System.Byte)(150)));
				containerStyle1.BorderStyle = PureComponents.NicePanel.BorderStyle.Solid;
				containerStyle1.CaptionAlign = PureComponents.NicePanel.CaptionAlign.Left;
				containerStyle1.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(217)), ((System.Byte)(232)), ((System.Byte)(252)));
				containerStyle1.FillStyle = PureComponents.NicePanel.FillStyle.DiagonalForward;
				containerStyle1.FlashItemBackColor = System.Drawing.Color.Red;
				containerStyle1.FocusItemBackColor = System.Drawing.Color.FromArgb(((System.Byte)(229)), ((System.Byte)(255)), ((System.Byte)(255)));
				containerStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				containerStyle1.ForeColor = System.Drawing.Color.Black;
				containerStyle1.Shape = PureComponents.NicePanel.Shape.Rounded;
				panelStyle1.ContainerStyle = containerStyle1;
				panelHeaderStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle1.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle1.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle1.FillStyle = PureComponents.NicePanel.FillStyle.HorizontalFading;
				panelHeaderStyle1.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle1.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle1.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
				panelHeaderStyle1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(169)), ((System.Byte)(198)), ((System.Byte)(237)));
				panelHeaderStyle1.Size = PureComponents.NicePanel.PanelHeaderSize.Small;
				panelStyle1.FooterStyle = panelHeaderStyle1;
				panelHeaderStyle2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle2.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle2.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle2.FillStyle = PureComponents.NicePanel.FillStyle.VerticalFading;
				panelHeaderStyle2.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle2.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle2.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
				panelHeaderStyle2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(215)), ((System.Byte)(230)), ((System.Byte)(251)));
				panelHeaderStyle2.Size = PureComponents.NicePanel.PanelHeaderSize.Medium;
				panelStyle1.HeaderStyle = panelHeaderStyle2;
				panel.Style = panelStyle1;
			}
				#endregion Blue
				#region Maveric
			else if(ThemeColor == Style.Maveric)
			{
				PureComponents.NicePanel.ContainerImage containerImage1 = new PureComponents.NicePanel.ContainerImage();
				PureComponents.NicePanel.HeaderImage headerImage1 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.HeaderImage headerImage2 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.PanelStyle panelStyle1 = new PureComponents.NicePanel.PanelStyle();
				PureComponents.NicePanel.ContainerStyle containerStyle1 = new PureComponents.NicePanel.ContainerStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle1 = new PureComponents.NicePanel.PanelHeaderStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle2 = new PureComponents.NicePanel.PanelHeaderStyle();
				// 
				// panel
				// 
				panel.BackColor = System.Drawing.Color.Transparent;
				panel.CollapseButton = false;
				containerImage1.Alignment = System.Drawing.ContentAlignment.TopRight;
				containerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.None;
				containerImage1.Image = null;
				containerImage1.Size = PureComponents.NicePanel.ContainerImageSize.Small;
				containerImage1.Transparency = 50;
				panel.ContainerImage = containerImage1;
				panel.ContextMenuButton = false;
				headerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.Applications;
				headerImage1.Image = null;
				panel.FooterImage = headerImage1;
				panel.FooterText = "";
				panel.FooterVisible = false;
				panel.ForeColor = System.Drawing.Color.Black;
				headerImage2.ClipArt = PureComponents.NicePanel.ImageClipArt.Block;
				headerImage2.Image = null;
				panel.HeaderImage = headerImage2;
				panel.HeaderText = "";
				panel.HeaderVisible = false;
				panel.IsExpanded = true;
				panel.Location = new System.Drawing.Point(44, 134);
				panel.Name = "panel";
				panel.OriginalFooterVisible = false;
				panel.OriginalHeight = 0;
				panel.ShowChildFocus = false;
				panel.Size = new System.Drawing.Size(417, 50);
				containerStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(231)), ((System.Byte)(239)), ((System.Byte)(245)));
				containerStyle1.BaseColor = System.Drawing.Color.Transparent;
				containerStyle1.BorderColor = System.Drawing.Color.FromArgb(122, 162, 217);
				containerStyle1.BorderStyle = PureComponents.NicePanel.BorderStyle.Solid;
				containerStyle1.CaptionAlign = PureComponents.NicePanel.CaptionAlign.Left;
				containerStyle1.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(247)), ((System.Byte)(249)), ((System.Byte)(251)));
				containerStyle1.FillStyle = PureComponents.NicePanel.FillStyle.DiagonalForward;
				containerStyle1.FlashItemBackColor = System.Drawing.Color.FromArgb(((System.Byte)(248)), ((System.Byte)(248)), ((System.Byte)(250)));
				containerStyle1.FocusItemBackColor = System.Drawing.Color.FromArgb(((System.Byte)(229)), ((System.Byte)(255)), ((System.Byte)(255)));
				containerStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				containerStyle1.ForeColor = System.Drawing.Color.Black;
				containerStyle1.Shape = PureComponents.NicePanel.Shape.Rounded;
				panelStyle1.ContainerStyle = containerStyle1;
				panelHeaderStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle1.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle1.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle1.FillStyle = PureComponents.NicePanel.FillStyle.HorizontalFading;
				panelHeaderStyle1.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle1.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle1.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
				panelHeaderStyle1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(169)), ((System.Byte)(198)), ((System.Byte)(237)));
				panelHeaderStyle1.Size = PureComponents.NicePanel.PanelHeaderSize.Small;
				panelStyle1.FooterStyle = panelHeaderStyle1;
				panelHeaderStyle2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle2.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle2.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle2.FillStyle = PureComponents.NicePanel.FillStyle.VerticalFading;
				panelHeaderStyle2.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle2.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle2.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
				panelHeaderStyle2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(215)), ((System.Byte)(230)), ((System.Byte)(251)));
				panelHeaderStyle2.Size = PureComponents.NicePanel.PanelHeaderSize.Medium;
				panelStyle1.HeaderStyle = panelHeaderStyle2;
				panel.Style = panelStyle1;
			}
				#endregion Maveric
				#region Silver
			else if(ThemeColor == Style.Silver)
			{
				PureComponents.NicePanel.ContainerImage containerImage1 = new PureComponents.NicePanel.ContainerImage();
				PureComponents.NicePanel.HeaderImage headerImage1 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.HeaderImage headerImage2 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.PanelStyle panelStyle1 = new PureComponents.NicePanel.PanelStyle();
				PureComponents.NicePanel.ContainerStyle containerStyle1 = new PureComponents.NicePanel.ContainerStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle1 = new PureComponents.NicePanel.PanelHeaderStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle2 = new PureComponents.NicePanel.PanelHeaderStyle();
				// 
				// panel
				// 
				panel.BackColor = System.Drawing.Color.Transparent;
				panel.CollapseButton = false;
				containerImage1.Alignment = System.Drawing.ContentAlignment.TopRight;
				containerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.None;
				containerImage1.Image = null;
				containerImage1.Size = PureComponents.NicePanel.ContainerImageSize.Small;
				containerImage1.Transparency = 50;
				panel.ContainerImage = containerImage1;
				panel.ContextMenuButton = false;
				headerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.Applications;
				headerImage1.Image = null;
				panel.FooterImage = headerImage1;
				panel.FooterText = "";
				panel.FooterVisible = false;
				panel.ForeColor = System.Drawing.Color.Black;
				headerImage2.ClipArt = PureComponents.NicePanel.ImageClipArt.Block;
				headerImage2.Image = null;
				panel.HeaderImage = headerImage2;
				panel.HeaderText = "";
				panel.HeaderVisible = false;
				panel.IsExpanded = true;
				panel.Location = new System.Drawing.Point(16, 26);
				panel.Name = "panel";
				panel.OriginalFooterVisible = false;
				panel.OriginalHeight = 0;
				panel.ShowChildFocus = false;
				panel.Size = new System.Drawing.Size(417, 50);
				containerStyle1.BackColor = System.Drawing.Color.FromArgb(226, 226, 227);//(203, 206, 217);
				containerStyle1.BaseColor = System.Drawing.Color.Transparent;
				containerStyle1.BorderColor = System.Drawing.Color.FromArgb(194, 194, 194);//(188, 194, 220);
				containerStyle1.BorderStyle = PureComponents.NicePanel.BorderStyle.Solid;
				containerStyle1.CaptionAlign = PureComponents.NicePanel.CaptionAlign.Left;
				containerStyle1.FadeColor = System.Drawing.Color.White;
				containerStyle1.FillStyle = PureComponents.NicePanel.FillStyle.DiagonalForward;
				containerStyle1.FlashItemBackColor = System.Drawing.Color.FromArgb(((System.Byte)(215)), ((System.Byte)(255)), ((System.Byte)(231)));
				containerStyle1.FocusItemBackColor = System.Drawing.Color.FromArgb(246, 243, 249);//(248, 248, 250);
				containerStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				containerStyle1.ForeColor = System.Drawing.Color.Black;
				containerStyle1.Shape = PureComponents.NicePanel.Shape.Rounded;
				panelStyle1.ContainerStyle = containerStyle1;
				panelHeaderStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle1.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle1.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle1.FillStyle = PureComponents.NicePanel.FillStyle.HorizontalFading;
				panelHeaderStyle1.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle1.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle1.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
				panelHeaderStyle1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(169)), ((System.Byte)(198)), ((System.Byte)(237)));
				panelHeaderStyle1.Size = PureComponents.NicePanel.PanelHeaderSize.Small;
				panelStyle1.FooterStyle = panelHeaderStyle1;
				panelHeaderStyle2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle2.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle2.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle2.FillStyle = PureComponents.NicePanel.FillStyle.VerticalFading;
				panelHeaderStyle2.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle2.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle2.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
				panelHeaderStyle2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(215)), ((System.Byte)(230)), ((System.Byte)(251)));
				panelHeaderStyle2.Size = PureComponents.NicePanel.PanelHeaderSize.Medium;
				panelStyle1.HeaderStyle = panelHeaderStyle2;
				panel.Style = panelStyle1;
			}
				#endregion Silver
				#region Cyan
			else if(m_ThemeColor == Style.Cyan)
			{
				PureComponents.NicePanel.ContainerImage containerImage1 = new PureComponents.NicePanel.ContainerImage();
				PureComponents.NicePanel.HeaderImage headerImage1 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.HeaderImage headerImage2 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.PanelStyle panelStyle1 = new PureComponents.NicePanel.PanelStyle();
				PureComponents.NicePanel.ContainerStyle containerStyle1 = new PureComponents.NicePanel.ContainerStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle1 = new PureComponents.NicePanel.PanelHeaderStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle2 = new PureComponents.NicePanel.PanelHeaderStyle();
				// 
				// panel
				// 
				panel.BackColor = System.Drawing.Color.Transparent;
				panel.CollapseButton = false;
				containerImage1.Alignment = System.Drawing.ContentAlignment.TopRight;
				containerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.None;
				containerImage1.Image = null;
				containerImage1.Size = PureComponents.NicePanel.ContainerImageSize.Small;
				containerImage1.Transparency = 50;
				panel.ContainerImage = containerImage1;
				panel.ContextMenuButton = false;
				headerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.Applications;
				headerImage1.Image = null;
				panel.FooterImage = headerImage1;
				panel.FooterText = "";
				panel.FooterVisible = false;
				panel.ForeColor = System.Drawing.Color.Black;
				headerImage2.ClipArt = PureComponents.NicePanel.ImageClipArt.Block;
				headerImage2.Image = null;
				panel.HeaderImage = headerImage2;
				panel.HeaderText = "";
				panel.HeaderVisible = false;
				panel.IsExpanded = true;
				panel.Location = new System.Drawing.Point(8, 32);
				panel.Name = "panel";
				panel.OriginalFooterVisible = false;
				panel.OriginalHeight = 0;
				panel.ShowChildFocus = false;
				panel.Size = new System.Drawing.Size(284, 200);
				containerStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(137)), ((System.Byte)(197)), ((System.Byte)(179)));
				containerStyle1.BaseColor = System.Drawing.Color.Transparent;
				containerStyle1.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(53)), ((System.Byte)(131)), ((System.Byte)(111)));
				containerStyle1.BorderStyle = PureComponents.NicePanel.BorderStyle.Solid;
				containerStyle1.CaptionAlign = PureComponents.NicePanel.CaptionAlign.Left;
				containerStyle1.FadeColor = System.Drawing.Color.LightCyan;
				containerStyle1.FillStyle = PureComponents.NicePanel.FillStyle.DiagonalForward;
				containerStyle1.FlashItemBackColor = System.Drawing.Color.Red;
				containerStyle1.FocusItemBackColor = System.Drawing.Color.FromArgb(((System.Byte)(229)), ((System.Byte)(255)), ((System.Byte)(255)));
				containerStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				containerStyle1.ForeColor = System.Drawing.Color.Black;
				containerStyle1.Shape = PureComponents.NicePanel.Shape.Rounded;
				panelStyle1.ContainerStyle = containerStyle1;
				panelHeaderStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle1.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle1.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle1.FillStyle = PureComponents.NicePanel.FillStyle.HorizontalFading;
				panelHeaderStyle1.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle1.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle1.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
				panelHeaderStyle1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(169)), ((System.Byte)(198)), ((System.Byte)(237)));
				panelHeaderStyle1.Size = PureComponents.NicePanel.PanelHeaderSize.Small;
				panelStyle1.FooterStyle = panelHeaderStyle1;
				panelHeaderStyle2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(102)), ((System.Byte)(145)), ((System.Byte)(215)));
				panelHeaderStyle2.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle2.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(9)), ((System.Byte)(42)), ((System.Byte)(127)));
				panelHeaderStyle2.FillStyle = PureComponents.NicePanel.FillStyle.VerticalFading;
				panelHeaderStyle2.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle2.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle2.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
				panelHeaderStyle2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(215)), ((System.Byte)(230)), ((System.Byte)(251)));
				panelHeaderStyle2.Size = PureComponents.NicePanel.PanelHeaderSize.Medium;
				panelStyle1.HeaderStyle = panelHeaderStyle2;
				panel.Style = panelStyle1;
			}
				#endregion Cyan
				#region Orange
			else if(m_ThemeColor == Style.Orange)
			{
				PureComponents.NicePanel.ContainerImage containerImage1 = new PureComponents.NicePanel.ContainerImage();
				PureComponents.NicePanel.HeaderImage headerImage1 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.HeaderImage headerImage2 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.PanelStyle panelStyle1 = new PureComponents.NicePanel.PanelStyle();
				PureComponents.NicePanel.ContainerStyle containerStyle1 = new PureComponents.NicePanel.ContainerStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle1 = new PureComponents.NicePanel.PanelHeaderStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle2 = new PureComponents.NicePanel.PanelHeaderStyle();
				// 
				// panel
				// 
				panel.BackColor = System.Drawing.Color.Transparent;
				panel.CollapseButton = false;
				containerImage1.Alignment = System.Drawing.ContentAlignment.TopRight;
				containerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.None;
				containerImage1.Image = null;
				containerImage1.Size = PureComponents.NicePanel.ContainerImageSize.Small;
				containerImage1.Transparency = 50;
				panel.ContainerImage = containerImage1;
				panel.ContextMenuButton = false;
				headerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.Applications;
				headerImage1.Image = null;
				panel.FooterImage = headerImage1;
				panel.FooterText = "";
				panel.FooterVisible = false;
				panel.ForeColor = System.Drawing.Color.Black;
				headerImage2.ClipArt = PureComponents.NicePanel.ImageClipArt.Block;
				headerImage2.Image = null;
				panel.HeaderImage = headerImage2;
				panel.HeaderText = "";
				panel.HeaderVisible = false;
				panel.IsExpanded = true;
				panel.Location = new System.Drawing.Point(25, 42);
				panel.Name = "panel";
				panel.OriginalFooterVisible = false;
				panel.OriginalHeight = 0;
				panel.ShowChildFocus = false;
				panel.Size = new System.Drawing.Size(311, 206);
				containerStyle1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(180)), ((System.Byte)(97)));
				containerStyle1.BaseColor = System.Drawing.Color.Transparent;
				containerStyle1.BorderColor = System.Drawing.Color.DarkRed;
				containerStyle1.BorderStyle = PureComponents.NicePanel.BorderStyle.Solid;
				containerStyle1.CaptionAlign = PureComponents.NicePanel.CaptionAlign.Left;
				containerStyle1.FadeColor = System.Drawing.Color.NavajoWhite;
				containerStyle1.FillStyle = PureComponents.NicePanel.FillStyle.DiagonalForward;
				containerStyle1.FlashItemBackColor = System.Drawing.Color.Red;
				containerStyle1.FocusItemBackColor = System.Drawing.Color.FromArgb(255, 255, 128);
				containerStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				containerStyle1.ForeColor = System.Drawing.Color.Black;
				containerStyle1.Shape = PureComponents.NicePanel.Shape.Rounded;
				panelStyle1.ContainerStyle = containerStyle1;
				panelHeaderStyle1.BackColor = System.Drawing.Color.Gray;
				panelHeaderStyle1.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle1.FadeColor = System.Drawing.Color.LightGray;
				panelHeaderStyle1.FillStyle = PureComponents.NicePanel.FillStyle.HorizontalFading;
				panelHeaderStyle1.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle1.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle1.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
				panelHeaderStyle1.ForeColor = System.Drawing.Color.Gainsboro;
				panelHeaderStyle1.Size = PureComponents.NicePanel.PanelHeaderSize.Small;
				panelStyle1.FooterStyle = panelHeaderStyle1;
				panelHeaderStyle2.BackColor = System.Drawing.Color.Silver;
				panelHeaderStyle2.ButtonColor = System.Drawing.Color.Gainsboro;
				panelHeaderStyle2.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(51)), ((System.Byte)(51)));
				panelHeaderStyle2.FillStyle = PureComponents.NicePanel.FillStyle.VerticalFading;
				panelHeaderStyle2.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle2.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle2.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
				panelHeaderStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
				panelHeaderStyle2.Size = PureComponents.NicePanel.PanelHeaderSize.Medium;
				panelStyle1.HeaderStyle = panelHeaderStyle2;
				panel.Style = panelStyle1;
			}
			else
			{
				PureComponents.NicePanel.ContainerImage containerImage1 = new PureComponents.NicePanel.ContainerImage();
				PureComponents.NicePanel.HeaderImage headerImage1 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.HeaderImage headerImage2 = new PureComponents.NicePanel.HeaderImage();
				PureComponents.NicePanel.PanelStyle panelStyle1 = new PureComponents.NicePanel.PanelStyle();
				PureComponents.NicePanel.ContainerStyle containerStyle1 = new PureComponents.NicePanel.ContainerStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle1 = new PureComponents.NicePanel.PanelHeaderStyle();
				PureComponents.NicePanel.PanelHeaderStyle panelHeaderStyle2 = new PureComponents.NicePanel.PanelHeaderStyle();
				// 
				// panel
				// 
				panel.BackColor = System.Drawing.Color.Transparent;
				panel.CollapseButton = false;
				containerImage1.Alignment = System.Drawing.ContentAlignment.TopRight;
				containerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.None;
				containerImage1.Image = null;
				containerImage1.Size = PureComponents.NicePanel.ContainerImageSize.Small;
				containerImage1.Transparency = 50;
				panel.ContainerImage = containerImage1;
				panel.ContextMenuButton = false;
				headerImage1.ClipArt = PureComponents.NicePanel.ImageClipArt.Applications;
				headerImage1.Image = null;
				panel.FooterImage = headerImage1;
				panel.FooterText = "";
				panel.FooterVisible = false;
				panel.ForeColor = System.Drawing.Color.Black;
				headerImage2.ClipArt = PureComponents.NicePanel.ImageClipArt.Block;
				headerImage2.Image = null;
				panel.HeaderImage = headerImage2;
				panel.HeaderText = "";
				panel.HeaderVisible = false;
				panel.IsExpanded = true;
				panel.Location = new System.Drawing.Point(25, 42);
				panel.Name = "panel";
				panel.OriginalFooterVisible = false;
				panel.OriginalHeight = 0;
				panel.ShowChildFocus = false;
				panel.Size = new System.Drawing.Size(311, 206);
				containerStyle1.BackColor = SystemColors.Control;//System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(180)), ((System.Byte)(97)));
				containerStyle1.BaseColor = SystemColors.Control;//System.Drawing.Color.Transparent;
				containerStyle1.BorderColor = SystemColors.Control;//System.Drawing.Color.DarkRed;
				containerStyle1.BorderStyle = PureComponents.NicePanel.BorderStyle.Solid;
				containerStyle1.CaptionAlign = PureComponents.NicePanel.CaptionAlign.Left;
				containerStyle1.FadeColor = SystemColors.Control;//System.Drawing.Color.NavajoWhite;
				containerStyle1.FillStyle = PureComponents.NicePanel.FillStyle.DiagonalForward;
				containerStyle1.FlashItemBackColor = System.Drawing.Color.Red;
				containerStyle1.FocusItemBackColor = System.Drawing.Color.FromArgb(255, 255, 128);
				containerStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				containerStyle1.ForeColor = System.Drawing.Color.Black;
				containerStyle1.Shape = PureComponents.NicePanel.Shape.Rounded;
				panelStyle1.ContainerStyle = containerStyle1;
				panelHeaderStyle1.BackColor = System.Drawing.Color.Gray;
				panelHeaderStyle1.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(172)), ((System.Byte)(191)), ((System.Byte)(227)));
				panelHeaderStyle1.FadeColor = System.Drawing.Color.LightGray;
				panelHeaderStyle1.FillStyle = PureComponents.NicePanel.FillStyle.HorizontalFading;
				panelHeaderStyle1.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle1.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle1.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
				panelHeaderStyle1.ForeColor = System.Drawing.Color.Gainsboro;
				panelHeaderStyle1.Size = PureComponents.NicePanel.PanelHeaderSize.Small;
				panelStyle1.FooterStyle = panelHeaderStyle1;
				panelHeaderStyle2.BackColor = System.Drawing.Color.Silver;
				panelHeaderStyle2.ButtonColor = System.Drawing.Color.Gainsboro;
				panelHeaderStyle2.FadeColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(51)), ((System.Byte)(51)));
				panelHeaderStyle2.FillStyle = PureComponents.NicePanel.FillStyle.VerticalFading;
				panelHeaderStyle2.FlashBackColor = System.Drawing.Color.FromArgb(((System.Byte)(243)), ((System.Byte)(122)), ((System.Byte)(1)));
				panelHeaderStyle2.FlashFadeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(215)), ((System.Byte)(159)));
				panelHeaderStyle2.FlashForeColor = System.Drawing.Color.White;
				panelHeaderStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
				panelHeaderStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
				panelHeaderStyle2.Size = PureComponents.NicePanel.PanelHeaderSize.Medium;
				panelStyle1.HeaderStyle = panelHeaderStyle2;
				panel.Style = panelStyle1;
			}
			#endregion Orange
			panel.ResumeLayout(false);
			return panel;
		}
		//public static 
		#endregion
	}

	public enum Style
	{
		None, 
		Blue, 
		Maveric, 
		Silver, 
		Cyan, 
		Orange
	}
}
