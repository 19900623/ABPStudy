using Abp.Collections.Extensions;
using Abp.Localization;
using Abp.UI.Inputs;

namespace Abp.Application.Features
{
    /// <summary>
    /// Base for implementing <see cref="IFeatureDefinitionContext"/>.
    /// <see cref="IFeatureDefinitionContext"/>�Ļ���ʵ��
    /// </summary>
    public abstract class FeatureDefinitionContextBase : IFeatureDefinitionContext
    {
        /// <summary>
        /// �����ֵ�
        /// </summary>
        protected readonly FeatureDictionary Features;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDefinitionContextBase"/> class.
        /// ���캯��
        /// </summary>
        protected FeatureDefinitionContextBase()
        {
            Features = new FeatureDictionary();
        }

        /// <summary>
        /// Creates a new feature.
        /// ����һ���¹���
        /// </summary>
        /// <param name="name">Unique name of the feature / ���ܵ�Ψһ����</param>
        /// <param name="defaultValue">Default value / Ĭ��ֵ</param>
        /// <param name="displayName">Display name of the feature / ���ܵ���ʾ����</param>
        /// <param name="description">A brief description for this feature / ���ܵļ�Ҫ����</param>
        /// <param name="scope">Feature scope / ������</param>
        /// <param name="inputType">Input type / ��������</param>
        public Feature Create(string name, string defaultValue, ILocalizableString displayName = null,
            ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null)
        {
            if (Features.ContainsKey(name))
            {
                throw new AbpException("There is already a feature with name: " + name);
            }

            var feature = new Feature(name, defaultValue, displayName, description, scope, inputType);
            Features[feature.Name] = feature;
            return feature;

        }

        /// <summary>
        /// Gets a feature with given name or null if can not find.
        /// ���ݸ������ƻ�ȡ���ܣ����û�ҵ��򷵻�NULL
        /// </summary>
        /// <param name="name">Unique name of the feature / ���ܵ�Ψһ����</param>
        /// <returns>
        /// <see cref="Feature" /> object or null / �������null
        /// </returns>
        public Feature GetOrNull(string name)
        {
            return Features.GetOrDefault(name);
        }
    }
}