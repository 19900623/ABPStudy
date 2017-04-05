using Abp.Events.Bus.Handlers;

namespace Abp.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events by a single instance object. 
    /// ����<see cref="IEventHandlerFactory"/>��ʵ�֣�ͨ�����������������¼�
    /// </summary>
    /// <remarks>
    /// This class always gets the same single instance of handler.
    /// ��������ǻ�ȡ��ͬ�ĵ����������¼�
    /// </remarks>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// The event handler instance.
        /// �¼�������ʵ��
        /// </summary>
        public IEventHandler HandlerInstance { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="handler">�¼�������ʵ��</param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        /// <summary>
        /// ��ȡһ���¼���������������
        /// </summary>
        /// <returns>�¼���������������</returns>
        public IEventHandler GetHandler()
        {
            return HandlerInstance;
        }

        /// <summary>
        /// �ͷ�һ���¼���������������
        /// </summary>
        /// <param name="handler">�����ͷŵĴ�����</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            
        }
    }
}