using Abp.Dependency;

namespace Abp.Localization
{
    /// <summary>
    /// Implements <see cref="ILocalizationContext"/>.
    /// <see cref="ILocalizationContext"/>��ʵ��
    /// </summary>
    public class LocalizationContext : ILocalizationContext, ISingletonDependency
    {
        /// <summary>
        /// ���ػ�������
        /// </summary>
        public ILocalizationManager LocalizationManager { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationContext"/> class.
        /// ���캯��
        /// </summary>
        /// <param name="localizationManager">The localization manager. / ���ػ�������</param>
        public LocalizationContext(ILocalizationManager localizationManager)
        {
            LocalizationManager = localizationManager;
        }
    }
}