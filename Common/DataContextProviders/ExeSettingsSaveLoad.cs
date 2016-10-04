using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Common.DataContextProviders
{
    public static class ExeSettingsSaveLoad
    {
        private static KeyValueConfigurationCollection _configSettings
        {
            get
            {
                if(_config == null)
                    _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                return _config.AppSettings.Settings;
            }
        }
        private static System.Configuration.Configuration _config;
        
        public static void Save(string key, string data)
        {
            if (!_configSettings.AllKeys.Contains(key))
            {
                _configSettings.Add(key, data);
            }
            else
                _configSettings[key].Value = data;

            _config.Save();
        }

        public static string Load(string key)
        {
            if (_configSettings.AllKeys.Contains(key))
                return _configSettings[key].Value;

            return "";
        }
    }
}
