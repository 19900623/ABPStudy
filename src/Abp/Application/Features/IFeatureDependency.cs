using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a feature dependency.
    /// ���������ӿ�
    /// </summary>
    public interface IFeatureDependency
    {
        /// <summary>
        /// Checks depended features and returns true if dependencies are satisfied.
        /// �������������ģ�����������ܷ���True
        /// </summary>
        Task<bool> IsSatisfiedAsync(IFeatureDependencyContext context);
    }
}