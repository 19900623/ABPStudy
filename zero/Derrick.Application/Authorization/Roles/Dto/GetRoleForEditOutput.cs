using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Derrick.Authorization.Permissions.Dto;

namespace Derrick.Authorization.Roles.Dto
{
    /// <summary>
    /// ��ȡ�༭��ɫOutput
    /// </summary>
    public class GetRoleForEditOutput
    {
        /// <summary>
        /// �༭��ɫDto
        /// </summary>
        public RoleEditDto Role { get; set; }
        /// <summary>
        /// Ȩ��Dto�б�
        /// </summary>
        public List<FlatPermissionDto> Permissions { get; set; }
        /// <summary>
        /// ����Ȩ�����Ƽ���
        /// </summary>
        public List<string> GrantedPermissionNames { get; set; }
    }
}