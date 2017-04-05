using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Linq
{
    /// <summary>
    /// This interface is intended to be used by ABP.
    /// �˽ӿڵ�Ŀ����Ҫʹ��ABP(�첽��ѯִ����)
    /// </summary>
    public interface IAsyncQueryableExecuter
    {
        /// <summary>
        /// ��ȡ���� - �첽
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="queryable">���͵Ĳ�ѯ���ݼ�</param>
        /// <returns>��ѯ������</returns>
        Task<int> CountAsync<T>(IQueryable<T> queryable);

        /// <summary>
        /// ת����List - �첽
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="queryable">���͵Ĳ�ѯ���ݼ�</param>
        /// <returns></returns>
        Task<List<T>> ToListAsync<T>(IQueryable<T> queryable);

        /// <summary>
        /// ��ȡĬ�ϻ��һ��
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="queryable">���͵Ĳ�ѯ���ݼ�</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable);
    }
}