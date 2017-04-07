namespace Abp.MemoryDb
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MemoryDatabase"/> object.
    /// ����һ���ӿ����ڻ��һ��<see cref="MemoryDatabase"/>����
    /// </summary>
    public interface IMemoryDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MemoryDatabase"/>.
        /// ��ȡ<see cref="MemoryDatabase"/>����
        /// </summary>
        MemoryDatabase Database { get; }
    }
}