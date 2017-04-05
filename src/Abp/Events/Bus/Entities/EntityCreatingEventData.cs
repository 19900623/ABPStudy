using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event is used to notify just before creation of an Entity.
    /// �������͵��¼�����ʵ�崴��ǰ֪ͨ
    /// </summary>
    /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is being created / ���ڱ�������ʵ��</param>
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}