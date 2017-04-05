using Abp.Dependency;

namespace Abp.Domain.Repositories
{
    /// <summary>
    /// This interface must be implemented by all repositories to identify them by convention.
    /// ͨ��Լ��ȥ��ʶ����ʵ�ִ˽ӿڵĲִ�
    /// Implement generic version instead of this one.
    /// ʵ�ַ��Ͱ汾��ȡ����
    /// </summary>
    public interface IRepository : ITransientDependency
    {
        
    }
}