using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// EF�Զ��ִ�����
    /// </summary>
    public static class EfAutoRepositoryTypes
    {
        /// <summary>
        /// �Զ��ִ���������
        /// </summary>
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        static EfAutoRepositoryTypes()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof(IRepository<>),
                typeof(IRepository<,>),
                typeof(EfRepositoryBase<,>),
                typeof(EfRepositoryBase<,,>)
            );
        }
    }
}