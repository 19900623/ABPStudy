using System;
using Abp.Dependency;
using Abp.Domain.Uow;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// ���ڴ���ʵ��ı��¼�
    /// </summary>
    public class EntityChangeEventHelper : ITransientDependency, IEntityChangeEventHelper
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// ������Ԫ������
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="unitOfWorkManager">������Ԫ������</param>
        public EntityChangeEventHelper(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// ����ʵ�����ڴ����¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityCreatingEvent(object entity)
        {
            TriggerEventWithEntity(typeof(EntityCreatingEventData<>), entity, true);
        }

        /// <summary>
        /// ��������Ԫ���ʱ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
            TriggerEventWithEntity(typeof(EntityCreatedEventData<>), entity, false);
        }

        /// <summary>
        /// ����ʵ�����ڸ����¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityUpdatingEvent(object entity)
        {
            TriggerEventWithEntity(typeof(EntityUpdatingEventData<>), entity, true);
        }

        /// <summary>
        /// ��������Ԫ���ʱ����ʵ������¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
            TriggerEventWithEntity(typeof(EntityUpdatedEventData<>), entity, false);
        }

        /// <summary>
        /// ����ʵ������ɾ���¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityDeletingEvent(object entity)
        {
            TriggerEventWithEntity(typeof(EntityDeletingEventData<>), entity, true);
        }

        /// <summary>
        /// ��������Ԫ���ʱ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
            TriggerEventWithEntity(typeof(EntityDeletedEventData<>), entity, false);
        }

        /// <summary>
        /// ����ʵ���¼�
        /// </summary>
        /// <param name="genericEventType">�����¼�����</param>
        /// <param name="entity">ʵ��</param>
        /// <param name="triggerImmediately">�Ƿ���������</param>
        private void TriggerEventWithEntity(Type genericEventType, object entity, bool triggerImmediately)
        {
            var entityType = entity.GetType();
            var eventType = genericEventType.MakeGenericType(entityType);

            if (triggerImmediately || _unitOfWorkManager.Current == null)
            {
                EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, new[] { entity }));
                return;
            }

            _unitOfWorkManager.Current.Completed += (sender, args) => EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, new[] { entity }));
        }
    }
}