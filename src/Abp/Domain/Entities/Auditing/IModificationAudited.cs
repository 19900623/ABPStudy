namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// ��洢�޸���Ϣ��ʵ����ʵ�ִ˽ӿ�(ʵ������޸���/�޸�ʱ��)
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// ��ʵ��<see cref="IEntity"/>�޸�ʱ�������Զ���ֵ
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        /// <summary>
        /// Last modifier user for this entity. / ʵ�������޸���ID
        /// </summary>
        long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IModificationAudited"/> interface for user.
    /// Ϊ�û���� <see cref="IModificationAudited"/> �ӿڵĵ�������
    /// </summary>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// ʵ������޸��˵�����
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}