using System.Collections.Generic;
using Abp.Application.Navigation;

namespace Abp.Web.Models.AbpUserConfiguration
{
    /// <summary>
    /// ABP�û���������Dto
    /// </summary>
    public class AbpUserNavConfigDto
    {
        public Dictionary<string, UserMenu> Menus { get; set; }
    }
}