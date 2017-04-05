using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Linq
{
    /// <summary>
    /// NULL�����첽��ѯִ����
    /// </summary>
    public class NullAsyncQueryableExecuter : IAsyncQueryableExecuter
    {
        /// <summary>
        /// <see cref="NullAsyncQueryableExecuter"/> ʵ��
        /// </summary>
        public static NullAsyncQueryableExecuter Instance { get; } = new NullAsyncQueryableExecuter();

        /// <summary>
        /// ��ȡ���� - �첽
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="queryable">���͵Ĳ�ѯ���ݼ�</param>
        /// <returns>��ѯ������</returns>
        public Task<int> CountAsync<T>(IQueryable<T> queryable)
        {
            return Task.FromResult(queryable.Count());
        }

        /// <summary>
        /// ת����List - �첽
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="queryable">���͵Ĳ�ѯ���ݼ�</param>
        /// <returns></returns>
        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable)
        {
            return Task.FromResult(queryable.ToList());
        }

        /// <summary>
        /// ��ȡĬ�ϻ��һ��
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="queryable">���͵Ĳ�ѯ���ݼ�</param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable)
        {
            return Task.FromResult(queryable.FirstOrDefault());
        }
    }
}