using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;

namespace Abp.Net.Mail
{
    /// <summary>
    /// This class can be used as base to implement <see cref="IEmailSender"/>.
    /// ����Ϊʵ�ֽӿ�<see cref="IEmailSender"/>�Ļ���
    /// </summary>
    public abstract class EmailSenderBase : IEmailSender
    {
        /// <summary>
        /// �ʼ�����
        /// </summary>
        private readonly IEmailSenderConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="configuration">Configuration</param>
        protected EmailSenderBase(IEmailSenderConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(_configuration.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(_configuration.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        public async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        /// <param name="mail">��Ҫ���͵ĵ����ʼ�</param>
        /// <param name="normalize">
        /// �Ƿ���Ҫ�淶�������ʼ�?�����Ҫ���������õ�ַ/���ƣ��������ǰû�����ã������õ����ʼ�����ΪUTF-8��
        /// </param>
        public async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            await SendEmailAsync(mail);
        }

        /// <summary>
        /// ����һ������ʼ�
        /// </summary>
        /// <param name="mail">��Ҫ���͵ĵ����ʼ�</param>
        /// <param name="normalize">
        /// �Ƿ���Ҫ�淶�������ʼ�?�����Ҫ���������õ�ַ/���ƣ��������ǰû�����ã������õ����ʼ�����ΪUTF-8��
        /// </param>
        public void Send(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeMail(mail);
            }

            SendEmail(mail);
        }

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// �ڼ̳�������Ҫʵ�ִ˷���
        /// </summary>
        /// <param name="mail">Mail to be sent / ��Ҫ���͵ĵ����ʼ�</param>
        protected abstract Task SendEmailAsync(MailMessage mail);

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// �ڼ̳�������Ҫʵ�ִ˷���
        /// </summary>
        /// <param name="mail">Mail to be sent / ��Ҫ���͵ĵ����ʼ�</param>
        protected abstract void SendEmail(MailMessage mail);

        /// <summary>
        /// Normalizes given email.Fills <see cref="MailMessage.From"/> if it's not filled before.Sets encodings to UTF8 if they are not set before.
        /// �淶���������ʼ�,���û����д<see cref="MailMessage.From"/>�����ᱻ�ϡ����û�����ñ��룬���ᱻ���ó�UTF8
        /// </summary>
        /// <param name="mail">Mail to be normalized / ���ᱻ�淶�����ʼ�</param>
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(
                    _configuration.DefaultFromAddress,
                    _configuration.DefaultFromDisplayName,
                    Encoding.UTF8
                    );
            }

            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
    }
}