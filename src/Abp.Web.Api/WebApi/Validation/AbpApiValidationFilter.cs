using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Abp.Dependency;
using Abp.WebApi.Configuration;

namespace Abp.WebApi.Validation
{
    /// <summary>
    /// ABP Api��֤������
    /// </summary>
    public class AbpApiValidationFilter : IActionFilter, ITransientDependency
    {
        /// <summary>
        /// �����ѡ
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        /// IOC������
        /// </summary>
        private readonly IIocResolver _iocResolver;
        /// <summary>
        /// ABP WebApi����
        /// </summary>
        private readonly IAbpWebApiConfiguration _configuration;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="configuration"></param>
        public AbpApiValidationFilter(IIocResolver iocResolver, IAbpWebApiConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        /// <summary>
        /// �첽ִ��
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (!_configuration.IsValidationEnabledForControllers)
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

            using (var validator = _iocResolver.ResolveAsDisposable<WebApiActionInvocationValidator>())
            {
                validator.Object.Initialize(actionContext, methodInfo);
                validator.Object.Validate();
            }

            return await continuation();
        }
    }
}