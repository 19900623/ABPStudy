using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// �������(<see cref="int"/>)��Ϊ��������ʱ��ʹ��<see cref="AuditedEntity{TPrimaryKey}"/> �Ŀ�ݷ�ʽ.
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>, IEntity
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// ����Ϊ <see cref="IAudited{TUser}"/>�ļ�ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity / ʵ�����������</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// ʵ�������޸�ʱ��
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// ʵ������޸���ID
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited{TUser}"/>.
    /// ����Ϊ <see cref="IAudited{TUser}"/>�ļ�ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity / ʵ�����������</typeparam>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey>, IAudited<TUser>
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
        /// ʵ������޸��˵�����
        /// </summary>
        [ForeignKey("LastModifierUserId")]
        public virtual TUser LastModifierUser { get; set; }
    }
}