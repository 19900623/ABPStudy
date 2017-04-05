namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Names of standard caches used in ABP.
    /// ABP��ʹ�õı�׼��������
    /// </summary>
    public static class AbpCacheNames
    {
        /// <summary>
        /// Application settings cache: AbpApplicationSettingsCache.
        /// Ӧ�ó������û��棺AbpApplicationSettingsCache
        /// </summary>
        public const string ApplicationSettings = "AbpApplicationSettingsCache";

        /// <summary>
        /// Tenant settings cache: AbpTenantSettingsCache.
        /// �⻧���û��棺AbpTenantSettingsCache
        /// </summary>
        public const string TenantSettings = "AbpTenantSettingsCache";

        /// <summary>
        /// User settings cache: AbpUserSettingsCache.
        /// �û����û��棺AbpUserSettingsCache
        /// </summary>
        public const string UserSettings = "AbpUserSettingsCache";

        /// <summary>
        /// Localization scripts cache: AbpLocalizationScripts.
        /// ���ػ��ű����棺AbpLocalizationScripts
        /// </summary>
        public const string LocalizationScripts = "AbpLocalizationScripts";
    }
}