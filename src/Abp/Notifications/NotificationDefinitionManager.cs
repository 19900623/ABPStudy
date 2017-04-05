using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;

namespace Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationDefinitionManager"/>.
    /// <see cref="INotificationDefinitionManager"/>��ʵ��
    /// </summary>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        /// <summary>
        /// ֪ͨϵͳ������
        /// </summary>
        private readonly INotificationConfiguration _configuration;

        /// <summary>
        /// IOC������
        /// </summary>
        private readonly IocManager _iocManager;

        /// <summary>
        /// ֪ͨ�����ֵ�
        /// </summary>
        private readonly IDictionary<string, NotificationDefinition> _notificationDefinitions;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocManager">IOC������</param>
        /// <param name="configuration">֪ͨϵͳ������</param>
        public NotificationDefinitionManager(
            IocManager iocManager,
            INotificationConfiguration configuration)
        {
            _configuration = configuration;
            _iocManager = iocManager;

            _notificationDefinitions = new Dictionary<string, NotificationDefinition>();
        }

        /// <summary>
        /// ֪ͨ�����ʼ��
        /// </summary>
        public void Initialize()
        {
            var context = new NotificationDefinitionContext(this);

            foreach (var providerType in _configuration.Providers)
            {
                _iocManager.RegisterIfNot(providerType, DependencyLifeStyle.Transient); //TODO: Needed?
                using (var provider = _iocManager.ResolveAsDisposable<NotificationProvider>(providerType))
                {
                    provider.Object.SetNotifications(context);
                }
            }
        }

        /// <summary>
        /// ���ָ����֪ͨ����
        /// </summary>
        /// <param name="notificationDefinition"></param>
        public void Add(NotificationDefinition notificationDefinition)
        {
            if (_notificationDefinitions.ContainsKey(notificationDefinition.Name))
            {
                throw new AbpInitializationException("There is already a notification definition with given name: " + notificationDefinition.Name + ". Notification names must be unique!");
            }

            _notificationDefinitions[notificationDefinition.Name] = notificationDefinition;
        }

        /// <summary>
        /// ͨ�����ƻ�ȡ֪ͨ����,���û���ҵ����׳��쳣
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NotificationDefinition Get(string name)
        {
            var definition = GetOrNull(name);
            if (definition == null)
            {
                throw new AbpException("There is no notification definition with given name: " + name);
            }

            return definition;
        }

        /// <summary>
        /// ͨ���������ƻ�ȡ֪ͨ����,���û���ҵ��򷵻�Null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NotificationDefinition GetOrNull(string name)
        {
            return _notificationDefinitions.GetOrDefault(name);
        }

        /// <summary>
        /// ��ȡ���е�֪ͨ����
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<NotificationDefinition> GetAll()
        {
            return _notificationDefinitions.Values.ToImmutableList();
        }

        /// <summary>
        /// Ϊָ���û�������֪ͨ(<see cref="name"/>)�Ƿ����
        /// </summary>
        /// <param name="name">֪ͨ����</param>
        /// <param name="user">ָ�����û�</param>
        /// <returns></returns>
        public async Task<bool> IsAvailableAsync(string name, UserIdentifier user)
        {
            var notificationDefinition = GetOrNull(name);
            if (notificationDefinition == null)
            {
                return true;
            }

            if (notificationDefinition.FeatureDependency != null)
            {
                using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
                {
                    featureDependencyContext.Object.TenantId = user.TenantId;

                    if (!await notificationDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext.Object))
                    {
                        return false;
                    }
                }
            }

            if (notificationDefinition.PermissionDependency != null)
            {
                using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext>())
                {
                    permissionDependencyContext.Object.User = user;

                    if (!await notificationDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext.Object))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Ϊָ���û���ȡ���п��õ�֪ͨ����
        /// </summary>
        /// <param name="user">�û�</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(UserIdentifier user)
        {
            var availableDefinitions = new List<NotificationDefinition>();

            using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext>())
            {
                permissionDependencyContext.Object.User = user;

                using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
                {
                    featureDependencyContext.Object.TenantId = user.TenantId;

                    foreach (var notificationDefinition in GetAll())
                    {
                        if (notificationDefinition.PermissionDependency != null &&
                            !await notificationDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext.Object))
                        {
                            continue;
                        }

                        if (user.TenantId.HasValue &&
                            notificationDefinition.FeatureDependency != null &&
                            !await notificationDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext.Object))
                        {
                            continue;
                        }

                        availableDefinitions.Add(notificationDefinition);
                    }
                }
            }

            return availableDefinitions.ToImmutableList();
        }
    }
}