using System.Collections.Generic;
using Derrick.Authorization.Users.Dto;
using Derrick.Dto;

namespace Derrick.Authorization.Users.Exporting
{
    /// <summary>
    /// �û��б�Excel������
    /// </summary>
    public interface IUserListExcelExporter
    {
        /// <summary>
        /// ���û��б�����Excel
        /// </summary>
        /// <param name="userListDtos">�û�dto�б�</param>
        /// <returns></returns>
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}