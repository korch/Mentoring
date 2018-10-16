using System;
using System.Collections.Generic;
using System.Text;

namespace FileVisitor
{
    public class FileEventHandler
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public bool Stop { get; set; }

        public FileEventHandler(string name, string dictionary)
        {
            Name = name;
            Directory = dictionary;
        }
    }
}
