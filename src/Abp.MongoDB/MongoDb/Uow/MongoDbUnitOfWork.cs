using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MongoDb.Configuration;
using MongoDB.Driver;

namespace Abp.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// MongoDB������Ԫʵ��
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// ��ȡһ��MongoDB���ݿ�����
        /// </summary>
        public MongoDatabase Database { get; private set; }

        /// <summary>
        /// ABP MongoDBģ������
        /// </summary>
        private readonly IAbpMongoDbModuleConfiguration _configuration;

        /// <summary>
        /// ���캯��.
        /// </summary>
        public MongoDbUnitOfWork(
            IAbpMongoDbModuleConfiguration configuration, 
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkFilterExecuter filterExecuter,
            IUnitOfWorkDefaultOptions defaultOptions)
            : base(
                  connectionStringResolver, 
                  defaultOptions,
                  filterExecuter)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ����������Ԫ
        /// </summary>
        #pragma warning disable
        protected override void BeginUow()
        {
            //TODO: MongoClientExtensions.GetServer(MongoClient)' is obsolete: 'Use the new API instead.
            Database = new MongoClient(_configuration.ConnectionString)
                .GetServer()
                .GetDatabase(_configuration.DatatabaseName);
        }
        #pragma warning restore

        /// <summary>
        /// �������еĸ���
        /// </summary>
        public override void SaveChanges()
        {

        }

        /// <summary>
        /// �첽�������и���
        /// </summary>
        /// <returns></returns>
        #pragma warning disable 1998
        public override async Task SaveChangesAsync()
        {

        }
        #pragma warning restore 1998

        /// <summary>
        /// ��ɹ�����Ԫ
        /// </summary>
        protected override void CompleteUow()
        {

        }

        /// <summary>
        /// �첽��ɹ�����Ԫ
        /// </summary>
        /// <returns></returns>
        #pragma warning disable 1998
        protected override async Task CompleteUowAsync()
        {

        }

        /// <summary>
        /// �ͷŹ�����Ԫ
        /// </summary>
        #pragma warning restore 1998
        protected override void DisposeUow()
        {

        }
    }
}