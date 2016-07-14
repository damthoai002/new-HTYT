using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace UKPI.BlendedReport
{
    public class ConfigUtils
    {
        const string XML_ROOT = "Configurations";
        const string XML_CONFIG = "Config";
        const string XML_CONFIG_NAME = "name";
        const string XML_PARAM = "Param";
        const string XML_PARAM_KEY = "key";
        private static Dictionary<string, Config> configs = new Dictionary<string, Config>();
        private static bool initialized = false;

        public static Dictionary<string, Config> LoadConfiguration(string configPath)
        {
            Dictionary<string, Config> result = new Dictionary<string, Config>();
            XDocument doc = XDocument.Load(configPath);
            XElement root = doc.Element(XML_ROOT);
            if (root != null)
            {
                foreach (XElement cf in root.Elements(XML_CONFIG))
                {
                    XAttribute name = cf.Attribute(XML_CONFIG_NAME);
                    if (name != null)
                    {
                        Config config = new Config(name.Value);
                        foreach (XElement p in cf.Elements(XML_PARAM))
                        {
                            XAttribute key = p.Attribute(XML_PARAM_KEY);
                            if (key != null)
                            {
                                config.Add(key.Value, p.Value);
                            }
                        }
                        result.Add(name.Value, config);
                    }
                }
            }
            return result;
        }

        public static void Initialize(string configPath)
        {
            if (!initialized)
            {
                configs = LoadConfiguration(configPath);
                initialized = true;
            }
        }

        public static Dictionary<string, Config> Configuration
        {
            get
            {
                return configs;
            }
        }
    }

    public class Config : IComparable
    {
        private Dictionary<string, string> values;

        public string Name { get; set; }

        public IEnumerable<string> Keys
        {
            get
            {
                return values.Keys.AsEnumerable();
            }
        }

        public string this[string key]
        {
            get
            {
                if (values.ContainsKey(key))
                {
                    return values[key];
                }
                return string.Empty;
            }
        }

        public void Add(string key, string value)
        {
            if (!values.ContainsKey(key))
                values.Add(key, value);
        }

        public Config(string name)
        {
            Name = name;
            values = new Dictionary<string, string>();
        }

        public Config()
        {
            Name = string.Empty;
            values = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            return Name;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.Name.CompareTo(obj.ToString());
        }

        #endregion
    }
}
