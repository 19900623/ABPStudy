using System;
using System.Collections.Generic;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Used to store features of a Tenant in the cache.
    /// �����ڻ����д洢�̻��Ĺ���
    /// </summary>
    [Serializable]
    public class TenantFeatureCacheItem
    {
        /// <summary>
        /// The cache store name.
        /// ����洢������
        /// </summary>
        public const string CacheStoreName = "AbpZeroTenantFeatures";

        /// <summary>
        /// Edition of the tenant.
        /// �̻��İ汾
        /// </summary>
        public int? EditionId { get; set; }

        /// <summary>
        /// Feature values.
        /// ����ֵ�ֵ�
        /// </summary>
        public IDictionary<string, string> FeatureValues { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public TenantFeatureCacheItem()
        {
            FeatureValues = new Dictionary<string, string>();
        }
    }
}