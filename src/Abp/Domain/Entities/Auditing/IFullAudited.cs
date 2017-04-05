namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface ads <see cref="IDeletionAudited"/> to <see cref="IAudited"/> for a fully audited entity.
    /// �˽ӿ�Ϊһ��ӵ��ȫ����Ƶ�ʵ�����<see cref="IDeletionAudited"/>��<see cref="IAudited"/>
    /// </summary>
    public interface IFullAudited : IAudited, IDeletionAudited
    {
        
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IFullAudited"/> interface for user.
    /// Ϊ�û���� <see cref="IFullAudited"/> �ӿڵĵ�������
    /// </summary>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    public interface IFullAudited<TUser> : IAudited<TUser>, IFullAudited, IDeletionAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}