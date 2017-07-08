using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;

namespace Derrick.Authorization.Users
{
    /// <summary>
    /// �û����ӹ�����
    /// </summary>
    public interface IUserLinkManager
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="firstUser">��һ���û�</param>
        /// <param name="secondUser">�ڶ����û�</param>
        /// <returns></returns>
        Task Link(User firstUser, User secondUser);
        /// <summary>
        /// �û�����
        /// </summary>
        /// <param name="firstUserIdentifier">��һ���û���ʶ</param>
        /// <param name="secondUserIdentifier">�ڶ����û���ʶ</param>
        /// <returns></returns>
        Task<bool> AreUsersLinked(UserIdentifier firstUserIdentifier, UserIdentifier secondUserIdentifier);
        /// <summary>
        /// �Ͽ�����
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <returns></returns>
        Task Unlink(UserIdentifier userIdentifier);
        /// <summary>
        /// ��ȡ�û��ʺ�
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <returns></returns>
        Task<UserAccount> GetUserAccountAsync(UserIdentifier userIdentifier);
    }
}