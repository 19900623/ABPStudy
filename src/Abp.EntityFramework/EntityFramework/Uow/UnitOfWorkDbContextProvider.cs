using System.Data.Entity;
using Abp.Domain.Uow;
using Abp.MultiTenancy;

namespace Abp.EntityFramework.Uow
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from active unit of work.
    /// <see cref="IDbContextProvider{TDbContext}"/>��ʵ�֣��ӹ�����Ԫ�л�ȡ���ݿ�������
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext / ���ݿ�����������</typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// ��ǰ������Ԫ�ṩ��
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// ���캯��
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <returns>���ݿ������Ķ���</returns>
        public TDbContext GetDbContext()
        {
            return GetDbContext(null);
        }

        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <param name="multiTenancySide">��ʾ���⻧˫���е�һ��</param>
        /// <returns>���ݿ�������</returns>
        public TDbContext GetDbContext(MultiTenancySides? multiTenancySide)
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(multiTenancySide);
        }
    }
}