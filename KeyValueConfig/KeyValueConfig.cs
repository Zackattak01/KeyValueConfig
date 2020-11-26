using System;
using System.Collections.Generic;
using System.IO;

namespace KeyValueConfig
{
    public class Config
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();

        private Dictionary<string, KeyValueConfigGroup> groups = new Dictionary<string, KeyValueConfigGroup>();

        private ConfigParser parser;
        public Config(string path)
        {
            parser = new ConfigParser(path);
            Reload();
        }

        public bool HasKey(string key)
            => dict.ContainsKey(key);

        public string GetValue(string key)
            => dict[key];

        public bool HasGroupKey(string key)
            => groups.ContainsKey(key);

        public KeyValueConfigGroup GetGroup(string key)
            => groups[key];

        public void Reload()
        {
            parser.Parse();
            dict = parser.KeyValuePairs;
            groups = parser.Groups;
        }

    }
}