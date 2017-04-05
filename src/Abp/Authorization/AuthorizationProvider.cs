using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// This is the main interface to define permissions for an application.Implement it to define permissions for your module.
    /// ����Ϊһ��Ӧ�ö���Ȩ�޵���Ҫ�ӿ�,ʵ�ֶ�ģ���Ȩ�޶���
    /// </summary>
    public abstract class AuthorizationProvider : ITransientDependency
    {
        /// <summary>
        /// This method is called once on application startup to allow to define permissions.
        /// ���������Ӧ�ó�������ʱ���ã���������Ȩ�ޡ�
        /// </summary>
        /// <param name="context">Permission definition context / Ȩ�޶���������</param>
        public abstract void SetPermissions(IPermissionDefinitionContext context);
    }
}