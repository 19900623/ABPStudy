using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.Auditing
{
    /// <summary>
    /// ��Ƹ����ӿ�
    /// </summary>
    public interface IAuditingHelper
    {
        /// <summary>
        /// �Ƿ���Ҫ�������
        /// </summary>
        /// <param name="methodInfo">������Ϣ</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <returns></returns>
        bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false);

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="method">����</param>
        /// <param name="arguments">����</param>
        /// <returns></returns>
        AuditInfo CreateAuditInfo(MethodInfo method, object[] arguments);

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="method">����</param>
        /// <param name="arguments">����</param>
        /// <returns></returns>
        AuditInfo CreateAuditInfo(MethodInfo method, IDictionary<string, object> arguments);

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="auditInfo">�����Ϣ</param>
        void Save(AuditInfo auditInfo);

        /// <summary>
        /// �첽���������Ϣ
        /// </summary>
        /// <param name="auditInfo">�����Ϣ</param>
        /// <returns></returns>
        Task SaveAsync(AuditInfo auditInfo);
    }
}