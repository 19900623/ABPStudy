using System.Globalization;
using System.Threading.Tasks;

namespace Abp.Localization
{
    /// <summary>
    /// Manages localization texts for host and tenants.
    /// �����������⻧�ı��ػ��ı�
    /// </summary>
    public interface IApplicationLanguageTextManager
    {
        /// <summary>
        /// Gets a localized string value.
        /// ��ȡһ�����ػ��ַ���ֵ
        /// </summary>
        /// <param name="tenantId">TenantId or null for host / �̻�ID��Null(�����̻�)</param>
        /// <param name="sourceName">Source name / Դ����</param>
        /// <param name="culture">Culture / �����Ļ���Ϣ</param>
        /// <param name="key">Localization key / ���ػ��ַ���Key</param>
        /// <param name="tryDefaults">True: fallbacks to default languages if can not find in given culture / True�������ָ����������û���ҵ��򷵻�Ĭ������</param>
        string GetStringOrNull(int? tenantId, string sourceName, CultureInfo culture, string key, bool tryDefaults = true);

        /// <summary>
        /// Updates a localized string value.
        /// ����һ�����ػ��ַ���ֵ
        /// </summary>
        /// <param name="tenantId">TenantId or null for host / �̻�ID��Null(�����̻�)</param>
        /// <param name="sourceName">Source name / Դ����</param>
        /// <param name="culture">Culture / �����Ļ���Ϣ</param>
        /// <param name="key">Localization key / ���ػ��ַ���Key</param>
        /// <param name="value">New localized value. / �±��ػ�ֵ</param>
        Task UpdateStringAsync(int? tenantId, string sourceName, CultureInfo culture, string key, string value);
    }
}