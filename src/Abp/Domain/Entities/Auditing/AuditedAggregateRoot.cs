using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedAggregateRoot{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// �������(<see cref="int"/>)��Ϊ��������ʱ��ʹ��<see cref="CreationAuditedAggregateRoot{TPrimaryKey}"/> �Ŀ�ݷ�ʽ.
    /// </summary>
    [Serializable]
    public abstract class AuditedAggregateRoot : AuditedAggregateRoot<int>
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/> for aggregate roots.
    /// ����Ϊ <see cref="IAudited"/> �ӿڵľۺϸ���ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity / ʵ�����������</typeparam>
    [Serializable]
    public abstract class AuditedAggregateRoot<TPrimaryKey> : CreationAuditedAggregateRoot<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity. / ʵ�������޸�ʱ��
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity. / ʵ�������޸���ID
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited{TUser}"/> for aggregate roots.
    /// ����Ϊ <see cref="IAudited{TUser}"/> �ӿڵľۺϸ���ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity  / ʵ�����������</typeparam>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    [Serializable]
    public abstract class AuditedAggregateRoot<TPrimaryKey, TUser> : AuditedAggregateRoot<TPrimaryKey>, IAudited<TUser>
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// ʵ�崴���˵�����
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// ʵ������޸�������
        /// </summary>
        [ForeignKey("LastModifierUserId")]
        public virtual TUser LastModifierUser { get; set; }
    }
}