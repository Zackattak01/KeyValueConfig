using System;
using System.Collections.Generic;
using System.IO;

namespace KeyValueConfig
{
    public class KeyValueConfig
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();
        public KeyValueConfig(string path)
        {
            KeyValueParser parser = new KeyValueParser(path);
            dict = parser.Parse();
        }

        public bool HasKey(string key)
            => dict.ContainsKey(key);

        public string GetValue(string key)
            => dict[key];
    }
}