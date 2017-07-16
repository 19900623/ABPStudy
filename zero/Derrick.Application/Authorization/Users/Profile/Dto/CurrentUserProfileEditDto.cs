using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Derrick.Authorization.Users.Profile.Dto
{
    /// <summary>
    /// ��ǰ�û����ϱ༭Dto
    /// </summary>
    [AutoMap(typeof(User))]
    public class CurrentUserProfileEditDto
    {
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }
        /// <summary>
        /// �û���
        /// </summary>
        [Required]
        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }
        /// <summary>
        /// �ʼ���ַ
        /// </summary>
        [Required]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        /// <summary>
        /// �绰����
        /// </summary>
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        public string Timezone { get; set; }
    }
}