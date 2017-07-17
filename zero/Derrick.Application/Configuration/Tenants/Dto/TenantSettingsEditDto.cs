using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Derrick.Configuration.Host.Dto;

namespace Derrick.Configuration.Tenants.Dto
{
    /// <summary>
    /// �̻����ñ༭Dto
    /// </summary>
    public class TenantSettingsEditDto
    {
        /// <summary>
        /// �������ñ༭Dto
        /// </summary>
        public GeneralSettingsEditDto General { get; set; }

        /// <summary>
        /// �̻��û��������ñ༭Dto
        /// </summary>
        [Required]
        public TenantUserManagementSettingsEditDto UserManagement { get; set; }

        /// <summary>
        /// �ʼ����ñ༭Dto
        /// </summary>
        public EmailSettingsEditDto Email { get; set; }

        /// <summary>
        /// Ldap���ñ༭Dto
        /// </summary>
        public LdapSettingsEditDto Ldap { get; set; }

        /// <summary>
        /// ��ȫ����Ա༭Dto
        /// </summary>
        [Required]
        public SecuritySettingsEditDto Security { get; set; }

        /// <summary>
        /// This validation is done for single-tenant applications.
        /// �����֤��Ϊ�����̻�Ӧ�ó������ġ�
        /// Because, these settings can only be set by tenant in a single-tenant application.
        /// ��Ϊ����Щ����ֻ�ܱ����̻�Ӧ�ó������á�
        /// </summary>
        public void ValidateHostSettings()
        {
            var validationErrors = new List<ValidationResult>();
            if (General == null)
            {
                validationErrors.Add(new ValidationResult("General settings can not be null", new[] { "General" }));
            }
            else
            {
                if (General.WebSiteRootAddress.IsNullOrEmpty())
                {
                    validationErrors.Add(new ValidationResult("General.WebSiteRootAddress can not be null or empty", new[] { "WebSiteRootAddress" }));
                }
            }

            if (Email == null)
            {
                validationErrors.Add(new ValidationResult("Email settings can not be null", new[] { "Email" }));
            }

            if (validationErrors.Count > 0)
            {
                throw new AbpValidationException("Method arguments are not valid! See ValidationErrors for details.", validationErrors);
            }
        }
    }
}