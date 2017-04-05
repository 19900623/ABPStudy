using System;
using Abp.Reflection;

namespace Abp.Domain.Entities
{
    /// <summary>
    /// Some helper methods for entities.
    /// ʵ�������
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// ��������Ƿ�̳�/ʵ�ֻ������ͽӿ�
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEntity(Type type)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, typeof (IEntity<>));
        }

        /// <summary>
        /// ��ȡ����ʵ�����͵���������
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof (TEntity));
        }

        /// <summary>
        /// Gets primary key type of given entity type
        /// ��ȡ����ʵ�����͵���������
        /// </summary>
        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof (IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            throw new AbpException("Can not find primary key type of given entity type: " + entityType + ". Be sure that this entity type implements IEntity<TPrimaryKey> interface");
        }
    }
}