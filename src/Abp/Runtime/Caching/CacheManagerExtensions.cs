namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/>.
    /// <see cref="ICacheManager"/>����չ����
    /// </summary>
    public static class CacheManagerExtensions
    {
        /// <summary>
        /// ��ȡ���ͻ���
        /// </summary>
        /// <typeparam name="TKey">����Key������</typeparam>
        /// <typeparam name="TValue">����ֵ������</typeparam>
        /// <param name="cacheManager">���������</param>
        /// <param name="name">��������</param>
        /// <returns></returns>
        public static ITypedCache<TKey, TValue> GetCache<TKey, TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetCache(name).AsTyped<TKey, TValue>();
        }
    }
}