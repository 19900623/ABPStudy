using System.Collections.Generic;
using System.Globalization;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization.Sources
{
    /// <summary>
    /// A Localization Source is used to obtain localized strings.
    /// һ��Localization Source�����ڻ�ȡ���ػ��ַ���
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// Ψһ������
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This method is called by ABP before first usage.
        /// �÷�����abp��һ��ʹ��ǰ����
        /// </summary>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        /// Gets localized string for given name in current language.Fallbacks to default language if not found in current culture.
        /// ��ȡ�������Ƶĵ�ǰ���Ա�ʾ���ַ��������û�е�ǰ��������Ϣ�򷵻�Ĭ������
        /// </summary>
        /// <param name="name">Key name / ������</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        string GetString(string name);

        /// <summary>
        /// Gets localized string for given name and specified culture.Fallbacks to default language if not found in given culture.
        /// ��ȡ�������ƺ�����ı��ػ��ַ��������û�е�ǰ��������Ϣ�򷵻�Ĭ������
        /// </summary>
        /// <param name="name">Key name / ������</param>
        /// <param name="culture">culture information / ������Ϣ</param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        /// Gets localized string for given name in current language.Returns null if not found.
        /// ��ȡ��ǰ�����и������Ƶı��ػ��ַ���������Ҳ�������NULL��
        /// </summary>
        /// <param name="name">Key name / ������</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// true:���˵�Ĭ����������ڵ�ǰ����û�з���
        /// </param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// Gets localized string for given name and specified culture.Returns null if not found.
        /// ��ȡ��ǰ�����и������Ƶı��ػ��ַ���������Ҳ�������NULL��
        /// </summary>
        /// <param name="name">Key name / ������</param>
        /// <param name="culture">culture information / ������Ϣ</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// true:���˵�Ĭ����������ڵ�ǰ����û�з���
        /// </param>
        /// <returns>Localized string / ���ػ��ַ���</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets all strings in current language.
        /// ��ȡ���б��ػ��ַ���
        /// </summary>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// ���˵�Ĭ�������ı������ǰ����û�з��֡�
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        /// Gets all strings in specified culture.
        /// ��ȡ���б��ػ��ַ���
        /// </summary>
        /// <param name="culture">culture information / ������Ϣ</param>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// ���˵�Ĭ�������ı������ǰ����û�з���
        /// </param>
        IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}