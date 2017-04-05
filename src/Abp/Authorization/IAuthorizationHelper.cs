using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// ��Ȩ�����ӿ�
    /// </summary>
    public interface IAuthorizationHelper
    {
        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="authorizeAttributes">ABP��Ȩ����</param>
        /// <returns></returns>
        Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes);

        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="methodInfo">������Ϣ</param>
        /// <returns></returns>
        Task AuthorizeAsync(MethodInfo methodInfo);
    }
}