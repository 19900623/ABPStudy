using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Derrick.Editions.Dto;

namespace Derrick.MultiTenancy.Dto
{
    /// <summary>
    /// �̻����ܱ༭Output
    /// </summary>
    public class GetTenantFeaturesForEditOutput
    {
        /// <summary>
        /// ����ֵ�б�
        /// </summary>
        public List<NameValueDto> FeatureValues { get; set; }
        /// <summary>
        /// �����б�
        /// </summary>
        public List<FlatFeatureDto> Features { get; set; }
    }
}