using System;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// Used to unregister a <see cref="IEventHandlerFactory"/> on <see cref="Dispose"/> method.
    /// ʹ�÷���<see cref="Dispose"/>��ע��<see cref="IEventHandlerFactory"/> 
    /// </summary>
    internal class FactoryUnregistrar : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="eventType"></param>
        /// <param name="factory"></param>
        public FactoryUnregistrar(IEventBus eventBus, Type eventType, IEventHandlerFactory factory)
        {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }

        /// <summary>
        /// ע���¼�������
        /// </summary>
        public void Dispose()
        {
            _eventBus.Unregister(_eventType, _factory);
        }
    }
}