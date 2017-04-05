namespace Abp.Logging
{
    /// <summary>
    /// Indicates severity for log.
    /// ��־����
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Debug.
        /// ��ʾ���Ե���־����
        /// </summary>
        Debug,

        /// <summary>
        /// Info.
        /// ��ʾ��Ϣ����־����
        /// </summary>
        Info,

        /// <summary>
        /// Warn.
        /// ��ʾ�������־����
        /// </summary>
        Warn,

        /// <summary>
        /// Error.
        /// ��ʾ�������־����
        /// </summary>
        Error,

        /// <summary>
        /// Fatal.
        /// ��ʾ���ش������־����
        /// </summary>
        Fatal
    }
}