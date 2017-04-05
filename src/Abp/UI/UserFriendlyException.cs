using System;
using System.Runtime.Serialization;
using Abp.Logging;

namespace Abp.UI
{
    /// <summary>
    /// This exception type is directly shown to the user.
    /// ����쳣������ֱ�����û���ʾ��
    /// </summary>
    [Serializable]
    public class UserFriendlyException : AbpException, IHasLogSeverity, IHasErrorCode
    {
        /// <summary>
        /// Additional information about the exception.
        /// �쳣�Ķ�����Ϣ
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// An arbitrary error code.
        /// ���������
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Severity of the exception.Default: Warn.
        /// �쳣����Ĭ��:����
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        public UserFriendlyException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor for serializing.
        /// ���캯��-�������л�
        /// </summary>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        public UserFriendlyException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        /// <param name="severity">Exception severity / �쳣����</param>
        public UserFriendlyException(string message, LogSeverity severity)
            : base(message)
        {
            Severity = severity;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="code">Error code / ������</param>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        /// <param name="details">Additional information about the exception / �����쳣�ĸ�����Ϣ</param>
        public UserFriendlyException(string message, string details)
            : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="code">Error code / ������</param>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        /// <param name="details">Additional information about the exception / �����쳣�ĸ�����Ϣ</param>
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        /// <param name="innerException">Inner exception / �ڲ��쳣</param>
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="message">Exception message / �쳣��Ϣ</param>
        /// <param name="details">Additional information about the exception / �����쳣�ĸ�����Ϣ</param>
        /// <param name="innerException">Inner exception / �ڲ��쳣</param>
        public UserFriendlyException(string message, string details, Exception innerException)
            : this(message, innerException)
        {
            Details = details;
        }
    }
}