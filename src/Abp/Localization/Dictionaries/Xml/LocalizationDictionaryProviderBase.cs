using System.Collections.Generic;

namespace Abp.Localization.Dictionaries.Xml
{
    /// <summary>
    /// �����ֵ��ṩ�߻���
    /// </summary>
    public abstract class LocalizationDictionaryProviderBase : ILocalizationDictionaryProvider
    {
        /// <summary>
        /// Դ����
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// ���ػ��ֵ�
        /// </summary>
        public ILocalizationDictionary DefaultDictionary { get; protected set; }

        public IDictionary<string, ILocalizationDictionary> Dictionaries { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        protected LocalizationDictionaryProviderBase()
        {
            Dictionaries = new Dictionary<string, ILocalizationDictionary>();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sourceName"></param>
        public virtual void Initialize(string sourceName)
        {
            SourceName = sourceName;
        }

        /// <summary>
        /// ��չ
        /// </summary>
        /// <param name="dictionary">�����ֵ�</param>
        public void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            ILocalizationDictionary existingDictionary;
            if (!Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                Dictionaries[dictionary.CultureInfo.Name] = dictionary;
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