using System;
using System.Data;
using System.Configuration;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;

namespace UKPI.Core
{
    public class TextResources
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(TextResources));

        public const string DEFAULT_LANGUAGE = "EN";

        public const string MESSAGES_RESOURCES = "Resources.Messages.";

        public const string TITLES_RESOURCES = "Resources.Titles.";

        protected static Dictionary<string, Message[]> messages = new Dictionary<string, Message[]>();
        protected static Dictionary<string, Message[]> titles = new Dictionary<string, Message[]>();

        public static string CurrentLanguage = string.Empty;

        public TextResources()
        {
        }
        public static bool m_LogLackResource = false;

        public static bool LogLackResource
        {
            get { return m_LogLackResource; }
            set { m_LogLackResource = value; }
        }

        public static void Init(string languageCode, string msgPath, string titlePath)
        {
            if (!string.IsNullOrEmpty(msgPath))
                TextResources.InitMessage(languageCode, msgPath);
            if (!string.IsNullOrEmpty(titlePath))
                TextResources.InitTitle(languageCode, titlePath);
            CurrentLanguage = languageCode.ToUpper();
        }

        public static void InitMessage(string languageCode, string filename)
        {
            DataSet ds = new DataSet("Resources");
            ds.ReadXml(filename);
            try
            {
                DataTable dt = ds.Tables["Messages"];
                if (!dt.Columns.Contains("Name") || !dt.Columns.Contains("Value"))
                    throw new Exception("XML file is not valid.");

                DataView vw = dt.DefaultView;
                vw.Sort = "Name";
                Message[] msgs = new Message[vw.Count];
                int i = 0;
                string name = "";
                string value = "";
                foreach (DataRowView rview in vw)
                {
                    name = (string)rview["Name"];
                    value = (string)rview["Value"];
                    msgs[i] = new Message(name, value);
                    i++;
                }
                if (!messages.ContainsKey(languageCode.ToUpper()))
                    messages.Add(languageCode.ToUpper(), new Message[0]);
                messages[languageCode.ToUpper()] = msgs;
            }
            catch (Exception ex)
            {
                throw new Exception("XML file is not valid.", ex);
            }
        }

        public static void InitTitle(string languageCode, string filename)
        {
            DataSet ds = new DataSet("UKPI");
            ds.ReadXml(filename);
            try
            {
                DataTable dt = ds.Tables["Titles"];
                if (!dt.Columns.Contains("Name") || !dt.Columns.Contains("Value"))
                    throw new Exception("XML file is not valid.");

                DataView vw = dt.DefaultView;
                vw.Sort = "Name";
                Message[] msgs = new Message[vw.Count];
                int i = 0;
                string name = "";
                string value = "";
                foreach (DataRowView rview in vw)
                {
                    name = (string)rview["Name"];
                    value = (string)rview["Value"];
                    msgs[i] = new Message(name, value);
                    i++;
                }
                if (!titles.ContainsKey(languageCode.ToUpper()))
                    titles.Add(languageCode.ToUpper(), new Message[0]);
                titles[languageCode.ToUpper()] = msgs;
            }
            catch (Exception ex)
            {
                throw new Exception("XML file is not valid.", ex);
            }
        }

        public static string GetTitle(string languageCode, string key)
        {
            if (!titles.ContainsKey(languageCode.ToUpper())) return string.Empty;
            int i = Array.BinarySearch(titles[languageCode.ToUpper()], key);
            if (i < 0)
            {
                if (m_LogLackResource)
                    log.Error("Cannot get title of control: " + key);
                return "";
            }
            else
                return titles[languageCode.ToUpper()][i].Value;
        }

        public static string GetTitle(string key)
        {
            return GetTitle(CurrentLanguage, key);
        }

        public static string GetMessage(string languageCode, string key)
        {
            if (!messages.ContainsKey(languageCode.ToUpper())) return string.Empty;
            int i = Array.BinarySearch(messages[languageCode.ToUpper()], key);
            if (i < 0)
            {
                if (m_LogLackResource)
                    log.Error("Cannot get message of key: " + key);
                return "";
            }
            else
            {
                return messages[languageCode.ToUpper()][i].Value;
            }
        }

        public static string GetMessage(string key)
        {
            return GetMessage(CurrentLanguage, key);
        }

        public static string GetMessage(string languageCode, string key, object arg0)
        {
            if (!messages.ContainsKey(languageCode.ToUpper())) return string.Empty;
            object[] args = new object[] { arg0 };
            return GetMessage(languageCode, key, args);
        }

        public static string GetMessage(string key, object arg0)
        {
            return GetMessage(CurrentLanguage, key, arg0);
        }

        public static string GetMessage(string languageCode, string key, object arg0, object arg1)
        {
            if (!messages.ContainsKey(languageCode.ToUpper())) return string.Empty;
            object[] args = new object[] { arg0, arg1 };
            return GetMessage(languageCode, key, args);
        }

        public static string GetMessage(string key, object arg0, object arg1)
        {
            return GetMessage(CurrentLanguage, key, arg0, arg1);
        }

        public static string GetMessage(string languageCode, string key, object arg0, object arg1, object arg2)
        {
            if (!messages.ContainsKey(languageCode.ToUpper())) return string.Empty;
            object[] args = new object[] { arg0, arg1, arg2 };
            return GetMessage(languageCode, key, args);
        }

        public static string GetMessage(string key, object arg0, object arg1, object arg2)
        {
            return GetMessage(CurrentLanguage, key, arg0, arg1, arg2);
        }

        public static string GetMessage(string languageCode, string key, object[] args)
        {
            if (!messages.ContainsKey(languageCode.ToUpper())) return string.Empty;
            string value = GetMessage(languageCode, key);
            try
            {
                string msg = string.Format(value, args);
                return msg;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return value;
            }
        }

        public static string GetMessage(string key, object[] args)
        {
            return GetMessage(CurrentLanguage, key, args);
        }

        protected class Message : IComparable
        {
            public string Name = "";
            public string Value = "";
            public Message() { }

            public Message(string Name, string Value)
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
