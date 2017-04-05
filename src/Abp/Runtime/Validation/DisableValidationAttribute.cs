using System;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Can be added to a method to disable auto validation.
    /// ������ӵ������Զ���֤�ķ�����
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    public class DisableValidationAttribute : Attribute
    {
        
    }
}