using System;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// A registered cache configurator.
    /// ע�Ỻ��������
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// Name of the cache.It will be null if this configurator configures all caches.
        /// �������ƣ�����Ϊnull���������������������л���
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// Configuration action. Called just after the cache is created.
        /// ���÷���,�����ڻ��洴�������
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}