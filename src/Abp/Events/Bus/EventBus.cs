using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Abp.Events.Bus.Factories;
using Abp.Events.Bus.Factories.Internals;
using Abp.Events.Bus.Handlers;
using Abp.Events.Bus.Handlers.Internals;
using Abp.Extensions;
using Abp.Threading.Extensions;
using Castle.Core.Logging;

namespace Abp.Events.Bus
{
    /// <summary>
    /// Implements EventBus as Singleton pattern.
    /// �¼����ߣ��Ե���ģʽʵ��
    /// </summary>
    public class EventBus : IEventBus
    {
        #region ����
        /// <summary>
        /// Gets the default <see cref="EventBus"/> instance.
        /// ��ȡĬ�ϵ� <see cref="EventBus"/> ʵ��.
        /// </summary>
        public static EventBus Default { get; } = new EventBus();

        /// <summary>
        /// Reference to the Logger.
        /// ��־����
        /// </summary>
        public ILogger Logger { get; set; }
        #endregion

        #region ˽���ֶ�
        /// <summary>
        /// All registered handler factories.
        /// ����ע��Ĵ���������
        /// Key: Type of the event Value: List of handler factories
        /// Key: �¼�����  Value: �����������б�
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<IEventHandlerFactory>> _handlerFactories;
        #endregion

        #region ���캯��
        /// <summary>
        /// Creates a new <see cref="EventBus"/> instance.
        /// ����һ�� <see cref="EventBus"/> ʵ��.
        /// Instead of creating a new instace, you can use <see cref="Default"/> to use Global <see cref="EventBus"/>.
        /// ����һ��ʵ��, �������ȫ�ַ��� <see cref="Default"/> ��ʹ�� <see cref="EventBus"/>.
        /// </summary>
        public EventBus()
        {
            _handlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
            Logger = NullLogger.Instance;
        }
        #endregion

        #region ��������

        #region ע��
        /// <summary>
        /// ע��һ���¼�
        /// �¼�����ʱ��������action�ᱻ����
        /// </summary>
        /// <param name="action">�����¼���Action</param>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return Register(typeof(TEventData), new ActionEventHandler<TEventData>(action));
        }

