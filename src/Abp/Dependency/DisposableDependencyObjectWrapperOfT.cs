namespace Abp.Dependency
{
    /// <summary>
    /// DisposableDependencyObjectWrapper{object}�ļ����ࡣ
    /// ��װ��IOC�����н������Ķ���
    /// ���̳����ӿ�<see cref="IDisposable"/>, ��ˣ����������Ķ��󣬽������ױ��ͷš�
    /// �ڷ���<see cref="IDisposable.Dispose"/>��,�����÷���<see cref="IIocResolver.Release"/>���ٶ���
    /// </summary>
    internal class DisposableDependencyObjectWrapper : DisposableDependencyObjectWrapper<object>, IDisposableDependencyObjectWrapper
    {
        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, object obj)
            : base(iocResolver, obj)
        {

        }
    }

    /// <summary>
    /// ��װ��IOC�����н������Ķ���
    /// ���̳����ӿ�<see cref="IDisposable"/>, ��ˣ����������Ķ��󣬽������ױ��ͷš�
    /// �ڷ���<see cref="IDisposable.Dispose"/>��,�����÷���<see cref="IIocResolver.Release"/>���ٶ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DisposableDependencyObjectWrapper<T> : IDisposableDependencyObjectWrapper<T>
    {
        /// <summary>
        /// IOC������
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// ��IOC�����н������Ķ���
        /// </summary>
        public T Object { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">IOC������</param>
        /// <param name="obj">��IOC�����н������Ķ���</param>
        public DisposableDependencyObjectWrapper(IIocResolver iocResolver, T obj)
        {
            _iocResolver = iocResolver;
            Object = obj;
        }

        /// <summary>
        /// �ͷŶ���
        /// </summary>
        public void Dispose()
        {
            _iocResolver.Release(Object);
        }
    }
}