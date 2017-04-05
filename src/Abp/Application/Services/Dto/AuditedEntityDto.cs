using System;
using Abp.Domain.Entities.Auditing;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// ����ʹ��(<see cref="int"/>)����������<see cref="AuditedEntityDto{TPrimaryKey}"/>һ�������ʽ
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityDto : AuditedEntityDto<int>
    {

    }

    /// <summary>
    /// This class can be inherited for simple Dto objects those are used for entities implement <see cref="IAudited{TUser}"/> interface.
    /// ������Ա���Dto����̳У���ʵ�ֽӿ� <see cref="IAudited{TUser}"/> 
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of primary key / ��������</typeparam>
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// ���һ���޸�ʵ���ʱ��
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// ���һ���޸�ʵ����û�
        /// </summary>
        public long? LastModifierUserId { get; set; }
    }
}