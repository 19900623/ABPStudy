using System.Data.Common;
using System.Data.Entity;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.BackgroundJobs;
using Abp.EntityFramework.Extensions;
using Abp.MultiTenancy;
using Abp.Notifications;

namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// Base DbContext for ABP zero.Derive your DbContext from this class to have base entities.
    /// ABP Zero�����ݿ������Ļ��࣬�Ӹ��������Լ������ݿ�������
    /// </summary>
    public abstract class AbpZeroDbContext<TTenant, TRole, TUser> : AbpZeroCommonDbContext<TRole, TUser>
        where TTenant : AbpTenant<TUser>
        where TRole : AbpRole<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// �̻�
        /// </summary>
        public virtual IDbSet<TTenant> Tenants { get; set; }

        /// <summary>
        /// �汾.
        /// </summary>
        public virtual IDbSet<Edition> Editions { get; set; }

        /// <summary>
        /// ��������.
        /// </summary>
        public virtual IDbSet<FeatureSetting> FeatureSettings { get; set; }

        /// <summary>
        /// �̻���������.
        /// </summary>
        public virtual IDbSet<TenantFeatureSetting> TenantFeatureSettings { get; set; }

        /// <summary>
        /// �汾��������.
        /// </summary>
        public virtual IDbSet<EditionFeatureSetting> EditionFeatureSettings { get; set; }

        /// <summary>
        /// ��̨��ҵ.
        /// </summary>
        public virtual IDbSet<BackgroundJobInfo> BackgroundJobs { get; set; }

        /// <summary>
        /// �û�
        /// </summary>
        public virtual IDbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// Default constructor.Do not directly instantiate this class. Instead, use dependency injection!
        /// Ĭ�Ϲ��캯������Ҫֱ��ʵ��������࣬�෴��ʹ������ע��
        /// </summary>
        protected AbpZeroDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// �����ַ��������Ĺ��캯��
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file / �����ַ������������ļ��е������ַ���Name</param>
        protected AbpZeroDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// ������캯���ɱ����ڵ�Ԫ����
        /// </summary>
        protected AbpZeroDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region BackgroundJobInfo.IX_IsAbandoned_NextTryTime

            modelBuilder.Entity<BackgroundJobInfo>()
                .Property(j => j.IsAbandoned)
                .CreateIndex("IX_IsAbandoned_NextTryTime", 1);

            modelBuilder.Entity<BackgroundJobInfo>()
                .Property(j => j.NextTryTime)
                .CreateIndex("IX_IsAbandoned_NextTryTime", 2);

            #endregion

            #region NotificationSubscriptionInfo.IX_NotificationName_EntityTypeName_EntityId_UserId

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.NotificationName)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 1);

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.EntityTypeName)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 2);

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.EntityId)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 3);

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.UserId)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 4);

            #endregion

            #region UserNotificationInfo.IX_UserId_State_CreationTime

            modelBuilder.Entity<UserNotificationInfo>()
                .Property(un => un.UserId)
                .CreateIndex("IX_UserId_State_CreationTime", 1);

            modelBuilder.Entity<UserNotificationInfo>()
                .Property(un => un.State)
                .CreateIndex("IX_UserId_State_CreationTime", 2);

            modelBuilder.Entity<UserNotificationInfo>()
                .Property(un => un.CreationTime)
                .CreateIndex("IX_UserId_State_CreationTime", 3);

            #endregion

            #region UserLoginAttempt.IX_TenancyName_UserNameOrEmailAddress_Result

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.TenancyName)
                .CreateIndex("IX_TenancyName_UserNameOrEmailAddress_Result", 1);

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.UserNameOrEmailAddress)
                .CreateIndex("IX_TenancyName_UserNameOrEmailAddress_Result", 2);

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.Result)
                .CreateIndex("IX_TenancyName_UserNameOrEmailAddress_Result", 3);

            #endregion

            #region UserLoginAttempt.IX_UserId_TenantId

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.UserId)
                .CreateIndex("IX_UserId_TenantId", 1);

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.TenantId)
                .CreateIndex("IX_UserId_TenantId", 2);

            #endregion
        }
    }
}
