using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;

namespace Derrick.Authorization.Users.Profile.Dto
{
    /// <summary>
    /// �޸�����Input
    /// </summary>
    public class ChangePasswordInput
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        [Required]
        [DisableAuditing]
        public string CurrentPassword { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [Required]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}