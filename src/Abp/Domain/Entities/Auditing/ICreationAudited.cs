namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store creation information (who and when created).
    /// ��洢������Ϣ��ʵ����ʵ�ִ˽ӿ�(ʵ��Ĵ�����/����ʱ��)
    /// Creation time and creator user are automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface ICreationAudited : IHasCreationTime
    {
        /// <summary>
        /// Id of the creator user of this entity. / ʵ�崴����ID
        /// </summary>
        long? CreatorUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="ICreationAudited"/> interface for user.
    /// Ϊ�û���� <see cref="ICreationAudited"/> �ӿڵĵ�������
    /// </summary>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    public interface ICreationAudited<TUser> : ICreationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// ɾ��ʵ���û�������
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}