using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Collections.Extensions;

namespace Abp.Localization
{
    /// <summary>
    /// Extends <see cref="ILocalizationDictionaryProvider"/> to add tenant and database based localization.
    /// <see cref="ILocalizationDictionaryProvider"/>����չ��������̻��ͻ������ݿ�ı��ػ�
    /// </summary>
    public class MultiTenantLocalizationDictionaryProvider : ILocalizationDictionaryProvider
    {
        /// <summary>
        /// ���ػ��ַ�����Ĭ���ֵ�
        /// </summary>
        public ILocalizationDictionary DefaultDictionary
        {
            get { return GetDefaultDictionary(); }
        }
        /// <summary>
        /// ���ػ��ֵ伯��
        /// </summary>
        public IDictionary<string, ILocalizationDictionary> Dictionaries
        {
            get { return GetDictionaries(); }
        }
        /// <summary>
        /// ��ǰ���ػ��ֵ伯��
        /// </summary>
        private readonly ConcurrentDictionary<string, ILocalizationDictionary> _dictionaries;
        /// <summary>
        /// Դ����
        /// </summary>
        private string _sourceName;
        /// <summary>
        /// ���ػ��ֵ��ڲ��ṩ��
        /// </summary>
        private readonly ILocalizationDictionaryProvider _internalProvider;
        /// <summary>
        /// IOC��������
        /// </summary>
        private readonly IIocManager _iocManager;
        /// <summary>
        /// ���Թ�������
        /// </summary>
        private ILanguageManager _languageManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        public MultiTenantLocalizationDictionaryProvider(ILocalizationDictionaryProvider internalProvider, IIocManager iocManager)
        {
            _internalProvider = internalProvider;
            _iocManager = iocManager;
            _dictionaries = new ConcurrentDictionary<string, ILocalizationDictionary>();
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sourceName">Դ����</param>
        public void Initialize(string sourceName)
        {
            _sourceName = sourceName;
            _languageManager = _iocManager.Resolve<ILanguageManager>();
            _internalProvider.Initialize(_sourceName);
        }
        /// <summary>
        /// ��ȡ�ֵ伯��
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, ILocalizationDictionary> GetDictionaries()
        {
            var languages = _languageManager.GetLanguages();

            foreach (var language in languages)
            {
                _dictionaries.GetOrAdd(language.Name, s => CreateLocalizationDictionary(language));
            }

            return _dictionaries;
        }
        /// <summary>
        /// ��ȡĬ���ֵ�
        /// </summary>
        /// <returns></returns>
        protected virtual ILocalizationDictionary GetDefaultDictionary()
        {
            var languages = _languageManager.GetLanguages();
            if (!languages.Any())
            {
                throw new ApplicationException("No language defined!");
            }

            var defaultLanguage = languages.FirstOrDefault(l => l.IsDefault);
            if (defaultLanguage == null)
            {
                throw new ApplicationException("Default language is not defined!");
            }

            return _dictionaries.GetOrAdd(defaultLanguage.Name, s => CreateLocalizationDictionary(defaultLanguage));
        }
        /// <summary>
        /// �������ػ��ֵ�
        /// </summary>
        /// <param name="language">������Ϣ</param>
        /// <returns></returns>
        protected virtual IMultiTenantLocalizationDictionary CreateLocalizationDictionary(LanguageInfo language)
        {
            var internalDictionary =
                _internalProvider.Dictionaries.GetOrDefault(language.Name) ??
                new EmptyDictionary(CultureInfo.GetCultureInfo(language.Name));

            var dictionary =  _iocManager.Resolve<IMultiTenantLocalizationDictionary>(new
            {
                sourceName = _sourceName,
                internalDictionary = internalDictionary
            });

            return dictionary;
        }
        /// <summary>
        /// ��չ
        /// </summary>
        /// <param name="dictionary">���ػ��ֵ����</param>
        public virtual void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            ILocalizationDictionary existingDictionary;
            if (!_internalProvider.Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                _internalProvider.Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }
}