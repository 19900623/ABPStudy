using System;
using System.Globalization;
using Abp.Dependency;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// This static class is used to simplify getting localized strings.
    /// �����̬�����ڼ򻯻�ȡ���ػ��ַ���
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// Gets a reference to the localization manager.Inject and use <see cref="ILocalizationManager"/> wherever it's possible, instead of this shortcut.
        /// ���ػ������������ã�ʹ��<see cref="ILocalizationManager"/>ע��
        /// </summary>
        public static ILocalizationManager Manager { get { return LocalizationManager.Value; } }

        private static readonly Lazy<ILocalizationManager> LocalizationManager;

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static LocalizationHelper()
        {
            LocalizationManager = new Lazy<ILocalizationManager>(
                () => IocManager.Instance.IsRegistered<ILocalizationManager>()
                    ? IocManager.Instance.Resolve<ILocalizationManager>()
                    : NullLocalizationManager.Instance
                );
        }

        /// <summary>
        /// Gets a pre-registered localization source.
        /// ��ȡһ��Ԥ��ע���localization source.
        /// </summary>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.Value.GetSource(name);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// �ڵ�ǰ�����л�ȡһ�����ػ����ַ���
        /// </summary>
        /// <param name="sourceName">Name of the localization source / localization source������</param>
        /// <param name="name">Key name to get localized string / ��ȡ���ػ��ַ���key����</param>
        /// <returns>Localized strin / ���ػ��ַ���</returns>
        public static string GetString(string sourceName, string name)
        {
            return LocalizationManager.Value.GetString(sourceName, name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// ���ض����������»�ȡ���ػ��ַ���
        /// </summary>
        /// <param name="sourceName">Name of the localization source / localization source������</param>
        /// <param name="name">Key name to get localized string / ��ȡ���ػ��ַ���key����</param>
        /// <param name="culture">culture / ����</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture);
        }
    }
}