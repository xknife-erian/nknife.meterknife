using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NKnife.Utility
{
    public class UtilityCollection
    {
        /// <summary>
        ///     Runs an action for all elements in the input.
        /// </summary>
        public static void ForEach<T>(IEnumerable<T> input, Action<T> action)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            foreach (T element in input)
            {
                action(element);
            }
        }

        /// <summary>
        ///     Adds all
        ///     <paramref name="elements" /> to <paramref name="list" />.
        /// </summary>
        public static void AddRange<T>(ICollection<T> list, IEnumerable<T> elements)
        {
            foreach (T o in elements)
                list.Add(o);
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(T[] arr)
        {
            return Array.AsReadOnly(arr);
        }

        /// <summary>
        ///     指示指定的数组是 null 或者 数组为空。
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <returns>
        ///     <c>true</c> if [is null or empty] [the specified objects]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(object[] objects)
        {
            if (objects == null)
                return true;
            if (objects.Length <= 0)
                return true;
            return false;
        }

        /// <summary>
        ///     指示指定的数组是 null 或者 数组为空。
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <returns>
        ///     <c>true</c> if [is null or empty] [the specified objects]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(T[] objects)
        {
            if (objects == null)
                return true;
            if (objects.Length <= 0)
                return true;
            return false;
        }

        /// <summary>
        ///     合并数组
        /// </summary>
        /// <param name="first">第一个数组</param>
        /// <param name="second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static T[] MergerArray<T>(T[] first, T[] second)
        {
            if (first == null && second == null)
                return null;
            if (first != null && second == null)
                return first;
            if (first == null)
                return second;
            var result = new T[first.Length + second.Length];
            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);
            return result;
        }

        /// <summary>
        ///     数组追加
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="value">待追加项</param>
        /// <returns>合并后的数组(数组+待追加项)</returns>
        public static T[] MergerArray<T>(T[] source, T value)
        {
            var result = new T[source.Length + 1];
            source.CopyTo(result, 0);
            result[source.Length] = value;
            return result;
        }

        /// <summary>
        ///     从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="startIndex">原数组的起始位置</param>
        /// <param name="endIndex">原数组的截止位置</param>
        /// <returns></returns>
        public static T[] SplitArray<T>(T[] source, int startIndex, int endIndex)
        {
            var result = new T[endIndex - startIndex + 1];
            for (int i = 0; i <= endIndex - startIndex; i++) result[i] = source[i + startIndex];
            return result;
        }
    }
}