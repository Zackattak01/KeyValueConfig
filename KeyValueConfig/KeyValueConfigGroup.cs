using System;
using System.Collections.Generic;

namespace KeyValueConfig
{
    public class KeyValueConfigGroup
    {
        private Dictionary<string, string> KeyValuePairs { get; }

        public string Key { get; }

        public KeyValueConfigGroup(string key)
        {
            Key = key;

        }

        internal void Add(string key, string value)
        {
            if (KeyValuePairs.ContainsKey(key))
            {
                KeyValuePairs[key] = value;
            }
            else
            {
                KeyValuePairs.Add(key, value);
            }
        }

        public string GetValue(string key)
            => KeyValuePairs[key];

        public bool HasKey(string key)
            => KeyValuePairs.ContainsKey(key);
    }
}