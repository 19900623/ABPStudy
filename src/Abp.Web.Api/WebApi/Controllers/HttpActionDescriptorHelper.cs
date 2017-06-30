using System.Linq;
using System.Web.Http.Controllers;
using Abp.Collections.Extensions;
using Abp.Web.Models;

namespace Abp.WebApi.Controllers
{
    /// <summary>
    /// Http Action������������
    /// </summary>
    internal static class HttpActionDescriptorHelper
    {
        /// <summary>
        /// ��ȡ��װ�������(û��ȡ���򷵻�Null)
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static WrapResultAttribute GetWrapResultAttributeOrNull(HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
            {
                return null;
            }

            //Try to get for dynamic APIs (dynamic web api actions always define __AbpDynamicApiDontWrapResultAttribute)
            var wrapAttr = actionDescriptor.Properties.GetOrDefault("__AbpDynamicApiDontWrapResultAttribute") as WrapResultAttribute;
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Get for the action
            wrapAttr = actionDescriptor.GetCustomAttributes<WrapResultAttribute>(true).FirstOrDefault();
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Get for the controller
            wrapAttr = actionDescriptor.ControllerDescriptor.GetCustomAttributes<WrapResultAttribute>(true).FirstOrDefault();
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Not found
            return null;
        }
    }
}