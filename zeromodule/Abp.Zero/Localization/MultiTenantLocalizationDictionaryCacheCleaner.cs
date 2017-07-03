using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;

namespace Abp.Localization
{
    /// <summary>
    /// Clears related localization cache when a <see cref="ApplicationLanguageText"/> changes.
    /// ��<see cref="ApplicationLanguageText"/>�޸�ʱ�����صı��ػ�����
    /// </summary>
    public class MultiTenantLocalizationDictionaryCacheCleaner : 
        ITransientDependency,
        IEventHandler<EntityChangedEventData<ApplicationLanguageText>>
    {
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        public MultiTenantLocalizationDictionaryCacheCleaner(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// ����<see cref="ApplicationLanguageText"/>�޸��¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<ApplicationLanguageText> eventData)
        {
            _cacheManager
                .GetMultiTenantLocalizationDictionaryCache()
                .Remove(MultiTenantLocalizationDictionaryCacheHelper.CalculateCacheKey(
                    eventData.Entity.TenantId,
                    eventData.Entity.Source,
                    eventData.Entity.LanguageName)
                );
        }
    }
}