namespace Derrick.Authorization.Permissions.Dto
{
    /// <summary>
    /// ƽ��Ȩ�޼���Dto
    /// </summary>
    public class FlatPermissionWithLevelDto : FlatPermissionDto
    {
        /// <summary> 
        /// ����
        /// </summary>
        public int Level { get; set; }
    }
}