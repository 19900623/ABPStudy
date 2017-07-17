using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;

namespace Derrick.Organizations.Dto
{
    /// <summary>
    /// ��֯�ܹ�Dto
    /// </summary>
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : AuditedEntityDto<long>
    {
        /// <summary>
        /// ��ID
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ��Ա����
        /// </summary>
        public int MemberCount { get; set; }
    }
}