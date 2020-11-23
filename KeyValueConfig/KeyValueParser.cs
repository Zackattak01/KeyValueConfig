using System;
using System.Collections.Generic;
using System.IO;

namespace KeyValueConfig
{
    public class KeyValueParser
    {

        public string Path { get; }
        public KeyValueParser(string path)
        {
            Path = path;
        }

        public Dictionary<string, string> Parse()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (var sr = new StreamReader(Path))
            {

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] splitLine = line.Split('=', 2);
                    string key = splitLine[0];
                    string value = splitLine[1];


                    //ensures it wont fail on duplicate key value pairs
                    //this will take the last key value pair
                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, value);
                    }
                    else
                    {
                        dict[key] = value;
                    }
                }

            }
            return dict;
        }
    }
}
