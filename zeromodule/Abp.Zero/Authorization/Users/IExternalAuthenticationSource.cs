using System.Threading.Tasks;
using Abp.MultiTenancy;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Defines an authorization source to be used by <see cref="AbpUserManager{TRole,TUser}.LoginAsync"/> method.
    /// ����һ����ȨԴ��<see cref="AbpUserManager{TRole,TUser}.LoginAsync"/>����ʹ��
    /// </summary>
    /// <typeparam name="TTenant">Tenant type</typeparam>
    /// <typeparam name="TUser">User type</typeparam>
    public interface IExternalAuthenticationSource<TTenant, TUser>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// Unique name of the authentication source.This source name is set to <see cref="AbpUser{TTenant,TUser}.AuthenticationSource"/>if the user authenticated by this authentication source
        /// ��֤Դ��Ψһ���ơ�Դ����ͨ��<see cref="AbpUser{TTenant,TUser}.AuthenticationSource"/>���ã�����û�ͨ�������ȨԴ��Ȩ��
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Used to try authenticate a user by this source.
        /// ���ڳ���ͨ����Դ��֤�û�
        /// </summary>
        /// <param name="userNameOrEmailAddress">User name or email address / �û����������ַ</param>
        /// <param name="plainPassword">Plain password of the user / �û�����ͨ����</param>
        /// <param name="tenant">Tenant of the user or null (if user is a host user) / �û����⻧��NULL������û��������û���</param>
        /// <returns>True, indicates that this used has authenticated by this source / true����ʾ��Ӧ�ó������ɸ�Դ���������֤��</returns>
        Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, TTenant tenant);

        /// <summary>
        /// This method is a user authenticated by this source which does not exists yet.So, source should create the User and fill properties.
        /// �˷������ɸ�Դ��֤���û�����Դ��δ���ڡ���ˣ�ԴӦ�ô����û���������ԡ�
        /// </summary>
        /// <param name="userNameOrEmailAddress">User name or email address / �û����������ַ</param>
        /// <param name="tenant">Tenant of the user or null (if user is a host user) / �û����⻧��NULL������û��������û���</param>
        /// <returns>Newly created user / �´������û�</returns>
        Task<TUser> CreateUserAsync(string userNameOrEmailAddress, TTenant tenant);

        /// <summary>
        /// This method is called after an existing user is authenticated by this source.It can be used to update some properties of the user by the source.
        /// �˷�����ͨ����Դ��֤�Ѵ��ڵ��û�֮�󱻵��ã�ͨ����Դ�����޸��û���һЩ����
        /// </summary>
        /// <param name="user">The user that can be updated / �ܱ��޸ĵ��û�</param>
        /// <param name="tenant">Tenant of the user or null (if user is a host user) / �û����⻧��NULL������û��������û���</param>
        Task UpdateUserAsync(TUser user, TTenant tenant);
    }
}