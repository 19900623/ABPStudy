using System;
using System.Threading.Tasks;
using Abp.Events.Bus.Factories;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus
{
    /// <summary>
    /// Defines interface of the event bus.
    /// �����¼����ߵĽӿ�
    /// </summary>
    public interface IEventBus
    {
        #region ע�� Register

        /// <summary>
        /// Registers to an event.Given action is called for all event occurrences.
        /// ע��һ���¼����¼�����ʱ��������action�ᱻ����
        /// </summary>
        /// <param name="action">Action to handle events / �����¼���Action</param>
        /// <typeparam name="TEventData">Event type / �¼���������</typeparam>
        IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event. Same (given) instance of the handler is used for all event occurrences.
        /// ע��һ���¼����¼�����ʱ����������ͬ���¼�����ʵ���ᱻ����
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="handler">Object to handle the event / �¼��������Ķ���</param>
        IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event.A new instance of <see cref="THandler"/> object is created for every event occurrence.
        /// ע��һ���¼�,�¼�����ʱ��һ���µ� <see cref="THandler"/>ʵ�����󱻴���
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <typeparam name="THandler">Type of the event handler / �¼�������������</typeparam>
        IDisposable Register<TEventData, THandler>() where TEventData : IEventData where THandler : IEventHandler<TEventData>, new();

        /// <summary>
        /// Registers to an event.Same (given) instance of the handler is used for all event occurrences.
        /// ע��һ���¼�,�¼�����ʱ����������ͬ���¼�����ʵ���ᱻ����
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="handler">Object to handle the event / �¼��������Ķ���</param>
        IDisposable Register(Type eventType, IEventHandler handler);

        /// <summary>
        /// Registers to an event.Given factory is used to create/release handlers
        /// ע��һ���¼�,�����Ĺ�������������/�ͷ��¼�������
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="handlerFactory">A factory to create/release handlers / ��������/�ͷ��¼��������Ĺ���</param>
        IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData;

        /// <summary>
        /// Registers to an event.
        /// ע��һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="handlerFactory">A factory to create/release handlers / ��������/�ͷ��¼��������Ĺ���</param>
        IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory);

        #endregion

        #region ע�� Unregister

        /// <summary>
        /// Unregisters from an event.
        /// ע��һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="action"></param>
        void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData;

        /// <summary>
        /// Unregisters from an event.
        /// ע��һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="handler">Handler object that is registered before / ֮ǰע����¼�����������</param>
        void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData;

        /// <summary>
        /// Unregisters from an event.
        /// ע��һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="handler">Handler object that is registered before / ֮ǰע����¼�����������</param>
        void Unregister(Type eventType, IEventHandler handler);

        /// <summary>
        /// Unregisters from an event.
        /// ע��һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="factory">Factory object that is registered before / ֮ǰע��Ĺ�������</param>
        void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData;

        /// <summary>
        /// Unregisters from an event.
        /// ע��һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="factory">Factory object that is registered before / ֮ǰע��Ĺ�������</param>
        void Unregister(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// ע��������͵����е��¼���������������
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        void UnregisterAll<TEventData>() where TEventData : IEventData;

        /// <summary>
        /// Unregisters all event handlers of given event type.
        /// ע��������͵����е��¼���������������
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        void UnregisterAll(Type eventType);

        #endregion

        #region Trigger

        /// <summary>
        /// Triggers an event.
        /// ������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event.
        /// ������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="eventSource">The object which triggers the event / �����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event.
        /// ������һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        void Trigger(Type eventType, IEventData eventData);

        /// <summary>
        /// Triggers an event.
        /// ������һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="eventSource">The object which triggers the event / �����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        void Trigger(Type eventType, object eventSource, IEventData eventData);

        /// <summary>
        /// Triggers an event asynchronously.
        /// �첽������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="eventData">Related data for the even / ���¼�����������t</param>
        /// <returns>The task to handle async operation / �����첽����������</returns>
        Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event asynchronously.
        /// �첽������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">Event type / �¼�����</typeparam>
        /// <param name="eventSource">The object which triggers the event / �����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        /// <returns>The task to handle async operation / �����첽����������</returns>
        Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData;

        /// <summary>
        /// Triggers an event asynchronously.
        /// �첽������һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        /// <returns>The task to handle async operation / �����첽����������</returns>
        Task TriggerAsync(Type eventType, IEventData eventData);

        /// <summary>
        /// Triggers an event asynchronously.
        /// �첽������һ���¼�
        /// </summary>
        /// <param name="eventType">Event type / �¼�����</param>
        /// <param name="eventSource">The object which triggers the event / �����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">Related data for the event / ���¼�����������</param>
        /// <returns>The task to handle async operation / �����첽����������</returns>
        Task TriggerAsync(Type eventType, object eventSource, IEventData eventData);


        #endregion
    }
}