using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;

namespace Abp.EntityFramework
{
    /// <summary>
    /// <see cref="IDbContextTypeMatcher"/>��Ĭ��ʵ��
    /// </summary>
    /// <typeparam name="TBaseDbContext">���ݿ������Ķ���</typeparam>
    public abstract class DbContextTypeMatcher<TBaseDbContext> : IDbContextTypeMatcher, ISingletonDependency
    {
        /// <summary>
        /// ��ǰ�Ĺ�����Ԫ�ṩ��
        /// </summary>
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// ���ݿ������������ֵ�
        /// </summary>
        private readonly Dictionary<Type, List<Type>> _dbContextTypes;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="currentUnitOfWorkProvider">��ǰ�Ĺ�����Ԫ�ṩ��</param>
        protected DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _dbContextTypes = new Dictionary<Type, List<Type>>();
        }

        /// <summary>
        /// ������ݿ�����������
        /// </summary>
        /// <param name="dbContextTypes">���ݿ�����������</param>
        public void Populate(Type[] dbContextTypes)
        {
            foreach (var dbContextType in dbContextTypes)
            {
                var types = new List<Type>();

                AddWithBaseTypes(dbContextType, types);

                foreach (var type in types)
                {
                    Add(type, dbContextType);
                }
            }
        }

        //TODO: GetConcreteType method can be optimized by extracting/caching MultiTenancySideAttribute attributes for DbContexes.
        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <param name="sourceDbContextType">Դ���ݿ�����������</param>
        /// <returns>��������</returns>
        public virtual Type GetConcreteType(Type sourceDbContextType)
        {
            //TODO: This can also get MultiTenancySide to filter dbcontexes

            if (!sourceDbContextType.IsAbstract)
            {
                return sourceDbContextType;
            }
            
            //Get possible concrete types for given DbContext type
            var allTargetTypes = _dbContextTypes.GetOrDefault(sourceDbContextType);

            if (allTargetTypes.IsNullOrEmpty())
            {
                throw new AbpException("Could not find a concrete implementation of given DbContext type: " + sourceDbContextType.AssemblyQualifiedName);
            }

            if (allTargetTypes.Count == 1)
            {
                //Only one type does exists, return it
                return allTargetTypes[0];
            }

            CheckCurrentUow();

            var currentTenancySide = GetCurrentTenancySide();

            var multiTenancySideContexes = GetMultiTenancySideContextTypes(allTargetTypes, currentTenancySide);

            if (multiTenancySideContexes.Count == 1)
            {
                return multiTenancySideContexes[0];
            }

            if (multiTenancySideContexes.Count > 1)
            {
                return GetDefaultDbContextType(multiTenancySideContexes, sourceDbContextType, currentTenancySide);
            }

            return GetDefaultDbContextType(allTargetTypes, sourceDbContextType, currentTenancySide);
        }

        /// <summary>
        /// ��鵱ǰ������Ԫ
        /// </summary>
        private void CheckCurrentUow()
        {
            if (_currentUnitOfWorkProvider.Current == null)
            {
                throw new AbpException("GetConcreteType method should be called in a UOW.");
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�⻧����
        /// </summary>
        /// <returns>���⻧˫���е�һ��</returns>
        private MultiTenancySides GetCurrentTenancySide()
        {
            return _currentUnitOfWorkProvider.Current.GetTenantId() == null
                       ? MultiTenancySides.Host
                       : MultiTenancySides.Tenant;
        }

        /// <summary>
        /// ��ȡ���⻧������������
        /// </summary>
        /// <param name="dbContextTypes">���ݿ�����������</param>
        /// <param name="tenancySide">���⻧˫���е�һ��</param>
        /// <returns>�����б�</returns>
        private static List<Type> GetMultiTenancySideContextTypes(List<Type> dbContextTypes, MultiTenancySides tenancySide)
        {
            return dbContextTypes.Where(type =>
            {
                var attrs = type.GetCustomAttributes(typeof(MultiTenancySideAttribute), true);
                if (attrs.IsNullOrEmpty())
                {
                    return false;
                }

                return ((MultiTenancySideAttribute)attrs[0]).Side.HasFlag(tenancySide);
            }).ToList();
        }

        /// <summary>
        /// ��ȡĬ�ϵ����ݿ�����������
        /// </summary>
        /// <param name="dbContextTypes">���ݿ��������б�</param>
        /// <param name="sourceDbContextType">Դ���ݿ�����������</param>
        /// <param name="tenancySide">���⻧˫���е�һ��</param>
        /// <returns>����</returns>
        private static Type GetDefaultDbContextType(List<Type> dbContextTypes, Type sourceDbContextType, MultiTenancySides tenancySide)
        {
            var filteredTypes = dbContextTypes
                .Where(type => !type.IsDefined(typeof(AutoRepositoryTypesAttribute), true))
                .ToList();

            if (filteredTypes.Count == 1)
            {
                return filteredTypes[0];
            }

            filteredTypes = filteredTypes
                .Where(type => !type.IsDefined(typeof(DefaultDbContextAttribute), true))
                .ToList();

            if (filteredTypes.Count == 1)
            {
                return filteredTypes[0];
            }

            throw new AbpException(string.Format(
                "Found more than one concrete type for given DbContext Type ({0}) define MultiTenancySideAttribute with {1}. Found types: {2}.",
                sourceDbContextType,
                tenancySide,
                dbContextTypes.Select(c => c.AssemblyQualifiedName).JoinAsString(", ")
                ));
        }

        /// <summary>
        /// ��ӻ�����
        /// </summary>
        /// <param name="dbContextType">���ݿ�����������</param>
        /// <param name="types">�����б�</param>
        private static void AddWithBaseTypes(Type dbContextType, List<Type> types)
        {
            types.Add(dbContextType);
            if (dbContextType != typeof(TBaseDbContext))
            {
                AddWithBaseTypes(dbContextType.BaseType, types);
            }
        }

        /// <summary>
        /// ������ݿ�����������
        /// </summary>
        /// <param name="sourceDbContextType">���Դ����</param>
        /// <param name="targetDbContextType">���Ŀ������</param>
        private void Add(Type sourceDbContextType, Type targetDbContextType)
        {
            if (!_dbContextTypes.ContainsKey(sourceDbContextType))
            {
                _dbContextTypes[sourceDbContextType] = new List<Type>();
            }

            _dbContextTypes[sourceDbContextType].Add(targetDbContextType);
        }
    }
}