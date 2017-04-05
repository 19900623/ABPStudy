namespace Abp.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities which must be audited.
    /// ��Ƶ�ʵ����Ҫʵ�ִ˽ӿ�
    /// Related properties automatically set when saving/updating <see cref="Entity"/> objects.
    /// �����������Զ������õ� ����/�޸� <see cref="Entity"/> ����
    /// </summary>
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }

    /// <summary>
    /// Adds navigation properties to <see cref="IAudited"/> interface for user.
    /// Ϊ�û���� <see cref="IAudited"/> �ӿڵĵ�������
    /// </summary>
    /// <typeparam name="TUser">Type of the user / �û�����</typeparam>
    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}