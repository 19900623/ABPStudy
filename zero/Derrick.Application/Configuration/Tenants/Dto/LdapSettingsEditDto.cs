namespace Derrick.Configuration.Tenants.Dto
{
    /// <summary>
    /// Ldap���ñ༭Dto
    /// </summary>
    public class LdapSettingsEditDto
    {
        /// <summary>
        /// �Ƿ�����ģ��
        /// </summary>
        public bool IsModuleEnabled { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Password { get; set; }
    }
}