using System;
using Abp.Authorization.Roles;
using Abp.MultiTenancy;
using Abp.Threading;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Extension methods for <see cref="AbpUserManager{TRole,TUser}"/>.
    /// <see cref="AbpUserManager{TRole,TUser}"/>����չ����
    /// </summary>
    public static class AbpUserManagerExtensions
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// ����û��Ƿ���Ȩ
        /// </summary>
        /// <param name="manager">User manager / �û�����</param>
        /// <param name="userId">User id / �û�ID</param>
        /// <param name="permissionName">Permission name / Ȩ������</param>
        public static bool IsGranted<TRole, TUser>(AbpUserManager<TRole, TUser> manager, long userId, string permissionName)
            where TRole : AbpRole<TUser>, new()
            where TUser : AbpUser<TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return AsyncHelper.RunSync(() => manager.IsGrantedAsync(userId, permissionName));
        }

        //public static AbpUserManager<TRole, TUser> Login<TRole, TUser>(AbpUserManager<TRole, TUser> manager, string userNameOrEmailAddress, string plainPassword, string tenancyName = null)
        //    where TRole : AbpRole<TUser>, new()
        //    where TUser : AbpUser<TUser>
        //{
        //    if (manager == null)
        //    {
        //        throw new ArgumentNullException(nameof(manager));
        //    }

        //    return AsyncHelper.RunSync(() => manager.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName));
        //}
    }
}