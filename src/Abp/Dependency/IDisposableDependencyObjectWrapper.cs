using System;

namespace Abp.Dependency
{
    /// <summary>
    /// This interface is used to wrap an object that is resolved from IOC container.
    /// ��װ��IOC�����н������Ķ���
    /// It inherits <see cref="IDisposable"/>, so resolved object can be easily released.
    /// ���̳����ӿ�<see cref="IDisposable"/>, ��ˣ����������Ķ��󣬽������ױ��ͷš�
    /// In <see cref="IDisposable.Dispose"/> method, <see cref="IIocResolver.Release"/> is called to dispose the object.
    /// �ڷ���<see cref="IDisposable.Dispose"/>��,�����÷���<see cref="IIocResolver.Release"/>���ٶ���
    /// This is non-generic version of <see cref="IDisposableDependencyObjectWrapper{T}"/> interface.
    /// �����ǽӿ�<see cref="IDisposableDependencyObjectWrapper{T}"/> �ķǷ��Ͱ汾.
    /// </summary>
    public interface IDisposableDependencyObjectWrapper : IDisposableDependencyObjectWrapper<object>
    {

    }
}