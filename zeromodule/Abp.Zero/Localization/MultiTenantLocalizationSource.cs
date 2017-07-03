using System;
using System.Globalization;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Localization.Dictionaries;
using Castle.Core.Logging;

namespace Abp.Localization
{
    /// <summary>
    /// ���̻����ػ�Դ
    /// </summary>
    public class MultiTenantLocalizationSource : DictionaryBasedLocalizationSource, IMultiTenantLocalizationSource
    {
        /// <summary>
        /// ���̻����ػ��ֵ��ṩ��
        /// </summary>
        public new MultiTenantLocalizationDictionaryProvider DictionaryProvider
        {
            get { return base.DictionaryProvider.As<MultiTenantLocalizationDictionaryProvider>(); }
        }
        /// <summary>
        /// ��־��¼������
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="dictionaryProvider">���̻����ػ��ֵ��ṩ��</param>
        public MultiTenantLocalizationSource(string name, MultiTenantLocalizationDictionaryProvider dictionaryProvider) 
            : base(name, dictionaryProvider)
        {
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="configuration">���ػ����ö���</param>
        /// <param name="iocResolver">IOC������</param>
        public override void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            base.Initialize(configuration, iocResolver);

            if (Logger is NullLogger && iocResolver.IsRegistered(typeof(ILoggerFactory)))
            {
                Logger = iocResolver.Resolve<ILoggerFactory>().Create(typeof (MultiTenantLocalizationSource));
            }
        }
        /// <summary>
        /// ��ȡ�ַ���
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="name">����</param>
        /// <param name="culture">������Ϣ</param>
        /// <returns></returns>
        public string GetString(int? tenantId, string name, CultureInfo culture)
        {
            var value = GetStringOrNull(tenantId, name, culture);

            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name, culture);
            }

            return value;
        }
        /// <summary>
        /// ��ȡ�ַ�����Null
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="name">����</param>
        /// <param name="culture">������Ϣ</param>
        /// <param name="tryDefaults">����Ĭ��</param>
        /// <returns></returns>
        public string GetStringOrNull(int? tenantId, string name, CultureInfo culture, bool tryDefaults = true)
        {
            var cultureName = culture.Name;
            var dictionaries = DictionaryProvider.Dictionaries;

            //Try to get from original dictionary (with country code)
            ILocalizationDictionary originalDictionary;
            if (dictionaries.TryGetValue(cultureName, out originalDictionary))
            {
                var strOriginal = originalDictionary
                    .As<IMultiTenantLocalizationDictionary>()
                    .GetOrNull(tenantId, name);

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
                    var strLang = langDictionary.As<IMultiTenantLocalizationDictionary>().GetOrNull(tenantId, name);
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

            var strDefault = defaultDictionary.As<IMultiTenantLocalizationDictionary>().GetOrNull(tenantId, name);
            if (strDefault == null)
            {
                return null;
            }

            return strDefault.Value;
        }
        /// <summary>
        /// ��չ
        /// </summary>
        /// <param name="dictionary">���ػ��ֵ�</param>
        public override void Extend(ILocalizationDictionary dictionary)
        {
            DictionaryProvider.Extend(dictionary);
        }
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <param name="cultureName">��������</param>
        /// <returns></returns>
        private static string GetBaseCultureName(string cultureName)
        {
            return cultureName.Contains("-")
                ? cultureName.Left(cultureName.IndexOf("-", StringComparison.InvariantCulture))
                : cultureName;
        }
    }
}