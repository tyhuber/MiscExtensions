using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions.FileSystem;
using Extensions.IEnumerables;
using Extensions.Objects;
using static System.Console;

namespace Extensions
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"C:\Git\TestFiles\test.txt";
            string folder = @"C:\Git\TestFiles\folder";
            var fi = file.GetFileInfo();
            WriteLine($"Got file info {fi.FullName}");
            WriteLine($"File text = {fi.ReadAllText()}");
            WriteLine($"File lines = {fi.ReadAllLines().Count}");
            var sibiling = fi.CreateSibling("sibling.txt");
            sibiling.WriteAllText("writin all text");
            var sibsib = sibiling.CreateSibling("yo.txt");
            sibsib.WriteAllLines(new [] {"1","2","3"});

            var dir = folder.GetDirInfo();
            WriteLine($"Got dir {dir.FullName}");
            var childDir = dir.GetChild("f", "u", "ck");
            WriteLine($"Got childDir {childDir.FullName}");
            childDir.CreateChildFile("you", "ok.txt");

            var dirPaths = dir.GetDirPaths("*", SearchOption.AllDirectories);
            WriteLine($"Got dir paths:\n{dirPaths.StringJoin("\n\t")}");
            var fPaths = dir.GetFilePaths("*.*", SearchOption.AllDirectories);
            WriteLine($"Got file paths:\n{fPaths.StringJoin("\n\t")}");

            var sibling = dir.CreateSibling("Sibling");
            dir.Copy(sibling);
            ReadLine();





        }
    }
}
