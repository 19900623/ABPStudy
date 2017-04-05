using System;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events by a single instance object. 
    /// ͨ��������ʵ�������������¼�
    /// </summary>
    /// <remarks>
    /// This class always gets the same single instance of handler.
    /// ��������ǻ�ȡһ���µĵĴ�����ʵ��
    /// </remarks>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory 
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// Creates a new instance of the handler object.
        /// ����һ���¼���������ʵ��
        /// </summary>
        /// <returns>The handler object / �¼�����������</returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        /// <summary>
        /// Disposes the handler object if it's <see cref="IDisposable"/>. Does nothing if it's not.
        /// ����¼�������ʵ���� <see cref="IDisposable"/>�������ٶ������û�У���ʲôҲ����
        /// </summary>
        /// <param name="handler">Handler to be released / Ҫ�ͷŵ��¼�������ʵ��</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}