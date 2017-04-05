using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// Ĭ�������ṩ��
    /// </summary>
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        /// <summary>
        /// ���ػ�����
        /// </summary>
        private readonly ILocalizationConfiguration _configuration;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration">���ػ�����</param>
        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>������Ϣ�б�</returns>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}