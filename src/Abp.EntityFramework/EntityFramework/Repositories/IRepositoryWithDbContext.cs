using System.Data.Entity;

namespace Abp.EntityFramework.Repositories
{
    /// <summary>
    /// ���������Ĳִ��ӿ�
    /// </summary>
    public interface IRepositoryWithDbContext
    {
        /// <summary>
        /// ��ȡ���ݿ������Ķ���
        /// </summary>
        /// <returns>���ݿ�������</returns>
        DbContext GetDbContext();
    }
}