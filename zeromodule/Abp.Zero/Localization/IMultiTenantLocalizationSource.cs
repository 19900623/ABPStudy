using System.Globalization;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// Extends <see cref="ILocalizationSource"/> to add tenant and database based localization.
    /// <see cref="ILocalizationSource"/>����չ��������̻��ͻ������ݿ�ı��ػ�
    /// </summary>
    public interface IMultiTenantLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Gets a <see cref="LocalizedString"/>.
        /// ��ȡ<see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host. / �̻�ID��Null(�����̻�)</param>
        /// <param name="name">Localization key name. / ���ػ�Key����</param>
        /// <param name="culture">Culture / ������Ϣ</param>
        string GetString(int? tenantId, string name, CultureInfo culture);

        /// <summary>
        /// Gets a <see cref="LocalizedString"/>.
        /// ��ȡ<see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="tenantId">TenantId or null for host. / �̻�ID��Null(�����̻�)</param>
        /// <param name="name">Localization key name. / ���ػ�Key����</param>
        /// <param name="culture">Culture / ������Ϣ</param>
        /// <param name="tryDefaults">True: fallbacks to default languages if can not find in given culture / True���������ָ����������Ϣ�����ҵ��򷵻�Ĭ������</param>
        string GetStringOrNull(int? tenantId, string name, CultureInfo culture, bool tryDefaults = true);
    }
}