using System.ComponentModel.DataAnnotations;

namespace Derrick.Friendships.Dto
{
    /// <summary>
    /// ��ֹ�û�Input
    /// </summary>
    public class BlockUserInput 
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