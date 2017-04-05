using Abp.Domain.Entities;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Standard filters of ABP.
    /// ABP�ı�׼������
    /// </summary>
    public static class AbpDataFilters
    {
        /// <summary>
        /// "SoftDelete".Soft delete filter.
        /// ��ɾ��������
        /// Prevents getting deleted data from database.See <see cref="ISoftDelete"/> interface.
        /// ��ֹ�����ݿ��ȡ��ɾ�������ݡ��鿴 <see cref="ISoftDelete"/> �ӿ�.
        /// </summary>
        public const string SoftDelete = "SoftDelete";

        /// <summary>
        /// "MustHaveTenant".Tenant filter to prevent getting data that is not belong to current tenant.
        /// �⻧����������ֹ��ȡ�����ڵ�ǰ�⻧������
        /// </summary>
        public const string MustHaveTenant = "MustHaveTenant";

        /// <summary>
        /// "MayHaveTenant".Tenant filter to prevent getting data that is not belong to current tenant.
        /// �⻧����������ֹ��ȡ�����ڵ�ǰ�⻧������
        /// </summary>
        public const string MayHaveTenant = "MayHaveTenant";

        /// <summary>
        /// Standard parameters of ABP.
        /// ABP�ı�׼����
        /// </summary>
        public static class Parameters
        {
            /// <summary>
            /// "tenantId". / �⻧ID
            /// </summary>
            public const string TenantId = "tenantId";
        }
    }
}