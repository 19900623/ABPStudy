using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// ���Թ�����
    /// </summary>
    public class LanguageManager : ILanguageManager, ITransientDependency
    {
        /// <summary>
        /// �Ļ���Ϣ
        /// </summary>
        public LanguageInfo CurrentLanguage { get { return GetCurrentLanguage(); } }

        /// <summary>
        /// �����ṩ��
        /// </summary>
        private readonly ILanguageProvider _languageProvider;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="languageProvider">�����ṩ��</param>
        public LanguageManager(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _languageProvider.GetLanguages();
        }

        /// <summary>
        /// ��ȡ��ǰ������Ϣ
        /// </summary>
        /// <returns></returns>
        private LanguageInfo GetCurrentLanguage()
        {
            var languages = _languageProvider.GetLanguages();
            if (languages.Count <= 0)
            {
                throw new AbpException("No language defined in this application.");
            }

            var currentCultureName = Thread.CurrentThread.CurrentUICulture.Name;

            //Try to find exact match
            var currentLanguage = languages.FirstOrDefault(l => l.Name == currentCultureName);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find best match
            currentLanguage = languages.FirstOrDefault(l => currentCultureName.StartsWith(l.Name));
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Try to find default language
            currentLanguage = languages.FirstOrDefault(l => l.IsDefault);
            if (currentLanguage != null)
            {
                return currentLanguage;
            }

            //Get first one
            return languages[0];
        }
    }
}