using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using Derrick.Authorization.Roles;
using Derrick.Authorization.Users;
using Derrick.MultiTenancy;

namespace Derrick.Authorization
{
    /// <summary>
    /// ��¼����
    /// </summary>
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="userManager">�û�����</param>
        /// <param name="multiTenancyConfig">���̻�������Ϣ</param>
        /// <param name="tenantRepository">�̻��ִ�</param>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
        /// <param name="settingManager">���ù���</param>
        /// <param name="userLoginAttemptRepository">�û����Ե�¼�ִ�</param>
        /// <param name="userManagementConfig">�û�����������Ϣ</param>
        /// <param name="iocResolver">IOC������</param>
        /// <param name="roleManager">��ɫ����</param>
        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig, 
            IRepository<Tenant> tenantRepository, 
            IUnitOfWorkManager unitOfWorkManager, 
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig, 
            IIocResolver iocResolver, 
            RoleManager roleManager)
            : base(
                  userManager, 
                  multiTenancyConfig, 
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  roleManager)
        {

        }
    }
}