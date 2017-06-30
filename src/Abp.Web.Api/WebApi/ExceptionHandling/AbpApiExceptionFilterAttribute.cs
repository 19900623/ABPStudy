using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Extensions;
using Abp.Logging;
using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.WebApi.Configuration;
using Abp.WebApi.Controllers;
using Castle.Core.Logging;

namespace Abp.WebApi.ExceptionHandling
{
    /// <summary>
    /// Used to handle exceptions on web api controllers.
    /// ������web api�����������쳣
    /// </summary>
    public class AbpApiExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        /// <summary>
        /// Reference to the <see cref="ILogger"/>.
        /// ��־����
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to the <see cref="IEventBus"/>.
        /// Event Bus����
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// AbpSession����
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Abp WebApi ��������
        /// </summary>
        private readonly IAbpWebApiConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpApiExceptionFilterAttribute"/> class.
        /// ���캯��
        /// </summary>
        public AbpApiExceptionFilterAttribute(IAbpWebApiConfiguration configuration)
        {
            _configuration = configuration;
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// ������쳣�¼�
        /// </summary>
        /// <param name="context">The context for the action. / Actionִ��������</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var wrapResultAttribute = HttpActionDescriptorHelper
                .GetWrapResultAttributeOrNull(context.ActionContext.ActionDescriptor) ??
                _configuration.DefaultWrapResultAttribute;

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            if (!wrapResultAttribute.WrapOnError)
            {
                return;
            }

            if (IsIgnoredUrl(context.Request.RequestUri))
            {
                return;
            }

            context.Response = context.Request.CreateResponse(
                GetStatusCode(context),
                new AjaxResponse(
                    SingletonDependency<ErrorInfoBuilder>.Instance.BuildForException(context.Exception),
                    context.Exception is Abp.Authorization.AbpAuthorizationException)
            );

            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
        }

        /// <summary>
        /// ��ȡHttp״̬��
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception is Abp.Authorization.AbpAuthorizationException)
            {
                return AbpSession.UserId.HasValue
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// �Ƿ����URL
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private bool IsIgnoredUrl(Uri uri)
        {
            if (uri == null || uri.AbsolutePath.IsNullOrEmpty())
            {
                return false;
            }

            return _configuration.ResultWrappingIgnoreUrls.Any(url => uri.AbsolutePath.StartsWith(url));
        }
    }
}