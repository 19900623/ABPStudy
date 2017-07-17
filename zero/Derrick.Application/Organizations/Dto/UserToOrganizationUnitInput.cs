using System.ComponentModel.DataAnnotations;

namespace Derrick.Organizations.Dto
{
    /// <summary>
    /// �û�����֯Input
    /// </summary>
    public class UserToOrganizationUnitInput
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }
        /// <summary>
        /// ��֯ID
        /// </summary>
        [Range(1, long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}