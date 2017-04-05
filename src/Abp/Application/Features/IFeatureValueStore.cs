using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a store to get feature values.
    /// ���幦��ֵ�Ĵ洢
    /// </summary>
    public interface IFeatureValueStore
    {
        /// <summary>
        /// Gets the feature value or null.
        /// ��ȡ����ֵ��null
        /// </summary>
        /// <param name="tenantId">The tenant id. / �⻧ID</param>
        /// <param name="feature">The feature. / ���ܶ���</param>
        Task<string> GetValueOrNullAsync(int tenantId, Feature feature);
    }
}