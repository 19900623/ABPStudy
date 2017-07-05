using System.Threading.Tasks;

namespace Abp.Organizations
{
    /// <summary>
    /// Used to get settings related to OrganizationUnits.
    /// ���ڻ�ȡ��֯�ܹ���ص�����
    /// </summary>
    public interface IOrganizationUnitSettings
    {
        /// <summary>
        /// Get Maximum allowed organization unit membership count for a user.Returns value for current tenant.
        /// ��ȡ�û�����������֯�ܹ���Ա���������ص�ǰ�̻���ֵ
        /// </summary>
        int MaxUserMembershipCount { get; }

        /// <summary>
        /// Gets Maximum allowed organization unit membership count for a user.Returns value for given tenant.
        /// ��ȡ�û�����������֯�ܹ���Ա���������ص�ǰ�̻���ֵ
        /// </summary>
        /// <param name="tenantId">The tenant Id or null for the host.</param>
        Task<int> GetMaxUserMembershipCountAsync(int? tenantId);

        /// <summary>
        /// Sets Maximum allowed organization unit membership count for a user.
        /// �����û�����������֯�ܹ���Ա����
        /// </summary>
        /// <param name="tenantId">The tenant Id or null for the host. / �̻�ID��Null(�̻��������̻�)</param>
        /// <param name="value">Setting value. / ���õ�ֵ</param>
        /// <returns></returns>
        Task SetMaxUserMembershipCountAsync(int? tenantId, int value);
    }
}