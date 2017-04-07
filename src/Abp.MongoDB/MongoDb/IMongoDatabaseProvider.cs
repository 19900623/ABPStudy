using MongoDB.Driver;

namespace Abp.MongoDb
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MongoDatabase"/> object.
    /// ����һ���ӿ����ڻ��һ��<see cref="MongoDatabase"/>����
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MongoDatabase"/>.
        /// ��ȡ<see cref="MongoDatabase"/>����
        /// </summary>
        MongoDatabase Database { get; }
    }
}