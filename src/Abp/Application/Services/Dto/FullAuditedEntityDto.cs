using System;
using Abp.Domain.Entities.Auditing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// ������������Ϊ(<see cref="int"/>)��<see cref="FullAuditedEntityDto{TPrimaryKey}"/>��һ����ݷ�ʽ
    /// </summary>
    [Serializable]
    public abstract class FullAuditedEntityDto : FullAuditedEntityDto<int>
    {

    }

    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IFullAudited{TUser}"/> interface.
    /// �̳д������ΪDto����ʵ�ֽӿ�<see cref="IFullAudited{TUser}"/>
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key / ����������</typeparam>
    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// Is this entity deleted?
        /// ��ʵ������Ƿ��ѱ�ɾ��
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Deleter user's Id, if this entity is deleted,
        /// ɾ��ʵ����û�Id
        /// </summary>
        public long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time, if this entity is deleted,
        /// ɾ��ʱ��
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}