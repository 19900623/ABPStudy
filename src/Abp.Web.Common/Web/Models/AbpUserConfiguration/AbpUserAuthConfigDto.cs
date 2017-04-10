using System.Collections.Generic;

namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP�û���֤����Dto
    /// </summary>
    public class AbpUserAuthConfigDto
    {
        public Dictionary<string,string> AllPermissions { get; set; }

        public Dictionary<string, string> GrantedPermissions { get; set; }
        
    }
}