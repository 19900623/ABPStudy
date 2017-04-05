namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used to get connection string when a database connection is needed.
    /// ����Ҫ�������ݿ��ʱ��������ȡ�����ַ���
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// Gets a connection string name (in config file) or a valid connection string.
        /// ��ȡһ�������ַ�������(�������ļ�) ��һ����Ч�������ַ���
        /// </summary>
        /// <param name="args">Arguments that can be used while resolving connection string. / �����������ַ���ʱʹ�õĲ���</param>
        string GetNameOrConnectionString(ConnectionStringResolveArgs args);
    }
}