using System.Data.Entity;
using Abp.MultiTenancy;

namespace Abp.EntityFramework
{
    /// <summary>
    /// <see cref="IDbContextProvider{TDbContext}"/>��ʵ��
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public sealed class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// ���ݿ������Ķ���
        /// </summary>
        public TDbContext DbContext { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dbContext">���ݿ������Ķ���</param>
        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <returns>���ݿ������Ķ���</returns>
        public TDbContext GetDbContext()
        {
            return DbContext;
        }

        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <param name="multiTenancySide">��ʾ���⻧˫���е�һ��</param>
        /// <returns>���ݿ������Ķ���</returns>
        public TDbContext GetDbContext(MultiTenancySides? multiTenancySide)
        {
            return DbContext;
        }
    }
}