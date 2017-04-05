using System.Net.Mail;

namespace Abp.Net.Mail.Smtp
{
    /// <summary>
    /// Used to send emails over SMTP.
    /// ͨ��SMTP���͵����ʼ�
    /// </summary>
    public interface ISmtpEmailSender : IEmailSender
    {
        /// <summary>
        /// Creates and configures new <see cref="SmtpClient"/> object to send emails.
        /// ������������<see cref="SmtpClient"/> ���󣬲����͵����ʼ�
        /// </summary>
        /// <returns>
        /// An <see cref="SmtpClient"/> object that is ready to send emails.
        /// ����׼�������ʼ��Ķ���<see cref="SmtpClient"/>
        /// </returns>
        SmtpClient BuildClient();
    }
}