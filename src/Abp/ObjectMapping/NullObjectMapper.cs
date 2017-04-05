using Abp.Dependency;

namespace Abp.ObjectMapping
{
    /// <summary>
    /// <see cref="NULL"/>����ӳ����
    /// </summary>
    public sealed class NullObjectMapper : IObjectMapper, ISingletonDependency
    {
        /// <summary>
        /// Singleton instance.
        /// ��ʵ��
        /// </summary>
        public static NullObjectMapper Instance { get { return SingletonInstance; } }
        private static readonly NullObjectMapper SingletonInstance = new NullObjectMapper();

        /// <summary>
        /// ת��һ����������һ�����󡣴���һ���µ�<see cref="TDestination"/>����
        /// </summary>
        /// <typeparam name="TDestination">Ŀ����������</typeparam>
        /// <param name="source">ԭ����</param>
        /// <returns></returns>
        public TDestination Map<TDestination>(object source)
        {
            throw new AbpException("Abp.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }

        /// <summary>
        /// ִ��һ��ӳ���ԭ����һ���Ѵ��ڵ�Ŀ�����
        /// </summary>
        /// <typeparam name="TSource">ԭ��������</typeparam>
        /// <typeparam name="TDestination">Ŀ���������</typeparam>
        /// <param name="source">ԭ����</param>
        /// <param name="destination">Ŀ�����</param>
        /// <returns>ӳ�������Ķ���</returns>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new AbpException("Abp.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }
    }
}