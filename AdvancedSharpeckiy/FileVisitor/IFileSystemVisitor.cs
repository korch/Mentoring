using System;
using System.Collections.Generic;
using System.Text;

namespace FileVisitor
{
    public delegate void FileStateHandler(object sender, FileEventHandler args);
    interface IFileSystemVisitor
    {
        int Count { get; }
        event FileStateHandler Start;
        event FileStateHandler Finish;
        event FileStateHandler FileFinded;
        event FileStateHandler DirectoryFinded;
        event FileStateHandler FilteredFileFinded;
        event FileStateHandler FilteredDirectoryFinded;
        void Find();
    }
}
