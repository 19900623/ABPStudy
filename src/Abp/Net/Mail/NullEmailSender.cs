using System.Net.Mail;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Abp.Net.Mail
{
    /// <summary>
    /// This class is an implementation of <see cref="IEmailSender"/> as similar to null pattern.It does not send emails but logs them.
    /// �����ǽӿ� <see cref="IEmailSender"/>��nullģʽʵ�֣����������ʼ��������¼�ʼ���
    /// </summary>
    public class NullEmailSender : EmailSenderBase
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullEmailSender"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="configuration">Configuration / �ʼ�����</param>
        public NullEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        /// <param name="mail">�ʼ�</param>
        /// <returns></returns>
        protected override Task SendEmailAsync(MailMessage mail)
        {
            Logger.Warn("USING NullEmailSender!");
            Logger.Debug("SendEmailAsync:");
            LogEmail(mail);
            return Task.FromResult(0);
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        /// <param name="mail">�ʼ�</param>
        protected override void SendEmail(MailMessage mail)
        {
            Logger.Warn("USING NullEmailSender!");
            Logger.Debug("SendEmail:");
            LogEmail(mail);
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        /// <param name="mail">�ʼ�</param>
        private void LogEmail(MailMessage mail)
        {
            Logger.Debug(mail.To.ToString());
            Logger.Debug(mail.CC.ToString());
            Logger.Debug(mail.Subject);
            Logger.Debug(mail.Body);
        }
    }
}