using System.Collections.Generic;
using Abp.Runtime.Caching;

namespace Abp.Localization
{
    /// <summary>
    /// A helper to implement localization cache.
    /// ʵ�ֱ��ػ�����İ�����
    /// </summary>
    public static class MultiTenantLocalizationDictionaryCacheHelper
    {
        /// <summary>
        /// ����Key����.
        /// </summary>
        public const string CacheName = "AbpZeroMultiTenantLocalizationDictionaryCache";
        /// <summary>
        /// ��ȡ���̻����ػ������ֵ�
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <returns></returns>
        public static ITypedCache<string, Dictionary<string, string>> GetMultiTenantLocalizationDictionaryCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache(CacheName).AsTyped<string, Dictionary<string, string>>();
        }
        /// <summary>
        /// ���㻺��Key
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="sourceName">Դ����</param>
        /// <param name="languageName">��������</param>
        /// <returns></returns>
        public static string CalculateCacheKey(int? tenantId, string sourceName, string languageName)
        {
            return sourceName + "#" + languageName + "#" + (tenantId ?? 0);
        }
    }
}