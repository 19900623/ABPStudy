namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP���⻧����Dto
    /// </summary>
    public class AbpMultiTenancyConfigDto
    {
        public bool IsEnabled { get; set; }

        public AbpMultiTenancySidesConfigDto Sides { get; private set; }

        public AbpMultiTenancyConfigDto()
        {
            Sides = new AbpMultiTenancySidesConfigDto();
        }
    }
}