using Abp.Threading;

namespace Abp.Authorization
{
    /// <summary>
    /// Extension methods for <see cref="IPermissionDependency"/>.
    /// <see cref="IPermissionDependency"/>����չ����
    /// </summary>
    public static class PermissionDependencyExtensions
    {
        /// <summary>
        /// Checks if permission dependency is satisfied.
        /// ����Ƿ�����Ȩ������
        /// </summary>
        /// <param name="permissionDependency">The permission dependency / Ȩ������</param>
        /// <param name="context">Context. / Ȩ������������</param>
        public static bool IsSatisfied(this IPermissionDependency permissionDependency, IPermissionDependencyContext context)
        {
            return AsyncHelper.RunSync(() => permissionDependency.IsSatisfiedAsync(context));
        }
    }
}