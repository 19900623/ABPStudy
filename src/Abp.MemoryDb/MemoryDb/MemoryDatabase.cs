using System;
using System.Collections.Generic;
using Abp.Dependency;
using Abp.Modules;

namespace Abp.MemoryDb
{
    /// <summary>
    /// �ڴ����ݿ����
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class MemoryDatabase : ISingletonDependency
    {
        private readonly Dictionary<Type, object> _sets;

        private readonly object _syncObj = new object();

        /// <summary>
        /// ���캯��
        /// </summary>
        public MemoryDatabase()
        {
            _sets = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Setֵ
        /// </summary>
        /// <typeparam name="TEntity">ֵ����</typeparam>
        /// <returns></returns>
        public List<TEntity> Set<TEntity>()
        {
            var entityType = typeof(TEntity);

            lock (_syncObj)
            {
                if (!_sets.ContainsKey(entityType))
                {
                    _sets[entityType] = new List<TEntity>();
                }

                return _sets[entityType] as List<TEntity>;
            }
        }
    }
}