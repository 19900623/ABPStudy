using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// �������(<see cref="int"/>)��Ϊ��������ʱ��ʹ��<see cref="CreationAuditedEntity{TPrimaryKey}"/> �Ŀ�ݷ�ʽ.
    /// </summary>
    [Serializable]
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int>, IEntity
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// ����Ϊ <see cref="ICreationAudited"/>�ļ�ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity / ʵ�����������</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        /// <summary>
        /// Creation time of this entity. / ʵ��Ĵ���ʱ��
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator of this entity. / ʵ��Ĵ�����ID
        /// </summary>
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// Constructor. / ���캯��
        /// </summary>
        protected CreationAuditedEntity()
        {
            CreationTime = Clock.Now;
        }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited{TUser}"/>.
    /// ����Ϊ <see cref="ICreationAudited{TUser}"/>�ļ�ʵ��
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity / ʵ�����������</typeparam>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey, TUser> : CreationAuditedEntity<TPrimaryKey>, ICreationAudited<TUser>
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// ʵ�崴���˵�����
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }
    }
}