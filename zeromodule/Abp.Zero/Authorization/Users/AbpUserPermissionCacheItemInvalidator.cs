using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// ABP���û�Ȩ�޻�����
    /// </summary>
    public class AbpUserPermissionCacheItemInvalidator :
        IEventHandler<EntityChangedEventData<UserPermissionSetting>>,
        IEventHandler<EntityChangedEventData<UserRole>>,
        IEventHandler<EntityDeletedEventData<AbpUserBase>>,

        ITransientDependency
    {
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cacheManager">�����������</param>
        public AbpUserPermissionCacheItemInvalidator(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// �����û�Ȩ�������޸��¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityChangedEventData<UserPermissionSetting> eventData)
        {
            var cacheKey = eventData.Entity.UserId + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetUserPermissionCache().Remove(cacheKey);
        }
        /// <summary>
        /// �����û���ɫ�޸��¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityChangedEventData<UserRole> eventData)
        {
            var cacheKey = eventData.Entity.UserId + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetUserPermissionCache().Remove(cacheKey);
        }
        /// <summary>
        /// �����û�ɾ���¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityDeletedEventData<AbpUserBase> eventData)
        {
            var cacheKey = eventData.Entity.Id + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetUserPermissionCache().Remove(cacheKey);
        }
    }
}