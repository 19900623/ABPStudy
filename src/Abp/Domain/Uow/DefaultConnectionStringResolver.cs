using System;
using System.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Default implementation of <see cref="IConnectionStringResolver"/>.
    /// <see cref="IConnectionStringResolver"/> Ĭ��ʵ��
    /// Get connection string from <see cref="IAbpStartupConfiguration"/>,
    /// �� <see cref="IAbpStartupConfiguration"/> ��ȡ�����ַ���
    /// or "Default" connection string in config file,or single connection string in config file.
    /// Ҫô��Ĭ�������ַ�����Ҫô�ǵ��������ַ���
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        /// <summary>
        /// ABP����ʱĬ��������Ϣ
        /// </summary>
        private readonly IAbpStartupConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConnectionStringResolver"/> class.
        /// ���캯��.��ʼ��<see cref="DefaultConnectionStringResolver"/>���µ�ʵ��
        /// </summary>
        public DefaultConnectionStringResolver(IAbpStartupConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///  ��ȡһ�������ַ�������(�������ļ�) ��һ����Ч�������ַ���
        /// </summary>
        /// <param name="args">�����������ַ���ʱʹ�õĲ���</param>
        /// <returns></returns>
        public virtual string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            var defaultConnectionString = _configuration.DefaultNameOrConnectionString;
            if (!string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                return defaultConnectionString;
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            if (ConfigurationManager.ConnectionStrings.Count == 1)
            {
                return ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }

            throw new AbpException("Could not find a connection string definition for the application. Set IAbpStartupConfiguration.DefaultNameOrConnectionString or add a 'Default' connection string to application .config file.");
        }
    }
}