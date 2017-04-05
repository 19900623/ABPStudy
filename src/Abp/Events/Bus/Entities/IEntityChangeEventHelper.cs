namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// ���ڴ���ʵ��ı��¼�
    /// </summary>
    public interface IEntityChangeEventHelper
    {
        /// <summary>
        /// ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityCreatingEvent(object entity);

        /// <summary>
        /// �ڹ�����Ԫ��ɵ�ʱ�򴥷�ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        void TriggerEntityCreatedEventOnUowCompleted(object entity);

        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityUpdatingEvent(object entity);

        /// <summary>
        /// �ڹ�����Ԫ��ɵ�ʱ�򴥷�ʵ������¼�
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityUpdatedEventOnUowCompleted(object entity);

        /// <summary>
        /// ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityDeletingEvent(object entity);

        /// <summary>
        /// �ڹ�����Ԫ��ɵ�ʱ�򴥷�ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity"></param>
        void TriggerEntityDeletedEventOnUowCompleted(object entity);
    }
}