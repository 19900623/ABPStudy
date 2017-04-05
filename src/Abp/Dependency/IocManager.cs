using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to directly perform dependency injection tasks.
    /// ������������������ע�������
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// The Singleton instance.
        /// ����ʵ��
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// Reference to the Castle Windsor Container.
        /// Castle Windsor����������
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// List of all registered conventional registrars.
        /// ��Ҫע���Լ��ע���б�
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// Creates a new <see cref="IocManager"/> object.
        /// ����һ���µ� <see cref="IocManager"/> ����.
        /// Normally, you don't directly instantiate an <see cref="IocManager"/>.This may be useful for test purposes.
        /// ͨ������£��㲻��Ҫֱ��ʵ����һ�� <see cref="IocManager"/>.����ڲ�����˵�����а���.
        /// </summary>
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //Register self!
            IocContainer.Register(
                Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this)
                );
        }

        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// ���һ��Լ��ע�������ע����
        /// </summary>
        /// <param name="registrar">dependency registrar</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="AddConventionalRegistrar"/> method.
        /// ͨ��Լ��ע�ᣬע��������򼯵����ͣ��鿴���� <see cref="AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">Assembly to register / ��Ҫע��ĳ���</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="AddConventionalRegistrar"/> method.
        /// ͨ��Լ��ע�ᣬע��������򼯵����ͣ��鿴���� <see cref="AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">Assembly to register / Ҫע��ĳ���</param>
        /// <param name="config">Additional configuration / ���������</param>
        public void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config)
        {
            var context = new ConventionalRegistrationContext(assembly, this, config);

            foreach (var registerer in _conventionalRegistrars)
            {
                registerer.RegisterAssembly(context);
            }

            if (config.InstallInstallers)
            {
                IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }

        /// <summary>
        /// Registers a type as self registration.
        /// ��ע��һ��ע������
        /// </summary>
        /// <typeparam name="TType">Type of the class / ע���������</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// Registers a type as self registration.
        /// ��ע��һ��ע������
        /// </summary>
        /// <param name="type">Type of the class / ע���������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        /// Registers a type with it's implementation.
        /// ע��һ��һ�����ͺ�����ʵ��
        /// </summary>
        /// <typeparam name="TType">Registering type / ע���������</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> / �� <see cref="TType"/>��ʵ��</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        /// <summary>
        /// Registers a type with it's implementation.
        /// ��ע��һ��һ�����ͺ�����ʵ��
        /// </summary>
        /// <param name="type">Type of the class / ע���������</param>
        /// <param name="impl">The type that implements <paramref name="type"/> / �� <paramref name="type"/>��ʵ��</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type / �������������</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        /// Checks whether given type is registered before.
        /// �������������Ƿ��Ѿ�ע��
        /// </summary>
        /// <param name="type">Type to check / ��Ҫ��������</param>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// Checks whether given type is registered before.
        /// �������������Ƿ��Ѿ�ע��
        /// </summary>
        /// <typeparam name="TType">Type to check / ��Ҫ��������</typeparam>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the object to get / Ҫ��ȡ���������</typeparam>
        /// <returns>The instance object / ����ʵ��</returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the object to cast / Ҫת���Ķ�������</typeparam>
        /// <param name="type">Type of the object to resolve / Ҫ����Ķ�������</param>
        /// <returns>The object instance / ����ʵ��</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the object to get / Ҫ��ȡ���������</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments / ���캯������</param>
        /// <returns>The instance object / ����ʵ��</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <param name="type">Type of the object to get / Ҫ��ȡ���������</param>
        /// <returns>The instance object / ����ʵ��</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <param name="type">Type of the object to get / Ҫ��ȡ���������</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments / ���캯������</param>
        /// <returns>The instance object / ����ʵ��</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        /// <summary>
        /// ����������ƥ���������Ч���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        /// <summary>
        /// ����������ƥ���������Ч���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns></returns>
        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// �����˷�����ƥ���������Ч���
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        /// <summary>
        /// ���������˷�����ƥ��ķ������Ч�����ƥ�����
        /// </summary>
        /// <param name="type">����</param>
        /// <param name="argumentsAsAnonymousType">���캯������</param>
        /// <returns></returns>
        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.ResolveAll(type, argumentsAsAnonymousType).Cast<object>().ToArray();
        }

        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// �ͷŷ���Resolve��ȡ�Ķ��󡣲鿴Resolve����
        /// </summary>
        /// <param name="obj">Object to be released</param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        /// <summary>
        /// Ӧ����������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="registration">�������齨ע�Ṥ��</param>
        /// <param name="lifeStyle">��������ö��</param>
        /// <returns></returns>
        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}