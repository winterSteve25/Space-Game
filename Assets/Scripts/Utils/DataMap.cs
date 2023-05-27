using System;
using System.Collections.Generic;

namespace Utils
{
    [Serializable]
    public class DataMap
    {
        private Dictionary<string, object> _data;

        public DataMap()
        {
            _data = new Dictionary<string, object>();
        }

        public DataMap(Dictionary<string, object> data)
        {
            _data = data;
        }

        public void Add<T>(DataMapKey<T> key, T value)
        {
            _data.Add(key.Key, value);
        }

        public void Remove<T>(DataMapKey<T> key)
        {
            _data.Remove(key.Key);
        }

        public Optional<T> Get<T>(DataMapKey<T> key)
        {
            return !_data.ContainsKey(key.Key) ? Optional<T>.Empty() : new Optional<T>((T)_data[key.Key]);
        }
    }
}