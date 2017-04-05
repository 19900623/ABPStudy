using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify just after update of an Entity.
    /// �������͵��¼�����ʵ����º�֪ͨ
    /// </summary>
    /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is updated / �Ѿ����µ�ʵ��</param>
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}