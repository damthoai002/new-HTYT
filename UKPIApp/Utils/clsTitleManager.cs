using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Drawing;

using UKPI.Controls;

namespace UKPI.Utils
{

	/// <summary>
	/// Init title for all forms.
	/// </summary>
	/// Author:			Nguyen Minh Duc. G3.
	/// Created date:	14/05/2006
	/// </remarks>
	public class clsTitleManager
	{
		private static string STAR = "*";
		public clsTitleManager()
		{
		}

		#region Init Title
		/// <summary>
		/// Init title all the control in form
		/// </summary>
		/// <param name="frm"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitTitle(Form frm)
		{
			if(frm == null)
				return;

			frm.SuspendLayout();

			frm.Text = clsResources.GetTitle(frm.Name + ".Title");
			foreach(Control control in frm.Controls)
			{
				InitTitle(frm, control);
			}

			frm.ResumeLayout(false);
		}

		/// <summary>
		/// Init ToolTip for this form
		/// </summary>
		/// <param name="frm"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitToolBarToolTip(Form frm)
		{
			ToolBar tb = null;
			string formName = frm.Name;
			foreach(Control ctrl in frm.Controls)
			{
				tb = ctrl as ToolBar;
				if(tb != null)
				{
					foreach(MDToolBarButton tbb in tb.Buttons)
					{
						tbb.ToolTipText = clsResources.GetTitle(formName + tbb.Name);
					}
				}
			}
		}
		/// <summary>
		/// Init title all the control in form
		/// </summary>
		/// <param name="frm"></param>
		/// <param name="control"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitTitle(Form frm, Control control)
		{
			if(frm == null || control == null)
				return;

			Button btn = control as Button;
			Label lbl = control as Label;
			CheckBox chk  = control as CheckBox;
			RadioButton rad = control as RadioButton;
			GroupBox grp = control as GroupBox;
			DataGrid grd = control as DataGrid;
            DataGridView grdview = control as DataGridView;
            TabControl tabGroup = control as TabControl;

            if (lbl != null && lbl.Text != STAR)
            {
                control.Text = clsResources.GetTitle(frm.Name + "." + control.Name);
                lbl.AutoSize = true;
            }
            else if (btn != null || chk != null || rad != null)
            {
                control.Text = clsResources.GetTitle(frm.Name + "." + control.Name);
            }
            else if (grp != null)
            {
                if (!clsStyleManager.ColorStyle)
                    control.Text = clsResources.GetTitle(frm.Name + "." + control.Name);

                foreach (Control sub in control.Controls)
                {
                    InitTitle(frm, sub);
                }
            }
            else if (grd != null)
            {
                grd.CaptionText = clsResources.GetTitle(frm.Name + "." + control.Name);
                foreach (DataGridTableStyle tblStyle in grd.TableStyles)
                {
                    foreach (DataGridColumnStyle col in tblStyle.GridColumnStyles)
                    {
                        string title = clsResources.GetTitle(frm.Name + "." + grd.Name + "." + tblStyle.MappingName + "." + col.MappingName);
                        if (title.Length > 0)
                        {
                            //							char chr = title[title.Length - 1];
                            //							if(col.Alignment == HorizontalAlignment.Right && (chr == 'i' || chr == 't' || chr == 'l' || chr == 'u' || chr == 'h'))
                            //								title = title + ".";
                            //							col.HeaderText = title
                            col.HeaderText = clsResources.GetTitle(frm.Name + "." + grd.Name + "." + tblStyle.MappingName + "." + col.MappingName);
                        }

                    }
                }
            }
            else if (grdview != null)
            {
                foreach (DataGridViewColumn col in grdview.Columns)
                {
                    string title = clsResources.GetTitle(frm.Name + "." + grdview.Name + "." + col.DataPropertyName);
                    if (title.Length > 0)
                    {
                        col.HeaderText = clsResources.GetTitle(frm.Name + "." + grdview.Name + "." + col.DataPropertyName);
                    }
                }
            }
            else if (tabGroup != null)
            {
                foreach (TabPage tp in tabGroup.TabPages)
                {
                    tp.Text = clsResources.GetTitle(frm.Name + "." + tp.Name);
                    foreach (Control sub in tp.Controls)
                    {
                        InitTitle(frm, sub);
                    }
                }
            }
            else
            {
                foreach (Control sub in control.Controls)
                {
                    InitTitle(frm, sub);
                }
            }
		}

		/// <summary>
		/// Init title column header for Main Menu.
		/// </summary>
		/// <param name="frm"></param>
		/// <param name="control"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitTitle(Form frm, MainMenu main)
		{
			if(frm == null || main == null)
				return;

			frm.SuspendLayout();
			foreach(MDMenuItem item in main.MenuItems)
			{
				InitTitle(frm, item);
			}
			frm.ResumeLayout(false);
		}
		/// <summary>
		/// Init title column header for Main Menu.
		/// </summary>
		/// <param name="frm"></param>
		/// <param name="control"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitTitle(Form frm, MDMenuItem item)
		{
			if(frm == null || item == null)
				return;

			if(item.Name == clsConstants.MENU_SEPARATE)
				item.Text = clsConstants.MINUS;
			else
				item.Text = clsResources.GetTitle(frm.Name + "." + item.Name);

			foreach(MDMenuItem sub in item.MenuItems)
			{
				InitTitle(frm, sub);
			}
		}
		#endregion Init Title
	}
}
