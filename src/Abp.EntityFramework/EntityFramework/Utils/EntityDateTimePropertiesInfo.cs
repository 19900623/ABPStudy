using System.Collections.Generic;
using System.Reflection;

namespace Abp.EntityFramework.Utils
{
    /// <summary>
    /// ʵ��ʱ��������Ϣ
    /// </summary>
    public class EntityDateTimePropertiesInfo
    {
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public List<PropertyInfo> DateTimePropertyInfos { get; set; }

        /// <summary>
        /// ����������·��
        /// </summary>
        public List<string> ComplexTypePropertyPaths { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public EntityDateTimePropertiesInfo()
        {
            DateTimePropertyInfos = new List<PropertyInfo>();
            ComplexTypePropertyPaths = new List<string>();
        }
    }
}