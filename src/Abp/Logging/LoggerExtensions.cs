using System;
using Castle.Core.Logging;

namespace Abp.Logging
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/>.
    /// <see cref="ILogger"/>����չ
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="logger">��¼��־�Ķ���</param>
        /// <param name="severity">��־����</param>
        /// <param name="message">��־��Ϣ</param>
        public static void Log(this ILogger logger, LogSeverity severity, string message)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.Fatal(message);
                    break;
                case LogSeverity.Error:
                    logger.Error(message);
                    break;
                case LogSeverity.Warn:
                    logger.Warn(message);
                    break;
                case LogSeverity.Info:
                    logger.Info(message);
                    break;
                case LogSeverity.Debug:
                    logger.Debug(message);
                    break;
                default:
                    throw new AbpException("Unknown LogSeverity value: " + severity);
            }
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="logger">��¼��־�Ķ���</param>
        /// <param name="severity">��־����</param>
        /// <param name="message">��־��Ϣ</param>
        /// <param name="exception">�쳣����</param>
        public static void Log(this ILogger logger, LogSeverity severity, string message, Exception exception)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.Fatal(message, exception);
                    break;
                case LogSeverity.Error:
                    logger.Error(message, exception);
                    break;
                case LogSeverity.Warn:
                    logger.Warn(message, exception);
                    break;
                case LogSeverity.Info:
                    logger.Info(message, exception);
                    break;
                case LogSeverity.Debug:
                    logger.Debug(message, exception);
                    break;
                default:
                    throw new AbpException("Unknown LogSeverity value: " + severity);
            }
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="logger">��¼��־�Ķ���</param>
        /// <param name="severity">��־����</param>
        /// <param name="messageFactory">��¼��־�ķ���</param>
        public static void Log(this ILogger logger, LogSeverity severity, Func<string> messageFactory)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.Fatal(messageFactory);
                    break;
                case LogSeverity.Error:
                    logger.Error(messageFactory);
                    break;
                case LogSeverity.Warn:
                    logger.Warn(messageFactory);
                    break;
                case LogSeverity.Info:
                    logger.Info(messageFactory);
                    break;
                case LogSeverity.Debug:
                    logger.Debug(messageFactory);
                    break;
                default:
                    throw new AbpException("Unknown LogSeverity value: " + severity);
            }
        }
    }
}