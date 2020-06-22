using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Slave
{
    class Config
    {
        private static Config _instance = null;
        public static Config getInstance()
        {
            if (_instance == null)
            {
                _instance = new Config();
            }
            return _instance;
        }


        Config()
        {
            LoadGlobalConfig();
        }

        // Global Config
        private string _serverUrl = "";
        private void LoadGlobalConfig()
        {
            var xml = XDocument.Load(@"Config.xml");
            var query = from c in xml.Root.Descendants("Item")
                        select c;
            foreach (var item in query)
            {
                switch ((string)item.Attribute("Key"))
                {
                    case "ServerUrl":
                        _serverUrl = (string)item.Attribute("Value");
                        break;
                }
            }
        }
        public string getServerUrl()
        {
            return _serverUrl;
        }
    }
}
