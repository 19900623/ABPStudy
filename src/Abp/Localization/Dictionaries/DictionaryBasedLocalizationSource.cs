using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Extensions;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// This class is used to build a localization source which works on memory based dictionaries to find strings.
    /// ���������������һ�����ص���Դ�����ڻ����ڴ���ֵ��ϲ����ַ���
    /// </summary>
    public class DictionaryBasedLocalizationSource : IDictionaryBasedLocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// ��Դ��Ψһ����
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// ���ػ��ֵ��ṩ��
        /// </summary>
        public ILocalizationDictionaryProvider DictionaryProvider { get { return _dictionaryProvider; } }

        /// <summary>
        /// ���ػ�����
        /// </summary>
        protected ILocalizationConfiguration LocalizationConfiguration { get; private set; }

        /// <summary>
        /// ���ػ��ֵ��ṩ��
        /// </summary>
        private readonly ILocalizationDictionaryProvider _dictionaryProvider;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="dictionaryProvider">���ػ��ֵ��ṩ��</param>
        public DictionaryBasedLocalizationSource(string name, ILocalizationDictionaryProvider dictionaryProvider)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }

            Name = name;

            if (dictionaryProvider == null)
            {
                throw new ArgumentNullException("dictionaryProvider");
            }

            _dictionaryProvider = dictionaryProvider;
        }

        /// <summary>
        /// ��ʼ��(��������ڵ�һ��ʹ��֮ǰ��ABP����)
        /// </summary>
        /// <param name="configuration">���ػ�����</param>
        /// <param name="iocResolver">IOC������</param>
        public virtual void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            LocalizationConfiguration = configuration;
            DictionaryProvider.Initialize(Name);
        }

        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="name">����</param>
        /// <returns></returns>
        public string GetString(string name)
        {
            return GetString(name, Thread.CurrentThread.CurrentUICulture);
        }

        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <returns></returns>
        public string GetString(string name, CultureInfo culture)
        {
            var value = GetStringOrNull(name, culture);

            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name, culture);
            }

            return value;
        }

        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="tryDefaults">�Ƿ�Ĭ��</param>
        /// <returns>���û���ҵ��򷵻�null</returns>
        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return GetStringOrNull(name, Thread.CurrentThread.CurrentUICulture, tryDefaults);
        }

        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <param name="tryDefaults">�Ƿ�Ĭ��</param>
        /// <returns>���û���ҵ��򷵻�null</returns>
        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            var cultureName = culture.Name;
            var dictionaries = DictionaryProvider.Dictionaries;

            //Try to get from original dictionary (with country code)
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(cultureName, out originalDictionary))
            {
                var strOriginal = originalDictionary.GetOrNull(name);
                if (strOriginal != null)
                {
                    return strOriginal.Value;
                }
            }

            if (!tryDefaults)
            {
                return null;
            }

            //Try to get from same language dictionary (without country code)
            if (cultureName.Contains("-")) //Example: "tr-TR" (length=5)
            {
                ILocalizationDictionary langDictionary;
                if (dictionaries.TryGetValue(GetBaseCultureName(cultureName), out langDictionary))
                {
                    var strLang = langDictionary.GetOrNull(name);
                    if (strLang != null)
                    {
                        return strLang.Value;
                    }
                }
            }

            //Try to get from default language
            var defaultDictionary = DictionaryProvider.DefaultDictionary;
            if (defaultDictionary == null)
            {
                return null;
            }

            var strDefault = defaultDictionary.GetOrNull(name);
            if (strDefault == null)
            {
                return null;
            }

            return strDefault.Value;
        }

        /// <summary>
        /// ��ȡ��ǰ�����е������ַ���
        /// </summary>
        /// <param name="includeDefaults">���˵�Ĭ�������ı������ǰ�Ļ�û�з��֡�</param>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(Thread.CurrentThread.CurrentUICulture, includeDefaults);
        }

        /// <summary>
        /// ��ȡָ���Ļ��е������ַ���
        /// </summary>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <param name="includeDefaults">���˵�Ĭ�������ı������ǰ�Ļ�û�з��֡�</param>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            //TODO: Can be optimized (example: if it's already default dictionary, skip overriding)

            var dictionaries = DictionaryProvider.Dictionaries;

            //Create a temp dictionary to build
            var allStrings = new Dictionary<string, LocalizedString>();

            if (includeDefaults)
            {
                //Fill all strings from default dictionary
                var defaultDictionary = DictionaryProvider.DefaultDictionary;
                if (defaultDictionary != null)
                {
                    foreach (var defaultDictString in defaultDictionary.GetAllStrings())
                    {
                        allStrings[defaultDictString.Name] = defaultDictString;
                    }
                }

                //Overwrite all strings from the language based on country culture
                if (culture.Name.Contains("-"))
                {
                    ILocalizationDictionary langDictionary;
                    if (dictionaries.TryGetValue(GetBaseCultureName(culture.Name), out langDictionary))
                    {
                        foreach (var langString in langDictionary.GetAllStrings())
                        {
                            allStrings[langString.Name] = langString;
                        }
                    }
                }
            }

            //Overwrite all strings from the original dictionary
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(culture.Name, out originalDictionary))
            {
                foreach (var originalLangString in originalDictionary.GetAllStrings())
                {
                    allStrings[originalLangString.Name] = originalLangString;
                }
            }

            return allStrings.Values.ToImmutableList();
        }

        /// <summary>
        /// Extends the source with given dictionary.
        /// Ϊ�������ֵ���չԴ
        /// </summary>
        /// <param name="dictionary">Dictionary to extend the source / ��չԴ���ֵ�</param>
        public virtual void Extend(ILocalizationDictionary dictionary)
        {
            DictionaryProvider.Extend(dictionary);
        }

        /// <summary>
        /// ���ظ��������ƻ��׳��쳣
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="culture">�Ļ���Ϣ</param>
        /// <returns></returns>
        protected virtual string ReturnGivenNameOrThrowException(string name, CultureInfo culture)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(LocalizationConfiguration, Name, name, culture);
        }

        /// <summary>
        /// ��ȡ�����Ļ�����
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        private static string GetBaseCultureName(string cultureName)
        {
            return cultureName.Contains("-")
                ? cultureName.Left(cultureName.IndexOf("-", StringComparison.InvariantCulture))
                : cultureName;
        }
    }
}
