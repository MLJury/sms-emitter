using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kama.SmsService.Tools
{
    class ObjectSerializer : AppCore.IObjectSerializer
    {
        public T Deserialize<T>(string serializedValue)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedValue);

        public string Serialize(object obj)
            => Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }
}
