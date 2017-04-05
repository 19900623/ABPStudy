using System;
using System.Reflection;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to register dependencies.
    /// ����ע������ע����Ľӿ�
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// ���һ������Լ��ע�������ע����
        /// </summary>
        /// <param name="registrar">dependency registrar / ����ע����</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// ͨ�����е�Լ��ע������ע��������򼯵����ͣ��鿴����<see cref="IocManager.AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">Assembly to register / ע��ĳ���</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// ͨ�����е�Լ��ע������ע��������򼯵����ͣ��鿴����<see cref="IocManager.AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">Assembly to register / ע��ĳ���</param>
        /// <param name="config">Additional configuration / ���������</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);

        /// <summary>
        /// Registers a type as self registration.
        /// ��ע��һ��ע������
        /// </summary>
        /// <typeparam name="T">Type of the class / ע���������</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// Registers a type as self registration.
        /// ��ע��һ��ע������
        /// </summary>
        /// <param name="type">Type of the class / ע���������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Registers a type with it's implementation.
        /// ע��һ�����ͺ�����ʵ��
        /// </summary>
        /// <typeparam name="TType">Registering type / ע���������</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> / �� <see cref="TType"/>��ʵ��</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// Registers a type with it's implementation.
        /// ע��һ�����ͺ�����ʵ��
        /// </summary>
        /// <param name="type">Type of the class / ע���������</param>
        /// <param name="impl">The type that implements <paramref name="type"/> / �� <paramref name="type"/>��ʵ��</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Checks whether given type is registered before.
        /// �������������Ƿ��Ѿ�ע��
        /// </summary>
        /// <param name="type">Type to check / ��Ҫ��������</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// �������������Ƿ��Ѿ�ע��
        /// </summary>
        /// <typeparam name="TType">Type to check / ��Ҫ��������</typeparam>
        bool IsRegistered<TType>();
    }
}