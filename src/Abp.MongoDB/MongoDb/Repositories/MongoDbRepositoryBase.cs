using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Abp.MongoDb.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// MongoDB�ִ�ʵ�� - ����ΪInt����
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository / �ִ���ʵ������</typeparam>
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="databaseProvider">MongDB���ݿ��ṩ��</param>
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// Implements IRepository for MongoDB.
    /// MongoDB�ִ�ʵ��
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// MongoDB���ݿ�
        /// </summary>
        public virtual MongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        /// <summary>
        /// MongoDB���ݿ⼯��
        /// </summary>
        public virtual MongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        /// <summary>
        /// MongoDB���ݿ��ṩ��
        /// </summary>
        private readonly IMongoDatabaseProvider _databaseProvider;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="databaseProvider"></param>
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        /// <summary>
        /// ��ȡһ��IQueryalbe�������Դ����ű��л�ȡʵ��
        /// Ҫʹ�ô˷�������ʹ��<see cref="UnitOfWorkAttribute"/> ���ԣ���Ϊ�˲�����Ҫʹ�ù�����Ԫ�������ݿ�����
        /// </summary>
        /// <returns>���ڴ����ݿ��л�ȡʵ���IQueryable</returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        /// <summary>
        /// ͨ��������ȡʵ��
        /// </summary>
        /// <param name="id">����������</param>
        /// <returns>ʵ��</returns>
        public override TEntity Get(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            var entity = Collection.FindOne(query);
            if (entity == null)
            {
                throw new EntityNotFoundException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        /// <summary>
        /// ͨ��������ȡʵ�壬���û���ҵ��򷵻�Null
        /// </summary>
        /// <param name="id">ʵ������</param>
        /// <returns>ʵ�����Null</returns>
        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query);
        }

        /// <summary>
        /// ����һ���µ�ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }

        /// <summary>
        /// �����Ѵ��ڵ�ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public override TEntity Update(TEntity entity)
        {
            Collection.Save(entity);
            return entity;
        }

        /// <summary>
        /// ɾ��һ��ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// ��������ɾ��ʵ��
        /// </summary>
        /// <param name="id">ʵ�������</param>
        public override void Delete(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
        }
    }
}