using Abp.WebApi.Controllers;

namespace Derrick.WebApi
{
    /// <summary>
    /// Web API ����
    /// </summary>
    public abstract class AbpZeroTemplateApiControllerBase : AbpApiController
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        protected AbpZeroTemplateApiControllerBase()
        {
            LocalizationSourceName = AbpZeroTemplateConsts.LocalizationSourceName;
        }
    }
}