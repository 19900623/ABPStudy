namespace Derrick.Configuration.Host.Dto
{
    /// <summary>
    /// �̻��������ñ༭Dto
    /// </summary>
    public class TenantManagementSettingsEditDto
    {
        /// <summary>
        /// �Ƿ�������ע��
        /// </summary>
        public bool AllowSelfRegistration { get; set; }
        /// <summary>
        /// Ĭ��������Ƿ񼤻����̻�
        /// </summary>
        public bool IsNewRegisteredTenantActiveByDefault { get; set; }
        /// <summary>
        /// �Ƿ�ʹ����֤��ע��
        /// </summary>
        public bool UseCaptchaOnRegistration { get; set; }
        /// <summary>
        /// Ĭ�ϰ汾ID
        /// </summary>
        public int? DefaultEditionId { get; set; }
        
    }
}