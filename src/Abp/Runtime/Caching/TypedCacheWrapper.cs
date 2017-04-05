using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Implements <see cref="ITypedCache{TKey,TValue}"/> to wrap a <see cref="ICache"/>.
    /// <see cref="ITypedCache{TKey,TValue}"/>��ʵ��ȥ��װһ��<see cref="ICache"/>
    /// </summary>
    /// <typeparam name="TKey">����Key�Ķ���</typeparam>
    /// <typeparam name="TValue">����ֵ�Ķ���</typeparam>
    public class TypedCacheWrapper<TKey, TValue> : ITypedCache<TKey, TValue>
    {
        /// <summary>
        /// �����Ψһ����
        /// </summary>
        public string Name
        {
            get { return InternalCache.Name; }
        }

        /// <summary>
        /// �������Ĭ�ϻ�������ʱ��
        /// </summary>
        public TimeSpan DefaultSlidingExpireTime
        {
            get { return InternalCache.DefaultSlidingExpireTime; }
            set { InternalCache.DefaultSlidingExpireTime = value; }
        }

        /// <summary>
        /// ��ȡ�ڲ�����
        /// </summary>
        public ICache InternalCache { get; private set; }

        /// <summary>
        /// Creates a new <see cref="TypedCacheWrapper{TKey,TValue}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="internalCache">The actual internal cache / ʵ�ʵ��ڲ�����</param>
        public TypedCacheWrapper(ICache internalCache)
        {
            InternalCache = internalCache;
        }

        /// <summary>
        /// �ͷŻ���
        /// </summary>
        public void Dispose()
        {
            InternalCache.Dispose();
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Clear()
        {
            InternalCache.Clear();
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public Task ClearAsync()
        {
            return InternalCache.ClearAsync();
        }

        /// <summary>
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="key">�����</param>
        /// <param name="factory">���������ʹ�ù�����������������</param>
        /// <returns>������</returns>
        public TValue Get(TKey key, Func<TKey, TValue> factory)
        {
            return InternalCache.Get(key, factory);
        }

        /// <summary>
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="key">�����</param>
        /// <param name="factory">���������ʹ�ù�����������������</param>
        /// <returns>������</returns>
        public Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory)
        {
            return InternalCache.GetAsync(key, factory);
        }

        /// <summary>
        /// �ӻ����ȡ���ݣ����û���ҵ�����null
        /// </summary>
        /// <param name="key">�����</param>
        /// <returns>������Ŀ / û���ҵ�����null</returns>
        public TValue GetOrDefault(TKey key)
        {
            return InternalCache.GetOrDefault<TKey, TValue>(key);
        }

        /// <summary>
        /// �ӻ����ȡ���ݣ����û���ҵ�����null
        /// </summary>
        /// <param name="key">�����</param>
        /// <returns>������Ŀ / û���ҵ�����null</returns>
        public Task<TValue> GetOrDefaultAsync(TKey key)
        {
            return InternalCache.GetOrDefaultAsync<TKey, TValue>(key);
        }

        /// <summary>
        /// ͨ��������/���ǻ����е���
        /// </summary>
        /// <param name="key">�����</param>
        /// <param name="value">���������</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        public void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            InternalCache.Set(key.ToString(), value, slidingExpireTime);
        }

        /// <summary>
        /// ͨ��������/���ǻ����е���
        /// </summary>
        /// <param name="key">�����</param>
        /// <param name="value">���������</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        public Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            return InternalCache.SetAsync(key.ToString(), value, slidingExpireTime);
        }

        /// <summary>
        /// �Ƴ�ָ�����Ļ���(���ָ����key�����ڻ����У���ɶ��Ҳ����)
        /// </summary>
        /// <param name="key">�����</param>
        public void Remove(TKey key)
        {
            InternalCache.Remove(key.ToString());
        }

        /// <summary>
        /// �Ƴ�ָ�����Ļ���
        /// </summary>
        /// <param name="key">�����</param>
        public Task RemoveAsync(TKey key)
        {
            return InternalCache.RemoveAsync(key.ToString());
        }
    }
}