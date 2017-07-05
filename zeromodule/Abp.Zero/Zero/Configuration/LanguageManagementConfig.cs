using System.Linq;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Castle.Core.Logging;

namespace Abp.Zero.Configuration
{
    /// <summary>
    /// ���Թ�������
    /// </summary>
    internal class LanguageManagementConfig : ILanguageManagementConfig
    {
        /// <summary>
        /// ��־����
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// IOC����
        /// </summary>
        private readonly IIocManager _iocManager;
        /// <summary>
        /// ABP��������
        /// </summary>
        private readonly IAbpStartupConfiguration _configuration;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocManager">IOC����</param>
        /// <param name="configuration">ABP��������</param>
        public LanguageManagementConfig(IIocManager iocManager, IAbpStartupConfiguration configuration)
        {
            _iocManager = iocManager;
            _configuration = configuration;

            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// �������ݿⱾ�ػ�
        /// </summary>
        public void EnableDbLocalization()
        {
            _iocManager.Register<ILanguageProvider, ApplicationLanguageProvider>(DependencyLifeStyle.Transient);

            var sources = _configuration
                .Localization
                .Sources
                .Where(s => s is IDictionaryBasedLocalizationSource)
                .Cast<IDictionaryBasedLocalizationSource>()
                .ToList();
            
            foreach (var source in sources)
            {
                _configuration.Localization.Sources.Remove(source);
                _configuration.Localization.Sources.Add(
                    new MultiTenantLocalizationSource(
                        source.Name,
                        new MultiTenantLocalizationDictionaryProvider(
                            source.DictionaryProvider,
                            _iocManager
                            )
                        )
                    );

                Logger.DebugFormat("Converted {0} ({1}) to MultiTenantLocalizationSource", source.Name, source.GetType());
            }
        }
    }
}