using Abp.AutoMapper;

namespace Derrick.Authorization.Permissions.Dto
{
    /// <summary>
    /// ͳһȨ��Dto
    /// </summary>
    [AutoMapFrom(typeof(Abp.Authorization.Permission))] 
    public class FlatPermissionDto
    {
        /// <summary>
        /// ��Name
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// �Ƿ�Ĭ������
        /// </summary>
        public bool IsGrantedByDefault { get; set; }
    }
}