using System.Collections.Generic;
using System.Threading;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// ���ػ���������null����ʵ��
    /// </summary>
    public class NullLocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// Singleton instance.
        /// ����
        /// </summary>
        public static NullLocalizationManager Instance { get { return SingletonInstance; } }
        private static readonly NullLocalizationManager SingletonInstance = new NullLocalizationManager();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public LanguageInfo CurrentLanguage { get { return new LanguageInfo(Thread.CurrentThread.CurrentUICulture.Name, Thread.CurrentThread.CurrentUICulture.DisplayName); } }

        /// <summary>
        /// ����������
        /// </summary>
        private readonly IReadOnlyList<LanguageInfo> _emptyLanguageArray = new LanguageInfo[0];

        /// <summary>
        /// �ձ��ػ�Դ����
        /// </summary>
        private readonly IReadOnlyList<ILocalizationSource> _emptyLocalizationSourceArray = new ILocalizationSource[0];

        /// <summary>
        /// ���캯��
        /// </summary>
        private NullLocalizationManager()
        {
            
        }

        /// <summary>
        /// ��ȡ���е�����
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _emptyLanguageArray;
        }

        /// <summary>
        /// ��ȡ���ػ�Դ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILocalizationSource GetSource(string name)
        {
            return NullLocalizationSource.Instance;
        }

        /// <summary>
        /// ��ȡ���еı��ػ�Դ
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _emptyLocalizationSourceArray;
        }
    }
}