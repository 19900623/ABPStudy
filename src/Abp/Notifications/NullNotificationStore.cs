using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Null pattern implementation of <see cref="INotificationStore"/>.
    /// <see cref="INotificationStore"/>�Ŀ�ģʽʵ��
    /// </summary>
    public class NullNotificationStore : INotificationStore
    {
        /// <summary>
        /// ����һ������֪ͨ - �첽
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ��һ������֪ͨ - �첽
        /// </summary>
        /// <param name="user"></param>
        /// <param name="notificationName"></param>
        /// <param name="entityTypeName"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task DeleteSubscriptionAsync(UserIdentifier user, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ����һ��֪ͨ - �첽
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public Task InsertNotificationAsync(NotificationInfo notification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ͨ��ID��ȡ֪ͨ�����û���ҵ��򷵻�null
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            return Task.FromResult(null as NotificationInfo);
        }

        /// <summary>
        /// ����һ���û�֪ͨ
        /// </summary>
        /// <param name="userNotification"></param>
        /// <returns></returns>
        public Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ֪ͨ����
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="entityTypeName"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName = null, string entityId = null)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// Ϊָ�����û���ȡ֪ͨ����
        /// </summary>
        /// <param name="tenantIds"></param>
        /// <param name="notificationName"></param>
        /// <param name="entityTypeName"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// ��ȡ�û��Ķ���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(UserIdentifier user)
        {
            return Task.FromResult(new List<NotificationSubscriptionInfo>());
        }

        /// <summary>
        /// ����û��Ƿ�����֪ͨ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="notificationName"></param>
        /// <param name="entityTypeName"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, string entityTypeName, string entityId)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// �����û�֪ͨ״̬
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="userNotificationId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Task UpdateUserNotificationStateAsync(int? notificationId, Guid userNotificationId, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// �����û�������֪ͨ״̬
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Task UpdateAllUserNotificationStatesAsync(UserIdentifier user, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ���û�֪ͨ
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="userNotificationId"></param>
        /// <returns></returns>
        public Task DeleteUserNotificationAsync(int? notificationId, Guid userNotificationId)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ�������û���֪ͨ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAllUserNotificationsAsync(UserIdentifier user)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��֪ͨ���û�
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(UserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            return Task.FromResult(new List<UserNotificationInfoWithNotificationInfo>());
        }

        /// <summary>
        /// ��ȡ�û�֪ͨ������
        /// </summary>
        /// <param name="user"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Task<int> GetUserNotificationCountAsync(UserIdentifier user, UserNotificationState? state = null)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ�û���֪ͨ
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="userNotificationId">����������</param>
        /// <returns></returns>
        public Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(int? tenantId, Guid userNotificationId)
        {
            return Task.FromResult((UserNotificationInfoWithNotificationInfo)null);
        }

        /// <summary>
        /// Ϊ�⻧����֪ͨ
        /// </summary>
        /// <param name="tenantNotificationInfo"></param>
        /// <returns></returns>
        public Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ��֪ͨ
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public Task DeleteNotificationAsync(NotificationInfo notification)
        {
            return Task.FromResult(0);
        }
    }
}