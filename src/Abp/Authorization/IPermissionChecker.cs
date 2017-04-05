using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// This class is used to permissions for users.
    /// ���������û���Ȩ�޼��
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// �첽��鵱ǰ�û��Ƿ����������Ȩ��
        /// </summary>
        /// <param name="permissionName">Name of the permission / Ȩ������</param>
        Task<bool> IsGrantedAsync(string permissionName);

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// ���һ���û��Ƿ����������Ȩ��
        /// </summary>
        /// <param name="user">User to check / ��Ҫ�����û����</param>
        /// <param name="permissionName">Name of the permission / Ȩ������</param>
        Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName);
    }
}