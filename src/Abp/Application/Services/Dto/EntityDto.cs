using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="EntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// �����ʹ��(<see cref="int"/>)��Ϊ�������͵�<see cref="EntityDto{TPrimaryKey}"/>��һ����ݷ�ʽ).
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// ���캯��
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="id">Id of the entity / ʵ��ID</param>
        public EntityDto(int id)
            : base(id)
        {
        }
    }

    /// <summary>
    /// Implements common properties for entity based DTOs.
    /// Ϊʵ��DTOʵ��ͨ������
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key / ��������</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Id of the entity.
        /// ʵ���Ψһ��ʶ
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// ����һ���µ�<see cref="EntityDto{TPrimaryKey}"/> ����.
        /// </summary>
        public EntityDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// ����һ���µ�<see cref="EntityDto{TPrimaryKey}"/> ����.
        /// </summary>
        /// <param name="id">Id of the entity / ʵ��ID</param>
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}