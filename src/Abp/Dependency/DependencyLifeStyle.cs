namespace Abp.Dependency
{
    /// <summary>
    /// Lifestyles of types used in dependency injection system.
    /// ������ע��ϵͳ�����͵���������
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// Singleton object. Created a single object on first resolving and same instance is used for subsequent resolves.
        /// �������ڵ�һ�ν���ʱ��������֮��Ľ�����������ͬ�Ķ���
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient object. Created one object for every resolving.
        /// ʵʱ, Ϊÿһ�ν������󣬴���һ���µĶ���
        /// </summary>
        Transient
    }
}