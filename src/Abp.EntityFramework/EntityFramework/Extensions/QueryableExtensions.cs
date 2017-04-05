using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Abp.EntityFramework.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IQueryable"/> and <see cref="IQueryable{T}"/>.
    /// <see cref="IQueryable"/> �� <see cref="IQueryable{T}"/>����չ����
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Specifies the related objects to include in the query results.
        /// ָ��Ҫ�����ڲ�ѯ����е���ض���
        /// </summary>
        /// <param name="source">The source <see cref="IQueryable"/> on which to call Include. / Ҫ���õĲ�ѯԴ<see cref="IQueryable"/></param>
        /// <param name="condition">A boolean value to determine to include <paramref name="path"/> or not. / �Ƿ����<paramref name="path"/></param>
        /// <param name="path">The dot-separated list of related objects to return in the query results. / �ڲ�ѯ����з�����ض���ĵ�ָ��б�</param>
        public static IQueryable IncludeIf(this IQueryable source, bool condition, string path)
        {
            return condition
                ? source.Include(path)
                : source;
        }

        /// <summary>
        /// Specifies the related objects to include in the query results.
        /// ָ��Ҫ�����ڲ�ѯ����е���ض���
        /// </summary>
        /// <param name="source">The source <see cref="IQueryable{T}"/> on which to call Include./ Ҫ���õĲ�ѯԴ</param>
        /// <param name="condition">A boolean value to determine to include <paramref name="path"/> or not. / �Ƿ����<paramref name="path"/></param>
        /// <param name="path">The dot-separated list of related objects to return in the query results. / �ڲ�ѯ����з�����ض���ĵ�ָ��б�</param>
        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, string path)
        {
            return condition
                ? source.Include(path)
                : source;
        }

        /// <summary>
        /// Specifies the related objects to include in the query results.
        /// ָ��Ҫ�����ڲ�ѯ����е���ض���
        /// </summary>
        /// <param name="source">The source <see cref="IQueryable{T}"/> on which to call Include. / Ҫ���õĲ�ѯԴ</param>
        /// <param name="condition">A boolean value to determine to include <paramref name="path"/> or not. / �Ƿ����<paramref name="path"/></param>
        /// <param name="path">The type of navigation property being included. / �����������Ե�����</param>
        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path)
        {
            return condition
                ? source.Include(path)
                : source;
        }
    }
}