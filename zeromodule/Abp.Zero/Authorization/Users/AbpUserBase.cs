using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Base class for user.
    /// �û�����
    /// </summary>
    [Table("AbpUsers")]
    public abstract class AbpUserBase : FullAuditedEntity<long>, IUser<long>, IMayHaveTenant
    {
        /// <summary>
        /// <see cref="UserName"/>������󳤶�
        /// </summary>
        public const int MaxUserNameLength = 32;

        /// <summary>
        /// <see cref="EmailAddress"/>������󳤶�
        /// </summary>
        public const int MaxEmailAddressLength = 256;

        /// <summary>
        /// User name.User name must be unique for it's tenant.
        /// �û�������ͬһ���̻����û�������Ψһ
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Tenant Id of this user.
        /// ���û����̻�ID
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Email address of the user.Email address must be unique for it's tenant.
        /// �û��������ַ��ͬһ���̻����û��ʼ���ַ����Ψһ
        /// </summary>
        [Required]
        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// The last time this user entered to the system.
        /// ��ǰ�û����һ�ε���ϵͳ��ʱ��
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// Creates <see cref="UserIdentifier"/> from this User.
        /// ���캯��
        /// </summary>
        /// <returns></returns>
        public virtual UserIdentifier ToUserIdentifier()
        {
            return new UserIdentifier(TenantId, Id);
        }
    }
}