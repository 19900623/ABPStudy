using Abp;
using Abp.Application.Services.Dto;

namespace Derrick.Authorization.Users.Dto
{
    /// <summary>
    /// �û��Ͽ�����Input
    /// </summary>
    public class UnlinkUserInput
    {
        /// <summary>
        /// �̻�ID
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// ת�����û���ʶ
        /// </summary>
        /// <returns></returns>
        public UserIdentifier ToUserIdentifier()
        {
            return new UserIdentifier(TenantId, UserId);
        }
    }
}