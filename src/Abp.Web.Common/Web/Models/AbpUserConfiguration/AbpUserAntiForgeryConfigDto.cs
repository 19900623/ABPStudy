namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP�û���α����Dto
    /// </summary>
    public class AbpUserAntiForgeryConfigDto
    {
        public string TokenCookieName { get; set; }

        public string TokenHeaderName { get; set; }
    }
}