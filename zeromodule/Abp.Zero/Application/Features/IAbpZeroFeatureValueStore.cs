using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// ABP Zero����ֵ�洢
    /// </summary>
    public interface IAbpZeroFeatureValueStore : IFeatureValueStore
    {
        /// <summary>
        /// ��ȡֵ(û���򷵻�Null)
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="featureName">��������</param>
        /// <returns></returns>
        Task<string> GetValueOrNullAsync(int tenantId, string featureName);
        /// <summary>
        /// ��ȡ�汾ֵ(û���򷵻�Null)
        /// </summary>
        /// <param name="editionId">�⻧ID</param>
        /// <param name="featureName">��������</param>
        /// <returns></returns>
        Task<string> GetEditionValueOrNullAsync(int editionId, string featureName);
        /// <summary>
        /// ���ð汾����ֵ
        /// </summary>
        /// <param name="editionId">�̻�ID</param>
        /// <param name="featureName">��������</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        Task SetEditionFeatureValueAsync(int editionId, string featureName, string value);
    }
}