using Abp.Dependency;

namespace Abp.EntityFramework
{
    /// <summary>
    /// <see cref="IDbContextResolver"/>��Ĭ��ʵ��
    /// </summary>
    public class DefaultDbContextResolver : IDbContextResolver, ITransientDependency
    {
        /// <summary>
        /// IOC������
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// ���ݿ�����������ƥ����
        /// </summary>
        private readonly IDbContextTypeMatcher _dbContextTypeMatcher;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">IOC������</param>
        /// <param name="dbContextTypeMatcher">���ݿ�����������ƥ����</param>
        public DefaultDbContextResolver(IIocResolver iocResolver, IDbContextTypeMatcher dbContextTypeMatcher)
        {
            _iocResolver = iocResolver;
            _dbContextTypeMatcher = dbContextTypeMatcher;
        }

        /// <summary>
        /// �������ݿ�������
        /// </summary>
        /// <typeparam name="TDbContext">���ݿ������Ķ���</typeparam>
        /// <param name="connectionString">�����ַ���</param>
        /// <returns>���ݿ�������</returns>
        public TDbContext Resolve<TDbContext>(string connectionString)
        {
            var dbContextType = typeof(TDbContext);

            if (!dbContextType.IsAbstract)
            {
                return _iocResolver.Resolve<TDbContext>(new
                {
                    nameOrConnectionString = connectionString
                });
            }
            else
            {
                var concreteType = _dbContextTypeMatcher.GetConcreteType(dbContextType);
                return (TDbContext)_iocResolver.Resolve(concreteType, new
                {
                    nameOrConnectionString = connectionString
                });
            }
        }
    }
}