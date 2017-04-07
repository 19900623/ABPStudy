using Abp.Dependency;
using Abp.Domain.Uow;
using MongoDB.Driver;

namespace Abp.MongoDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// <see cref="IMongoDatabaseProvider"/>��ʵ�ִ���Ч�Ĺ�����Ԫ�л�ȡ���ݿ�
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        /// <summary>
        /// MongoDB
        /// </summary>
        public MongoDatabase Database { get { return ((MongoDbUnitOfWork)_currentUnitOfWork.Current).Database; } }

        /// <summary>
        /// ��ǰ������Ԫ�ṩ��
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="currentUnitOfWork"></param>
        public UnitOfWorkMongoDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}