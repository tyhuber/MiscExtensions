using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Extensions.FileSystem
{
    public static class FileInfoExtensions
    {
        public static string ReadAllText(this FileInfo f)
        {
            if (!f.ExistsNow()) return string.Empty;
            return File.ReadAllText(f.FullName);
        }

        public static List<string> ReadAllLines(this FileInfo f)
        {
            var l = new List<string>();
            if (f.ExistsNow()) l = File.ReadAllLines(f.FullName).ToList();
            return l;
        }

        public static FileInfo GetSibling(this FileInfo t, string fileName)
        {
            return new FileInfo(t.FullName.Replace(t.Name, fileName));
        }
    }
}