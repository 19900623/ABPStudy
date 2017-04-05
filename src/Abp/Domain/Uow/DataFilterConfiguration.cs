using System.Collections.Generic;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// ���ݹ�������
    /// </summary>
    public class DataFilterConfiguration
    {
        /// <summary>
        /// ����������
        /// </summary>
        public string FilterName { get; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// ����������
        /// </summary>
        public IDictionary<string, object> FilterParameters { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="isEnabled">�Ƿ�����</param>
        public DataFilterConfiguration(string filterName, bool isEnabled)
        {
            FilterName = filterName;
            IsEnabled = isEnabled;
            FilterParameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// ��¡һ�����ݹ��Ƕ���
        /// </summary>
        /// <param name="filterToClone">����¡�Ķ���</param>
        /// <param name="isEnabled">�Ƿ�����</param>
        internal DataFilterConfiguration(DataFilterConfiguration filterToClone, bool? isEnabled = null)
            : this(filterToClone.FilterName, isEnabled ?? filterToClone.IsEnabled)
        {
            foreach (var filterParameter in filterToClone.FilterParameters)
            {
                FilterParameters[filterParameter.Key] = filterParameter.Value;
            }
        }
    }
}