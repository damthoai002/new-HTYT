using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace UKPI.Controls
{
	/// <summary>
	/// Summary description for MyMenuItem.
	/// </summary>
	/// <remarks>
	/// Author:	Nguyễn Minh Đức. G3
	/// </remarks>
	public class MDMenuItem:MenuItem
	{
		protected Icon m_Icon = null;
		protected Font m_Font = new Font("Arial",8);
		protected string m_FormName = "";
		//protected string m_Name = "";
		protected string m_Description = "";
		protected int m_ID = 0;

		private Brush br1 = new SolidBrush(SystemColors.Menu);//(Color.White);
		private Brush br2 = new SolidBrush(SystemColors.Highlight);
		private static string SEPARATE = "-";
		private static Pen pen = new Pen(Color.FromArgb(172,168,153));//(127,157,185),1);//180, 186, 216//188, 194, 220

		public Icon Icon
		{
			get
			{
				return m_Icon;
			}
			set
			{
				m_Icon = value;
				if(m_Icon != null)
				{
					m_Icon = new Icon(m_Icon,16,16);
				}
			}
		}

		public Font Font
		{
			get{return m_Font;}
			set{m_Font = value;}
		}

		public string FormName
		{
			get{return m_FormName;}
			set{m_FormName = value;}
		}
        //public string Name
        //{
        //    get{return m_Name;}
        //    set{m_Name = value;}
        //}
		public string Description
		{
			get{return m_Description;}
			set{m_Description = value;}
		}

		public int ID
		{
			get{return m_ID;}
			set{m_ID = value;}
		}


		public MDMenuItem()
		{
		}

		private string GetRealText()
		{

			string s = Text;

			// Append shortcut if one is set and it should be visible

			if (ShowShortcut && Shortcut != Shortcut.None) 
			{

				// To get a string representation of a Shortcut value, cast
				// it into a Keys value and use the KeysConverter class (via TypeDescriptor).

				Keys k = (Keys) Shortcut;
				s = s + Convert.ToChar(9) + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(k);

			}
			return s;

		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{

			// OnDrawItem perfoms the task of actually drawing the item after
			// measurement is complete

			base.OnDrawItem(e);

			Brush br;

			Rectangle rcBk = e.Bounds;

			if(this.Text == SEPARATE)
			{
				e.Graphics.FillRectangle(br1, rcBk);
				e.Graphics.DrawLine(pen,rcBk.Left + 1, rcBk.Top + 4, rcBk.Right - 1, rcBk.Top + 4);
				return;
			}

			if ((e.State & DrawItemState.Selected) != 0)
			{
				br = br2;
			}
			else 
			{
				br = br1;
			}

			// Draw the main rectangle

			e.Graphics.FillRectangle(br, rcBk);

			if (m_Icon != null) 
			{
				e.Graphics.DrawIcon(m_Icon, rcBk.Left + 2, rcBk.Top + 2);
			}

			// Leave room for accelerator key

			StringFormat sf = new StringFormat();
			sf.HotkeyPrefix = HotkeyPrefix.Show;
			// Draw the actual menu text

			br = new SolidBrush(e.ForeColor);

			e.Graphics.DrawString(GetRealText(), m_Font, br, rcBk.Left + 20, rcBk.Top + 2, sf);

		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{

			// The MeasureItem event along with the OnDrawItem event are the two key events
			// that need to be handled in order to create owner drawn menus.
			// Measure the string that makes up a given menu item and use it to set the 
			// size of the menu item being drawn.

			StringFormat sf = new StringFormat();
			sf.HotkeyPrefix = HotkeyPrefix.Show;
			base.OnMeasureItem(e);
			if(this.Text == SEPARATE)
				e.ItemHeight = 9;
			else
				e.ItemHeight = 20;
			e.ItemWidth = Convert.ToInt32(e.Graphics.MeasureString(GetRealText(), m_Font, 10000, sf).Width) + 10;

		}
	}
}
