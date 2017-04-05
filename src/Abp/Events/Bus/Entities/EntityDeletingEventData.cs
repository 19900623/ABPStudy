using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event is used to notify just before deletion of an Entity.
    /// �������͵��¼�����ʵ��ɾ��ǰ֪ͨ
    /// </summary>
    /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
    [Serializable]
    public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is being deleted / ����ɾ����ʵ��</param>
        public EntityDeletingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}