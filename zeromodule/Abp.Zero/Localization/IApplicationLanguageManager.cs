using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Localization
{
    /// <summary>
    /// Manages host and tenant languages.
    /// �����������̻�������
    /// </summary>
    public interface IApplicationLanguageManager
    {
        /// <summary>
        /// Gets list of all languages available to given tenant (or null for host)
        /// ��ȡ�����̻����õ����������б�(host��Ϊnull)
        /// </summary>
        /// <param name="tenantId">TenantId or null for host / �̻�ID��Null(�̻��������̻�)</param>
        Task<IReadOnlyList<ApplicationLanguage>> GetLanguagesAsync(int? tenantId);

        /// <summary>
        /// Adds a new language.
        /// ���һ��������
        /// </summary>
        /// <param name="language">Ӧ�ó������Զ���.</param>
        Task AddAsync(ApplicationLanguage language);

        /// <summary>
        /// Deletes a language.
        /// ɾ��һ������
        /// </summary>
        /// <param name="tenantId">�̻�ID��Null(�̻��������̻�).</param>
        /// <param name="languageName">���Ե�����.</param>
        Task RemoveAsync(int? tenantId, string languageName);

        /// <summary>
        /// Updates a language.
        /// ����һ������
        /// </summary>
        /// <param name="tenantId">�̻�ID��Null(�̻��������̻�).</param>
        /// <param name="language">The language to be updated / ��Ҫ�����µ����Զ���</param>
        Task UpdateAsync(int? tenantId, ApplicationLanguage language);

        /// <summary>
        /// Gets the default language or null for a tenant or the host.
        /// ��ȡ�̻���Ĭ�����ԡ�����̻��������̻���ΪNull
        /// </summary>
        /// <param name="tenantId">Tenant Id of null for host / �̻�ID��Null(�̻��������̻�)</param>
        Task<ApplicationLanguage> GetDefaultLanguageOrNullAsync(int? tenantId);

        /// <summary>
        /// Sets the default language for a tenant or the host.
        /// Ϊ�̻�(�����̻�)����Ĭ�����ԡ�
        /// </summary>
        /// <param name="tenantId">Tenant Id of null for host / �̻�ID��Null(�̻��������̻�)</param>
        /// <param name="languageName">Name of the language. / ���Ե�����</param>
        Task SetDefaultLanguageAsync(int? tenantId, string languageName);
    }
}