using System.IO;
using System;

namespace Feif.IO
{
    public class KVFile : IDisposable
    {
        public readonly KVData Data;
        public string Location;

        public static KVFile Open(string path, FileMode mode = FileMode.OpenOrCreate)
        {
            if (mode == FileMode.OpenOrCreate && File.Exists(path))
            {
                return new KVFile(path, KVData.Create(File.ReadAllBytes(path)));
            }
            else
            {
                return new KVFile(path);
            }
        }

        public KVFile(string path)
        {
            Location = path;
            if (File.Exists(path))
            {
                Data = KVData.Create(File.ReadAllBytes(path));
            }
            else
            {
                Data = new KVData();
            }
        }

        public KVFile(string path, KVData data)
        {
            Location = path;
            Data = data;
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            if (Data.TryGetValue<T>(key, out var value))
            {
                return value;
            }
            return defaultValue;
        }

        public bool TryGet<T>(string key, out T value, T defaultValue = default)
        {
            if (Data.TryGetValue<T>(key, out value))
            {
                return true;
            }
            value = defaultValue;
            return false;
        }

        public void Set<T>(string key, T value)
        {
            Data[key] = value;
        }

        public void Remove(string key)
        {
            Data.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public void Save()
        {
            File.WriteAllBytes(Location, Data.GetBytes());
        }

        public void Dispose()
        {
            Save();
        }
    }
}
