using System.Collections.Generic;
using Abp.MultiTenancy;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// �����ַ�����������
    /// </summary>
    public class ConnectionStringResolveArgs : Dictionary<string, object>
    {
        /// <summary>
        /// ���⻧˫��
        /// </summary>
        public MultiTenancySides? MultiTenancySide { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="multiTenancySide"></param>
        public ConnectionStringResolveArgs(MultiTenancySides? multiTenancySide = null)
        {
            MultiTenancySide = multiTenancySide;
        }
    }
}