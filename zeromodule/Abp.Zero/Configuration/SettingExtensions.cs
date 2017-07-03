namespace Abp.Configuration
{
    /// <summary>
    /// Implements methods to convert objects between SettingInfo and Setting classes.
    /// ʵ�ֽ�������Ϣ��������֮��ת���ķ���
    /// </summary>
    internal static class SettingExtensions
    {
        /// <summary>
        /// �Ӹ�����<see cref="SettingInfo"/>���󴴽�һ��<see cref="Setting"/>����
        /// </summary>
        public static Setting ToSetting(this SettingInfo settingInfo)
        {
            return settingInfo == null
                ? null
                : new Setting(settingInfo.TenantId, settingInfo.UserId, settingInfo.Name, settingInfo.Value);
        }

        /// <summary>
        /// �Ӹ�����<see cref="Setting"/>���󴴽�һ��<see cref="SettingInfo"/>����
        /// </summary>
        public static SettingInfo ToSettingInfo(this Setting setting)
        {
            return setting == null
                ? null
                : new SettingInfo(setting.TenantId, setting.UserId, setting.Name, setting.Value);
        }
    }
}