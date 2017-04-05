namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// ��洢ɾ����Ϣ��ʵ����ʵ�ִ˽ӿ�(ʵ��ɾ����/ɾ��ʱ��)
    /// </summary>
    public interface IDeletionAudited : IHasDeletionTime
    {
        /// <summary>
        /// Which user deleted this entity? / ɾ��ʵ����û�ID
        /// </summary>
        long? DeleterUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IDeletionAudited"/> interface for user.
    /// Ϊ�û���� <see cref="IDeletionAudited"/> �ӿڵĵ�������
    /// </summary>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// ɾ��ʵ���û�������
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}