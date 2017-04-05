using System;
using Abp.Domain.Entities;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// Used to pass data for an event when an entity (<see cref="IEntity"/>) is being changed (creating, updating or deleting).
    /// See <see cref="EntityCreatingEventData{TEntity}"/>, <see cref="EntityDeletingEventData{TEntity}"/> and <see cref="EntityUpdatingEventData{TEntity}"/> classes.
    /// ���ڴ������ݸ��¼�,��ʵ��<see cref="IEntity"/>���ڸı��ʱ��(�������޸ģ�ɾ��)
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityChangingEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">Changing entity in this event / �¼������ڸı��ʵ��</param>
        public EntityChangingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}