        /// <summary>
        /// ע��һ���¼�
        /// �¼�����ʱ����������ͬ���¼�����ʵ���ᱻ����
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">�¼��������Ķ���</param>
        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handler);
        }

        /// <summary>
        ///  ע��һ���¼�
        ///  �¼�����ʱ��һ���µ� <see cref="THandler"/>ʵ�����󱻴���
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <typeparam name="THandler">�¼�������������</typeparam>
        public IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData
            where THandler : IEventHandler<TEventData>, new()
        {
            return Register(typeof(TEventData), new TransientEventHandlerFactory<THandler>());
        }

        /// <summary>
        /// ע��һ���¼�
        /// �¼�����ʱ����������ͬ���¼�����ʵ���ᱻ����
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="handler">�¼��������Ķ���</param>
        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            return Register(eventType, new SingleInstanceHandlerFactory(handler));
        }

        /// <summary>
        /// ע��һ���¼�
        /// �����Ĺ�������������/�ͷ��¼�������
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handlerFactory">��������/�ͷ��¼��������Ĺ���</param>
        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handlerFactory);
        }

        /// <summary>
        /// ע��һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="handlerFactory">��������/�ͷ��¼��������Ĺ���</param>
        public IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories => factories.Add(handlerFactory));

            return new FactoryUnregistrar(this, eventType, handlerFactory);
        }
        #endregion

        #region ע��
        /// <summary>
        /// ע��һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="action"></param>
        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            Check.NotNull(action, nameof(action));

            GetOrCreateHandlerFactories(typeof(TEventData))
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEventData>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
                });
        }

        /// <summary>
        /// ע��һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="handler">֮ǰע����¼�����������</param>
        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), handler);
        }

        /// <summary>
        /// ע��һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="handler">֮ǰע����¼�����������</param>
        public void Unregister(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                            factory is SingleInstanceHandlerFactory &&
                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                        );
                });
        }

        /// <summary>
        /// ע��һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="factory">֮ǰע��Ĺ�������</param>
        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), factory);
        }

        /// <summary>
        /// ע��һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="factory">֮ǰע��Ĺ�������</param>
        public void Unregister(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
        }

        /// <summary>
        /// ע��������͵����е��¼���������������
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
            UnregisterAll(typeof(TEventData));
        }

        /// <summary>
        /// ע��������͵����е��¼���������������
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        public void UnregisterAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
        }
        #endregion

        #region ����
        /// <summary>
        /// ������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventData">���¼�����������</param>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            Trigger((object)null, eventData);
        }

        /// <summary>
        /// ������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventSource">�����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">���¼�����������</param>
        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            Trigger(typeof(TEventData), eventSource, eventData);
        }

        /// <summary>
        ///  ������һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventData">���¼�����������</param>
        public void Trigger(Type eventType, IEventData eventData)
        {
            Trigger(eventType, null, eventData);
        }

        /// <summary>
        ///  ������һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventSource">�����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">���¼�����������</param>
        public void Trigger(Type eventType, object eventSource, IEventData eventData)
        {
            var exceptions = new List<Exception>();

            TriggerHandlingException(eventType, eventSource, eventData, exceptions);

            if (exceptions.Any())
            {
                if (exceptions.Count == 1)
                {
                    exceptions[0].ReThrow();
                }

                throw new AggregateException("More than one error has occurred while triggering the event: " + eventType, exceptions);
            }
        }
        #endregion

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���������쳣
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventSource">�����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">���¼�����������</param>
        /// <param name="exceptions">�쳣�б�</param>
        private void TriggerHandlingException(Type eventType, object eventSource, IEventData eventData, List<Exception> exceptions)
        {
            //TODO: This method can be optimized by adding all possibilities to a dictionary.

            eventData.EventSource = eventSource;

            foreach (var handlerFactories in GetHandlerFactories(eventType))
            {
                foreach (var handlerFactory in handlerFactories.EventHandlerFactories)
                {
                    var eventHandler = handlerFactory.GetHandler();

                    try
                    {
                        if (eventHandler == null)
                        {
                            throw new Exception($"Registered event handler for event type {handlerFactories.EventType.Name} does not implement IEventHandler<{handlerFactories.EventType.Name}> interface!");
                        }

                        var handlerType = typeof(IEventHandler<>).MakeGenericType(handlerFactories.EventType);

                        var method = handlerType.GetMethod(
                            "HandleEvent",
                            BindingFlags.Public | BindingFlags.Instance,
                            null,
                            new[] { handlerFactories.EventType },
                            null
                        );

                        method.Invoke(eventHandler, new object[] { eventData });
                    }
                    catch (TargetInvocationException ex)
                    {
                        exceptions.Add(ex.InnerException);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                    finally
                    {
                        handlerFactory.ReleaseHandler(eventHandler);
                    }
                }
            }

            //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            if (eventType.IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument) eventData).GetConstructorArgs();
                    var baseEventData = (IEventData) Activator.CreateInstance(baseEventType, constructorArgs);
                    baseEventData.EventTime = eventData.EventTime;
                    Trigger(baseEventType, eventData.EventSource, baseEventData);
                }
            }
        }

        /// <summary>
        /// ��ȡ�¼�����������
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <returns></returns>
        private IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

            foreach (var handlerFactory in _handlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
            {
                handlerFactoryList.Add(new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
            }

            return handlerFactoryList.ToArray();
        }

        /// <summary>
        /// Ӧ�ô������������¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="handlerType">����������</param>
        /// <returns></returns>
        private static bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            //Should trigger same type
            if (handlerType == eventType)
            {
                return true;
            }

            //Should trigger for inherited types
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region �첽

        /// <summary>
        /// �첽������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventData">���¼�����������</param>
        /// <returns>�����첽����������</returns>
        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return TriggerAsync((object)null, eventData);
        }

        /// <summary>
        /// �첽������һ���¼�
        /// </summary>
        /// <typeparam name="TEventData">�¼�����</typeparam>
        /// <param name="eventSource">�����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">���¼�����������</param>
        /// <returns>�����첽����������</returns>
        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            ExecutionContext.SuppressFlow();

            var task = Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Trigger(eventSource, eventData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.ToString(), ex);
                    }
                });

            ExecutionContext.RestoreFlow();

            return task;
        }

        /// <summary>
        /// �첽������һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventData">���¼�����������</param>
        /// <returns>�����첽����������</returns>
        public Task TriggerAsync(Type eventType, IEventData eventData)
        {
            return TriggerAsync(eventType, null, eventData);
        }

        /// <summary>
        /// �첽������һ���¼�
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <param name="eventSource">�����¼��Ķ����¼�Դ��</param>
        /// <param name="eventData">���¼�����������</param>
        /// <returns></returns>
        public Task TriggerAsync(Type eventType, object eventSource, IEventData eventData)
        {
            ExecutionContext.SuppressFlow();

            var task = Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Trigger(eventType, eventSource, eventData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.ToString(), ex);
                    }
                });

            ExecutionContext.RestoreFlow();

            return task;
        }

        /// <summary>
        /// ��ȡ����¼�������
        /// </summary>
        /// <param name="eventType">�¼�����</param>
        /// <returns></returns>
        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return _handlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());
        }

        /// <summary>
        /// �¼���������ص��¼�����
        /// </summary>
        private class EventTypeWithEventHandlerFactories
        {
            /// <summary>
            /// �¼��б�
            /// </summary>
            public Type EventType { get; }

            /// <summary>
            /// �¼��������б�
            /// </summary>
            public List<IEventHandlerFactory> EventHandlerFactories { get; }

            /// <summary>
            /// ���캯��
            /// </summary>
            /// <param name="eventType">�¼��б�</param>
            /// <param name="eventHandlerFactories">�¼��������б�</param>
            public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactories;
            }
        }
        #endregion
    }
}