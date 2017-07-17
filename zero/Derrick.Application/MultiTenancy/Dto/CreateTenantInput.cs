using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using Derrick.Authorization.Users;

namespace Derrick.MultiTenancy.Dto
{
    /// <summary>
    /// �����̻�Input
    /// </summary>
    public class CreateTenantInput
    {
        /// <summary>
        /// �̻�����
        /// </summary>
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }
        /// <summary>
        /// ����Ա����
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }
        /// <summary>
        /// ����Ա����
        /// </summary>
        [StringLength(User.MaxPasswordLength)]
        public string AdminPassword { get; set; }
        /// <summary>
        /// �����ַ���
        /// </summary>
        [MaxLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }
        /// <summary>
        /// �Ƿ����´ε�½ʱ�޸�����
        /// </summary>
        public bool ShouldChangePasswordOnNextLogin { get; set; }
        /// <summary>
        /// ���ͼ�������
        /// </summary>
        public bool SendActivationEmail { get; set; }
        /// <summary>
        /// �汾ID
        /// </summary>
        public int? EditionId { get; set; }
        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }
    }
}