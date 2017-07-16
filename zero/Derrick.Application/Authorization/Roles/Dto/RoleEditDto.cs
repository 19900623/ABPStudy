using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Derrick.Authorization.Roles.Dto
{
    /// <summary>
    /// �༭��ɫDto
    /// </summary>
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        [Required]
        public string DisplayName { get; set; }
        /// <summary>
        /// �Ƿ���Ĭ��
        /// </summary>
        public bool IsDefault { get; set; }
    }
}