using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Derrick.Friendships.Dto
{
    /// <summary>
    /// ������������Input
    /// </summary>
    public class CreateFriendshipRequestInput
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }
        /// <summary>
        /// �̻�ID
        /// </summary>
        public int? TenantId { get; set; }
    }
}