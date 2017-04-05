using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Dependency;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// �Զ�����֤������
    /// </summary>
    public class CustomValidationContext
    {
        /// <summary>
        /// List of validation results (errors). Add validation errors to this list.
        /// ��֤���������ϣ������֤�����������
        /// </summary>
        public List<ValidationResult> Results { get; }

        /// <summary>
        /// Can be used to resolve dependencies on validation.
        /// �������ڽ�����֤������
        /// </summary>
        public IIocResolver IocResolver { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="results">��֤���������ϣ������֤�����������</param>
        /// <param name="iocResolver">�������ڽ�����֤������</param>
        public CustomValidationContext(List<ValidationResult> results, IIocResolver iocResolver)
        {
            Results = results;
            IocResolver = iocResolver;
        }
    }
}