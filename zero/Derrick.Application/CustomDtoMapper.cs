using AutoMapper;
using Derrick.Authorization.Users;
using Derrick.Authorization.Users.Dto;

namespace Derrick
{
    /// <summary>
    /// �Զ���Dtoӳ����
    /// </summary>
    internal static class CustomDtoMapper
    {
        /// <summary>
        /// ӳ��ǰ
        /// </summary>
        private static volatile bool _mappedBefore;
        /// <summary>
        /// ͬ������
        /// </summary>
        private static readonly object SyncObj = new object();
        /// <summary>
        /// ����ӳ��
        /// </summary>
        /// <param name="mapper">ӳ�����ñ��ʽ</param>
        public static void CreateMappings(IMapperConfigurationExpression mapper)
        {
            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal(mapper);

                _mappedBefore = true;
            }
        }
        /// <summary>
        /// �ڲ�����ӳ��
        /// </summary>
        /// <param name="mapper">ӳ�����ñ��ʽ</param>
        private static void CreateMappingsInternal(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
        }
    }
}