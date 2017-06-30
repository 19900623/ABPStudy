using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Represents a summary user
    /// ����һ��ժҪ�û�
    /// </summary>
    [Table("AbpUserAccounts")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class UserAccount : FullAuditedEntity<long>
    {
        /// <summary>
        /// �̻�ID
        /// </summary>
        public virtual int? TenantId { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public virtual long UserId { get; set; }
        /// <summary>
        /// �û�Link Id
        /// </summary>
        public virtual long? UserLinkId { get; set; }
        /// <summary>
        /// �û���
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// �����ַ
        /// </summary>
        public virtual string EmailAddress { get; set; }
        /// <summary>
        /// ����¼ʱ��
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }
    }
}