using System;
using Abp.Configuration;
using Abp.Dependency;

namespace Abp.Timing.Timezone
{
    /// <summary>
    /// Time zone converter class
    /// ʱ��ת����
    /// </summary>
    public class TimeZoneConverter : ITimeZoneConverter, ITransientDependency
    {
        /// <summary>
        /// ���ù�����
        /// </summary>
        private readonly ISettingManager _settingManager;

        /// <summary>
        /// Constructor
        /// ���캯��
        /// </summary>
        /// <param name="settingManager"></param>
        public TimeZoneConverter(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// ��������ʱ��ת��Ϊ�û���ʱ��,���δָ����ʱ�����ã����ظ�������
        /// </summary>
        /// <param name="date">Ҫת����ʱ��</param>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public DateTime? Convert(DateTime? date, int? tenantId, long userId)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var usersTimezone = _settingManager.GetSettingValueForUser(TimingSettingNames.TimeZone, tenantId, userId);
            if(string.IsNullOrEmpty(usersTimezone))
            {
                return date;
            }
            
            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), usersTimezone);
        }

        /// <summary>
        /// ��������ʱ��ת��Ϊ�⻧��ʱ�������δָ����ʱ�����ã����ظ������ڡ�
        /// </summary>
        /// <param name="date">Ҫת����ʱ��</param>
        /// <param name="tenantId">�⻧ID</param>
        /// <returns></returns>
        public DateTime? Convert(DateTime? date, int tenantId)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var tenantsTimezone = _settingManager.GetSettingValueForTenant(TimingSettingNames.TimeZone, tenantId);
            if (string.IsNullOrEmpty(tenantsTimezone))
            {
                return date;
            }

            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), tenantsTimezone);
        }

        /// <summary>
        /// ��������ʱ��ת��ΪӦ�ó����ʱ�������δָ����ʱ�����ã����ظ������ڡ�
        /// </summary>
        /// <param name="date">Ҫת����ʱ��</param>
        /// <returns></returns>
        public DateTime? Convert(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            if (!Clock.SupportsMultipleTimezone)
            {
                return date;
            }

            var applicationsTimezone = _settingManager.GetSettingValueForApplication(TimingSettingNames.TimeZone);
            if (string.IsNullOrEmpty(applicationsTimezone))
            {
                return date;
            }

            return TimezoneHelper.ConvertFromUtc(date.Value.ToUniversalTime(), applicationsTimezone);
        }
    }
}