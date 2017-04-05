using System.Collections.Generic;
using Abp.Threading;

namespace Abp.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="INotificationDefinitionManager"/>.
    /// <see cref="INotificationDefinitionManager"/>����չ����
    /// </summary>
    public static class NotificationDefinitionManagerExtensions
    {
        /// <summary>
        /// Gets all available notification definitions for given user.
        /// Ϊָ�����û���ȡ���п��õ�֪ͨ����
        /// </summary>
        /// <param name="notificationDefinitionManager">Notification definition manager / ֪ͨ���������</param>
        /// <param name="user">User</param>
        public static IReadOnlyList<NotificationDefinition> GetAllAvailable(this INotificationDefinitionManager notificationDefinitionManager, UserIdentifier user)
        {
            return AsyncHelper.RunSync(() => notificationDefinitionManager.GetAllAvailableAsync(user));
        }
    }
}