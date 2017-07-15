namespace Derrick.Configuration
{
    /// <summary>
    /// ����Ӧ�ó����ڵ��������Ƴ�����ͨ��<see cref="AppSettingProvider"/>�鿴���ö���
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// ��������
        /// </summary>
        public static class General
        {
            public const string WebSiteRootAddress = "App.General.WebSiteRootAddress";
        }

        /// <summary>
        /// �̻�����
        /// </summary>
        public static class TenantManagement
        {
            public const string AllowSelfRegistration = "App.TenantManagement.AllowSelfRegistration";
            public const string IsNewRegisteredTenantActiveByDefault = "App.TenantManagement.IsNewRegisteredTenantActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.TenantManagement.UseCaptchaOnRegistration";
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition";
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public static class UserManagement
        {
            public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";
            public const string IsNewRegisteredUserActiveByDefault = "App.UserManagement.IsNewRegisteredUserActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";
        }

        /// <summary>
        /// ��ȫ����
        /// </summary>
        public static class Security
        {
            public const string PasswordComplexity = "App.Security.PasswordComplexity";
        }
    }
}