using System;
using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// Represents a string that can be localized.
    /// ��ʾһ������Ҫʱ�ܱ��ػ����ַ���
    /// </summary>
    [Serializable]
    public class LocalizableString : ILocalizableString
    {
        /// <summary>
        /// Unique name of the localization source.
        /// ���ػ�Դ����
        /// </summary>
        public virtual string SourceName { get; private set; }

        /// <summary>
        /// Unique Name of the string to be localized.
        /// �����ػ����ַ���������
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// Ϊ�����л�
        /// </summary>
        private LocalizableString()
        {
            
        }

        /// <param name="name">Unique Name of the string to be localized / ���ػ�Դ����</param>
        /// <param name="sourceName">Unique name of the localization source / �������ػ����ַ�������</param>
        public LocalizableString(string name, string sourceName)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (sourceName == null)
            {
                throw new ArgumentNullException("sourceName");
            }

            Name = name;
            SourceName = sourceName;
        }

        /// <summary>
        /// ʹ�õ�ǰ���Ա��ػ��ַ���
        /// </summary>
        /// <returns>���ػ�����ַ���</returns>
        public string Localize(ILocalizationContext context)
        {
            return context.LocalizationManager.GetString(SourceName, Name);
        }

        /// <summary>
        /// ʹ�õ�ǰ���Ա��ػ��ַ���
        /// </summary>
        /// <param name="culture">����</param>
        /// <returns>���ػ�����ַ���</returns>
        public string Localize(ILocalizationContext context, CultureInfo culture)
        {
            return context.LocalizationManager.GetString(SourceName, Name, culture);
        }

        public override string ToString()
        {
            return string.Format("[LocalizableString: {0}, {1}]", Name, SourceName);
        }
    }
}
