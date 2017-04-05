namespace Abp.Logging
{
    /// <summary>
    /// Interface to define a <see cref="Severity"/> property (see <see cref="LogSeverity"/>).
    /// ����һ����־���س̶ȵĽӿ�
    /// </summary>
    public interface IHasLogSeverity
    {
        /// <summary>
        /// Log severity.
        /// ��־���س̶�
        /// </summary>
        LogSeverity Severity { get; set; }
    }
}