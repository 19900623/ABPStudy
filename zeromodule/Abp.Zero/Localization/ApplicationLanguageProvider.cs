using System.Collections.Generic;
using System.Linq;
using Abp.Runtime.Session;
using Abp.Threading;

namespace Abp.Localization
{
    /// <summary>
    /// Implements <see cref="ILanguageProvider"/> to get languages from <see cref="IApplicationLanguageManager"/>.
    /// <see cref="ILanguageProvider"/>��ʵ�֣���<see cref="IApplicationLanguageManager"/>��ȡ����
    /// </summary>
    public class ApplicationLanguageProvider : ILanguageProvider
    {
        /// <summary>
        /// ABP Session������
        /// </summary>
        public IAbpSession AbpSession { get; set; }
        /// <summary>
        /// Ӧ�ó������Թ���
        /// </summary>
        private readonly IApplicationLanguageManager _applicationLanguageManager;

        /// <summary>
        /// ���캯��.
        /// </summary>
        public ApplicationLanguageProvider(IApplicationLanguageManager applicationLanguageManager)
        {
            _applicationLanguageManager = applicationLanguageManager;

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// Ϊ��ǰ�̻���ȡĬ������.
        /// </summary>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            var languageInfos = AsyncHelper.RunSync(() => _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                    .OrderBy(l => l.DisplayName)
                    .Select(l => l.ToLanguageInfo())
                    .ToList();

            SetDefaultLanguage(languageInfos);

            return languageInfos;
        }
        /// <summary>
        /// Ϊ��ǰ�̻�����Ĭ������
        /// </summary>
        /// <param name="languageInfos"></param>
        private void SetDefaultLanguage(List<LanguageInfo> languageInfos)
        {
            if (languageInfos.Count <= 0)
            {
                return;
            }

            var defaultLanguage = AsyncHelper.RunSync(() => _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId));
            if (defaultLanguage == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }
            
            var languageInfo = languageInfos.FirstOrDefault(l => l.Name == defaultLanguage.Name);
            if (languageInfo == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }

            languageInfo.IsDefault = true;
        }
    }
}