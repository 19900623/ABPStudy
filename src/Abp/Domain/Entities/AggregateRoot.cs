using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Events.Bus;

namespace Abp.Domain.Entities
{
    /// <summary>
    /// ��������Ϊint�ľۺϸ�
    /// </summary>
    public class AggregateRoot : AggregateRoot<int>, IAggregateRoot
    {

    }

    /// <summary>
    /// �ۺϸ���
    /// </summary>
    /// <typeparam name="TPrimaryKey">��������</typeparam>
    public class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        /// <summary>
        /// �����¼����ݼ���
        /// </summary>
        [NotMapped]
        public virtual ICollection<IEventData> DomainEvents { get; }

        /// <summary>
        /// ��ʼ�������¼�����
        /// </summary>
        public AggregateRoot()
        {
            DomainEvents = new Collection<IEventData>();
        }
    }
}