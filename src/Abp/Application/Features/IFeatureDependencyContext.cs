using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// ��<see cref="IFeatureDependency.IsSatisfiedAsync"/>������ʹ��
    /// </summary>
    public interface IFeatureDependencyContext
    {
        /// <summary>
        /// Tenant id which required the feature.Null for current tenant.
        /// ��Ҫ�˹��ܵ��⻧ID��NULL���ߵ�ǰ�⻧
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// IOC������
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// ���ܼ����
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IFeatureChecker FeatureChecker { get; }
    }
}