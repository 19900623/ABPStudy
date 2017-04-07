using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.MemoryDb.Repositories
{
    //TODO: Implement thread-safety..? ʵ���̰߳�ȫ?
    /// <summary>
    /// �ڴ�ִ�����
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����</typeparam>
    /// <typeparam name="TPrimaryKey">��������</typeparam>
    public class MemoryRepository<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// �ڴ����ݿ�
        /// </summary>
        public virtual MemoryDatabase Database { get { return _databaseProvider.Database; } }

        /// <summary>
        /// ʵ��Table
        /// </summary>
        public virtual List<TEntity> Table { get { return Database.Set<TEntity>(); } }

        /// <summary>
        /// �ڴ����ݿ��ṩ��
        /// </summary>
        private readonly IMemoryDatabaseProvider _databaseProvider;

        /// <summary>
        /// �ڴ�����������
        /// </summary>
        private readonly MemoryPrimaryKeyGenerator<TPrimaryKey> _primaryKeyGenerator;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="databaseProvider"></param>
        public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            _primaryKeyGenerator = new MemoryPrimaryKeyGenerator<TPrimaryKey>();
        }

        /// <summary>
        /// ��ȡһ��IQueryalbe�������Դ����ű��л�ȡʵ��
        /// Ҫʹ�ô˷�������ʹ��<see cref="UnitOfWorkAttribute"/> ���ԣ���Ϊ�˲�����Ҫʹ�ù�����Ԫ�������ݿ�����
        /// </summary>
        /// <returns>���ڴ����ݿ��л�ȡʵ���IQueryable</returns>
        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        /// <summary>
        /// ����һ���µ�ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns></returns>
        public override TEntity Insert(TEntity entity)
        {
            if (entity.IsTransient())
            {
                entity.Id = _primaryKeyGenerator.GetNext();
            }

            Table.Add(entity);
            return entity;
        }

        /// <summary>
        /// �����Ѵ��ڵ�ʵ��
        /// </summary>
        /// <param name="entity">ʵ��</param>
        /// <returns>ʵ��</returns>
        public override TEntity Update(TEntity entity)
        {
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, entity.Id));
            if (index >= 0)
            {
                Table[index] = entity;
            }

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
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, id));
            if (index >= 0)
            {
                Table.RemoveAt(index);
            }
        }
    }
}