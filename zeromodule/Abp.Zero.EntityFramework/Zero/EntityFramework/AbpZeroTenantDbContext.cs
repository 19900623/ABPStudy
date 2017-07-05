using System.Data.Common;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;

namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// ABP Zero�̻����ݿ�������
    /// </summary>
    /// <typeparam name="TRole">��ɫ����</typeparam>
    /// <typeparam name="TUser">�û�����</typeparam>
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class AbpZeroTenantDbContext<TRole, TUser> : AbpZeroCommonDbContext<TRole, TUser>
        where TRole : AbpRole<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// Default constructor.Do not directly instantiate this class. Instead, use dependency injection!
        /// Ĭ�Ϲ��캯������Ҫֱ��ʵ��������࣬�෴��ʹ������ע��
        /// </summary>
        protected AbpZeroTenantDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// �����ַ��������Ĺ��캯��
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file / �����ַ������������ļ��е������ַ���Name</param>
        protected AbpZeroTenantDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// ������캯���ɱ����ڵ�Ԫ����
        /// </summary>
        protected AbpZeroTenantDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }
}