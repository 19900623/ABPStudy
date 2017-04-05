namespace Abp.Runtime.Validation
{
    /// <summary>
    /// This interface is used to normalize inputs before method execution.
    /// �˽ӿ������ڷ���ִ��֮ǰ��������й淶����
    /// </summary>
    public interface IShouldNormalize
    {
        /// <summary>
        /// This method is called lastly before method execution (after validation if exists).
        /// �˷����ڷ���ִ��֮ǰ�����ã����������֤֮��
        /// </summary>
        void Normalize();
    }
}