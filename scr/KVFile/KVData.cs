using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Feif.IO
{
    public class KVData
    {
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
                KeyTypeValue[key] = new TypeValue(type.FullName, JsonConvert.SerializeObject(value));
            }
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            if (KeyTypeValue.TryGetValue(key, out var typeValue) && typeof(T).FullName == typeValue.Type)
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
    }
}