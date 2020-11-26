using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;

namespace KeyValueConfig
{
    internal class ConfigParser
    {
        public Dictionary<string, string> KeyValuePairs { get; private set; }

        public Dictionary<string, KeyValueConfigGroup> Groups { get; private set; }

        private KeyValueConfigGroup currentGroup;

        //essentially only used for throwing errors
        private int currentLine;

        public string Path { get; }
        public ConfigParser(string path)
        {
            Path = path;


        }

        public void Parse()
        {
            currentGroup = null;
            KeyValuePairs = new Dictionary<string, string>();
            Groups = new Dictionary<string, KeyValueConfigGroup>();
            currentLine = 0;

            using (var sr = new StreamReader(Path))
            {

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    currentLine++;

                    //primitive parsing for attributes
                    if (line.StartsWith('[') && line.EndsWith(']'))
                    {
                        // parse as attrib
                    }
                    else
                    {
                        ParseAsKeyValue(line);
                    }
                }

            }
        }

        private void ParseAsKeyValue(string line)
        {
            string[] splitLine = line.Split('=', 2);

            if (splitLine.Length != 2)
                ThrowConfigException("Invalid key value pair");

            string key = splitLine[0];
            string value = splitLine[1];

            //ensures it wont fail on duplicate key value pairs
            //this will take the last key value pair
            if (KeyValuePairs.ContainsKey(key))
            {
                KeyValuePairs[key] = value;
                AddToCurrentGroup(key, value);
            }
            else
            {
                KeyValuePairs.Add(key, value);
                AddToCurrentGroup(key, value);
            }
        }

        private void AddToCurrentGroup(string key, string value)
        {
            if (currentGroup != null)
            {
                currentGroup.Add(key, value);
            }
        }

        private void ParseAsAttribute(string line)
        {
            //remove begginning [ and ending ]
            line = new string(line.Skip(1).ToArray());
            line = new string(line.Take(line.Length - 1).ToArray());

            //remove whitespace
            line.Replace(" ", "");
            string[] splitLine = line.Split(':', 2);

            if (splitLine.Length != 2)
                ThrowConfigException("Invalid attribute syntax");

            string token = splitLine[0].ToLower();
            string key = splitLine[1];

            //for now hardcheck for attribute token
            if (token == "group")
            {
                if (Groups.ContainsKey(key))
                {
                    ThrowConfigException("Group key already in use");
                }

                currentGroup = new KeyValueConfigGroup(key);
                Groups.Add(key, currentGroup);
            }
            else
            {
                ThrowConfigException("Unknown attribute token");
            }
        }

        internal void ThrowConfigException(string message)
        {
            throw new InvalidConfigException($"Line {currentLine}: {message}");
        }
    }
}
