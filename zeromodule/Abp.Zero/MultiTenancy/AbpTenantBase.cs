using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Security;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Base class for tenants.
    /// �̻��Ļ���
    /// </summary>
    [Table("AbpTenants")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class AbpTenantBase : FullAuditedEntity<int>
    {
        /// <summary>
        /// <see cref="TenancyName"/>���Ե���󳤶�
        /// </summary>
        public const int MaxTenancyNameLength = 64;

        /// <summary>
        /// <see cref="ConnectionString"/>���Ե���󳤶�
        /// </summary>
        public const int MaxConnectionStringLength = 1024;

        /// <summary>
        /// Tenancy name. This property is the UNIQUE name of this Tenant.It can be used as subdomain name in a web application.
        /// �̻��������������̻�����Ψһ�ģ���������webӦ�ó���������������
        /// </summary>
        [Required]
        [StringLength(MaxTenancyNameLength)]
        public virtual string TenancyName { get; set; }

        /// <summary>
        /// ENCRYPTED connection string of the tenant database.Can be null if this tenant is stored in host database.Use <see cref="SimpleStringCipher"/> to encrypt/decrypt this.
        /// �̻��ļ��������ַ���������̻��洢���������ݿ��������Ϊnull��ʹ��<see cref="SimpleStringCipher"/>����/����
        /// </summary>
        [StringLength(MaxConnectionStringLength)]
        public virtual string ConnectionString { get; set; }
    }
}