using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Defines a cache that can be store and get items by keys.
    /// �������ͨ�����洢�ͻ�ȡ��Ļ���
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// �����Ψһ����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.Default value: 60 minutes (1 hour).Can be changed by configuration.
        /// �������Ĭ�ϻ�������ʱ�䡣Ĭ��ֵ:60����(1��Сʱ)������ͨ�������޸�
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Default absolute expire time of cache items.Default value: null (not used).
        /// ������Ŀ��Ĭ�Ͼ��Թ���ʱ�䡣Ĭ��ֵ:null(û�б�ʹ��)
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// <summary>
        /// Gets an item from the cache.
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="key">Key / Key</param>
        /// <param name="factory">Factory method to create cache item if not exists / ���������ʹ�ù�����������������</param>
        /// <returns>Cached item / ������</returns>
        object Get(string key, Func<string, object> factory);

        /// <summary>
        /// Gets an item from the cache.
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="key">Key / Key</param>
        /// <param name="factory">Factory method to create cache item if not exists / ���������ʹ�ù�����������������</param>
        /// <returns>Cached item / ������</returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// �ӻ����ȡ���ݣ����û���ҵ�����null
        /// </summary>
        /// <param name="key">Key / Key</param>
        /// <returns>Cached item or null if not found / ������Ŀ / û���ҵ�����null</returns>
        object GetOrDefault(string key);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// �ӻ����ȡ���ݣ����û���ҵ�����null
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item or null if not found / ������Ŀ / û���ҵ�����null</returns>
        Task<object> GetOrDefaultAsync(string key);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// ͨ��������/���ǻ����е���
        /// Use one of the expire times at most (<paramref name="slidingExpireTime"/> or <paramref name="absoluteExpireTime"/>).
        /// If none of them is specified, then <see cref="DefaultAbsoluteExpireTime"/> will be used if it's not null. Othewise, <see cref="DefaultSlidingExpireTime"/> will be used.
        /// ��һ������ʱ�����(<paramref name="slidingExpireTime"/> �� <paramref name="absoluteExpireTime"/>).
        /// ���û��ָ��,��<see cref="DefaultAbsoluteExpireTime"/>����ʹ�ã������Ϊnull��Ȼ����<see cref="DefaultSlidingExpireTime"/>����ʹ��
        /// </summary>
        /// <param name="key">Key / ����key</param>
        /// <param name="value">Value / ���������</param>
        /// <param name="slidingExpireTime">Sliding expire time / ��������ʱ��</param>
        /// <param name="absoluteExpireTime">Absolute expire time / ���Թ���ʱ��</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// ͨ��������/���ǻ����е���
        /// Use one of the expire times at most (<paramref name="slidingExpireTime"/> or <paramref name="absoluteExpireTime"/>).
        /// If none of them is specified, then <see cref="DefaultAbsoluteExpireTime"/> will be used if it's not null. Othewise, <see cref="DefaultSlidingExpireTime"/> will be used.
        /// ��һ������ʱ�����(<paramref name="slidingExpireTime"/> �� <paramref name="absoluteExpireTime"/>).
        /// ���û��ָ��,��<see cref="DefaultAbsoluteExpireTime"/>����ʹ�ã������Ϊnull��Ȼ����<see cref="DefaultSlidingExpireTime"/>����ʹ��
        /// </summary>
        /// <param name="key">Key / ����key</param>
        /// <param name="value">Value / ���������</param>
        /// <param name="slidingExpireTime">Sliding expire time / ��������ʱ��</param>
        /// <param name="absoluteExpireTime">Absolute expire time / ���Թ���ʱ��</param>
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key.
        /// �Ƴ�ָ�����Ļ���
        /// </summary>
        /// <param name="key">Key / ����key</param>
        void Remove(string key);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// �Ƴ�ָ�����Ļ���(���ָ����key�����ڻ����У���ɶ��Ҳ����)
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// Clears all items in this cache.
        /// ������еĻ�������
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all items in this cache.
        /// ������еĻ������� - �첽
        /// </summary>
        Task ClearAsync();
    }
}