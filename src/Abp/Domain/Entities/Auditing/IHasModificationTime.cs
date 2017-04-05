using System;

namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// An entity can implement this interface if <see cref="LastModificationTime"/> of this entity must be stored.
    /// <see cref="LastModificationTime"/> is automatically set when updating <see cref="Entity"/>.
    /// ʵ�ִ˽ӿڵ�ʵ�����ӵ�� <see cref="LastModificationTime"/>�ֶΣ����������޸�ʱ���Զ���ֵ
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        /// The last modified time for this entity. / ʵ�������޸�ʱ��
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}