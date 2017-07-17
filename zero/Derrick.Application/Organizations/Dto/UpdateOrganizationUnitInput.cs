using System.ComponentModel.DataAnnotations;
using Abp.Organizations;

namespace Derrick.Organizations.Dto
{
    /// <summary>
    /// ������֯�ܹ�Input
    /// </summary>
    public class UpdateOrganizationUnitInput
    {
        /// <summary>
        /// ID
        /// </summary>
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
    }
}