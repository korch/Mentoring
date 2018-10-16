using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileVisitor
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private string _path;
        private List<FileVisitorInfo> _fileVisitorInfos;
        private SearchOption _searchOption;

        public string[] FilteredTypes;

        public event FileStateHandler Start;
        public event FileStateHandler Finish;
        public event FileStateHandler FileFinded;
        public event FileStateHandler DirectoryFinded;
        public event FileStateHandler FilteredFileFinded;
        public event FileStateHandler FilteredDirectoryFinded;

        public FileSystemVisitor(string path)
        {
            _path = path;
            _fileVisitorInfos = new List<FileVisitorInfo>();
            _searchOption = SearchOption.AllDirectories;
        }

        public FileSystemVisitor(string path, Action<FileSystemVisitor> action, SearchOption option = SearchOption.AllDirectories)
        {
            _path = path;
            _fileVisitorInfos = new List<FileVisitorInfo>();
            _searchOption = option;
            action.Invoke(this);
        }

        public int Count => _fileVisitorInfos.Count;

        public void Find()
        {
            Start?.Invoke(null, new FileEventHandler(string.Empty, string.Empty));

            var root = new DirectoryInfo(_path);
            FindFilesAndDirectories(root);

            Finish?.Invoke(null, new FileEventHandler(string.Empty, string.Empty));
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in _fileVisitorInfos) {
                yield return item;
            }
        }

        private void FindFilesAndDirectories(DirectoryInfo root)
        {
            FileInfo[] files = null;
            var filteringList = new List<string>(FilteredTypes);
            try {
                files = root.GetFiles("*.*");
            }
       
            catch (UnauthorizedAccessException e) {
            }

            catch (DirectoryNotFoundException e) {
                Console.WriteLine(e.Message);
            }

            if (files == null) {
                return;
            }
   
            foreach (var file in files) {
                if (filteringList.Count > 0) {
                    if (!filteringList.Contains(file.Extension)) {
                        FilteredFileFinded?.Invoke(null, new FileEventHandler(file.Name, file.DirectoryName));
                        _fileVisitorInfos.Add(new FileVisitorInfo { Name = file.Name, DirectoryName = file.DirectoryName });
                    }    
                } else {
                    FileFinded?.Invoke(null, new FileEventHandler(file.Name, file.DirectoryName));
                    _fileVisitorInfos.Add(new FileVisitorInfo { Name = file.Name, DirectoryName = file.DirectoryName });
                }
            }

            if (_searchOption != SearchOption.AllDirectories)
                return;

            var subDirectories = root.GetDirectories();
            foreach (var dirInfo in subDirectories)
            {
                DirectoryFinded?.Invoke(null, new FileEventHandler(dirInfo.Name, dirInfo.Parent?.Name));
                _fileVisitorInfos.Add(new FileVisitorInfo { Name = dirInfo.Name, DirectoryName = dirInfo.Parent?.Name });

                FindFilesAndDirectories(dirInfo);
            }
        }
    }
}
