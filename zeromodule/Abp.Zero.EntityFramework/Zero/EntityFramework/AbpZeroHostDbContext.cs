using System.Data.Common;
using System.Data.Entity;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.BackgroundJobs;
using Abp.MultiTenancy;

namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// ABP Zero�������ݿ�������
    /// </summary>
    /// <typeparam name="TTenant">�̻�����</typeparam>
    /// <typeparam name="TRole">��ɫ����</typeparam>
    /// <typeparam name="TUser">�û�����</typeparam>
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class AbpZeroHostDbContext<TTenant, TRole, TUser> : AbpZeroCommonDbContext<TRole, TUser>
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
        /// ��̨��ҵ
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
        protected AbpZeroHostDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// �����ַ��������Ĺ��캯��
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file / �����ַ������������ļ��е������ַ���Name</param>
        protected AbpZeroHostDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// ������캯���ɱ����ڵ�Ԫ����
        /// </summary>
        protected AbpZeroHostDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }
}