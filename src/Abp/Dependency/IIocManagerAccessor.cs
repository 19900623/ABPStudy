namespace Abp.Dependency
{
    /// <summary>
    /// ִ������ע������ķ�����(IOC������������)
    /// </summary>
    public interface IIocManagerAccessor
    {
        /// <summary>
        /// IOC������
        /// </summary>
        IIocManager IocManager { get; }
    }
}