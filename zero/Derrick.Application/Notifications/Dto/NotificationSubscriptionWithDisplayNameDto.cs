using Abp.AutoMapper;
using Abp.Notifications;

namespace Derrick.Notifications.Dto
{
    /// <summary>
    /// ����ʾ���Ķ���֪ͨDto
    /// </summary>
    [AutoMapFrom(typeof(NotificationDefinition))]
    public class NotificationSubscriptionWithDisplayNameDto : NotificationSubscriptionDto
    {
        /// <summary>
        /// ��ʾ�� 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
    }
}