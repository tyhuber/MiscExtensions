using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using Extensions.Objects;

namespace Extensions.FileSystem
{
    public static class DirectoryInfoExtensions
    {
        public static DirectoryInfo GetChild(this DirectoryInfo dir, params string[] relPath)
        {
            var exDir = dir.CheckDirExists<DirectoryInfo>();
            if (!exDir.Exists) return exDir.Val;
            return dir.FullName.GetDirInfo(relPath);
        }
        public static FileInfo GetChildFile(this DirectoryInfo dir, params string[] relPath)
        {
            var exDir = dir.CheckDirExists<FileInfo>();
            if (!exDir.Exists) return exDir.Val;
            return dir.FullName.GetFileInfo(relPath);
        }

        public static DirectoryInfo GetSibling(this DirectoryInfo dir, string name)
        {
            var exDir = dir.CheckDirExists<DirectoryInfo>();
            if (!exDir.Exists) return exDir.Val;
            return dir.FullName.Replace(dir.Name, name).GetDirInfo();
        }

        public static DirectoryInfo CreateSibling(this DirectoryInfo dir,string name)
        {
            var exDir = dir.CheckDirExists<DirectoryInfo>();
            if (!exDir.Exists) return exDir.Val;
            var sib = dir.GetSibling(name);
            if(!sib.ExistsNow())sib.Create();
            return sib;
        }

        public static FileInfo CreateChildFile(this DirectoryInfo dir, params string[] relPath)
        {
            var exDir = dir.CheckDirExists<FileInfo>();
            if (!exDir.Exists) return exDir.Val;
            var childDir = dir;
            if (relPath.Length>1)
            {
                childDir = dir.CreateChild(relPath.Take(relPath.Length - 1).ToArray());
            }
            var child = childDir.FullName.GetFileInfo(relPath.Last());
            if (!child.ExistsNow()) child.Create();
            return child;
        }

        public static DirectoryInfo CreateChild(this DirectoryInfo dir, params string[] relPath)
        {
            var exDir = dir.CheckDirExists<DirectoryInfo>();
            if (!exDir.Exists) return exDir.Val;
            var child = dir.GetChild(relPath);
            if(!child.ExistsNow())child.Create();
            return child;
        }

        public static IEnumerable<string> GetDirPaths(this DirectoryInfo dir, string pattern, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var check = dir.CheckDirExists<IEnumerable<string>>();
            if (!check.Exists) return check.Val;
            return Directory.EnumerateDirectories(dir.FullName, pattern, option);
        }
        public static IEnumerable<string> GetFilePaths(this DirectoryInfo dir, string pattern, SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var check = dir.CheckDirExists<IEnumerable<string>>();
            if (!check.Exists) return check.Val;
            return Directory.EnumerateFiles(dir.FullName, pattern, option);
        }

        public static DirectoryInfo Copy(this DirectoryInfo dir, string destPath)
        {
            var exDir = dir.CheckDirExists<DirectoryInfo>();
            if (!exDir.Exists) return exDir.Val;
            var dest = destPath.GetDirInfo();
            if (dest.ExistsNow())dest.Delete(true);
            foreach (var sd in dir.GetDirPaths("*",SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(sd.Replace(dir.FullName, destPath));
            }

            foreach (var filePath in dir.GetFilePaths("*",SearchOption.AllDirectories))
            {
                File.Copy(filePath,filePath.Replace(dir.FullName,destPath));
            }

            return destPath.GetDirInfo();
        }

        public static DirectoryInfo Copy(this DirectoryInfo dir, DirectoryInfo dest)
        {
            return dir.Copy(dest.FullName);
        }

        private static ExDir<T> CheckDirExists<T>(this DirectoryInfo dir)
        {
            return new ExDir<T>(dir);
        }

        internal class ExDir<T>
        {
            public T Val => default(T);
            public bool Exists { get; set; }

            public ExDir(DirectoryInfo dir)
            {
                Exists = dir.ExistsNow();
            }
        }

    }
}