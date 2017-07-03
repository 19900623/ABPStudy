using Abp.Runtime.Caching;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// �̻�����������չ
    /// </summary>
    public static class TenantCacheManagerExtensions
    {
        /// <summary>
        /// ��ȡ�̻�����
        /// </summary>
        /// <param name="cacheManager">����������</param>
        /// <returns></returns>
        public static ITypedCache<int, TenantCacheItem> GetTenantCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<int, TenantCacheItem>(TenantCacheItem.CacheName);
        }
        /// <summary>
        /// ͨ�����ƻ����ȡ�̻�
        /// </summary>
        /// <param name="cacheManager">����������</param>
        /// <returns></returns>
        public static ITypedCache<string, int?> GetTenantByNameCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, int?>(TenantCacheItem.ByNameCacheName);
        }
    }
}