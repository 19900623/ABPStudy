namespace Abp.Authorization.Users
{
    /// <summary>
    /// ABP��¼�������
    /// </summary>
    public enum AbpLoginResultType : byte
    {
        /// <summary>
        /// �ɹ�
        /// </summary>
        Success = 1,
        /// <summary>
        /// ��Ч�ĵ����ʼ����û���
        /// </summary>
        InvalidUserNameOrEmailAddress,
        /// <summary>
        /// ��Ч����
        /// </summary>
        InvalidPassword,
        /// <summary>
        /// �û�δ����
        /// </summary>
        UserIsNotActive,
        /// <summary>
        /// ��Ч���̻���
        /// </summary>
        InvalidTenancyName,
        /// <summary>
        /// �̻�δ����
        /// </summary>
        TenantIsNotActive,
        /// <summary>
        /// �û��ʼ�δȷ��
        /// </summary>
        UserEmailIsNotConfirmed,
        /// <summary>
        /// δ֪���ⲿ��¼
        /// </summary>
        UnknownExternalLogin,
        /// <summary>
        /// ����
        /// </summary>
        LockedOut
    }
}