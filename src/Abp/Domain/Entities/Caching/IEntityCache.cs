using System.Threading.Tasks;
using Abp.Runtime.Caching;

namespace Abp.Domain.Entities.Caching
{
    /// <summary>
    /// ����Ϊ <see cref="int"/> ���͵�ʵ�建��
    /// </summary>
    /// <typeparam name="TCacheItem">������</typeparam>
    public interface IEntityCache<TCacheItem> : IEntityCache<TCacheItem, int>
    {

    }

    /// <summary>
    /// ʵ�建��ӿ�
    /// </summary>
    /// <typeparam name="TCacheItem">������</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    public interface IEntityCache<TCacheItem, TPrimaryKey>
    {
        TCacheItem this[TPrimaryKey id] { get; }

        string CacheName { get; }

        ITypedCache<TPrimaryKey, TCacheItem> InternalCache { get; }

        TCacheItem Get(TPrimaryKey id);

        Task<TCacheItem> GetAsync(TPrimaryKey id);
    }
}