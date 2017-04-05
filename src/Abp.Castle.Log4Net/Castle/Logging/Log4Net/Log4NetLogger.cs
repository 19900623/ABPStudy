using System;
using System.Globalization;
using log4net;
using log4net.Core;
using log4net.Util;
using ILogger = Castle.Core.Logging.ILogger;

namespace Abp.Castle.Logging.Log4Net
{
    /// <summary>
    /// Log4Net��־��¼��
    /// </summary>
    [Serializable]
    public class Log4NetLogger : MarshalByRefObject, ILogger
    {
        /// <summary>
        /// ��־��¼����������
        /// </summary>
        private static readonly Type DeclaringType = typeof(Log4NetLogger);

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger">��־��¼��</param>
        /// <param name="factory">Log4Net��־����</param>
        public Log4NetLogger(log4net.Core.ILogger logger, Log4NetLoggerFactory factory)
        {
            Logger = logger;
            Factory = factory;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public Log4NetLogger()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="log">��־��¼�ӿ�</param>
        /// <param name="factory">Log4Net��־����</param>
        public Log4NetLogger(ILog log, Log4NetLoggerFactory factory)
            : this(log.Logger, factory)
        {
        }

        /// <summary>
        /// Debug�Ƿ����
        /// </summary>
        public bool IsDebugEnabled
        {
            get { return Logger.IsEnabledFor(Level.Debug); }
        }

        /// <summary>
        /// Error�Ƿ����
        /// </summary>
        public bool IsErrorEnabled
        {
            get { return Logger.IsEnabledFor(Level.Error); }
        }

        /// <summary>
        /// Fatal�Ƿ����
        /// </summary>
        public bool IsFatalEnabled
        {
            get { return Logger.IsEnabledFor(Level.Fatal); }
        }

        /// <summary>
        /// Info�Ƿ����
        /// </summary>
        public bool IsInfoEnabled
        {
            get { return Logger.IsEnabledFor(Level.Info); }
        }

        /// <summary>
        /// Warn�Ƿ����
        /// </summary>
        public bool IsWarnEnabled
        {
            get { return Logger.IsEnabledFor(Level.Warn); }
        }

        /// <summary>
        /// Log4Net��־����
        /// </summary>
        protected internal Log4NetLoggerFactory Factory { get; set; }

        /// <summary>
        /// Log4Net��־��¼��
        /// </summary>
        protected internal log4net.Core.ILogger Logger { get; set; }

        public override string ToString()
        {
            return Logger.ToString();
        }

        /// <summary>
        /// ��������־��¼��
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual global::Castle.Core.Logging.ILogger CreateChildLogger(string name)
        {
            return Factory.Create(Logger.Name + "." + name);
        }

        /// <summary>
        /// д��Debug��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, message, null);
            }
        }

