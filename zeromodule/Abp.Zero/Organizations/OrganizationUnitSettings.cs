using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Zero.Configuration;

namespace Abp.Organizations
{
    /// <summary>
    /// Implements <see cref="IOrganizationUnitSettings"/> to get settings from <see cref="ISettingManager"/>.
    /// <see cref="IOrganizationUnitSettings"/> ��ʵ�ִ�<see cref="ISettingManager"/>��ȡ����
    /// </summary>
    public class OrganizationUnitSettings : IOrganizationUnitSettings, ITransientDependency
    {
        /// <summary>
        /// Maximum allowed organization unit membership count for a user.Returns value for current tenant.
        /// ��ȡ�û�����������֯�ܹ���Ա���������ص�ǰ�̻���ֵ
        /// </summary>
        public int MaxUserMembershipCount
        {
            get
            {
                return _settingManager.GetSettingValue<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount);
            }
        }

        /// <summary>
        /// ���ù�������
        /// </summary>
        private readonly ISettingManager _settingManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        public OrganizationUnitSettings(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// Maximum allowed organization unit membership count for a user.Returns value for given tenant.
        /// ��ȡ�û�����������֯�ܹ���Ա���������ص�ǰ�̻���ֵ
        /// </summary>
        public async Task<int> GetMaxUserMembershipCountAsync(int? tenantId)
        {
            if (tenantId.HasValue)
            {
                return await _settingManager.GetSettingValueForTenantAsync<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, tenantId.Value);
            }
            else
            {
                return await _settingManager.GetSettingValueForApplicationAsync<int>(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount);
            }
        }
        /// <summary>
        /// �����û�����������֯�ܹ���Ա����
        /// </summary>
        /// <param name="tenantId">�̻�ID��Null(�̻��������̻�)</param>
        /// <param name="value">���õ�ֵ</param>
        /// <returns></returns>
        public async Task SetMaxUserMembershipCountAsync(int? tenantId, int value)
        {
            if (tenantId.HasValue)
            {
                await _settingManager.ChangeSettingForTenantAsync(tenantId.Value, AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, value.ToString());
            }
            else
            {
                await _settingManager.ChangeSettingForApplicationAsync(AbpZeroSettingNames.OrganizationUnits.MaxUserMembershipCount, value.ToString());
            }
        }
    }
}