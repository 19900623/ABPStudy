using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.GraphDiff.Mapping;
using Abp.EntityFramework.Repositories;
using RefactorThis.GraphDiff;

namespace Abp.EntityFramework.GraphDiff.Extensions
{
    /// <summary>
    /// This class is an extension for GraphDiff library which provides a possibility to attach a detached graphs (i.e. entities) to a context.
    /// ������GraphDiff���һ����չ�����ṩΪ�����ĸ���һ�������graphs(��ʵ��)�Ŀ����ԡ�
    /// Attaching a whole graph using this methods updates all entity's navigation properties on entity creation or modification.
    /// ʹ�ô˷�������graph����ʵ�崴�����޸�ʱ��������ʵ��ĵ�������
    /// </summary>
    public static class GraphDiffExtensions
    {
        /// <summary>
        /// Attaches an <paramref name="entity"/> (as a detached graph) to a context.
        /// Ϊ�����ĸ���һ��<paramref name="entity"/>(��Ϊһ�������graph)
        /// </summary>
        /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
        /// <typeparam name="TPrimaryKey">Primary key type of the entity / ӵ��������ʵ������</typeparam>
        /// <param name="repository">�ִ��ӿ�</param>
        /// <param name="entity">ʵ��</param>
        /// <returns>Attached entity / ���ӵ�ʵ��</returns>
        public static TEntity AttachGraph<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var iocResolver = ((AbpRepositoryBase<TEntity, TPrimaryKey>)repository).IocResolver;

            using (var mappingManager = iocResolver.ResolveAsDisposable<IEntityMappingManager>())
            {
                var mapping = mappingManager.Object.GetEntityMappingOrNull<TEntity>();
                return repository
                    .GetDbContext()
                    .UpdateGraph(entity, mapping);
            }
        }

        /// <summary>
        /// Attaches an <paramref name="entity"/> (as a detached graph) to a context.
        /// Ϊ�����ĸ���һ��<paramref name="entity"/>(��Ϊһ�������graph)
        /// </summary>
        /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
        /// <typeparam name="TPrimaryKey">Primary key type of the entity / ӵ��������ʵ������</typeparam>
        /// <param name="repository">�ִ��ӿ�</param>
        /// <param name="entity">ʵ��</param>
        /// <returns>Attached entity / ���ӵ�ʵ��</returns>
        public static Task<TEntity> AttachGraphAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            return Task.FromResult(AttachGraph(repository, entity));
        }

        /// <summary>
        /// Attaches an <paramref name="entity"/> (as a detached graph) to a context and gets it's Id.
        /// Ϊ�����ĸ���һ��<paramref name="entity"/>(��Ϊһ�������graph),���һ�ȡ����Id
        /// It may require to save current unit of work to be able to retrieve id.
        /// ��������Ҫ���浱ǰ�Ĺ�����Ԫ���Ա��ָܻ�ID
        /// </summary>
        /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
        /// <typeparam name="TPrimaryKey">Primary key type of the entity / ӵ��������ʵ������</typeparam>
        /// <param name="repository">�ִ��ӿ�</param>
        /// <param name="entity">ʵ��</param>
        /// <returns>Id of the entity / ���ӵ�ʵ��</returns>
        public static TPrimaryKey AttachGraphAndGetId<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            return AttachGraph(repository, entity).Id;
        }

        /// <summary>
        /// Attaches an <paramref name="entity"/> (as a detached graph) to a context and gets it's Id.
        /// Ϊ�����ĸ���һ��<paramref name="entity"/>(��Ϊһ�������graph),���һ�ȡ����Id
        /// It may require to save current unit of work to be able to retrieve id.
        /// ��������Ҫ���浱ǰ�Ĺ�����Ԫ���Ա��ָܻ�ID
        /// </summary>
        /// <typeparam name="TEntity">Entity type / ʵ������</typeparam>
        /// <typeparam name="TPrimaryKey">Primary key type of the entity /ӵ��������ʵ������</typeparam>
        /// <param name="repository">�ִ��ӿ�</param>
        /// <param name="entity">ʵ��</param>
        /// <returns>Id of the entity / ���ӵ�ʵ��</returns>
        public static Task<TPrimaryKey> AttachGraphAndGetIdAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TEntity entity)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            return Task.FromResult(AttachGraphAndGetId(repository, entity));
        }
    }
}