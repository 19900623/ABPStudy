using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// This class handles related events and invalidated tenant feature cache items if needed.
    /// �����Ҫ�����ദ������¼�����Ч���⻧���������
    /// </summary>
    public class TenantFeatureCacheItemInvalidator :
        IEventHandler<EntityChangedEventData<TenantFeatureSetting>>,
        ITransientDependency
    {
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        public TenantFeatureCacheItemInvalidator(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// �����̻����������޸��¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EntityChangedEventData<TenantFeatureSetting> eventData)
        {
            _cacheManager.GetTenantFeatureCache().Remove(eventData.Entity.TenantId);
        }
    }
}