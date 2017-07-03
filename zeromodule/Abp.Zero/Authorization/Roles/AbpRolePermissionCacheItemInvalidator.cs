using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;

namespace Abp.Authorization.Roles
{
    /// <summary>
    /// ABP ��ɫȨ�޻��������� ����
    /// </summary>
    public class AbpRolePermissionCacheItemInvalidator :
        IEventHandler<EntityChangedEventData<RolePermissionSetting>>,
        IEventHandler<EntityDeletedEventData<AbpRoleBase>>,
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
        public AbpRolePermissionCacheItemInvalidator(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// �����ɫȨ�������޸��¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityChangedEventData<RolePermissionSetting> eventData)
        {
            var cacheKey = eventData.Entity.RoleId + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetRolePermissionCache().Remove(cacheKey);
        }
        /// <summary>
        /// �����ɫɾ���¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityDeletedEventData<AbpRoleBase> eventData)
        {
            var cacheKey = eventData.Entity.Id + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetRolePermissionCache().Remove(cacheKey);
        }
    }
}