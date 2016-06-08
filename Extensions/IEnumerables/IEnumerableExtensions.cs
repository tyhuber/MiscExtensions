using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.IEnumerables
{
    public static class IEnumerableExtensions
    {
        public static string StringJoin<T>(this IEnumerable<T> ie, string join = ",")
        {
            return string.Join(join, ie.Select(x => x.ToString()));
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> ie, Predicate<T> pred)
        {
            return ie.Where(x => !pred.Invoke(x));
        }

        public static List<T> Collate<T>(this T t, params T[] others)
        {
            List<T> l = new List<T> {t};
            l.AddRange(others);
            return l;
        }
    }
}