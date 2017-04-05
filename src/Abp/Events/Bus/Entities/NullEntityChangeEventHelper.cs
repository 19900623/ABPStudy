namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Null-object implementation of <see cref="IEntityChangeEventHelper"/>
    /// <see cref="IEntityChangeEventHelper"/>��Null����ʵ��.
    /// </summary>
    public class NullEntityChangeEventHelper : IEntityChangeEventHelper
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEventBus"/> class.
        /// ��ȡһ�� <see cref="NullEventBus"/> ��ĵ���.
        /// </summary>
        public static NullEntityChangeEventHelper Instance { get { return SingletonInstance; } }
        private static readonly NullEntityChangeEventHelper SingletonInstance = new NullEntityChangeEventHelper();

        /// <summary>
        /// ���캯��
        /// </summary>
        private NullEntityChangeEventHelper()
        {

        }

        /// <summary>
        /// ����ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityCreatingEvent(object entity)
        {
            
        }

        /// <summary>
        /// �ڹ�����Ԫ��ɵ�ʱ�򴥷�ʵ�崴���¼�
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
            
        }

        /// <summary>
        /// ����ʵ������¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityUpdatingEvent(object entity)
        {
            
        }

        /// <summary>
        /// �ڹ�����Ԫ��ɵ�ʱ�򴥷�ʵ������¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
            
        }

        /// <summary>
        /// ����ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityDeletingEvent(object entity)
        {
            
        }

        /// <summary>
        /// �ڹ�����Ԫ��ɵ�ʱ�򴥷�ʵ��ɾ���¼�
        /// </summary>
        /// <param name="entity"></param>
        public void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
            
        }
    }
}