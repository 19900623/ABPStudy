using System;
using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// A class that gets the same string on every localization.
    /// ��ÿ�����ػ����ܻ�ȡ��ͬ���ַ���
    /// </summary>
    [Serializable]
    public class FixedLocalizableString : ILocalizableString
    {
        /// <summary>
        /// The fixed string.Whenever Localize methods called, this string is returned.
        /// �̶����ַ��������ܱ��ػ������Ƿ���ã������ش��ַ���
        /// </summary>
        public virtual string FixedString { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// Ϊ�����л�
        /// </summary>
        private FixedLocalizableString()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="FixedLocalizableString"/>.
        /// ���캯��
        /// </summary>
        /// <param name="fixedString">
        /// The fixed string.Whenever Localize methods called, this string is returned.
        /// �̶����ַ��������ܱ��ػ������Ƿ���ã������ش��ַ���
        /// </param>
        public FixedLocalizableString(string fixedString)
        {
            FixedString = fixedString;
        }

        /// <summary>
        /// ���ǻ�ȡ <see cref="FixedString"/> �ַ���.
        /// </summary>
        /// <param name="context">���ػ�������</param>
        /// <returns></returns>
        public string Localize(ILocalizationContext context)
        {
            return FixedString;
        }

        /// <summary>
        /// ���ǻ�ȡ <see cref="FixedString"/> �ַ���.
        /// </summary>
        public string Localize(ILocalizationContext context, CultureInfo culture)
        {
            return FixedString;
        }

        public override string ToString()
        {
            return FixedString;
        }
    }
}