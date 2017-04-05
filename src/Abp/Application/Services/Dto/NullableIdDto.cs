using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be directly used (or inherited)to pass an nullable Id value to an application service method.
    /// ���DTO����ֱ��ʹ�ã���̳У�ͨ��һ����IDֵӦ�÷���ķ���
    /// </summary>
    /// <typeparam name="TId">Type of the Id / ����ID</typeparam>
    [Serializable]
    public class NullableIdDto<TId>
        where TId : struct
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public TId? Id { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public NullableIdDto()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id">����ID</param>
        public NullableIdDto(TId? id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="NullableIdDto{TId}"/> for <see cref="int"/>.
    /// <see cref="NullableIdDto{TId}"/> <see cref="int"/> �Ŀ�ݷ�ʽ
    /// </summary>
    [Serializable]
    public class NullableIdDto : NullableIdDto<int>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public NullableIdDto()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id"></param>
        public NullableIdDto(int? id)
            : base(id)
        {

        }
    }
}