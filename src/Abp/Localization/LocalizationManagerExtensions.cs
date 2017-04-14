using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// <see cref="LocalizationManager"/>����չ
    /// </summary>
    public static class LocalizationManagerExtensions
    {
        /// <summary>
        /// Gets a localized string in current language.
        /// ��ȡ��ǰ�����еı��ػ��ַ���
        /// </summary>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        public static string GetString(this ILocalizationManager localizationManager, LocalizableString localizableString)
        {
            return localizationManager.GetString(localizableString.SourceName, localizableString.Name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// ���ض������л�ȡ���ػ��ַ���
        /// </summary>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        public static string GetString(this ILocalizationManager localizationManager, LocalizableString localizableString, CultureInfo culture)
        {
            return localizationManager.GetString(localizableString.SourceName, localizableString.Name, culture);
        }

        /// <summary>
        /// Gets a localized string in current language.
        /// ��ȡ��ǰ�����еı��ػ��ַ���
        /// </summary>
        /// <param name="localizationManager">Localization manager instance / ���ػ���������ʵ��</param>
        /// <param name="sourceName">Name of the localization source / ���ػ�Դ������</param>
        /// <param name="name">Key name to get localized string / ��ȡ���ػ��ַ�����key����</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        public static string GetString(this ILocalizationManager localizationManager, string sourceName, string name)
        {
            return localizationManager.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// Gets a localized string in specified language.
        /// ���ض������л�ȡ���ػ��ַ���
        /// </summary>
        /// <param name="localizationManager">Localization manager instance / ���ػ���������ʵ��</param>
        /// <param name="sourceName">Name of the localization source / ���ػ�Դ������</param>
        /// <param name="name">Key name to get localized string / ��ȡ���ػ��ַ�����key����</param>
        /// <param name="culture">culture / ������Ϣ</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        public static string GetString(this ILocalizationManager localizationManager, string sourceName, string name, CultureInfo culture)
        {
            return localizationManager.GetSource(sourceName).GetString(name, culture);
        }
    }
}