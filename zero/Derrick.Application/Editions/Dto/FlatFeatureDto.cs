using Abp.Application.Features;
using Abp.AutoMapper;
using Abp.UI.Inputs;

namespace Derrick.Editions.Dto
{
    /// <summary>
    /// ƽ������Dto
    /// </summary>
    [AutoMapFrom(typeof(Feature))]
    public class FlatFeatureDto
    {
        /// <summary>
        /// ��Name
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// Input����
        /// </summary>
        public IInputType InputType { get; set; }
    }
}