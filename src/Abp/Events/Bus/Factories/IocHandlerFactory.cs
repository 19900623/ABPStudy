using System;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to get/release handlers using Ioc.
    /// ��<see cref="IEventHandlerFactory"/>ʵ�֣�ʹ��Ioc��ȡ���ͷ��¼�������
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// Type of the handler.
        /// �¼�����������
        /// </summary>
        public Type HandlerType { get; private set; }

        /// <summary>
        /// IOC������
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// Creates a new instance of <see cref="IocHandlerFactory"/> class.
        /// ����һ���µ�<see cref="IocHandlerFactory"/>����.
        /// </summary>
        /// <param name="iocResolver">IOC������</param>
        /// <param name="handlerType">Type of the handler / �¼�����������</param>
        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType)
        {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        /// <summary>
        /// Resolves handler object from Ioc container.
        /// ��IOC�����н����¼�����������
        /// </summary>
        /// <returns>Resolved handler object / �¼�����������</returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        /// <summary>
        /// Releases handler object using Ioc container.
        /// ʹ��IOC�������ͷ��¼�����������
        /// </summary>
        /// <param name="handler">Handler to be released / �¼�����������</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}