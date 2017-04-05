using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Extension methods for <see cref="IIocRegistrar"/> interface.
    /// <see cref="IIocRegistrar"/> �ӿڵ���չ����.
    /// </summary>
    public static class IocRegistrarExtensions
    {
        #region RegisterIfNot

        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// ���֮ǰû��ע�ᣬ����ע��һ������
        /// </summary>
        /// <typeparam name="T">Type of the class / ע���������</typeparam>
        /// <param name="iocRegistrar">Registrar / ע������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / ���Ͷ������������</param>
        /// <returns>True, if registered for given implementation. / ����true,���ע��Ϊ������ʵ�֡�</returns>
        public static bool RegisterIfNot<T>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class
        {
            if (iocRegistrar.IsRegistered<T>())
            {
                return false;
            }

            iocRegistrar.Register<T>(lifeStyle);
            return true;
        }

        /// <summary>
        /// Registers a type as self registration if it's not registered before.
        /// ���֮ǰû��ע�ᣬ����ע��һ������
        /// </summary>
        /// <param name="iocRegistrar">Registrar / ע������</param>
        /// <param name="type">Type of the class / ע���������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / ���Ͷ������������</param>
        /// <returns>True, if registered for given implementation. / ����true,���ע��Ϊ������ʵ�֡�</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, lifeStyle);
            return true;
        }

        /// <summary>
        /// Registers a type with it's implementation if it's not registered before.
        /// ���֮ǰû��ע��ָ�����͵�ʵ�֣���ע����
        /// </summary>
        /// <typeparam name="TType">Registering type / ע�������</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> / ���� <see cref="TType"/>��ʵ��</typeparam>
        /// <param name="iocRegistrar">Registrar / ע������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / ע�����͵Ķ������������</param>
        /// <returns>True, if registered for given implementation. / ����true,���ע��Ϊ������ʵ�֡�</returns>
        public static bool RegisterIfNot<TType, TImpl>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return false;
            }

            iocRegistrar.Register<TType, TImpl>(lifeStyle);
            return true;
        }


        /// <summary>
        /// Registers a type with it's implementation if it's not registered before.
        /// ���֮ǰû��ע��ָ�����͵�ʵ�֣���ע����
        /// </summary>
        /// <param name="iocRegistrar">Registrar / ע������</param>
        /// <param name="type">Type of the class / ע�������</param>
        /// <param name="impl">The type that implements <paramref name="type"/> / ����</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / ע�����͵Ķ������������</param>
        /// <returns>True, if registered for given implementation. / ����true,���ע��Ϊ������ʵ�֡�</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, impl, lifeStyle);
            return true;
        }

        #endregion
    }
}