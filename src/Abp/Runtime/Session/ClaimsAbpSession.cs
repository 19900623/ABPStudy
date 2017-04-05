using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime.Security;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IAbpSession"/> to get session properties from claims of <see cref="Thread.CurrentPrincipal"/>.
    /// <see cref="IAbpSession"/>��ʵ�֣���<see cref="Thread.CurrentPrincipal"/>�����л�ȡsession����
    /// </summary>
    public class ClaimsAbpSession : IAbpSession, ISingletonDependency
    {
        /// <summary>
        /// ��ȡ��ǰ�û���ţ����ǿɿյģ�����û�û�е�¼
        /// </summary>
        public virtual long? UserId
        {
            get
            {
                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�⻧ID,����⻧IDӦ����<see cref="UserId"/>���⻧ID,����Ϊnull���������<see cref="UserId"/>�������û��������û�û�е�¼
        /// </summary>
        public virtual int? TenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
                if (string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(tenantIdClaim.Value);
            }
        }

        /// <summary>
        /// ������û�ID������û�����ִ�з�������(<see cref="UserId"/>)��Ӧ�������
        /// </summary>
        public virtual long? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        /// <summary>
        /// ������⻧ID,���һ���û�ʹ��<see cref="UserId"/>����ʹ�� <see cref="ImpersonatorUserId"/>ִ�з�������Ӧ�������
        /// </summary>
        public virtual int? ImpersonatorTenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var impersonatorTenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
                if (string.IsNullOrEmpty(impersonatorTenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        /// <summary>
        /// ��ʾ���⻧˫���е�һ��
        /// </summary>
        public virtual MultiTenancySides MultiTenancySide
        {
            get
            {
                return MultiTenancy.IsEnabled && !TenantId.HasValue
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;
            }
        }

        public IPrincipalAccessor PrincipalAccessor { get; set; } //TODO: Convert to constructor-injection

        /// <summary>
        /// ���⻧����
        /// </summary>
        protected readonly IMultiTenancyConfig MultiTenancy;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="multiTenancy"></param>
        public ClaimsAbpSession(IMultiTenancyConfig multiTenancy)
        {
            MultiTenancy = multiTenancy;
            PrincipalAccessor = DefaultPrincipalAccessor.Instance;
        }
    }
}