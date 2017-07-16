using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Derrick.Authorization.Users.Dto
{
    /// <summary>
    /// ���ӵ��û�Input
    /// </summary>
    public class LinkToUserInput 
    {
        /// <summary>
        /// �̻�����
        /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// �û���������
        /// </summary>
        [Required]
        public string UsernameOrEmailAddress { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}