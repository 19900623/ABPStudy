using System;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Used to declare multi tenancy side of an object.
    /// ������������Ķ��⻧���
    /// </summary>
    public class MultiTenancySideAttribute : Attribute
    {
        /// <summary>
        /// Multitenancy side.
        /// ���⻧˫���е�һ��
        /// </summary>
        public MultiTenancySides Side { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenancySideAttribute"/> class.
        /// ��ʼ��<see cref="MultiTenancySideAttribute"/> ���һ����ʵ��
        /// </summary>
        /// <param name="side">Multitenancy side. / ���⻧˫���е�һ��</param>
        public MultiTenancySideAttribute(MultiTenancySides side)
        {
            Side = side;
        }
    }
}