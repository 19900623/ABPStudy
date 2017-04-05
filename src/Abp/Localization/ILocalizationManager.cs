using System.Collections.Generic;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// This interface is used to manage localization system.
    /// ���ڹ����ػ�ϵͳ�Ľӿ�
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Gets a localization source with name.
        /// ͨ�����ƻ�ȡһ��localization source
        /// </summary>
        /// <param name="name">Unique name of the localization source / ���ػ���ԴΨһ������</param>
        /// <returns>The localization source / ���ػ�ȡ����localization source</returns>
        ILocalizationSource GetSource(string name);

        /// <summary>
        /// Gets all registered localization sources.
        /// ��ȡ����ע���˵�localization sources.
        /// </summary>
        /// <returns>List of sources / sources�б�</returns>
        IReadOnlyList<ILocalizationSource> GetAllSources();
    }
}