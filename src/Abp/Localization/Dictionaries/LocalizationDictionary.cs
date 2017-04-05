using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Represents a simple implementation of <see cref="ILocalizationDictionary"/> interface.
    /// <see cref="ILocalizationDictionary"/>�ӿڼ򵥵�ʵ��
    /// </summary>
    public class LocalizationDictionary : ILocalizationDictionary, IEnumerable<LocalizedString>
    {
        /// <summary>
        /// �ֵ���Ļ�
        /// </summary>
        public CultureInfo CultureInfo { get; private set; }

        /// <summary>
        /// ��ȡ�����ø��ּ����Ƶ��ַ���
        /// </summary>
        /// <param name="name">���ڻ�ȡ�����õ�����</param>
        /// <returns></returns>
        public virtual string this[string name]
        {
            get
            {
                var localizedString = GetOrNull(name);
                return localizedString == null ? null : localizedString.Value;
            }
            set
            {
                _dictionary[name] = new LocalizedString(name, value, CultureInfo);
            }
        }

        private readonly Dictionary<string, LocalizedString> _dictionary;

        /// <summary>
        /// Creates a new <see cref="LocalizationDictionary"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="cultureInfo">Culture of the dictionary / �ֵ���Ļ�</param>
        public LocalizationDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
            _dictionary = new Dictionary<string, LocalizedString>();
        }

        /// <summary>
        /// ��ȡһ���������� <paramref name="name"/>��<see cref="LocalizedString"/> .
        /// </summary>
        /// <param name="name">����</param>
        /// <returns>���ػ��ַ�����������ֵ��в�����ָ�������ƣ�����null</returns>
        public virtual LocalizedString GetOrNull(string name)
        {
            LocalizedString localizedString;
            return _dictionary.TryGetValue(name, out localizedString) ? localizedString : null;
        }

        /// <summary>
        /// ��ȡ�ֵ��еı��ػ��ַ���
        /// </summary>
        /// <returns>���ػ� <see cref="LocalizedString"/> �б����</returns>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return _dictionary.Values.ToImmutableList();
        }

        /// <summary>
        /// ����һ��ѭ�����ʼ��ϵ�ö����
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator<LocalizedString> GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        /// <summary>
        /// ����һ��ѭ�����ʼ��ϵ�ö������
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        /// <param name="name">����</param>
        /// <returns></returns>
        protected bool Contains(string name)
        {
            return _dictionary.ContainsKey(name);
        }
    }
}