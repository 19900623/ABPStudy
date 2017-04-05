using Castle.Core;

namespace Abp
{
    /// <summary>
    /// Defines interface for objects those should be Initialized before using it.If the object resolved using dependency injection, <see cref="IInitializable.Initialize"/>.method is automatically called just after creation of the object.
    /// Ϊʹ��ǰ��Ҫ��ʼ���Ķ�����Ľӿڡ����ʹ������ע����ж������������<see cref="IInitializable.Initialize"/>���ڶ��󴴽����Զ�����
    /// </summary>
    public interface IShouldInitialize : IInitializable
    {
        
    }
}