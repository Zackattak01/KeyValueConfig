using System;
using System.Collections.Generic;
using System.IO;

namespace KeyValueConfig
{
    public class Config
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();

        private KeyValueParser parser;
        public Config(string path)
        {
            parser = new KeyValueParser(path);
            dict = parser.Parse();
        }

        public bool HasKey(string key)
            => dict.ContainsKey(key);

        public string GetValue(string key)
            => dict[key];

        public void Reload()
            => dict = parser.Parse();
    }
}