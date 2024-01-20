using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EJClient
{
    class LocalCache
    {
        Dictionary<string, string> _data;
        public LocalCache()
        {
            try
            {
                _data = File.ReadAllText("./LocalCache.json", Encoding.UTF8).ToJsonObject<Dictionary<string, string>>();
            }
            catch 
            {
                _data = new Dictionary<string, string>();
            }            
        }

        public string this[string key]
        {
            get
            {
                if (_data.ContainsKey(key) == false)
                    return null;
                return _data[key];
            }
            set
            {
                _data[key] = value;
            }
        }

        public void Save()
        {
            File.WriteAllText("./LocalCache.json",_data.ToJsonString() , Encoding.UTF8);
        }
    }
}
