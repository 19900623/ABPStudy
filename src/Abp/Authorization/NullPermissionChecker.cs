using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// Null (and default) implementation of <see cref="IPermissionChecker"/>.
    /// <see cref="IPermissionChecker"/>��null��Ĭ��ʵ��
    /// </summary>
    public sealed class NullPermissionChecker : IPermissionChecker
    {
        /// <summary>
        /// Singleton instance.
        /// ����ģʽ
        /// </summary>
        public static NullPermissionChecker Instance { get; } = new NullPermissionChecker();

        /// <summary>
        /// �첽��鵱ǰ�û��Ƿ����������Ȩ��
        /// </summary>
        /// <param name="permissionName">Ȩ������</param>
        /// <returns></returns>
        public Task<bool> IsGrantedAsync(string permissionName)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// ���һ���û��Ƿ����������Ȩ��
        /// </summary>
        /// <param name="user">��Ҫ�����û����</param>
        /// <param name="permissionName">Ȩ������</param>
        /// <returns></returns>
        public Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            return Task.FromResult(true);
        }
    }
}