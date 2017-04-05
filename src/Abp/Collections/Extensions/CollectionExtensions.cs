using System;
using System.Collections.Generic;

namespace Abp.Collections.Extensions
{
    /// <summary>
    /// Extension methods for Collections.
    /// Collections���͵Ļ�չ����
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// �������Ķ����Ƿ�Ϊnull������Ϊ��
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// ���һ�������������������ڼ�����
        /// </summary>
        /// <param name="source">Collection / Դ����</param>
        /// <param name="item">Item to check and add / ���Ⲣ��ӵ���</param>
        /// <typeparam name="T">Type of the items in the collection / �������������</typeparam>
        /// <returns>Returns True if added, returns False if not. / �������˷���True,û��ӷ���false</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}