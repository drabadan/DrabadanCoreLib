using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Common.DataContextProviders
{
    public static class JSONSaveLoad        
    {
        public static void Save<T>(string key, T obj)
        {
            var converted = JsonConvert.SerializeObject(obj);
            ExeSettingsSaveLoad.Save(key, converted);
        }

        public static T Load<T>(string key)
        {
            string str = ExeSettingsSaveLoad.Load(key);
            if(!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str))
                try
                {
                    return JsonConvert.DeserializeObject<T>(str);                   
                }
                catch (Exception){}
            return default(T);
        }
    }
}
