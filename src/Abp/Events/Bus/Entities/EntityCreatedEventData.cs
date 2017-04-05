using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify just after creation of an Entity.
    /// �������͵��¼�����ʵ�崴����֪ͨ
    /// </summary>
    /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
    [Serializable]
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is created / �Ѿ�������ʵ��</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}