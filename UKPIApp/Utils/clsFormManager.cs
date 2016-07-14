using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Drawing;

namespace UKPI.Utils
{
	/// <summary>
	/// Summary description for FormManager.
	/// </summary>
	/// <remarks>
	/// Author: Nguyễn Minh Đức. G3.
	/// </remarks>
	public class clsFormManager
	{
		private static Hashtable m_formCache = new Hashtable(1);
		private static Hashtable m_formParent = new Hashtable();
		private static Form m_MainForm = null;
		public static bool m_Maximized = false;

		public static bool Maximized
		{
			get{return m_Maximized;}
			set{m_Maximized = value;}
		}

		public static Form MainForm
		{
			get{return m_MainForm;}
			set{m_MainForm = value;}
		}

		public static void Config()
		{
			try
			{
				string str = ConfigurationManager.AppSettings["Form.Maximized"];
				if( str == "true" )
					m_Maximized = true;
				else
					m_Maximized = false;
			}
			catch
			{
			}
		}

		public static void ShowMDIChild(Form frm)
		{
			if(frm.IsDisposed)
				return;
			if(!Contain(frm.GetType()))
			{
				//Add form to cache
				m_formCache[frm.GetType()] = frm;
				frm.Closed+=new EventHandler(frm_Closed);

				if(clsStyleManager.SystemStyle)
					clsStyleManager.FlatSystem(frm);

				//PhongNTT - remove relative with PureComponent.NicePanel
                //clsStyleManager.ChangeStyle(frm);

				if(frm.FormBorderStyle == FormBorderStyle.FixedToolWindow || frm.MaximizeBox == false)
				{
					frm.StartPosition = FormStartPosition.CenterScreen;
					frm.ShowDialog();
				}
				else
				{
					frm.MdiParent = m_MainForm;
					if(Maximized && frm.FormBorderStyle != FormBorderStyle.FixedToolWindow && frm.FormBorderStyle == FormBorderStyle.Sizable && frm.MaximizeBox)
						frm.WindowState = FormWindowState.Maximized;
					frm.Show();
				}

			}
			else
			{
				Form preFrm = (Form)m_formCache[frm.GetType()];
				if(preFrm.Visible)
				{
					preFrm.Show();
					preFrm.Select();
				}
				else
				{
					bool ischild = false;
					foreach(Form child in m_formParent.Keys)
					{
						if(m_formParent[child] == preFrm)
						{
							ischild = true;
							if(m_Maximized)
							{
								child.WindowState = FormWindowState.Maximized;
							}
							else
							{
								child.WindowState = FormWindowState.Normal;
							}

							child.Show();
							child.Select();
							MessageBox.Show(clsResources.GetMessage("messages.form.alreadyopen", preFrm.Text), clsResources.GetMessage("messages.general"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
					}
					if(!ischild)
					{
						preFrm.Show();
						//preFrm.Select();
					}
				}
			}
		}

		public static DialogResult ShowDialogMDIChild(Form frm)
		{
			if(frm.IsDisposed)
				return DialogResult.Cancel;
			//frm.MdiParent = m_MainForm;
			if(clsStyleManager.SystemStyle)
				clsStyleManager.FlatSystem(frm);

            //PhongNTT - remove relative with PureComponent.NicePanel
            //clsStyleManager.ChangeStyle(frm);

			frm.StartPosition = FormStartPosition.CenterParent;

			if(Maximized && frm.FormBorderStyle != FormBorderStyle.FixedToolWindow && frm.FormBorderStyle == FormBorderStyle.Sizable && frm.MaximizeBox)
				frm.WindowState = FormWindowState.Maximized;
			return frm.ShowDialog();
		}

		public static void ShowMDIChild(Form frm, Form parent)
		{
			if(frm.IsDisposed)
				return;
			if(!Contain(frm.GetType()))
			{
				//Add form to cache
				m_formCache[frm.GetType()] = frm;
				m_formParent[frm] = parent;

				frm.Closed+=new EventHandler(frmChild_Closed);
				frm.MdiParent = m_MainForm;

				if(clsStyleManager.SystemStyle)
					clsStyleManager.FlatSystem(frm);

                //PhongNTT - remove relative with PureComponent.NicePanel
                //clsStyleManager.ChangeStyle(frm);

				parent.Hide();

				//if(Maximized && frm.FormBorderStyle == FormBorderStyle.Sizable)
				if(Maximized && frm.FormBorderStyle != FormBorderStyle.FixedToolWindow && frm.FormBorderStyle == FormBorderStyle.Sizable && frm.MaximizeBox)
					frm.WindowState = FormWindowState.Maximized;

				frm.Show();	
			}
			else
			{
				((Form)m_formCache[frm.GetType()]).Show();
				((Form)m_formCache[frm.GetType()]).Select();
				parent.Hide();
			}
		}

		public static void ShowFloatForm(Form frm)
		{
			if(frm == null)
				return;
			if(!Contain(frm.GetType()))
			{
				m_formCache[frm.GetType()] = frm;
				frm.Closed += new EventHandler(frm_Closed);
				frm.Owner = m_MainForm.ActiveMdiChild;
				frm.Show();
			}
			else
			{
				((Form)m_formCache[frm.GetType()]).Show();
				((Form)m_formCache[frm.GetType()]).Select();
			}
		}

		public static DialogResult ShowModelForm(Form frm)
		{
			frm.Closed += new EventHandler(frm_Closed);
			return frm.ShowDialog(m_MainForm);
		}

		public static bool Contain(System.Type formType)
		{
			return m_formCache.Contains(formType);
		}

		private static void frm_Closed(object sender, EventArgs e)
		{
			m_formCache.Remove(sender.GetType());
		}

		private static void frmChild_Closed(object sender, EventArgs e)
		{
			Form frm = (Form)sender;
			Form parent = (Form)m_formParent[frm];
			try
			{
				if(parent != null)
				{
					parent.Show();
				}
			}
			catch
			{}
			m_formParent.Remove(frm);

			m_formCache.Remove(sender.GetType());

		}

	}
}
