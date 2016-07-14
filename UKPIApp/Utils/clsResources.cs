using System;
using System.Data;
using System.Configuration;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace UKPI.Utils
{
	/// <summary>
	/// This class is used to handle the message resources for the whole system.
	/// </summary>
	/// <remarks>
	/// Author:			Nguyen Minh Duc. G3.
	/// Created date:	14/05/2006
	/// </remarks>
	public class clsResources
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(clsResources));

		//private static string ENGLISH = "EN";
		private static string VIETNAMESE = "VN";

		private static string ENGLISH_MESSAGES_RESOURCES = "Resources.Messages.EN";
		private static string VIETNAMESE_MESSAGES_RESOURCES = "Resources.Messages.VN";

		private static string ENGLISH_TITLES_RESOURCES = "Resources.Titles.EN";
		private static string VIETNAMESE_TITLES_RESOURCES = "Resources.Titles.VN";

		protected static clsMessages[]messages = new clsMessages[0];
		protected static clsMessages[]titles = new clsMessages[0];
		public clsResources()
		{
			
		}
		public static bool m_LogLackResource = false;

		public static bool LogLackResource
		{
			get{return m_LogLackResource;}
			set{m_LogLackResource = value;}
		}

		/// <summary>
		/// Initialize the message resource by the configuration
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void Init()
		{
			//string language = ConfigurationManager.AppSettings["Resources.Language"];
            string language = ConfigurationManager.AppSettings["Resources.Language"];
			if(VIETNAMESE != language)
			{
				clsResources.InitMessage(ConfigurationManager.AppSettings[ENGLISH_MESSAGES_RESOURCES]);
				clsResources.InitTitle(ConfigurationManager.AppSettings[ENGLISH_TITLES_RESOURCES]);
			}
			else
			{
				clsResources.InitMessage(ConfigurationManager.AppSettings[VIETNAMESE_MESSAGES_RESOURCES]);
				clsResources.InitTitle(ConfigurationManager.AppSettings[VIETNAMESE_TITLES_RESOURCES]);
			}

			string logLack = ConfigurationManager.AppSettings["Resources.Lack.Log"];
			if(logLack == "true")
			{
				m_LogLackResource = true;
			}
			else
			{
				m_LogLackResource = false;
			}
		}

		/// <summary>
		/// Reset resources
		/// </summary>
		/// <param name="language"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void Init(string language)
		{
			if(VIETNAMESE != language)
			{
				clsResources.InitMessage(ConfigurationManager.AppSettings[ENGLISH_MESSAGES_RESOURCES]);
				clsResources.InitTitle(ConfigurationManager.AppSettings[ENGLISH_TITLES_RESOURCES]);
			}
			else
			{
				clsResources.InitMessage(ConfigurationManager.AppSettings[VIETNAMESE_MESSAGES_RESOURCES]);
				clsResources.InitTitle(ConfigurationManager.AppSettings[VIETNAMESE_TITLES_RESOURCES]);
			}
		}

        /// <summary>
        /// Inits title by language. This is use for export KPI CheckList
        /// </summary>
        /// <param name="language">Vietnamese (VN) or English (EN)</param>
        public static void InitTitleByLanguage(string language)
        {
			if(VIETNAMESE != language)
			{
				clsResources.InitTitle(ConfigurationManager.AppSettings[ENGLISH_TITLES_RESOURCES]);
			}
			else
			{
				clsResources.InitTitle(ConfigurationManager.AppSettings[VIETNAMESE_TITLES_RESOURCES]);
			}
        }

		/// <summary>
		/// Initialize the message resoucre by the message xml file
		/// </summary>
		/// <param name="filename"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitMessage(string filename)
		{
			DataSet ds = new DataSet("UKPI");

            filename = System.IO.Path.Combine(Application.StartupPath, filename);

			ds.ReadXml(filename);
			try
			{
				DataTable dt = ds.Tables["Messages"];
				if(!dt.Columns.Contains("Name") || !dt.Columns.Contains("Value"))
					throw new Exception("XML file is not valid.");

				DataView vw = dt.DefaultView;
				vw.Sort = "Name";
				clsMessages[]msgs = new clsMessages[vw.Count];
				int i = 0;
				string name = "";
				string value = "";
				foreach(DataRowView rview in vw)
				{
					name = (string)rview["Name"];
					value = (string)rview["Value"];
					msgs[i] = new clsMessages(name, value);
					i ++ ;
				}
				messages = msgs;
			}
			catch(Exception ex)
			{
				throw new Exception("XML file is not valid.", ex);
			}
		}

		/// <summary>
		/// Initialize the message resoucre by the message xml file
		/// </summary>
		/// <param name="filename"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void InitTitle(string filename)
		{
			DataSet ds = new DataSet("UKPI");
            filename = System.IO.Path.Combine(Application.StartupPath, filename);
			ds.ReadXml(filename);
			try
			{
				DataTable dt = ds.Tables["Titles"];
				if(!dt.Columns.Contains("Name") || !dt.Columns.Contains("Value"))
					throw new Exception("XML file is not valid.");

				DataView vw = dt.DefaultView;
				vw.Sort = "Name";
				clsMessages[]msgs = new clsMessages[vw.Count];
				int i = 0;
				string name = "";
				string value = "";
				foreach(DataRowView rview in vw)
				{
					name = (string)rview["Name"];
					value = (string)rview["Value"];
					msgs[i] = new clsMessages(name, value);
					i ++ ;
				}
				titles = msgs;
			}
			catch(Exception ex)
			{
				throw new Exception("XML file is not valid.", ex);
			}
		}

		/// <summary>
		/// Get title value by title name
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static string GetTitle(string key)
		{
			int i = Array.BinarySearch(titles, key);
			if(i < 0)
			{
				if(m_LogLackResource)
					log.Error("Cannot get title of control: " + key);
				return "";
			}
			else
				return titles[i].Value;
		}

		/// <summary>
		/// Get Message by message name
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static string GetMessage(string key)
		{
			int i = Array.BinarySearch(messages, key);
			if(i < 0)
			{
				if(m_LogLackResource)
					log.Error("Cannot get message of key: " + key);
				return "";
			}
			else
			{
				return messages[i].Value;
			}
		}

		/// <summary>
		/// Get Message by message name
		/// </summary>
		/// <example>
		/// GetMessage("errors.invalid", "User name"). errors.invalid={0} is not valid. Please enter again.
		/// return User name is not valid. Please enter again.
		/// </example>
		/// <param name="key"></param>
		/// <param name="arg0"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static string GetMessage(string key, object arg0)
		{
			object[]args = new object[]{arg0};
			return GetMessage(key, args);
		}

		/// <summary>
		/// Get Message by message name
		/// </summary>
		/// <example>
		/// GetMessage("errors.invalid", "User name"). errors.invalid={0} is not valid. Please enter again.
		/// return User name is not valid. Please enter again.
		/// </example>
		/// <param name="key"></param>
		/// <param name="arg0"></param>
		/// <param name="arg1"></param>
		/// <returns></returns>
		public static string GetMessage(string key, object arg0, object arg1)
		{
			object[]args = new object[]{arg0, arg1};
			return GetMessage(key, args);
		}

		/// <summary>
		/// Get Message by message name
		/// </summary>
		/// <example>
		/// GetMessage("errors.invalid", "User name"). errors.invalid={0} is not valid. Please enter again.
		/// return User name is not valid. Please enter again.
		/// </example>
		/// <param name="key"></param>
		/// <param name="arg0"></param>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static string GetMessage(string key, object arg0, object arg1, object arg2)
		{
			object[]args = new object[]{arg0, arg1, arg2};
			return GetMessage(key, args);
		}

		/// <summary>
		/// Get Message by message name
		/// </summary>
		/// <example>
		/// GetMessage("errors.invalid", "User name"). errors.invalid={0} is not valid. Please enter again.
		/// return User name is not valid. Please enter again.
		/// </example>
		/// <param name="key"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static string GetMessage(string key, object[]args)
		{
			string value = GetMessage(key);
			try
			{
				string msg = string.Format(value, args);
				return msg;
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				return value;
			}
		}

		/// <summary>
		/// This class is the value object of one message.
		/// </summary>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		protected class clsMessages:IComparable
		{
			public string Name = "";
			public string Value = "";
			public clsMessages(){}

			public clsMessages(string Name, string Value)
			{
				this.Name = Name;
				this.Value = Value;
			}

			public int CompareTo(object obj)
			{
				return this.Name.CompareTo(obj.ToString());
			}
		}
	}
}
