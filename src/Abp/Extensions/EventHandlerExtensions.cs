using System;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="EventHandler"/>.
    /// <see cref="EventHandler"/>��չ����.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raises given event safely with given arguments.
        /// ʹ�ø����Ĳ�������ȫ�����������¼�
        /// </summary>
        /// <param name="eventHandler">The event handler / �¼����(�¼���������</param>
        /// <param name="sender">Source of the event / �¼�Դ</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Raises given event safely with given arguments.
        /// ʹ�ø����Ĳ�������ȫ�����������¼�
        /// </summary>
        /// <param name="eventHandler">The event handler / �¼����(�¼���������</param>
        /// <param name="sender">Source of the event / �¼�Դ</param>
        /// <param name="e">Event argument / �¼�����</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// Raises given event safely with given arguments.
        /// ʹ�ø����Ĳ�������ȫ�����������¼�
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the <see cref="EventArgs"/> / <see cref="EventArgs"/>������</typeparam>
        /// <param name="eventHandler">The event handler / �¼����(�¼���������</param>
        /// <param name="sender">Source of the event / �¼�Դ</param>
        /// <param name="e">Event argument / �¼�����</param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }
    }
}