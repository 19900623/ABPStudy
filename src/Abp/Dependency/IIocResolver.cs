using System;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to resolve dependencies.
    /// Ϊ��Ҫʹ�������������ඨ��ӿ�(����ע����������ȡ��)
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the object to get / Ҫ��ȡ���������</typeparam>
        /// <returns>The object instance / ����ʵ��</returns>
        T Resolve<T>();

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the object to cast / Ҫ��ȡ���������</typeparam>
        /// <param name="type">Type of the object to resolve / Ҫ����Ķ�������</param>
        /// <returns>The object instance / ����ʵ��</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the object to get / Ҫ��ȡ���������</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments / ���캯������</param>
        /// <returns>The object instance / ����ʵ��</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <param name="type">Type of the object to get / Ҫ��ȡ���������</param>
        /// <returns>The object instance / ����ʵ��</returns>
        object Resolve(Type type);

        /// <summary>
        /// Gets an object from IOC container.Returning object must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <param name="type">Type of the object to get / Ҫ��ȡ���������</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments / ���캯������</param>
        /// <returns>The object instance / ����ʵ��</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// Gets all implementations for given type.Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the objects to resolve / Ҫ����Ķ�������</typeparam>
        /// <returns>Object instances / ����ʵ��</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// Gets all implementations for given type.Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <typeparam name="T">Type of the objects to resolve / Ҫ����Ķ�������</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments / ���캯������</param>
        /// <returns>Object instances / ����ʵ��</returns>
        T[] ResolveAll<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets all implementations for given type.Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <param name="type">Type of the objects to resolve / Ҫ����Ķ�������</param>
        /// <returns>Object instances / ����ʵ��</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        /// Gets all implementations for given type.Returning objects must be Released (see <see cref="Release"/>) after usage.
        /// ��Ioc�����л�ȡһ������.����ʹ��������ͷ�(see <see cref="Release"/>) �Ķ���
        /// </summary> 
        /// <param name="type">Type of the objects to resolve / Ҫ����Ķ�������</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments / ���캯������</param>
        /// <returns>Object instances / objects</returns>
        object[] ResolveAll(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// �ͷ�һ��֮ǰ��ȡ�Ķ�����鿴����Resolve.
        /// </summary>
        /// <param name="obj">Object to be released / ��Ҫ�ͷŵĶ���</param>
        void Release(object obj);

        /// <summary>
        /// Checks whether given type is registered before.
        /// ��������Ƿ��Ѿ�ע��
        /// </summary>
        /// <param name="type">Type to check / ��Ҫ��������</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// ��������Ƿ��Ѿ�ע��
        /// </summary>
        /// <typeparam name="T">Type to check / ��Ҫ��������</typeparam>
        bool IsRegistered<T>();
    }
}