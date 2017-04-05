using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// ���ڷַ�֪ͨ���û�
    /// </summary>
    public interface INotificationDistributer : IDomainService
    {
        /// <summary>
        /// Distributes given notification to users.
        /// �ַ�֪ͨ���û�
        /// </summary>
        /// <param name="notificationId">The notification id. / ֪ͨID</param>
        Task DistributeAsync(Guid notificationId);
    }
}