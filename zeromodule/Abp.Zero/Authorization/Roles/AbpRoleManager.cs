using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Zero;
using Abp.Zero.Configuration;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Roles
{
    /// <summary>
    /// Extends <see cref="RoleManager{TRole,TKey}"/> of ASP.NET Identity Framework.Applications should derive this class with appropriate generic arguments.
    /// ASP.NET Identity��ܵ���չ<see cref="RoleManager{TRole,TKey}"/> .Ӧ�ó���Ӧ�����ʵ��ķ��Ͳ������������
    /// </summary>
    public abstract class AbpRoleManager<TRole, TUser>
        : RoleManager<TRole, int>,
        IDomainService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// ���ػ���������
        /// </summary>
        public ILocalizationManager LocalizationManager { get; set; }
        /// <summary>
        /// ABP Session����
        /// </summary>
        public IAbpSession AbpSession { get; set; }
        /// <summary>
        /// ��ɫ��������
        /// </summary>
        public IRoleManagementConfig RoleManagementConfig { get; private set; }
        /// <summary>
        /// ��ɫȨ�޴洢����
        /// </summary>
        private IRolePermissionStore<TRole, TUser> RolePermissionStore
        {
            get
            {
                if (!(Store is IRolePermissionStore<TRole, TUser>))
                {
                    throw new AbpException("Store is not IRolePermissionStore");
                }

                return Store as IRolePermissionStore<TRole, TUser>;
            }
        }
        /// <summary>
        /// ��ɫ�洢����
        /// </summary>
        protected AbpRoleStore<TRole, TUser> AbpStore { get; private set; }
        /// <summary>
        /// Ȩ�޹�������
        /// </summary>
        private readonly IPermissionManager _permissionManager;
        /// <summary>
        /// ��������
        /// </summary>
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// ������Ԫ����
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��.
        /// </summary>
        protected AbpRoleManager(
            AbpRoleStore<TRole, TUser> store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(store)
        {
            _permissionManager = permissionManager;
            _cacheManager = cacheManager;
            _unitOfWorkManager = unitOfWorkManager;

            RoleManagementConfig = roleManagementConfig;
            AbpStore = store;
            AbpSession = NullAbpSession.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        #region Obsolete methods

        /// <summary>
        /// Checks if a role has a permission.
        /// ����ɫ�Ƿ��и�����Ȩ��
        /// </summary>
        /// <param name="roleName">The role's name to check it's permission / �����Ȩ�޵Ľ�ɫ����</param>
        /// <param name="permissionName">Name of the permission / Ȩ������</param>
        /// <returns>True, if the role has the permission / true,�����ɫӵ�д�Ȩ��</returns>
        [Obsolete("Use IsGrantedAsync instead.")]
        public virtual async Task<bool> HasPermissionAsync(string roleName, string permissionName)
        {
            return await IsGrantedAsync((await GetRoleByNameAsync(roleName)).Id, _permissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role has a permission.
        /// ����ɫ�Ƿ��и�����Ȩ��
        /// </summary>
        /// <param name="roleId">The role's id to check it's permission / �����Ȩ�޵Ľ�ɫID</param>
        /// <param name="permissionName">Name of the permission / Ȩ������</param>
        /// <returns>True, if the role has the permission / true,�����ɫӵ�д�Ȩ��</returns>
        [Obsolete("Use IsGrantedAsync instead.")]
        public virtual async Task<bool> HasPermissionAsync(int roleId, string permissionName)
        {
            return await IsGrantedAsync(roleId, _permissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role has a permission.
        /// ����ɫ�Ƿ��и�����Ȩ��
        /// </summary>
        /// <param name="role">The role / ��ɫ����</param>
        /// <param name="permission">The permission / Ȩ�޶���</param>
        /// <returns>True, if the role has the permission /  true,�����ɫӵ�д�Ȩ��</returns>
        [Obsolete("Use IsGrantedAsync instead.")]
        public Task<bool> HasPermissionAsync(TRole role, Permission permission)
        {
            return IsGrantedAsync(role.Id, permission);
        }

        /// <summary>
        /// Checks if a role has a permission.
        /// ����ɫ�Ƿ��и�����Ȩ��
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <param name="permission">Ȩ�޶���</param>
        /// <returns>True, if the role has the permission /  true,�����ɫӵ�д�Ȩ��</returns>
        [Obsolete("Use IsGrantedAsync instead.")]
        public Task<bool> HasPermissionAsync(int roleId, Permission permission)
        {
            return IsGrantedAsync(roleId, permission);
        }

        #endregion

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// ����ɫ�Ƿ��и�����Ȩ��
        /// </summary>
        /// <param name="roleName">The role's name to check it's permission / �����Ȩ�޵Ľ�ɫ����</param>
        /// <param name="permissionName">Name of the permission / Ȩ������</param>
        /// <returns>True, if the role has the permission / true,�����ɫӵ�д�Ȩ��</returns>
        public virtual async Task<bool> IsGrantedAsync(string roleName, string permissionName)
        {
            return await IsGrantedAsync((await GetRoleByNameAsync(roleName)).Id, _permissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role has a permission.
        /// ����ɫ�Ƿ��и�����Ȩ��
        /// </summary>
        /// <param name="roleId">The role's id to check it's permission / �����Ȩ�޵Ľ�ɫID</param>
        /// <param name="permissionName">Name of the permission / Ȩ������</param>
        /// <returns>True, if the role has the permission / true,�����ɫӵ�д�Ȩ��</returns>
        public virtual async Task<bool> IsGrantedAsync(int roleId, string permissionName)
        {
            return await IsGrantedAsync(roleId, _permissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// ���ĳ����ɫ�Ƿ�����Ȩ��
        /// </summary>
        /// <param name="role">The role / ��ɫ</param>
        /// <param name="permission">The permission / Ȩ��</param>
        /// <returns>True, if the role has the permission / true,�����ɫӵ�д�Ȩ��</returns>
        public Task<bool> IsGrantedAsync(TRole role, Permission permission)
        {
            return IsGrantedAsync(role.Id, permission);
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// ���ĳ����ɫ�Ƿ�����Ȩ��
        /// </summary>
        /// <param name="roleId">role id / ��ɫID</param>
        /// <param name="permission">The permission / Ȩ�޶���</param>
        /// <returns>True, if the role has the permission / true,�����ɫӵ�д�Ȩ��</returns>
        public virtual async Task<bool> IsGrantedAsync(int roleId, Permission permission)
        {
            //Get cached role permissions
            var cacheItem = await GetRolePermissionCacheItemAsync(roleId);

            //Check the permission
            return cacheItem.GrantedPermissions.Contains(permission.Name);
        }

        /// <summary>
        /// Gets granted permission names for a role.
        /// ��ȡ��ɫ������Ȩ�޼���
        /// </summary>
        /// <param name="roleId">Role id / ��ɫID</param>
        /// <returns>List of granted permissions / �����Ȩ�޼���</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(int roleId)
        {
            return await GetGrantedPermissionsAsync(await GetRoleByIdAsync(roleId));
        }

        /// <summary>
        /// Gets granted permission names for a role.
        /// ��ȡ��ɫ������Ȩ�޼���
        /// </summary>
        /// <param name="roleName">Role name / ��ɫ����</param>
        /// <returns>List of granted permissions / ����Ȩ�޼���</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(string roleName)
        {
            return await GetGrantedPermissionsAsync(await GetRoleByNameAsync(roleName));
        }

        /// <summary>
        /// Gets granted permissions for a role.
        /// ��ȡ��ɫ������Ȩ�޼���
        /// </summary>
        /// <param name="role">Role / ��ɫ����</param>
        /// <returns>List of granted permissions / �����Ȩ�޼���</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(TRole role)
        {
            var permissionList = new List<Permission>();

            foreach (var permission in _permissionManager.GetAllPermissions())
            {
                if (await IsGrantedAsync(role.Id, permission))
                {
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }

        /// <summary>
        /// Sets all granted permissions of a role at once.Prohibits all other permissions.
        /// �������ý�ɫ����������Ȩ�ޣ���ֹ���е�����Ȩ��
        /// </summary>
        /// <param name="roleId">Role id / ��ɫID</param>
        /// <param name="permissions">Permissions / Ȩ�޼���</param>
        public virtual async Task SetGrantedPermissionsAsync(int roleId, IEnumerable<Permission> permissions)
        {
            await SetGrantedPermissionsAsync(await GetRoleByIdAsync(roleId), permissions);
        }

        /// <summary>
        /// Sets all granted permissions of a role at once.Prohibits all other permissions.
        /// �������ý�ɫ����������Ȩ�ޣ���ֹ���е�����Ȩ��
        /// </summary>
        /// <param name="role">The role / ��ɫ����</param>
        /// <param name="permissions">Permissions / Ȩ�޼���</param>
        public virtual async Task SetGrantedPermissionsAsync(TRole role, IEnumerable<Permission> permissions)
        {
            var oldPermissions = await GetGrantedPermissionsAsync(role);
            var newPermissions = permissions.ToArray();

            foreach (var permission in oldPermissions.Where(p => !newPermissions.Contains(p, PermissionEqualityComparer.Instance)))
            {
                await ProhibitPermissionAsync(role, permission);
            }

            foreach (var permission in newPermissions.Where(p => !oldPermissions.Contains(p, PermissionEqualityComparer.Instance)))
            {
                await GrantPermissionAsync(role, permission);
            }
        }

        /// <summary>
        /// Grants a permission for a role.
        /// Ϊ��ɫ����һ��Ȩ��
        /// </summary>
        /// <param name="role">��ɫ����</param>
        /// <param name="permission">Ȩ�޶���</param>
        public async Task GrantPermissionAsync(TRole role, Permission permission)
        {
            if (await IsGrantedAsync(role.Id, permission))
            {
                return;
            }

            await RolePermissionStore.AddPermissionAsync(role, new PermissionGrantInfo(permission.Name, true));
        }

        /// <summary>
        /// Prohibits a permission for a role.
        /// Ϊ��ɫ��ֹһ��Ȩ��
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <param name="permission">Ȩ��</param>
        public async Task ProhibitPermissionAsync(TRole role, Permission permission)
        {
            if (!await IsGrantedAsync(role.Id, permission))
            {
                return;
            }

            await RolePermissionStore.RemovePermissionAsync(role, new PermissionGrantInfo(permission.Name, true));
        }

        /// <summary>
        /// Prohibits all permissions for a role.
        /// Ϊ��ɫ��ֹ����Ȩ��
        /// </summary>
        /// <param name="role">��ɫ</param>
        public async Task ProhibitAllPermissionsAsync(TRole role)
        {
            foreach (var permission in _permissionManager.GetAllPermissions())
            {
                await ProhibitPermissionAsync(role, permission);
            }
        }

        /// <summary>
        /// Resets all permission settings for a role.It removes all permission settings for the role.
        /// Ϊ��ɫ��������Ȩ�ޣ�����Ϊ��ɫ�Ƴ����е�Ȩ������
        /// Role will have permissions those have <see cref="Permission.IsGrantedByDefault"/> set to true.
        /// ��ɫ��������<see cref="Permission.IsGrantedByDefault"/>Ϊtrue��Ȩ��
        /// </summary>
        /// <param name="role">Role</param>
        public async Task ResetAllPermissionsAsync(TRole role)
        {
            await RolePermissionStore.RemoveAllPermissionSettingsAsync(role);
        }

        /// <summary>
        /// ����һ����ɫ
        /// </summary>
        /// <param name="role">��ɫ</param>
        public override async Task<IdentityResult> CreateAsync(TRole role)
        {
            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name, role.DisplayName);
            if (!result.Succeeded)
            {
                return result;
            }

            var tenantId = GetCurrentTenantId();
            if (tenantId.HasValue && !role.TenantId.HasValue)
            {
                role.TenantId = tenantId.Value;
            }

            return await base.CreateAsync(role);
        }
        /// <summary>
        /// �޸Ľ�ɫ
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <returns></returns>
        public override async Task<IdentityResult> UpdateAsync(TRole role)
        {
            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name, role.DisplayName);
            if (!result.Succeeded)
            {
                return result;
            }

            return await base.UpdateAsync(role);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="role">��ɫ</param>
        public async override Task<IdentityResult> DeleteAsync(TRole role)
        {
            if (role.IsStatic)
            {
                return AbpIdentityResult.Failed(string.Format(L("CanNotDeleteStaticRole"), role.Name));
            }

            return await base.DeleteAsync(role);
        }

        /// <summary>
        /// Gets a role by given id.Throws exception if no role with given id.
        /// ͨ��������ID��ȡ��ɫ�����û�����׳��쳣
        /// </summary>
        /// <param name="roleId">Role id / ��ɫID</param>
        /// <returns>��ɫ</returns>
        /// <exception cref="AbpException">���û���ҵ����׳����쳣����</exception>
        public virtual async Task<TRole> GetRoleByIdAsync(int roleId)
        {
            var role = await FindByIdAsync(roleId);
            if (role == null)
            {
                throw new AbpException("There is no role with id: " + roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets a role by given name.Throws exception if no role with given roleName.
        /// ͨ�����������ƻ�ȡ��ɫ�����û���ҵ����׳��쳣
        /// </summary>
        /// <param name="roleName">��ɫ����</param>
        /// <returns>��ɫ����</returns>
        /// <exception cref="AbpException">�����ɫ����û���ҵ���Ӧ�Ľ�ɫ���׳����쳣����</exception>
        public virtual async Task<TRole> GetRoleByNameAsync(string roleName)
        {
            var role = await FindByNameAsync(roleName);
            if (role == null)
            {
                throw new AbpException("There is no role with name: " + roleName);
            }

            return role;
        }
        /// <summary>
        /// Ϊ��ɫ��������Ȩ��
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <returns></returns>
        public async Task GrantAllPermissionsAsync(TRole role)
        {
            var permissions = _permissionManager.GetAllPermissions(role.GetMultiTenancySide());
            await SetGrantedPermissionsAsync(role, permissions);
        }
        /// <summary>
        /// ������̬��ɫ
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<IdentityResult> CreateStaticRoles(int tenantId)
        {
            var staticRoleDefinitions = RoleManagementConfig.StaticRoles.Where(sr => sr.Side == MultiTenancySides.Tenant);

            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var staticRoleDefinition in staticRoleDefinitions)
                {
                    var role = new TRole
                    {
                        TenantId = tenantId,
                        Name = staticRoleDefinition.RoleName,
                        DisplayName = staticRoleDefinition.RoleName,
                        IsStatic = true
                    };

                    var identityResult = await CreateAsync(role);
                    if (!identityResult.Succeeded)
                    {
                        return identityResult;
                    }
                }
            }

            return IdentityResult.Success;
        }
        /// <summary>
        /// ����ظ��Ľ�ɫ����
        /// </summary>
        /// <param name="expectedRoleId">Ԥ�ƵĽ�ɫID</param>
        /// <param name="name">��ɫ����</param>
        /// <param name="displayName">��ʾ����</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> CheckDuplicateRoleNameAsync(int? expectedRoleId, string name, string displayName)
        {
            var role = await FindByNameAsync(name);
            if (role != null && role.Id != expectedRoleId)
            {
                return AbpIdentityResult.Failed(string.Format(L("RoleNameIsAlreadyTaken"), name));
            }

            role = await FindByDisplayNameAsync(displayName);
            if (role != null && role.Id != expectedRoleId)
            {
                return AbpIdentityResult.Failed(string.Format(L("RoleDisplayNameIsAlreadyTaken"), displayName));
            }

            return IdentityResult.Success;
        }
        /// <summary>
        /// ͨ����ʾ�����ҽ�ɫ
        /// </summary>
        /// <param name="displayName">��ʾ��</param>
        /// <returns>��ɫ����</returns>
        private Task<TRole> FindByDisplayNameAsync(string displayName)
        {
            return AbpStore.FindByDisplayNameAsync(displayName);
        }
        /// <summary>
        /// ��ȡ��ɫȨ�޻�����
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        /// <returns></returns>
        private async Task<RolePermissionCacheItem> GetRolePermissionCacheItemAsync(int roleId)
        {
            var cacheKey = roleId + "@" + (GetCurrentTenantId() ?? 0);
            return await _cacheManager.GetRolePermissionCache().GetAsync(cacheKey, async () =>
            {
                var newCacheItem = new RolePermissionCacheItem(roleId);

                foreach (var permissionInfo in await RolePermissionStore.GetPermissionsAsync(roleId))
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.Add(permissionInfo.Name);
                    }
                }

                return newCacheItem;
            });
        }
        /// <summary>
        /// ��ȡ���ػ��ַ���
        /// </summary>
        /// <param name="name">��ȡ�ַ���Key</param>
        /// <returns></returns>
        private string L(string name)
        {
            return LocalizationManager.GetString(AbpZeroConsts.LocalizationSourceName, name);
        }
        /// <summary>
        /// ��ȡ��ǰ�̻�ID
        /// </summary>
        /// <returns></returns>
        private int? GetCurrentTenantId()
        {
            if (_unitOfWorkManager.Current != null)
            {
                return _unitOfWorkManager.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }
    }
}