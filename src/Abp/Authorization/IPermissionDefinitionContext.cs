using Abp.Application.Features;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// This context is used on <see cref="AuthorizationProvider.SetPermissions"/> method.
    /// �����������ڷ���<see cref="AuthorizationProvider.SetPermissions"/> .
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// Creates a new permission under this group.
        /// ����һ��Permission����
        /// </summary>
        /// <param name="name">Unique name of the permission / Ȩ��Ψһ����</param>
        /// <param name="displayName">Display name of the permission / ��ʾ����</param>
        /// <param name="description">A brief description for this permission / ��Ҫ������</param>
        /// <param name="multiTenancySides">Which side can use this permission / ��һ��ʹ�ø�Ȩ��</param>
        /// <param name="featureDependency">Depended feature(s) of this permission / ��Ȩ�޵���������</param>
        /// <returns>New created permission / �´�����Ȩ��</returns>
        Permission CreatePermission(
            string name, 
            ILocalizableString displayName = null, 
            ILocalizableString description = null, 
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null
            );

        /// <summary>
        /// Gets a permission with given name or null if can not find.
        /// ��ȡһ���������Ƶ�Ȩ�ޣ���������ڷ���null
        /// </summary>
        /// <param name="name">Unique name of the permission / Ψһ��Ȩ������</param>
        /// <returns>Permission object or null / Permission������� null</returns>
        Permission GetPermissionOrNull(string name);
    }
}