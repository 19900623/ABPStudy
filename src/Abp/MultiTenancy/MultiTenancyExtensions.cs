using Abp.Domain.Entities;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Extension methods for multi-tenancy.
    /// ���⻧��չ����
    /// </summary>
    public static class MultiTenancyExtensions
    {
        /// <summary>
        /// Gets multi-tenancy side (<see cref="MultiTenancySides"/>) of an object that implements <see cref="IMayHaveTenant"/>.
        /// ��ȡ���⻧�� (<see cref="MultiTenancySides"/>)����ʵ���˽ӿ� <see cref="IMayHaveTenant"/>.
        /// </summary>
        /// <param name="obj">The object / ��ʾ���⻧���Ķ���</param>
        public static MultiTenancySides GetMultiTenancySide(this IMayHaveTenant obj)
        {
            return obj.TenantId.HasValue
                ? MultiTenancySides.Tenant
                : MultiTenancySides.Host;
        }
    }
}