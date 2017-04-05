using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFramework.Utils;
using Abp.Extensions;
using Abp.MultiTenancy;
using Castle.Core.Internal;

namespace Abp.EntityFramework.Uow
{
    /// <summary>
    /// Implements Unit of work for Entity Framework.
    /// EF������Ԫ��ʵ��
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// ��������ݿ�������
        /// </summary>
        protected IDictionary<string, DbContext> ActiveDbContexts { get; private set; }

        /// <summary>
        /// IOC������
        /// </summary>
        protected IIocResolver IocResolver { get; private set; }

        /// <summary>
        /// ���ﷶΧ
        /// </summary>
        protected TransactionScope CurrentTransaction;

        /// <summary>
        /// ���ݿ������Ľ�����
        /// </summary>
        private readonly IDbContextResolver _dbContextResolver;

        /// <summary>
        /// ���ݿ�����������ƥ����
        /// </summary>
        private readonly IDbContextTypeMatcher _dbContextTypeMatcher;

        /// <summary>
        /// Creates a new <see cref="EfUnitOfWork"/>.
        /// ���캯��
        /// </summary>
        public EfUnitOfWork(
            IIocResolver iocResolver,
            IConnectionStringResolver connectionStringResolver,
            IDbContextResolver dbContextResolver,
            IEfUnitOfWorkFilterExecuter filterExecuter,
            IUnitOfWorkDefaultOptions defaultOptions, 
            IDbContextTypeMatcher dbContextTypeMatcher)
            : base(
                  connectionStringResolver, 
                  defaultOptions,
                  filterExecuter)
        {
            IocResolver = iocResolver;
            _dbContextResolver = dbContextResolver;
            _dbContextTypeMatcher = dbContextTypeMatcher;

            ActiveDbContexts = new Dictionary<string, DbContext>();
        }

        /// <summary>
        /// ��ʼ������Ԫ
        /// </summary>
        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        /// <summary>
        /// ���ڣ����湤����Ԫ�����е��޸ġ�
        /// �����������ҪӦ���޸�ʱ���á�
        /// ע�⣬���������Ԫ�������Եģ�����������������������ʧ��ʱ���ع�����
        /// һ�㲻����ʽ����SaveChages����Ϊ������Ԫ���Զ��������б��
        /// </summary>
        public override void SaveChanges()
        {
            ActiveDbContexts.Values.ForEach(SaveChangesInDbContext);
        }

        /// <summary>
        /// ���ڣ����湤����Ԫ�����е��޸ġ�
        /// �����������ҪӦ���޸�ʱ���á�
        /// ע�⣬���������Ԫ�������Եģ�����������������������ʧ��ʱ���ع�����
        /// һ�㲻����ʽ����SaveChages����Ϊ������Ԫ���Զ��������б��
        /// </summary>
        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        /// <summary>
        /// ��ȡ���е����ݿ�������
        /// </summary>
        /// <returns>���ݿ��������б�</returns>
        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }

        /// <summary>
        /// ����ɹ�����Ԫ
        /// </summary>
        protected override void CompleteUow()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }

            DisposeUow();
        }

        /// <summary>
        /// ��ɹ�����Ԫ - �첽
        /// </summary>
        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }

            DisposeUow();
        }

        /// <summary>
        /// ��ȡ�򴴽����ݿ�������
        /// </summary>
        /// <typeparam name="TDbContext">���ݿ������Ķ���</typeparam>
        /// <param name="multiTenancySide">���⻧˫���е�һ��</param>
        /// <returns>���ݿ������Ķ���</returns>
        public virtual TDbContext GetOrCreateDbContext<TDbContext>(MultiTenancySides? multiTenancySide = null)
            where TDbContext : DbContext
        {
            var concreteDbContextType = _dbContextTypeMatcher.GetConcreteType(typeof(TDbContext));

            var connectionStringResolveArgs = new ConnectionStringResolveArgs(multiTenancySide);
            connectionStringResolveArgs["DbContextType"] = typeof(TDbContext);
            connectionStringResolveArgs["DbContextConcreteType"] = concreteDbContextType;
            var connectionString = ResolveConnectionString(connectionStringResolveArgs);

            var dbContextKey = concreteDbContextType.FullName + "#" + connectionString;

            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            {

                dbContext = _dbContextResolver.Resolve<TDbContext>(connectionString);

                ((IObjectContextAdapter)dbContext).ObjectContext.ObjectMaterialized += (sender, args) =>
                {
                    ObjectContext_ObjectMaterialized(dbContext, args);
                };

                FilterExecuter.As<IEfUnitOfWorkFilterExecuter>().ApplyCurrentFilters(this, dbContext);

                ActiveDbContexts[dbContextKey] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        /// <summary>
        /// ���ٹ�����Ԫ
        /// </summary>
        protected override void DisposeUow()
        {
            ActiveDbContexts.Values.ForEach(Release);
            ActiveDbContexts.Clear();

            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
                CurrentTransaction = null;
            }
        }

        /// <summary>
        /// �����ݿ�ɽ�����б������еĸ���
        /// </summary>
        /// <param name="dbContext">���ݿ�������</param>
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// �����ݿ�ɽ�����б������еĸ��� - �첽
        /// </summary>
        /// <param name="dbContext">���ݿ�������</param>
        /// <returns></returns>
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// �ͷ����ݿ�������
        /// </summary>
        /// <param name="dbContext">���ݿ�������</param>
        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }

        /// <summary>
        /// ���������� - ʵ����
        /// </summary>
        /// <param name="dbContext">���ݿ�������</param>
        /// <param name="e">����</param>
        private static void ObjectContext_ObjectMaterialized(DbContext dbContext, ObjectMaterializedEventArgs e)
        {
            var entityType = ObjectContext.GetObjectType(e.Entity.GetType());

            dbContext.Configuration.AutoDetectChangesEnabled = false;
            var previousState = dbContext.Entry(e.Entity).State;

            DateTimePropertyInfoHelper.NormalizeDatePropertyKinds(e.Entity, entityType);

            dbContext.Entry(e.Entity).State = previousState;
            dbContext.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}