using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Implementation of <see cref="IFeatureDependencyContext"/>.
    /// <see cref="IFeatureDependencyContext"/>��ʵ��
    /// </summary>
    public class FeatureDependencyContext : IFeatureDependencyContext, ITransientDependency
    {
        /// <summary>
        /// ��Ҫ�˹��ܵ��⻧ID��NULL���ߵ�ǰ�⻧
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// IOC������
        /// </summary>
        public IIocResolver IocResolver { get; private set; }

        /// <summary>
        /// ���ܼ����
        /// </summary>
        public IFeatureChecker FeatureChecker { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDependencyContext"/> class.
        /// ���캯��
        /// </summary>
        public FeatureDependencyContext(IIocResolver iocResolver, IFeatureChecker featureChecker)
        {
            IocResolver = iocResolver;
            FeatureChecker = featureChecker;
        }
    }
}