        /// <summary>
        /// д��Debug��Ϣ��־
        /// </summary>
        /// <param name="messageFactory">д��Ϣ�ķ���</param>
        public void Debug(Func<string> messageFactory)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, messageFactory.Invoke(), null);
            }
        }

        /// <summary>
        /// д��Debug��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exception">�쳣����</param>
        public void Debug(string message, Exception exception)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, message, exception);
            }
        }

        /// <summary>
        /// д���ʽ��Debug��Ϣ��־
        /// </summary>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void DebugFormat(string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Debug��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void DebugFormat(Exception exception, string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// д���ʽ��Debug��Ϣ��־
        /// </summary>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void DebugFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, new SystemStringFormat(formatProvider, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Debug��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                Logger.Log(DeclaringType, Level.Debug, new SystemStringFormat(formatProvider, format, args), exception);
            }
        }

        /// <summary>
        /// д��Error��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public void Error(string message)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, message, null);
            }
        }

        /// <summary>
        /// д��Error��Ϣ��־
        /// </summary>
        /// <param name="messageFactory">д��Ϣ�ķ���</param>
        public void Error(Func<string> messageFactory)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, messageFactory.Invoke(), null);
            }
        }

        /// <summary>
        /// д��Error��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exception">�쳣����</param>
        public void Error(string message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, message, exception);
            }
        }

        /// <summary>
        /// д���ʽ��Error��Ϣ��־
        /// </summary>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void ErrorFormat(string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Error��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣</param>
        /// <param name="format">��ʽ�ַ���</param>
        /// <param name="args">����</param>
        public void ErrorFormat(Exception exception, string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// д���ʽ��Error��Ϣ��־
        /// </summary>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ�ַ���</param>
        /// <param name="args">����</param>
        public void ErrorFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, new SystemStringFormat(formatProvider, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Error��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ�ַ���</param>
        /// <param name="args">����</param>
        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.Log(DeclaringType, Level.Error, new SystemStringFormat(formatProvider, format, args), exception);
            }
        }

        /// <summary>
        /// д��Fatal��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public void Fatal(string message)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, message, null);
            }
        }

        /// <summary>
        /// д��Fatal��Ϣ��־
        /// </summary>
        /// <param name="messageFactory">д��Ϣ�ķ���</param>
        public void Fatal(Func<string> messageFactory)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, messageFactory.Invoke(), null);
            }
        }

        /// <summary>
        /// д��Fatal��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exception">�쳣����</param>
        public void Fatal(string message, Exception exception)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, message, exception);
            }
        }

        /// <summary>
        /// д���ʽ��Fatal��Ϣ��־
        /// </summary>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void FatalFormat(string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Fatal��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void FatalFormat(Exception exception, string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// д���ʽ��Fatal��Ϣ��־
        /// </summary>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void FatalFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, new SystemStringFormat(formatProvider, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Fatal��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                Logger.Log(DeclaringType, Level.Fatal, new SystemStringFormat(formatProvider, format, args), exception);
            }
        }

        /// <summary>
        /// д��Info��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public void Info(string message)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, message, null);
            }
        }

        /// <summary>
        /// д��Info��Ϣ��־
        /// </summary>
        /// <param name="messageFactory">д��Ϣ����</param>
        public void Info(Func<string> messageFactory)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, messageFactory.Invoke(), null);
            }
        }

        /// <summary>
        /// д��Info��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exception">�쳣����</param>
        public void Info(string message, Exception exception)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, message, exception);
            }
        }

        /// <summary>
        /// д���ʽ��Info��Ϣ��־
        /// </summary>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void InfoFormat(string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Info��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void InfoFormat(Exception exception, string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// д���ʽ��Info��Ϣ��־
        /// </summary>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void InfoFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, new SystemStringFormat(formatProvider, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Info��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                Logger.Log(DeclaringType, Level.Info, new SystemStringFormat(formatProvider, format, args), exception);
            }
        }

        /// <summary>
        /// д��Warn��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        public void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, message, null);
            }
        }

        /// <summary>
        /// д��Warn��Ϣ��־
        /// </summary>
        /// <param name="messageFactory">д��Ϣ����</param>
        public void Warn(Func<string> messageFactory)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, messageFactory.Invoke(), null);
            }
        }

        /// <summary>
        /// д��Warn��Ϣ��־
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="exception">�쳣����</param>
        public void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, message, exception);
            }
        }

        /// <summary>
        /// д���ʽ��Warn��Ϣ��־
        /// </summary>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void WarnFormat(string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Warn��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void WarnFormat(Exception exception, string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), exception);
            }
        }

        /// <summary>
        /// д���ʽ��Warn��Ϣ��־
        /// </summary>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void WarnFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, new SystemStringFormat(formatProvider, format, args), null);
            }
        }

        /// <summary>
        /// д���ʽ��Warn��Ϣ��־
        /// </summary>
        /// <param name="exception">�쳣����</param>
        /// <param name="formatProvider">��ʽ���ṩ��</param>
        /// <param name="format">��ʽ���ַ���</param>
        /// <param name="args">����</param>
        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                Logger.Log(DeclaringType, Level.Warn, new SystemStringFormat(formatProvider, format, args), exception);
            }
        }
    }
}