using System;
using System.Windows.Forms;

namespace UKPI.Controls
{
	/// <summary>
	/// Summary description for MDToolBarButton.
	/// </summary>
	public class MDToolBarButton:ToolBarButton
	{

		protected string m_FormName = "";
		protected string m_Name = "";

		public string FormName
		{
			get{return m_FormName;}
			set{m_FormName = value;}
		}

		public string Name
		{
			get{return m_Name;}
			set{m_Name = value;}
		}

		public MDToolBarButton()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
