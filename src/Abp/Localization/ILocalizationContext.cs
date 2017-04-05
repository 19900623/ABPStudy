namespace Abp.Localization
{
    /// <summary>
    /// Localization context.
    /// ���ػ�������
    /// </summary>
    public interface ILocalizationContext
    {
        /// <summary>
        /// Gets the localization manager.
        /// ��ȡ���ػ�������
        /// </summary>
        ILocalizationManager LocalizationManager { get; }
    }
}