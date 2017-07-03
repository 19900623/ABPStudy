using System.Collections.Generic;
using Abp.Localization.Dictionaries;

namespace Abp.Localization
{
    /// <summary>
    /// Extends <see cref="ILocalizationDictionary"/> to add tenant and database based localization.
    /// <see cref="ILocalizationDictionary"/>����չ��������̻��ͻ������ݿ�ı��ػ�
    /// </summary>
    public interface IMultiTenantLocalizationDictionary : ILocalizationDictionary
    {
        /// <summary>
        /// ��ȡһ��<see cref="LocalizedString"/>
        /// </summary>
        /// <param name="tenantId">TenantId or null for host. / �̻�ID��Null(�����̻�)</param>
        /// <param name="name">Localization key name. / ���ػ�Key����</param>
        LocalizedString GetOrNull(int? tenantId, string name);

        /// <summary>
        /// Gets all <see cref="LocalizedString"/>s.
        /// ��ȡ���е�<see cref="LocalizedString"/>
        /// </summary>
        /// <param name="tenantId">TenantId or null for host. / �̻�ID��Null(�����̻�)</param>
        IReadOnlyList<LocalizedString> GetAllStrings(int? tenantId);
    }
}