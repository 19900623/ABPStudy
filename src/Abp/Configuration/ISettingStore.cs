using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Configuration
{
    /// <summary>
    /// This interface is used to get/set settings from/to a data source (database).
    /// �˽ӿ����ڴ�����Դ�����ݿ⣩�л�ȡ����ֵ������ֵ���浽����Դ��
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// Gets a setting or null.
        /// ��ȡһ������ֵ��null
        /// </summary>
        /// <param name="tenantId">TenantId or null / �⻧Id��Null</param>
        /// <param name="userId">UserId or null / �û�Id��Null</param>
        /// <param name="name">Name of the setting / ���õ�����</param>
        /// <returns>Setting object / ���õ�����</returns>
        Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name);

        /// <summary>
        /// Deletes a setting.
        /// ɾ��һ������
        /// </summary>
        /// <param name="setting">Setting to be deleted / ����ɾ��������</param>
        Task DeleteAsync(SettingInfo setting);

        /// <summary>
        /// Adds a setting.
        /// ���һ������
        /// </summary>
        /// <param name="setting">Setting to add / ������ӵ�����</param>
        Task CreateAsync(SettingInfo setting);

        /// <summary>
        /// Update a setting.
        /// ����һ������
        /// </summary>
        /// <param name="setting">Setting to add / �������µ�����</param>
        Task UpdateAsync(SettingInfo setting);

        /// <summary>
        /// Gets a list of setting.
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="tenantId">TenantId or null / �⻧Id��Null</param>
        /// <param name="userId">UserId or null / �û�Id��Null</param>
        /// <returns>List of settings / �����б�</returns>
        Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId);
    }
}