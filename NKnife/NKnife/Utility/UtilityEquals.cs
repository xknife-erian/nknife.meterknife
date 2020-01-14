using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NKnife.Utility
{
    public static class UtilityEquals
    {

        static public bool PairEquals<T>(T a, T b) where T : class
        {
            if ((a == null) && (b == null))
                return true;
            if ((a != null) && (b == null)) return false;
            if ((a == null)) return false;
            //至此，a与b均不应是Null值
            if (!a.Equals(b))
                return false;
            return true;
        }

        /// <summary>
        /// 比较两个字符串的值是否相等，在本方法中当字符串为null时或者为“”时等同对待。
        /// </summary>
        static public bool StringEquals(String a, String b)
        {
            if (a == null && b == null) return true;
            bool aBool = string.IsNullOrWhiteSpace(a);
            bool bBool = string.IsNullOrWhiteSpace(b);
            if (aBool && bBool)
            {
                return true;
            }
            return a == null ? b.Equals(null) : a.Equals(b);
        }

        static public bool EnumerableEquals(IEnumerable a, IEnumerable b)
        {
            if ((a == null) && (b == null))
                return true;
            if (a != null && !ReferenceEquals(a.GetType(), b.GetType())) 
                return false;
            if ((a == null)) 
                return false;
            //至此，a与b均不应是Null值
            IEnumerator e1 = a.GetEnumerator();
            IEnumerator e2 = b.GetEnumerator();
            for (int i = 0; e1.MoveNext() && e2.MoveNext(); i++)
            {
                if (!e1.Current.Equals(e2.Current))
                    return false;
            }
            return true;
        }

        public static bool CollectionsEquals<T>(ICollection<T> a, ICollection<T> b, IComparer<T> comparer)
        {
            if ((a == null) && (b == null))
                return true;
            if (a != null && !ReferenceEquals(a.GetType(), b.GetType())) 
                return false;
            if (a != null && a.Count != b.Count) 
                return false;
            if (a != null)
            {
                IEnumerator e1 = a.GetEnumerator();
                IEnumerator e2 = b.GetEnumerator();
                for (int i = 0; e1.MoveNext() && e2.MoveNext(); i++)
                {
                    if (comparer == null)
                    {
                        var c1 = (T)e1.Current;
                        var c2 = (T)e2.Current;
                        if (!c1.Equals(c2))
                            return false;
                    }
                    else if (0 != comparer.Compare((T)e1.Current, (T)e2.Current))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CollectionsEquals<T>(ICollection<T> expected, ICollection<T> actual)
        {
            return CollectionsEquals(expected, actual, null);
        }

        /// <summary>
        /// 对集合类型的比较。
        /// 重要，指定的两个集合中的数据可能顺序不同。
        /// </summary>
        public static bool CollectionsNoSortedEquals<T>(ICollection<T> a, ICollection<T> b)
        {
            if ((a == null) && (b == null))
                return true;
            if (a != null && !ReferenceEquals(a.GetType(), b.GetType())) 
                return false;
            if ((a == null)) 
                return false;
            if (a.Count != b.Count) return false;
            return a.All(b.Contains);
        }
    }
}
