using Abp.Runtime.Session;

namespace Derrick.MultiTenancy
{
    /// <summary>
    /// Used to get current tenant id where <see cref="IAbpSession"/> is not usable.
    /// ��<see cref="IAbpSession"/>�ǲ�����ʱ��������ȡ��ǰ�̻���ID
    /// </summary>
    public interface ITenantIdAccessor
    {
        /// <summary>
        /// Gets current tenant id.Use <see cref="IAbpSession.TenantId"/> wherever possible (if user logged in).
        /// ��ȡ��ǰ�̻�ID�����κο��ܵĵط�ʹ��<see cref="IAbpSession.TenantId"/>(����û��ѵ�¼)
        /// This method tries to get current tenant id even if current user did not log in.
        /// ��������ǵ��û�û�е�¼��ʱ���������Ի�ȡ��ǰ�̻���ID
        /// </summary>
        /// <param name="useSession">Set false to skip session usage</param>
        int? GetCurrentTenantIdOrNull(bool useSession = true);
    }
}