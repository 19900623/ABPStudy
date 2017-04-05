using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// ���Թ�����
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        LanguageInfo CurrentLanguage { get; }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}