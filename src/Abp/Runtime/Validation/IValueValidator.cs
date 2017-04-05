using System.Collections.Generic;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// ֵ��֤��
    /// </summary>
    public interface IValueValidator
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="key">ͨ��key��ȡ����</param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// �����ֵ�
        /// </summary>
        IDictionary<string, object> Attributes { get; }

        /// <summary>
        /// �Ƿ���֤
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsValid(object value);
    }
}