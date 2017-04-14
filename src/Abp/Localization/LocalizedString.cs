using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a localized string.
    /// ��ʾһ�����ػ��ַ���
    /// </summary>
    public class LocalizedString
    {
        /// <summary>
        /// Culture info for this string.
        /// ���ַ���������
        /// </summary>
        public CultureInfo CultureInfo { get; internal set; }

        /// <summary>
        /// Unique Name of the string.
        /// �ַ�����Ψһ����
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Value for the <see cref="Name"/>.
        /// ���� <see cref="Name"/>��Ӧ�ı��ػ��ַ���ֵ.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates a localized string instance.
        /// ����һ�����ػ��ַ���ʵ��
        /// </summary>
        /// <param name="cultureInfo">Culture info for this string / ���ַ���������</param>
        /// <param name="name">Unique Name of the string / �ַ�����Ψһ����</param>
        /// <param name="value">Value for the <paramref name="name"/> / ���� <see cref="Name"/>��Ӧ�ı��ػ��ַ���ֵ.</param>
        public LocalizedString(string name, string value, CultureInfo cultureInfo)
        {
            Name = name;
            Value = value;
            CultureInfo = cultureInfo;
        }
    }
}