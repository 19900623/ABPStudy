using System.Collections.Generic;

namespace Abp.Localization
{
    /// <summary>
    /// �����ṩ��
    /// </summary>
    public interface ILanguageProvider
    {
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}