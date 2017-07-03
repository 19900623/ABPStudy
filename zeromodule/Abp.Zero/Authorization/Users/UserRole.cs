using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Represents role record of a user. 
    /// ��ʾһ���û��Ľ�ɫ��¼
    /// </summary>
    [Table("AbpUserRoles")]
    public class UserRole : CreationAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// �̻�ID
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// �û�ID.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// ��ɫID.
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// ���캯��
        /// </summary>
        public UserRole()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="roleId">��ɫID</param>
        public UserRole(int? tenantId, long userId, int roleId)
        {
            TenantId = tenantId;
            UserId = userId;
            RoleId = roleId;
        }
    }
}