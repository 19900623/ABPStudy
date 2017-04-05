using Abp.Domain.Uow;

namespace Abp.EntityFramework
{
    /// <summary>
    /// <see cref="DbContextTypeMatcher{AbpDbContext}"/>��ʵ�֡�
    /// </summary>
    public class DbContextTypeMatcher : DbContextTypeMatcher<AbpDbContext>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) 
            : base(currentUnitOfWorkProvider)
        {
        }
    }
}