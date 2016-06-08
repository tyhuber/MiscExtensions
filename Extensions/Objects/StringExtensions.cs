using System.Collections.Generic;
using System.IO;
using Extensions.FileSystem;

namespace Extensions.Objects
{
    public static class StringExtensions
    {
        public static string PathCombine(this string s, params string[] other)
        {
            List<string> l = new List<string> {s};
            l.AddRange(other);
            return Path.Combine(l.ToArray());
        }

        public static FileInfo GetFileInfo(this string path)
        {
            return new FileInfo(path);
        }
        public static FileInfo GetFileInfo(this string path, params string[] other)
        {
            return new FileInfo(path.PathCombine(other));
        }
        public static DirectoryInfo GetDirInfo(this string path)
        {
            return new DirectoryInfo(path);
        }
        public static DirectoryInfo GetDirInfo(this string path, params string[] other)
        {
            return new DirectoryInfo(path.PathCombine(other));
        }
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrWhiteSpace(s);
        public static bool Exists(this string path)
        {
            if (!Path.GetExtension(path).IsNullOrEmpty()) return path.FileExists();
            return path.DirExists();
        }
        public static bool FileExists(this string path) => File.Exists(path);
        public static bool FileExists(this string path,params string[] other) => GetFileInfo(path,other).ExistsNow();
        public static bool DirExists(this string path) => Directory.Exists(path);
        public static bool DirExists(this string path,params string[] other) => GetDirInfo(path,other).ExistsNow();

        public static string SurroundWith(this string s, string surround = "\"")
        {
            return $"\"{s}\"";
        }

    }
}