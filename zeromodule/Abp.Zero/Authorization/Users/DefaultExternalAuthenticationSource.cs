using System.Threading.Tasks;
using Abp.MultiTenancy;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// This is a helper base class to easily update <see cref="IExternalAuthenticationSource{TTenant,TUser}"/>.Implements some methods as default but you can override all methods.
    /// ���ǰ���<see cref="IExternalAuthenticationSource{TTenant,TUser}"/>�����޸ĵĻ��࣬ʵ����һЩĬ�Ϸ�������������д���з���
    /// </summary>
    /// <typeparam name="TTenant">�̻�����</typeparam>
    /// <typeparam name="TUser">�û�����</typeparam>
    public abstract class DefaultExternalAuthenticationSource<TTenant, TUser> : IExternalAuthenticationSource<TTenant, TUser>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUser<TUser>, new()
    {
        /// <summary>
        /// ��֤Դ��Ψһ���ơ�Դ����ͨ��<see cref="AbpUser{TTenant,TUser}.AuthenticationSource"/>���ã�����û�ͨ�������ȨԴ��Ȩ��
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// ���ڳ���ͨ����Դ��֤�û�
        /// </summary>
        /// <param name="userNameOrEmailAddress">�û����������ַ</param>
        /// <param name="plainPassword">�û�����ͨ����</param>
        /// <param name="tenant">�û����⻧��NULL������û��������û���</param>
        /// <returns>true����ʾ��Ӧ�ó������ɸ�Դ���������֤��</returns>
        public abstract Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, TTenant tenant);

        /// <summary>
        /// �˷������ɸ�Դ��֤���û�����Դ��δ���ڡ���ˣ�ԴӦ�ô����û���������ԡ�
        /// </summary>
        /// <param name="userNameOrEmailAddress">�û����������ַ</param>
        /// <param name="tenant">�û����⻧��NULL������û��������û���</param>
        /// <returns>�´������û�</returns>
        public virtual Task<TUser> CreateUserAsync(string userNameOrEmailAddress, TTenant tenant)
        {
            return Task.FromResult(
                new TUser
                {
                    UserName = userNameOrEmailAddress,
                    Name = userNameOrEmailAddress,
                    Surname = userNameOrEmailAddress,
                    EmailAddress = userNameOrEmailAddress,
                    IsEmailConfirmed = true,
                    IsActive = true
                });
        }

        /// <summary>
        /// �˷�����ͨ����Դ��֤�Ѵ��ڵ��û�֮�󱻵��ã�ͨ����Դ�����޸��û���һЩ����
        /// </summary>
        /// <param name="user">�ܱ��޸ĵ��û�</param>
        /// <param name="tenant">�û����⻧��NULL������û��������û���</param>
        public virtual Task UpdateUserAsync(TUser user, TTenant tenant)
        {
            return Task.FromResult(0);
        }
    }
}