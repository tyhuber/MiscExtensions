using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

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

        public static FileInfo CreateSibling(this FileInfo f, string fileName)
        {
            var sib = f.GetSibling(fileName);
            if (!sib.ExistsNow()) sib.Create();
            sib.Refresh();
            return sib;
        }

        public static void WriteAllText(this FileInfo f, string text)
        {
            File.WriteAllText(f.FullName,text);
        }

        public static void WriteAllLines(this FileInfo f,IEnumerable<string> lines)
        {
            File.WriteAllLines(f.FullName,lines);
        }

        public static T DeserializeXml<T>(this FileInfo f)
        {
            T t = default(T);
            if (!f.ExistsNow())
            {
                Console.Error.WriteLine($"{f.FullName} does not exist. Cannot deserialize.");
                return t;
            }
            if (!f.Extension.Contains("xml"))
            {
                Console.Error.WriteLine($"{f.FullName} has extension {f.Extension}. Extension must be xml.");
                return t;
            }
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (var reader = new StreamReader(f.FullName))
                {
                    t = (T) ser.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Caught exception deserializing {f.FullName} - {e}");
            }
            return t;
        }

        public static string NameWithoutExt(this FileInfo f) => f.Name.Replace(f.Extension, string.Empty);


    }
}