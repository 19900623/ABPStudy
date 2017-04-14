using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a string that can be localized when needed.
    /// ��ʾһ������Ҫʱ�ܱ��ػ����ַ���
    /// </summary>
    public interface ILocalizableString
    {
        /// <summary>
        /// Localizes the string in current culture.
        /// �ڵ�ǰ������ַ���
        /// </summary>
        /// <param name="context">Localization context / ���ػ�������</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        string Localize(ILocalizationContext context);

        /// <summary>
        /// Localizes the string in given culture.
        /// ��������ı��ػ��ַ���
        /// </summary>
        /// <param name="context">Localization context / ���ػ�������</param>
        /// <param name="culture">culture / ������Ϣ</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        string Localize(ILocalizationContext context, CultureInfo culture);
    }
}