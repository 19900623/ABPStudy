using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;

namespace Abp
{
    /// <summary>
    /// A shortcut to use <see cref="Random"/> class.Also provides some useful methods.
    /// <see cref="Random"/>�Ŀ��ʹ���࣬ͬʱ�ṩһЩ���õķ���
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        /// <summary>
        /// Returns a random number within a specified range.
        /// ����һ��ָ����Χ�������
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned. / ���ص���������½磨�������ȡ���½�ֵ����</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue. / ���ص���������Ͻ磨���������ȡ���Ͻ�ֵ���� maxValue ������ڻ���� minValue</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue; 
        /// that is, the range of return values includes minValue but not maxValue.If minValue equals maxValue, minValue is returned. 
        ///  һ�����ڵ��� minValue ��С�� maxValue �� 32 λ�������������������ص�ֵ��Χ���� minValue �������� maxValue��
        ///  ��� minValue ���� maxValue���򷵻� minValue��
        /// </returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// ����һ��С����ָ�����ֵ�ķǸ������
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.  / TҪ���ɵ�����������ޣ����������ȡ������ֵ���� maxValue ������ڻ������</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than maxValue; 
        /// that is, the range of return values ordinarily includes zero but not maxValue. However, if maxValue equals zero, maxValue is returned.
        /// ���ڵ�������С�� maxValue �� 32 λ��������������������ֵ�ķ�Χͨ�������㵫������ maxValue�� ��������� maxValue.�����㣬�򷵻� maxValue��
        /// </returns>
        public static int GetRandom(int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(maxValue);
            }
        }

        /// <summary>
        /// Returns a nonnegative random number.
        /// ����һ���Ǹ������
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="int.MaxValue"/>. / ���ڵ�������С�� System.Int32.MaxValue �� 32 λ����������.</returns>
        public static int GetRandom()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }

        /// <summary>
        /// Gets random of given objects.
        /// ��ȡ�����Ķ��󼯺��е��������
        /// </summary>
        /// <typeparam name="T">Type of the objects / ���󼯺��е�Ԫ������</typeparam>
        /// <param name="objs">List of object to select a random one / �����в��������ļ���</param>
        public static T GetRandomOf<T>(params T[] objs)
        {
            if (objs.IsNullOrEmpty())
            {
                throw new ArgumentException("objs can not be null or empty!", "objs");
            }

            return objs[GetRandom(0, objs.Length)];
        }

        /// <summary>
        /// Generates a randomized list from given enumerable.
        /// �Ӹ����Ŀ�ö������һ��������б�
        /// </summary>
        /// <typeparam name="T">Type of items in the list / �б�������</typeparam>
        /// <param name="items">items / �б���</param>
        public static List<T> GenerateRandomizedList<T>(IEnumerable<T> items)
        {
            var currentList = new List<T>(items);
            var randomList = new List<T>();

            while (currentList.Any())
            {
                var randomIndex = RandomHelper.GetRandom(0, currentList.Count);
                randomList.Add(currentList[randomIndex]);
                currentList.RemoveAt(randomIndex);
            }

            return randomList;
        }
    }
}
