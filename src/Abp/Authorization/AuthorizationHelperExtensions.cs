using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Threading;

namespace Abp.Authorization
{
    /// <summary>
    /// <see cref="AuthorizationHelper"/>����չ
    /// </summary>
    public static class AuthorizationHelperExtensions
    {
        /// <summary>
        /// ��Ȩ - �첽
        /// </summary>
        /// <param name="authorizationHelper">��Ȩ������ӿ�</param>
        /// <param name="authorizeAttribute">ABP��Ȩ����</param>
        /// <returns></returns>
        public static async Task AuthorizeAsync(this IAuthorizationHelper authorizationHelper, IAbpAuthorizeAttribute authorizeAttribute)
        {
            await authorizationHelper.AuthorizeAsync(new[] { authorizeAttribute });
        }

        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="authorizationHelper">��Ȩ������ӿ�</param>
        /// <param name="authorizeAttributes">�ɵ�����ABP��Ȩ�����б�</param>
        public static void Authorize(this IAuthorizationHelper authorizationHelper, IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
        {
            AsyncHelper.RunSync(() => authorizationHelper.AuthorizeAsync(authorizeAttributes));
        }

        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="authorizationHelper">��Ȩ������ӿ�</param>
        /// <param name="authorizeAttribute">ABP��Ȩ����</param>
        public static void Authorize(this IAuthorizationHelper authorizationHelper, IAbpAuthorizeAttribute authorizeAttribute)
        {
            authorizationHelper.Authorize(new[] { authorizeAttribute });
        }

        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="authorizationHelper">��Ȩ������ӿ�</param>
        /// <param name="methodInfo">������Ϣ</param>
        public static void Authorize(this IAuthorizationHelper authorizationHelper, MethodInfo methodInfo)
        {
            AsyncHelper.RunSync(() => authorizationHelper.AuthorizeAsync(methodInfo));
        }
    }
}