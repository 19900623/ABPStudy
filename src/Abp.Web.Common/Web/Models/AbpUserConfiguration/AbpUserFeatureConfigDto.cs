using System.Collections.Generic;

namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP�û���������Dto
    /// </summary>
    public class AbpUserFeatureConfigDto
    {
        public Dictionary<string, AbpStringValueDto> AllFeatures { get; set; }
    }
}