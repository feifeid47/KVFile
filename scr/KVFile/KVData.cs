using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Feif.IO
{
    public class KVData
    {
        public static KVData Create(byte[] data)
        {
            return JsonConvert.DeserializeObject<KVData>(Encoding.UTF8.GetString(data));
        }

        [JsonRequired]
        private Dictionary<string, TypeValue> KeyTypeValue = new Dictionary<string, TypeValue>();

        [JsonIgnore]
        public IEnumerable<string> Keys { get => KeyTypeValue.Keys; }

        public object this[string key]
        {
            get
            {
                if (KeyTypeValue.TryGetValue(key, out var typeValue))
                {
                    var type = Type.GetType(typeValue.Type);
                    return JsonConvert.DeserializeObject(typeValue.Value, type);
                }
                return null;
            }
            set
            {
                var type = value.GetType();
                KeyTypeValue[key] = new TypeValue(type.ToString(), JsonConvert.SerializeObject(value));
            }
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            if (KeyTypeValue.TryGetValue(key, out var typeValue) && typeof(T).ToString() == typeValue.Type)
            {
                var type = Type.GetType(typeValue.Type);
                value = (T)JsonConvert.DeserializeObject(typeValue.Value, type);
                return true;
            }
            value = default;
            return false;
        }

        public void Remove(string key)
        {
            KeyTypeValue.Remove(key);
        }

        public void Clear()
        {
            KeyTypeValue.Clear();
        }

        public bool ContainsKey(string key)
        {
            return KeyTypeValue.ContainsKey(key);
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
        }
    }
}
