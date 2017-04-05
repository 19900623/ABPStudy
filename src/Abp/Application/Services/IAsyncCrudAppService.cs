using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Abp.Application.Services
{
    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    public interface IAsyncCrudAppService<TEntityDto>
        : IAsyncCrudAppService<TEntityDto, int>
        where TEntityDto : IEntityDto<int>
    {

    }

    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    public interface IAsyncCrudAppService<TEntityDto, TPrimaryKey>
        : IAsyncCrudAppService<TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    public interface IAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput>
        : IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    public interface IAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput>
        : IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TCreateInput>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TCreateInput : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    /// <typeparam name="TUpdateInput">�����������</typeparam>
    public interface IAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput>
        : IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    /// <typeparam name="TUpdateInput">�����������</typeparam>
    /// <typeparam name="TGetInput">��ȡ�������</typeparam>
    public interface IAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput>
        : IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// �첽��ɾ�ò�Ӧ�÷���
    /// </summary>
    /// <typeparam name="TEntity">ʵ��</typeparam>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">����</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    /// <typeparam name="TUpdateInput">�����������</typeparam>
    /// <typeparam name="TGetInput">��ȡ�������</typeparam>
    /// <typeparam name="TDeleteInput">ɾ���������</typeparam>
    public interface IAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput, in TDeleteInput>
        : IApplicationService
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TDeleteInput : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// ��ȡʵ��DTO����
        /// </summary>
        /// <param name="input">��ȡ�������</param>
        /// <returns></returns>
        Task<TEntityDto> Get(TGetInput input);

        /// <summary>
        /// ��ȡ��ҳ���DTO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input);

        /// <summary>
        /// ����ʵ��DTO����
        /// </summary>
        /// <param name="input">�����������</param>
        /// <returns></returns>
        Task<TEntityDto> Create(TCreateInput input);

        /// <summary>
        /// ����ʵ��DTO
        /// </summary>
        /// <param name="input">�����������</param>
        /// <returns></returns>
        Task<TEntityDto> Update(TUpdateInput input);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="input">ɾ���������</param>
        /// <returns></returns>
        Task Delete(TDeleteInput input);
    }
}
