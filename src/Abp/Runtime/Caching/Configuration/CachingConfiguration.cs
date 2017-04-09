using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Configuration.Startup;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// ��������
    /// </summary>
    public class CachingConfiguration : ICachingConfiguration
    {
        /// <summary>
        /// ��ȡABP���ö���
        /// </summary>
        public IAbpStartupConfiguration AbpConfiguration { get; private set; }

        /// <summary>
        /// ����ע��Ļ��������б�
        /// </summary>
        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }
        private readonly List<ICacheConfigurator> _configurators;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="abpConfiguration"></param>
        public CachingConfiguration(IAbpStartupConfiguration abpConfiguration)
        {
            AbpConfiguration = abpConfiguration;

            _configurators = new List<ICacheConfigurator>();
        }

        /// <summary>
        /// �����������л���
        /// </summary>
        /// <param name="initAction">һ���������û���ķ������÷������ڻ��洴���󱻵���</param>
        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        /// <summary>
        /// ���������ض��Ļ���
        /// </summary>
        /// <param name="cacheName">��������</param>
        /// <param name="initAction">һ���������û���ķ������÷������ڻ��洴���󱻵���</param>
        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}