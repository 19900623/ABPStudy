using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Localization;
using Abp.Logging;
using Abp.Web;
using Abp.Web.Models;
using Abp.WebApi.Configuration;
using Abp.WebApi.Validation;

namespace Abp.WebApi.Authorization
{
    /// <summary>
    /// ABP Api��Ȩ������
    /// </summary>
    public class AbpApiAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        /// <summary>
        /// ��ȡ������һ������ֵ����ֵָʾ�ܷ�Ϊһ������Ԫ��ָ�����ָʾ����ʵ����
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        /// ��Ȩ������
        /// </summary>
        private readonly IAuthorizationHelper _authorizationHelper;

        /// <summary>
        /// ABP Web Api����
        /// </summary>
        private readonly IAbpWebApiConfiguration _configuration;

        /// <summary>
        /// ���ػ�������
        /// </summary>
        private readonly ILocalizationManager _localizationManager;

        /// <summary>
        /// �¼�����
        /// </summary>
        private readonly IEventBus _eventBus;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="authorizationHelper">��Ȩ������</param>
        /// <param name="configuration">ABP Web Api����</param>
        /// <param name="localizationManager">���ػ�������</param>
        /// <param name="eventBus">�¼�����</param>
        public AbpApiAuthorizeFilter(
            IAuthorizationHelper authorizationHelper, 
            IAbpWebApiConfiguration configuration,
            ILocalizationManager localizationManager,
            IEventBus eventBus)
        {
            _authorizationHelper = authorizationHelper;
            _configuration = configuration;
            _localizationManager = localizationManager;
            _eventBus = eventBus;
        }

        /// <summary>
        /// ִ����Ȩ������ - �첽
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public virtual async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return await continuation();
            }
            
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicAbpAction())
            {
                return await continuation();
            }

            try
            {
                await _authorizationHelper.AuthorizeAsync(methodInfo);
                return await continuation();
            }
            catch (AbpAuthorizationException ex)
            {
                LogHelper.Logger.Warn(ex.ToString(), ex);
                _eventBus.Trigger(this, new AbpHandledExceptionData(ex));
                return CreateUnAuthorizedResponse(actionContext);
            }
        }

        /// <summary>
        /// ����û����Ȩ����Ӧ
        /// </summary>
        /// <param name="actionContext">HttpAction������</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext)
        {
            HttpStatusCode statusCode;
            ErrorInfo error;

            if (actionContext.RequestContext.Principal?.Identity?.IsAuthenticated ?? false)
            {
                statusCode = HttpStatusCode.Forbidden;
                error = new ErrorInfo(
                    _localizationManager.GetString(AbpWebConsts.LocalizaionSourceName, "DefaultError403"),
                    _localizationManager.GetString(AbpWebConsts.LocalizaionSourceName, "DefaultErrorDetail403")
                );
            }
            else
            {
                statusCode = HttpStatusCode.Unauthorized;
                error = new ErrorInfo(
                    _localizationManager.GetString(AbpWebConsts.LocalizaionSourceName, "DefaultError401"),
                    _localizationManager.GetString(AbpWebConsts.LocalizaionSourceName, "DefaultErrorDetail401")
                );
            }

            var response = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<AjaxResponse>(
                    new AjaxResponse(error, true),
                    _configuration.HttpConfiguration.Formatters.JsonFormatter
                )
            };

            return response;
        }
    }
}