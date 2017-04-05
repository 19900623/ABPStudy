namespace Abp.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="IEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// һ����ݷ�ʽ<see cref="IEntity{TPrimaryKey}"/>Ϊ�󲿷�ʹ��(<see cref="int"/>)������Ϊ����
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}