using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MemoryDb.Configuration;

namespace Abp.MemoryDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MemoryDb.
    /// �ڴ����ݿ⹤����Ԫʵ��
    /// </summary>
    public class MemoryDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to Memory Database.
        /// ��ȡһ���ڴ����ݿ�����
        /// </summary>
        public MemoryDatabase Database { get; private set; }

        /// <summary>
        /// �ڴ����ݿ�ģ����������
        /// </summary>
        private readonly IAbpMemoryDbModuleConfiguration _configuration;

        /// <summary>
        /// �ڴ����ݿ�
        /// </summary>
        private readonly MemoryDatabase _memoryDatabase;

        /// <summary>
        /// ���캯��
        /// </summary>
        public MemoryDbUnitOfWork(
            IAbpMemoryDbModuleConfiguration configuration, 
            MemoryDatabase memoryDatabase, 
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkFilterExecuter filterExecuter,
            IUnitOfWorkDefaultOptions defaultOptions)
            : base(
                  connectionStringResolver, 
                  defaultOptions,
                  filterExecuter)
        {
            _configuration = configuration;
            _memoryDatabase = memoryDatabase;
        }

        /// <summary>
        /// ����������Ԫ
        /// </summary>
        protected override void BeginUow()
        {
            Database = _memoryDatabase;
        }

        /// <summary>
        /// �������еĸ���
        /// </summary>
        public override void SaveChanges()
        {

        }

        /// <summary>
        /// �첽�������еĸ���
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
        #pragma warning restore 1998

        /// <summary>
        /// �ͷŹ�����Ԫ
        /// </summary>
        protected override void DisposeUow()
        {

        }
    }
}