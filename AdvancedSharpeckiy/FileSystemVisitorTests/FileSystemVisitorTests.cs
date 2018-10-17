using System;
using System.IO;
using FileVisitor;
using NUnit.Framework;

namespace FileSystemVisitorTests
{
    [TestFixture]
    public class FileSystemVisitorTests
    {
        [Test]
        public void GetAllFilesFromDirectoryTest()
        {
            var expectedResult = 7;
            // we axactly know that this folder contains only six files
            var path = "D:\\scripts";
            var file = new FileSystemVisitor(path);

            file.Find();
            Assert.AreEqual(expectedResult, file.Count);
        }

        [TestCase(new[] { ".txt" }, 3)]
        [TestCase(new[] { ".bmp" }, 1)]
        [TestCase(new[] { ".docx" }, 2)]
        [TestCase(new[] { ".rtf" }, 1)]
        [TestCase(new[] { "" }, 0)]
        public void GetFilteredFilesFromDirectoryTest(string[] filteredTypes, int expectedResult)
        {
            
            // we axactly know that this folder contains only (3 txt file, 1 bmp file, 2 docx file, 1rtf file)
            var path = "D:\\Scripts";
            var file = new FileSystemVisitor(path, a => a.FilteredTypes = filteredTypes);
            var root = new DirectoryInfo(path);

            file.FindFilesAndDirectories(root);
            Assert.AreEqual(expectedResult, file.Count);
        }

        [Test]
        public void GetAllFilesAndFoldersFromDirectoryWithSubDirectoriesTest()
        {
            var expectedResult = 8;
            // we axactly know how many files and folders exist in directories.
            var path = "D:\\shared";
            var file = new FileSystemVisitor(path);
            var root = new DirectoryInfo(path);

            file.FindFilesAndDirectories(root);
            Assert.AreEqual(expectedResult, file.Count);
        }

        [Test]
        public void DirectoryNotFoundedExceptionTest()
        {
            var path = "D:\\BESTOFTHEBESTFOLDER";
            var file = new FileSystemVisitor(path);
            var root = new DirectoryInfo(path);

            Assert.Throws<DirectoryNotFoundException>(() => file.FindFilesAndDirectories(root));
        }
    }
}
