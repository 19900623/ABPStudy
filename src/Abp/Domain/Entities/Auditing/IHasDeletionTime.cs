using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// An entity can implement this interface if <see cref="DeletionTime"/> of this entity must be stored.
    /// <see cref="DeletionTime"/> is automatically set when deleting <see cref="Entity"/>.
    /// ʵ�ִ˽ӿڵ�ʵ�����ӵ�� <see cref="DeletionTime"/>�ֶΣ���������ɾ��ʱ���Զ���ֵ
    /// </summary>
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>
        /// Deletion time of this entity. / ʵ���ɾ��ʱ��
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}