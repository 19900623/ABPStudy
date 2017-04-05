using System.Collections.Generic;
using Abp.Events.Bus;

namespace Abp.Domain.Entities
{
    /// <summary>
    /// �ۺϸ��ӿ�
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity
    {

    }

    /// <summary>
    /// �ۺϸ��ӿ�(����)
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>, IGeneratesDomainEvents
    {

    }

    /// <summary>
    /// ���������¼��ӿ�
    /// </summary>
    public interface IGeneratesDomainEvents
    {
        /// <summary>
        /// �����¼����ݼ���
        /// </summary>
        ICollection<IEventData> DomainEvents { get; }
    }
}