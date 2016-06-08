using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Extensions.IEnumerables;

namespace Extensions.FileSystem
{
    public static class FileSystemInfoExtensions
    {
        public static bool ExistsNow(this FileSystemInfo fs)
        {
            fs.Refresh();
            return fs.Exists;
        }

        public static bool AllExist(this FileSystemInfo fs, params FileSystemInfo[] others)
        {
            return fs.Collate(others).TrueForAll(x => x.ExistsNow());
        }

        public static IEnumerable<FileSystemInfo> WhereNotExists(this FileSystemInfo fs,
            params FileSystemInfo[] others)
        {
            return fs.Collate(others).WhereNot(x => x.ExistsNow());
        }

        


    }
}