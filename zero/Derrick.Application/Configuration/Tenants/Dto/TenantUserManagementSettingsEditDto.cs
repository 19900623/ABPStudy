namespace Derrick.Configuration.Tenants.Dto
{
    /// <summary>
    /// �̻��û��������ñ༭Dto
    /// </summary>
    public class TenantUserManagementSettingsEditDto
    {
        /// <summary>
        /// �Ƿ�������ע��
        /// </summary>
        public bool AllowSelfRegistration { get; set; }
        /// <summary>
        /// �Ƿ�Ĭ�ϼ�����ע���û�
        /// </summary>
        public bool IsNewRegisteredUserActiveByDefault { get; set; }
        /// <summary>
        /// ��¼ʱ�Ƿ���Ҫ����ȷ��
        /// </summary>
        public bool IsEmailConfirmationRequiredForLogin { get; set; }
        /// <summary>
        /// �Ƿ�ʹ����֤��ע��
        /// </summary>
        public bool UseCaptchaOnRegistration { get; set; }
    }
}