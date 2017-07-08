using Abp.Application.Features;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// �����������ڷ���<see cref="AuthorizationProvider.SetPermissions"/> .
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// ����һ��Permission����
        /// </summary>
        /// <param name="name">Ȩ��Ψһ����</param>
        /// <param name="displayName">��ʾ����</param>
        /// <param name="description">��Ҫ������</param>
        /// <param name="multiTenancySides">��һ��ʹ�ø�Ȩ��</param>
        /// <param name="featureDependency">��Ȩ�޵���������</param>
        /// <returns>�´�����Ȩ��</returns>
        Permission CreatePermission(
            string name, 
            ILocalizableString displayName = null, 
            ILocalizableString description = null, 
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null
            );

        /// <summary>
        /// ��ȡһ���������Ƶ�Ȩ�ޣ���������ڷ���null
        /// </summary>
        /// <param name="name">Ψһ��Ȩ������</param>
        /// <returns>Permission������� null</returns>
        Permission GetPermissionOrNull(string name);
    }
}