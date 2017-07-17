using System.ComponentModel.DataAnnotations;

namespace Derrick.Organizations.Dto
{
    /// <summary>
    /// �ƶ���֯�ܹ�Input
    /// </summary>
    public class MoveOrganizationUnitInput
    {
        /// <summary>
        /// ID
        /// </summary>
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        /// <summary>
        /// �¸�ID
        /// </summary>
        public long? NewParentId { get; set; }
    }
}