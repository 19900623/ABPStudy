namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Defines interface that must be implemented by classes those must be validated with custom rules.So, implementing class can define it's own validation logic.
    /// Ϊ��Ҫ�Զ�����֤������ඨ��ӿڣ����ʵ�ִ����ܶ����Լ�����֤�߼�
    /// </summary>
    public interface ICustomValidate
    {
        /// <summary>
        /// This method is used to validate the object.
        /// �˷���������֤����
        /// </summary>
        /// <param name="context">Validation context. / �Զ�����֤������</param>
        void AddValidationErrors(CustomValidationContext context);
    }
}