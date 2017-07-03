using System;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// �̻�������
    /// </summary>
    [Serializable]
    public class TenantCacheItem
    {
        /// <summary>
        /// ��������
        /// </summary>
        public const string CacheName = "AbpZeroTenantCache";
        /// <summary>
        /// �̻�������������
        /// </summary>
        public const string ByNameCacheName = "AbpZeroTenantByNameCache";
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// �̻�����
        /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// �����ַ���
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// �汾
        /// </summary>
        public int? EditionId { get; set; }
        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// �Զ�������
        /// </summary>
        public object CustomData { get; set; }
    }
}