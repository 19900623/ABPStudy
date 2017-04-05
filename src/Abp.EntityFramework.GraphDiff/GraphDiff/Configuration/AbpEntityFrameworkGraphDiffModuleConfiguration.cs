using System.Collections.Generic;
using Abp.EntityFramework.GraphDiff.Mapping;

namespace Abp.EntityFramework.GraphDiff.Configuration
{
    /// <summary>
    /// <see cref="IAbpEntityFrameworkGraphDiffModuleConfiguration"/>��Ĭ��ʵ��
    /// </summary>
    public class AbpEntityFrameworkGraphDiffModuleConfiguration : IAbpEntityFrameworkGraphDiffModuleConfiguration
    {
        /// <summary>
        /// ʵ��ӳ���б�
        /// </summary>
        public List<EntityMapping> EntityMappings { get; set; }
    }
}