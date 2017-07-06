using System.Data.Common;
using System.Data.Entity;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.EntityFramework;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;

namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// ABP Zero Common���ݿ�������
    /// </summary>
    /// <typeparam name="TRole">��ɫ����</typeparam>
    /// <typeparam name="TUser">�û�����</typeparam>
    public abstract class AbpZeroCommonDbContext<TRole, TUser> : AbpDbContext
        where TRole : AbpRole<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// ��ɫ.
        /// </summary>
        public virtual IDbSet<TRole> Roles { get; set; }

        /// <summary>
        /// �û�.
        /// </summary>
        public virtual IDbSet<TUser> Users { get; set; }

        /// <summary>
        /// �û���¼.
        /// </summary>
        public virtual IDbSet<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// �û���¼����.
        /// </summary>
        public virtual IDbSet<UserLoginAttempt> UserLoginAttempts { get; set; }

        /// <summary>
        /// �û���ɫ.
        /// </summary>
        public virtual IDbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// �û�����.
        /// </summary>
        public virtual IDbSet<UserClaim> UserClaims { get; set; }

        /// <summary>
        /// Ȩ������.
        /// </summary>
        public virtual IDbSet<PermissionSetting> Permissions { get; set; }

        /// <summary>
        /// Role permissions.
        /// </summary>
        public virtual IDbSet<RolePermissionSetting> RolePermissions { get; set; }

        /// <summary>
        /// ��ɫȨ��.
        /// </summary>
        public virtual IDbSet<UserPermissionSetting> UserPermissions { get; set; }

        /// <summary>
        /// ����.
        /// </summary>
        public virtual IDbSet<Setting> Settings { get; set; }

        /// <summary>
        /// �����־.
        /// </summary>
        public virtual IDbSet<AuditLog> AuditLogs { get; set; }

        /// <summary>
        /// Ӧ�ó�������.
        /// </summary>
        public virtual IDbSet<ApplicationLanguage> Languages { get; set; }

        /// <summary>
        /// Ӧ�ó��������ı�.
        /// </summary>
        public virtual IDbSet<ApplicationLanguageText> LanguageTexts { get; set; }

        /// <summary>
        /// ��֯��Ԫ.
        /// </summary>
        public virtual IDbSet<OrganizationUnit> OrganizationUnits { get; set; }

        /// <summary>
        /// �û���֯��Ԫ.
        /// </summary>
        public virtual IDbSet<UserOrganizationUnit> UserOrganizationUnits { get; set; }

        /// <summary>
        /// ֪ͨ.
        /// </summary>
        public virtual IDbSet<NotificationInfo> Notifications { get; set; }

        /// <summary>
        /// �̻�֪ͨ.
        /// </summary>
        public virtual IDbSet<TenantNotificationInfo> TenantNotifications { get; set; }

        /// <summary>
        /// �û�֪ͨ.
        /// </summary>
        public virtual IDbSet<UserNotificationInfo> UserNotifications { get; set; }

        /// <summary>
        /// ����֪ͨ.
        /// </summary>
        public virtual IDbSet<NotificationSubscriptionInfo> NotificationSubscriptions { get; set; }

        /// <summary>
        /// Default constructor.Do not directly instantiate this class. Instead, use dependency injection!
        /// Ĭ�Ϲ��캯������Ҫֱ��ʵ��������࣬�෴��ʹ������ע��
        /// </summary>
        protected AbpZeroCommonDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// �����ַ��������Ĺ��캯��
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file / �����ַ������������ļ��е������ַ���Name</param>
        protected AbpZeroCommonDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// ������캯���ɱ����ڵ�Ԫ����
        /// </summary>
        protected AbpZeroCommonDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }
}