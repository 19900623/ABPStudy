using Abp.Auditing;
using Derrick.Authorization.Users;

namespace Derrick.Auditing
{
    /// <summary>
    /// A helper class to store an <see cref="AuditLog"/> and a <see cref="User"/> object.
    /// �洢<see cref="AuditLog"/>��<see cref="User"/>����İ�����
    /// </summary>
    public class AuditLogAndUser
    {
        /// <summary>
        /// �����־ʵ��
        /// </summary>
        public AuditLog AuditLog { get; set; }
        /// <summary>
        /// �û�ʵ��
        /// </summary>
        public User User { get; set; }
    }
}