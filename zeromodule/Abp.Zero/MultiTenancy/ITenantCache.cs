namespace Abp.MultiTenancy
{
    /// <summary>
    /// �̻�����
    /// </summary>
    public interface ITenantCache
    {
        /// <summary>
        /// ��ȡ�̻�����
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        TenantCacheItem Get(int tenantId);
        /// <summary>
        /// ��ȡ�̻�����
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        TenantCacheItem Get(string tenancyName);
        /// <summary>
        /// ��ȡ�̻������Null
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        TenantCacheItem GetOrNull(string tenancyName);
        /// <summary>
        /// ��ȡ�̻������Null
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        TenantCacheItem GetOrNull(int tenantId);
    }
}