using System;
using System.Data.Entity;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// EF�ִ���չ
    /// </summary>
    public static class EfRepositoryExtensions
    {
        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <typeparam name="TPrimaryKey">��������</typeparam>
        /// <param name="repository">�ִ��ӿ�</param>
        /// <returns>���ݿ�������</returns>
        public static DbContext GetDbContext<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            var repositoryWithDbContext = repository as IRepositoryWithDbContext;
            if (repositoryWithDbContext == null)
            {
                throw new ArgumentException("Given repository does not implement IRepositoryWithDbContext", nameof(repository));
            }

            return repositoryWithDbContext.GetDbContext();
        }

        /// <summary>
        /// �����ݿ������ķ���
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <typeparam name="TPrimaryKey">��������</typeparam>
        /// <param name="repository">�ִ�</param>
        /// <param name="entity">ʵ������</param>
        public static void DetachFromDbContext<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            repository.GetDbContext().Entry(entity).State = EntityState.Detached;
        }
    }
}