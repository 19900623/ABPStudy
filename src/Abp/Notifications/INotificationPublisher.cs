using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Runtime.Session;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to publish notifications.
    /// ���ڷ���֪ͨ
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Publishes a new notification.
        /// ����һ����֪ͨ
        /// </summary>
        /// <param name="notificationName">Unique notification name / Ψһ��֪ͨ����</param>
        /// <param name="data">Notification data (optional) / ֪ͨ����(��ѡ)</param>
        /// <param name="entityIdentifier">The entity identifier if this notification is related to an entity / ��ʶʵ��(���֪ͨ������һ��ʵ��)</param>
        /// <param name="severity">Notification severity / ֪ͨ���س̶�</param>
        /// <param name="userIds">
        /// Target user id(s). Used to send notification to specific user(s) (without checking the subscription). 
        /// Ŀ���û�ID,���ڷ���֪ͨ����ȷ���û�(û�м�鶩��)
        /// If this is null/empty, the notification is sent to subscribed users.
        /// �����null/�գ�֪ͨ���͸������û�
        /// </param>
        /// <param name="excludedUserIds">
        /// Excluded user id(s).This can be set to exclude some users while publishing notifications to subscribed users.
        /// �ų��û�ID���������û�����֪ͨʱ����������Ϊ�ų�ĳЩ�û�
        /// It's normally not set if <see cref="userIds"/> is set.
        /// ���<see cref="userIds"/>����������ͨ��������
        /// </param>
        /// <param name="tenantIds">
        /// Target tenant id(s).Used to send notification to subscribed users of specific tenant(s).
        /// Ŀ���⻧ID���������ض��⻧���Ͷ����û���֪ͨ
        /// This should not be set if <see cref="userIds"/> is set.
        /// ���<see cref="userIds"/>��ֵ���������Ӧ�ñ�����
        /// <see cref="NotificationPublisher.AllTenants"/> can be passed to indicate all tenants.
        /// <see cref="NotificationPublisher.AllTenants"/>����ͨ��ָʾ�����⻧
        /// But this can only work in a single database approach (all tenants are stored in host database).
        /// ������������������ڶ��⻧Ӧ�õ�һ���ݿ�ķ���(�����⻧�洢��һ�����ݿ�)
        /// If this is null, then it's automatically set to the current tenant on <see cref="IAbpSession.TenantId"/>.
        /// �����Ϊnullʱ�������Զ�����Ϊ��ǰ�⻧<see cref="IAbpSession.TenantId"/>
        /// </param>
        Task PublishAsync(
            string notificationName,
            NotificationData data = null,
            EntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            UserIdentifier[] userIds = null,
            UserIdentifier[] excludedUserIds = null,
            int?[] tenantIds = null);
    }
}