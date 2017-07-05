using Abp.Localization.Dictionaries;

namespace Abp.Zero.Configuration
{
    /// <summary>
    /// ���Թ�������
    /// </summary>
    public interface ILanguageManagementConfig
    {
        /// <summary>
        /// Enables the database localization.Replaces all <see cref="IDictionaryBasedLocalizationSource"/> localization sources with database based localization source.
        /// �������ݿⱾ�ػ���ͨ�����ݿ�������ػ�Դ�滻���е�<see cref="IDictionaryBasedLocalizationSource"/>���ػ�Դ
        /// </summary>
        void EnableDbLocalization();
    }
}