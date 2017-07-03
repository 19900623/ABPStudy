using System.Collections.Generic;
using System.Globalization;
using Abp.Localization.Dictionaries;

namespace Abp.Localization
{
    /// <summary>
    /// ���ػ����ֵ�
    /// </summary>
    internal class EmptyDictionary : ILocalizationDictionary
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public CultureInfo CultureInfo { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cultureInfo">������Ϣ</param>
        public EmptyDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }
        /// <summary>
        /// ���ػ��ַ���
        /// </summary>
        /// <param name="name">����</param>
        /// <returns></returns>
        public LocalizedString GetOrNull(string name)
        {
            return null;
        }
        /// <summary>
        /// ��ȡ���б��ػ��ַ���
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return new LocalizedString[0];
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="name">����</param>
        /// <returns></returns>
        public string this[string name]
        {
            get { return null; }
            set { }
        }
    }
}