using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Base class for caches.It's used to simplify implementing <see cref="ICache"/>.
    /// ������࣬������<see cref="ICache"/>�򵥵�ʵ��
    /// </summary>
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// �����Ψһ����
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Ĭ�ϻ�������ʱ��
        /// </summary>
        public TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Ĭ�Ͼ��Թ���ʱ��
        /// </summary>
        public TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// <summary>
        /// ͬ������
        /// </summary>
        protected readonly object SyncObj = new object();

        private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="name">�����Ψһ����</param>
        protected CacheBase(string name)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }

        /// <summary>
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="key">�����</param>
        /// <param name="factory">���������ʹ�ù�����������������</param>
        /// <returns>������</returns>
        public virtual object Get(string key, Func<string, object> factory)
        {
            var cacheKey = key;
            var item = GetOrDefault(key);
            if (item == null)
            {
                lock (SyncObj)
                {
                    item = GetOrDefault(key);
                    if (item == null)
                    {
                        item = factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        Set(cacheKey, item);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// �ӻ����л�ȡ����
        /// </summary>
        /// <param name="key">�����</param>
        /// <param name="factory">���������ʹ�ù�����������������</param>
        /// <returns></returns>
        public virtual async Task<object> GetAsync(string key, Func<string, Task<object>> factory)
        {
            var cacheKey = key;
            var item = await GetOrDefaultAsync(key);
            if (item == null)
            {
                using (await _asyncLock.LockAsync())
                {
                    item = await GetOrDefaultAsync(key);
                    if (item == null)
                    {
                        item = await factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        await SetAsync(cacheKey, item);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// ����������д���ӻ����ȡ���ݣ����û���ҵ�����null
        /// </summary>
        /// <param name="key">�����</param>
        /// <returns></returns>
        public abstract object GetOrDefault(string key);

        /// <summary>
        /// ����������д���ӻ����ȡ���ݣ����û���ҵ�����null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }

        /// <summary>
        /// ͨ��������/���ǻ����е���
        /// /// ��һ������ʱ�����(<paramref name="slidingExpireTime"/> �� <paramref name="absoluteExpireTime"/>).
        /// ���û��ָ��,��<see cref="DefaultAbsoluteExpireTime"/>����ʹ�ã������Ϊnull��Ȼ����<see cref="DefaultSlidingExpireTime"/>����ʹ��
        /// </summary>
        /// <param name="key">����key</param>
        /// <param name="value">���������</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Թ���ʱ��</param>
        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// ͨ��������/���ǻ����е���
        /// ��һ������ʱ�����(<paramref name="slidingExpireTime"/> �� <paramref name="absoluteExpireTime"/>).
        /// ���û��ָ��,��<see cref="DefaultAbsoluteExpireTime"/>����ʹ�ã������Ϊnull��Ȼ����<see cref="DefaultSlidingExpireTime"/>����ʹ��
        /// </summary>
        /// <param name="key">����key</param>
        /// <param name="value">���������</param>
        /// <param name="slidingExpireTime">��������ʱ��</param>
        /// <param name="absoluteExpireTime">���Թ���ʱ��</param>
        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            Set(key, value, slidingExpireTime);
            return Task.FromResult(0);
        }

        /// <summary>
        /// �Ƴ�ָ�����Ļ���
        /// </summary>
        /// <param name="key">����key</param>
        public abstract void Remove(string key);

        /// <summary>
        /// �Ƴ�ָ�����Ļ���(���ָ����key�����ڻ����У���ɶ��Ҳ����)
        /// </summary>
        /// <param name="key">����key</param>
        /// <returns></returns>
        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        /// <summary>
        /// ������еĻ�������
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// ������еĻ������� - �첽
        /// </summary>
        /// <returns></returns>
        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        /// <summary>
        /// �ͷ�
        /// </summary>
        public virtual void Dispose()
        {

        }
    }
}