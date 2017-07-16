using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Derrick.Auditing.Dto;
using Derrick.Dto;

namespace Derrick.Auditing
{
    /// <summary>
    /// �����־APP����
    /// </summary>
    public interface IAuditLogAppService : IApplicationService
    {
        /// <summary>
        /// ��ȡ�����־�б�(����ҳ)
        /// </summary>
        /// <param name="input">�����־�������</param>
        /// <returns></returns>
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        /// <summary>
        /// ��ȡ������Excel�����־
        /// </summary>
        /// <param name="input">�����־�������</param>
        /// <returns></returns>
        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}