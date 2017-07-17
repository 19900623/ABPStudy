using System.ComponentModel.DataAnnotations;

namespace Derrick.Friendships.Dto
{
    /// <summary>
    /// ͨ���û���������������Input
    /// </summary>
    public class CreateFriendshipRequestByUserNameInput
    {
        /// <summary>
        /// �̻�����
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string TenancyName { get; set; }
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }
    }
}