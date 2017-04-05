using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;

namespace Abp.Net.Mail.Smtp
{
    /// <summary>
    /// Used to send emails over SMTP.
    /// ʹ��SMTP���͵����ʼ�
    /// </summary>
    public class SmtpEmailSender : EmailSenderBase, ISmtpEmailSender, ITransientDependency
    {
        /// <summary>
        /// SMTP�ʼ���������
        /// </summary>
        private readonly ISmtpEmailSenderConfiguration _configuration;

        /// <summary>
        /// Creates a new <see cref="SmtpEmailSender"/>.
        /// ���캯��
        /// </summary>
        /// <param name="configuration">Configuration / SMTP�ʼ���������</param>
        public SmtpEmailSender(ISmtpEmailSenderConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ������������<see cref="SmtpClient"/> ���󣬲����͵����ʼ�
        /// </summary>
        /// <returns>����׼�������ʼ��Ķ���<see cref="SmtpClient"/></returns>
        public SmtpClient BuildClient()
        {
            var host = _configuration.Host;
            var port = _configuration.Port;

            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (_configuration.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (_configuration.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;

                    var userName = _configuration.UserName;
                    if (!userName.IsNullOrEmpty())
                    {
                        var password = _configuration.Password;
                        var domain = _configuration.Domain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(userName, password, domain)
                            : new NetworkCredential(userName, password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        /// <summary>
        /// �ڼ̳�������Ҫʵ�ִ˷��� 
        /// </summary>
        /// <param name="mail">��Ҫ���͵ĵ����ʼ�</param>
        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }

        /// <summary>
        /// �ڼ̳�������Ҫʵ�ִ˷��� 
        /// </summary>
        /// <param name="mail">��Ҫ���͵ĵ����ʼ�</param>
        protected override void SendEmail(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                smtpClient.Send(mail);
            }
        }
    }
}