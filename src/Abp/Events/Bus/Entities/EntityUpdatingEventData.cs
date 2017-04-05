using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event is used to notify just before update of an Entity.
    /// �������͵��¼�����ʵ�����ǰ֪ͨ
    /// </summary>
    /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
    [Serializable]
    public class EntityUpdatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is being updated / ���ڸ��µ�ʵ��</param>
        public EntityUpdatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}