namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP�û�ʱ������Dto
    /// </summary>
    public class AbpUserTimeZoneConfigDto
    {
        public AbpUserWindowsTimeZoneConfigDto Windows { get; set; }

        public AbpUserIanaTimeZoneConfigDto Iana { get; set; }
    }
}