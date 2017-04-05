using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
    /// <see cref="CreationTime"/> is automatically set when saving <see cref="Entity"/> to database.
    /// ʵ�ִ˽ӿڵ�ʵ�����ӵ�� <see cref="CreationTime"/>�ֶΣ����ڱ��浽����ʱ�ܱ��Զ�����ֵ
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity. / ʵ��Ĵ���ʱ��
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}