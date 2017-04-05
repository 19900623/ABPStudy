using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Json;

namespace Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationSubscriptionManager"/>.
    /// <see cref="INotificationSubscriptionManager"/>��ʵ��
    /// </summary>
    public class NotificationSubscriptionManager : INotificationSubscriptionManager, ITransientDependency
    {
        /// <summary>
        /// ֪ͨ�洢��
        /// </summary>
        private readonly INotificationStore _store;

        /// <summary>
        /// ֪ͨ���������
        /// </summary>
        private readonly INotificationDefinitionManager _notificationDefinitionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscriptionManager"/> class.
        /// ��ʼ��<see cref="NotificationSubscriptionManager"/>���µ�ʵ��
        /// </summary>
        public NotificationSubscriptionManager(INotificationStore store, INotificationDefinitionManager notificationDefinitionManager)
        {
            _store = store;
            _notificationDefinitionManager = notificationDefinitionManager;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityIdentifier">��ʶʵ��</param>
        /// <returns></returns>
        public async Task SubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            if (await IsSubscribedAsync(user, notificationName, entityIdentifier))
            {
                return;
            }

            await _store.InsertSubscriptionAsync(
                new NotificationSubscriptionInfo(
                    user.TenantId,
                    user.UserId,
                    notificationName,
                    entityIdentifier
                    )
                );
        }

        /// <summary>
        /// �������п��õ�֪ͨ
        /// </summary>
        /// <param name="user">�û�</param>
        /// <returns></returns>
        public async Task SubscribeToAllAvailableNotificationsAsync(UserIdentifier user)
        {
            var notificationDefinitions = (await _notificationDefinitionManager
                .GetAllAvailableAsync(user))
                .Where(nd => nd.EntityType == null)
                .ToList();

            foreach (var notificationDefinition in notificationDefinitions)
            {
                await SubscribeAsync(user, notificationDefinition.Name);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityIdentifier">ʵ���ʶ</param>
        /// <returns></returns>
        public async Task UnsubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            await _store.DeleteSubscriptionAsync(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }
        
        // TODO: Can work only for single database approach!
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityIdentifier">ʵ���ʶ</param>
        /// <returns></returns>
        public async Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        /// <summary>
        /// Ϊָ����֪ͨ��ȡ���ж���
        /// </summary>
        /// <param name="tenantId">�⻧ID����������null</param>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityIdentifier">��ʶʵ��</param>
        /// <returns></returns>
        public async Task<List<NotificationSubscription>> GetSubscriptionsAsync(int? tenantId, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                new[] { tenantId },
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        /// <summary>
        /// Ϊ�û���ȡ���ж���֪ͨ
        /// </summary>
        /// <param name="user">�û�</param>
        /// <returns></returns>
        public async Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(UserIdentifier user)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(user);

            return notificationSubscriptionInfos
                .Select(nsi => nsi.ToNotificationSubscription())
                .ToList();
        }

        /// <summary>
        /// ����û��Ƿ�����֪ͨ
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="notificationName">֪ͨ����</param>
        /// <param name="entityIdentifier">��ʶʵ��</param>
        /// <returns></returns>
        public Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null)
        {
            return _store.IsSubscribedAsync(
                user,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id.ToJsonString()
                );
        }
    }
}