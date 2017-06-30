using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Extensions;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// ��ʾһ���û�
    /// </summary>
    public abstract class AbpUser<TUser> : AbpUserBase, IFullAudited<TUser>, IPassivable
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// ����Ա���û���������Ա���ܱ�ɾ�����ҹ���Ա���û������ܱ��޸ġ�
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// <see cref="Name"/>������󳤶�
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// <see cref="Surname"/>������󳤶�
        /// </summary>
        public const int MaxSurnameLength = 32;

        /// <summary>
        /// <see cref="Password"/>������󳤶�
        /// </summary>
        public const int MaxPasswordLength = 128;

        /// <summary>
        /// <see cref="Password"/>������󳤶�
        /// </summary>
        public const int MaxPlainPasswordLength = 32;

        /// <summary>
        /// <see cref="EmailConfirmationCode"/>������󳤶�
        /// </summary>
        public const int MaxEmailConfirmationCodeLength = 328;

        /// <summary>
        /// <see cref="PasswordResetCode"/>������󳤶�
        /// </summary>
        public const int MaxPasswordResetCodeLength = 328;

        /// <summary>
        /// <see cref="AuthenticationSource"/>������󳤶�
        /// </summary>
        public const int MaxAuthenticationSourceLength = 64;

        /// <summary>
        /// Authorization source name.It's set to external authentication source name if created by an external source.Default: null.
        /// ��ȨԴ���ơ�������ⲿԴ��������������Ϊ�ⲿ�����֤Դ���ơ�Ĭ��ֵ��null
        /// </summary>
        [MaxLength(MaxAuthenticationSourceLength)]
        public virtual string AuthenticationSource { get; set; }

        /// <summary>
        /// Name of the user.
        /// �û�������
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Surname of the user.
        /// �û�����
        /// </summary>
        [Required]
        [StringLength(MaxSurnameLength)]
        public virtual string Surname { get; set; }

        /// <summary>
        /// Return full name (Name Surname )
        /// ȫ��
        /// </summary>
        [NotMapped]
        public virtual string FullName { get { return this.Name + " " + this.Surname; } }

        /// <summary>
        /// Password of the user.
        /// �û�������
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        /// Is the <see cref="AbpUserBase.EmailAddress"/> confirmed.
        /// �û���<see cref="AbpUserBase.EmailAddress"/>�Ƿ�ȷ��
        /// </summary>
        public virtual bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Confirmation code for email.
        /// �����ʼ�ȷ����
        /// </summary>
        [StringLength(MaxEmailConfirmationCodeLength)]
        public virtual string EmailConfirmationCode { get; set; }

        /// <summary>
        /// Reset code for password.It's not valid if it's null.It's for one usage and must be set to null after reset.
        /// ���������Code���������null����Ч����ֻ����һ�Σ����ú��������Ϊnull
        /// </summary>
        [StringLength(MaxPasswordResetCodeLength)]
        public virtual string PasswordResetCode { get; set; }

        /// <summary>
        /// Lockout end date.
        /// �������������
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the access failed count.
        /// ����ʧ�ܵĴ���
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the lockout enabled.
        /// �����Ƿ���
        /// </summary>
        public virtual bool IsLockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// �绰��
        /// </summary>
        public virtual string PhoneNumber {get; set; }

        /// <summary>
        /// Is the <see cref="PhoneNumber"/> confirmed.
        /// <see cref="PhoneNumber"/>�Ƿ�ȷ��
        /// </summary>
        public virtual bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the security stamp.
        /// ��ȫ���
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Is two factor auth enabled.
        /// �Ƿ�����˫���������֤
        /// </summary>
        public virtual bool IsTwoFactorEnabled { get; set; }

        /// <summary>
        /// Is this user active?If as user is not active, he/she can not use the application.
        /// �Ƿ��ǻ�û���������ǻ״̬����������ʹ��Ӧ�ó���
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Login definitions for this user.
        /// ���û��ĵ�¼����
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserLogin> Logins { get; set; }

        /// <summary>
        /// Roles of this user.
        /// ���û��Ľ�ɫ�б�
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserRole> Roles { get; set; }

        /// <summary>
        /// Claims of this user.
        /// ���û���Claims
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserClaim> Claims { get; set; }

        /// <summary>
        /// Permission definitions for this user.
        /// ���û���Ȩ�޶���
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<UserPermissionSetting> Permissions { get; set; }

        /// <summary>
        /// Settings for this user.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<Setting> Settings { get; set; }

        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        protected AbpUser()
        {
            IsActive = true;
            SecurityStamp = SequentialGuidGenerator.Instance.Create().ToString();
        }

        public virtual void SetNewPasswordResetCode()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(MaxPasswordResetCodeLength);
        }

        public virtual void SetNewEmailConfirmationCode()
        {
            EmailConfirmationCode = Guid.NewGuid().ToString("N").Truncate(MaxEmailConfirmationCodeLength);
        }

        public override string ToString()
        {
            return string.Format("[User {0}] {1}", Id, UserName);
        }
    }
}