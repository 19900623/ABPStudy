using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Timing;
using Abp.Extensions;

namespace Derrick.Timing
{
    /// <summary>
    /// <see cref="ITimeZoneService"/>ʱ������ʵ�֡�
    /// </summary>
    public class TimeZoneService : ITimeZoneService, ITransientDependency
    {
        /// <summary>
        /// ���ù�����
        /// </summary>
        readonly ISettingManager _settingManager;
        /// <summary>
        /// ���ö��������
        /// </summary>
        readonly ISettingDefinitionManager _settingDefinitionManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="settingManager">���ù�����</param>
        /// <param name="settingDefinitionManager">���ö��������</param>
        public TimeZoneService(
            ISettingManager settingManager, 
            ISettingDefinitionManager settingDefinitionManager)
        {
            _settingManager = settingManager;
            _settingDefinitionManager = settingDefinitionManager;
        }

        /// <summary>
        /// ��ȡĬ��ʱ��
        /// </summary>
        /// <param name="scope">����������</param>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        public async Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId)
        {
            if (scope == SettingScopes.User)
            {
                if (tenantId.HasValue)
                {
                    return await _settingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, tenantId.Value);
                }

                return await _settingManager.GetSettingValueForApplicationAsync(TimingSettingNames.TimeZone);
            }

            if (scope == SettingScopes.Tenant)
            {
                return await _settingManager.GetSettingValueForApplicationAsync(TimingSettingNames.TimeZone);
            }

            if (scope == SettingScopes.Application)
            {
                var timezoneSettingDefinition = _settingDefinitionManager.GetSettingDefinition(TimingSettingNames.TimeZone);
                return timezoneSettingDefinition.DefaultValue;
            }

            throw new Exception("Unknown scope for default timezone setting.");
        }
    }
}