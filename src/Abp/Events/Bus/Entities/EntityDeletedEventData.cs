using System;

namespace Abp.Events.Bus.Entities
{
    /// <summary>
    /// This type of event can be used to notify just after deletion of an Entity.
    /// �������͵��¼�����ʵ��ɾ����֪ͨ
    /// </summary>
    /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="entity">The entity which is deleted / �Ѿ�ɾ����ʵ��</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}