namespace Abp.Zero.Configuration
{
    /// <summary>
    /// ABP Zero��������
    /// </summary>
    public static class AbpZeroSettingNames
    {
        /// <summary>
        /// �û�����
        /// </summary>
        public static class UserManagement
        {
            /// <summary>
            /// ��¼ʱ������ʼ��Ƿ���Ҫȷ��
            /// </summary>
            public const string IsEmailConfirmationRequiredForLogin = "Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin";

            /// <summary>
            /// �û�����
            /// </summary>
            public static class UserLockOut
            {
                /// <summary>
                /// �û������Ƿ�����
                /// </summary>
                public const string IsEnabled = "Abp.Zero.UserManagement.UserLockOut.IsEnabled";

                /// <summary>
                /// ����ǰ���ʧ�ܷ��ʳ���
                /// </summary>
                public const string MaxFailedAccessAttemptsBeforeLockout = "Abp.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout";

                /// <summary>
                /// �˻�Ĭ��������
                /// </summary>
                public const string DefaultAccountLockoutSeconds = "Abp.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds";
            }
            /// <summary>
            /// ˫���ӵ�¼
            /// </summary>
            public static class TwoFactorLogin
            {
                /// <summary>
                /// �Ƿ�����˫���ӵ�¼
                /// </summary>
                public const string IsEnabled = "Abp.Zero.UserManagement.TwoFactorLogin.IsEnabled";

                /// <summary>
                /// �Ƿ����õ����ʼ��ṩ����
                /// </summary>
                public const string IsEmailProviderEnabled = "Abp.Zero.UserManagement.TwoFactorLogin.IsEmailProviderEnabled";

                /// <summary>
                /// ˫���ӵ�¼�Ƿ��ṩ���ŷ���
                /// </summary>
                public const string IsSmsProviderEnabled = "Abp.Zero.UserManagement.TwoFactorLogin.IsSmsProviderEnabled";

                /// <summary>
                /// ������Ƿ��ס
                /// </summary>
                public const string IsRememberBrowserEnabled = "Abp.Zero.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled";
            }
        }
        /// <summary>
        /// ��֯�ܹ�
        /// </summary>
        public static class OrganizationUnits
        {
            /// <summary>
            /// ��֯������Ա����
            /// </summary>
            public const string MaxUserMembershipCount = "Abp.Zero.OrganizationUnits.MaxUserMembershipCount";
        }
    }
}