using System.ComponentModel.DataAnnotations;

namespace Derrick.Chat.Dto
{
    /// <summary>
    /// �û�������ϢInput
    /// </summary>
    public class GetUserChatMessagesInput
    {
        /// <summary>
        /// �̻�ID
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// �û�ID
        /// </summary>
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }
        /// <summary>
        /// ��С��ϢID
        /// </summary>
        public long? MinMessageId { get; set; }
    }
}