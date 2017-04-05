namespace Abp.EntityFramework
{
    /// <summary>
    /// ���ݿ������Ľ������ӿ�
    /// </summary>
    public interface IDbContextResolver
    {
        /// <summary>
        /// �������ݿ�������
        /// </summary>
        /// <typeparam name="TDbContext">���ݿ������Ķ���</typeparam>
        /// <param name="connectionString">�����ַ���</param>
        /// <returns>���ݿ�������</returns>
        TDbContext Resolve<TDbContext>(string connectionString);
    }
}