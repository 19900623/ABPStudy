using System.Collections.Generic;
using System.Globalization;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization.Sources
{
    /// <summary>
    /// Null object pattern for <see cref="ILocalizationSource"/>.
    /// <see cref="ILocalizationSource"/>��NULl����ģʽʵ��
    /// </summary>
    public class NullLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Singleton instance.
        /// ����
        /// </summary>
        public static NullLocalizationSource Instance { get { return SingletonInstance; } }
        private static readonly NullLocalizationSource SingletonInstance = new NullLocalizationSource();

        /// <summary>
        /// Ψһ������
        /// </summary>
        public string Name { get { return null; } }

        /// <summary>
        /// ���ػ��ַ�������
        /// </summary>
        private readonly IReadOnlyList<LocalizedString> _emptyStringArray = new LocalizedString[0];

        /// <summary>
        /// ���캯��
        /// </summary>
        private NullLocalizationSource()
        {
            
        }

        /// <summary>
        /// �÷�����abp��һ��ʹ��ǰ����
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="iocResolver"></param>
        public void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            
        }

        /// <summary>
        ///��ȡ�������Ƶĵ�ǰ���Ա�ʾ���ַ���
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>���ػ��ַ���</returns>
        public string GetString(string name)
        {
            return name;
        }

        /// <summary>
        //��ȡ�������ƺ�����ı��ػ��ַ���
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="culture">������Ϣ</param>
        /// <returns>���ػ��ַ���</returns>
        public string GetString(string name, CultureInfo culture)
        {
            return name;
        }

        /// <summary>
        /// ��ȡ��ǰ�����и������Ƶı��ػ��ַ���������Ҳ�������NULL��
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="tryDefaults">true:���˵�Ĭ����������ڵ�ǰ����û�з���</param>
        /// <returns>���ػ��ַ���</returns>
        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            return null;
        }

        /// <summary>
        /// ��ȡ��ǰ�����и������Ƶı��ػ��ַ���������Ҳ�������NULL��
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="culture">������Ϣ</param>
        /// <param name="tryDefaults">true:���˵�Ĭ����������ڵ�ǰ����û�з���</param>
        /// <returns>���ػ��ַ���</returns>
        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            return null;
        }

        /// <summary>
        /// ��ȡ���б��ػ��ַ���
        /// </summary>
        /// <param name="includeDefaults">true:���˵�Ĭ�������ı������ǰ����û�з��֡�</param>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return _emptyStringArray;
        }

        /// <summary>
        /// ��ȡ���б��ػ��ַ���
        /// </summary>
        /// <param name="culture">������Ϣ</param>
        /// <param name="includeDefaults">true:���˵�Ĭ�������ı������ǰ����û�з��֡�</param>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            return _emptyStringArray;
        }
    }
}