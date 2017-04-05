using System.Data.Entity;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// EF�ִ�����
    /// </summary>
    /// <typeparam name="TDbContext">���ݿ�����������</typeparam>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dbContextProvider">���ݿ��������ṩ��</param>
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}