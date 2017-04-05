using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// This class should be inherited in order to provide <see cref="Feature"/>s.
    /// Ϊ���ṩ<see cref="Feature"/>�������Ӧ�ñ��̳�
    /// </summary>
    public abstract class FeatureProvider : ITransientDependency
    {
        /// <summary>
        /// Used to set <see cref="Feature"/>s.
        /// ��������<see cref="Feature"/>
        /// </summary>
        /// <param name="context">Feature definition context / ���ܶ���������</param>
        public abstract void SetFeatures(IFeatureDefinitionContext context);
    }
}