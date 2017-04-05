using Abp.Application.Services.Dto;

namespace Abp.Application.Services
{
    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    public interface ICrudAppService<TEntityDto>
        : ICrudAppService<TEntityDto, int>
        where TEntityDto : IEntityDto<int>
    {

    }

    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    public interface ICrudAppService<TEntityDto, TPrimaryKey>
        : ICrudAppService<TEntityDto, TPrimaryKey, PagedAndSortedResultRequestDto>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    public interface ICrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput>
        : ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    public interface ICrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput>
        : ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TCreateInput>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TCreateInput : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    /// <typeparam name="TUpdateInput">�����������</typeparam>
    public interface ICrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput>
        : ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    /// <typeparam name="TUpdateInput">�����������</typeparam>
    /// <typeparam name="TGetInput">��ȡ�������</typeparam>
    public interface ICrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput>
    : ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, EntityDto<TPrimaryKey>>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey> 
        where TGetInput : IEntityDto<TPrimaryKey>
    {

    }

    /// <summary>
    /// ��ɾ�ò�Ӧ�÷���ӿ�
    /// </summary>
    /// <typeparam name="TEntityDto">ʵ��DTO����</typeparam>
    /// <typeparam name="TPrimaryKey">ʵ������</typeparam>
    /// <typeparam name="TGetAllInput">��ȡ�����������</typeparam>
    /// <typeparam name="TCreateInput">�����������</typeparam>
    /// <typeparam name="TUpdateInput">�����������</typeparam>
    /// <typeparam name="TGetInput">��ȡ�������</typeparam>
    /// <typeparam name="TDeleteInput">ɾ���������</typeparam>
    public interface ICrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput, in TDeleteInput>
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
        TEntityDto Get(TGetInput input);

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <param name="input">�������</param>
        /// <returns></returns>
        PagedResultDto<TEntityDto> GetAll(TGetAllInput input);

        /// <summary>
        /// ����ʵ��DTO����
        /// </summary>
        /// <param name="input">�����������</param>
        /// <returns></returns>
        TEntityDto Create(TCreateInput input);

        /// <summary>
        /// ����ʵ��DTO
        /// </summary>
        /// <param name="input">�����������</param>
        /// <returns></returns>
        TEntityDto Update(TUpdateInput input);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="input">ɾ���������</param>
        /// <returns></returns>
        void Delete(TDeleteInput input);
    }
}
