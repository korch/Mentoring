using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileVisitor;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor("D:\\Tracker", file => { file.FilteredTypes = new[] {".cs", ".dll", ".docx", ".pdf", ".sln", ".js", ".ts", ".exe"}; },
                SearchOption.TopDirectoryOnly);

            fileVisitor.Start += (o, handler) => {
                Console.WriteLine("Starting...");
            };

            fileVisitor.Finish += (o, handler) => {
                Console.WriteLine("Finish...");
            };

            fileVisitor.DirectoryFinded += (o, handler) => {
                Console.WriteLine($"Directory finded... ParentDirectory: {handler.Directory} | DirectoryName: {handler.Name}");
            };

            fileVisitor.FilteredFileFinded += (o, handler) => {
                Console.WriteLine($"Finded filtered file... DirectoryName: {handler.Directory} | FileName: {handler.Name}");
            };

            fileVisitor.Find();
     
            foreach (FileVisitorInfo item in fileVisitor) {
                Console.WriteLine($"Directory:{item.DirectoryName}, Name: {item.Name}");
            }

            Console.WriteLine($"Count: {fileVisitor.Count}");
            Console.ReadKey(true);
        }
    }
}
