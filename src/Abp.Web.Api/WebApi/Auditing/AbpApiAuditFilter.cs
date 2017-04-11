using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Abp.Auditing;
using Abp.Dependency;
using Abp.WebApi.Validation;

namespace Abp.WebApi.Auditing
{
    /// <summary>
    /// ABP Api ��ƹ�����
    /// </summary>
    public class AbpApiAuditFilter : IActionFilter, ITransientDependency
    {
        /// <summary>
        /// ��ȡ������һ������ֵ����ֵָʾ�ܷ�Ϊһ������Ԫ��ָ�����ָʾ����ʵ����
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        /// ��ư�����
        /// </summary>
        private readonly IAuditingHelper _auditingHelper;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="auditingHelper">��ư�����</param>
        public AbpApiAuditFilter(IAuditingHelper auditingHelper)
        {
            _auditingHelper = auditingHelper;
        }

        /// <summary>
        /// �첽ִ��Action Filter
        /// </summary>
        /// <param name="actionContext">HttpAction������</param>
        /// <param name="cancellationToken"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var method = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (method == null || !ShouldSaveAudit(actionContext))
            {
                return await continuation();
            }

            var auditInfo = _auditingHelper.CreateAuditInfo(
                method,
                actionContext.ActionArguments
            );

            var stopwatch = Stopwatch.StartNew();

            try
            {
                return await continuation();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                await _auditingHelper.SaveAsync(auditInfo);
            }
        }

        /// <summary>
        /// �Ƿ񱣴����
        /// </summary>
        /// <param name="context">HttpAction������</param>
        /// <returns></returns>
        private bool ShouldSaveAudit(HttpActionContext context)
        {
            if (context.ActionDescriptor.IsDynamicAbpAction())
            {
                return false;
            }

            return _auditingHelper.ShouldSaveAudit(context.ActionDescriptor.GetMethodInfoOrNull(), true);
        }
    }
}