using System.Collections.Generic;
using System.Globalization;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Represents a dictionary that is used to find a localized string.
    /// ��ʾ���ڲ��ұ��ػ��ַ������ֵ�
    /// </summary>
    public interface ILocalizationDictionary
    {
        /// <summary>
        /// Culture of the dictionary.
        /// �ֵ������
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// Gets/sets a string for this dictionary with given name (key).
        /// ��ȡ�����ø��ּ����Ƶ��ַ���
        /// </summary>
        /// <param name="name">Name to get/set / ���ڻ�ȡ�����õ�����</param>
        string this[string name] { get; set; }

        /// <summary>
        /// Gets a <see cref="LocalizedString"/> for given <paramref name="name"/>.
        /// ��ȡһ���������� <paramref name="name"/>��<see cref="LocalizedString"/> .
        /// </summary>
        /// <param name="name">Name (key) to get localized string / ����</param>
        /// <returns>The localized string or null if not found in this dictionary / ���ػ��ַ�����������ֵ��в�����ָ�������ƣ�����null</returns>
        LocalizedString GetOrNull(string name);

        /// <summary>
        /// Gets a list of all strings in this dictionary.
        /// ��ȡ�ֵ��еı��ػ��ַ���
        /// </summary>
        /// <returns>List of all <see cref="LocalizedString"/> object / ���ػ� <see cref="LocalizedString"/> �б����</returns>
        IReadOnlyList<LocalizedString> GetAllStrings();
    }
}