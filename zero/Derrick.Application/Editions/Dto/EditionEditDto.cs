using System.ComponentModel.DataAnnotations;
using Abp.Application.Editions;
using Abp.AutoMapper;

namespace Derrick.Editions.Dto
{
    /// <summary>
    /// �汾�༭Dto
    /// </summary>
    [AutoMap(typeof(Edition))]
    public class EditionEditDto
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
    }
}