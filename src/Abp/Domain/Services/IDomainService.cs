using Abp.Dependency;

namespace Abp.Domain.Services
{
    /// <summary>
    /// This interface must be implemented by all domain services to identify them by convention.
    /// ���е�������������˽ӿڣ�ͨ��Լ����ʶ����
    /// </summary>
    public interface IDomainService : ITransientDependency
    {

    }
}