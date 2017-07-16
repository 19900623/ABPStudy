using System.Threading.Tasks;
using Abp;
using Abp.Localization;
using Abp.Notifications;
using Derrick.Authorization.Users;
using Derrick.MultiTenancy;

namespace Derrick.Notifications
{
    /// <summary>
    /// <see cref="IAppNotifier"/>ʵ�֣�APP֪ͨ��
    /// </summary>
    public class AppNotifier : AbpZeroTemplateDomainServiceBase, IAppNotifier
    {
        /// <summary>
        /// ֪ͨ������
        /// </summary>
        private readonly INotificationPublisher _notificationPublisher;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="notificationPublisher">֪ͨ������</param>
        public AppNotifier(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        /// <summary>
        /// ��ӭ����Ӧ�ó���
        /// </summary>
        /// <param name="user">�û�����</param>
        /// <returns></returns>
        public async Task WelcomeToTheApplicationAsync(User user)
        {
            await _notificationPublisher.PublishAsync(
                AppNotificationNames.WelcomeToTheApplication,
                new MessageNotificationData(L("WelcomeToTheApplicationNotificationMessage")),
                severity: NotificationSeverity.Success,
                userIds: new[] { user.ToUserIdentifier() }
                );
        }
        /// <summary>
        /// ���û�ע��
        /// </summary>
        /// <param name="user">�û�����</param>
        /// <returns></returns>
        public async Task NewUserRegisteredAsync(User user)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "NewUserRegisteredNotificationMessage",
                    AbpZeroTemplateConsts.LocalizationSourceName
                    )
                );

            notificationData["userName"] = user.UserName;
            notificationData["emailAddress"] = user.EmailAddress;

            await _notificationPublisher.PublishAsync(AppNotificationNames.NewUserRegistered, notificationData, tenantIds: new[] { user.TenantId });
        }
        /// <summary>
        /// ���̻�ע��
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <returns></returns>
        public async Task NewTenantRegisteredAsync(Tenant tenant)
        {
            var notificationData = new LocalizableMessageNotificationData(
                new LocalizableString(
                    "NewTenantRegisteredNotificationMessage",
                    AbpZeroTemplateConsts.LocalizationSourceName
                    )
                );

            notificationData["tenancyName"] = tenant.TenancyName;
            await _notificationPublisher.PublishAsync(AppNotificationNames.NewTenantRegistered, notificationData);
        }

        //This is for test purposes
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="user">�û���ʶ</param>
        /// <param name="message">��Ϣ</param>
        /// <param name="severity">֪ͨ�ļ���</param>
        /// <returns></returns>
        public async Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info)
        {
            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: severity,
                userIds: new[] { user }
                );
        }
    }
}