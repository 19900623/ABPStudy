namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP�û�����Dto
    /// </summary>
    public class AbpUserConfigurationDto
    {
        public AbpMultiTenancyConfigDto MultiTenancy { get; set; }

        public AbpUserSessionConfigDto Session { get; set; }

        public AbpUserLocalizationConfigDto Localization { get; set; }

        public AbpUserFeatureConfigDto Features { get; set; }

        public AbpUserAuthConfigDto Auth { get; set; }

        public AbpUserNavConfigDto Nav { get; set; }

        public AbpUserSettingConfigDto Setting { get; set; }

        public AbpUserClockConfigDto Clock { get; set; }

        public AbpUserTimingConfigDto Timing { get; set; }

        public AbpUserSecurityConfigDto Security { get; set; }
    }
}